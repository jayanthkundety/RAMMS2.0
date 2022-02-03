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

namespace RAMMS.Business.ServiceProvider.Services
{

    public class FormN2Service : IFormN2Service
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly IProcessService processService;
        public FormN2Service(IRepositoryUnit repoUnit, IMapper mapper, IProcessService proService)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            processService = proService;
        }

        public (int id, bool isExists) CheckExistence(string rdCode, int month, int year)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CheckHdrRefereceId(string id)
        {
            return await _repoUnit.FormN2Repository.CheckHdrRefereceId(id);
        }

        public async Task<int> DeActivateFormN2Async(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormD = await _repoUnit.FormN2Repository.GetByIdAsync(formNo);
                domainModelFormD.FnihActiveYn = false;
                _repoUnit.FormN2Repository.Update(domainModelFormD);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<PagingResult<FormN2HeaderRequestDTO>> GetFilteredFormN2Grid(FilteredPagingDefinition<FormN2SearchGridDTO> filterOptions)
        {
            PagingResult<FormN2HeaderRequestDTO> result = new PagingResult<FormN2HeaderRequestDTO>();

            List<FormN2HeaderRequestDTO> formDList = new List<FormN2HeaderRequestDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormN2Repository.GetFilteredRecordList(filterOptions);

                result.TotalRecords = await _repoUnit.FormN2Repository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    formDList.Add(_mapper.Map<FormN2HeaderRequestDTO>(listData));
                }

                result.PageResult = formDList;

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

        public async Task<FormN2HeaderRequestDTO> GetFormN2WithDetailsByNoAsync(int formNo)
        {
            FormN2HeaderRequestDTO formN;
            try
            {
                var currentFormD = await _repoUnit.FormN2Repository.GetFormWithDetailsByNoAsync(formNo).ConfigureAwait(false);
                formN = _mapper.Map<FormN2HeaderRequestDTO>(currentFormD);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return formN;
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
                var codes = await _repoUnit.FormN2Repository.GetRoadCodesByRMU(rmu);

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
                var codes = await _repoUnit.FormDRepository.GetSectionCode();

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
                var codes = await _repoUnit.FormN2Repository.GetRoadCodesByRMU(rmu);

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

        public async Task<int> SaveFormN2Async(FormN2HeaderRequestDTO formN2HeaderBO)
        {
            FormN2HeaderRequestDTO formDRequest;
            try
            {                
                var domainModelForm = _mapper.Map<RmFormN2Hdr>(formN2HeaderBO);
                domainModelForm.FnthStatus = Common.StatusList.N2Init;
                var entity = _repoUnit.FormN2Repository.CreateReturnEntity(domainModelForm);
                if (entity.FnihSubmitSts)
                {
                    await processService.Save(new ProcessDTO()
                    {
                        ApproveDate = DateTime.Now,
                        Form = "FormN2",
                        IsApprove = true,
                        RefId = entity.FnthPkRefNo,
                        Remarks = "",
                        Stage = entity.FnthStatus,
                    });

                }
                formDRequest = _mapper.Map<FormN2HeaderRequestDTO>(entity);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return int.Parse(formDRequest.No.ToString());
        }

        public async Task<FormN2HeaderRequestDTO> SaveHeaderWithResponse(FormN2HeaderRequestDTO headerReq)
        {
            FormN2HeaderRequestDTO FormD;

            int refCheckCount = await _repoUnit.FormN2Repository.CheckWithRef(headerReq);
            if (refCheckCount != 0)
            {
                headerReq.No = refCheckCount;
                var currentFormD = await _repoUnit.FormN2Repository.GetFormWithDetailsByNoAsync(Convert.ToInt32(headerReq.No)).ConfigureAwait(false);
                FormD = _mapper.Map<FormN2HeaderRequestDTO>(currentFormD);
                return FormD;

            }
            else
            {
                var domainModelFormD = _mapper.Map<RmFormDHdr>(headerReq);

                _repoUnit.FormDRepository.Create(domainModelFormD);

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
                domainModelformD.FnthStatus = Common.StatusList.N1Init;
                _repoUnit.FormN2Repository.Update(domainModelformD);
                rowsAffected = await _repoUnit.CommitAsync();
                if (domainModelformD.FnihSubmitSts)
                {
                    await processService.Save(new ProcessDTO()
                    {
                        ApproveDate = DateTime.Now,
                        Form = "FormN2",
                        IsApprove = true,
                        RefId = domainModelformD.FnthPkRefNo,
                        Remarks = "",
                        Stage = domainModelformD.FnthStatus,
                    });

                }

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

        public FORMN2Rpt GetReportData(int headerId, int pageIndex, int pageCount)
        {
            return _repoUnit.FormN2Repository.GetReportData(headerId, pageIndex, pageCount);
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
                FORMN2Rpt rpt = this.GetReportData(id, 0, 10);
                System.IO.File.Copy(oldFileName, cacheFile, true);
                using (var workBook = new XLWorkbook(cacheFile))
                {

                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    if (workSheet != null)
                    {
                        workSheet.Cell(7, 4).Value = rpt.ReferenceNo;
                        workSheet.Cell(9, 4).Value = rpt.NCRNo;
                        workSheet.Cell(11, 4).Value = rpt.IssueDate;
                        workSheet.Cell(7, 12).Value = rpt.Region;
                        workSheet.Cell(9, 12).Value = rpt.Division;
                        workSheet.Cell(11, 12).Value = rpt.RMU.ToUpper();
                        workSheet.Cell(14, 4).Value = rpt.ServiceProvider;
                        workSheet.Cell(15, 4).Value = rpt.Attn;
                        workSheet.Cell(16, 4).Value = rpt.CC;
                        workSheet.Cell(19, 4).Value = rpt.Subject;
                        workSheet.Cell(24, 2).Value = rpt.NonConferenceRemarks;
                        workSheet.Cell(29, 3).Value = rpt.IssuedBy;
                        workSheet.Cell(29, 10).Value = rpt.ReceivedBy;
                        workSheet.Cell(34, 5).Value = rpt.ProposedCorrectiveAction;
                        workSheet.Cell(41, 3).Value = rpt.CompletedBy;
                        workSheet.Cell(41, 10).Value = rpt.AcceptedBy;
                        workSheet.Cell(43, 3).Value = rpt.CompletedDate;
                        workSheet.Cell(43, 10).Value = rpt.AcceptedDate;
                        workSheet.Cell(45, 9).Value = rpt.ActionToBeTaken;
                        workSheet.Cell(51, 3).Value = rpt.ActionCompletedBy;
                        workSheet.Cell(51, 10).Value = rpt.ActionCompletedDate;
                        workSheet.Cell(58, 5).Value = rpt.ReIssueNCR;
                        workSheet.Cell(60, 5).Value = rpt.ReportInRTC_NTC;
                        workSheet.Cell(62, 5).Value = rpt.WarningLetter;
                        workSheet.Cell(54, 5).Value = rpt.CloseOut;
                        workSheet.Cell(58, 9).Value = rpt.CallMeeting;
                        workSheet.Cell(60, 9).Value = rpt.Penalty;
                        workSheet.Cell(62, 9).Value = rpt.Deduction;
                        workSheet.Cell(64, 7).Value = rpt.Date;
                        workSheet.Cell(64, 5).Value = rpt.Close;
                        workSheet.Cell(64, 12).Value = rpt.VerifiedBy;
                        workSheet.Cell(57, 11).Value = rpt.CloseOutRemarks;

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
    }
}

