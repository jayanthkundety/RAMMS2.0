using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.RequestBO;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RAMMS.Common;
using Serilog;
using AutoMapper;
using RAMMS.Domain.Models;
using RAMMS.DTO.Wrappers;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using ClosedXML.Excel;
using System.IO;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormAService : IFormAService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormAService(IRepositoryUnit repoUnit, IMapper mapper, ISecurity security, IProcessService proService)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security ?? throw new ArgumentNullException(nameof(security));
            processService = proService;
        }


        public async Task<FormAHeaderRequestDTO> GetFormAWithDetailsByNoAsync(int formNo)
        {
            FormAHeaderRequestDTO formA;
            try
            {
                var domainModelFormA = await _repoUnit.FormARepository.GetFormWithDetailsByNoAsync(formNo).ConfigureAwait(false);
                if (string.IsNullOrEmpty(domainModelFormA.FahSection) ||
                    string.IsNullOrEmpty(domainModelFormA.FahRoadName) ||
                    string.IsNullOrEmpty(domainModelFormA.FahRmu))
                {
                    var rd = await _repoUnit.RoadmasterRepository.GetAllRoadCodeData(new RoadMasterRequestDTO
                    {
                        RoadCode = domainModelFormA.FahRoadCode
                    });
                    if (rd != null)
                    {
                        domainModelFormA.FahSection = rd.RdmSecName;
                        domainModelFormA.FahRmu = rd.RdmRmuCode;
                        domainModelFormA.FahRoadName = rd.RdmRdName;
                    }
                }

                formA = _mapper.Map<FormAHeaderRequestDTO>(domainModelFormA);
                foreach (var formADetail in domainModelFormA.RmFormADtl)
                {
                    var _ = _mapper.Map<FormADetailsRequestDTO>(formADetail);
                    _.Dt = formADetail.FadDt.HasValue ? formADetail.FadDt.Value.ToString("yyyy-MM-dd") : "";
                    formA.FormADetails.Add(_);
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return formA;
        }

        public async Task<int> SaveFormAAsync(FormAHeaderRequestDTO formAHeaderBO)
        {
            int rowsAffected;
            try
            {              
                var domainModelFormA = _mapper.Map<RmFormAHdr>(formAHeaderBO);
                domainModelFormA.FahStatus = StatusList.FormAInit;

                foreach (var formADetail in formAHeaderBO.FormADetails)
                {
                    domainModelFormA.RmFormADtl.Add(_mapper.Map<RmFormADtl>(formADetail));
                }

                if (string.IsNullOrEmpty(domainModelFormA.FahSection) ||
                    string.IsNullOrEmpty(domainModelFormA.FahRoadName) ||
                    string.IsNullOrEmpty(domainModelFormA.FahRmu))
                {
                    var rd = await _repoUnit.RoadmasterRepository.GetAllRoadCodeData(new RoadMasterRequestDTO
                    {
                        RoadCode = domainModelFormA.FahRoadCode
                    });
                    if (rd != null)
                    {
                        domainModelFormA.FahSection = rd.RdmSecName;
                        domainModelFormA.FahRmu = rd.RdmRmuCode;
                        domainModelFormA.FahRoadName = rd.RdmRdName;
                    }
                }


                if (domainModelFormA.FahPkRefNo != 0)
                {
                    domainModelFormA.FahModDt = DateTime.Now;
                    domainModelFormA.FahModBy = _security.UserID.ToString();
                    domainModelFormA.FahActiveYn = domainModelFormA.FahActiveYn.GetValueOrDefault(true);
                    _repoUnit.FormARepository.Update(domainModelFormA);

                    if(domainModelFormA.FahSubmitSts)
                    {
                        var dtlList = await _repoUnit.FormADtlRepository.GetAllDtlById(domainModelFormA.FahPkRefNo);
                        var dtl = _mapper.Map<List<RmFormADtl>>(dtlList);
                        foreach (var list in dtl)
                        {
                            list.FadSubmitSts = true;
                            _repoUnit.FormADtlRepository.Update(list);
                            var imgList = await _repoUnit.FormAImgDtlRepository.GetAllImageByAssetPK(list.FadPkRefNo);
                            var img = _mapper.Map<List<RmFormaImageDtl>>(imgList);
                            foreach (var data in img)
                            {
                                data.FaiSubmitSts = true;
                                _repoUnit.FormAImgDtlRepository.Update(data);
                            }
                        } 
                    }
                }
                else
                {
                    domainModelFormA.FahModDt = DateTime.Now;
                    domainModelFormA.FahCrDt = DateTime.Now;
                    domainModelFormA.FahModBy = _security.UserID.ToString();
                    domainModelFormA.FahCrBy = _security.UserID.ToString();
                    _repoUnit.FormARepository.Create(domainModelFormA);
                }                
                rowsAffected = await _repoUnit.CommitAsync();
                if (domainModelFormA.FahSubmitSts == true)
                {
                    int iResult = processService.Save(new ProcessDTO()
                    {
                        ApproveDate = DateTime.Now,
                        Form = "FormA",
                        IsApprove = true,
                        RefId = domainModelFormA.FahPkRefNo,
                        Remarks = "",
                        Stage = domainModelFormA.FahStatus
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

        public async Task<int> UpdateFormAAsync(FormADetailsRequestDTO fornmAdtlDTO)
        {
            int rowsAffected;
            try
            {
                var domainModelFormA = _mapper.Map<RmFormADtl>(fornmAdtlDTO);
                domainModelFormA.FadModDt = DateTime.Now;
                domainModelFormA.FadModBy = _security.UserID.ToString();
                _repoUnit.FormARepository.UpdateDetail(domainModelFormA);
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
                var domainModelFormA = await _repoUnit.FormARepository.GetByIdAsync(formNo);
                domainModelFormA.FahActiveYn = false;
                _repoUnit.FormARepository.Update(domainModelFormA);

                var dtlList = await _repoUnit.FormADtlRepository.GetAllDtlById(domainModelFormA.FahPkRefNo);
                var dtl = _mapper.Map<List<RmFormADtl>>(dtlList);
                foreach (var list in dtl)
                {
                    list.FadActiveYn = false;
                    _repoUnit.FormADtlRepository.Update(list);
                    var imgList = await _repoUnit.FormAImgDtlRepository.GetAllImageByAssetPK(list.FadPkRefNo);
                    var img = _mapper.Map<List<RmFormaImageDtl>>(imgList);
                    foreach (var data in img)
                    {
                        data.FaiActiveYn = false;
                        _repoUnit.FormAImgDtlRepository.Update(data);
                    }
                }            

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<PagingResult<FormAHeaderResponseDTO>> GetFilteredFormAGrid(FilteredPagingDefinition<FormASearchGridDTO> filterOptions)
        {
            PagingResult<FormAHeaderResponseDTO> result = new PagingResult<FormAHeaderResponseDTO>();

            List<FormAHeaderResponseDTO> formAList = new List<FormAHeaderResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormARepository.GetFilteredRecordList(filterOptions);

                result.TotalRecords = await _repoUnit.FormARepository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    var _ = _mapper.Map<FormAHeaderResponseDTO>(listData);
                    _.ProcessStatus = listData.FahStatus;
                    _.MonthYear = $"{_.Month}/{_.Year}";
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
            var data = await _repoUnit.FormARepository.GetSectionByRMU(rmu).ConfigureAwait(false);

            return data;

        }

        public async Task<IEnumerable<SelectListItem>> GetDefectCodeService(string assetGroup)
        {
            var defectCode = new List<SelectListItem>();
            try
            {
                var list = await _repoUnit.FormARepository.GetDefectCode(assetGroup);
                foreach (var listData in list)
                {
                    defectCode.Add(new SelectListItem
                    {
                        Value = listData.AdcDefCode.ToString(),
                        Text = listData.AdcDefCode.ToString()+ "-"+listData.AdcDefName.ToString()
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

        public async Task<FormAHeaderResponseDTO> SaveHeaderwithResponse(FormAHeaderRequestDTO headerReq)
        {
            FormAHeaderResponseDTO formA;

            int RefCheckCount = await _repoUnit.FormARepository.CheckwithRef(headerReq);
            if (RefCheckCount != 0)
            {
                headerReq.No = RefCheckCount;
                var domainModelFormA = await _repoUnit.FormARepository.GetFormWithDetailsByNoAsync(headerReq.No).ConfigureAwait(false);
                if (string.IsNullOrEmpty(domainModelFormA.FahSection) ||
                    string.IsNullOrEmpty(domainModelFormA.FahRoadName) ||
                    string.IsNullOrEmpty(domainModelFormA.FahRmu))
                {
                    var rd = await _repoUnit.RoadmasterRepository.GetAllRoadCodeData(new RoadMasterRequestDTO
                    {
                        RoadCode = domainModelFormA.FahRoadCode
                    });
                    if (rd != null)
                    {
                        domainModelFormA.FahSection = rd.RdmSecName;
                        domainModelFormA.FahRmu = rd.RdmRmuCode;
                        domainModelFormA.FahRoadName = rd.RdmRdName;
                    }
                }
                formA = _mapper.Map<FormAHeaderResponseDTO>(domainModelFormA);
                if (domainModelFormA != null)
                {
                    foreach (var formADetail in domainModelFormA.RmFormADtl)
                    {
                        formA.FormADetails.Add(_mapper.Map<FormADetailResponseDTO>(formADetail));
                    }
                }

                return formA;

            }
            else
            {
                var domainModelFormA = _mapper.Map<RmFormAHdr>(headerReq);
                if (string.IsNullOrEmpty(domainModelFormA.FahSection) ||
                    string.IsNullOrEmpty(domainModelFormA.FahRoadName) ||
                    string.IsNullOrEmpty(domainModelFormA.FahRmu))
                {
                    var rd = await _repoUnit.RoadmasterRepository.GetAllRoadCodeData(new RoadMasterRequestDTO
                    {
                        RoadCode = domainModelFormA.FahRoadCode
                    });
                    if (rd != null)
                    {
                        domainModelFormA.FahSection = rd.RdmSecName;
                        domainModelFormA.FahRmu = rd.RdmRmuCode;
                        domainModelFormA.FahRoadName = rd.RdmRdName;
                    }
                }
                domainModelFormA.FahModDt = DateTime.Now;
                domainModelFormA.FahCrDt = DateTime.Now;
                domainModelFormA.FahModBy = _security.UserID.ToString();
                domainModelFormA.FahCrBy = _security.UserID.ToString();
                _repoUnit.FormARepository.Create(domainModelFormA);

                await _repoUnit.CommitAsync();
                var res = _mapper.Map<FormAHeaderResponseDTO>(domainModelFormA);
                return res;

            }
        }

        public async Task<int?> SaveDetailforHeader(FormADetailsRequestDTO detailDTO)
        {
            try
            {
                int? headerID = null;
                var domainModelFormA = _mapper.Map<RmFormADtl>(detailDTO);
                if (detailDTO.SiteRef_multiSelect?.Count > 0)
                {
                    domainModelFormA.FadSiteRef = string.Join<string>(",", detailDTO.SiteRef_multiSelect);
                }


                if (domainModelFormA.FadPkRefNo != 0)
                {

                    domainModelFormA.FadActiveYn = true;
                    domainModelFormA.FadModDt = DateTime.Now;
                    domainModelFormA.FadModBy = _security.UserID.ToString();
                    _repoUnit.FormARepository.UpdateDetail(domainModelFormA);
                    _repoUnit.Commit();
                    return domainModelFormA.FadFahPkRefNo;
                }
                else
                {
                    (int id, bool exists) d = await _repoUnit.FormARepository.CheckAutoGeneratedReferenceNumber(domainModelFormA.FadRefId);
                    if (d.exists)
                    {
                        var replacedId = domainModelFormA.FadRefId.Remove(domainModelFormA.FadRefId.LastIndexOf('/'), (domainModelFormA.FadRefId.Length - domainModelFormA.FadRefId.LastIndexOf('/')) - 1);
                        domainModelFormA.FadRefId = $"{replacedId}/{d.id+1}";
                        domainModelFormA.FadSrno = d.id + 1;
                    }
                    domainModelFormA.FadFormhApp = "No";
                    domainModelFormA.FadModDt = DateTime.Now;
                    domainModelFormA.FadModBy = _security.UserID.ToString();
                    domainModelFormA.FadCrDt = DateTime.Now;
                    domainModelFormA.FadCrBy = _security.UserID.ToString();
                    headerID = await _repoUnit.FormARepository.CreateDtl(domainModelFormA).ConfigureAwait(false);
                    return headerID;
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

        }

        public async Task<int?> SaveDetailforHeaderV1(FormADetailsRequestDTO detailDTO)
        {
            try
            {
                int? headerID = null;
                var domainModelFormA = _mapper.Map<RmFormADtl>(detailDTO);
                if (detailDTO.SiteRef_multiSelect?.Count > 0)
                {
                    domainModelFormA.FadSiteRef = string.Join<string>(",", detailDTO.SiteRef_multiSelect);
                }


                if (domainModelFormA.FadPkRefNo != 0)
                {

                    domainModelFormA.FadActiveYn = true;
                    domainModelFormA.FadModDt = DateTime.Now;
                    domainModelFormA.FadModBy = _security.UserID.ToString();
                    _repoUnit.FormARepository.UpdateDetail(domainModelFormA);
                    _repoUnit.Commit();
                    return domainModelFormA.FadPkRefNo;
                }
                else
                {
                    domainModelFormA.FadModDt = DateTime.Now;
                    domainModelFormA.FadCrDt = DateTime.Now;
                    domainModelFormA.FadModBy = _security.UserID.ToString();
                    domainModelFormA.FadCrBy = _security.UserID.ToString();
                    headerID = await _repoUnit.FormARepository.CreateDtlV1(domainModelFormA).ConfigureAwait(false);
                    return headerID;
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

        }

        public async Task<FormADetailsRequestDTO> GetDetailById(int detailId)
        {
            var detailData = await _repoUnit.FormARepository.GetDetailByIdAsync(detailId).ConfigureAwait(false);
            if (detailData != null)
            {
                FormADetailsRequestDTO formA = _mapper.Map<FormADetailsRequestDTO>(detailData);
                formA.Dt = detailData.FadDt.HasValue ? detailData.FadDt.Value.ToString("yyyy-MM-dd") : "";
                var data = await _repoUnit.FormARepository.GetFAHRefIDById(detailData.FadFahPkRefNo.GetValueOrDefault());
                formA.FadRefNO = data?.FahRefId;
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

        public async Task<PagingResult<FormADetailResponseDTO>> GetFormADetailGrid(FilteredPagingDefinition<FormADetailsRequestDTO> detailList)
        {
            PagingResult<FormADetailResponseDTO> result = new PagingResult<FormADetailResponseDTO>();

            List<FormADetailResponseDTO> formAList = new List<FormADetailResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormARepository.GetDetailRecordList(detailList);

                result.TotalRecords = await _repoUnit.FormARepository.GetDetailRecordCount(detailList).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    var _ = _mapper.Map<FormADetailResponseDTO>(listData);
                    _.Dt = listData.FadDt.HasValue ? $"{listData.FadDt.Value.ToString("dd/MM/yyyy")}" : "";
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
                var domainModelFormA = await _repoUnit.FormARepository.GetDetailByIdAsync(detailId);
                domainModelFormA.FadActiveYn = false;
                domainModelFormA.FadModDt = DateTime.Now;
                domainModelFormA.FadModBy = _security.UserID.ToString();
                _repoUnit.FormARepository.UpdateDetail(domainModelFormA);

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
            int imageCt = await _repoUnit.FormARepository.GetSrNo(headerId);
            return imageCt;
        }

        public async Task<FormAHeaderResponseDTO> GetHeaderById(int headerId)
        {

            var formHeader = await _repoUnit.FormARepository.GetByIdAsync(headerId);

            FormAHeaderResponseDTO responseDTO = _mapper.Map<FormAHeaderResponseDTO>(formHeader);
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

        public async Task<int> GetAssetPK(string AssetId)
        {
            return await _repoUnit.AllAssetRepository.GetAssetPK(AssetId);
        }


        public async Task<int> UpdateSignature(FormAHeaderRequestDTO formA)
        {
            int rowsAffected;
            try
            {
               
                var getHeader = await _repoUnit.FormARepository.GetByIdAsync(formA.No);
                getHeader.FahSubmitSts = formA.SubmitSts;
                getHeader.FahSignPrp = formA.SignPrp ?? getHeader.FahSignPrp ?? null;
                getHeader.FahSignVer = formA.SignVer ?? getHeader.FahSignVer ?? null;


                getHeader.FahUseridPrp = formA.UseridPrp ?? getHeader.FahUseridPrp ?? null;
                getHeader.FahUseridVer = formA.UseridVer ?? getHeader.FahUseridVer ?? null;
                getHeader.FahUsernamePrp = formA.UsernamePrp ?? getHeader.FahUsernamePrp ?? null;
                getHeader.FahUsernameVer = formA.UsernameVer ?? getHeader.FahUsernameVer ?? null;
                getHeader.FahDesignationPrp = formA.DesignationPrp ?? getHeader.FahDesignationPrp ?? null;
                getHeader.FahDesignationVer = formA.DesignationVer ?? getHeader.FahDesignationVer ?? null;
                getHeader.FahDtPrp = formA.DtPrp ?? getHeader.FahDtPrp ?? null;
                getHeader.FahDtVer = formA.VerifiedDt ?? getHeader.FahDtVer ?? null;


                var formAUpdate = _mapper.Map<RmFormAHdr>(getHeader);

                formAUpdate.FahModDt = DateTime.Now;
                formAUpdate.FahModBy = _security.UserID.ToString();

                _repoUnit.FormARepository.Update(formAUpdate);


                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> GetLastInsertedHeader()
        {
            return await _repoUnit.FormARepository.GetLastInsertedHeader();
        }

        public FormASearchDropdown GetDropdown(RequestDropdownFormA request)
        {
            return _repoUnit.FormARepository.GetDropdown(request);
        }

        public string CheckAlreadyExists(string roadCode, int month, int year, string assetGroup)
        {
            return _repoUnit.FormARepository.CheckAlreadyExists(roadCode, month, year, assetGroup);
        }

        public string GetAssetCodeByName(string name)
        {
            return _repoUnit.FormARepository.GetAssetCodeByName(name);
        }

        public FORMARpt GetReportData(int headerId, int pageIndex, int pageCount)
        {
            return _repoUnit.FormARepository.GetReportData(headerId, pageIndex, pageCount);
        }


        public Byte[] FormDownload(string formName, int id, string filePath)
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
                FORMARpt rpt = this.GetReportData(id, 0, 10);
                System.IO.File.Copy(oldFileName, cacheFile, true);
                using (var workBook = new XLWorkbook(cacheFile))
                {
                    int noOfSheets = (rpt.Detail.Count /14) + ((rpt.Detail.Count % 14) >0 ? 1 : 1);
                    for (int sheet = 2; sheet <= noOfSheets; sheet++)
                    {
                        using (var tempWorkBook = new XLWorkbook(cacheFile))
                        {
                            string sheetName = "sheet" + Convert.ToString(sheet);
                            IXLWorksheet copySheet = tempWorkBook.Worksheet(1);
                            copySheet.Worksheet.Name = sheetName;
                            copySheet.Cell(2, 3).Value = rpt.Header.RoadCode;
                            copySheet.Cell(3, 3).Value = rpt.Header.RoadName;
                            copySheet.Cell(2, 11).Value = rpt.Header.RMUCode;
                            copySheet.Cell(2, 17).Value = sheet;
                            copySheet.Cell(2, 19).Value = noOfSheets;
                            copySheet.Cell(2, 40).Value = rpt.Header.RefId;
                            copySheet.Cell(37, 30).Value = rpt.Header.InspectedBy;
                            copySheet.Cell(39, 30).Value = rpt.Header.InspectedByDesignation;
                            copySheet.Cell(41, 30).Value = rpt.Header.InspectedDate.HasValue ? rpt.Header.InspectedDate.Value.ToString("dd-MM-yyyy") : "";
                            copySheet.Cell(37, 38).Value = rpt.Header.VerifiedBY;
                            copySheet.Cell(39, 38).Value = rpt.Header.VerifiedByDesignation;
                            copySheet.Cell(41, 38).Value = rpt.Header.VerifiedDate.HasValue ? rpt.Header.VerifiedDate.Value.ToString("dd-MM-yyyy") : "";
                            workBook.AddWorksheet(copySheet);
                        }
                    }
                    int index = 1;
                    for (int sheet = 1; sheet <= noOfSheets; sheet++)
                    {


                        IXLWorksheet workSheet;
                        workBook.Worksheets.TryGetWorksheet($"sheet{sheet}", out workSheet);

                        if (workSheet != null)
                        {
                            workSheet.Cell(2, 3).Value = rpt.Header.RoadCode;
                            workSheet.Cell(3, 3).Value = rpt.Header.RoadName;
                            workSheet.Cell(2, 10).Value = rpt.Header.RMUName;
                            workSheet.Cell(2, 17).Value = sheet;
                            workSheet.Cell(2, 19).Value = noOfSheets;
                            workSheet.Cell(2, 40).Value = rpt.Header.RefId;
                            workSheet.Cell(37, 30).Value = rpt.Header.InspectedBy;
                            workSheet.Cell(39, 30).Value = rpt.Header.InspectedByDesignation;
                            workSheet.Cell(41, 30).Value = rpt.Header.InspectedDate.HasValue ? rpt.Header.InspectedDate.Value.ToString("dd-MM-yyyy") : "";
                            workSheet.Cell(37, 38).Value = rpt.Header.VerifiedBY;
                            workSheet.Cell(39, 38).Value = rpt.Header.VerifiedByDesignation;
                            workSheet.Cell(41, 38).Value = rpt.Header.VerifiedDate.HasValue ? rpt.Header.VerifiedDate.Value.ToString("dd-MM-yyyy") : "";

                            int i = 7;
                            
                            var data = rpt.Detail.AsEnumerable().Skip((sheet - 1) * 14).Take(14);
                            foreach (var r in data)
                            {

                                i++;
                                if (i > 22) { }
                                else
                                {
                                    workSheet.Cell(i, 1).Value = r.Date;
                                    workSheet.Cell(i, 3).Value = index;
                                    workSheet.Cell(i, 5).Value = r.SiteRef;
                                    workSheet.Cell(i, 7).Value = r.LocationFrom;
                                    workSheet.Cell(i, 9).Value = r.LocationTo;
                                    workSheet.Cell(i, 11).Value = r.L_R;
                                    workSheet.Cell(i, 12).Value = r.L_D;
                                    workSheet.Cell(i, 13).Value = r.L_Sh;
                                    workSheet.Cell(i, 14).Value = r.L_EL;
                                    workSheet.Cell(i, 15).Value = r.L_P;
                                    workSheet.Cell(i, 16).Value = r.CL;
                                    workSheet.Cell(i, 17).Value = r.R_P;
                                    workSheet.Cell(i, 18).Value = r.R_EL;
                                    workSheet.Cell(i, 19).Value = r.R_Sh;
                                    workSheet.Cell(i, 20).Value = r.R_D;
                                    workSheet.Cell(i, 21).Value = r.R_R;
                                    workSheet.Cell(i, 22).Value = r.Cul;
                                    workSheet.Cell(i, 23).Value = r.Br;
                                    workSheet.Cell(i, 24).Value = r.Description;
                                    workSheet.Cell(i, 27).Value = r.ActivityCode;
                                    workSheet.Cell(i, 29).Value = r.Unit;
                                    workSheet.Cell(i, 30).Value = r.Dimention;
                                    workSheet.Cell(i, 33).Value = r.ADP;
                                    workSheet.Cell(i, 34).Value = r.CDR;
                                    workSheet.Cell(i, 35).Value = r.Pr;
                                    workSheet.Cell(i, 36).Value = r.WI;
                                    workSheet.Cell(i, 37).Value = r.WS;
                                    workSheet.Cell(i, 38).Value = r.WTC;
                                    workSheet.Cell(i, 39).Value = r.WC;
                                    workSheet.Cell(i, 40).Value = r.PS;
                                    workSheet.Cell(i, 41).Value = r.WIS;
                                    workSheet.Cell(i, 42).Value = r.RT;
                                    workSheet.Cell(i, 43).Value = r.Remarks;

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

        public async Task<IEnumerable<object>> GetActiveRefIDs(string activityCode, string roadCode, int fromCHKM, string fromCHM, int toCHKM, string toCHM)
        {
            return await _repoUnit.FormADtlRepository.GetActiveRefIDs(activityCode,roadCode, fromCHKM,fromCHM,toCHKM,toCHM);
        }
    }
}
