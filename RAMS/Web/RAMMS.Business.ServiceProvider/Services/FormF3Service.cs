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

    public class FormF3Service : IFormF3Service
    {
        private readonly IFormF3Repository _repo;
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private readonly IProcessService processService;
        public FormF3Service(IRepositoryUnit repoUnit, IFormF3Repository repo, IMapper mapper, ISecurity security, IProcessService process)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
            processService = process;
            _repo = repo;
        }

        //public async Task<FormF3ResponseDTO> FindF3ByW1ID(int id)
        //{
        //    RmIwFormF3 formWC = await _repo.FindF3Byw1ID(id);
        //    return _mapper.Map<FormF3ResponseDTO>(formWC);
        //}

        //public async Task<FormF3ResponseDTO> FindFormF3ByID(int id)
        //{
        //    RmIwFormF3 formF3 = await _repo.FindFormF3ByID(id);
        //    return _mapper.Map<FormF3ResponseDTO>(formF3);
        //}

        //public async Task<IEnumerable<FormF3DtlResponseDTO>> FindFormF3DtlByID(int id)
        //{
        //    IEnumerable<RmFormF3Dtl> formF3Dtl = await _repo.FindFormF3DtlByID(id);
        //    return _mapper.Map<IEnumerable<FormF3DtlResponseDTO>>(formF3Dtl);
        //}

        public async Task<PagingResult<FormF2HeaderRequestDTO>> GetHeaderList(FilteredPagingDefinition<FormF2SearchGridDTO> filterOptions)
        {
            PagingResult<FormF2HeaderRequestDTO> result = new PagingResult<FormF2HeaderRequestDTO>();
            List<FormF2HeaderRequestDTO> formAlist = new List<FormF2HeaderRequestDTO>();
            result.PageResult = await _repo.GetFilteredRecordList(filterOptions);
            result.TotalRecords = await _repo.GetFilteredRecordCount(filterOptions);
            result.PageNo = filterOptions.StartPageNo;
            result.FilteredRecords = result.PageResult != null ? result.PageResult.Count : 0;
            return result;
        }

        public async Task<PagingResult<FormF3DtlGridDTO>> GetDetailList(FilteredPagingDefinition<FormF3DtlResponseDTO> filterOptions)
        {


            PagingResult<FormF3DtlGridDTO> result = new PagingResult<FormF3DtlGridDTO>();

            List<FormF3DtlGridDTO> formF3DtlList = new List<FormF3DtlGridDTO>();
            try
            {
                var Records = await _repoUnit.FormF3Repository.GetFormF3DtlGridList(filterOptions);
 
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


        public IEnumerable<CSelectListItem> GetAssetDetails(FormF3ResponseDTO FormF3)
        {
            IEnumerable<RmAllassetInventory> list = _repo.GetAssetDetails(FormF3);

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

        public async Task<FormF3ResponseDTO> GetHeaderById(int id)
        {
            var header = await _repoUnit.FormF3Repository.FindAsync(s => s.Ff3hPkRefNo == id && s.Ff3hActiveYn == true);
            if (header == null)
            {
                return null;
            }
            return _mapper.Map<FormF3ResponseDTO>(header);
        }


        public async Task<FormF3ResponseDTO> SaveFormF3(FormF3ResponseDTO FormF3)
        {
            try
            {
                var domainModelFormF3 = _mapper.Map<RmFormF3Hdr>(FormF3);
                domainModelFormF3.Ff3hPkRefNo = 0;


                var obj = _repoUnit.FormF3Repository.FindAsync(x => x.Ff3hRmuCode == domainModelFormF3.Ff3hRmuCode && x.Ff3hSecCode == domainModelFormF3.Ff3hSecCode && x.Ff3hRdCode == domainModelFormF3.Ff3hRdCode && x.Ff3hCrewSup == domainModelFormF3.Ff3hCrewSup && x.Ff3hInspectedYear == domainModelFormF3.Ff3hInspectedYear && x.Ff3hActiveYn == true).Result;
                if (obj != null)
                {
                    var res = _mapper.Map<FormF3ResponseDTO>(obj);
                    res.FormExist = true;
                    return res;
                }

                IDictionary<string, string> lstData = new Dictionary<string, string>();

                lstData.Add("RoadCode", domainModelFormF3.Ff3hRdCode);
                lstData.Add("Year", domainModelFormF3.Ff3hInspectedYear.ToString());
                domainModelFormF3.Ff3hPkRefId = FormRefNumber.GetRefNumber(RAMMS.Common.RefNumber.FormType.FormF3Header, lstData);

                var entity = _repoUnit.FormF3Repository.CreateReturnEntity(domainModelFormF3);
                FormF3.PkRefNo = _mapper.Map<FormF3ResponseDTO>(entity).PkRefNo;
                FormF3.PkRefId = domainModelFormF3.Ff3hPkRefId;
                FormF3.Status = domainModelFormF3.Ff3hStatus;

                if (FormF3.Source == "G1G2")
                    _repo.LoadG1G2Data(FormF3);

                return FormF3;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public int? DeleteFormF3(int id)
        {
            int? rowsAffected;
            try
            {
                rowsAffected = _repo.DeleteFormF3(id);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }
        public int? DeleteFormF3Dtl(int Id)
        {
            try
            {
                return _repo.DeleteFormF3Dtl(Id);
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        public int? SaveFormF3Dtl(FormF3DtlResponseDTO FormF3Dtl)
        {
            try
            {
                var model = _mapper.Map<RmFormF3Dtl>(FormF3Dtl);
                model.Ff3dPkRefNo = 0;
                return _repo.SaveFormF3Dtl(model);
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }


        public int? UpdateFormF3Dtl(FormF3DtlResponseDTO FormF3Dtl)
        {

            try
            {
                int? Ff3hPkRefNo = FormF3Dtl.Ff3hPkRefNo;
                int Ff3dPkRefNo = FormF3Dtl.PkRefNo;
                var model = _mapper.Map<RmFormF3Dtl>(FormF3Dtl);
                model.Ff3dPkRefNo = Ff3dPkRefNo;
                model.Ff3dFf3hPkRefNo = Ff3hPkRefNo;
                return _repo.UpdateFormF3Dtl(model);
            }
            catch (Exception ex)
            {
                _repoUnit.RollbackAsync();
                throw ex;
            }
        }



        public async Task<int> Update(FormF3ResponseDTO FormF3)
        {
            int rowsAffected;
            try
            {
                int PkRefNo = FormF3.PkRefNo;
                int? Fw1PkRefNo = FormF3.PkRefNo;

                var domainModelformF3 = _mapper.Map<RmFormF3Hdr>(FormF3);
                domainModelformF3.Ff3hPkRefNo = PkRefNo;
                // domainModelformF3.FF3Fw1PkRefNo = Fw1PkRefNo;

                domainModelformF3.Ff3hActiveYn = true;
                domainModelformF3 = UpdateStatus(domainModelformF3);
                _repoUnit.FormF3Repository.Update(domainModelformF3);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }


        public RmFormF3Hdr UpdateStatus(RmFormF3Hdr form)
        {
            if (form.Ff3hPkRefNo > 0)
            {
                var existsObj = _repoUnit.FormF3Repository._context.RmFormF3Hdr.Where(x => x.Ff3hPkRefNo == form.Ff3hPkRefNo).Select(x => new { Status = x.Ff3hStatus, Log = x.Ff3hAuditLog }).FirstOrDefault();
                if (existsObj != null)
                {
                    form.Ff3hAuditLog = existsObj.Log;
                    form.Ff3hStatus = existsObj.Status;

                }

            }
            if (form.Ff3hSubmitSts && (form.Ff3hStatus == "Saved" || form.Ff3hStatus == "Initialize"))
            {
                form.Ff3hStatus = Common.StatusList.FormW2Submitted;
                form.Ff3hAuditLog = Utility.ProcessLog(form.Ff3hAuditLog, "Submitted By", "Submitted", form.Ff3hInspectedName, string.Empty, form.Ff3hInspectedDate, _security.UserName);
                processService.SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = _security.UserName,
                    RmNotGroup = GroupNames.OperationsExecutive,
                    RmNotMessage = "Recorded By:" + form.Ff3hInspectedName + " - Form F3 (" + form.Ff3hPkRefNo + ")",
                    RmNotOn = DateTime.Now,
                    RmNotUrl = "/InstructedWorks/EditFormF3?id=" + form.Ff3hPkRefNo.ToString(),
                    RmNotUserId = "",
                    RmNotViewed = ""
                }, true);
            }

            return form;
        }


        public async Task<int> DeActivateFormF3(int formNo)
        {
            int rowsAffected;
            try
            {
                var domainModelFormF3 = await _repoUnit.FormF3Repository.GetByIdAsync(formNo);
                domainModelFormF3.Ff3hActiveYn = false;
                _repoUnit.FormF3Repository.Update(domainModelFormF3);
                rowsAffected = await _repoUnit.CommitAsync();
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }


        public async Task<FORMF3Rpt> GetReportData(int headerid)
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
                FORMF3Rpt rpt = await this.GetReportData(id);
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
