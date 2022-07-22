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
    public class FormC1C2Service : IFormC1C2Service
    {
        private readonly IFormC1C2Repository _repo;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly IAssetsService _assetsService;
        private readonly IProcessService processService;
        public FormC1C2Service(IRepositoryUnit repoUnit, IFormC1C2Repository repo,
            IAssetsService assetsService, IMapper mapper, IProcessService proService)
        {
            _repo = repo;
            _mapper = mapper;
            _assetsService = assetsService;
            _repoUnit = repoUnit;
            processService = proService;
        }
        public async Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData)
        {
            return await _repo.GetHeaderGrid(searchData);
        }
        public async Task<FormC1C2DTO> Save(FormC1C2DTO frmC1C2, bool updateSubmit)
        {
            RmFormCvInsHdr frmC1C2_1 = this._mapper.Map<RmFormCvInsHdr>((object)frmC1C2);
            frmC1C2_1.FcvihStatus = "Open";
            foreach (RmFormCvInsDtl rmFormCvInsDtl in (IEnumerable<RmFormCvInsDtl>)frmC1C2_1.RmFormCvInsDtl)
                rmFormCvInsDtl.FcvidIimPkRefNoNavigation = (RmInspItemMas)null;
            RmFormCvInsHdr source = await this._repo.Save(frmC1C2_1, updateSubmit);
            if (source != null && source.FcvihSubmitSts)
            {
                int result = this.processService.Save(new ProcessDTO()
                {
                    ApproveDate = new System.DateTime?(System.DateTime.Now),
                    Form = "FormC1C2",
                    IsApprove = true,
                    RefId = source.FcvihPkRefNo,
                    Remarks = "",
                    Stage = source.FcvihStatus
                }).Result;
            }
            frmC1C2 = this._mapper.Map<FormC1C2DTO>((object)source);
            return frmC1C2;
        }
        public async Task<FormC1C2DTO> FindByHeaderID(int headerId)
        {
            RmFormCvInsHdr header = await _repo.FindByHeaderID(headerId);
            if (header.RmFormCvInsDtl != null)
            {
                var lst = header.RmFormCvInsDtl.OrderBy(x => x.FcvidInspCode).ThenBy(x => x.FcvidInspCodeDesc);
                header.RmFormCvInsDtl = lst.ToList();
            }
            return _mapper.Map<FormC1C2DTO>(header);
        }
        public async Task<FormC1C2DTO> FindDetails(FormC1C2DTO frmC1C2, int createdBy)
        {
            RmFormCvInsHdr header = _mapper.Map<RmFormCvInsHdr>(frmC1C2);
            header = await _repo.FindDetails(header);
            if (header != null)
            {
                frmC1C2 = _mapper.Map<FormC1C2DTO>(header);
            }
            else
            {
                List<string> lstCVUNChar = Utility.GetAlphabets(1);
                var asset = _assetsService.GetAssetById(frmC1C2.AiPkRefNo.Value).Result;
                var assetother = _assetsService.GetOtherAssetByIdAsync(frmC1C2.AiPkRefNo.Value).Result;
                frmC1C2.AiDivCode = asset.DivisionCode;
                frmC1C2.AiRmuName = asset.RmuName;
                frmC1C2.AiRdCode = asset.RoadCode;
                frmC1C2.AiRdName = asset.RoadName;
                frmC1C2.AiStrucCode = asset.StructureCode;
                frmC1C2.AiLocChKm = asset.LocationChKm;
                frmC1C2.AiLocChM = asset.LocationChM;
                frmC1C2.AiLength = asset.Length;
                frmC1C2.AiMaterial = asset.Material == "Others" ? asset.Material + (assetother != null ? (assetother.MaterialOthers != null ? " - " + Utility.ToString(assetother.MaterialOthers) : "") : "") : asset.Material;
                frmC1C2.AiFinRdLevel = asset.FindRoadLevel;
                frmC1C2.AiCatchArea = asset.CatchArea;
                frmC1C2.AiSkew = asset.Skew;
                frmC1C2.AiGrpType = asset.CulvertType == "Others" ? asset.CulvertType + (assetother != null ? (assetother.CulvertTypeOthers != null ? " - " + Utility.ToString(assetother.CulvertTypeOthers) : "") : "") : asset.GroupType;
                frmC1C2.AiDesignFlow = asset.DesignFlow;
                frmC1C2.AiPrecastSitu = asset.PrecastSitu;
                frmC1C2.AiBarrelNo = asset.BarrelNo;
                frmC1C2.AiIntelLevel = asset.IntelLevel;
                frmC1C2.AiOutletLevel = asset.OutletLevel;
                frmC1C2.AiGpsEasting = asset.GpsEasting;
                frmC1C2.AiGpsNorthing = asset.GpsNorthing;
                frmC1C2.AiIntelStruc = asset.InletStruc;
                frmC1C2.AiOutletStruc = asset.OutletStruc;
                frmC1C2.CrBy = frmC1C2.ModBy = createdBy;
                frmC1C2.CrDt = frmC1C2.ModDt = DateTime.UtcNow;
                var lstMaster = await _repo.GetInspItemMaster();
                if (lstMaster.Count > 0)
                {
                    frmC1C2.InsDtl = new List<RmFormCvInsDtDTO>();
                    lstMaster.ForEach((RmInspItemMas item) =>
                    {
                        foreach (var dtl in item.RmInspItemMasDtl)
                        {
                            if (item.IimInspName == "Culvert Units" && dtl.IimdInspCode.Last() == '+')
                            {
                                if (frmC1C2.AiBarrelNo.HasValue)
                                {
                                    for (int i = 0; i < frmC1C2.AiBarrelNo.Value; i++)
                                    {
                                        frmC1C2.InsDtl.Add(new RmFormCvInsDtDTO()
                                        {
                                            ActiveYn = true,
                                            CrBy = createdBy,
                                            ModBy = createdBy,
                                            CrDt = DateTime.UtcNow,
                                            ModDt = DateTime.UtcNow,
                                            mdPkRefNo = dtl.IimdPkRefNo,
                                            SubmitSts = true,
                                            InspCode = dtl.IimdInspCode.Replace("+", lstCVUNChar[i]),
                                            InspCodeDesc = dtl.IimdInspCodeDesc.Replace("+", (i + 1).ToString()),
                                            mPkRefNo = item.IimPkRefNo
                                        });
                                    }
                                }
                            }
                            else
                                frmC1C2.InsDtl.Add(new RmFormCvInsDtDTO()
                                {
                                    ActiveYn = true,
                                    CrBy = createdBy,
                                    ModBy = createdBy,
                                    CrDt = DateTime.UtcNow,
                                    ModDt = DateTime.UtcNow,
                                    mdPkRefNo = dtl.IimdPkRefNo,
                                    SubmitSts = true,
                                    InspCode = dtl.IimdInspCode,
                                    InspCodeDesc = dtl.IimdInspCodeDesc,
                                    mPkRefNo = item.IimPkRefNo
                                });
                        }

                    });
                }



                header = _mapper.Map<RmFormCvInsHdr>(frmC1C2);
                header = await _repo.Save(header, false);
                frmC1C2 = _mapper.Map<FormC1C2DTO>(header);
            }
            //frmC1C2.PotentialHazards = true;
            return frmC1C2;
        }
        public async Task<List<FormC1C2PhotoTypeDTO>> GetExitingPhotoType(int headerId)
        {
            return await _repo.GetExitingPhotoType(headerId);
        }
        public async Task<RmFormCvInsImage> AddImage(FormC1C2ImageDTO imageDTO)
        {
            RmFormCvInsImage image = _mapper.Map<RmFormCvInsImage>(imageDTO);
            return await _repo.AddImage(image);
        }
        public async Task<(IList<RmFormCvInsImage>, int)> AddMultiImage(IList<FormC1C2ImageDTO> imagesDTO)
        {
            IList<RmFormCvInsImage> images = new List<RmFormCvInsImage>();
            foreach (var img in imagesDTO)
            {
                var count = await _repo.ImageCount(img.ImageTypeCode, img.hPkRefNo.Value);
                if (count > 2)
                {
                    return (null, -1);
                }

                images.Add(_mapper.Map<RmFormCvInsImage>(img));
            }
            return (await _repo.AddMultiImage(images), 1);
        }
        public List<FormC1C2ImageDTO> ImageList(int headerId)
        {
            List<RmFormCvInsImage> lstImages = _repo.ImageList(headerId).Result;
            List<FormC1C2ImageDTO> lstResult = new List<FormC1C2ImageDTO>();
            if (lstImages != null && lstImages.Count > 0)
            {
                lstImages.ForEach((RmFormCvInsImage img) =>
                {
                    lstResult.Add(_mapper.Map<FormC1C2ImageDTO>(img));
                });
            }
            return lstResult;
        }
        public async Task<int> DeleteImage(int headerId, int imgId)
        {
            RmFormCvInsImage img = new RmFormCvInsImage();
            img.FcviPkRefNo = imgId;
            img.FcviFcvihPkRefNo = headerId;
            img.FcviActiveYn = false;
            return await _repo.DeleteImage(img);
        }
        public int Delete(int id)
        {
            if (id > 0)
            {
                id = _repo.DeleteHeader(new RmFormCvInsHdr() { FcvihActiveYn = false, FcvihPkRefNo = id });
            }
            return id;
        }

        public List<FormC1C2Rpt> GetReportData(int headerid)
        {
            return _repo.GetReportData(headerid);
        }

        public byte[] FormDownload(string formname, int id, string basepath, string filepath)
        {
            string structureCode = _repoUnit.DDLookUpRepository.GetConcatenateDdlTypeValue(new DTO.RequestBO.DDLookUpDTO { Type = "Structure Code", TypeCode = "CV" });
            string culvertType = _repoUnit.DDLookUpRepository.GetConcatenateDdlTypeDesc(new DTO.RequestBO.DDLookUpDTO { Type = "Culvert Type", TypeCode = "CV" });
            string culvertMaterial = _repoUnit.DDLookUpRepository.GetConcatenateDdlTypeDesc(new DTO.RequestBO.DDLookUpDTO { Type = "Culvert Material", TypeCode = "CV" });
            string inletStructure = _repoUnit.DDLookUpRepository.GetConcatenateDdlTypeDesc(new DTO.RequestBO.DDLookUpDTO { Type = "Inlet Structure", TypeCode = "CV" });
            string outletStructure = _repoUnit.DDLookUpRepository.GetConcatenateDdlTypeDesc(new DTO.RequestBO.DDLookUpDTO { Type = "Outlet Structure", TypeCode = "CV" });
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
                List<FormC1C2Rpt> _rpt = this.GetReportData(id);
                System.IO.File.Copy(Oldfilename, cachefile, true);
                using (var workbook = new XLWorkbook(cachefile))
                {

                    IXLWorksheet worksheet = workbook.Worksheet(1);

                    if (worksheet != null)
                    {
                        var rpt = _rpt[0];
                        worksheet.Cell(6, 3).Value = rpt.RefernceNo;
                        worksheet.Cell(7, 3).Value = rpt.Division;
                        worksheet.Cell(8, 3).Value = rpt.RMU;
                        worksheet.Cell(9, 3).Value = rpt.FinishedRoadLevel;
                        worksheet.Cell(10, 3).Value = rpt.CatchmentArea;
                        worksheet.Cell(11, 3).Value = rpt.DesignFlow;
                        worksheet.Cell(12, 3).Value = rpt.Precast;
                        worksheet.Cell(13, 3).Value = rpt.BarrelNumber;
                        worksheet.Cell(14, 3).Value = rpt.InletLevel;
                        worksheet.Cell(15, 3).Value = rpt.OutletLevel;
                        worksheet.Cell(6, 10).Value = rpt.RoadName;
                        worksheet.Cell(7, 10).Value = rpt.RoadCode;
                        worksheet.Cell(8, 10).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";
                        worksheet.Cell(10, 10).Value = rpt.CulverSkew;
                        worksheet.Cell(11, 10).Value = rpt.CulvertLength;

                        if (!string.IsNullOrEmpty(structureCode))
                        {
                            worksheet.Cell(9, 10).Value = structureCode;
                            //worksheet.Cell(9, 10).RichText.Substring(0, structureCode.Length).Strikethrough = true;
                            //if (!string.IsNullOrEmpty(rpt.StructureCode) && structureCode.IndexOf(" " + rpt.StructureCode + " ") > -1)
                            //{
                            //    worksheet.Cell(9, 10).RichText.Substring(structureCode.IndexOf(" " + rpt.StructureCode + " "), (" " + rpt.StructureCode + " ").Length).Bold = true;
                            //    worksheet.Cell(9, 10).RichText.Substring(structureCode.IndexOf(" " + rpt.StructureCode + " "), (" " + rpt.StructureCode + " ").Length).Strikethrough = false;
                            //}
                        }

                        if (!string.IsNullOrEmpty(culvertType))
                        {
                            worksheet.Cell(12, 10).Value = culvertType;
                            //worksheet.Cell(12, 10).RichText.Substring(0, culvertType.Length).Strikethrough = true;
                            //if (!string.IsNullOrEmpty(rpt.CulvertType) && culvertType.IndexOf(" " + rpt.CulvertType + " ") > -1)
                            //{
                            //    worksheet.Cell(12, 10).RichText.Substring(culvertType.IndexOf(" " + rpt.CulvertType + " "), (" " + rpt.CulvertType + " ").Length).Bold = true;
                            //    worksheet.Cell(12, 10).RichText.Substring(culvertType.IndexOf(" " + rpt.CulvertType + " "), (" " + rpt.CulvertType + " ").Length).Strikethrough = false;
                            //}
                        }
                        if (!string.IsNullOrEmpty(culvertMaterial))
                        {
                            worksheet.Cell(13, 10).Value = culvertMaterial;
                            //worksheet.Cell(13, 10).RichText.Substring(0, culvertMaterial.Length).Strikethrough = true;
                            //if (!string.IsNullOrEmpty(rpt.Culvertmaterial) && culvertMaterial.IndexOf(" " + rpt.Culvertmaterial + " ") > -1)
                            //{
                            //    worksheet.Cell(13, 10).RichText.Substring(culvertMaterial.IndexOf(" " + rpt.Culvertmaterial + " "), (" " + rpt.Culvertmaterial + " ").Length).Bold = true;
                            //    worksheet.Cell(13, 10).RichText.Substring(culvertMaterial.IndexOf(" " + rpt.Culvertmaterial + " "), (" " + rpt.Culvertmaterial + " ").Length).Strikethrough = false;
                            //}
                        }

                        if (!string.IsNullOrEmpty(inletStructure))
                        {
                            worksheet.Cell(14, 10).Value = inletStructure;
                            //worksheet.Cell(14, 10).RichText.Substring(0, inletStructure.Length).Strikethrough = true;
                            //if (!string.IsNullOrEmpty(rpt.InletStructure) && inletStructure.IndexOf(" " + rpt.InletStructure + " ") > -1)
                            //{
                            //    worksheet.Cell(14, 10).RichText.Substring(inletStructure.IndexOf(" " + rpt.InletStructure + " "), (" " + rpt.InletStructure + " ").Length).Bold = true;
                            //    worksheet.Cell(14, 10).RichText.Substring(inletStructure.IndexOf(" " + rpt.InletStructure + " "), (" " + rpt.InletStructure + " ").Length).Strikethrough = false;
                            //}
                        }

                        if (!string.IsNullOrEmpty(outletStructure))
                        {
                            worksheet.Cell(15, 10).Value = outletStructure;
                            //worksheet.Cell(15, 10).RichText.Substring(0, outletStructure.Length).Strikethrough = true;
                            //if (!string.IsNullOrEmpty(rpt.OutletStructure) && outletStructure.IndexOf(" " + rpt.OutletStructure + " ") > -1)
                            //{
                            //    worksheet.Cell(15, 10).RichText.Substring(outletStructure.IndexOf(" " + rpt.OutletStructure + " "), (" " + rpt.OutletStructure + " ").Length).Bold = true;
                            //    worksheet.Cell(15, 10).RichText.Substring(outletStructure.IndexOf(" " + rpt.OutletStructure + " "), (" " + rpt.OutletStructure + " ").Length).Strikethrough = false;
                            //}
                        }

                        worksheet.Cell(10, 14).Value = rpt.GPSEasting;
                        worksheet.Cell(11, 14).Value = rpt.GPSNorthing;

                        worksheet.Cell(18, 2).Value = rpt.ParkingPosition;
                        worksheet.Cell(19, 2).Value = rpt.Accessiblity;
                        worksheet.Cell(20, 2).Value = rpt.PotentialHazards;

                        for (int index1 = 0; index1 < _rpt.Count(); ++index1)
                        {
                            var formC1C2Rpt = _rpt[index1];
                            worksheet.Cell(18, 7 + index1).Value = formC1C2Rpt.Year;
                            worksheet.Cell(19, 7 + index1).Value = formC1C2Rpt.Month;
                            worksheet.Cell(20, 7 + index1).Value = formC1C2Rpt.Day;
                            worksheet.Cell(23, 7 + index1).Value = formC1C2Rpt.CulvertDistress != null ? formC1C2Rpt.CulvertDistress.Replace("-1", "/") : (string)null;
                            IXLCell xlCell1 = worksheet.Cell(24, 7 + index1);
                            int? nullable = formC1C2Rpt.CulvertSeverity;
                            string str3;
                            if (!nullable.HasValue)
                            {
                                str3 = (string)null;
                            }
                            else
                            {
                                nullable = formC1C2Rpt.CulvertSeverity;
                                int num = -1;
                                if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
                                {
                                    nullable = formC1C2Rpt.CulvertSeverity;
                                    str3 = nullable.ToString();
                                }
                                else
                                    str3 = "/";
                            }
                            xlCell1.Value = str3;
                            worksheet.Cell(25, 7 + index1).Value = formC1C2Rpt.WaterwayDistress != null ? formC1C2Rpt.WaterwayDistress.Replace("-1", "/") : (string)null;
                            IXLCell xlCell2 = worksheet.Cell(26, 7 + index1);
                            nullable = formC1C2Rpt.WaterwaySeverity;
                            string str4;
                            if (!nullable.HasValue)
                            {
                                str4 = (string)null;
                            }
                            else
                            {
                                nullable = formC1C2Rpt.WaterwaySeverity;
                                int num = -1;
                                if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
                                {
                                    nullable = formC1C2Rpt.WaterwaySeverity;
                                    str4 = nullable.ToString();
                                }
                                else
                                    str4 = "/";
                            }
                            xlCell2.Value = str4;
                            worksheet.Cell(27, 7 + index1).Value = formC1C2Rpt.EmbankmentDistress != null ? formC1C2Rpt.EmbankmentDistress.Replace("-1", "/") : (string)null;
                            IXLCell xlCell3 = worksheet.Cell(28, 7 + index1);
                            nullable = formC1C2Rpt.EmbankmentSeverity;
                            string str5;
                            if (!nullable.HasValue)
                            {
                                str5 = (string)null;
                            }
                            else
                            {
                                nullable = formC1C2Rpt.EmbankmentSeverity;
                                int num = -1;
                                if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
                                {
                                    nullable = formC1C2Rpt.EmbankmentSeverity;
                                    str5 = nullable.ToString();
                                }
                                else
                                    str5 = "/";
                            }
                            xlCell3.Value = str5;
                            worksheet.Cell(29, 7 + index1).Value = formC1C2Rpt.HeadwallInletDistress != null ? formC1C2Rpt.HeadwallInletDistress.Replace("-1", "/") : (string)null;
                            IXLCell xlCell4 = worksheet.Cell(30, 7 + index1);
                            nullable = formC1C2Rpt.HeadwallInletSeverity;
                            string str6;
                            if (!nullable.HasValue)
                            {
                                str6 = (string)null;
                            }
                            else
                            {
                                nullable = formC1C2Rpt.HeadwallInletSeverity;
                                int num = -1;
                                if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
                                {
                                    nullable = formC1C2Rpt.HeadwallInletSeverity;
                                    str6 = nullable.ToString();
                                }
                                else
                                    str6 = "/";
                            }
                            xlCell4.Value = str6;
                            worksheet.Cell(31, 7 + index1).Value = formC1C2Rpt.WingwallInletDistress != null ? formC1C2Rpt.WingwallInletDistress.Replace("-1", "/") : (string)null;
                            IXLCell xlCell5 = worksheet.Cell(32, 7 + index1);
                            nullable = formC1C2Rpt.WingwalInletSeverity;
                            string str7;
                            if (!nullable.HasValue)
                            {
                                str7 = (string)null;
                            }
                            else
                            {
                                nullable = formC1C2Rpt.WingwalInletSeverity;
                                int num = -1;
                                if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
                                {
                                    nullable = formC1C2Rpt.WingwalInletSeverity;
                                    str7 = nullable.ToString();
                                }
                                else
                                    str7 = "/";
                            }
                            xlCell5.Value = str7;
                            worksheet.Cell(33, 7 + index1).Value = formC1C2Rpt.ApronInletDistress != null ? formC1C2Rpt.ApronInletDistress.Replace("-1", "/") : (string)null;
                            IXLCell xlCell6 = worksheet.Cell(34, 7 + index1);
                            nullable = formC1C2Rpt.ApronInletSeverity;
                            string str8;
                            if (!nullable.HasValue)
                            {
                                str8 = (string)null;
                            }
                            else
                            {
                                nullable = formC1C2Rpt.ApronInletSeverity;
                                int num = -1;
                                if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
                                {
                                    nullable = formC1C2Rpt.ApronInletSeverity;
                                    str8 = nullable.ToString();
                                }
                                else
                                    str8 = "/";
                            }
                            xlCell6.Value = str8;
                            worksheet.Cell(35, 7 + index1).Value = formC1C2Rpt.RiprapInletDistress != null ? (formC1C2Rpt.RiprapInletDistress == "-1" ? "/" : formC1C2Rpt.RiprapInletDistress) : (string)null;
                            IXLCell xlCell7 = worksheet.Cell(36, 7 + index1);
                            nullable = formC1C2Rpt.RiprapInletSeverity;
                            string str9;
                            if (!nullable.HasValue)
                            {
                                str9 = (string)null;
                            }
                            else
                            {
                                nullable = formC1C2Rpt.RiprapInletSeverity;
                                int num = -1;
                                if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
                                {
                                    nullable = formC1C2Rpt.RiprapInletSeverity;
                                    str9 = nullable.ToString();
                                }
                                else
                                    str9 = "/";
                            }
                            xlCell7.Value = str9;
                            worksheet.Cell(37, 7 + index1).Value = formC1C2Rpt.HeadwallOutletDistress != null ? formC1C2Rpt.HeadwallOutletDistress.Replace("-1", "/") : (string)null;
                            IXLCell xlCell8 = worksheet.Cell(38, 7 + index1);
                            nullable = formC1C2Rpt.HeadwallOutletSeverity;
                            string str10;
                            if (!nullable.HasValue)
                            {
                                str10 = (string)null;
                            }
                            else
                            {
                                nullable = formC1C2Rpt.HeadwallOutletSeverity;
                                int num = -1;
                                if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
                                {
                                    nullable = formC1C2Rpt.HeadwallOutletSeverity;
                                    str10 = nullable.ToString();
                                }
                                else
                                    str10 = "/";
                            }
                            xlCell8.Value = str10;
                            worksheet.Cell(39, 7 + index1).Value = formC1C2Rpt.WingwallOutletDistress != null ? formC1C2Rpt.WingwallOutletDistress.Replace("-1", "/") : (string)null;
                            IXLCell xlCell9 = worksheet.Cell(40, 7 + index1);
                            nullable = formC1C2Rpt.WingwallOutletSeverity;
                            string str11;
                            if (!nullable.HasValue)
                            {
                                str11 = (string)null;
                            }
                            else
                            {
                                nullable = formC1C2Rpt.WingwallOutletSeverity;
                                int num = -1;
                                if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
                                {
                                    nullable = formC1C2Rpt.WingwallOutletSeverity;
                                    str11 = nullable.ToString();
                                }
                                else
                                    str11 = "/";
                            }
                            xlCell9.Value = str11;
                            worksheet.Cell(41, 7 + index1).Value = formC1C2Rpt.ApronOutletDistress != null ? formC1C2Rpt.ApronOutletDistress.Replace("-1", "/") : (string)null;
                            IXLCell xlCell10 = worksheet.Cell(42, 7 + index1);
                            nullable = formC1C2Rpt.ApronOutletSeverity;
                            string str12;
                            if (!nullable.HasValue)
                            {
                                str12 = (string)null;
                            }
                            else
                            {
                                nullable = formC1C2Rpt.ApronOutletSeverity;
                                int num = -1;
                                if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
                                {
                                    nullable = formC1C2Rpt.ApronOutletSeverity;
                                    str12 = nullable.ToString();
                                }
                                else
                                    str12 = "/";
                            }
                            xlCell10.Value = str12;
                            worksheet.Cell(43, 7 + index1).Value = formC1C2Rpt.RiprapOutletDistress != null ? formC1C2Rpt.RiprapOutletDistress.Replace("-1", "/") : (string)null;
                            IXLCell xlCell11 = worksheet.Cell(44, 7 + index1);
                            nullable = formC1C2Rpt.RiprapOutletSeverity;
                            string str13;
                            if (!nullable.HasValue)
                            {
                                str13 = (string)null;
                            }
                            else
                            {
                                nullable = formC1C2Rpt.RiprapOutletSeverity;
                                int num = -1;
                                if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
                                {
                                    nullable = formC1C2Rpt.RiprapOutletSeverity;
                                    str13 = nullable.ToString();
                                }
                                else
                                    str13 = "/";
                            }
                            xlCell11.Value = str13;
                            worksheet.Cell(45, 7 + index1).Value = formC1C2Rpt.Barrel_1_Distress != null ? formC1C2Rpt.Barrel_1_Distress.Replace("-1", "/") : (string)null;
                            IXLCell xlCell12 = worksheet.Cell(46, 7 + index1);
                            nullable = formC1C2Rpt.Barrel_1_Severity;
                            string str14;
                            if (!nullable.HasValue)
                            {
                                str14 = (string)null;
                            }
                            else
                            {
                                nullable = formC1C2Rpt.Barrel_1_Severity;
                                int num = -1;
                                if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
                                {
                                    nullable = formC1C2Rpt.Barrel_1_Severity;
                                    str14 = nullable.ToString();
                                }
                                else
                                    str14 = "/";
                            }
                            xlCell12.Value = str14;
                            worksheet.Cell(47, 7 + index1).Value = formC1C2Rpt.Barrel_2_Distress != null ? formC1C2Rpt.Barrel_2_Distress.Replace("-1", "/") : (string)null;
                            IXLCell xlCell13 = worksheet.Cell(48, 7 + index1);
                            nullable = formC1C2Rpt.Barrel_2_Severity;
                            string str15;
                            if (!nullable.HasValue)
                            {
                                str15 = (string)null;
                            }
                            else
                            {
                                nullable = formC1C2Rpt.Barrel_2_Severity;
                                int num = -1;
                                if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
                                {
                                    nullable = formC1C2Rpt.Barrel_2_Severity;
                                    str15 = nullable.ToString();
                                }
                                else
                                    str15 = "/";
                            }
                            xlCell13.Value = str15;
                            worksheet.Cell(49, 7 + index1).Value = formC1C2Rpt.Barrel_3_Distress != null ? formC1C2Rpt.Barrel_3_Distress.Replace("-1", "/") : (string)null;
                            IXLCell xlCell14 = worksheet.Cell(50, 7 + index1);
                            nullable = formC1C2Rpt.Barrel_3_Severity;
                            string str16;
                            if (!nullable.HasValue)
                            {
                                str16 = (string)null;
                            }
                            else
                            {
                                nullable = formC1C2Rpt.Barrel_3_Severity;
                                int num = -1;
                                if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
                                {
                                    nullable = formC1C2Rpt.Barrel_3_Severity;
                                    str16 = nullable.ToString();
                                }
                                else
                                    str16 = "/";
                            }
                            xlCell14.Value = str16;
                            worksheet.Cell(51, 7 + index1).Value = formC1C2Rpt.Barrel_4_Distress != null ? formC1C2Rpt.Barrel_4_Distress.Replace("-1", "/") : (string)null;
                            IXLCell xlCell15 = worksheet.Cell(52, 7 + index1);
                            nullable = formC1C2Rpt.Barrel_4_Severity;
                            string str17;
                            if (!nullable.HasValue)
                            {
                                str17 = (string)null;
                            }
                            else
                            {
                                nullable = formC1C2Rpt.Barrel_4_Severity;
                                int num = -1;
                                if (!(nullable.GetValueOrDefault() == num & nullable.HasValue))
                                {
                                    nullable = formC1C2Rpt.Barrel_4_Severity;
                                    str17 = nullable.ToString();
                                }
                                else
                                    str17 = "/";
                            }
                            xlCell15.Value = str17;
                            int num1 = formC1C2Rpt.BarrelList.Count * 2;
                            if (formC1C2Rpt.BarrelList.Count > 0)
                            {
                                worksheet.Row(52).InsertRowsBelow(formC1C2Rpt.BarrelList.Count * 2);
                                worksheet.Range(worksheet.Cell(46, 1), worksheet.Cell(52 + num1, 2)).Merge();
                                int num2 = 1;
                                for (int index2 = 0; index2 < formC1C2Rpt.BarrelList.Count; ++index2)
                                {
                                    worksheet.Range(worksheet.Cell(52 + num2, 3), worksheet.Cell(52 + num2, 4)).Merge();
                                    worksheet.Range(worksheet.Cell(52 + num2, 8), worksheet.Cell(52 + num2, 9)).Merge();
                                    worksheet.Range(worksheet.Cell(52 + (num2 + 1), 8), worksheet.Cell(52 + (num2 + 1), 9)).Merge();
                                    worksheet.Cell(52 + num2, 3).Value = formC1C2Rpt.BarrelList[index2].Description;
                                    worksheet.Cell(52 + num2, 5).Style.Fill.SetBackgroundColor(XLColor.Gray);
                                    worksheet.Cell(52 + num2, 5).Style.Font.FontSize = 10.0;
                                    worksheet.Cell(52 + num2, 3).Style.Font.Bold = true;
                                    worksheet.Cell(52 + num2, 3).Style.Font.Italic = false;
                                    worksheet.Cell(52 + num2, 5).Style.Font.Bold = true;
                                    worksheet.Cell(52 + num2, 5).Style.Font.Italic = false;
                                    worksheet.Cell(52 + num2, 5).Style.Font.FontColor = XLColor.White;
                                    worksheet.Cell(52 + num2, 5).Value = formC1C2Rpt.BarrelList[index2].Code;
                                    worksheet.Cell(52 + num2, 6).Value = "DISTRESS";
                                    worksheet.Cell(52 + (num2 + 1), 6).Value = "SEVERITY";
                                    worksheet.Cell(52 + num2, 6).Style.Font.Bold = true;
                                    worksheet.Cell(52 + num2, 6).Style.Font.Italic = false;
                                    worksheet.Cell(52 + (num2 + 1), 6).Style.Font.Bold = true;
                                    worksheet.Cell(52 + (num2 + 1), 6).Style.Font.Italic = false;
                                    worksheet.Range(worksheet.Cell(52 + (num2 + 1), 3), worksheet.Cell(52 + (num2 + 1), 5)).Merge();
                                    worksheet.Cell(52 + (num2 + 1), 3).Value = "* for multi cells culvert";
                                    worksheet.Cell(52 + num2, 7 + index1).Value = formC1C2Rpt.BarrelList[index2].Distress != null ? formC1C2Rpt.BarrelList[index2].Distress.Replace("-1", "/") : (string)null;
                                    IXLCell xlCell16 = worksheet.Cell(52 + (num2 + 1), 7 + index1);
                                    nullable = formC1C2Rpt.BarrelList[index2].Severity;
                                    string str18;
                                    if (!nullable.HasValue)
                                    {
                                        str18 = (string)null;
                                    }
                                    else
                                    {
                                        nullable = formC1C2Rpt.BarrelList[index2].Severity;
                                        int num3 = -1;
                                        if (!(nullable.GetValueOrDefault() == num3 & nullable.HasValue))
                                        {
                                            nullable = formC1C2Rpt.BarrelList[index2].Severity;
                                            str18 = nullable.ToString();
                                        }
                                        else
                                            str18 = "/";
                                    }
                                    xlCell16.Value = str18;
                                    num2 += 2;
                                }
                            }
                            worksheet.Cell(53 + num1, 7 + index1).Value = formC1C2Rpt.CulvertApproachDistress != null ? (formC1C2Rpt.CulvertApproachDistress.ToString() == "-1" ? "/" : formC1C2Rpt.CulvertApproachDistress) : null;
                            worksheet.Cell(54 + num1, 7 + index1).Value = formC1C2Rpt.CulvertApproachSeverity != null ? (formC1C2Rpt.CulvertApproachSeverity.ToString() == "-1" ? "/" : formC1C2Rpt.CulvertApproachSeverity.ToString()) : null;
                            worksheet.Cell(74 + num1, 3).Value = formC1C2Rpt.ReportforYear;
                            worksheet.Cell(75 + num1, 3).Value = formC1C2Rpt.AssetRefNO;
                            worksheet.Cell(76 + num1, 3).Value = formC1C2Rpt.RoadCode;
                            worksheet.Cell(77 + num1, 3).Value = formC1C2Rpt.RoadName;
                            worksheet.Cell(74 + num1, 13).Value = formC1C2Rpt.RefernceNo;
                            worksheet.Cell(76 + num1, 13).Value = string.Format("{0}+{1}", formC1C2Rpt.LocationChainageKm, formC1C2Rpt.LocationChainageM);
                            worksheet.Cell(87 + num1, 1).Value = formC1C2Rpt.PartB2ServiceProvider;
                            worksheet.Cell(87 + num1, 9).Value = formC1C2Rpt.PartB2ServicePrvdrCons;
                            worksheet.Cell(100 + num1, 1).Value = formC1C2Rpt.PartCGeneralComments;
                            worksheet.Cell(100 + num1, 9).Value = formC1C2Rpt.PartCGeneralCommentsCons;
                            worksheet.Cell(113 + num1, 1).Value = formC1C2Rpt.PartDFeedback;
                            worksheet.Cell(113 + num1, 9).Value = formC1C2Rpt.PartDFeedbackCons;
                            worksheet.Cell(129 + num1, 2).Value = formC1C2Rpt.InspectedByName;
                            worksheet.Cell(130 + num1, 2).Value = formC1C2Rpt.InspectedByDesignation;
                            worksheet.Cell(131 + num1, 2).Value = formC1C2Rpt.InspectedByDate;
                            worksheet.Cell(129 + num1, 12).Value = formC1C2Rpt.AuditedByName;
                            worksheet.Cell(130 + num1, 12).Value = formC1C2Rpt.AuditedByDesignation;
                            worksheet.Cell(131 + num1, 12).Value = formC1C2Rpt.AuditedByDate;
                            worksheet.Cell(132 + num1, 16).Value = formC1C2Rpt.CulverConditionRate;
                            worksheet.Cell(133 + num1, 16).Value = formC1C2Rpt.HaveIssueFound;
                        }

                        worksheet.Cell(138, 3).Value = rpt.ReportforYear;
                        worksheet.Cell(139, 3).Value = rpt.AssetRefNO;
                        worksheet.Cell(140, 3).Value = rpt.RoadCode;
                        worksheet.Cell(141, 3).Value = rpt.RoadName;
                        worksheet.Cell(138, 12).Value = rpt.RefernceNo;
                        worksheet.Cell(139, 12).Value = 1;
                        worksheet.Cell(140, 12).Value = string.Format("{0}+{1}", rpt.LocationChainageKm, rpt.LocationChainageM);
                        worksheet.Cell(202, 3).Value = rpt.ReportforYear;
                        worksheet.Cell(203, 3).Value = rpt.AssetRefNO;
                        worksheet.Cell(204, 3).Value = rpt.RoadCode;
                        worksheet.Cell(205, 3).Value = rpt.RoadName;
                        worksheet.Cell(202, 12).Value = rpt.RefernceNo;
                        worksheet.Cell(203, 12).Value = 1;
                        worksheet.Cell(204, 12).Value = string.Format("{0}+{1}", rpt.LocationChainageKm, rpt.LocationChainageM);
                        worksheet.Cell(74, 3).Value = rpt.ReportforYear;
                        worksheet.Cell(75, 3).Value = rpt.AssetRefNO;
                        worksheet.Cell(76, 3).Value = rpt.RoadCode;
                        worksheet.Cell(77, 3).Value = rpt.RoadName;
                        worksheet.Cell(74, 12).Value = rpt.RefernceNo;
                        worksheet.Cell(75, 12).Value = 1;
                        worksheet.Cell(76, 12).Value = string.Format("{0}+{1}", rpt.LocationChainageKm, rpt.LocationChainageM);

                        //picture

                        for (int index = 0; index < _rpt[0].Pictures.Count<Pictures>(); ++index)
                        {
                            if (File.Exists(basepath + "/" + _rpt[0].Pictures[index].ImageUrl + "/" + _rpt[0].Pictures[index].FileName))
                            {
                                MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(basepath + "/" + _rpt[0].Pictures[index].ImageUrl + "/" + _rpt[0].Pictures[index].FileName));
                                switch (index)
                                {
                                    case 0:
                                        worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(144, 1), new Point(25, 6)).WithSize(347, 178);
                                        continue;
                                    case 2:
                                        worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(157, 1), new Point(25, 6)).WithSize(347, 178);
                                        continue;
                                    case 3:
                                        worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(157, 9), new Point(4, 6)).WithSize(347, 178);
                                        continue;
                                    case 4:
                                        worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(170, 1), new Point(25, 6)).WithSize(347, 178);
                                        continue;
                                    case 5:
                                        worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(170, 9), new Point(4, 6)).WithSize(347, 178);
                                        continue;
                                    case 6:
                                        worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(183, 1), new Point(25, 6)).WithSize(347, 178);
                                        continue;
                                    case 7:
                                        worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(183, 9), new Point(4, 6)).WithSize(347, 178);
                                        continue;
                                    case 8:
                                        worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(208, 1), new Point(25, 6)).WithSize(347, 178);
                                        continue;
                                    case 9:
                                        worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(208, 9), new Point(4, 6)).WithSize(347, 178);
                                        continue;
                                    case 10:
                                        worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(221, 1), new Point(25, 6)).WithSize(347, 178);
                                        continue;
                                    case 11:
                                        worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(221, 9), new Point(4, 6)).WithSize(347, 178);
                                        continue;
                                    case 12:
                                        worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(234, 1), new Point(25, 6)).WithSize(347, 178);
                                        continue;
                                    case 13:
                                        worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(234, 9), new Point(4, 6)).WithSize(347, 178);
                                        continue;
                                    case 14:
                                        worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(247, 1), new Point(25, 6)).WithSize(347, 178);
                                        continue;
                                    case 15:
                                        worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(247, 9), new Point(4, 6)).WithSize(347, 178);
                                        continue;
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

        public async Task<IEnumerable<SelectListItem>> GetCVIds(DTO.RequestBO.AssetDDLRequestDTO request)
        {
            return await _repo.GetCVId(request);
        }
    }
}
