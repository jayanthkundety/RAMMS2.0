using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.RequestBO;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RAMMS.Domain.Models;
using RAMMS.DTO.Wrappers;
using RAMMS.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using RAMMS.DTO.Report;
using ClosedXML.Excel;
using System.IO;
using RAMMS.DTO.ResponseBO;

namespace RAMMS.Business.ServiceProvider.Services
{

    public class FormQa2Service : IFormQa2Service
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;

        public FormQa2Service(IRepositoryUnit repoUnit, IMapper mapper)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> CheckHdrRefereceId(string id)
        {
            return await _repoUnit.FormN2Repository.CheckHdrRefereceId(id);
        }


        public async Task<int> DeActivateFormQA2Async(int hdrFormNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormQa2 = await _repoUnit.FormQa2Repository.GetByIdAsync(hdrFormNo);
                domainModelFormQa2.FqaiihActiveYn = false;
                _repoUnit.FormQa2Repository.Update(domainModelFormQa2);

                var dtlList = await _repoUnit.FormQa2DtlRepository.GetAllDtlById(domainModelFormQa2.FqaiihPkRefNo);
                var dtl = _mapper.Map<List<RmFormQa2Dtl>>(dtlList);
                foreach (var list in dtl)
                {
                    list.FqaiidActiveYn = false;
                    _repoUnit.FormQa2DtlRepository.Update(list);
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

        public async Task<int> DeActivateFormQA2DtlAsync(int dtlFormNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormQa2 = await _repoUnit.FormQa2DtlRepository.GetByIdAsync(dtlFormNo);
                domainModelFormQa2.FqaiidActiveYn = false;
                _repoUnit.FormQa2DtlRepository.Update(domainModelFormQa2);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<PagingResult<FormQa2DtlRequestDTO>> GetFilteredFormQa2Grid(FilteredPagingDefinition<FormQa2SearchGridDTO> filterOptions)
        {
            PagingResult<FormQa2DtlRequestDTO> result = new PagingResult<FormQa2DtlRequestDTO>();

            List<FormQa2DtlRequestDTO> formList = new List<FormQa2DtlRequestDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormQa2Repository.GetFilteredRecordList(filterOptions);

                result.TotalRecords = await _repoUnit.FormQa2Repository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    formList.Add(_mapper.Map<FormQa2DtlRequestDTO>(listData));
                }

                result.PageResult = formList;

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

        public async Task<PagingResult<FormQa2DtlRequestDTO>> GetFilteredFormQa2DetailsGrid(FilteredPagingDefinition<FormQa2SearchGridDTO> filterOptions)
        {
            PagingResult<FormQa2DtlRequestDTO> result = new PagingResult<FormQa2DtlRequestDTO>();

            List<FormQa2DtlRequestDTO> formList = new List<FormQa2DtlRequestDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormQa2DtlRepository.GetFilteredRecordList(filterOptions, filterOptions.Filters.HeaderNo);

                result.TotalRecords = await _repoUnit.FormQa2DtlRepository.GetFilteredRecordCount(filterOptions, filterOptions.Filters.HeaderNo).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    formList.Add(_mapper.Map<FormQa2DtlRequestDTO>(listData));
                }

                result.PageResult = formList;

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

        public async Task<IEnumerable<SelectListItem>> GetFormN1ReferenceId(string rodeCode)
        {
            try
            {
                var codes = await _repoUnit.FormN2Repository.GetFormN1ReferenceId(true);

                return codes.OrderBy(s => s.FnihPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.FnihPkRefNo.ToString(),
                    Text = Convert.ToString(s.FnihPkRefNo) == "" ? "No Refrence ID" : Convert.ToString(s.FnihRefId),
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetRMU()
        {
            try
            {
                var codes = await _repoUnit.FormDRepository.GetRMU();

                return codes.OrderBy(s => s.DdlPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.DdlTypeCode.ToString(),
                    Text = s.DdlTypeCode + "-" + s.DdlTypeValue.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }


        public async Task<IEnumerable<SelectListItem>> GetRoadCodeList()
        {
            try
            {
                var codes = await _repoUnit.FormDRepository.GetRoadCodes();

                return codes.OrderBy(s => s.RdmPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.RdmRdCode.ToString(),
                    Text = s.RdmRdCode + "-" + s.RdmRmuName.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetRoadCodesByRMU(string rmu)
        {
            try
            {
                var codes = await _repoUnit.FormQa2Repository.GetRoadCodesByRMU(rmu);

                return codes.OrderBy(s => s.RdmPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.RdmRdCode.ToString(),
                    Text = s.RdmRdCode + "-" + s.RdmRdName.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetSectionCode()
        {
            try
            {
                var codes = await _repoUnit.FormQa2Repository.GetSectionCode();

                return codes.OrderBy(s => s.DdlPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.DdlTypeCode.ToString(),
                    Text = s.DdlTypeCode + "-" + s.DdlTypeValue.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetSectionCodesByRMU(string rmu)
        {
            try
            {
                var codes = await _repoUnit.FormQa2Repository.GetRoadCodesByRMU(rmu);

                return codes.OrderBy(s => s.RdmPkRefNo).Select(s => new SelectListItem
                {

                    Value = s.RdmSecCode.ToString(),
                    Text = s.RdmSecCode + "-" + s.RdmSecName.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<FormQa2HeaderRequestDTO> SaveFormQa2Hdr(FormQa2HeaderRequestDTO formDetailBO)
        {
            try
            {
                var domainModelForm = _mapper.Map<RmFormQa2Hdr>(formDetailBO);

                var result= await _repoUnit.FormQa2Repository.SaveFormQa2Hdr(domainModelForm);
                return _mapper.Map<FormQa2HeaderRequestDTO>(result);
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public async Task<int> SaveFormQa2Detail(FormQa2DtlRequestDTO formDetailBO)
        {
            FormQa2DtlRequestDTO formRequest;
            try
            {
                var domainModelForm = _mapper.Map<RmFormQa2Dtl>(formDetailBO);

                if (formDetailBO.No != 0)
                {
                    domainModelForm.FqaiidActiveYn = true;
                    _repoUnit.FormQa2DtlRepository.Update(domainModelForm);
                    await _repoUnit.CommitAsync();
                    formRequest = _mapper.Map<FormQa2DtlRequestDTO>(domainModelForm);
                }
                else
                {
                    var entity = _repoUnit.FormQa2DtlRepository.CreateReturnEntity(domainModelForm);
                    formRequest = _mapper.Map<FormQa2DtlRequestDTO>(entity);
                }
                return int.Parse(formRequest.No.ToString());

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public async Task<FormQa2HeaderRequestDTO> SaveHeaderWithResponse(FormQa2HeaderRequestDTO headerReq)
        {
            FormQa2HeaderRequestDTO Form;

            int refCheckCount = await _repoUnit.FormQa2Repository.CheckwithRef(headerReq);
            if (refCheckCount != 0)
            {
                headerReq.No = refCheckCount;
                var currentForm = await _repoUnit.FormN2Repository.GetFormWithDetailsByNoAsync(Convert.ToInt32(headerReq.No)).ConfigureAwait(false);
                Form = _mapper.Map<FormQa2HeaderRequestDTO>(currentForm);
                return Form;

            }
            else
            {
                var domainModelForm = _mapper.Map<RmFormQa2Hdr>(headerReq);
                _repoUnit.FormQa2Repository.Create(domainModelForm);
                await _repoUnit.CommitAsync();
                return null;
            }
        }

        public async Task<FormQa2DtlRequestDTO> SaveDetailsWithResponse(FormQa2DtlRequestDTO headerReq)
        {
            FormQa2DtlRequestDTO form;

            int refCheckCount = await _repoUnit.FormQa2DtlRepository.CheckwithRef(headerReq);
            if (refCheckCount != 0)
            {
                headerReq.No = refCheckCount;
                var currentForm = await _repoUnit.FormQa2Repository.GetFormWithDetailsByNoAsync(Convert.ToInt32(headerReq.No)).ConfigureAwait(false);
                form = _mapper.Map<FormQa2DtlRequestDTO>(currentForm);
                return form;

            }
            else
            {
                var domainModelForm = _mapper.Map<RmFormQa2Hdr>(headerReq);
                _repoUnit.FormQa2Repository.Create(domainModelForm);
                await _repoUnit.CommitAsync();
                return null;
            }
        }

        public async Task<int> UpdateFormN2Async(FormN2HeaderRequestDTO formniiDTO)
        {
            int rowsAffected;
            try
            {
                var domainModelformD = _mapper.Map<RmFormN2Hdr>(formniiDTO);
                _repoUnit.FormN2Repository.Update(domainModelformD);

                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> GetMaxCount()
        {
            try
            {
                return await _repoUnit.FormN2Repository.GetMaxCount();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
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
                FORMQA2Rpt rpt = this.GetReportData(id);
                System.IO.File.Copy(oldFileName, cacheFile, true);
                using (var workBook = new XLWorkbook(cacheFile))
                {
                    int noOfSheets = (rpt.Detail.Count() / 6) + ((rpt.Detail.Count() % 6) > 0 ? 1 : 1);
                    for (int sheet = 2; sheet <= noOfSheets; sheet++)
                    {
                        using (var tempWorkBook = new XLWorkbook(cacheFile))
                        {
                            string sheetName = "sheet" + Convert.ToString(sheet);
                            IXLWorksheet copySheet = tempWorkBook.Worksheet(1);
                            copySheet.Worksheet.Name = sheetName;
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
                            int i = 8;
                            workSheet.Cell(1, 3).Value = rpt.Header.RMU;
                            workSheet.Cell(5, 4).Value = rpt.Header.RoadCode;
                            workSheet.Cell(5, 8).Value = rpt.Header.RoadName;
                            workSheet.Cell(5, 16).Value = rpt.Header.CrewSupervisor;
                            workSheet.Cell(3, 17).Value = rpt.Header.ReferenceNo;
                            workSheet.Cell(3, 19).Value = sheet;
                            workSheet.Cell(39, 13).Value = rpt.Header.InitialConSignDate;
                            workSheet.Cell(39, 17).Value = rpt.Header.InitialRemarks;
                            workSheet.Cell(40, 13).Value = rpt.Header.InitialConName;
                            workSheet.Cell(41, 13).Value = rpt.Header.InitialConDesignation;
                            workSheet.Cell(42, 13).Value = rpt.Header.ISignDate;
                            workSheet.Cell(42, 17).Value = rpt.Header.IRemarks;
                            workSheet.Cell(43, 13).Value = rpt.Header.IName;
                            workSheet.Cell(44, 13).Value = rpt.Header.IDesignation;
                            workSheet.Cell(45, 13).Value = rpt.Header.IISignDate;
                            workSheet.Cell(45, 17).Value = rpt.Header.IIRemarks;
                            workSheet.Cell(46, 13).Value = rpt.Header.IIName;
                            workSheet.Cell(47, 13).Value = rpt.Header.IIDesignation;
                            workSheet.Cell(48, 13).Value = rpt.Header.IIISignDate;
                            workSheet.Cell(48, 17).Value = rpt.Header.IIIRemarks;
                            workSheet.Cell(49, 13).Value = rpt.Header.IIIName;
                            workSheet.Cell(50, 13).Value = rpt.Header.IIIDesignation;
                            workSheet.Cell(51, 13).Value = rpt.Header.IVSignDate;
                            workSheet.Cell(51, 17).Value = rpt.Header.IVRemarks;
                            workSheet.Cell(52, 13).Value = rpt.Header.IVName;
                            workSheet.Cell(53, 13).Value = rpt.Header.IVDesignation;
                            var data = rpt.Detail.AsEnumerable().Skip((sheet - 1) * 6).Take(6);
                            int j = 1;
                            foreach (var r in data)
                            {
                                workSheet.Cell(i, 2).Value = j;
                                workSheet.Cell(i, 3).Value = r.DateI;
                                workSheet.Cell(i + 1, 3).Value = r.DateII;
                                workSheet.Cell(i + 2, 3).Value = r.DateIII;
                                workSheet.Cell(i + 3, 3).Value = r.DateIV;
                                workSheet.Cell(i + 4, 3).Value = r.DateV;
                                workSheet.Cell(i, 4).Value = r.SiteRef;
                                workSheet.Cell(i, 5).Value = r.LocationFrom;
                                workSheet.Cell(i, 6).Value = r.LocationTo;
                                workSheet.Cell(i, 7).Value = r.Defect;
                                workSheet.Cell(i, 8).Value = r.WorkActivity;
                                workSheet.Cell(i, 9).Value = r.InitCondRating;
                                workSheet.Cell(i, 10).Value = r.IRating;
                                workSheet.Cell(i, 11).Value = r.IIRating;
                                workSheet.Cell(i, 12).Value = r.IIIRating;
                                workSheet.Cell(i, 13).Value = r.IVRating;
                                workSheet.Cell(i, 14).Value = r.DefectDescription;
                                workSheet.Cell(i, 15).Value = (r.DimLen.HasValue ? r.DimLen : 0) + "*" + (r.DimWid.HasValue ? r.DimWid : 0) + "*" + (r.DimHeight.HasValue ? r.DimHeight : 0);
                                workSheet.Cell(i, 17).Value = r.WWS;
                                workSheet.Cell(i, 18).Value = r.RemarksComments;
                                j++;
                                i += 5;
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


        public async Task<FormQa2HeaderRequestDTO> GetRmFormQa2Hdr(int id)
        {
            var rmuQa2Hdr = await _repoUnit.FormQa2Repository.GetFormWithDetailsByNoAsync(id);
            if (rmuQa2Hdr != null)
            {
                var dd = _mapper.Map<FormQa2HeaderRequestDTO>(rmuQa2Hdr);
                return dd;
            }
            else
            {
                return null;
            }
        }

        public async Task<FormQa2DtlRequestDTO> GetFormWithDetailsByNoAsync(int formNo)
        {
            FormQa2DtlRequestDTO form;
            try
            {
                var currentForm = await _repoUnit.FormQa2DtlRepository.GetFormWithDetailsByNoAsync(formNo).ConfigureAwait(false);
                form = _mapper.Map<FormQa2DtlRequestDTO>(currentForm);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return form;
        }

        public async Task<(int id, bool aleadyExists)> CheckExistence(string rdCode, string rmu, string month, string year)
        {
            return await _repoUnit.FormQa2Repository.CheckExistence(rdCode, rmu, month, year);
        }

        public async Task<int> LastInsertedRecord()
        {
            return await _repoUnit.FormQa2Repository.LastInsertedRecord();
        }

        public async Task<FormQa2HeaderRequestDTO> GetHeaderWithDetailById(int hdrNo)
        {
            try
            {
                var hdrvalue = await _repoUnit.FormQa2Repository.GetFormQa2WithDetailsAsync(hdrNo);
                FormQa2HeaderRequestDTO response = _mapper.Map<FormQa2HeaderRequestDTO>(hdrNo);

                var dtl = _mapper.Map<FormQa2DtlRequestDTO>(hdrvalue.RmFormQa2Dtl);
                response.FormQa2Detail.Add(dtl);

                return response;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }
        public async Task<int> RemoveDetail(int id)
        {
            return await _repoUnit.FormQa2DtlRepository.Remove(id);
        }

        public FORMQA2Rpt GetReportData(int headerId)
        {
            return _repoUnit.FormQa2Repository.GetReportData(headerId);

        }

        public async Task<int?> DtlSrNo(int headerNo)
        {
            return await _repoUnit.FormQa2DtlRepository.GetSrNo(headerNo);
        }

        public async Task<FormQa2HeaderResponseDTO> UpdateQa2Hdr(FormQa2HeaderRequestDTO requestDTO)
        {
            var hdr = _mapper.Map<RmFormQa2Hdr>(requestDTO);

            hdr.FqaiihActiveYn = true;

            _repoUnit.FormQa2Repository.Update(hdr);


            if (hdr.FqaiihSubmitSts)
            {
                var dtlList = await _repoUnit.FormQa2DtlRepository.GetAllDtlById(hdr.FqaiihPkRefNo);
                var dtl = _mapper.Map<List<RmFormQa2Dtl>>(dtlList);
                foreach (var list in dtl)
                {
                    list.FqaiidSubmitSts = true;
                    _repoUnit.FormQa2DtlRepository.Update(list);
                }
            }

            await _repoUnit.CommitAsync();

            var response = _mapper.Map<FormQa2HeaderResponseDTO>(hdr);
            return response;
        }

        public async Task<string> GetFormADetailByIdAsync(int detailId)
        {
            var formADel = await _repoUnit.FormADtlRepository.GetByIdAsync(detailId);
            var desCode = formADel.FadDefCode;
            return desCode;
        }
    }
}

