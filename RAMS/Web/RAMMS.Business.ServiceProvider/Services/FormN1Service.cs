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
    public class FormN1Service : IFormN1Service
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly IProcessService processService;
        public FormN1Service(IRepositoryUnit repoUnit, IMapper mapper, IProcessService proService)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            processService = proService;
        }

        public async Task<bool> CheckHdrRefereceId(string id)
        {
            return await _repoUnit.FormN1Repository.CheckHdrRefereceId(id);
        }
        public async Task<bool> CheckHdrRefereceNo(string refNo)
        {
            return await _repoUnit.FormN1Repository.CheckHdrRefereceNo(refNo);
        }
        public async Task<int> DeActivateFormN1Async(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormD = await _repoUnit.FormN1Repository.GetByIdAsync(formNo);
                domainModelFormD.FnihActiveYn = false;
                _repoUnit.FormN1Repository.Update(domainModelFormD);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<PagingResult<FormN1HeaderRequestDTO>> GetFilteredFormN1Grid(FilteredPagingDefinition<FormN1SearchGridDTO> filterOptions)
        {
            PagingResult<FormN1HeaderRequestDTO> result = new PagingResult<FormN1HeaderRequestDTO>();

            List<FormN1HeaderRequestDTO> formDList = new List<FormN1HeaderRequestDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormN1Repository.GetFilteredRecordList(filterOptions);

                result.TotalRecords = await _repoUnit.FormN1Repository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    formDList.Add(_mapper.Map<FormN1HeaderRequestDTO>(listData));
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


        public async Task<int> SaveFormN1Async(FormN1HeaderRequestDTO formN1HeaderBO)
        {
            FormN1HeaderRequestDTO formDRequest;
            try
            {
                var domainModelForm = _mapper.Map<RmFormN1Hdr>(formN1HeaderBO);
                domainModelForm.FnihStatus = Common.StatusList.N1Init;
                var entity = _repoUnit.FormN1Repository.CreateReturnEntity(domainModelForm);
                if (entity.FnihSubmitSts)
                {
                    await processService.Save(new ProcessDTO()
                    {
                        ApproveDate = DateTime.Now,
                        Form="FormN1",
                        IsApprove = true,
                        RefId = entity.FnihPkRefNo,
                        Remarks="",
                        Stage = entity.FnihStatus,                        
                    });

                }
                formDRequest = _mapper.Map<FormN1HeaderRequestDTO>(entity);


            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return int.Parse(formDRequest.No.ToString());
        }

        public async Task<FormN1HeaderRequestDTO> SaveHeaderwithResponse(FormN1HeaderRequestDTO headerReq)
        {
            FormN1HeaderRequestDTO FormD;

            int refCheckCount = await _repoUnit.FormN1Repository.CheckwithRef(headerReq);
            if (refCheckCount != 0)
            {
                headerReq.No = refCheckCount;
                var currentFormD = await _repoUnit.FormN1Repository.GetFormWithDetailsByNoAsync(Convert.ToInt32(headerReq.No)).ConfigureAwait(false);
                FormD = _mapper.Map<FormN1HeaderRequestDTO>(currentFormD);
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

        public async Task<int> UpdateFormN1Async(FormN1HeaderRequestDTO fornmNilDTO)
        {
            int rowsAffected;
            try
            {
                var domainModelformD = _mapper.Map<RmFormN1Hdr>(fornmNilDTO);
                domainModelformD.FnihStatus = Common.StatusList.N1Init;
                _repoUnit.FormN1Repository.Update(domainModelformD);
                rowsAffected = await _repoUnit.CommitAsync();
                if (domainModelformD.FnihSubmitSts)
                {
                    await processService.Save(new ProcessDTO()
                    {
                        ApproveDate = DateTime.Now,
                        Form = "FormN1",
                        IsApprove = true,
                        RefId = domainModelformD.FnihPkRefNo,
                        Remarks = "",
                        Stage = domainModelformD.FnihStatus,
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

        public async Task<FormN1HeaderRequestDTO> GetFormN1WithDetailsByNoAsync(int formNo)
        {
            FormN1HeaderRequestDTO formN;
            try
            {
                var currentFormD = await _repoUnit.FormN1Repository.GetFormWithDetailsByNoAsync(formNo).ConfigureAwait(false);
                formN = _mapper.Map<FormN1HeaderRequestDTO>(currentFormD);

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
            return formN;
        }

        public (int id, bool isExists) CheckExistence(string rdCode, int month, int year)
        {
            return _repoUnit.FormN1Repository.CheckExistence(rdCode, month, year);
        }

        public async Task<IEnumerable<SelectListItem>> GetRoadCodesByRMU(string rmu)
        {
            try
            {
                var codes = await _repoUnit.FormN1Repository.GetRoadCodesByRMU(rmu);

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

        public async Task<IEnumerable<SelectListItem>> GetSectionCodesByRMU(string rmu)
        {
            try
            {
                var codes = await _repoUnit.FormN1Repository.GetRoadCodesByRMU(rmu);

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

        public async Task<IEnumerable<SelectListItem>> GetFormS1ReferenceId(string rodeCode)
        {
            try
            {
                var codes = await _repoUnit.FormN1Repository.GetFormS1ReferenceId(rodeCode);

                return codes.OrderBy(s => s.FsidPkRefNo).Select(s => new SelectListItem
                {
                    Value = s.FsidPkRefNo.ToString(),
                    Text = s.FsidRefId,
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<RmFormS1Hdr> GetFormS1Data(int id)
        {
            try
            {
                return await _repoUnit.FormN1Repository.GetFormS1Data(id);
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<FormQa2HeaderResponseDTO> GetFormQa2Data(int id)
        {
            return await _repoUnit.FormN1Repository.GetFormQa2Data(id);
        }

        public async Task<IEnumerable<SelectListItem>> GetFormQA2ReferenceId(string rodeCode)
        {
            try
            {
                var codes = await _repoUnit.FormN1Repository.GetFormQA2ReferenceId(rodeCode);

                return codes.Select(s => new SelectListItem
                {
                    Value = s.FqaiidPkRefNo.ToString(),
                    Text = Convert.ToString(s.FqaiidRefId) == "" ? "No Refrence ID" : Convert.ToString(s.FqaiidRefId),
                }).ToArray();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public async Task<int> GetMaxCount()
        {
            try
            {
                return await _repoUnit.FormN1Repository.GetMaxCount();
            }
            catch (Exception Ex)
            {
                await _repoUnit.RollbackAsync();
                throw Ex;
            }
        }

        public FORMN1Rpt GetReportData(int headerId, int pageIndex, int pageCount)
        {
            return _repoUnit.FormN1Repository.GetReportData(headerId, pageIndex, pageCount);
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
                FORMN1Rpt rpt = this.GetReportData(id, 0, 10);
                System.IO.File.Copy(oldFileName, cacheFile, true);
                using (var workBook = new XLWorkbook(cacheFile))
                {

                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    if (workSheet != null)
                    {
                        workSheet.Cell(8, 4).Value = rpt.NCNNo;
                        workSheet.Cell(10, 4).Value = rpt.IssueDate;
                        workSheet.Cell(8, 12).Value = rpt.Division;
                        workSheet.Cell(10, 12).Value = rpt.RMU;
                        workSheet.Cell(13, 4).Value = rpt.ServiceProvider;
                        workSheet.Cell(14, 4).Value = rpt.Attn;
                        workSheet.Cell(15, 4).Value = rpt.CC;
                        workSheet.Cell(21, 4).Value = rpt.RoadCode;
                        workSheet.Cell(21, 9).Value = rpt.RoadName;
                        workSheet.Cell(21, 14).Value = rpt.Chainage;
                        workSheet.Cell(23, 2).Value = rpt.DescriptionOfNC;
                        workSheet.Cell(26, 6).Value = rpt.CorrectToBeTakenBefore;
                        workSheet.Cell(30, 3).Value = rpt.IssuedBy;
                        workSheet.Cell(30, 10).Value = rpt.ReceivedBy;
                        workSheet.Cell(35, 6).Value = rpt.ReworkSpecification;
                        workSheet.Cell(42, 3).Value = rpt.CompletedBy;
                        workSheet.Cell(42, 10).Value = rpt.AcceptedBy;
                        workSheet.Cell(44, 3).Value = rpt.CompletedDate;
                        workSheet.Cell(44, 10).Value = rpt.AcceptedDate;
                        workSheet.Cell(50, 6).Value = rpt.IsCorrectionTaken;
                        workSheet.Cell(51, 6).Value = rpt.IsNCRIssue;
                        workSheet.Cell(52, 6).Value = rpt.NCRIssueDate;
                        workSheet.Cell(54, 5).Value = rpt.WarningLetter;
                        workSheet.Cell(56, 5).Value = rpt.Deduction;
                        workSheet.Cell(54, 9).Value = rpt.Penalty;
                        workSheet.Cell(50, 11).Value = rpt.Remarks;
                        workSheet.Cell(56, 12).Value = rpt.VerifiedBy;

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

