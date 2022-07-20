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




                        //Picture
                        int nextoadd = 0;
                        int sheetNo = 3;
                        bool IsFirst = true;
                        int index = 0;
                        foreach (var irpt in _rpt)
                        {
                            Pictures[] pictures;
                            pictures = irpt.Pictures.Skip(index * 6).Take(6).ToArray();
                            index++;
                            int noofsheets = (irpt.Pictures.Count() / 6) + ((irpt.Pictures.Count() % 6) > 0 ? 1 : 1);
                            using (var book = new XLWorkbook(cachefile))
                            {
                                IXLWorksheet image = nextoadd == 0 && IsFirst ? workbook.Worksheet(2) : book.Worksheet(2);
                                image.Cell(4, 6).Value = irpt.ReportforYear;
                                image.Cell(5, 6).Value = irpt.AssetRefNO;
                                image.Cell(6, 6).Value = irpt.RoadCode;
                                image.Cell(7, 6).Value = irpt.RoadName;
                                image.Cell(4, 17).Value = irpt.RefernceNo;
                                image.Cell(5, 17).Value = index;
                                image.Cell(6, 17).Value = $"{irpt.LocationChainageKm}+{irpt.LocationChainageM}";
                                pictures = irpt.Pictures.Take(6).ToArray();
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
                                    copysheet.Cell(4, 6).Value = irpt.ReportforYear;
                                    copysheet.Cell(5, 6).Value = irpt.AssetRefNO;
                                    copysheet.Cell(6, 6).Value = irpt.RoadCode;
                                    copysheet.Cell(7, 6).Value = irpt.RoadName;
                                    copysheet.Cell(4, 17).Value = irpt.RefernceNo;
                                    copysheet.Cell(5, 17).Value = index;
                                    copysheet.Cell(6, 17).Value = $"{irpt.LocationChainageKm}+{irpt.LocationChainageM}";
                                    pictures = irpt.Pictures.Skip((tobeskipped - 1) * 6).Take(6).ToArray();
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
