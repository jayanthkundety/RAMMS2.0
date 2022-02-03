using AutoMapper;
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
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using RAMMS.DTO.Report;
using ClosedXML.Excel;
using System.IO;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormHService : IFormHService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormHService(IRepositoryUnit repoUnit, IMapper mapper, ISecurity security, IProcessService proService)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security ?? throw new ArgumentNullException(nameof(security));
            processService = proService;
        }
        public async Task<int> DeleteFormH(int formHId)
        {
            int rowsAffected;
            try
            {
                var formH = await _repoUnit.FormHRepository.GetByIdAsync(formHId);
                formH.FhhActiveYn = false;
                formH.FhhModDt = DateTime.Now;
                formH.FhhModBy = _security.UserID.ToString();
                _repoUnit.FormHRepository.Update(formH);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<FormHRequestDTO> SaveFormH(FormHRequestDTO formHDTO)
        {
            int rowsAffected;
            try
            {
                var domainModelFormH = _mapper.Map<RmFormHHdr>(formHDTO);
                domainModelFormH.FhhStatus = Common.StatusList.FormHInit;
                if (domainModelFormH.FhhPkRefNo != 0)
                {
                    domainModelFormH.FhhModDt = DateTime.Now;
                    domainModelFormH.FhhModBy = _security.UserID.ToString();
                    domainModelFormH.FhhActiveYn = domainModelFormH.FhhActiveYn.GetValueOrDefault(true);
                    _repoUnit.FormHRepository.Update(domainModelFormH);
                    rowsAffected = await _repoUnit.CommitAsync();
                }
                else
                {
                    domainModelFormH.FhhActiveYn = domainModelFormH.FhhActiveYn.GetValueOrDefault(true);
                    domainModelFormH.FhhModDt = DateTime.Now;
                    domainModelFormH.FhhModBy = _security.UserID.ToString();
                    domainModelFormH.FhhCrDt = DateTime.Now;
                    domainModelFormH.FhhCrBy = _security.UserID.ToString();
                    var result = _repoUnit.FormHRepository.CreateReturnEntity(domainModelFormH);
                    formHDTO = _mapper.Map<FormHRequestDTO>(result);
                }

                if (domainModelFormH.FhhFjdPkRefNo.GetValueOrDefault() > 0)
                {
                    _repoUnit.FormJRepository.SubmittedToFormH(domainModelFormH.FhhFjdPkRefNo.GetValueOrDefault());
                    _repoUnit.Commit();
                }
                else if (domainModelFormH.FhhFadPkRefNo.GetValueOrDefault() > 0)
                {
                    _repoUnit.FormADtlRepository.SubmitToFormH(domainModelFormH.FhhFadPkRefNo.GetValueOrDefault());
                    _repoUnit.Commit();
                }
                if (domainModelFormH.FhhSubmitSts == true)
                {
                    int iResult = processService.Save(new ProcessDTO()
                    {
                        ApproveDate = DateTime.Now,
                        Form = "FormH",
                        IsApprove = true,
                        RefId = domainModelFormH.FhhPkRefNo,
                        Remarks = "",
                        Stage = domainModelFormH.FhhStatus
                    }).Result;

                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return formHDTO;
        }

        public async Task<PagingResult<FormHResponseDTO>> GetFilteredFormHGrid(FilteredPagingDefinition<FormHSearchDTO> filterOptions)
        {
            PagingResult<FormHResponseDTO> result = new PagingResult<FormHResponseDTO>();

            List<FormHResponseDTO> formHList = new List<FormHResponseDTO>();
            try
            {
                var filteredRecords = await _repoUnit.FormHRepository.GetFilteredRecordList(filterOptions).ConfigureAwait(false);

                result.TotalRecords = await _repoUnit.FormHRepository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                foreach (var listData in filteredRecords)
                {
                    var formH = _mapper.Map<FormHResponseDTO>(listData);
                    formH.ProcessStatus = listData.FhhStatus;

                    var ddl = _repoUnit.DDLookUpRepository.FindBy(s => s.DdlType == "RMU" && s.DdlTypeCode == formH.Rmu).Select(x => x.DdlTypeDesc).FirstOrDefault();

                    if (ddl != null)
                    {
                        formH.RmuName = ddl;
                    }

                    var sec = _repoUnit.DDLookUpRepository.FindBy(s => s.DdlType == "Section Code" && s.DdlTypeDesc == formH.Section).Select(x => x.DdlTypeCode).FirstOrDefault();

                    if (sec != null)
                    {
                        formH.SectionCode = sec;
                    }

                    formHList.Add(formH);
                }

                result.PageResult = formHList;

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

        public async Task<FormHRequestDTO> GetByID(int formHId)
        {
            FormHRequestDTO FormH;
            try
            {
                var formDetail = await _repoUnit.FormHRepository.GetByIdAsync(formHId).ConfigureAwait(false);
                FormH = _mapper.Map<FormHRequestDTO>(formDetail);
                return FormH;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public async Task<int> SaveFormHImage(List<FormHImageRequestDTO> formHImageList)
        {
            List<RmFormhImageDtl> imageDtl = new List<RmFormhImageDtl>();
            int rowsAffected;
            try
            {
                foreach (var listItem in formHImageList)
                {
                    listItem.ActiveYn = listItem.ActiveYn.GetValueOrDefault(true);
                    imageDtl.Add(_mapper.Map<RmFormhImageDtl>(formHImageList));
                }
                _repoUnit.FormHRepository.SaveImageList(imageDtl);

                rowsAffected = await _repoUnit.CommitAsync();

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<int> DeleteFormHImage(int imageId)
        {
            int rowsAffected;
            try
            {
                var formHImageDtl = await _repoUnit.FormHRepository.GetFormHImageByIdAsync(imageId);
                formHImageDtl.FhiActiveYn = false;

                _repoUnit.FormHRepository.UpdateImage(formHImageDtl);

                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }

        public async Task<List<FormHImageResponseDTO>> GetFormHImageList(int formHeaderId)
        {
            List<FormHImageResponseDTO> formHImages = new List<FormHImageResponseDTO>();
            try
            {
                var imageList = await _repoUnit.FormHRepository.GetFormHImageListAsync(formHeaderId);

                foreach (var listItem in imageList)
                {
                    formHImages.Add(_mapper.Map<FormHImageResponseDTO>(imageList));
                }

                return formHImages;

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public async Task<int> GetImageSrNo(int headerId)
        {
            int imageCt = await _repoUnit.FormHRepository.GetSrNo(headerId);
            return imageCt;
        }

        public List<SelectListItem> GetReferenceNoByFormType(RequestFormReference lookUp)
        {
            return _repoUnit.FormHRepository.GetReferenceNoByFormType(lookUp);
        }

        public async Task<FormHRequestDTO> GetByReferenceID(string id)
        {
            FormHRequestDTO FormH;
            try
            {
                RmFormHHdr formDetail = await _repoUnit.FormHRepository.GetByReferenceID(id).ConfigureAwait(false);
                FormH = _mapper.Map<FormHRequestDTO>(formDetail);
                return FormH;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public FORMHRpt GetReportData(int headerId, int pageIndex, int pageCount)
        {
            return _repoUnit.FormHRepository.GetReportData(headerId, pageIndex, pageCount);
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
                FORMHRpt rpt = this.GetReportData(id, 0, 10);
                System.IO.File.Copy(oldFileName, cacheFile, true);
                using (var workBook = new XLWorkbook(cacheFile))
                {

                    IXLWorksheet workSheet = workBook.Worksheet(1);

                    if (workSheet != null)
                    {
                        workSheet.Cell(7, 4).Value = rpt.InspectionDate;
                        workSheet.Cell(8, 4).Value = rpt.RoadCode;
                        workSheet.Cell(9, 4).Value = rpt.RoadName;
                        workSheet.Cell(10, 4).Value = rpt.Chainage;
                        workSheet.Cell(8, 9).Value = rpt.RMU;
                        workSheet.Cell(7, 14).Value = rpt.ReferenceNumber;
                        workSheet.Cell(8, 14).Value = rpt.Division;
                        workSheet.Cell(15, 2).Value = rpt.DamageDetail;
                        workSheet.Cell(23, 2).Value = rpt.DamageCausedBy;
                        workSheet.Cell(29, 2).Value = rpt.GeneralComments;
                        workSheet.Cell(51, 4).Value = rpt.ReportNumber;
                        workSheet.Cell(52, 4).Value = rpt.Recommendation;
                        workSheet.Cell(36, 4).Value = rpt.ReportedByName;
                        workSheet.Cell(37, 4).Value = rpt.ReportedByDesignation;
                        workSheet.Cell(38, 4).Value = rpt.ReportedByDate;
                        workSheet.Cell(36, 10).Value = rpt.VerifiedBy;
                        workSheet.Cell(37, 10).Value = rpt.VerifiedByDesignation;
                        workSheet.Cell(38, 10).Value = rpt.VerifiedByDate;
                        workSheet.Cell(44, 2).Value = rpt.Remarks;
                        workSheet.Cell(53, 12).Value = rpt.ReceivedBy;
                        workSheet.Cell(54, 13).Value = rpt.ReceivedByDesignation;
                        workSheet.Cell(57, 12).Value = rpt.VettedBy;
                        workSheet.Cell(58, 13).Value = rpt.VettedByDesignation;

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

        public async Task<int> GetLastInsertedHeader()
        {
            return await _repoUnit.FormHRepository.GetLastInsertedHeader();
        }

        public (int id, bool alreadyExists) CheckAlreadyExists(FormType form, string roadCode, DateTime inspectionDate, string assetGroup, int locationFrom, int locationTo, int sourceRefNo)
        {
            return  _repoUnit.FormHRepository.CheckAlreadyExists(form,roadCode, inspectionDate, assetGroup, locationFrom, locationTo, sourceRefNo);
        }
    }
}
