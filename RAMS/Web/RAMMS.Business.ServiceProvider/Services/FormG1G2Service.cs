using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Common;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.Repository.Interfaces;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormG1G2Service : IFormG1G2Service
    {
        private readonly IFormG1Repository _repo;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly IAssetsService _assetsService;
        private readonly IProcessService processService;
        private readonly ISecurity _security;
        public FormG1G2Service(IRepositoryUnit repoUnit, IFormG1Repository repo,
            IAssetsService assetsService, IMapper mapper, IProcessService proService,
            ISecurity security)
        {
            _repo = repo;
            _mapper = mapper;
            _assetsService = assetsService;
            _repoUnit = repoUnit;
            processService = proService;
            _security = security;
        }
        public async Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData)
        {
            return await _repo.GetHeaderGrid(searchData);
        }
        public async Task<FormG1DTO> Save(FormG1DTO frmG1G2, bool updateSubmit)
        {
            RmFormG1Hdr frmG1G2_1 = this._mapper.Map<RmFormG1Hdr>((object)frmG1G2);
            frmG1G2_1 = UpdateStatus(frmG1G2_1);

            RmFormG2Hdr frmG2 = this._mapper.Map<RmFormG2Hdr>(frmG1G2.FormG2);
            frmG2.Fg2hPkRefNo = frmG1G2.FormG2.PkRefNo;
            frmG2.Fg2hFg1hPkRefNo = frmG1G2.FormG2.Fg1hPkRefNo;


            RmFormG1Hdr source = await this._repo.Save(frmG1G2_1, updateSubmit);

            RmFormG2Hdr sourceG2 = await this._repo.SaveG2(frmG2, updateSubmit);

            //if (source != null && source.Fg1hSubmitSts)
            //{
            //    int result = this.processService.Save(new ProcessDTO()
            //    {
            //        ApproveDate = new System.DateTime?(System.DateTime.Now),
            //        Form = "FormG1G2",
            //        IsApprove = true,
            //        RefId = source.Fg1hPkRefNo,
            //        Remarks = "",
            //        Stage = source.Fg1hStatus
            //    }).Result;
            //}
            frmG1G2 = this._mapper.Map<FormG1DTO>((object)source);
            return frmG1G2;
        }

        public RmFormG1Hdr UpdateStatus(RmFormG1Hdr form)
        {
            if (form.Fg1hPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormG1Repository._context.RmFormG1Hdr.Where(x => x.Fg1hPkRefNo == form.Fg1hPkRefNo).Select(x => new { Status = x.Fg1hStatus, Log = x.Fg1hAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.Fg1hAuditLog = existsObj.Log;
                    form.Fg1hStatus = existsObj.Status;
                }

            }


            if (form.Fg1hSubmitSts && (string.IsNullOrEmpty(form.Fg1hStatus) || form.Fg1hStatus == Common.StatusList.FormQA1Saved || form.Fg1hStatus == Common.StatusList.FormQA1Rejected))
            {
                //form.Fg1hInspectedBy = _security.UserID;
                //form.Fg1hInspectedName = _security.UserName;
                //form.Fg1hInspectedDt = DateTime.Today;
                form.Fg1hStatus = Common.StatusList.FormQA1Submitted;
                form.Fg1hAuditLog = Utility.ProcessLog(form.Fg1hAuditLog, "Submitted", "Submitted", form.Fg1hInspectedName, string.Empty, form.Fg1hInspectedDt, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Executed By:" + form.Fg1hInspectedName + " - Form G1 (" + form.Fg1hPkRefNo + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/FormG1G2/Edit/" + form.Fg1hPkRefNo.ToString() + "?view=1",
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }
            else if (string.IsNullOrEmpty(form.Fg1hStatus) || form.Fg1hStatus == "Initialize")
                form.Fg1hStatus = Common.StatusList.FormG1G2Saved;

            return form;
        }


        public async Task<FormG1DTO> FindByHeaderID(int headerId)
        {
            RmFormG1Hdr header = await _repo.FindByHeaderID(headerId);
            RmFormG2Hdr frmG2 = new RmFormG2Hdr();
            if (header.RmFormG2Hdr != null)
            {
                frmG2 = header.RmFormG2Hdr.FirstOrDefault(x => x.Fg2hFg1hPkRefNo == headerId);
            }
            var frmG1 = _mapper.Map<FormG1DTO>(header);

            frmG1.FormG2 = frmG2 != null ? _mapper.Map<FormG2DTO>(frmG2) : new FormG2DTO();
            return frmG1;
        }
        public async Task<FormG1DTO> FindDetails(FormG1DTO frmG1G2, int createdBy)
        {
            RmFormG1Hdr header = _mapper.Map<RmFormG1Hdr>(frmG1G2);
            header = await _repo.FindDetails(header);
            if (header != null)
            {
                frmG1G2 = _mapper.Map<FormG1DTO>(header);
            }
            else
            {
                List<string> lstCVUNChar = Utility.GetAlphabets(1);
                var asset = _assetsService.GetAssetById(frmG1G2.AiPkRefNo.Value).Result;
                var assetother = _assetsService.GetOtherAssetByIdAsync(frmG1G2.AiPkRefNo.Value).Result;
                frmG1G2.DivCode = asset.DivisionCode;
                frmG1G2.RmuCode = asset.RMUCode;
                frmG1G2.RmuName = asset.RmuName;
                frmG1G2.RdCode = asset.RoadCode;
                frmG1G2.RdName = asset.RoadName;
                frmG1G2.StrucCode = asset.StructureCode;
                frmG1G2.LocChKm = asset.LocationChKm;
                frmG1G2.LocChM = Convert.ToInt32(asset.LocationChM);
                //frmG1G2.Length = asset.Length;
                //frmG1G2.Material = asset.Material == "Others" ? asset.Material + (assetother != null ? (assetother.MaterialOthers != null ? " - " + Utility.ToString(assetother.MaterialOthers) : "") : "") : asset.Material;
                //frmG1G2.AiFinRdLevel = asset.FindRoadLevel;
                //frmG1G2.AiCatchArea = asset.CatchArea;
                //frmG1G2.AiSkew = asset.Skew;
                //frmG1G2.AiGrpType = asset.CulvertType == "Others" ? asset.CulvertType + (assetother != null ? (assetother.CulvertTypeOthers != null ? " - " + Utility.ToString(assetother.CulvertTypeOthers) : "") : "") : asset.GroupType;
                //frmG1G2.AiDesignFlow = asset.DesignFlow;
                //frmG1G2.AiPrecastSitu = asset.PrecastSitu;
                //frmG1G2.AiBarrelNo = asset.BarrelNo;
                //frmG1G2.AiIntelLevel = asset.IntelLevel;
                //frmG1G2.AiOutletLevel = asset.OutletLevel;
                frmG1G2.GpsEasting = (decimal?)asset.GpsEasting;
                frmG1G2.GpsNorthing = (decimal?)asset.GpsNorthing;
                //frmG1G2.AiIntelStruc = asset.InletStruc;
                //frmG1G2.AiOutletStruc = asset.OutletStruc;
                frmG1G2.Status = "Initialize";
                //frmG1G2.InspectedBy = _security.UserID;
                //frmG1G2.InspectedName = _security.UserName;
                frmG1G2.CrBy = frmG1G2.ModBy = createdBy;
                frmG1G2.CrDt = frmG1G2.ModDt = DateTime.UtcNow;

                header = _mapper.Map<RmFormG1Hdr>(frmG1G2);
                header = await _repo.Save(header, false);
                frmG1G2 = _mapper.Map<FormG1DTO>(header);
            }
            //frmG1G2.PotentialHazards = true;
            return frmG1G2;
        }
        public async Task<List<FormG1G2PhotoTypeDTO>> GetExitingPhotoType(int headerId)
        {
            return await _repo.GetExitingPhotoType(headerId);
        }
        public async Task<RmFormGImages> AddImage(FormGImagesDTO imageDTO)
        {
            RmFormGImages image = _mapper.Map<RmFormGImages>(imageDTO);
            return await _repo.AddImage(image);
        }
        public async Task<(IList<RmFormGImages>, int)> AddMultiImage(IList<FormGImagesDTO> imagesDTO)
        {
            IList<RmFormGImages> images = new List<RmFormGImages>();
            foreach (var img in imagesDTO)
            {
                var count = await _repo.ImageCount(img.ImageTypeCode, img.Fg1hPkRefNo.Value);
                if (count > 2)
                {
                    return (null, -1);
                }
                var imgs = _mapper.Map<RmFormGImages>(img);
                imgs.FgiPkRefNo = img.PkRefNo;
                imgs.FgiFg1hPkRefNo = img.Fg1hPkRefNo;
                images.Add(imgs);
            }
            return (await _repo.AddMultiImage(images), 1);
        }
        public List<FormGImagesDTO> ImageList(int headerId)
        {
            List<RmFormGImages> lstImages = _repo.ImageList(headerId).Result;
            List<FormGImagesDTO> lstResult = new List<FormGImagesDTO>();
            if (lstImages != null && lstImages.Count > 0)
            {
                lstImages.ForEach((RmFormGImages img) =>
                {
                    lstResult.Add(_mapper.Map<FormGImagesDTO>(img));
                });
            }
            return lstResult;
        }
        public async Task<int> DeleteImage(int headerId, int imgId)
        {
            RmFormGImages img = new RmFormGImages();
            img.FgiPkRefNo = imgId;
            img.FgiFg1hPkRefNo = headerId;
            img.FgiActiveYn = false;
            return await _repo.DeleteImage(img);
        }
        public int Delete(int id)
        {
            if (id > 0)
            {
                id = _repo.DeleteHeader(new RmFormG1Hdr() { Fg1hActiveYn = false, Fg1hPkRefNo = id });
            }
            return id;
        }

        public List<FormG1G2Rpt> GetReportData(int headerid)
        {
            return _repo.GetReportData(headerid);
        }

        public byte[] FormDownload(string formname, int id, string basepath, string filepath)
        {
            //string structureCode = _repoUnit.DDLookUpRepository.GetConcatenateDdlTypeValue(new DTO.RequestBO.DDLookUpDTO { Type = "Structure Code", TypeCode = "Y" });
            string Oldfilename = "";
            string filename = "";
            string cachefile = "";
            basepath = $"{basepath}/Uploads";
            if (!filepath.Contains(".xlsx"))
            {
                Oldfilename = filepath + formname + ".xlsx";// formdetails.FgdFilePath+"\\" + formdetails.FgdFileName+ ".xlsx";
                filename = formname + DateTime.Now.ToString("yyyyMMddHHmmssfffffff").ToString();
                cachefile = filepath + filename + ".xlsx";
            }
            else
            {
                Oldfilename = filepath;
                filename = filepath.Replace(".xlsx", DateTime.Now.ToString("yyyyMMddHHmmssfffffff").ToString() + ".xlsx");
                cachefile = filename;
            }

            try
            {
                List<FormG1G2Rpt> _rpt = this.GetReportData(id);
                System.IO.File.Copy(Oldfilename, cachefile, true);
                using (var workbook = new XLWorkbook(cachefile))
                {
                    IXLWorksheet worksheet = workbook.Worksheet(1);

                    using (var book = new XLWorkbook(cachefile))
                    {
                        if (worksheet != null)
                        {
                            var rpt = _rpt[0];
                            worksheet.Cell(7, 3).Value = rpt.RefernceNo;
                            worksheet.Cell(9, 10).Value = rpt.Division;
                            worksheet.Cell(9, 10).Value = rpt.RMU;
                            worksheet.Cell(8, 10).Value = rpt.RoadName;
                            worksheet.Cell(7, 10).Value = rpt.RoadCode;
                            worksheet.Cell(9, 3).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";
                            var structureCode = rpt.StructureCode;
                            if (!string.IsNullOrEmpty(structureCode))
                            {
                                worksheet.Cell(8, 3).Value = structureCode;
                                worksheet.Cell(8, 3).RichText.Substring(0, structureCode.Length).Strikethrough = true;
                                if (!string.IsNullOrEmpty(rpt.StructureCode) && structureCode.IndexOf(" " + rpt.StructureCode + " ") > -1)
                                {
                                    worksheet.Cell(8, 3).RichText.Substring(structureCode.IndexOf(" " + rpt.StructureCode + " "), (" " + rpt.StructureCode + " ").Length).Bold = true;
                                    worksheet.Cell(8, 3).RichText.Substring(structureCode.IndexOf(" " + rpt.StructureCode + " "), (" " + rpt.StructureCode + " ").Length).Strikethrough = false;
                                }
                            }

                            worksheet.Cell(10, 3).Value = rpt.GPSEasting;
                            worksheet.Cell(10, 10).Value = rpt.GPSNorthing;

                            worksheet.Cell(13, 2).Value = rpt.ParkingPosition;
                            worksheet.Cell(14, 2).Value = rpt.Accessiblity;
                            worksheet.Cell(15, 2).Value = rpt.PotentialHazards;


                            for (int i = 0; i < _rpt.Count; i++)
                            {
                                rpt = _rpt[i];
                                worksheet.Cell(13, 6 + i).Value = rpt.Year;
                                worksheet.Cell(14, 6 + i).Value = rpt.Month;
                                worksheet.Cell(15, 6 + i).Value = rpt.Day;

                                worksheet.Cell(18, 6 + i).Value = rpt.BarriersYes == 1 ? "/" : "";
                                worksheet.Cell(19, 6 + i).Value = rpt.BarriersNo == 1 ? "/" : "";
                                worksheet.Cell(20, 6 + i).Value = rpt.BarriersCritical == 1 ? "/" : "";
                                worksheet.Cell(21, 6 + i).Value = rpt.BarriersClosed == 1 ? "/" : "";

                                worksheet.Cell(22, 6 + i).Value = rpt.GantryBeamsYes == 1 ? "/" : "";
                                worksheet.Cell(23, 6 + i).Value = rpt.GantryBeamsNo == 1 ? "/" : "";
                                worksheet.Cell(24, 6 + i).Value = rpt.GantryBeamsCritical == 1 ? "/" : "";
                                worksheet.Cell(25, 6 + i).Value = rpt.GantryBeamsClosed == 1 ? "/" : "";

                                worksheet.Cell(26, 6 + i).Value = rpt.GantryColsYes == 1 ? "/" : "";
                                worksheet.Cell(27, 6 + i).Value = rpt.GantryColsNo == 1 ? "/" : "";
                                worksheet.Cell(28, 6 + i).Value = rpt.GantryColsCritical == 1 ? "/" : "";
                                worksheet.Cell(29, 6 + i).Value = rpt.GantryColsClosed == 1 ? "/" : "";

                                worksheet.Cell(30, 6 + i).Value = rpt.FootingYes == 1 ? "/" : "";
                                worksheet.Cell(31, 6 + i).Value = rpt.FootingNo == 1 ? "/" : "";
                                worksheet.Cell(32, 6 + i).Value = rpt.FootingCritical == 1 ? "/" : "";
                                worksheet.Cell(33, 6 + i).Value = rpt.FootingClosed == 1 ? "/" : "";

                                worksheet.Cell(34, 6 + i).Value = rpt.AnchorYes == 1 ? "/" : "";
                                worksheet.Cell(35, 6 + i).Value = rpt.AnchorNo == 1 ? "/" : "";
                                worksheet.Cell(36, 6 + i).Value = rpt.AnchorCritical == 1 ? "/" : "";
                                worksheet.Cell(37, 6 + i).Value = rpt.AnchorClosed == 1 ? "/" : "";

                                worksheet.Cell(38, 6 + i).Value = rpt.MaintenanceAccessYes == 1 ? "/" : "";
                                worksheet.Cell(39, 6 + i).Value = rpt.MaintenanceAccessNo == 1 ? "/" : "";
                                worksheet.Cell(40, 6 + i).Value = rpt.MaintenanceAccessCritical == 1 ? "/" : "";
                                worksheet.Cell(41, 6 + i).Value = rpt.MaintenanceAccessClosed == 1 ? "/" : "";

                                worksheet.Cell(42, 6 + i).Value = rpt.StaticSignsYes == 1 ? "/" : "";
                                worksheet.Cell(43, 6 + i).Value = rpt.StaticSignsNo == 1 ? "/" : "";
                                worksheet.Cell(44, 6 + i).Value = rpt.StaticSignsCritical == 1 ? "/" : "";
                                worksheet.Cell(45, 6 + i).Value = rpt.StaticSignsClosed == 1 ? "/" : "";

                                worksheet.Cell(46, 6 + i).Value = rpt.VariableMessagYes == 1 ? "/" : "";
                                worksheet.Cell(47, 6 + i).Value = rpt.VariableMessagNo == 1 ? "/" : "";
                                worksheet.Cell(48, 6 + i).Value = rpt.VariableMessagCritical == 1 ? "/" : "";
                                worksheet.Cell(49, 6 + i).Value = rpt.VariableMessagClosed == 1 ? "/" : "";


                            }

                            worksheet.Cell(71, 3).Value = rpt.ReportforYear;

                            worksheet.Cell(72, 3).Value = rpt.StructureCode;
                            worksheet.Cell(73, 3).Value = rpt.RoadCode;
                            worksheet.Cell(74, 3).Value = rpt.RoadName;

                            worksheet.Cell(71, 11).Value = rpt.RefernceNo;
                            worksheet.Cell(72, 11).Value = rpt.RatingRecordNo;
                            worksheet.Cell(73, 11).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";

                            worksheet.Cell(81, 1).Value = rpt.PartB2ServiceProvider;
                            worksheet.Cell(81, 8).Value = rpt.PartB2ServicePrvdrCons;
                            worksheet.Cell(95, 1).Value = rpt.PartCGeneralComments;
                            worksheet.Cell(95, 8).Value = rpt.PartCGeneralCommentsCons;
                            worksheet.Cell(109, 1).Value = rpt.PartDFeedback;
                            worksheet.Cell(109, 8).Value = rpt.PartDFeedbackCons;
                            worksheet.Cell(127, 2).Value = rpt.InspectedByName;
                            worksheet.Cell(128, 2).Value = rpt.InspectedByDesignation;
                            worksheet.Cell(129, 2).Value = rpt.InspectedByDate;
                            worksheet.Cell(127, 10).Value = rpt.AuditedByName;
                            worksheet.Cell(128, 10).Value = rpt.AuditedByDesignation;
                            worksheet.Cell(129, 10).Value = rpt.AuditedByDate;
                            worksheet.Cell(130, 14).Value = rpt.GantrySignConditionRate;
                            worksheet.Cell(131, 14).Value = rpt.HaveIssueFound;

                            worksheet.Cell(136, 3).Value = rpt.ReportforYear;
                            worksheet.Cell(137, 3).Value = rpt.StructureCode;
                            worksheet.Cell(138, 3).Value = rpt.RoadCode;
                            worksheet.Cell(139, 3).Value = rpt.RoadName;

                            worksheet.Cell(136, 13).Value = rpt.RefernceNo;
                            worksheet.Cell(137, 13).Value = rpt.RatingRecordNo;
                            worksheet.Cell(138, 13).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";

                            worksheet.Cell(200, 3).Value = rpt.ReportforYear;
                            worksheet.Cell(201, 3).Value = rpt.StructureCode;
                            worksheet.Cell(202, 3).Value = rpt.RoadCode;
                            worksheet.Cell(203, 3).Value = rpt.RoadName;

                            worksheet.Cell(200, 13).Value = rpt.RefernceNo;
                            worksheet.Cell(201, 13).Value = rpt.RatingRecordNo;
                            worksheet.Cell(202, 13).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";


                            for (int index = 0; index < rpt.Pictures.Count; ++index)
                            {
                                if (File.Exists(basepath + "/" + rpt.Pictures[index].ImageUrl + "/" + rpt.Pictures[index].FileName))
                                {
                                    MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(basepath + "/" + rpt.Pictures[index].ImageUrl + "/" + rpt.Pictures[index].FileName));
                                    switch (index)
                                    {
                                        case 0:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(143, 1), new Point(45, 4)).WithSize(347, 178);
                                            continue;
                                        case 2:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(155, 1), new Point(45, 4)).WithSize(347, 178);
                                            continue;
                                        case 3:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(155, 9), new Point(45, 6)).WithSize(347, 178);
                                            continue;
                                        case 4:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(167, 1), new Point(45, 4)).WithSize(347, 178);
                                            continue;
                                        case 5:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(167, 9), new Point(45, 6)).WithSize(347, 178);
                                            continue;
                                        case 6:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(180, 1), new Point(45, 4)).WithSize(347, 178);
                                            continue;
                                        case 7:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(180, 9), new Point(45, 6)).WithSize(347, 178);
                                            continue;
                                        case 8:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(207, 1), new Point(45, 4)).WithSize(347, 178);
                                            continue;
                                        case 9:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(207, 9), new Point(45, 6)).WithSize(347, 178);
                                            continue;
                                        case 10:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(220, 1), new Point(45, 4)).WithSize(347, 178);
                                            continue;
                                        case 11:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(220, 9), new Point(45, 6)).WithSize(347, 178);
                                            continue;
                                        case 12:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(232, 1), new Point(45, 4)).WithSize(347, 178);
                                            continue;
                                        case 13:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(232, 9), new Point(45, 6)).WithSize(347, 178);
                                            continue;
                                        //case 14:
                                        //    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(247, 1), new Point(45, 4)).WithSize(347, 178);
                                        //    continue;
                                        //case 15:
                                        //    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(247, 9), new Point(4, 6)).WithSize(347, 178);
                                        //    continue;
                                        default:
                                            continue;
                                    }
                                }
                            }


                        }
                        using (var stream = new MemoryStream())
                        {
                            workbook.SaveAs(stream);
                            var content = stream.ToArray();
                            System.IO.File.Delete(cachefile);
                            return content;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.IO.File.Copy(Oldfilename, cachefile, true);
                using (var workbook = new XLWorkbook(cachefile))
                {
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        System.IO.File.Delete(cachefile);
                        return content;
                    }
                }

            }
        }
    }
}
