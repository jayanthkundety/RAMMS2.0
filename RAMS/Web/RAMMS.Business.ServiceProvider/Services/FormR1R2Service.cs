using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
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
    public class FormR1R2Service : IFormR1R2Service
    {
        private readonly IFormR1Repository _repo;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly IAssetsService _assetsService;
        private readonly IProcessService processService;
        private readonly ISecurity _security;
        public FormR1R2Service(IRepositoryUnit repoUnit, IFormR1Repository repo,
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
        public async Task<FormR1DTO> Save(FormR1DTO frmR1R2, bool updateSubmit)
        {
            RmFormR1Hdr frmR1R2_1 = this._mapper.Map<RmFormR1Hdr>((object)frmR1R2);
            frmR1R2_1 = UpdateStatus(frmR1R2_1);

            RmFormR2Hdr frmR2 = this._mapper.Map<RmFormR2Hdr>(frmR1R2.FormR2);
            frmR2.Fr2hPkRefNo = frmR1R2.FormR2.PkRefNo;
            frmR2.Fr2hFr1hPkRefNo = frmR1R2.FormR2.FR1hPkRefNo;


            RmFormR1Hdr source = await this._repo.Save(frmR1R2_1, updateSubmit);

            RmFormR2Hdr sourceG2 = await this._repo.SaveR2(frmR2, updateSubmit);

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
            frmR1R2 = this._mapper.Map<FormR1DTO>((object)source);
            return frmR1R2;
        }

        public RmFormR1Hdr UpdateStatus(RmFormR1Hdr form)
        {
            if (form.Fr1hPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormR1Repository._context.RmFormR1Hdr.Where(x => x.Fr1hPkRefNo == form.Fr1hPkRefNo).Select(x => new { Status = x.Fr1hStatus, Log = x.Fr1hAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.Fr1hAuditLog = existsObj.Log;
                    form.Fr1hStatus = existsObj.Status;
                }

            }


            if (form.Fr1hSubmitSts && (string.IsNullOrEmpty(form.Fr1hStatus) || form.Fr1hStatus == Common.StatusList.FormQA1Saved || form.Fr1hStatus == Common.StatusList.FormQA1Rejected))
            {
                //form.Fg1hInspectedBy = _security.UserID;
                //form.Fg1hInspectedName = _security.UserName;
                //form.Fg1hInspectedDt = DateTime.Today;
                form.Fr1hStatus = Common.StatusList.FormQA1Submitted;
                form.Fr1hAuditLog = Utility.ProcessLog(form.Fr1hAuditLog, "Submitted", "Submitted", form.Fr1hInspectedName, string.Empty, form.Fr1hDtOfInsp, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Executed By:"+ form.Fr1hInspectedName + " - Form R1 (" + form.Fr1hPkRefNo + ")",//doubt
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/FormR1R2/Edit/" + form.Fr1hPkRefNo.ToString() + "?view=1",
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }
            else if (string.IsNullOrEmpty(form.Fr1hStatus) || form.Fr1hStatus == "Initialize")
                form.Fr1hStatus = Common.StatusList.FormR1R2Saved;

            return form;
        }


        public async Task<FormR1DTO> FindByHeaderID(int headerId)
        {
            RmFormR1Hdr header = await _repo.FindByHeaderID(headerId);
            RmFormR2Hdr frmR2 = new RmFormR2Hdr();
            if (header.RmFormR2Hdr != null)
            {
                frmR2 = header.RmFormR2Hdr.FirstOrDefault(x => x.Fr2hFr1hPkRefNo == headerId);
            }
            var frmR1 = _mapper.Map<FormR1DTO>(header);

            frmR1.FormR2 = frmR2 != null ? _mapper.Map<FormR2DTO>(frmR2) : new FormR2DTO();
            return frmR1;
        }
        public async Task<FormR1DTO> FindDetails(FormR1DTO frmR1R2, int createdBy)
        {
            RmFormR1Hdr header = _mapper.Map<RmFormR1Hdr>(frmR1R2);
            header = await _repo.FindDetails(header);
            if (header != null)
            {
                frmR1R2 = _mapper.Map<FormR1DTO>(header);
            }
            else
            {
                List<string> lstCVUNChar = Utility.GetAlphabets(1);
                var asset = _assetsService.GetAssetById(frmR1R2.AidPkRefNo.Value).Result;
                var assetother = _assetsService.GetOtherAssetByIdAsync(frmR1R2.AidPkRefNo.Value).Result;
                frmR1R2.DivCode = asset.DivisionCode;
                frmR1R2.RmuCode = asset.RMUCode;
                frmR1R2.RmuName = asset.RmuName;
                frmR1R2.RdCode = asset.RoadCode;
                frmR1R2.RdName = asset.RoadName;
                frmR1R2.StrucCode = asset.StructureCode;
                frmR1R2.LocChKm = asset.LocationChKm;
                frmR1R2.LocChM = Convert.ToInt32(asset.LocationChM);

                frmR1R2.GpsEasting = (decimal?)asset.GpsEasting;
                frmR1R2.GpsNorthing = (decimal?)asset.GpsNorthing;

                frmR1R2.Status = "Initialize";

                frmR1R2.InspectedBy = _security.UserID;
                frmR1R2.InspectedName = _security.UserName;
                frmR1R2.InspectedDt = DateTime.Today;
                frmR1R2.CrBy = frmR1R2.ModBy = createdBy;
                frmR1R2.CrDt = frmR1R2.ModDt = DateTime.UtcNow;

                header = _mapper.Map<RmFormR1Hdr>(frmR1R2);
                header = await _repo.Save(header, false);
                frmR1R2 = _mapper.Map<FormR1DTO>(header);
            }
            //frmG1G2.PotentialHazards = true;
            return frmR1R2;
        }
        public async Task<List<FormR1R2PhotoTypeDTO>> GetExitingPhotoType(int headerId)
        {
            return await _repo.GetExitingPhotoType(headerId);
        }
        public async Task<RmFormRImages> AddImage(FormRImagesDTO imageDTO)
        {
            RmFormRImages image = _mapper.Map<RmFormRImages>(imageDTO);
            return await _repo.AddImage(image);
        }
        public async Task<(IList<RmFormRImages>, int)> AddMultiImage(IList<FormRImagesDTO> imagesDTO)
        {
            IList<RmFormRImages> images = new List<RmFormRImages>();
            foreach (var img in imagesDTO)
            {
                var count = await _repo.ImageCount(img.ImageTypeCode, img.FR1hPkRefNo.Value);
                if (count > 2)
                {
                    return (null, -1);
                }
                var imgs = _mapper.Map<RmFormRImages>(img);
                imgs.FriPkRefNo = img.PkRefNo;
                imgs.FriFr1hPkRefNo = img.FR1hPkRefNo;
                images.Add(imgs);
            }
            return (await _repo.AddMultiImage(images), 1);
        }
        public List<FormRImagesDTO> ImageList(int headerId)
        {
            List<RmFormRImages> lstImages = _repo.ImageList(headerId).Result;
            List<FormRImagesDTO> lstResult = new List<FormRImagesDTO>();
            if (lstImages != null && lstImages.Count > 0)
            {
                lstImages.ForEach((RmFormRImages img) =>
                {
                    lstResult.Add(_mapper.Map<FormRImagesDTO>(img));
                });
            }
            return lstResult;
        }
        public async Task<int> DeleteImage(int headerId, int imgId)
        {
            RmFormRImages img = new RmFormRImages();
            img.FriPkRefNo = imgId;
            img.FriFr1hPkRefNo = headerId;
            img.FriActiveYn = false;
            return await _repo.DeleteImage(img);
        }
        public int Delete(int id)
        {
            if (id > 0&& !_repo.isF1Exist(id))
            {
                id = _repo.DeleteHeader(new RmFormR1Hdr() { Fr1hActiveYn = false, Fr1hPkRefNo = id });
            }
            else
            {
                return -1;
            }
            return id;
        }

        public List<FormR1R2Rpt> GetReportData(int headerid)
        {
            return _repo.GetReportData(headerid);
        }

        public byte[] FormDownload(string formname, int id, string basepath, string filepath)
        {
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
                List<FormR1R2Rpt> _rpt = this.GetReportData(id);
                System.IO.File.Copy(Oldfilename, cachefile, true);
                using (var workbook = new XLWorkbook(cachefile))
                {
                    IXLWorksheet worksheet = workbook.Worksheet(1);

                    using (var book = new XLWorkbook(cachefile))
                    {
                        if (worksheet != null)
                        {
                            var rptCount = _rpt.Count;
                            var rpt = _rpt[rptCount-1];
                            worksheet.Cell(4, 21).Value = rpt.RefernceNo;
                            worksheet.Cell(5, 7).Value = rpt.RoadCode;
                            worksheet.Cell(5, 17).Value = rpt.RoadName;
                            var structureCode = rpt.StructureCode;
                            if (!string.IsNullOrEmpty(structureCode))
                            {
                                worksheet.Cell(6, 7).Value = structureCode;
                                //worksheet.Cell(6, 7).RichText.Substring(0, structureCode.Length).Strikethrough = true;
                                if (!string.IsNullOrEmpty(rpt.StructureCode) && structureCode.IndexOf(" " + rpt.StructureCode + " ") > -1)
                                {
                                    worksheet.Cell(6, 6).RichText.Substring(structureCode.IndexOf(" " + rpt.StructureCode + " "), (" " + rpt.StructureCode + " ").Length).Bold = true;
                                    worksheet.Cell(6, 6).RichText.Substring(structureCode.IndexOf(" " + rpt.StructureCode + " "), (" " + rpt.StructureCode + " ").Length).Strikethrough = false;
                                }
                            }
                            worksheet.Cell(6, 17).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";
                            worksheet.Cell(6, 27).Value = rpt.Division;
                            worksheet.Cell(7, 7).Value = rpt.RMU;
                            worksheet.Cell(7, 17).Value = rpt.GPSEasting;
                            worksheet.Cell(7, 27).Value = rpt.GPSNorthing;
                            if (!string.IsNullOrEmpty(rpt.WallFunction))
                            {
                                if (rpt.WallFunction == "F1")
                                    worksheet.Cell(10, 3).Value = "✓";
                                else if (rpt.WallFunction == "F2")
                                    worksheet.Cell(11, 3).Value = "✓";
                                else if (rpt.WallFunction == "F3")
                                    worksheet.Cell(10, 10).Value = "✓";
                                else if (rpt.WallFunction == "F4")
                                    worksheet.Cell(11, 10).Value = "✓";
                                else if (rpt.WallFunction == "F5")
                                    worksheet.Cell(10, 17).Value = "✓";
                                else if (rpt.WallFunction == "F6")
                                    worksheet.Cell(11, 17).Value = "✓";
                                else if (rpt.WallFunction == "F7")
                                    worksheet.Cell(10, 24).Value = "✓";
                                else if (rpt.WallFunction == "F8")
                                    worksheet.Cell(11, 24).Value = "✓";
                            }
                            if (!string.IsNullOrEmpty(rpt.WallMember))
                            {
                                if (rpt.WallMember == "M1")
                                    worksheet.Cell(15, 3).Value = "✓";
                                else if (rpt.WallMember == "M2")
                                    worksheet.Cell(16, 3).Value = "✓";
                                else if (rpt.WallMember == "M3")
                                    worksheet.Cell(17, 3).Value = "✓";
                                else if (rpt.WallMember == "M4")
                                    worksheet.Cell(18, 3).Value = "✓";
                                else if (rpt.WallMember == "M5")
                                    worksheet.Cell(19, 3).Value = "✓";
                                else if (rpt.WallMember == "M6")
                                    worksheet.Cell(20, 3).Value = "✓";
                                else if (rpt.WallMember == "M7")
                                    worksheet.Cell(21, 3).Value = "✓";
                                else if (rpt.WallMember == "M8")
                                    worksheet.Cell(22, 3).Value = "✓";

                                else if (rpt.WallMember == "M9")
                                    worksheet.Cell(15, 13).Value = "✓";
                                else if (rpt.WallMember == "M10")
                                    worksheet.Cell(16, 13).Value = "✓";
                                else if (rpt.WallMember == "M11")
                                    worksheet.Cell(17, 13).Value = "✓";
                                else if (rpt.WallMember == "M12")
                                    worksheet.Cell(18, 13).Value = "✓";
                                else if (rpt.WallMember == "M13")
                                    worksheet.Cell(19, 13).Value = "✓";
                                else if (rpt.WallMember == "M14")
                                    worksheet.Cell(20, 13).Value = "✓";
                                else if (rpt.WallMember == "M15")
                                    worksheet.Cell(21, 13).Value = "✓";
                                else if (rpt.WallMember == "M16")
                                    worksheet.Cell(22, 13).Value = "✓";

                                else if (rpt.WallMember == "M17")
                                    worksheet.Cell(15, 24).Value = "✓";
                                else if (rpt.WallMember == "M18")
                                    worksheet.Cell(16, 24).Value = "✓";
                                else if (rpt.WallMember == "M19")
                                    worksheet.Cell(17, 24).Value = "✓";
                                else if (rpt.WallMember == "M20")
                                    worksheet.Cell(18, 24).Value = "✓";
                                else if (rpt.WallMember == "M21")
                                    worksheet.Cell(19, 24).Value = "✓";
                                else if (rpt.WallMember == "M22")
                                    worksheet.Cell(20, 24).Value = "✓";
                                else if (rpt.WallMember == "M23")
                                    worksheet.Cell(21, 24).Value = "✓";
                                else if (rpt.WallMember == "M24")
                                    worksheet.Cell(22, 24).Value = "✓";

                            }
                            if (!string.IsNullOrEmpty(rpt.FacingType))
                            {
                                if (rpt.FacingType == "T1")
                                    worksheet.Cell(26, 3).Value = "✓";
                                else if (rpt.FacingType == "T2")
                                    worksheet.Cell(27, 3).Value = "✓";
                                else if (rpt.FacingType == "T3")
                                    worksheet.Cell(28, 3).Value = "✓";
                                else if (rpt.FacingType == "T4")
                                    worksheet.Cell(29, 3).Value = "✓";
                                else if (rpt.FacingType == "T5")
                                    worksheet.Cell(30, 3).Value = "✓";

                                else if (rpt.FacingType == "T6")
                                    worksheet.Cell(26, 13).Value = "✓";
                                else if (rpt.FacingType == "T7")
                                    worksheet.Cell(27, 13).Value = "✓";
                                else if (rpt.FacingType == "T8")
                                    worksheet.Cell(28, 13).Value = "✓";
                                else if (rpt.FacingType == "T9")
                                    worksheet.Cell(29, 13).Value = "✓";
                                else if (rpt.FacingType == "T10")
                                    worksheet.Cell(30, 13).Value = "✓";

                                else if (rpt.FacingType == "T11")
                                    worksheet.Cell(26, 24).Value = "✓";
                                else if (rpt.FacingType == "T12")
                                    worksheet.Cell(27, 24).Value = "✓";
                                else if (rpt.FacingType == "T13")
                                    worksheet.Cell(28, 24).Value = "✓";
                                else if (rpt.FacingType == "T14")
                                    worksheet.Cell(29, 24).Value = "✓";
                                else if (rpt.FacingType == "T15")
                                    worksheet.Cell(30, 24).Value = "✓";
                            }

                            for (int i = 0; i < _rpt.Count; i++)
                            {
                                rpt = _rpt[i];
                                int j = 0;
                                #region looping Data
                                if (i == 0)
                                    j=5;
                                else if(i==1)
                                    j = 7;
                                else if (i == 2)
                                    j = 9;
                                else if (i == 3)
                                    j = 11;
                                else if (i == 4)
                                    j = 13;
                                else if (i == 5)
                                    j = 15;
                                else if (i == 6)
                                    j = 17;
                                else if (i == 7)
                                    j = 19;
                                else if (i == 8)
                                    j = 21;
                                else if (i == 9)
                                    j = 23;
                                else if (i == 10)
                                    j = 25;
                                else if (i == 11)
                                    j = 27;
                                else if (i == 12)
                                    j = 29;
                                else if (i == 13)
                                    j = 31;
                                else if (i == 14)
                                    j = 33;
                                else if (i == 15)
                                    j = 35;
                                #endregion
                                worksheet.Cell(34, j).Value = rpt.Year;
                                worksheet.Cell(35, j).Value = rpt.Month;
                                worksheet.Cell(36, j).Value = rpt.Day;

                                if (rpt.DistressObserved != null)
                                {
                                    worksheet.Cell(37, j).Value = rpt.DistressObserved.Split(',').Length >= 1 ? rpt.DistressObserved.Split(',')[0] : "";
                                    worksheet.Cell(38, j).Value = rpt.DistressObserved.Split(',').Length >= 2 ? rpt.DistressObserved.Split(',')[1] : "";
                                    worksheet.Cell(39, j).Value = rpt.DistressObserved.Split(',').Length == 3 ? rpt.DistressObserved.Split(',')[2] : "";
                                }
                                worksheet.Cell(40, j).Value = rpt.Severity;
                            }

                            worksheet.Cell(65, 8).Value = rpt.ReportforYear;
                            worksheet.Cell(65, 21).Value = rpt.RefernceNo;

                            worksheet.Cell(66, 7).Value = rpt.RoadCode;
                            worksheet.Cell(66, 21).Value = rpt.RoadName;

                            worksheet.Cell(67, 8).Value = rpt.StructureCode;
                            worksheet.Cell(67, 23).Value = rptCount;//rpt.RatingRecordNo;

                            worksheet.Cell(73, 2).Value = rpt.PartB2ServiceProvider;
                            worksheet.Cell(73, 19).Value = rpt.PartB2ServicePrvdrCons;

                            worksheet.Cell(83, 2).Value = rpt.PartCGeneralComments;
                            worksheet.Cell(83, 19).Value = rpt.PartCGeneralCommentsCons;

                            worksheet.Cell(93, 2).Value = rpt.PartDFeedback;
                            worksheet.Cell(93, 19).Value = rpt.PartDFeedbackCons;

                            worksheet.Cell(107, 6).Value = rpt.InspectedByName;
                            worksheet.Cell(108, 6).Value = rpt.InspectedByDesignation;
                            worksheet.Cell(109, 6).Value = rpt.InspectedByDate;

                            worksheet.Cell(107, 23).Value = rpt.AuditedByName;
                            worksheet.Cell(108, 23).Value = rpt.AuditedByDesignation;
                            worksheet.Cell(109, 23).Value = rpt.AuditedByDate;

                            worksheet.Cell(110, 32).Value = rpt.RatingWallConditionRate;
                            worksheet.Cell(111, 32).Value = rpt.HaveIssueFound;

                            worksheet.Cell(116, 8).Value = rpt.ReportforYear;
                            worksheet.Cell(116, 21).Value = rpt.RefernceNo;

                            worksheet.Cell(117, 7).Value = rpt.RoadCode;
                            worksheet.Cell(117, 21).Value = rpt.RoadName;

                            worksheet.Cell(118, 8).Value = rpt.StructureCode;
                            worksheet.Cell(118, 23).Value = rptCount; //rpt.RatingRecordNo;

                            for (int index = 0; index < rpt.Pictures.Count; ++index)
                            {
                                if (File.Exists(basepath + "/" + rpt.Pictures[index].ImageUrl + "/" + rpt.Pictures[index].FileName))
                                {
                                    MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(basepath + "/" + rpt.Pictures[index].ImageUrl + "/" + rpt.Pictures[index].FileName));
                                    switch (index)
                                    {
                                        case 0:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(124, 2), new Point(45, 4)).WithSize(290, 140);
                                            continue;
                                        case 1:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(124, 19), new Point(45, 4)).WithSize(290, 140);
                                            continue;
                                        case 2:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(135, 2), new Point(45, 6)).WithSize(290, 140);
                                            continue;
                                        case 3:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(135, 19), new Point(45, 4)).WithSize(290, 140);
                                            continue;
                                        case 4:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(146, 2), new Point(45, 6)).WithSize(290, 140);
                                            continue;
                                        case 5:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(146, 19), new Point(45, 4)).WithSize(290, 140);
                                            continue;
                                        case 6:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(155, 2), new Point(45, 6)).WithSize(290, 140);
                                            continue;
                                        case 7:
                                            worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(155, 19), new Point(45, 4)).WithSize(290, 140);
                                            continue;
                                        //case 9:
                                        //    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(207, 9), new Point(45, 6)).WithSize(347, 178);
                                        //    continue;
                                        //case 10:
                                        //    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(220, 1), new Point(45, 4)).WithSize(347, 178);
                                        //    continue;
                                        //case 11:
                                        //    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(220, 9), new Point(45, 6)).WithSize(347, 178);
                                        //    continue;
                                        //case 12:
                                        //    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(232, 1), new Point(45, 4)).WithSize(347, 178);
                                        //    continue;
                                        //case 13:
                                        //    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(232, 9), new Point(45, 6)).WithSize(347, 178);
                                        //    continue;
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