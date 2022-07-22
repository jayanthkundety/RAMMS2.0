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

                return _repo.SaveFormTDtl(model, veh);
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


        public async Task<FORMTRpt> GetReportData(int headerid)
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
                FORMTRpt rpt = await this.GetReportData(id);
                System.IO.File.Copy(Oldfilename, cachefile, true);
                using (var workbook = new XLWorkbook(cachefile))
                {
                    int noofsheets = 1;


                    for (int sheet = 1; sheet <= noofsheets; sheet++)
                    {


                        IXLWorksheet worksheet;
                        workbook.Worksheets.TryGetWorksheet($"sheet{sheet}", out worksheet);

                        if (worksheet != null)
                        {
                            worksheet.Cell(1, 13).Value = rpt.RefId;
                            worksheet.Cell(3, 35).Value = rpt.InspectedDate.HasValue ? rpt.InspectedDate.Value.ToString("dd-MM-yyyy") : "";
                            worksheet.Cell(4, 8).Value = rpt.RMU;
                            worksheet.Cell(4, 19).Value = rpt.Details.Day;
                            worksheet.Cell(4, 22).Value = rpt.Details.TotalDay;
                            worksheet.Cell(4, 26).Value = rpt.Details.HourlycountPerDay;
                            worksheet.Cell(4, 35).Value = rpt.RefNo;
                            worksheet.Cell(5, 6).Value = rpt.RoadCode;
                            worksheet.Cell(5, 17).Value = rpt.Details.DirectionFrom;
                            worksheet.Cell(5, 29).Value = rpt.Details.DirectionTo;
                            worksheet.Cell(6, 6).Value = rpt.RoadName;

                            worksheet.Cell(7, 4).Value = rpt.Details.FromTime;

                            int colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "PC").FirstOrDefault();
                                worksheet.Cell(8, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }
                            worksheet.Cell(8, 30).Value = rpt.Details.DescriptionPC;

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "2R" && x.Loading == "2RE").FirstOrDefault();
                                worksheet.Cell(26, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "2R" && x.Loading == "2RN").FirstOrDefault();
                                worksheet.Cell(27, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "2R" && x.Loading == "2RO").FirstOrDefault();
                                worksheet.Cell(28, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "3R" && x.Loading == "3RE").FirstOrDefault();
                                worksheet.Cell(29, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "3R" && x.Loading == "3RN").FirstOrDefault();
                                worksheet.Cell(30, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "3R" && x.Loading == "3RO").FirstOrDefault();
                                worksheet.Cell(31, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "3A" && x.Loading == "3AE").FirstOrDefault();
                                worksheet.Cell(32, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "3A" && x.Loading == "3AN").FirstOrDefault();
                                worksheet.Cell(33, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "3A" && x.Loading == "3AO").FirstOrDefault();
                                worksheet.Cell(34, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "4A" && x.Loading == "4AE").FirstOrDefault();
                                worksheet.Cell(35, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "4A" && x.Loading == "4AN").FirstOrDefault();
                                worksheet.Cell(36, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "4A" && x.Loading == "4AO").FirstOrDefault();
                                worksheet.Cell(37, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "5A" && x.Loading == "5AE").FirstOrDefault();
                                worksheet.Cell(38, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "5A" && x.Loading == "5AN").FirstOrDefault();
                                worksheet.Cell(39, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "5A" && x.Loading == "5AO").FirstOrDefault();
                                worksheet.Cell(40, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "6A" && x.Loading == "6AE").FirstOrDefault();
                                worksheet.Cell(41, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "6A" && x.Loading == "6AN").FirstOrDefault();
                                worksheet.Cell(42, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "6A" && x.Loading == "6AO").FirstOrDefault();
                                worksheet.Cell(43, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "7A" && x.Loading == "7AE").FirstOrDefault();
                                worksheet.Cell(44, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "7A" && x.Loading == "7AN").FirstOrDefault();
                                worksheet.Cell(45, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "HV" && x.Axle == "7A" && x.Loading == "7AO").FirstOrDefault();
                                worksheet.Cell(46, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }
                            worksheet.Cell(27, 30).Value = rpt.Details.DescriptionHV;

                            colindex = 8;
                            for (int i = 1; i <= 12; i++)
                            {
                                var obj = rpt.Details.Vechilce.Where(x => x.Time == (i * 5) && x.VechicleType == "MC").FirstOrDefault();
                                worksheet.Cell(50, colindex).Value = obj == null ? "" : obj.Count.ToString();
                                colindex = colindex + 2;
                            }
                            worksheet.Cell(51, 30).Value = rpt.Details.DescriptionMC;

                            worksheet.Cell(63, 1).Value = rpt.Details.Description;

                            worksheet.Cell(63, 33).Value = rpt.RecName;
                            worksheet.Cell(64, 33).Value = rpt.RecDesg;
                            worksheet.Cell(65, 33).Value = rpt.RecDate.HasValue ? rpt.RecDate.Value.ToString("dd-MM-yyyy") : "";

                            worksheet.Cell(67, 33).Value = rpt.HdName;
                            worksheet.Cell(68, 33).Value = rpt.HdDesg;
                            worksheet.Cell(69, 33).Value = rpt.HdDate.HasValue ? rpt.HdDate.Value.ToString("dd-MM-yyyy") : "";
 
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
