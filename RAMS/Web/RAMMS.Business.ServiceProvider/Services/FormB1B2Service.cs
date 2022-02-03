using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Common;
using RAMMS.Common.RefNumber;
using RAMMS.Domain.Models;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;

namespace RAMMS.Business.ServiceProvider.Services
{

    public class FormB1B2Service : IFormB1B2Service
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper; private readonly ISecurity _security;
        public FormB1B2Service(IRepositoryUnit repoUnit,
            IMapper mapper, ISecurity security)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); _security = security ?? throw new ArgumentNullException(nameof(security));
        }
        public long LastHeaderInsertedNo()
        {
            var model = _repoUnit.FormB1B2HeaderRepository.GetAll().OrderByDescending(s => s.FbrihPkRefNo).FirstOrDefault();
            if (model != null) { return model.FbrihPkRefNo; } else { return 0; }
        }
        public async Task<FormB1B2HeaderRequestDTO> GetHeaderById(int id)
        {
            var model = await _repoUnit.FormB1B2HeaderRepository.FindAsync(s => s.FbrihPkRefNo == id);
            if (model == null) { return null; }
            var detail = await _repoUnit.FormB1B2DetailRepository.FindAsync(s => s.FbridFbrihPkRefNo == id);

            var result = _mapper.Map<Domain.Models.RmFormB1b2BrInsHdr, FormB1B2HeaderRequestDTO>(model);
            var SectionCode = await _repoUnit.RoadmasterRepository.FindAllAsync(s => s.RdmRdCode == result.AiRdCode && s.RdmActiveYn == true);
            result.SectionCode = SectionCode.FirstOrDefault().RdmSecCode.ToString();
            if (detail != null)
            {
                result.Detail = _mapper.Map<Domain.Models.RmFormB1b2BrInsDtl, FormB1B2DetailRequestDTO>(detail);
            }
            return result;
        }
        public async Task<int> SaveHeader(FormB1B2HeaderRequestDTO model)
        {
            try
            {
                bool isAdd = false;
                var form = _mapper.Map<Domain.Models.RmFormB1b2BrInsHdr>(model);
                form.FbrihActiveYn = true;
                if (form.FbrihPkRefNo != 0)
                {
                    if (form.FbrihSubmitSts)
                    {
                        DDLookUpDTO ddlookup = new DDLookUpDTO();
                        ddlookup.TypeCode = "BR";
                        ddlookup.Type = "Photo Type";
                        var lookup = (await _repoUnit.DDLookUpRepository.GetDdLookUp(ddlookup)).Select(s => s.DdlTypeDesc).ToArray();
                        bool issuccess = _repoUnit.FormB1B2ImgRepository.CheckImageExistforAllType(lookup, form.FbrihPkRefNo);
                        //if (!issuccess)
                        //    return -1;

                    }
                    _repoUnit.FormB1B2HeaderRepository.Update(form);
                }
                else
                {
                    isAdd = true;
                    _repoUnit.FormB1B2HeaderRepository.Create(form);
                }
                await _repoUnit.CommitAsync();
                if (form.FbrihPkRefNo > 0)
                {
                    var detail = _mapper.Map<Domain.Models.RmFormB1b2BrInsDtl>(model.Detail);
                    detail.FbridActiveYn = true;
                    detail.FbridFbrihPkRefNo = form.FbrihPkRefNo;
                    if (detail.FbridPkRefNo == 0)
                    {
                        _repoUnit.FormB1B2DetailRepository.Create(detail);
                    }
                    else
                    {
                        _repoUnit.FormB1B2DetailRepository.Update(detail);
                    }
                }
                if (isAdd)
                {
                    IDictionary<string, string> lstData = new Dictionary<string, string>();
                    lstData.Add("AssetID", model.DisplayAssetId);
                    lstData.Add("Year", form.FbrihYearOfInsp.ToString());
                    //lstData.Add("RatingRecord", form.FbrihRecordNo.ToString());
                    lstData.Add(FormRefNumber.NewRunningNumber, Utility.ToString(form.FbrihPkRefNo));
                    form.FbrihCInspRefNo = FormRefNumber.GetRefNumber(FormType.FormB1B2, lstData);
                }
                await _repoUnit.CommitAsync();
                return form.FbrihPkRefNo;
            }
            catch (Exception ex) { await _repoUnit.RollbackAsync(); throw ex; }
        }

        public async Task<int> UpdateB1B2(FormB1B2HeaderRequestDTO formB1B2)   //Tab
        {
            int rowsAffected = 0;
            var data = _mapper.Map<RmFormB1b2BrInsHdr>(formB1B2);
            _repoUnit.FormB1B2HeaderRepository.Update(data);
            rowsAffected = await _repoUnit.CommitAsync();

            if (formB1B2.Detail.PkRefNo != 0)
            {
                var detail = _mapper.Map<Domain.Models.RmFormB1b2BrInsDtl>(formB1B2.Detail);
                _repoUnit.FormB1B2DetailRepository.Update(detail);
                await _repoUnit.CommitAsync();
            }
            return rowsAffected;
        }
        public async Task<bool> RemoveHeader(int id)
        {
            var model = _repoUnit.FormB1B2HeaderRepository.Find(s => s.FbrihPkRefNo == id);
            var rowAffected = false;
            if (model != null)
            {
                model.FbrihActiveYn = false;
                rowAffected = await _repoUnit.CommitAsync() != 0;
                if (rowAffected)
                {
                    var dtl = _repoUnit.FormB1B2DetailRepository.Find(d => d.FbridFbrihPkRefNo == model.FbrihPkRefNo);
                    if (dtl != null)
                    {
                        dtl.FbridActiveYn = false;
                        rowAffected = await _repoUnit.CommitAsync() != 0;
                    }
                }
            }
            return rowAffected;
        }
        public async Task<PagingResult<FormB1B2HeaderRequestDTO>> GetHeaderList(FilteredPagingDefinition<FormB1B2SearchGridDTO> filterOptions)
        {
            PagingResult<FormB1B2HeaderRequestDTO> result = new PagingResult<FormB1B2HeaderRequestDTO>();
            result.PageResult = await _repoUnit.FormB1B2HeaderRepository.GetFilteredRecordList(filterOptions);
            result.TotalRecords = await _repoUnit.FormB1B2HeaderRepository.GetFilteredRecordCount(filterOptions);
            result.PageNo = filterOptions.StartPageNo;
            result.FilteredRecords = result.PageResult != null ? result.PageResult.Count : 0;
            return result;
        }

        public long LastDetailInsertedNo()
        {
            var model = _repoUnit.FormB1B2DetailRepository.GetAll().OrderByDescending(s => s.FbridPkRefNo).FirstOrDefault();
            if (model != null)
            {
                return model.FbridPkRefNo;
            }
            else
            {
                return 0;
            }
        }
        public async Task<FormB1B2DetailRequestDTO> GetDetailById(int id)
        {
            var model = await _repoUnit.FormB1B2DetailRepository.FindAsync(s => s.FbridPkRefNo == id);
            if (model == null) { return null; }
            return _mapper.Map<Domain.Models.RmFormB1b2BrInsDtl, FormB1B2DetailRequestDTO>(model);
        }
        public async Task<int> SaveDetail(FormB1B2DetailRequestDTO model)
        {
            try
            {
                var form = _mapper.Map<Domain.Models.RmFormB1b2BrInsDtl>(model);
                if (form.FbridPkRefNo != 0) { _repoUnit.FormB1B2DetailRepository.Update(form); }
                else { _repoUnit.FormB1B2DetailRepository.Create(form); }
                await _repoUnit.CommitAsync(); return form.FbridPkRefNo;
            }
            catch (Exception ex) { await _repoUnit.RollbackAsync(); throw ex; }

        }
        public async Task<bool> RemoveDetail(int id)
        {
            var model = _repoUnit.FormB1B2DetailRepository.Find(s => s.FbridPkRefNo == id);
            if (model != null) { return await _repoUnit.CommitAsync() != 0; } else { return false; }
        }
        public async Task<PagingResult<FormB1B2DetailRequestDTO>> GetDetailList(FilteredPagingDefinition<FormB1B2DetailRequestDTO> filterOptions)
        {
            PagingResult<FormB1B2DetailRequestDTO> result = new PagingResult<FormB1B2DetailRequestDTO>();
            result.PageResult = await _repoUnit.FormB1B2DetailRepository.GetFilteredRecordList(filterOptions); result.TotalRecords = await _repoUnit.FormB1B2DetailRepository.GetFilteredRecordCount(filterOptions);
            return result;
        }

        public IEnumerable<SelectListItem> GetBridgeIds(AssetDDLRequestDTO request)
        {
            return _repoUnit.FormB1B2HeaderRepository.GetBridgeIds(request);
        }

        public async Task<FormB1B2HeaderRequestDTO> GetBrideDetailById(long id)
        {
            return await _repoUnit.FormB1B2HeaderRepository.GetBrideDetailById(id);
        }

        public async Task<(bool IsExist, int PkRefNo)> AlreadyExists(int assetid, int year)
        {
            var result = await _repoUnit.FormB1B2HeaderRepository.FindAsync(s => s.FbrihAiPkRefNo == assetid && s.FbrihActiveYn == true && s.FbrihYearOfInsp.Value == year);
            if (result != null)
            {

                return (true, result.FbrihPkRefNo);
            }
            else
            {
                return (false, 0);
            }
        }

        public async Task<int> SaveImageDtl(List<FormB1B2ImgRequestDTO> imagelist)
        {
            int rowsAffected;
            try
            {
                var imagelistdtl = new List<RmFormB1b2BrInsImage>();

                foreach (var list in imagelist)
                {
                    var lst = _repoUnit.FormB1B2ImgRepository.FindAll(s => s.FbriImageTypeCode == list.ImageTypeCode && s.FbriFbrihPkRefNo == list.FbrihPkRefNo).ToList();
                    if (lst != null && lst.Count > 2)
                    {
                        return -1;
                    }
                    imagelistdtl.Add(_mapper.Map<RmFormB1b2BrInsImage>(list));
                }


                _repoUnit.FormB1B2ImgRepository.Create(imagelistdtl);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<List<FormB1B2ImgRequestDTO>> GetAllImageByAssetPK(int assetPK)
        {
            List<FormB1B2ImgRequestDTO> imagelist = new List<FormB1B2ImgRequestDTO>();
            var images = await _repoUnit.FormB1B2ImgRepository.GetAllImageByHeaderId(assetPK).ConfigureAwait(false);
            foreach (var image in images)
            {
                imagelist.Add(_mapper.Map<FormB1B2ImgRequestDTO>(image));
            }
            return imagelist;
        }

        public async Task<int> DectivateAssetImage(int assetimgId)
        {
            return await _repoUnit.FormB1B2ImgRepository.DectivateAssetImage(assetimgId);
        }

        public async Task<int> ImageLastInsertedSRNO(int hederid, string type)
        {
            return await _repoUnit.FormB1B2ImgRepository.LastInsertedSRNO(hederid, type);
        }

        public List<FormB1B2Rpt> GetReportData(int headerid)
        {
            return _repoUnit.FormB1B2HeaderRepository.GetReportData(headerid);
        }

        public byte[] FormDownload(string formname, int id, string basepath, string filepath)
        {
            string superStructure = _repoUnit.DDLookUpRepository.GetConcatenateDdlTypeValue(new DDLookUpDTO { Type = "Structure Code", TypeCode = "BR" });
            string parapetType = _repoUnit.DDLookUpRepository.GetConcatenateDdlTypeDesc(new DDLookUpDTO { Type = "Parapet Type", TypeCode = "BR" });
            string bearingType = _repoUnit.DDLookUpRepository.GetConcatenateDdlTypeDesc(new DDLookUpDTO { Type = "Bearing Type", TypeCode = "BR" });
            string expansionType = _repoUnit.DDLookUpRepository.GetConcatenateDdlTypeDesc(new DDLookUpDTO { Type = "Expansion Type", TypeCode = "BR" });
            string deckType = _repoUnit.DDLookUpRepository.GetConcatenateDdlTypeDesc(new DDLookUpDTO { Type = "Deck Type", TypeCode = "BR" });
            string abutmentType = _repoUnit.DDLookUpRepository.GetConcatenateDdlTypeDesc(new DDLookUpDTO { Type = "Abutment Type", TypeCode = "BR" });
            string pierType = _repoUnit.DDLookUpRepository.GetConcatenateDdlTypeDesc(new DDLookUpDTO { Type = "Pier Type", TypeCode = "BR" });
            string Oldfilename = "";
            string filename = "";
            string cachefile = "";
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
                List<FormB1B2Rpt> _rpt = this.GetReportData(id);
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
                            image.Cell(4, 6).Value = rpt.Year;
                            image.Cell(5, 6).Value = rpt.RoadCode;
                            image.Cell(6, 6).Value = rpt.StructureCode;
                            image.Cell(7, 6).Value = rpt.RoadName;
                            image.Cell(4, 17).Value = rpt.ReferenceNo;
                            image.Cell(5, 17).Value = rpt.RiverName;
                            image.Cell(6, 17).Value = index;

                            for (int i = 0; i < pictures.Count(); i++)
                            {
                                if (File.Exists($"{basepath}/{pictures[i].ImageUrl}"))
                                {
                                    byte[] buff = File.ReadAllBytes($"{basepath}/{pictures[i].ImageUrl}");
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
                                            image.Cell(39, 15).Value = rpt.Pictures[i].Type;
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
                                copysheet.Cell(4, 6).Value = rpt.Year;
                                copysheet.Cell(5, 6).Value = rpt.RoadCode;
                                copysheet.Cell(6, 6).Value = rpt.StructureCode;
                                copysheet.Cell(7, 6).Value = rpt.RoadName;
                                copysheet.Cell(4, 17).Value = rpt.ReferenceNo;
                                copysheet.Cell(5, 17).Value = rpt.RiverName;
                                copysheet.Cell(6, 17).Value = index;
                                pictures = rpt.Pictures.Skip((tobeskipped - 1) * 6).Take(6).ToArray();
                                for (int i = 0; i < pictures.Count(); i++)
                                {
                                    if (File.Exists($"{basepath}/{pictures[i].ImageUrl}"))
                                    {
                                        byte[] buff = File.ReadAllBytes($"{basepath}/{pictures[i].ImageUrl}");
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
                            }
                            if (nextoadd == 0)
                            {
                                nextoadd++;
                            }
                        }
                    }


                    if (worksheet != null)
                    {
                        var rpt = _rpt[0];
                        worksheet.Cell(82, 6).Value = rpt.Year;
                        worksheet.Cell(83, 6).Value = rpt.RoadCode;
                        worksheet.Cell(84, 6).Value = rpt.StructureCode;
                        worksheet.Cell(85, 6).Value = rpt.RoadName;
                        worksheet.Cell(82, 17).Value = rpt.ReferenceNo;
                        worksheet.Cell(83, 17).Value = rpt.RiverName;
                        //worksheet.Cell(80, 17).Value = rpt.BridgeConditionRating;
                        //worksheet.Cell(84, 17).Value = rpt.RatingRecordNo;
                        worksheet.Cell(94, 2).Value = rpt.PartB_Serviceprovider;
                        worksheet.Cell(94, 15).Value = rpt.PartB_Consultant;
                        worksheet.Cell(111, 2).Value = rpt.PartC_Serviceprovider;
                        worksheet.Cell(111, 15).Value = rpt.PartC_Consultant;
                        worksheet.Cell(129, 2).Value = rpt.PartD_Feedback;
                        worksheet.Cell(129, 15).Value = rpt.PartD_Consultant;
                        worksheet.Cell(149, 5).Value = rpt.InspectedByName;
                        worksheet.Cell(150, 5).Value = rpt.InspectedByDesignation;
                        worksheet.Cell(151, 5).Value = rpt.InspectedByDate;
                        worksheet.Cell(149, 17).Value = rpt.AuditedByName;
                        worksheet.Cell(150, 17).Value = rpt.AuditedByDesignation;
                        worksheet.Cell(151, 17).Value = rpt.AuditedByDate;
                        worksheet.Cell(152, 22).Value = rpt.BridgeConditionRating;
                        worksheet.Cell(153, 22).Value = rpt.RequireFurtherInvestigation;
                        worksheet.Cell(5, 6).Value = $"{rpt.ChainageKm}+{rpt.ChainageM}";
                        worksheet.Cell(6, 6).Value = rpt.StructureCode;
                        worksheet.Cell(7, 6).Value = rpt.GPSEasting;
                        worksheet.Cell(4, 15).Value = rpt.RoadCode;
                        worksheet.Cell(5, 15).Value = rpt.RoadName;
                        worksheet.Cell(6, 15).Value = rpt.RiverName;
                        worksheet.Cell(7, 15).Value = rpt.GPSNorthing;
                        worksheet.Cell(4, 21).Value = rpt.ReferenceNo;
                        worksheet.Cell(6, 22).Value = rpt.Division;
                        worksheet.Cell(7, 22).Value = rpt.Rmu;

                        if (!string.IsNullOrEmpty(superStructure))
                        {
                            worksheet.Cell(9, 5).Value = superStructure;
                            worksheet.Cell(9, 5).RichText.Substring(0, superStructure.Length).Strikethrough = true;
                        }

                        if (!string.IsNullOrEmpty(superStructure) && !string.IsNullOrEmpty(rpt.Superstructure))
                        {
                            if (superStructure.IndexOf(rpt.Superstructure) > -1)
                            {
                                worksheet.Cell(9, 5).RichText.Substring(superStructure.IndexOf(" " + rpt.Superstructure + " "), (" " + rpt.Superstructure + " ").Length).Bold = true;
                                worksheet.Cell(9, 5).RichText.Substring(superStructure.IndexOf(" " + rpt.Superstructure + " "), (" " + rpt.Superstructure + " ").Length).Strikethrough = false;
                            }
                        }
                        if (!string.IsNullOrEmpty(parapetType))
                        {
                            worksheet.Cell(10, 5).Value = parapetType;
                            worksheet.Cell(10, 5).RichText.Substring(0, parapetType.Length).Strikethrough = true;
                        }
                        if (!string.IsNullOrEmpty(parapetType) && !string.IsNullOrEmpty(rpt.ParapetType))
                        {
                            if (parapetType.IndexOf(rpt.ParapetType) > -1)
                            {
                                worksheet.Cell(10, 5).RichText.Substring(parapetType.IndexOf(" " + rpt.ParapetType + " "), (" " + rpt.ParapetType + " ").Length).Bold = true;
                                worksheet.Cell(10, 5).RichText.Substring(parapetType.IndexOf(" " + rpt.ParapetType + " "), (" " + rpt.ParapetType + " ").Length).Strikethrough = false;
                            }
                        }
                        if (!string.IsNullOrEmpty(bearingType))
                        {

                            worksheet.Cell(11, 5).Value = bearingType;
                            worksheet.Cell(11, 5).RichText.Substring(0, bearingType.Length).Strikethrough = true;

                        }
                        if (!string.IsNullOrEmpty(bearingType) && !string.IsNullOrEmpty(rpt.BearingType))
                        {
                            if (bearingType.IndexOf(rpt.BearingType) > -1)
                            {
                                worksheet.Cell(11, 5).RichText.Substring(bearingType.IndexOf(" " + rpt.BearingType + " "), (" " + rpt.BearingType + " ").Length).Bold = true;
                                worksheet.Cell(11, 5).RichText.Substring(bearingType.IndexOf(" " + rpt.BearingType + " "), (" " + rpt.BearingType + " ").Length).Strikethrough = false;
                            }
                        }
                        if (!string.IsNullOrEmpty(expansionType))
                        {

                            worksheet.Cell(12, 5).Value = expansionType;
                            worksheet.Cell(12, 5).RichText.Substring(0, expansionType.Length).Strikethrough = true;

                        }
                        if (!string.IsNullOrEmpty(expansionType) && !string.IsNullOrEmpty(rpt.ExpansionType))
                        {
                            if (expansionType.IndexOf(rpt.ExpansionType) > -1)
                            {
                                worksheet.Cell(12, 5).RichText.Substring(expansionType.IndexOf(" " + rpt.ExpansionType + " "), (" " + rpt.ExpansionType + " ").Length).Bold = true;
                                worksheet.Cell(12, 5).RichText.Substring(expansionType.IndexOf(" " + rpt.ExpansionType + " "), (" " + rpt.ExpansionType + " ").Length).Strikethrough = false;
                            }
                        }
                        if (!string.IsNullOrEmpty(deckType))
                        {
                            worksheet.Cell(9, 17).Value = deckType;
                            worksheet.Cell(9, 17).RichText.Substring(0, deckType.Length).Strikethrough = true;
                        }
                        if (!string.IsNullOrEmpty(deckType) && !string.IsNullOrEmpty(rpt.DeckType))
                        {

                            if (deckType.IndexOf(rpt.DeckType) > -1)
                            {
                                worksheet.Cell(9, 17).RichText.Substring(deckType.IndexOf(" " + rpt.DeckType + " "), (" " + rpt.DeckType + " ").Length).Bold = true;
                                worksheet.Cell(9, 17).RichText.Substring(deckType.IndexOf(" " + rpt.DeckType + " "), (" " + rpt.DeckType + " ").Length).Strikethrough = false;
                            }
                        }
                        if (!string.IsNullOrEmpty(abutmentType))
                        {
                            worksheet.Cell(10, 17).Value = abutmentType;
                            worksheet.Cell(10, 17).RichText.Substring(0, abutmentType.Length).Strikethrough = true;

                        }
                        if (!string.IsNullOrEmpty(abutmentType) && !string.IsNullOrEmpty(rpt.AbutmentType))
                        {

                            if (abutmentType.IndexOf(rpt.AbutmentType) > -1)
                            {
                                worksheet.Cell(10, 17).RichText.Substring(abutmentType.IndexOf(" " + rpt.AbutmentType + " "), (" " + rpt.AbutmentType + " ").Length).Bold = true;
                                worksheet.Cell(10, 17).RichText.Substring(abutmentType.IndexOf(" " + rpt.AbutmentType + " "), (" " + rpt.AbutmentType + " ").Length).Strikethrough = false;
                            }
                        }
                        if (!string.IsNullOrEmpty(pierType))
                        {
                            worksheet.Cell(11, 17).Value = pierType;
                            worksheet.Cell(11, 17).RichText.Substring(0, pierType.Length).Strikethrough = true;
                        }
                        if (!string.IsNullOrEmpty(pierType) && !string.IsNullOrEmpty(rpt.PierType))
                        {
                            if (pierType.IndexOf(rpt.PierType) > -1)
                            {
                                worksheet.Cell(11, 17).RichText.Substring(pierType.IndexOf(" " + rpt.PierType + " "), (" " + rpt.PierType + " ").Length).Bold = true;
                                worksheet.Cell(11, 17).RichText.Substring(pierType.IndexOf(" " + rpt.PierType + " "), (" " + rpt.PierType + " ").Length).Strikethrough = false;
                            }
                        }
                        worksheet.Cell(14, 5).Value = rpt.LaneWidth;
                        worksheet.Cell(15, 5).Value = rpt.SpanLength;
                        worksheet.Cell(16, 5).Value = rpt.BridgeLength;
                        worksheet.Cell(17, 5).Value = rpt.BridgeWidth;
                        worksheet.Cell(14, 11).Value = rpt.NoOfLane;
                        worksheet.Cell(15, 11).Value = rpt.NoOfSpan;
                        worksheet.Cell(16, 10).Value = rpt.Median;
                        worksheet.Cell(17, 10).Value = rpt.Walkway;
                        for (int i = 0; i < _rpt.Count; i++)
                        {
                            rpt = _rpt[i];

                            if (rpt.DateOfInspection.HasValue)
                            {
                                worksheet.Cell(14, 15 + i).Value = rpt.DateOfInspection.Value.Year;
                                worksheet.Cell(15, 15 + i).Value = rpt.DateOfInspection.Value.Month;
                                worksheet.Cell(16, 15 + i).Value = rpt.DateOfInspection.Value.Day;
                            }
                            else
                            {
                                worksheet.Cell(14, 15 + i).Value = "";
                                worksheet.Cell(15, 15 + i).Value = "";
                                worksheet.Cell(16, 15 + i).Value = "";
                            }



                            worksheet.Cell(12, 20).Value = "";
                            worksheet.Cell(12, 18).Value = rpt.NumberOfExpansion;


                            worksheet.Cell(19, 7).Value = _repoUnit.FormB1B2HeaderRepository.GetMaterialType("Abutment Walls, Foundation", rpt.AbutmentWall_Foundation_Material);
                            worksheet.Cell(19, 15 + i).Value = rpt.AbutmentWall_Foundation_Distress_Severity.Distress != null ? (rpt.AbutmentWall_Foundation_Distress_Severity.Distress.Replace("-1", "/")) : null;
                            worksheet.Cell(20, 15 + i).Value = rpt.AbutmentWall_Foundation_Distress_Severity.Severity != null ? (rpt.AbutmentWall_Foundation_Distress_Severity.Severity == -1 ? "/" : rpt.AbutmentWall_Foundation_Distress_Severity.Severity.ToString()) : null;

                            worksheet.Cell(21, 7).Value = _repoUnit.FormB1B2HeaderRepository.GetMaterialType("Piers, Connectiong of primary components", rpt.Piers_Connection_of_primary_components_Material);
                            worksheet.Cell(21, 15 + i).Value = rpt.Piers_Connection_of_primary_components_Distresss_Severity.Distress != null ? (rpt.Piers_Connection_of_primary_components_Distresss_Severity.Distress.Replace("-1", "/")) : null;
                            worksheet.Cell(22, 15 + i).Value = rpt.Piers_Connection_of_primary_components_Distresss_Severity.Severity != null ? (rpt.Piers_Connection_of_primary_components_Distresss_Severity.Severity == -1 ? "/" : rpt.Piers_Connection_of_primary_components_Distresss_Severity.Severity.ToString()) : null;

                            worksheet.Cell(23, 7).Value = _repoUnit.FormB1B2HeaderRepository.GetMaterialType("Bearing, Bearing Seats, Bearing Diaphgrams", rpt.Bearing_Material);
                            worksheet.Cell(23, 15 + i).Value = rpt.Bearing_Distress_Severity.Distress != null ? (rpt.Bearing_Distress_Severity.Distress.Replace("-1", "/")) : null;
                            worksheet.Cell(24, 15 + i).Value = rpt.Bearing_Distress_Severity.Severity != null ? (rpt.Bearing_Distress_Severity.Severity == -1 ? "/" : rpt.Bearing_Distress_Severity.Severity.ToString()) : null;

                            worksheet.Cell(25, 7).Value = _repoUnit.FormB1B2HeaderRepository.GetMaterialType("Beams, Girders, Trussess, Arches", rpt.Beam_Material);
                            worksheet.Cell(25, 15 + i).Value = rpt.Beam_Distress_Severity.Distress != null ? (rpt.Beam_Distress_Severity.Distress.Replace("-1", "/")) : null;
                            worksheet.Cell(26, 15 + i).Value = rpt.Beam_Distress_Severity.Severity != null ? (rpt.Beam_Distress_Severity.Severity == -1 ? "/" : rpt.Beam_Distress_Severity.Severity.ToString()) : null;

                            worksheet.Cell(27, 7).Value = _repoUnit.FormB1B2HeaderRepository.GetMaterialType("Deck Slab, Pavement", rpt.Deck_Material);
                            worksheet.Cell(27, 15 + i).Value = rpt.Deck_Distress_Severity.Distress != null ? (rpt.Deck_Distress_Severity.Distress.Replace("-1", "/")) : null;
                            worksheet.Cell(28, 15 + i).Value = rpt.Deck_Distress_Severity.Severity != null ? (rpt.Deck_Distress_Severity.Severity == -1 ? "/" : rpt.Deck_Distress_Severity.Severity.ToString()) : null;

                            worksheet.Cell(30, 7).Value = _repoUnit.FormB1B2HeaderRepository.GetMaterialType("Signboard, Utilities", rpt.Signboard_Material);
                            worksheet.Cell(30, 15 + i).Value = rpt.Signboard_Distress_Severity.Distress != null ? (rpt.Signboard_Distress_Severity.Distress.Replace("-1", "/")) : null;
                            worksheet.Cell(31, 15 + i).Value = rpt.Signboard_Distress_Severity.Severity != null ? (rpt.Signboard_Distress_Severity.Severity == -1 ? "/" : rpt.Signboard_Distress_Severity.Severity.ToString()) : null;

                            worksheet.Cell(32, 7).Value = _repoUnit.FormB1B2HeaderRepository.GetMaterialType("Waterway", rpt.Waterway_Material);
                            worksheet.Cell(32, 15 + i).Value = rpt.Waterway_Distress_Severity.Distress != null ? (rpt.Waterway_Distress_Severity.Distress.Replace("-1", "/")) : null;
                            worksheet.Cell(33, 15 + i).Value = rpt.Waterway_Distress_Severity.Severity != null ? (rpt.Waterway_Distress_Severity.Severity == -1 ? "/" : rpt.Waterway_Distress_Severity.Severity.ToString()) : null;

                            worksheet.Cell(34, 7).Value = _repoUnit.FormB1B2HeaderRepository.GetMaterialType("Drain Water Down Pipe, Drainage", rpt.Drainwater_Material);
                            worksheet.Cell(34, 15 + i).Value = rpt.Drainwater_Distress_Severity.Distress != null ? (rpt.Drainwater_Distress_Severity.Distress.Replace("-1", "/")) : null;
                            worksheet.Cell(35, 15 + i).Value = rpt.Drainwater_Distress_Severity.Severity != null ? (rpt.Drainwater_Distress_Severity.Severity == -1 ? "/" : rpt.Drainwater_Distress_Severity.Severity.ToString()) : null;

                            worksheet.Cell(36, 7).Value = _repoUnit.FormB1B2HeaderRepository.GetMaterialType("Parapet, Railing", rpt.Parapet_Material);
                            worksheet.Cell(36, 15 + i).Value = rpt.Parapet_Distress_Severity.Distress != null ? (rpt.Parapet_Distress_Severity.Distress.Replace("-1", "/")) : null;
                            worksheet.Cell(37, 15 + i).Value = rpt.Parapet_Distress_Severity.Severity != null ? (rpt.Parapet_Distress_Severity.Severity == -1 ? "/" : rpt.Parapet_Distress_Severity.Severity.ToString()) : null;

                            worksheet.Cell(38, 7).Value = _repoUnit.FormB1B2HeaderRepository.GetMaterialType("Kerb, Sidewalks, Approaches, Approch Slab", rpt.Kerb_Material);
                            worksheet.Cell(38, 15 + i).Value = rpt.Kerb_Distress_Severity.Distress != null ? (rpt.Kerb_Distress_Severity.Distress.Replace("-1", "/")) : null;
                            worksheet.Cell(39, 15 + i).Value = rpt.Kerb_Distress_Severity.Severity != null ? (rpt.Kerb_Distress_Severity.Severity == -1 ? "/" : rpt.Kerb_Distress_Severity.Severity.ToString()) : null;

                            worksheet.Cell(40, 7).Value = _repoUnit.FormB1B2HeaderRepository.GetMaterialType("Expansion Joint", rpt.Expansion_Material);
                            worksheet.Cell(40, 15 + i).Value = rpt.Expansion_Distress_Severity.Distress != null ? (rpt.Expansion_Distress_Severity.Distress.Replace("-1", "/")) : null;
                            worksheet.Cell(41, 15 + i).Value = rpt.Expansion_Distress_Severity.Severity != null ? (rpt.Expansion_Distress_Severity.Severity == -1 ? "/" : rpt.Expansion_Distress_Severity.Severity.ToString()) : null;

                            worksheet.Cell(42, 7).Value = _repoUnit.FormB1B2HeaderRepository.GetMaterialType("Slope Protections, Retaining Wall", rpt.Slope_Material);
                            worksheet.Cell(42, 15 + i).Value = rpt.Slope_Distress_Severity.Distress != null ? (rpt.Slope_Distress_Severity.Distress.Replace("-1", "/")) : null;
                            worksheet.Cell(43, 15 + i).Value = rpt.Slope_Distress_Severity.Severity != null ? (rpt.Slope_Distress_Severity.Severity == -1 ? "/" : rpt.Slope_Distress_Severity.Severity.ToString()) : null;


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

        public async Task<AssetDDLResponseDTO> GetAssetDDL(AssetDDLRequestDTO request)
        {
            try
            {
                AssetDDLResponseDTO roadlist = await _repoUnit.FormB1B2HeaderRepository.GetAssetDDL(request);

                return roadlist;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
