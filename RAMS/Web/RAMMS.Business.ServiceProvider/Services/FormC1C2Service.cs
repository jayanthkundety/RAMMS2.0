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
        public FormC1C2Service(IRepositoryUnit repoUnit, IFormC1C2Repository repo, IAssetsService assetsService, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
            _assetsService = assetsService;
            _repoUnit = repoUnit;
        }
        public async Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData)
        {
            return await _repo.GetHeaderGrid(searchData);
        }
        public async Task<FormC1C2DTO> Save(FormC1C2DTO frmC1C2, bool updateSubmit)
        {
            RmFormCvInsHdr header = _mapper.Map<RmFormCvInsHdr>(frmC1C2);
            //int i = 0; 
            foreach (var dtl in header.RmFormCvInsDtl)
            {
                dtl.FcvidIimPkRefNoNavigation = null;
                //i++;
            }

            header = await _repo.Save(header, updateSubmit);
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
                    int nextoadd = 0;
                    int sheetNo = 3;
                    bool IsFirst = true;
                    int index = 0;
                    foreach (var rpt in _rpt)
                    {
                        Pictures[] pictures;
                        pictures = rpt.Pictures.Skip(index * 6).Take(6).ToArray();
                        index++;
                        int noofsheets = (rpt.Pictures.Count() / 6) + ((rpt.Pictures.Count() % 6) > 0 ? 1 : 1);
                        using (var book = new XLWorkbook(cachefile))
                        {
                            IXLWorksheet image = nextoadd == 0 && IsFirst ? workbook.Worksheet(2) : book.Worksheet(2);
                            image.Cell(4, 6).Value = rpt.ReportforYear;
                            image.Cell(5, 6).Value = rpt.AssetRefNO;
                            image.Cell(6, 6).Value = rpt.RoadCode;
                            image.Cell(7, 6).Value = rpt.RoadName;
                            image.Cell(4, 17).Value = rpt.RefernceNo;
                            image.Cell(5, 17).Value = index;
                            image.Cell(6, 17).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";
                            pictures = rpt.Pictures.Take(6).ToArray();
                            for (int i = 0; i < pictures.Count(); i++)
                            {
                                if (File.Exists($"{basepath}/{pictures[i].ImageUrl}/{pictures[i].FileName}"))
                                {
                                    byte[] buff = File.ReadAllBytes($"{basepath}/{pictures[i].ImageUrl}/{pictures[i].FileName}");
                                    System.IO.MemoryStream str = new System.IO.MemoryStream(buff);
                                    switch (i)
                                    {
                                        case 0:
                                            image.AddPicture(str).MoveTo(image.Cell(9, 4)).WithSize(360, 170);
                                            image.Cell(17, 4).Value = pictures[i].Type;
                                            break;
                                        case 1:
                                            image.AddPicture(str).MoveTo(image.Cell(9, 15)).WithSize(360, 170);
                                            image.Cell(17, 15).Value = pictures[i].Type;
                                            if (!pictures[i].Type.Contains("P1"))
                                            {
                                                image.Range("O9:W9").Style.Border.TopBorder = XLBorderStyleValues.Thick;
                                                image.Range("O9:O16").Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                                                image.Range("W9:W16").Style.Border.RightBorder = XLBorderStyleValues.Thick;
                                                image.Range("O16:W16").Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                                            }
                                            break;
                                        case 2:
                                            image.AddPicture(str).MoveTo(image.Cell(20, 4)).WithSize(360, 170);
                                            image.Cell(28, 4).Value = pictures[i].Type;
                                            break;
                                        case 3:
                                            image.AddPicture(str).MoveTo(image.Cell(20, 15)).WithSize(360, 170);
                                            image.Cell(28, 15).Value = pictures[i].Type;
                                            break;
                                        case 4:
                                            image.AddPicture(str).MoveTo(image.Cell(31, 4)).WithSize(360, 170);
                                            image.Cell(39, 4).Value = pictures[i].Type;
                                            break;
                                        case 5:
                                            image.AddPicture(str).MoveTo(image.Cell(31, 15)).WithSize(360, 170);
                                            image.Cell(39, 15).Value = pictures[i].Type;
                                            break;
                                    }
                                }

                                switch (i)
                                {
                                    case 0:
                                        image.Cell(17, 4).Value = pictures[i].Type;
                                        break;
                                    case 1:
                                        image.Cell(17, 15).Value = pictures[i].Type;
                                        break;
                                    case 2:
                                        image.Cell(28, 4).Value = pictures[i].Type;
                                        break;
                                    case 3:
                                        image.Cell(28, 15).Value = pictures[i].Type;
                                        break;
                                    case 4:
                                        image.Cell(39, 4).Value = pictures[i].Type;
                                        break;
                                    case 5:
                                        image.Cell(39, 15).Value = pictures[i].Type;
                                        break;
                                }
                            }
                            if (nextoadd > 0 || !IsFirst)
                            {

                                image.Worksheet.Name = $"sheet{sheetNo}";
                                workbook.AddWorksheet(image);
                                nextoadd++;
                                sheetNo++;
                            }
                            IsFirst = false;
                        }
                        int tobeskipped = 1 + index;
                        for (int sheet = 2; sheet <= noofsheets; sheet++)
                        {
                            using (var tempworkbook = new XLWorkbook(cachefile))
                            {
                                string sheetname = $"sheet{sheetNo}";
                                IXLWorksheet copysheet = tempworkbook.Worksheet(2);
                                copysheet.Worksheet.Name = sheetname;
                                copysheet.Cell(4, 6).Value = rpt.ReportforYear;
                                copysheet.Cell(5, 6).Value = rpt.AssetRefNO;
                                copysheet.Cell(6, 6).Value = rpt.RoadCode;
                                copysheet.Cell(7, 6).Value = rpt.RoadName;
                                copysheet.Cell(4, 17).Value = rpt.RefernceNo;
                                copysheet.Cell(5, 17).Value = index;
                                copysheet.Cell(6, 17).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";
                                pictures = rpt.Pictures.Skip((tobeskipped - 1) * 6).Take(6).ToArray();
                                for (int i = 0; i < pictures.Count(); i++)
                                {
                                    if (File.Exists($"{basepath}/{pictures[i].ImageUrl}/{pictures[i].FileName}"))
                                    {
                                        byte[] buff = File.ReadAllBytes($"{basepath}/{pictures[i].ImageUrl}/{pictures[i].FileName}");
                                        System.IO.MemoryStream str = new System.IO.MemoryStream(buff);
                                        switch (i)
                                        {
                                            case 0:
                                                copysheet.AddPicture(str).MoveTo(copysheet.Cell(9, 4)).WithSize(360, 170);
                                                copysheet.Cell(17, 4).Value = pictures[i].Type;
                                                break;
                                            case 1:
                                                copysheet.AddPicture(str).MoveTo(copysheet.Cell(9, 15)).WithSize(360, 170);
                                                copysheet.Cell(17, 15).Value = pictures[i].Type;

                                                break;
                                            case 2:

                                                copysheet.AddPicture(str).MoveTo(copysheet.Cell(20, 4)).WithSize(360, 170);
                                                copysheet.Cell(28, 4).Value = pictures[i].Type;
                                                break;
                                            case 3:
                                                copysheet.AddPicture(str).MoveTo(copysheet.Cell(20, 15)).WithSize(360, 170);
                                                copysheet.Cell(28, 15).Value = pictures[i].Type;
                                                break;
                                            case 4:
                                                copysheet.AddPicture(str).MoveTo(copysheet.Cell(31, 4)).WithSize(360, 170);
                                                copysheet.Cell(39, 4).Value = pictures[i].Type;
                                                break;
                                            case 5:
                                                copysheet.AddPicture(str).MoveTo(copysheet.Cell(31, 15)).WithSize(360, 170);
                                                copysheet.Cell(39, 15).Value = pictures[i].Type;
                                                break;
                                        }
                                    }

                                    switch (i)
                                    {
                                        case 0:
                                            copysheet.Cell(17, 4).Value = pictures[i].Type;
                                            break;
                                        case 1:
                                            copysheet.Cell(17, 15).Value = pictures[i].Type;
                                            if (!pictures[i].Type.Contains("P1"))
                                            {
                                                copysheet.Range("O9:W9").Style.Border.TopBorder = XLBorderStyleValues.Thick;
                                                copysheet.Range("O9:O16").Style.Border.LeftBorder = XLBorderStyleValues.Thick;
                                                copysheet.Range("W9:W16").Style.Border.RightBorder = XLBorderStyleValues.Thick;
                                                copysheet.Range("O16:W16").Style.Border.BottomBorder = XLBorderStyleValues.Thick;
                                            }
                                            break;
                                        case 2:
                                            copysheet.Cell(28, 4).Value = pictures[i].Type;
                                            break;
                                        case 3:
                                            copysheet.Cell(28, 15).Value = pictures[i].Type;
                                            break;
                                        case 4:
                                            copysheet.Cell(39, 4).Value = pictures[i].Type;
                                            break;
                                        case 5:
                                            copysheet.Cell(39, 15).Value = pictures[i].Type;
                                            break;
                                    }
                                }

                                tobeskipped++;
                                nextoadd++;
                                workbook.AddWorksheet(copysheet);
                                sheetNo++;
                                if (nextoadd == 0)
                                {
                                    nextoadd++;
                                }
                            }
                        }
                    }

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
                        worksheet.Cell(6, 11).Value = rpt.RoadName;
                        worksheet.Cell(7, 11).Value = rpt.RoadCode;
                        worksheet.Cell(8, 11).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";
                        worksheet.Cell(10, 11).Value = rpt.CulverSkew;
                        worksheet.Cell(11, 11).Value = rpt.CulvertLength;

                        if (!string.IsNullOrEmpty(structureCode))
                        {
                            worksheet.Cell(9, 11).Value = structureCode;
                            worksheet.Cell(9, 11).RichText.Substring(0, structureCode.Length).Strikethrough = true;
                            if (!string.IsNullOrEmpty(rpt.StructureCode) && structureCode.IndexOf(" " + rpt.StructureCode + " ") > -1)
                            {
                                worksheet.Cell(9, 11).RichText.Substring(structureCode.IndexOf(" " + rpt.StructureCode + " "), (" " + rpt.StructureCode + " ").Length).Bold = true;
                                worksheet.Cell(9, 11).RichText.Substring(structureCode.IndexOf(" " + rpt.StructureCode + " "), (" " + rpt.StructureCode + " ").Length).Strikethrough = false;
                            }
                        }

                        if (!string.IsNullOrEmpty(culvertType))
                        {
                            worksheet.Cell(12, 11).Value = culvertType;
                            worksheet.Cell(12, 11).RichText.Substring(0, culvertType.Length).Strikethrough = true;
                            if (!string.IsNullOrEmpty(rpt.CulvertType) && culvertType.IndexOf(" " + rpt.CulvertType + " ") > -1)
                            {
                                worksheet.Cell(12, 11).RichText.Substring(culvertType.IndexOf(" " + rpt.CulvertType + " "), (" " + rpt.CulvertType + " ").Length).Bold = true;
                                worksheet.Cell(12, 11).RichText.Substring(culvertType.IndexOf(" " + rpt.CulvertType + " "), (" " + rpt.CulvertType + " ").Length).Strikethrough = false;
                            }
                        }
                        if (!string.IsNullOrEmpty(culvertMaterial))
                        {
                            worksheet.Cell(13, 11).Value = culvertMaterial;
                            worksheet.Cell(13, 11).RichText.Substring(0, culvertMaterial.Length).Strikethrough = true;
                            if (!string.IsNullOrEmpty(rpt.Culvertmaterial) && culvertMaterial.IndexOf(" " + rpt.Culvertmaterial + " ") > -1)
                            {
                                worksheet.Cell(13, 11).RichText.Substring(culvertMaterial.IndexOf(" " + rpt.Culvertmaterial + " "), (" " + rpt.Culvertmaterial + " ").Length).Bold = true;
                                worksheet.Cell(13, 11).RichText.Substring(culvertMaterial.IndexOf(" " + rpt.Culvertmaterial + " "), (" " + rpt.Culvertmaterial + " ").Length).Strikethrough = false;
                            }
                        }

                        if (!string.IsNullOrEmpty(inletStructure))
                        {
                            worksheet.Cell(14, 11).Value = inletStructure;
                            worksheet.Cell(14, 11).RichText.Substring(0, inletStructure.Length).Strikethrough = true;
                            if (!string.IsNullOrEmpty(rpt.InletStructure) && inletStructure.IndexOf(" " + rpt.InletStructure + " ") > -1)
                            {
                                worksheet.Cell(14, 11).RichText.Substring(inletStructure.IndexOf(" " + rpt.InletStructure + " "), (" " + rpt.InletStructure + " ").Length).Bold = true;
                                worksheet.Cell(14, 11).RichText.Substring(inletStructure.IndexOf(" " + rpt.InletStructure + " "), (" " + rpt.InletStructure + " ").Length).Strikethrough = false;
                            }
                        }

                        if (!string.IsNullOrEmpty(outletStructure))
                        {
                            worksheet.Cell(15, 11).Value = outletStructure;
                            worksheet.Cell(15, 11).RichText.Substring(0, outletStructure.Length).Strikethrough = true;
                            if (!string.IsNullOrEmpty(rpt.OutletStructure) && outletStructure.IndexOf(" " + rpt.OutletStructure + " ") > -1)
                            {
                                worksheet.Cell(15, 11).RichText.Substring(outletStructure.IndexOf(" " + rpt.OutletStructure + " "), (" " + rpt.OutletStructure + " ").Length).Bold = true;
                                worksheet.Cell(15, 11).RichText.Substring(outletStructure.IndexOf(" " + rpt.OutletStructure + " "), (" " + rpt.OutletStructure + " ").Length).Strikethrough = false;
                            }
                        }

                        worksheet.Cell(10, 15).Value = rpt.GPSEasting;
                        worksheet.Cell(11, 15).Value = rpt.GPSNorthing;

                        worksheet.Cell(18, 2).Value = rpt.ParkingPosition;
                        worksheet.Cell(19, 2).Value = rpt.Accessiblity;
                        worksheet.Cell(20, 2).Value = rpt.PotentialHazards;

                        //int j = 0;
                        //{
                        //    rpt = _rpt[0];
                        //    worksheet.Cell(18, 7 + j).Value = rpt.Year;
                        //    worksheet.Cell(19, 7 + j).Value = rpt.Month;
                        //    worksheet.Cell(20, 7 + j).Value = rpt.Day;

                        //    worksheet.Cell(23, 7 + j).Value = rpt.CulvertDistress != null ? (rpt.CulvertDistress.Replace("-1", "/")) : null;
                        //    worksheet.Cell(24, 7 + j).Value = rpt.CulvertSeverity != null ? (rpt.CulvertSeverity == -1 ? "/" : rpt.CulvertSeverity.ToString()) : null;
                        //    worksheet.Cell(25, 7 + j).Value = rpt.WaterwayDistress != null ? (rpt.WaterwayDistress.Replace("-1", "/")) : null;
                        //    worksheet.Cell(26, 7 + j).Value = rpt.WaterwaySeverity != null ? (rpt.WaterwaySeverity == -1 ? "/" : rpt.WaterwaySeverity.ToString()) : null;
                        //    worksheet.Cell(27, 7 + j).Value = rpt.EmbankmentDistress != null ? (rpt.EmbankmentDistress.Replace("-1", "/")) : null;
                        //    worksheet.Cell(28, 7 + j).Value = rpt.EmbankmentSeverity != null ? (rpt.EmbankmentSeverity == -1 ? "/" : rpt.EmbankmentSeverity.ToString()) : null;
                        //    worksheet.Cell(29, 7 + j).Value = rpt.HeadwallInletDistress != null ? (rpt.HeadwallInletDistress.Replace("-1", "/")) : null;
                        //    worksheet.Cell(30, 7 + j).Value = rpt.HeadwallInletSeverity != null ? (rpt.HeadwallInletSeverity == -1 ? "/" : rpt.HeadwallInletSeverity.ToString()) : null;
                        //    worksheet.Cell(31, 7 + j).Value = rpt.WingwallInletDistress != null ? (rpt.WingwallInletDistress.Replace("-1", "/")) : null;
                        //    worksheet.Cell(32, 7 + j).Value = rpt.WingwalInletSeverity != null ? (rpt.WingwalInletSeverity == -1 ? "/" : rpt.WingwalInletSeverity.ToString()) : null;
                        //    worksheet.Cell(33, 7 + j).Value = rpt.ApronInletDistress != null ? (rpt.ApronInletDistress.Replace("-1", "/")) : null;
                        //    worksheet.Cell(34, 7 + j).Value = rpt.ApronInletSeverity != null ? (rpt.ApronInletSeverity == -1 ? "/" : rpt.ApronInletSeverity.ToString()) : null;
                        //    worksheet.Cell(35, 7 + j).Value = rpt.RiprapInletDistress != null ? (rpt.RiprapInletDistress == "-1" ? "/" : rpt.RiprapInletDistress) : null;
                        //    worksheet.Cell(36, 7 + j).Value = rpt.RiprapInletSeverity != null ? (rpt.RiprapInletSeverity == -1 ? "/" : rpt.RiprapInletSeverity.ToString()) : null;
                        //    worksheet.Cell(37, 7 + j).Value = rpt.HeadwallOutletDistress != null ? (rpt.HeadwallOutletDistress.Replace("-1", "/")) : null;
                        //    worksheet.Cell(38, 7 + j).Value = rpt.HeadwallOutletSeverity != null ? (rpt.HeadwallOutletSeverity == -1 ? "/" : rpt.HeadwallOutletSeverity.ToString()) : null;
                        //    worksheet.Cell(39, 7 + j).Value = rpt.WingwallOutletDistress != null ? (rpt.WingwallOutletDistress.Replace("-1", "/")) : null;
                        //    worksheet.Cell(40, 7 + j).Value = rpt.WingwallOutletSeverity != null ? (rpt.WingwallOutletSeverity == -1 ? "/" : rpt.WingwallOutletSeverity.ToString()) : null;
                        //    worksheet.Cell(41, 7 + j).Value = rpt.ApronOutletDistress != null ? (rpt.ApronOutletDistress.Replace("-1", "/")) : null;
                        //    worksheet.Cell(42, 7 + j).Value = rpt.ApronOutletSeverity != null ? (rpt.ApronOutletSeverity == -1 ? "/" : rpt.ApronOutletSeverity.ToString()) : null;
                        //    worksheet.Cell(43, 7 + j).Value = rpt.RiprapOutletDistress != null ? (rpt.RiprapOutletDistress.Replace("-1", "/")) : null;
                        //    worksheet.Cell(44, 7 + j).Value = rpt.RiprapOutletSeverity != null ? (rpt.RiprapOutletSeverity == -1 ? "/" : rpt.RiprapOutletSeverity.ToString()) : null;

                        //    worksheet.Cell(45, 7 + j).Value = rpt.Barrel_1_Distress != null ? rpt.Barrel_1_Distress.Replace("-1", "/") : null;
                        //    worksheet.Cell(46, 7 + j).Value = rpt.Barrel_1_Severity != null ? (rpt.Barrel_1_Severity == -1 ? "/" : rpt.Barrel_1_Severity.ToString()) : null;
                        //    worksheet.Cell(47, 7 + j).Value = rpt.Barrel_2_Distress != null ? (rpt.Barrel_2_Distress.Replace("-1", "/")) : null;
                        //    worksheet.Cell(48, 7 + j).Value = rpt.Barrel_2_Severity != null ? (rpt.Barrel_2_Severity == -1 ? "/" : rpt.Barrel_2_Severity.ToString()) : null;
                        //    worksheet.Cell(49, 7 + j).Value = rpt.Barrel_3_Distress != null ? (rpt.Barrel_3_Distress.Replace("-1", "/")) : null;
                        //    worksheet.Cell(50, 7 + j).Value = rpt.Barrel_3_Severity != null ? (rpt.Barrel_3_Severity == -1 ? "/" : rpt.Barrel_3_Severity.ToString()) : null;
                        //    worksheet.Cell(51, 7 + j).Value = rpt.Barrel_4_Distress != null ? (rpt.Barrel_4_Distress.Replace("-1", "/")) : null;
                        //    worksheet.Cell(52, 7 + j).Value = rpt.Barrel_4_Severity != null ? (rpt.Barrel_4_Severity == -1 ? "/" : rpt.Barrel_4_Severity.ToString()) : null;

                        //    int furthercellincrement = rpt.BarrelList.Count * 2;

                        //    if (rpt.BarrelList.Count > 0)
                        //    {
                        //        worksheet.Row(52).InsertRowsBelow(rpt.BarrelList.Count * 2);
                        //        worksheet.Range(worksheet.Cell(46, 1), worksheet.Cell(52 + furthercellincrement, 2)).Merge();
                        //        int d = 1;
                        //        for (int i = 0; i < rpt.BarrelList.Count; i++)
                        //        {
                        //            worksheet.Range(worksheet.Cell(52 + (d), 3), worksheet.Cell(52 + (d), 4)).Merge();
                        //            worksheet.Range(worksheet.Cell(52 + (d), 8), worksheet.Cell(52 + (d), 9)).Merge();
                        //            worksheet.Range(worksheet.Cell(52 + (d + 1), 8), worksheet.Cell(52 + (d + 1), 9)).Merge();
                        //            worksheet.Cell(52 + (d), 3).Value = rpt.BarrelList[i].Description;
                        //            worksheet.Cell(52 + (d), 5).Style.Fill.SetBackgroundColor(XLColor.Gray);
                        //            worksheet.Cell(52 + (d), 5).Style.Font.FontSize = 10;
                        //            worksheet.Cell(52 + (d), 3).Style.Font.Bold = true;
                        //            worksheet.Cell(52 + (d), 3).Style.Font.Italic = false;
                        //            worksheet.Cell(52 + (d), 5).Style.Font.Bold = true;
                        //            worksheet.Cell(52 + (d), 5).Style.Font.Italic = false;
                        //            worksheet.Cell(52 + (d), 5).Style.Font.FontColor = XLColor.White;
                        //            worksheet.Cell(52 + (d), 5).Value = rpt.BarrelList[i].Code;
                        //            worksheet.Cell(52 + (d), 6).Value = "DISTRESS";
                        //            worksheet.Cell(52 + (d + 1), 6).Value = "SEVERITY";
                        //            worksheet.Cell(52 + (d), 6).Style.Font.Bold = true;
                        //            worksheet.Cell(52 + (d), 6).Style.Font.Italic = false;
                        //            worksheet.Cell(52 + (d + 1), 6).Style.Font.Bold = true;
                        //            worksheet.Cell(52 + (d + 1), 6).Style.Font.Italic = false;
                        //            worksheet.Range(worksheet.Cell(52 + (d + 1), 3), worksheet.Cell(52 + (d + 1), 5)).Merge();
                        //            worksheet.Cell(52 + (d + 1), 3).Value = "* for multi cells culvert";
                        //            worksheet.Cell(52 + (d), 7 + j).Value = rpt.BarrelList[i].Distress != null ? (rpt.BarrelList[i].Distress.Replace("-1", "/")) : null;
                        //            worksheet.Cell(52 + (d + 1), 7 + j).Value = rpt.BarrelList[i].Severity != null ? (rpt.BarrelList[i].Severity == -1 ? "/" : rpt.BarrelList[i].Severity.ToString()) : null;
                        //            d += 2;
                        //        }
                        //    }




                        //    worksheet.Cell(53 + furthercellincrement, 7 + j).Value = rpt.CulvertApproachDistress != null ? (rpt.CulvertApproachDistress.ToString() == "-1" ? "/" : rpt.CulvertApproachDistress) : null;
                        //    worksheet.Cell(54 + furthercellincrement, 7 + j).Value = rpt.CulvertApproachSeverity != null ? (rpt.CulvertApproachSeverity.ToString() == "-1" ? "/" : rpt.CulvertApproachSeverity.ToString()) : null;


                        //    worksheet.Cell(74 + furthercellincrement, 3).Value = rpt.ReportforYear;
                        //    worksheet.Cell(75 + furthercellincrement, 3).Value = rpt.AssetRefNO;
                        //    worksheet.Cell(76 + furthercellincrement, 3).Value = rpt.RoadCode;
                        //    worksheet.Cell(77 + furthercellincrement, 3).Value = rpt.RoadName;

                        //    worksheet.Cell(74 + furthercellincrement, 13).Value = rpt.RefernceNo;
                        //    // worksheet.Cell(75 + furthercellincrement, 13).Value = rpt.RatingRecordNo;
                        //    worksheet.Cell(76 + furthercellincrement, 13).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";
                        //    worksheet.Cell(87 + furthercellincrement, 1).Value = rpt.PartB2ServiceProvider;
                        //    worksheet.Cell(87 + furthercellincrement, 9).Value = rpt.PartB2ServicePrvdrCons;
                        //    worksheet.Cell(100 + furthercellincrement, 1).Value = rpt.PartCGeneralComments;
                        //    worksheet.Cell(100 + furthercellincrement, 9).Value = rpt.PartCGeneralCommentsCons;
                        //    worksheet.Cell(113 + furthercellincrement, 1).Value = rpt.PartDFeedback;
                        //    worksheet.Cell(113 + furthercellincrement, 9).Value = rpt.PartDFeedbackCons;


                        //    worksheet.Cell(129 + furthercellincrement, 2).Value = rpt.InspectedByName;
                        //    worksheet.Cell(130 + furthercellincrement, 2).Value = rpt.InspectedByDesignation;
                        //    worksheet.Cell(131 + furthercellincrement, 2).Value = rpt.InspectedByDate;

                        //    worksheet.Cell(129 + furthercellincrement, 12).Value = rpt.AuditedByName;
                        //    worksheet.Cell(130 + furthercellincrement, 12).Value = rpt.AuditedByDesignation;
                        //    worksheet.Cell(131 + furthercellincrement, 12).Value = rpt.AuditedByDate;

                        //    worksheet.Cell(132 + furthercellincrement, 16).Value = rpt.CulverConditionRate;
                        //    worksheet.Cell(133 + furthercellincrement, 16).Value = rpt.HaveIssueFound;
                        //}

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
