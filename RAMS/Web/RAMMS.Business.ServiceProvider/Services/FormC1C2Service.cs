using System;
using System.Collections.Generic;
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
        public FormC1C2Service(IRepositoryUnit repoUnit, IFormC1C2Repository repo, IAssetsService assetsService, IMapper mapper, IProcessService proService)
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
            RmFormCvInsHdr header = _mapper.Map<RmFormCvInsHdr>(frmC1C2);
            header.FcvihStatus = StatusList.FormC1C2Init;
            //int i = 0; 
            foreach (var dtl in header.RmFormCvInsDtl)
            {
                dtl.FcvidIimPkRefNoNavigation = null;
                //i++;
            }
            header = await _repo.Save(header, updateSubmit);
            if (header != null && header.FcvihSubmitSts)
            {
                int iResult = processService.Save(new DTO.RequestBO.ProcessDTO()
                {
                    ApproveDate = DateTime.Now,
                    Form = "FormC1C2",
                    IsApprove = true,
                    RefId = header.FcvihPkRefNo,
                    Remarks = "",
                    Stage = header.FcvihStatus
                }).Result;
            }
            frmC1C2 = _mapper.Map<FormC1C2DTO>(header);
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
                frmC1C2.AiGrpType = asset.CulvertType == "Others" ? asset.CulvertType + (assetother != null ? (assetother.CulvertTypeOthers != null ? " - " + Utility.ToString(assetother.CulvertTypeOthers) : "") : "") : asset.CulvertType;
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
        public async Task<IList<RmFormCvInsImage>> AddMultiImageTab(IList<FormC1C2ImageDTO> imagesDTO)
        {
            IList<RmFormCvInsImage> images = new List<RmFormCvInsImage>();
            foreach (var img in imagesDTO)
            {
                images.Add(_mapper.Map<RmFormCvInsImage>(img));
            }
            return await _repo.AddMultiImage(images);
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
                    int nextoadd = 0;
                    int sheetNo = 3;
                    bool IsFirst = true;
                    int index = 0;
                    int ratingrecordnumber = 0;                   

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
                            worksheet.Cell(9, 10).RichText.Substring(0, structureCode.Length).Strikethrough = true;
                            if (!string.IsNullOrEmpty(rpt.StructureCode) && structureCode.IndexOf(" " + rpt.StructureCode + " ") > -1)
                            {
                                worksheet.Cell(9, 10).RichText.Substring(structureCode.IndexOf(" " + rpt.StructureCode + " "), (" " + rpt.StructureCode + " ").Length).Bold = true;
                                worksheet.Cell(9, 10).RichText.Substring(structureCode.IndexOf(" " + rpt.StructureCode + " "), (" " + rpt.StructureCode + " ").Length).Strikethrough = false;
                            }
                        }

                        if (!string.IsNullOrEmpty(culvertType))
                        {
                            worksheet.Cell(12, 10).Value = culvertType;
                            worksheet.Cell(12, 10).RichText.Substring(0, culvertType.Length).Strikethrough = true;
                            if (!string.IsNullOrEmpty(rpt.CulvertType) && culvertType.IndexOf(" " + rpt.CulvertType + " ") > -1)
                            {
                                worksheet.Cell(12, 10).RichText.Substring(culvertType.IndexOf(" " + rpt.CulvertType + " "), (" " + rpt.CulvertType + " ").Length).Bold = true;
                                worksheet.Cell(12, 10).RichText.Substring(culvertType.IndexOf(" " + rpt.CulvertType + " "), (" " + rpt.CulvertType + " ").Length).Strikethrough = false;
                            }
                        }
                        if (!string.IsNullOrEmpty(culvertMaterial))
                        {
                            worksheet.Cell(13, 10).Value = culvertMaterial;
                            worksheet.Cell(13, 10).RichText.Substring(0, culvertMaterial.Length).Strikethrough = true;
                            if (!string.IsNullOrEmpty(rpt.Culvertmaterial) && culvertMaterial.IndexOf(" " + rpt.Culvertmaterial + " ") > -1)
                            {
                                worksheet.Cell(13, 10).RichText.Substring(culvertMaterial.IndexOf(" " + rpt.Culvertmaterial + " "), (" " + rpt.Culvertmaterial + " ").Length).Bold = true;
                                worksheet.Cell(13, 10).RichText.Substring(culvertMaterial.IndexOf(" " + rpt.Culvertmaterial + " "), (" " + rpt.Culvertmaterial + " ").Length).Strikethrough = false;
                            }
                        }

                        if (!string.IsNullOrEmpty(inletStructure))
                        {
                            worksheet.Cell(14, 10).Value = inletStructure;
                            worksheet.Cell(14, 10).RichText.Substring(0, inletStructure.Length).Strikethrough = true;
                            if (!string.IsNullOrEmpty(rpt.InletStructure) && inletStructure.IndexOf(" " + rpt.InletStructure + " ") > -1)
                            {
                                worksheet.Cell(14, 10).RichText.Substring(inletStructure.IndexOf(" " + rpt.InletStructure + " "), (" " + rpt.InletStructure + " ").Length).Bold = true;
                                worksheet.Cell(14, 10).RichText.Substring(inletStructure.IndexOf(" " + rpt.InletStructure + " "), (" " + rpt.InletStructure + " ").Length).Strikethrough = false;
                            }
                        }

                        if (!string.IsNullOrEmpty(outletStructure))
                        {
                            worksheet.Cell(15, 10).Value = outletStructure;
                            worksheet.Cell(15, 10).RichText.Substring(0, outletStructure.Length).Strikethrough = true;
                            if (!string.IsNullOrEmpty(rpt.OutletStructure) && outletStructure.IndexOf(" " + rpt.OutletStructure + " ") > -1)
                            {
                                worksheet.Cell(15, 10).RichText.Substring(outletStructure.IndexOf(" " + rpt.OutletStructure + " "), (" " + rpt.OutletStructure + " ").Length).Bold = true;
                                worksheet.Cell(15, 10).RichText.Substring(outletStructure.IndexOf(" " + rpt.OutletStructure + " "), (" " + rpt.OutletStructure + " ").Length).Strikethrough = false;
                            }
                        }

                        worksheet.Cell(10, 14).Value = rpt.GPSEasting;
                        worksheet.Cell(11, 14).Value = rpt.GPSNorthing;

                        worksheet.Cell(18, 2).Value = rpt.ParkingPosition;
                        worksheet.Cell(19, 2).Value = rpt.Accessiblity;
                        worksheet.Cell(20, 2).Value = rpt.PotentialHazards;

                        for (int j = 0; j < _rpt.Count(); j++)
                        {
                            rpt = _rpt[j];
                            worksheet.Cell(18, 7 + j).Value = rpt.Year;
                            worksheet.Cell(19, 7 + j).Value = rpt.Month;
                            worksheet.Cell(20, 7 + j).Value = rpt.Day;

                            worksheet.Cell(23, 7 + j).Value = rpt.CulvertDistress != null ? (rpt.CulvertDistress.Replace("-1", "/")) : null;
                            worksheet.Cell(24, 7 + j).Value = rpt.CulvertSeverity != null ? (rpt.CulvertSeverity == -1 ? "/" : rpt.CulvertSeverity.ToString()) : null;
                            worksheet.Cell(25, 7 + j).Value = rpt.WaterwayDistress != null ? (rpt.WaterwayDistress.Replace("-1", "/")) : null;
                            worksheet.Cell(26, 7 + j).Value = rpt.WaterwaySeverity != null ? (rpt.WaterwaySeverity == -1 ? "/" : rpt.WaterwaySeverity.ToString()) : null;
                            worksheet.Cell(27, 7 + j).Value = rpt.EmbankmentDistress != null ? (rpt.EmbankmentDistress.Replace("-1", "/")) : null;
                            worksheet.Cell(28, 7 + j).Value = rpt.EmbankmentSeverity != null ? (rpt.EmbankmentSeverity == -1 ? "/" : rpt.EmbankmentSeverity.ToString()) : null;
                            worksheet.Cell(29, 7 + j).Value = rpt.HeadwallInletDistress != null ? (rpt.HeadwallInletDistress.Replace("-1", "/")) : null;
                            worksheet.Cell(30, 7 + j).Value = rpt.HeadwallInletSeverity != null ? (rpt.HeadwallInletSeverity == -1 ? "/" : rpt.HeadwallInletSeverity.ToString()) : null;
                            worksheet.Cell(31, 7 + j).Value = rpt.WingwallInletDistress != null ? (rpt.WingwallInletDistress.Replace("-1", "/")) : null;
                            worksheet.Cell(32, 7 + j).Value = rpt.WingwalInletSeverity != null ? (rpt.WingwalInletSeverity == -1 ? "/" : rpt.WingwalInletSeverity.ToString()) : null;
                            worksheet.Cell(33, 7 + j).Value = rpt.ApronInletDistress != null ? (rpt.ApronInletDistress.Replace("-1", "/")) : null;
                            worksheet.Cell(34, 7 + j).Value = rpt.ApronInletSeverity != null ? (rpt.ApronInletSeverity == -1 ? "/" : rpt.ApronInletSeverity.ToString()) : null;
                            worksheet.Cell(35, 7 + j).Value = rpt.RiprapInletDistress != null ? (rpt.RiprapInletDistress == "-1" ? "/" : rpt.RiprapInletDistress) : null;
                            worksheet.Cell(36, 7 + j).Value = rpt.RiprapInletSeverity != null ? (rpt.RiprapInletSeverity == -1 ? "/" : rpt.RiprapInletSeverity.ToString()) : null;
                            worksheet.Cell(37, 7 + j).Value = rpt.HeadwallOutletDistress != null ? (rpt.HeadwallOutletDistress.Replace("-1", "/")) : null;
                            worksheet.Cell(38, 7 + j).Value = rpt.HeadwallOutletSeverity != null ? (rpt.HeadwallOutletSeverity == -1 ? "/" : rpt.HeadwallOutletSeverity.ToString()) : null;
                            worksheet.Cell(39, 7 + j).Value = rpt.WingwallOutletDistress != null ? (rpt.WingwallOutletDistress.Replace("-1", "/")) : null;
                            worksheet.Cell(40, 7 + j).Value = rpt.WingwallOutletSeverity != null ? (rpt.WingwallOutletSeverity == -1 ? "/" : rpt.WingwallOutletSeverity.ToString()) : null;
                            worksheet.Cell(41, 7 + j).Value = rpt.ApronOutletDistress != null ? (rpt.ApronOutletDistress.Replace("-1", "/")) : null;
                            worksheet.Cell(42, 7 + j).Value = rpt.ApronOutletSeverity != null ? (rpt.ApronOutletSeverity == -1 ? "/" : rpt.ApronOutletSeverity.ToString()) : null;
                            worksheet.Cell(43, 7 + j).Value = rpt.RiprapOutletDistress != null ? (rpt.RiprapOutletDistress.Replace("-1", "/")) : null;
                            worksheet.Cell(44, 7 + j).Value = rpt.RiprapOutletSeverity != null ? (rpt.RiprapOutletSeverity == -1 ? "/" : rpt.RiprapOutletSeverity.ToString()) : null;

                            worksheet.Cell(45, 7 + j).Value = rpt.Barrel_1_Distress != null ? rpt.Barrel_1_Distress.Replace("-1", "/") : null;
                            worksheet.Cell(46, 7 + j).Value = rpt.Barrel_1_Severity != null ? (rpt.Barrel_1_Severity == -1 ? "/" : rpt.Barrel_1_Severity.ToString()) : null;
                            worksheet.Cell(47, 7 + j).Value = rpt.Barrel_2_Distress != null ? (rpt.Barrel_2_Distress.Replace("-1", "/")) : null;
                            worksheet.Cell(48, 7 + j).Value = rpt.Barrel_2_Severity != null ? (rpt.Barrel_2_Severity == -1 ? "/" : rpt.Barrel_2_Severity.ToString()) : null;
                            worksheet.Cell(49, 7 + j).Value = rpt.Barrel_3_Distress != null ? (rpt.Barrel_3_Distress.Replace("-1", "/")) : null;
                            worksheet.Cell(50, 7 + j).Value = rpt.Barrel_3_Severity != null ? (rpt.Barrel_3_Severity == -1 ? "/" : rpt.Barrel_3_Severity.ToString()) : null;
                            worksheet.Cell(51, 7 + j).Value = rpt.Barrel_4_Distress != null ? (rpt.Barrel_4_Distress.Replace("-1", "/")) : null;
                            worksheet.Cell(52, 7 + j).Value = rpt.Barrel_4_Severity != null ? (rpt.Barrel_4_Severity == -1 ? "/" : rpt.Barrel_4_Severity.ToString()) : null;

                            int furthercellincrement = rpt.BarrelList.Count * 2;

                            if (rpt.BarrelList.Count > 0)
                            {
                                worksheet.Row(52).InsertRowsBelow(rpt.BarrelList.Count * 2);
                                worksheet.Range(worksheet.Cell(46, 1), worksheet.Cell(52 + furthercellincrement, 2)).Merge();
                                int d = 1;
                                for (int i = 0; i < rpt.BarrelList.Count; i++)
                                {
                                    worksheet.Range(worksheet.Cell(52 + (d), 3), worksheet.Cell(52 + (d), 4)).Merge();
                                    worksheet.Range(worksheet.Cell(52 + (d), 8), worksheet.Cell(52 + (d), 9)).Merge();
                                    worksheet.Range(worksheet.Cell(52 + (d + 1), 8), worksheet.Cell(52 + (d + 1), 9)).Merge();
                                    worksheet.Cell(52 + (d), 3).Value = rpt.BarrelList[i].Description;
                                    worksheet.Cell(52 + (d), 5).Style.Fill.SetBackgroundColor(XLColor.Gray);
                                    worksheet.Cell(52 + (d), 5).Style.Font.FontSize = 10;
                                    worksheet.Cell(52 + (d), 3).Style.Font.Bold = true;
                                    worksheet.Cell(52 + (d), 3).Style.Font.Italic = false;
                                    worksheet.Cell(52 + (d), 5).Style.Font.Bold = true;
                                    worksheet.Cell(52 + (d), 5).Style.Font.Italic = false;
                                    worksheet.Cell(52 + (d), 5).Style.Font.FontColor = XLColor.White;
                                    worksheet.Cell(52 + (d), 5).Value = rpt.BarrelList[i].Code;
                                    worksheet.Cell(52 + (d), 6).Value = "DISTRESS";
                                    worksheet.Cell(52 + (d + 1), 6).Value = "SEVERITY";
                                    worksheet.Cell(52 + (d), 6).Style.Font.Bold = true;
                                    worksheet.Cell(52 + (d), 6).Style.Font.Italic = false;
                                    worksheet.Cell(52 + (d + 1), 6).Style.Font.Bold = true;
                                    worksheet.Cell(52 + (d + 1), 6).Style.Font.Italic = false;
                                    worksheet.Range(worksheet.Cell(52 + (d + 1), 3), worksheet.Cell(52 + (d + 1), 5)).Merge();
                                    worksheet.Cell(52 + (d + 1), 3).Value = "* for multi cells culvert";
                                    worksheet.Cell(52 + (d), 7 + j).Value = rpt.BarrelList[i].Distress != null ? (rpt.BarrelList[i].Distress.Replace("-1", "/")) : null;
                                    worksheet.Cell(52 + (d + 1), 7 + j).Value = rpt.BarrelList[i].Severity != null ? (rpt.BarrelList[i].Severity == -1 ? "/" : rpt.BarrelList[i].Severity.ToString()) : null;
                                    d += 2;
                                }
                            }




                            worksheet.Cell(53 + furthercellincrement, 7 + j).Value = rpt.CulvertApproachDistress != null ? (rpt.CulvertApproachDistress.ToString() == "-1" ? "/" : rpt.CulvertApproachDistress) : null;
                            worksheet.Cell(54 + furthercellincrement, 7 + j).Value = rpt.CulvertApproachSeverity != null ? (rpt.CulvertApproachSeverity.ToString() == "-1" ? "/" : rpt.CulvertApproachSeverity.ToString()) : null;


                            worksheet.Cell(74 + furthercellincrement, 3).Value = rpt.ReportforYear;
                            worksheet.Cell(75 + furthercellincrement, 3).Value = rpt.AssetRefNO;
                            worksheet.Cell(76 + furthercellincrement, 3).Value = rpt.RoadCode;
                            worksheet.Cell(77 + furthercellincrement, 3).Value = rpt.RoadName;

                            worksheet.Cell(74 + furthercellincrement, 13).Value = rpt.RefernceNo;
                            // worksheet.Cell(75 + furthercellincrement, 13).Value = rpt.RatingRecordNo;
                            worksheet.Cell(76 + furthercellincrement, 13).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";
                            worksheet.Cell(87 + furthercellincrement, 1).Value = rpt.PartB2ServiceProvider;
                            worksheet.Cell(87 + furthercellincrement, 9).Value = rpt.PartB2ServicePrvdrCons;
                            worksheet.Cell(100 + furthercellincrement, 1).Value = rpt.PartCGeneralComments;
                            worksheet.Cell(100 + furthercellincrement, 9).Value = rpt.PartCGeneralCommentsCons;
                            worksheet.Cell(113 + furthercellincrement, 1).Value = rpt.PartDFeedback;
                            worksheet.Cell(113 + furthercellincrement, 9).Value = rpt.PartDFeedbackCons;


                            worksheet.Cell(129 + furthercellincrement, 2).Value = rpt.InspectedByName;
                            worksheet.Cell(130 + furthercellincrement, 2).Value = rpt.InspectedByDesignation;
                            worksheet.Cell(131 + furthercellincrement, 2).Value = rpt.InspectedByDate;

                            worksheet.Cell(129 + furthercellincrement, 12).Value = rpt.AuditedByName;
                            worksheet.Cell(130 + furthercellincrement, 12).Value = rpt.AuditedByDesignation;
                            worksheet.Cell(131 + furthercellincrement, 12).Value = rpt.AuditedByDate;

                            worksheet.Cell(132 + furthercellincrement, 16).Value = rpt.CulverConditionRate;
                            worksheet.Cell(133 + furthercellincrement, 16).Value = rpt.HaveIssueFound;
                        }

                        #region Image Printing

                        worksheet.Cell(138, 3).Value = rpt.ReportforYear;
                        worksheet.Cell(139, 3).Value = rpt.AssetRefNO;
                        worksheet.Cell(140, 3).Value = rpt.RoadCode;
                        worksheet.Cell(141, 3).Value = rpt.RoadName;
                        worksheet.Cell(138, 12).Value = rpt.RefernceNo;
                        worksheet.Cell(139, 12).Value = 1;
                        worksheet.Cell(140, 12).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";

                        worksheet.Cell(202, 3).Value = rpt.ReportforYear;
                        worksheet.Cell(203, 3).Value = rpt.AssetRefNO;
                        worksheet.Cell(204, 3).Value = rpt.RoadCode;
                        worksheet.Cell(205, 3).Value = rpt.RoadName;
                        worksheet.Cell(202, 12).Value = rpt.RefernceNo;
                        worksheet.Cell(203, 12).Value = 1;
                        worksheet.Cell(204, 12).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";

                        worksheet.Cell(74, 3).Value = rpt.ReportforYear;
                        worksheet.Cell(75, 3).Value = rpt.AssetRefNO;
                        worksheet.Cell(76, 3).Value = rpt.RoadCode;
                        worksheet.Cell(77, 3).Value = rpt.RoadName;
                        worksheet.Cell(74, 12).Value = rpt.RefernceNo;
                        worksheet.Cell(75, 12).Value = 1;
                        worksheet.Cell(76, 12).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";
                        //pictures = rpt.Pictures.Take(6).ToArray();
                        for (int i = 0; i < _rpt[0].Pictures.Count(); i++)
                        {
                            if (File.Exists($"{basepath}/{_rpt[0].Pictures[i].ImageUrl}/{_rpt[0].Pictures[i].FileName}"))
                            {
                                byte[] buff = File.ReadAllBytes($"{basepath}/{_rpt[0].Pictures[i].ImageUrl}/{_rpt[0].Pictures[i].FileName}");
                                System.IO.MemoryStream str = new System.IO.MemoryStream(buff);
                                switch (i)
                                {
                                    case 0:
                                        worksheet.AddPicture(str).MoveTo(worksheet.Cell(144, 1), new System.Drawing.Point(25, 6)).WithSize(347, 178);
                                        break;
                                    case 1:                                        
                                        break;
                                    case 2:
                                        worksheet.AddPicture(str).MoveTo(worksheet.Cell(157, 1), new System.Drawing.Point(25, 6)).WithSize(347, 178);                                        
                                        break;
                                    case 3:
                                        worksheet.AddPicture(str).MoveTo(worksheet.Cell(157, 9), new System.Drawing.Point(4, 6)).WithSize(347, 178);
                                        break;
                                    case 4:
                                        worksheet.AddPicture(str).MoveTo(worksheet.Cell(170, 1), new System.Drawing.Point(25, 6)).WithSize(347, 178);
                                        break;
                                    case 5:
                                        worksheet.AddPicture(str).MoveTo(worksheet.Cell(170, 9), new System.Drawing.Point(4, 6)).WithSize(347, 178);
                                        break;
                                    case 6:
                                        worksheet.AddPicture(str).MoveTo(worksheet.Cell(183, 1), new System.Drawing.Point(25, 6)).WithSize(347, 178);
                                        break;
                                    case 7:
                                        worksheet.AddPicture(str).MoveTo(worksheet.Cell(183, 9), new System.Drawing.Point(4, 6)).WithSize(347, 178);
                                        break;
                                    case 8:
                                        worksheet.AddPicture(str).MoveTo(worksheet.Cell(208, 1), new System.Drawing.Point(25, 6)).WithSize(347, 178);
                                        break;
                                    case 9:
                                        worksheet.AddPicture(str).MoveTo(worksheet.Cell(208, 9), new System.Drawing.Point(4, 6)).WithSize(347, 178);
                                        break;
                                    case 10:
                                        worksheet.AddPicture(str).MoveTo(worksheet.Cell(221, 1), new System.Drawing.Point(25, 6)).WithSize(347, 178);
                                        break;
                                    case 11:
                                        worksheet.AddPicture(str).MoveTo(worksheet.Cell(221, 9), new System.Drawing.Point(4, 6)).WithSize(347, 178);
                                        break;
                                    case 12:
                                        worksheet.AddPicture(str).MoveTo(worksheet.Cell(234, 1), new System.Drawing.Point(25, 6)).WithSize(347, 178);
                                        break;
                                    case 13:
                                        worksheet.AddPicture(str).MoveTo(worksheet.Cell(234, 9), new System.Drawing.Point(4, 6)).WithSize(347, 178);
                                        break;
                                    case 14:
                                        worksheet.AddPicture(str).MoveTo(worksheet.Cell(247, 1), new System.Drawing.Point(25, 6)).WithSize(347, 178);
                                        break;
                                    case 15:
                                        worksheet.AddPicture(str).MoveTo(worksheet.Cell(247, 9), new System.Drawing.Point(4, 6)).WithSize(347, 178);
                                        break;
                                }
                            }                            
                        }

                        #endregion


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
