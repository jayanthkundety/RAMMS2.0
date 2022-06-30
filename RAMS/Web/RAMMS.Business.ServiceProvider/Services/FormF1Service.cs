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

    public class FormF1Service : IFormF1Service
    {
        private readonly IFormF1Repository _repo;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormF1Service(IRepositoryUnit repoUnit, IFormF1Repository repo, IMapper mapper, ISecurity security, IProcessService process)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
            processService = process;
            _repo = repo;
        }


        public async Task<PagingResult<FormF1HeaderRequestDTO>> GetHeaderList(FilteredPagingDefinition<FormF1SearchGridDTO> filterOptions)
        {
            PagingResult<FormF1HeaderRequestDTO> result = new PagingResult<FormF1HeaderRequestDTO>();
            List<FormF1HeaderRequestDTO> formAlist = new List<FormF1HeaderRequestDTO>();
            result.PageResult = await _repo.GetFilteredRecordList(filterOptions);
            result.TotalRecords = await _repo.GetFilteredRecordCount(filterOptions);
            result.PageNo = filterOptions.StartPageNo;
            result.FilteredRecords = result.PageResult != null ? result.PageResult.Count : 0;
            return result;
        }

        public async Task<PagingResult<FormF1DtlGridDTO>> GetDetailList(FilteredPagingDefinition<FormF1DtlResponseDTO> filterOptions)
        {


            PagingResult<FormF1DtlGridDTO> result = new PagingResult<FormF1DtlGridDTO>();

            List<FormF1DtlGridDTO> formF1DtlList = new List<FormF1DtlGridDTO>();
            try
            {
                var Records = await _repoUnit.FormF1Repository.GetFormF1DtlGridList(filterOptions);

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


        public IEnumerable<CSelectListItem> GetAssetDetails(FormF1ResponseDTO FormF1)
        {
            IEnumerable<RmAllassetInventory> list = _repo.GetAssetDetails(FormF1);

            return list.Select(s => new CSelectListItem
            {
                Text = s.AiAssetId,
                Value = s.AiPkRefNo.ToString(),
                FromKm = s.AiLocChKm ?? 0,
                FromM = s.AiLocChM,
                Item1 = s.AiStrucCode,
                Item2 = s.AiBound,
                Item3 = s.AiWidth.ToString(),
                CValue = s.AiHeight.ToString()
            }).ToList();

        }

        public async Task<FormF1ResponseDTO> GetHeaderById(int id)
        {
            var header = await _repoUnit.FormF1Repository.FindAsync(s => s.Ff1hPkRefNo == id && s.Ff1hActiveYn == true);
            if (header == null)
            {
                return null;
            }
            return _mapper.Map<FormF1ResponseDTO>(header);
        }


        public async Task<FormF1ResponseDTO> SaveFormF1(FormF1ResponseDTO FormF1)
        {
            try
            {
                var domainModelFormF1 = _mapper.Map<RmFormF1Hdr>(FormF1);
                domainModelFormF1.Ff1hPkRefNo = 0;


                var obj = _repoUnit.FormF1Repository.FindAsync(x => x.Ff1hRmuCode == domainModelFormF1.Ff1hRmuCode && x.Ff1hSecCode == domainModelFormF1.Ff1hSecCode && x.Ff1hRdCode == domainModelFormF1.Ff1hRdCode && x.Ff1hCrewSup == domainModelFormF1.Ff1hCrewSup && x.Ff1hInspectedYear == domainModelFormF1.Ff1hInspectedYear && x.Ff1hActiveYn == true).Result;
                if (obj != null)
                {
                    var res = _mapper.Map<FormF1ResponseDTO>(obj);
                    res.FormExist = true;
                    return res;
                }



                IDictionary<string, string> lstData = new Dictionary<string, string>();
                lstData.Add("RoadCode", domainModelFormF1.Ff1hRdCode);
                lstData.Add("Year", domainModelFormF1.Ff1hInspectedYear.ToString());
                domainModelFormF1.Ff1hPkRefId = FormRefNumber.GetRefNumber(RAMMS.Common.RefNumber.FormType.FormF1Header, lstData);
 
                var entity = _repo.SaveFormF1(domainModelFormF1).Result;
                FormF1.PkRefNo = _mapper.Map<FormF1ResponseDTO>(entity).PkRefNo;
                FormF1.PkRefId = domainModelFormF1.Ff1hPkRefId;
                FormF1.Status = domainModelFormF1.Ff1hStatus;

                //if (FormF1.Source == "G1G2")
                //    _repo.LoadG1G2Data(FormF1);

                return FormF1;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public int? DeleteFormF1(int id)
        {
            int? rowsAffected;
            try
            {
                rowsAffected = _repo.DeleteFormF1(id);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }
        public int? DeleteFormF1Dtl(int Id)
        {
            try
            {
                return _repo.DeleteFormF1Dtl(Id);
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        public int? SaveFormF1Dtl(FormF1DtlResponseDTO FormF1Dtl)
        {
            try
            {
                var model = _mapper.Map<RmFormF1Dtl>(FormF1Dtl);
                model.Ff1dPkRefNo = 0;
                return _repo.SaveFormF1Dtl(model);
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }


        public int? UpdateFormF1Dtl(FormF1DtlResponseDTO FormF1Dtl)
        {

            try
            {
                int? Ff1hPkRefNo = FormF1Dtl.Ff1hPkRefNo;
                int Ff1dPkRefNo = FormF1Dtl.PkRefNo;
                // int? G1PkRefNo = FormF1Dtl.Ff1dG1hPkRefNo;
                var model = _mapper.Map<RmFormF1Dtl>(FormF1Dtl);
                model.Ff1dPkRefNo = Ff1dPkRefNo;
                //model.Ff1dFf1hPkRefNo = Ff1hPkRefNo;
                //model.Ff1dG1hPkRefNo = G1PkRefNo;
                return _repo.UpdateFormF1Dtl(model);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }
        }



        public async Task<int> Update(FormF1ResponseDTO FormF1)
        {
            int rowsAffected;
            try
            {
                int PkRefNo = FormF1.PkRefNo;
                int? Fw1PkRefNo = FormF1.PkRefNo;

                var domainModelformF1 = _mapper.Map<RmFormF1Hdr>(FormF1);
                domainModelformF1.Ff1hPkRefNo = PkRefNo;
                // domainModelformF1.FF1Fw1PkRefNo = Fw1PkRefNo;

                domainModelformF1.Ff1hActiveYn = true;
                domainModelformF1 = UpdateStatus(domainModelformF1);
                _repoUnit.FormF1Repository.Update(domainModelformF1);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }


        public RmFormF1Hdr UpdateStatus(RmFormF1Hdr form)
        {
            if (form.Ff1hPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormF1Repository._context.RmFormF1Hdr.Where(x => x.Ff1hPkRefNo == form.Ff1hPkRefNo).Select(x => new { Status = x.Ff1hStatus, Log = x.Ff1hAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.Ff1hAuditLog = existsObj.Log;
                    form.Ff1hStatus = existsObj.Status;

                }

            }
            if (form.Ff1hSubmitSts && (form.Ff1hStatus == "Saved" || form.Ff1hStatus == "Initialize"))
            {
                form.Ff1hStatus = Common.StatusList.FormW2Submitted;
                form.Ff1hAuditLog = Utility.ProcessLog(form.Ff1hAuditLog, "Submitted By", "Submitted", form.Ff1hInspectedName, string.Empty, form.Ff1hInspectedDate, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Recorded By:" + form.Ff1hInspectedName + " - Form F1 (" + form.Ff1hPkRefNo + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/InstructedWorks/EditFormF1?id=" + form.Ff1hPkRefNo.ToString(),
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }

            return form;
        }


        public async Task<int> DeActivateFormF1(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormF1 = await _repoUnit.FormF1Repository.GetByIdAsync(formNo);
                domainModelFormF1.Ff1hActiveYn = false;
                _repoUnit.FormF1Repository.Update(domainModelFormF1);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }


        public async Task<FORMF1Rpt> GetReportData(int headerid)
        {
            return await _repo.GetReportData(headerid);
        }

        public async Task<byte[]> FormDownload(string formname, int id, string filepath)
        {
            string Oldfilename = "";
            string filename = "";
            string cachefile = "";
            if (!filepath.Contains(".xlsx"))
            {
                Oldfilename = filepath + formname + ".xlsx";
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
                FORMF1Rpt rpt = await this.GetReportData(id);
                System.IO.File.Copy(Oldfilename, cachefile, true);
                using (var workbook = new XLWorkbook(cachefile))
                {
                    int noofsheets = (rpt.Details.Count() / 24) + ((rpt.Details.Count() % 24) > 0 ? 1 : 1);
                    for (int sheet = 2; sheet <= noofsheets; sheet++)
                    {
                        using (var tempworkbook = new XLWorkbook(cachefile))
                        {
                            string sheetname = "sheet" + Convert.ToString(sheet);
                            IXLWorksheet copysheet = tempworkbook.Worksheet(1);
                            copysheet.Worksheet.Name = sheetname;
                            copysheet.Cell(5, 7).Value = rpt.Division;
                            copysheet.Cell(5, 26).Value = rpt.District;
                            copysheet.Cell(5, 47).Value = rpt.RMU;
                            copysheet.Cell(6, 7).Value = rpt.RoadCode;
                            copysheet.Cell(7, 7).Value = rpt.RoadName;
                            copysheet.Cell(6, 26).Value = rpt.CrewLeader;
                            copysheet.Cell(5, 72).Value = rpt.InspectedByName;
                            copysheet.Cell(6, 72).Value = rpt.InspectedDate.HasValue ? rpt.InspectedDate.Value.ToString("dd-MM-yyyy") : "";
                            copysheet.Cell(7, 74).Value = rpt.RoadLength;
                            copysheet.Cell(2, 73).Value = sheet;
                            copysheet.Cell(2, 80).Value = noofsheets;
                            workbook.AddWorksheet(copysheet);
                        }
                    }
                    int index = 1;
                    int? condition1 = 0;
                    int? condition2 = 0;
                    int? condition3 = 0;
                    string conditiondata1 = "";
                    string conditiondata2 = "";
                    string conditiondata3 = "";
                    for (int sheet = 1; sheet <= noofsheets; sheet++)
                    {


                        IXLWorksheet worksheet;
                        workbook.Worksheets.TryGetWorksheet($"sheet{sheet}", out worksheet);

                        if (worksheet != null)
                        {
                            worksheet.Cell(5, 7).Value = (rpt.Division == "MIRI" ? "Miri" : rpt.Division);
                            worksheet.Cell(5, 26).Value = (rpt.District == "MIRI" ? "Miri" : rpt.District);
                            worksheet.Cell(5, 47).Value = (rpt.RMU == "MIRI" ? "Miri" : rpt.RMU);
                            worksheet.Cell(6, 7).Value = rpt.RoadCode;
                            worksheet.Cell(7, 7).Value = rpt.RoadName;
                            worksheet.Cell(6, 26).Value = rpt.CrewLeader;
                            worksheet.Cell(5, 72).Value = rpt.InspectedByName;
                            worksheet.Cell(6, 72).Value = rpt.InspectedDate.HasValue ? rpt.InspectedDate.Value.ToString("dd-MM-yyyy") : "";
                            worksheet.Cell(7, 74).Value = rpt.RoadLength;
                            worksheet.Cell(2, 80).Value = noofsheets;
                            //worksheet.Cell(9, 8).Value = condition1.ToString() == "0" ? "" : condition1.ToString();
                            //worksheet.Cell(9, 24).Value = condition2.ToString() == "0" ? "" : condition1.ToString();
                            //worksheet.Cell(9, 45).Value = condition3.ToString() == "0" ? "" : condition1.ToString();
                            int i = 13;

                            var data = rpt.Details.Skip((sheet - 1) * 24).Take(24);
                            foreach (var r in data)
                            {
                                conditiondata1 = "";
                                conditiondata2 = "";
                                conditiondata3 = "";

                                if (r.Condition == 1)
                                {
                                    condition1 += 1;
                                    conditiondata1 = "/";
                                }
                                if (r.Condition == 2)
                                {
                                    condition2 += 1;
                                    conditiondata2 = "/";
                                }
                                if (r.Condition == 3)
                                {
                                    condition3 += 1;
                                    conditiondata3 = "/";
                                }



                                worksheet.Cell(i, 2).Value = index;

                                worksheet.Cell(i, 4).Value = $"{r.LocationChKm}+{r.LocationChM}";
                                worksheet.Cell(i, 8).Value = r.StructCode;
                                worksheet.Cell(i, 10).Value = conditiondata1;
                                worksheet.Cell(i, 17).Value = conditiondata2;
                                worksheet.Cell(i, 24).Value = conditiondata3;
                                worksheet.Cell(i, 31).Value = r.Bound;
                                worksheet.Cell(i, 38).Value = r.Width;
                                worksheet.Cell(i, 45).Value = r.Height;
                                worksheet.Cell(i, 52).Value = r.Descriptions;

                                index++;
                                i++;

                            }
                            worksheet.Cell(38, 8).Value = condition1;
                            worksheet.Cell(38, 24).Value = condition2;
                            worksheet.Cell(38, 45).Value = condition3;
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

    }
}
