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
using RAMMS.Common.RefNumber;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;

namespace RAMMS.Business.ServiceProvider.Services
{

    public class FormTService : IFormTService
    {
        private readonly IFormTRepository _repo;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormTService(IRepositoryUnit repoUnit, IFormTRepository repo, IMapper mapper, ISecurity security, IProcessService process)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
            processService = process;
            _repo = repo;
        }


        public async Task<PagingResult<FormTHeaderRequestDTO>> GetHeaderList(FilteredPagingDefinition<FormTSearchGridDTO> filterOptions)
        {
            PagingResult<FormTHeaderRequestDTO> result = new PagingResult<FormTHeaderRequestDTO>();
            List<FormTHeaderRequestDTO> formAlist = new List<FormTHeaderRequestDTO>();
            result.PageResult = await _repo.GetFilteredRecordList(filterOptions);
            result.TotalRecords = result.PageResult.Count();
            result.PageNo = filterOptions.StartPageNo;
            result.FilteredRecords = result.PageResult != null ? result.PageResult.Count : 0;
            return result;
        }

        public async Task<PagingResult<FormTDtlGridDTO>> GetDetailList(FilteredPagingDefinition<FormTDtlResponseDTO> filterOptions)
        {


            PagingResult<FormTDtlGridDTO> result = new PagingResult<FormTDtlGridDTO>();

            List<FormTDtlGridDTO> formTDtlList = new List<FormTDtlGridDTO>();
            try
            {
                var Records = await _repoUnit.FormTRepository.GetFormTDtlGridList(filterOptions);

                var list = Records.Skip(filterOptions.StartPageNo).Take(filterOptions.RecordsPerPage).ToList();

                result.TotalRecords = Records.Count();

                result.PageResult = list;

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


       

        public async Task<FormTResponseDTO> GetHeaderById(int id)
        {
            var header = await _repoUnit.FormTRepository.FindAsync(s => s.FmtPkRefNo == id && s.FmtActiveYn == true);
            if (header == null)
            {
                return null;
            }
            return _mapper.Map<FormTResponseDTO>(header);
        }

        public async Task<FormTDtlResponseDTO> GetFormTDtlById(int id)
        {
            RmFormTDailyInspection res = _repo.GetFormTDtlById(id);
            FormTDtlResponseDTO DI = new FormTDtlResponseDTO();
            DI = _mapper.Map<FormTDtlResponseDTO>(res);
            DI.Vechicles = _mapper.Map<List<FormTVehicleResponseDTO>>(res.RmFormTVechicle);
            return DI;
        }


        public async Task<FormTResponseDTO> SaveFormT(FormTResponseDTO FormT)
        {
            try
            {
                var domainModelFormT = _mapper.Map<RmFormTHdr>(FormT);
                domainModelFormT.FmtPkRefNo = 0;


                var obj = _repoUnit.FormTRepository.FindAsync(x => x.FmtRmuCode == domainModelFormT.FmtRmuCode && x.FmtRdCode == domainModelFormT.FmtRdCode && x.FmtInspectionDate == domainModelFormT.FmtInspectionDate && x.FmtActiveYn == true).Result;
                if (obj != null)
                {
                    var res = _mapper.Map<FormTResponseDTO>(obj);
                    res.FormExist = true;
                    return res;
                }



                IDictionary<string, string> lstData = new Dictionary<string, string>();
                lstData.Add("RoadCode", domainModelFormT.FmtRdCode);
                lstData.Add("YYYYMMDD", Utility.ToString(Convert.ToDateTime(domainModelFormT.FmtInspectionDate).ToString("yyyyMMdd")));
                domainModelFormT.FmtPkRefId = FormRefNumber.GetRefNumber(RAMMS.Common.RefNumber.FormType.FormTHeader, lstData);

                var entity = _repoUnit.FormTRepository.CreateReturnEntity(domainModelFormT);
                FormT.PkRefNo = _mapper.Map<FormTResponseDTO>(entity).PkRefNo;
                FormT.PkRefId = domainModelFormT.FmtPkRefId;
                FormT.Status = domainModelFormT.FmtStatus;


                return FormT;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public int? DeleteFormT(int id)
        {
            int? rowsAffected;
            try
            {
                rowsAffected = _repo.DeleteFormT(id);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }
        public int? DeleteFormTDtl(int Id)
        {
            try
            {
                return _repo.DeleteFormTDtl(Id);
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        public int? SaveFormTDtl(FormTDtlResponseDTO FormTDtl)
        {
            try
            {
                var model = _mapper.Map<RmFormTDailyInspection>(FormTDtl);
                model.FmtdiPkRefNo = 0;

                var veh = _mapper.Map<List<RmFormTVechicle>>(FormTDtl.Vechicles);
               
                return _repo.SaveFormTDtl(model,veh);
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }


        public int? UpdateFormTDtl(FormTDtlResponseDTO FormTDtl)
        {

            try
            {
                int? FmtPkRefNo = FormTDtl.FmtPkRefNo;
                int Ff1dPkRefNo = FormTDtl.PkRefNo;
                // int? G1PkRefNo = FormTDtl.Ff1dG1hPkRefNo;
                var model = _mapper.Map<RmFormTDailyInspection>(FormTDtl);
                model.FmtdiPkRefNo = Ff1dPkRefNo;
                //model.Ff1dFmtPkRefNo = FmtPkRefNo;
                //model.Ff1dG1hPkRefNo = G1PkRefNo;
                var veh = _mapper.Map<List<RmFormTVechicle>>(FormTDtl.Vechicles);
                return _repo.UpdateFormTDtl(model, veh);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }
        }



        public async Task<int> Update(FormTResponseDTO FormT)
        {
            int rowsAffected;
            try
            {
                int PkRefNo = FormT.PkRefNo;
                int? Fw1PkRefNo = FormT.PkRefNo;

                var domainModelformT = _mapper.Map<RmFormTHdr>(FormT);
                domainModelformT.FmtPkRefNo = PkRefNo;
                // domainModelformT.FTFw1PkRefNo = Fw1PkRefNo;

                domainModelformT.FmtActiveYn = true;
                domainModelformT = UpdateStatus(domainModelformT);
                _repoUnit.FormTRepository.Update(domainModelformT);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }


        public RmFormTHdr UpdateStatus(RmFormTHdr form)
        {
            if (form.FmtPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormTRepository._context.RmFormTHdr.Where(x => x.FmtPkRefNo == form.FmtPkRefNo).Select(x => new { Status = x.FmtStatus, Log = x.FmtAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.FmtAuditLog = existsObj.Log;
                    form.FmtStatus = existsObj.Status;

                }

            }
            if (form.FmtSubmitSts && (form.FmtStatus == "Saved" || form.FmtStatus == "Initialize"))
            {
                form.FmtStatus = Common.StatusList.FormW2Submitted;
                form.FmtAuditLog = Utility.ProcessLog(form.FmtAuditLog, "Submitted By", "Submitted", form.FmtUsernameRcd, string.Empty, form.FmtDateRcd, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Recorded By:" + form.FmtUsernameRcd + " - Form T (" + form.FmtPkRefNo + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/InstructedWorks/EditFormT?id=" + form.FmtPkRefNo.ToString(),
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }

            return form;
        }


        public async Task<int> DeActivateFormT(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormT = await _repoUnit.FormTRepository.GetByIdAsync(formNo);
                domainModelFormT.FmtActiveYn = false;
                _repoUnit.FormTRepository.Update(domainModelFormT);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }


        //public async Task<FORMTRpt> GetReportData(int headerid)
        //{
        //    return await _repo.GetReportData(headerid);
        //}

        //public async Task<byte[]> FormDownload(string formname, int id, string filepath)
        //{
        //    string Oldfilename = "";
        //    string filename = "";
        //    string cachefile = "";
        //    if (!filepath.Contains(".xlsx"))
        //    {
        //        Oldfilename = filepath + formname + ".xlsx";
        //        filename = formname + DateTime.Now.ToString("yyyyMMddHHmmssfffffff").ToString();
        //        cachefile = filepath + filename + ".xlsx";
        //    }
        //    else
        //    {
        //        Oldfilename = filepath;
        //        filename = filepath.Replace(".xlsx", DateTime.Now.ToString("yyyyMMddHHmmssfffffff").ToString() + ".xlsx");
        //        cachefile = filename;
        //    }

        //    try
        //    {
        //        FORMTRpt rpt = await this.GetReportData(id);
        //        System.IO.File.Copy(Oldfilename, cachefile, true);
        //        using (var workbook = new XLWorkbook(cachefile))
        //        {
        //            int noofsheets = (rpt.Details.Count() / 24) + ((rpt.Details.Count() % 24) > 0 ? 1 : 1);
        //            for (int sheet = 2; sheet <= noofsheets; sheet++)
        //            {
        //                using (var tempworkbook = new XLWorkbook(cachefile))
        //                {
        //                    string sheetname = "sheet" + Convert.ToString(sheet);
        //                    IXLWorksheet copysheet = tempworkbook.Worksheet(1);
        //                    copysheet.Worksheet.Name = sheetname;
        //                    copysheet.Cell(5, 7).Value = rpt.Division;
        //                    copysheet.Cell(5, 26).Value = rpt.District;
        //                    copysheet.Cell(5, 47).Value = rpt.RMU;
        //                    copysheet.Cell(6, 7).Value = rpt.RoadCode;
        //                    copysheet.Cell(7, 7).Value = rpt.RoadName;
        //                    copysheet.Cell(6, 26).Value = rpt.CrewLeader;
        //                    copysheet.Cell(5, 72).Value = rpt.InspectedByName;
        //                    copysheet.Cell(6, 72).Value = rpt.InspectedDate.HasValue ? rpt.InspectedDate.Value.ToString("dd-MM-yyyy") : "";
        //                    copysheet.Cell(7, 74).Value = rpt.RoadLength;
        //                    copysheet.Cell(2, 73).Value = sheet;
        //                    copysheet.Cell(2, 80).Value = noofsheets;
        //                    workbook.AddWorksheet(copysheet);
        //                }
        //            }
        //            int index = 1;
        //            int? condition1 = 0;
        //            int? condition2 = 0;
        //            int? condition3 = 0;
        //            string conditiondata1 = "";
        //            string conditiondata2 = "";
        //            string conditiondata3 = "";
        //            for (int sheet = 1; sheet <= noofsheets; sheet++)
        //            {


        //                IXLWorksheet worksheet;
        //                workbook.Worksheets.TryGetWorksheet($"sheet{sheet}", out worksheet);

        //                if (worksheet != null)
        //                {
        //                    worksheet.Cell(5, 7).Value = (rpt.Division == "MIRI" ? "Miri" : rpt.Division);
        //                    worksheet.Cell(5, 26).Value = (rpt.District == "MIRI" ? "Miri" : rpt.District);
        //                    worksheet.Cell(5, 47).Value = (rpt.RMU == "MIRI" ? "Miri" : rpt.RMU);
        //                    worksheet.Cell(6, 7).Value = rpt.RoadCode;
        //                    worksheet.Cell(7, 7).Value = rpt.RoadName;
        //                    worksheet.Cell(6, 26).Value = rpt.CrewLeader;
        //                    worksheet.Cell(5, 72).Value = rpt.InspectedByName;
        //                    worksheet.Cell(6, 72).Value = rpt.InspectedDate.HasValue ? rpt.InspectedDate.Value.ToString("dd-MM-yyyy") : "";
        //                    worksheet.Cell(7, 74).Value = rpt.RoadLength;
        //                    worksheet.Cell(2, 80).Value = noofsheets;
        //                    //worksheet.Cell(9, 8).Value = condition1.ToString() == "0" ? "" : condition1.ToString();
        //                    //worksheet.Cell(9, 24).Value = condition2.ToString() == "0" ? "" : condition1.ToString();
        //                    //worksheet.Cell(9, 45).Value = condition3.ToString() == "0" ? "" : condition1.ToString();
        //                    int i = 14;

        //                    var data = rpt.Details.Skip((sheet - 1) * 24).Take(24);
        //                    foreach (var r in data)
        //                    {


        //                        if (r.Condition == 1)
        //                        {
        //                            condition1 += 1;
        //                        }
        //                        if (r.Condition == 2)
        //                        {
        //                            condition2 += 1;

        //                        }
        //                        if (r.Condition == 3)
        //                        {
        //                            condition3 += 1;
        //                        }

        //                        worksheet.Cell(i, 2).Value = index;

        //                        worksheet.Cell(i, 4).Value = $"{r.LocationChKm}+{r.LocationChM}";
        //                        worksheet.Cell(i, 8).Value = r.StructCode;
        //                        worksheet.Cell(i, 10).Value = r.Tier;
        //                        worksheet.Cell(i, 17).Value = r.Length;
        //                        worksheet.Cell(i, 24).Value = r.Width;
        //                        worksheet.Cell(i, 31).Value = r.BottomWidth;
        //                        worksheet.Cell(i, 38).Value = r.Height;
        //                        worksheet.Cell(i, 45).Value = r.Condition;
        //                        worksheet.Cell(i, 52).Value = r.Descriptions;

        //                        index++;
        //                        i++;

        //                    }
        //                    worksheet.Cell(39, 8).Value = condition1;
        //                    worksheet.Cell(39, 24).Value = condition2;
        //                    worksheet.Cell(39, 45).Value = condition3;
        //                }
        //            }


        //            using (var stream = new MemoryStream())
        //            {
        //                workbook.SaveAs(stream);
        //                var content = stream.ToArray();
        //                System.IO.File.Delete(cachefile);
        //                return content;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        System.IO.File.Copy(Oldfilename, cachefile, true);
        //        using (var workbook = new XLWorkbook(cachefile))
        //        {
        //            using (var stream = new MemoryStream())
        //            {
        //                workbook.SaveAs(stream);
        //                var content = stream.ToArray();
        //                System.IO.File.Delete(cachefile);
        //                return content;
        //            }
        //        }

        //    }
        //}

    }
}
