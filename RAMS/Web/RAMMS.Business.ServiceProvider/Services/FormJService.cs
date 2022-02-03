using AutoMapper;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.SearchBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormJService : IFormJServices
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormJService(IRepositoryUnit repoUnit, IMapper mapper, ISecurity security, IProcessService proService)        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security ?? throw new ArgumentNullException(nameof(security));
            processService = proService;
        }


        public async Task<FormJHeaderRequestDTO> GetFormAWithDetailsByNoAsync(int formNo)
        {
            FormJHeaderRequestDTO formA;
            try
            {
                var domainModelFormA = await _repoUnit.FormJRepository.GetFormWithDetailsByNoAsync(formNo).ConfigureAwait(false);
                if (string.IsNullOrEmpty(domainModelFormA.FjhSection) ||
                    string.IsNullOrEmpty(domainModelFormA.FjhRoadName) ||
                    string.IsNullOrEmpty(domainModelFormA.FjhRmu))
                {
                    var rd = await _repoUnit.RoadmasterRepository.GetAllRoadCodeData(new RoadMasterRequestDTO
                    {
                        RoadCode = domainModelFormA.FjhRoadCode
                    });
                    if (rd != null)
                    {
                        domainModelFormA.FjhSection = rd.RdmSecName;
                        domainModelFormA.FjhRmu = rd.RdmRmuCode;
                        domainModelFormA.FjhRoadName = rd.RdmRdName;
                    }
                }

                formA = _mapper.Map<FormJHeaderRequestDTO>(domainModelFormA);
                foreach (var formADetail in domainModelFormA.RmFormJDtl)
                {
                    var _ = _mapper.Map<FormJDetailsRequestDTO>(formADetail);
                    _.Dt = formADetail.FjdDt.HasValue ? formADetail.FjdDt.Value.ToString("yyyy-MM-dd") : "";
                    formA.FormJDetails.Add(_);
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return formA;
        }

        public async Task<int> SaveFormJAsync(FormJHeaderRequestDTO formAHeaderBO)
        {
            int rowsAffected;
            try
            {               
                var domainModelFormA = _mapper.Map<RmFormJHdr>(formAHeaderBO);
                domainModelFormA.FjhStatus = Common.StatusList.FormJInit;
                foreach (var formADetail in formAHeaderBO.FormJDetails)
                {
                    domainModelFormA.RmFormJDtl.Add(_mapper.Map<RmFormJDtl>(formADetail));
                }

                if (string.IsNullOrEmpty(domainModelFormA.FjhSection) ||
                    string.IsNullOrEmpty(domainModelFormA.FjhRoadName) ||
                    string.IsNullOrEmpty(domainModelFormA.FjhRmu))
                {
                    var rd = await _repoUnit.RoadmasterRepository.GetAllRoadCodeData(new RoadMasterRequestDTO
                    {
                        RoadCode = domainModelFormA.FjhRoadCode
                    });
                    if (rd != null)
                    {
                        domainModelFormA.FjhSection = rd.RdmSecName;
                        domainModelFormA.FjhRmu = rd.RdmRmuCode;
                        domainModelFormA.FjhRoadName = rd.RdmRdName;
                    }
                }


                if (domainModelFormA.FjhPkRefNo != 0)
                {
                    domainModelFormA.FjhModDt = DateTime.Now;
                    domainModelFormA.FjhModBy = _security.UserID.ToString();
                    domainModelFormA.FjhActiveYn = domainModelFormA.FjhActiveYn.GetValueOrDefault(true);
                    _repoUnit.FormJRepository.Update(domainModelFormA);

                    if (domainModelFormA.FjhSubmitSts)
                    {
                        var dtlList = await _repoUnit.FormJRepository.GetAllDtlById(domainModelFormA.FjhPkRefNo);
                        var dtl = _mapper.Map<List<RmFormJDtl>>(dtlList);
                        foreach (var list in dtl)
                        {
                            list.FjdSubmitSts = true;
                            _repoUnit.FormJRepository.UpdateDetail(list);
                            var imgList = await _repoUnit.FormJImgDtlRepository.GetAllImageByAssetPK(list.FjdPkRefNo);
                            var img = _mapper.Map<List<RmFormjImageDtl>>(imgList);
                            foreach (var data in img)
                            {
                                data.FjiSubmitSts = true;
                                _repoUnit.FormJImgDtlRepository.Update(data);
                            }
                        }
                    }


                }
                else
                {
                    domainModelFormA.FjhCrDt = DateTime.Now;
                    domainModelFormA.FjhCrBy = _security.UserID.ToString();
                    domainModelFormA.FjhModDt = DateTime.Now;
                    domainModelFormA.FjhModBy = _security.UserID.ToString();
                    _repoUnit.FormJRepository.Create(domainModelFormA);
                }

                rowsAffected = await _repoUnit.CommitAsync();
                if (domainModelFormA.FjhSubmitSts == true)
                {
                    int iResult = processService.Save(new ProcessDTO()
                    {
                        ApproveDate = DateTime.Now,
                        Form = "FormJ",
                        IsApprove = true,
                        RefId = domainModelFormA.FjhPkRefNo,
                        Remarks = "",
                        Stage = domainModelFormA.FjhStatus
                    }).Result;

                }

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> UpdateFormAAsync(FormJDetailsRequestDTO fornmADtlDTO)
        {
            int rowsAffected;
            try
            {
                var domainModelFormA = _mapper.Map<RmFormJDtl>(fornmADtlDTO);
                domainModelFormA.FjdModDt = DateTime.Now;
                domainModelFormA.FjdModBy = _security.UserID.ToString();
                _repoUnit.FormJRepository.UpdateDetail(domainModelFormA);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> DeActivateFormAAsync(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormA = await _repoUnit.FormJRepository.GetByIdAsync(formNo);
                domainModelFormA.FjhActiveYn = false;
                domainModelFormA.FjhModDt = DateTime.Now;
                domainModelFormA.FjhModBy = _security.UserID.ToString();

                var dtlList = await _repoUnit.FormJRepository.GetAllDtlById(domainModelFormA.FjhPkRefNo);
                var dtl = _mapper.Map<List<RmFormJDtl>>(dtlList);
                foreach (var list in dtl)
                {
                    list.FjdActiveYn = false;
                    _repoUnit.FormJRepository.UpdateDetail(list);
                    var imgList = await _repoUnit.FormJImgDtlRepository.GetAllImageByAssetPK(list.FjdPkRefNo);
                    var img = _mapper.Map<List<RmFormjImageDtl>>(imgList);
                    foreach (var data in img)
                    {
                        data.FjiActiveYn = false;
                        _repoUnit.FormJImgDtlRepository.Update(data);
                    }
                }
                _repoUnit.FormJRepository.Update(domainModelFormA);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<PagingResult<FormJHeaderResponseDTO>> GetFilteredFormJGrid(FilteredPagingDefinition<FormJSearchGridDTO> filterOptions)
        {
            PagingResult<FormJHeaderResponseDTO> result = new PagingResult<FormJHeaderResponseDTO>();

            List<FormJHeaderResponseDTO> formAList = new List<FormJHeaderResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormJRepository.GetFilteredRecordList(filterOptions);

                result.TotalRecords = await _repoUnit.FormJRepository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    var _ = _mapper.Map<FormJHeaderResponseDTO>(listData);
                    _.ProcessStatus = listData.FjhStatus;
                    var ddl = _repoUnit.DDLookUpRepository.FindBy(s => s.DdlType == "RMU" && (s.DdlTypeCode == _.Rmu || s.DdlTypeValue == _.Rmu)).FirstOrDefault();
                    if (ddl != null)
                    {
                        _.Rmu = ddl.DdlTypeCode;
                        _.RmuName = ddl.DdlTypeDesc;
                    }

                    var sec = _repoUnit.DDLookUpRepository.FindBy(s => s.DdlType == "Section Code" && (s.DdlTypeDesc == _.section || s.DdlTypeValue == _.section)).FirstOrDefault();
                    if (sec != null)
                    {
                        _.SectionCode = sec.DdlTypeCode;
                        _.section = sec.DdlTypeDesc;
                    }
                    formAList.Add(_);
                }

                result.PageResult = formAList;

                result.PageNo = filterOptions.StartPageNo;
                result.FilteredRecords = result.PageResult != null ? result.PageResult.Count : 0;
                }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return result;
        }

        public async Task<List<string>> GetSectionByRMU(string rmu)
        {
            var data = await _repoUnit.FormJRepository.GetSectionByRMU(rmu).ConfigureAwait(false);

            return data;

        }

        public async Task<IEnumerable<SelectListItem>> GetDefectCodeService(string assetGroup)
        {
            var defectCode = new List<SelectListItem>();
            try
            {
                var list = await _repoUnit.FormJRepository.GetDefectCode(assetGroup);
                foreach (var listData in list)
                {
                    defectCode.Add(new SelectListItem
                    {
                        Value = listData.AdcDefCode.ToString(),
                        Text = listData.AdcDefName.ToString()
                    });
                }
                return defectCode;
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }
        public async Task<IEnumerable<SelectListItem>> GetDefectCodeServiceConCat(string assetGroup)
        {
            var defectCode = new List<SelectListItem>();
            try
            {
                var list = await _repoUnit.FormJRepository.GetDefectCode(assetGroup);
                foreach (var listData in list)
                {
                    defectCode.Add(new SelectListItem
                    {
                        Value = listData.AdcDefCode.ToString(),
                        Text = listData.AdcDefCode.ToString()+"-"+listData.AdcDefName.ToString()
                    });
                }
                return defectCode;
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }
        public async Task<FormJHeaderResponseDTO> SaveHeaderWithResponse(FormJHeaderRequestDTO headerReq)
        {
            FormJHeaderResponseDTO formA;

            int refCheckCount = await _repoUnit.FormJRepository.CheckWithRef(headerReq);
            if (refCheckCount != 0)
            {
                headerReq.No = refCheckCount;
                var domainModelFormA = await _repoUnit.FormJRepository.GetFormWithDetailsByNoAsync(headerReq.No).ConfigureAwait(false);
                if (string.IsNullOrEmpty(domainModelFormA.FjhSection) ||
                    string.IsNullOrEmpty(domainModelFormA.FjhRoadName) ||
                    string.IsNullOrEmpty(domainModelFormA.FjhRmu))
                {
                    var rd = await _repoUnit.RoadmasterRepository.GetAllRoadCodeData(new RoadMasterRequestDTO
                    {
                        RoadCode = domainModelFormA.FjhRoadCode
                    });
                    if (rd != null)
                    {
                        domainModelFormA.FjhSection = rd.RdmSecName;
                        domainModelFormA.FjhRmu = rd.RdmRmuCode;
                        domainModelFormA.FjhRoadName = rd.RdmRdName;
                    }
                }
                formA = _mapper.Map<FormJHeaderResponseDTO>(domainModelFormA);
                if (domainModelFormA != null)
                {
                    foreach (var formADetail in domainModelFormA.RmFormJDtl)
                    {
                        formA.FormADetails.Add(_mapper.Map<FormJDetailResponseDTO>(formADetail));
                    }
                }

                return formA;

            }
            else
            {
                var domainModelFormA = _mapper.Map<RmFormJHdr>(headerReq);
                if (string.IsNullOrEmpty(domainModelFormA.FjhSection) ||
                    string.IsNullOrEmpty(domainModelFormA.FjhRoadName) ||
                    string.IsNullOrEmpty(domainModelFormA.FjhRmu))
                {
                    var rd = await _repoUnit.RoadmasterRepository.GetAllRoadCodeData(new RoadMasterRequestDTO
                    {
                        RoadCode = domainModelFormA.FjhRoadCode
                    });
                    if (rd != null)
                    {
                        domainModelFormA.FjhSection = rd.RdmSecName;
                        domainModelFormA.FjhRmu = rd.RdmRmuCode;
                        domainModelFormA.FjhRoadName = rd.RdmRdName;
                    }
                }
                domainModelFormA.FjhCrDt = DateTime.Now;
                domainModelFormA.FjhModDt = DateTime.Now;
                domainModelFormA.FjhCrBy = _security.UserID.ToString();
                domainModelFormA.FjhModBy = _security.UserID.ToString();
                _repoUnit.FormJRepository.Create(domainModelFormA);
                await _repoUnit.CommitAsync();
                var res = _mapper.Map<FormJHeaderResponseDTO>(domainModelFormA);
                return res;

            }
        }

        public async Task<int?> SaveDetailforHeader(FormJDetailsRequestDTO detailDTO)
        {
            try
            {
                int? headerID = null;
                var domainModelFormA = _mapper.Map<RmFormJDtl>(detailDTO);
                if (detailDTO.SiteRef_multiSelect?.Count > 0)
                {
                    domainModelFormA.FjdSiteRef = string.Join<string>(",", detailDTO.SiteRef_multiSelect);
                }


                if (domainModelFormA.FjdPkRefNo != 0)
                {
                    domainModelFormA.FjdModDt = DateTime.Now;
                    domainModelFormA.FjdModBy = _security.UserID.ToString();
                    domainModelFormA.FjdActiveYn = true;
                    _repoUnit.FormJRepository.UpdateDetail(domainModelFormA);
                    _repoUnit.Commit();
                    return domainModelFormA.FjdFjhPkRefNo;
                }
                else
                {
                    (int id, bool exists) d = await _repoUnit.FormJRepository.CheckAutoGeneratedReferenceNumber(domainModelFormA.FjdRefId);
                    if (d.exists)
                    {
                        var replacedId = domainModelFormA.FjdRefId.Remove(domainModelFormA.FjdRefId.LastIndexOf('/'), (domainModelFormA.FjdRefId.Length - domainModelFormA.FjdRefId.LastIndexOf('/')) - 1);
                        domainModelFormA.FjdRefId = $"{replacedId}/{d.id+1}";
                        domainModelFormA.FjdSrno = d.id + 1;
                    }
                    domainModelFormA.FjdFormhApp = "No";
                    domainModelFormA.FjdCrDt = DateTime.Now;
                    domainModelFormA.FjdCrBy = _security.UserID.ToString();
                    domainModelFormA.FjdModDt = DateTime.UtcNow;
                    domainModelFormA.FjdModBy = _security.UserID.ToString();
                    headerID = await _repoUnit.FormJRepository.CreateDtl(domainModelFormA).ConfigureAwait(false);
                    return headerID;
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

        }

        public async Task<int?> SaveDetailforHeaderV1(FormJDetailsRequestDTO detailDTO)
        {
            try
            {
                int? headerID = null;
                var domainModelFormA = _mapper.Map<RmFormJDtl>(detailDTO);
                if (detailDTO.SiteRef_multiSelect?.Count > 0)
                {
                    domainModelFormA.FjdSiteRef = string.Join<string>(",", detailDTO.SiteRef_multiSelect);
                }


                if (domainModelFormA.FjdPkRefNo != 0)
                {

                    domainModelFormA.FjdActiveYn = true;
                    domainModelFormA.FjdModDt = DateTime.Now;
                    domainModelFormA.FjdModBy = _security.UserID.ToString();
                    _repoUnit.FormJRepository.UpdateDetail(domainModelFormA);
                    _repoUnit.Commit();
                    return domainModelFormA.FjdPkRefNo;
                }
                else
                {
                    domainModelFormA.FjdCrDt = DateTime.Now;
                    domainModelFormA.FjdCrBy = _security.UserID.ToString();
                    domainModelFormA.FjdModDt = DateTime.Now;
                    domainModelFormA.FjdModBy = _security.UserID.ToString();
                    headerID = await _repoUnit.FormJRepository.CreateDtlV1(domainModelFormA).ConfigureAwait(false);
                    return headerID;
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

        }

        public async Task<FormJDetailsRequestDTO> GetDetailById(int detailId)
        {
            var detailData = await _repoUnit.FormJRepository.GetDetailByIdAsync(detailId).ConfigureAwait(false);
            if (detailData != null)
            {
                FormJDetailsRequestDTO formA = _mapper.Map<FormJDetailsRequestDTO>(detailData);
                formA.Dt = detailData.FjdDt.HasValue ? detailData.FjdDt.Value.ToString("yyyy-MM-dd") : "";
                var data = await _repoUnit.FormJRepository.GetFAHRefIDById(detailData.FjdFjhPkRefNo.GetValueOrDefault());
                formA.FadRefNO = data?.FjhRefId;
                if (!string.IsNullOrEmpty(formA.FadRefNO))
                {
                    formA.FadRefNO = $"{formA.FadRefNO}/{formA.Srno}";
                }
                if (!string.IsNullOrEmpty(formA.SiteRef))
                {
                    formA.SiteRef_multiSelect = formA.SiteRef.Split(",").OfType<string>().ToList();
                }
                return formA;
            }
            else
                return null;
        }

        public async Task<PagingResult<FormJDetailResponseDTO>> GetFormADetailGrid(FilteredPagingDefinition<FormJDetailsRequestDTO> detailList)
        {
            PagingResult<FormJDetailResponseDTO> result = new PagingResult<FormJDetailResponseDTO>();

            List<FormJDetailResponseDTO> formAList = new List<FormJDetailResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormJRepository.GetDetailRecordList(detailList);

                result.TotalRecords = await _repoUnit.FormJRepository.GetDetailRecordCount(detailList).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    var _ = _mapper.Map<FormJDetailResponseDTO>(listData);
                    _.Dt = listData.FjdDt.HasValue ? $"{listData.FjdDt.Value.ToString("dd/MM/yyyy")}" : "";
                    formAList.Add(_);
                }

                result.PageResult = formAList;

                result.PageNo = detailList.StartPageNo;
                result.FilteredRecords = result.PageResult != null ? result.PageResult.Count : 0;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return result;
        }

        public async Task<int> DeActivateDetail(int detailId)
        {
            int rowsAffected;
            try
            {
                var domainModelFormA = await _repoUnit.FormJRepository.GetDetailByIdAsync(detailId);
                domainModelFormA.FjdActiveYn = false;
                _repoUnit.FormJRepository.UpdateDetail(domainModelFormA);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> LastInsertedFormDetailSRNO(int headerId)
        {

            int imageCt = await _repoUnit.FormJRepository.GetSrNo(headerId);
            return imageCt;
        }

        public async Task<FormJHeaderResponseDTO> GetHeaderById(int headerId)
        {

            var formHeader = await _repoUnit.FormJRepository.GetByIdAsync(headerId);

            FormJHeaderResponseDTO responseDTO = _mapper.Map<FormJHeaderResponseDTO>(formHeader);
            return responseDTO;
        }

        public async Task<List<ImageListRequestDTO>> GetAllImageList()
        {
            List<ImageListRequestDTO> imageList = new List<ImageListRequestDTO>();
            try
            {
                var image = await _repoUnit.AllAssetRepository.GetAllImageList().ConfigureAwait(false);
                foreach (var listData in image)
                {
                    imageList.Add(_mapper.Map<ImageListRequestDTO>(listData));
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return imageList;
        }

        public async Task<List<ImageListRequestDTO>> GetFilterImageList(string imageTypeCode)
        {
            List<ImageListRequestDTO> imageList = new List<ImageListRequestDTO>();
            try
            {
                var image = await _repoUnit.AllAssetRepository.GetFilterImageList(imageTypeCode).ConfigureAwait(false);
                foreach (var listData in image)
                {
                    imageList.Add(_mapper.Map<ImageListRequestDTO>(listData));
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return imageList;
        }

        public async Task<int> GetAssetPK(string assetId)
        {
            return await _repoUnit.AllAssetRepository.GetAssetPK(assetId);
        }
        public string CheckAlreadyExists(string roadCode, int month, int year, string assetGroup)
        {
            return _repoUnit.FormJRepository.CheckAlreadyExists(roadCode, month, year, assetGroup);
        }

        public string GetAssetCodeByName(string name)
        {
            return _repoUnit.FormJRepository.GetAssetCodeByName(name);
        }

        public async Task<int> GetLastInsertedHeader()
        {
            return await _repoUnit.FormJRepository.GetLastInsertedHeader();
        }

        public FormASearchDropdown GetDropdown(RequestDropdownFormA request)
        {
            return _repoUnit.FormJRepository.GetDropdown(request);
        }

        public FORMJRpt GetReportData(int headerId, int pageIndex, int pageCount)
        {
            return _repoUnit.FormJRepository.GetReportData(headerId);
        }

        public byte[] FormDownload(string formName, int id, string filePath)
        {
            string oldFileName = "";
            string fileName = "";
            string cacheFile = "";
            if (!filePath.Contains(".xlsx"))
            {
                oldFileName = filePath + formName + ".xlsx";
                fileName = formName + DateTime.Now.ToString("yyyyMMddHHmmssfffffff").ToString();
                cacheFile = filePath + fileName + ".xlsx";
            }
            else
            {
                oldFileName = filePath;
                fileName = filePath.Replace(".xlsx", DateTime.Now.ToString("yyyyMMddHHmmssfffffff").ToString() + ".xlsx");
                cacheFile = fileName;
            }

            try
            {
                FORMJRpt rpt = this.GetReportData(id,0,0);
                System.IO.File.Copy(oldFileName, cacheFile, true);
                using (var workBook = new XLWorkbook(cacheFile))
                {
                    int noOfSheets = (rpt.Details.Count / 16) + ((rpt.Details.Count % 16) > 0 ? 1 : 1);
                    for (int sheet = 2; sheet <= noOfSheets; sheet++)
                    {
                        using (var tempWorkBook = new XLWorkbook(cacheFile))
                        {
                            string sheetName = "sheet" + Convert.ToString(sheet);
                            IXLWorksheet copySheet = tempWorkBook.Worksheet(1);
                            copySheet.Worksheet.Name = sheetName;
                            copySheet.Cell(10, 2).Value = rpt.Header.RMUCode; 
                            copySheet.Cell(11, 2).Value = rpt.Header.RoadName;
                            copySheet.Cell(10, 7).Value = rpt.Header.RoadCode;
                            copySheet.Cell(10, 20).Value = rpt.Header.RefId;
                            copySheet.Cell(12, 21).Value = sheet;
                            copySheet.Cell(12, 23).Value = noOfSheets;
                            copySheet.Cell(33, 20).Value = rpt.Header.InspectedBy;
                            copySheet.Cell(34, 20).Value = rpt.Header.InspectedByDesignation;
                            copySheet.Cell(35, 20).Value = rpt.Header.InspectedDate.HasValue ? rpt.Header.InspectedDate.Value.ToString("dd-MM-yyyy") : "";
                            copySheet.Cell(38, 20).Value = rpt.Header.CheckedBY;
                            copySheet.Cell(39, 20).Value = rpt.Header.CheckedByDesignation;
                            copySheet.Cell(40, 20).Value = rpt.Header.CheckedDate.HasValue ? rpt.Header.CheckedDate.Value.ToString("dd-MM-yyyy") : "";
                            copySheet.Cell(43, 20).Value = rpt.Header.AuditedBy;
                            copySheet.Cell(44, 20).Value = rpt.Header.AuditedByDesignation;
                            copySheet.Cell(45, 20).Value = rpt.Header.AuditedDate.HasValue ? rpt.Header.CheckedDate.Value.ToString("dd-MM-yyyy") : "";


                            workBook.AddWorksheet(copySheet);
                        }
                    }
                    int index = 1;
                    for (int sheet = 1; sheet <= noOfSheets; sheet++)
                    {


                        IXLWorksheet workSheet = workBook.Worksheet(sheet);

                        if (workSheet != null)
                        {

                            workSheet.Cell(10, 2).Value = rpt.Header.RMUName;
                            workSheet.Cell(11, 2).Value = rpt.Header.RoadName;
                            workSheet.Cell(10, 7).Value = rpt.Header.RoadCode;
                            workSheet.Cell(10, 19).Value = $"Ref:{rpt.Header.RefId}";
                            workSheet.Cell(12, 21).Value = sheet;
                            workSheet.Cell(12, 23).Value = noOfSheets;
                            workSheet.Cell(33, 20).Value = rpt.Header.InspectedBy;
                            workSheet.Cell(34, 20).Value = rpt.Header.InspectedByDesignation;
                            workSheet.Cell(35, 20).Value = rpt.Header.InspectedDate.HasValue ? rpt.Header.InspectedDate.Value.ToString("dd-MM-yyyy") : "";
                            workSheet.Cell(38, 20).Value = rpt.Header.CheckedBY;
                            workSheet.Cell(39, 20).Value = rpt.Header.CheckedByDesignation;
                            workSheet.Cell(40, 20).Value = rpt.Header.CheckedDate.HasValue ? rpt.Header.CheckedDate.Value.ToString("dd-MM-yyyy") : "";
                            workSheet.Cell(43, 20).Value = rpt.Header.AuditedBy;
                            workSheet.Cell(44, 20).Value = rpt.Header.AuditedByDesignation;
                            workSheet.Cell(45, 20).Value = rpt.Header.AuditedDate.HasValue ? rpt.Header.CheckedDate.Value.ToString("dd-MM-yyyy") : "";

                            int i = 14;

                            var data = rpt.Details.Skip((sheet - 1) * 16).Take(16);
                            foreach (var r in data)
                            {

                                i++;
                                if (i > 30) { }
                                else
                                {
                                    workSheet.Cell(i, 1).Value = r.Date;
                                    workSheet.Cell(i, 2).Value = index;
                                    workSheet.Cell(i, 3).Value = r.SiteRef;
                                    workSheet.Cell(i, 4).Value = r.LocationFrom;
                                    workSheet.Cell(i, 5).Value = r.LocationTo;
                                    workSheet.Cell(i, 6).Value = r.SACode;
                                    workSheet.Cell(i, 7).Value = r.Dificiencies;
                                    workSheet.Cell(i, 9).Value = r.WorkInstallation;
                                    workSheet.Cell(i, 11).Value = r.Dimention;
                                    workSheet.Cell(i, 13).Value = r.Pr;
                                    workSheet.Cell(i, 14).Value = r.WI;
                                    workSheet.Cell(i, 15).Value = r.WS;
                                    workSheet.Cell(i, 16).Value = r.WTC;
                                    workSheet.Cell(i, 17).Value = r.WC;
                                    workSheet.Cell(i, 18).Value = r.RT;
                                    workSheet.Cell(i, 19).Value = r.Remarks;

                                }
                                index++;

                            }
                        }
                    }


                    using (var stream = new MemoryStream())
                    {
                        workBook.SaveAs(stream);
                        var content = stream.ToArray();
                        System.IO.File.Delete(cacheFile);
                        return content;
                    }
                }
            }
            catch (Exception ex)
            {
                System.IO.File.Copy(oldFileName, cacheFile, true);
                using (var workBook = new XLWorkbook(cacheFile))
                {
                    using (var stream = new MemoryStream())
                    {
                        workBook.SaveAs(stream);
                        var content = stream.ToArray();
                        System.IO.File.Delete(cacheFile);
                        return content;
                    }
                }

            }
        }


        public async Task<int> UpdateFormJSignature(FormJHeaderRequestDTO formJDTO)
        {
            int rowsAffected;
            try
            {

                var getHeader = await _repoUnit.FormJRepository.GetByIdAsync(formJDTO.No);
                getHeader.FjhSubmitSts = formJDTO.SubmitSts;

                getHeader.FjhSignPrp = formJDTO.SignPrp ?? getHeader.FjhSignPrp ?? null;
                getHeader.FjhSignVer = formJDTO.SignVer ?? getHeader.FjhSignVer ?? null;
                getHeader.FjhSignVet = formJDTO.SignVet ?? getHeader.FjhSignVet ?? null;


                getHeader.FjhUseridPrp = formJDTO.UseridPrp ?? getHeader.FjhUseridPrp ?? null;
                getHeader.FjhUsernamePrp = formJDTO.UsernamePrp ?? getHeader.FjhUsernamePrp ?? null;
                getHeader.FjhDesignationPrp = formJDTO.DesignationPrp ?? getHeader.FjhDesignationPrp ?? null;
                getHeader.FjhDtPrp = formJDTO.DtPrp ?? getHeader.FjhDtPrp ?? null;

                getHeader.FjhUseridVer = formJDTO.UseridVer ?? getHeader.FjhUseridVer ?? null;
                getHeader.FjhUsernameVer = formJDTO.UsernameVer ?? getHeader.FjhUsernameVer ?? null;
                getHeader.FjhDesignationVer = formJDTO.DesignationVer ?? getHeader.FjhDesignationVer ?? null;
                getHeader.FjhDtVer = formJDTO.VerifiedDt ?? getHeader.FjhDtVer ?? null;

                getHeader.FjhUseridVet = formJDTO.UseridVet ?? getHeader.FjhUseridVet ?? null;
                getHeader.FjhUsernameVet = formJDTO.UsernameVet ?? getHeader.FjhUsernameVet ?? null;
                getHeader.FjhDesignationVet = formJDTO.DesignationVet ?? getHeader.FjhDesignationVet ?? null;
                getHeader.FjhDtVet = formJDTO.AuditedDt ?? getHeader.FjhDtVet ?? null;

                


                var formJ = _mapper.Map<RmFormJHdr>(getHeader);

                formJ.FjhModDt = DateTime.Now;
                formJ.FjhModBy = _security.UserID.ToString();
                _repoUnit.FormJRepository.Update(formJ);


                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }
    }

}
