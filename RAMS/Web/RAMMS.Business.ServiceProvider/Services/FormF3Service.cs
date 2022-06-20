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
                var filteredRecords = await _repoUnit.FormF3Repository.GetFormF3DtlGridList(filterOptions);

                result.TotalRecords = filteredRecords.Count();  // await _repoUnit.FormDRepository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);

                result.PageResult = filteredRecords;

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


        //public List<FormG1G2Rpt> GetReportData(int headerid)
        //{
        //    return _repo.GetReportData(headerid);
        //}

        //public byte[] FormDownload(string formname, int id, string basepath, string filepath)
        //{
        //    //string structureCode = _repoUnit.DDLookUpRepository.GetConcatenateDdlTypeValue(new DTO.RequestBO.DDLookUpDTO { Type = "Structure Code", TypeCode = "Y" });
        //    string Oldfilename = "";
        //    string filename = "";
        //    string cachefile = "";
        //    basepath = $"{basepath}/Uploads";
        //    if (!filepath.Contains(".xlsx"))
        //    {
        //        Oldfilename = filepath + formname + ".xlsx";// formdetails.FgdFilePath+"\\" + formdetails.FgdFileName+ ".xlsx";
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
        //        List<FormG1G2Rpt> _rpt = this.GetReportData(id);
        //        System.IO.File.Copy(Oldfilename, cachefile, true);
        //        using (var workbook = new XLWorkbook(cachefile))
        //        {
        //            IXLWorksheet worksheet = workbook.Worksheet(1);

        //            using (var book = new XLWorkbook(cachefile))
        //            {
        //                if (worksheet != null)
        //                {
        //                    var rpt = _rpt[0];
        //                    worksheet.Cell(7, 3).Value = rpt.RefernceNo;
        //                    worksheet.Cell(9, 10).Value = rpt.Division;
        //                    worksheet.Cell(9, 10).Value = rpt.RMU;
        //                    worksheet.Cell(8, 10).Value = rpt.RoadName;
        //                    worksheet.Cell(7, 10).Value = rpt.RoadCode;
        //                    worksheet.Cell(9, 3).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";
        //                    var structureCode = rpt.StructureCode;
        //                    if (!string.IsNullOrEmpty(structureCode))
        //                    {
        //                        worksheet.Cell(8, 3).Value = structureCode;
        //                        worksheet.Cell(8, 3).RichText.Substring(0, structureCode.Length).Strikethrough = true;
        //                        if (!string.IsNullOrEmpty(rpt.StructureCode) && structureCode.IndexOf(" " + rpt.StructureCode + " ") > -1)
        //                        {
        //                            worksheet.Cell(8, 3).RichText.Substring(structureCode.IndexOf(" " + rpt.StructureCode + " "), (" " + rpt.StructureCode + " ").Length).Bold = true;
        //                            worksheet.Cell(8, 3).RichText.Substring(structureCode.IndexOf(" " + rpt.StructureCode + " "), (" " + rpt.StructureCode + " ").Length).Strikethrough = false;
        //                        }
        //                    }

        //                    worksheet.Cell(10, 3).Value = rpt.GPSEasting;
        //                    worksheet.Cell(10, 10).Value = rpt.GPSNorthing;

        //                    worksheet.Cell(13, 2).Value = rpt.ParkingPosition;
        //                    worksheet.Cell(14, 2).Value = rpt.Accessiblity;
        //                    worksheet.Cell(15, 2).Value = rpt.PotentialHazards;


        //                    for (int i = 0; i < _rpt.Count; i++)
        //                    {
        //                        rpt = _rpt[i];
        //                        worksheet.Cell(13, 6 + i).Value = rpt.Year;
        //                        worksheet.Cell(14, 6 + i).Value = rpt.Month;
        //                        worksheet.Cell(15, 6 + i).Value = rpt.Day;

        //                        worksheet.Cell(18, 6 + i).Value = rpt.BarriersYes == 1 ? "/" : "";
        //                        worksheet.Cell(19, 6 + i).Value = rpt.BarriersNo == 1 ? "/" : "";
        //                        worksheet.Cell(20, 6 + i).Value = rpt.BarriersCritical == 1 ? "/" : "";
        //                        worksheet.Cell(21, 6 + i).Value = rpt.BarriersClosed == 1 ? "/" : "";

        //                        worksheet.Cell(22, 6 + i).Value = rpt.GantryBeamsYes == 1 ? "/" : "";
        //                        worksheet.Cell(23, 6 + i).Value = rpt.GantryBeamsNo == 1 ? "/" : "";
        //                        worksheet.Cell(24, 6 + i).Value = rpt.GantryBeamsCritical == 1 ? "/" : "";
        //                        worksheet.Cell(25, 6 + i).Value = rpt.GantryBeamsClosed == 1 ? "/" : "";

        //                        worksheet.Cell(26, 6 + i).Value = rpt.GantryColsYes == 1 ? "/" : "";
        //                        worksheet.Cell(27, 6 + i).Value = rpt.GantryColsNo == 1 ? "/" : "";
        //                        worksheet.Cell(28, 6 + i).Value = rpt.GantryColsCritical == 1 ? "/" : "";
        //                        worksheet.Cell(29, 6 + i).Value = rpt.GantryColsClosed == 1 ? "/" : "";

        //                        worksheet.Cell(30, 6 + i).Value = rpt.FootingYes == 1 ? "/" : "";
        //                        worksheet.Cell(31, 6 + i).Value = rpt.FootingNo == 1 ? "/" : "";
        //                        worksheet.Cell(32, 6 + i).Value = rpt.FootingCritical == 1 ? "/" : "";
        //                        worksheet.Cell(33, 6 + i).Value = rpt.FootingClosed == 1 ? "/" : "";

        //                        worksheet.Cell(34, 6 + i).Value = rpt.AnchorYes == 1 ? "/" : "";
        //                        worksheet.Cell(35, 6 + i).Value = rpt.AnchorNo == 1 ? "/" : "";
        //                        worksheet.Cell(36, 6 + i).Value = rpt.AnchorCritical == 1 ? "/" : "";
        //                        worksheet.Cell(37, 6 + i).Value = rpt.AnchorClosed == 1 ? "/" : "";

        //                        worksheet.Cell(38, 6 + i).Value = rpt.MaintenanceAccessYes == 1 ? "/" : "";
        //                        worksheet.Cell(39, 6 + i).Value = rpt.MaintenanceAccessNo == 1 ? "/" : "";
        //                        worksheet.Cell(40, 6 + i).Value = rpt.MaintenanceAccessCritical == 1 ? "/" : "";
        //                        worksheet.Cell(41, 6 + i).Value = rpt.MaintenanceAccessClosed == 1 ? "/" : "";

        //                        worksheet.Cell(42, 6 + i).Value = rpt.StaticSignsYes == 1 ? "/" : "";
        //                        worksheet.Cell(43, 6 + i).Value = rpt.StaticSignsNo == 1 ? "/" : "";
        //                        worksheet.Cell(44, 6 + i).Value = rpt.StaticSignsCritical == 1 ? "/" : "";
        //                        worksheet.Cell(45, 6 + i).Value = rpt.StaticSignsClosed == 1 ? "/" : "";

        //                        worksheet.Cell(46, 6 + i).Value = rpt.VariableMessagYes == 1 ? "/" : "";
        //                        worksheet.Cell(47, 6 + i).Value = rpt.VariableMessagNo == 1 ? "/" : "";
        //                        worksheet.Cell(48, 6 + i).Value = rpt.VariableMessagCritical == 1 ? "/" : "";
        //                        worksheet.Cell(49, 6 + i).Value = rpt.VariableMessagClosed == 1 ? "/" : "";


        //                    }

        //                    worksheet.Cell(71, 3).Value = rpt.ReportforYear;

        //                    worksheet.Cell(72, 3).Value = rpt.StructureCode;
        //                    worksheet.Cell(73, 3).Value = rpt.RoadCode;
        //                    worksheet.Cell(74, 3).Value = rpt.RoadName;

        //                    worksheet.Cell(71, 11).Value = rpt.RefernceNo;
        //                    worksheet.Cell(72, 11).Value = rpt.RatingRecordNo;
        //                    worksheet.Cell(73, 11).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";

        //                    worksheet.Cell(81, 1).Value = rpt.PartB2ServiceProvider;
        //                    worksheet.Cell(81, 8).Value = rpt.PartB2ServicePrvdrCons;
        //                    worksheet.Cell(95, 1).Value = rpt.PartCGeneralComments;
        //                    worksheet.Cell(95, 8).Value = rpt.PartCGeneralCommentsCons;
        //                    worksheet.Cell(109, 1).Value = rpt.PartDFeedback;
        //                    worksheet.Cell(109, 8).Value = rpt.PartDFeedbackCons;
        //                    worksheet.Cell(127, 2).Value = rpt.InspectedByName;
        //                    worksheet.Cell(128, 2).Value = rpt.InspectedByDesignation;
        //                    worksheet.Cell(129, 2).Value = rpt.InspectedByDate;
        //                    worksheet.Cell(127, 10).Value = rpt.AuditedByName;
        //                    worksheet.Cell(128, 10).Value = rpt.AuditedByDesignation;
        //                    worksheet.Cell(129, 10).Value = rpt.AuditedByDate;
        //                    worksheet.Cell(130, 14).Value = rpt.GantrySignConditionRate;
        //                    worksheet.Cell(131, 14).Value = rpt.HaveIssueFound;

        //                    worksheet.Cell(136, 3).Value = rpt.ReportforYear;
        //                    worksheet.Cell(137, 3).Value = rpt.StructureCode;
        //                    worksheet.Cell(138, 3).Value = rpt.RoadCode;
        //                    worksheet.Cell(139, 3).Value = rpt.RoadName;

        //                    worksheet.Cell(136, 13).Value = rpt.RefernceNo;
        //                    worksheet.Cell(137, 13).Value = rpt.RatingRecordNo;
        //                    worksheet.Cell(138, 13).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";

        //                    worksheet.Cell(200, 3).Value = rpt.ReportforYear;
        //                    worksheet.Cell(201, 3).Value = rpt.StructureCode;
        //                    worksheet.Cell(202, 3).Value = rpt.RoadCode;
        //                    worksheet.Cell(203, 3).Value = rpt.RoadName;

        //                    worksheet.Cell(200, 13).Value = rpt.RefernceNo;
        //                    worksheet.Cell(201, 13).Value = rpt.RatingRecordNo;
        //                    worksheet.Cell(202, 13).Value = $"{rpt.LocationChainageKm}+{rpt.LocationChainageM}";


        //                    for (int index = 0; index < rpt.Pictures.Count; ++index)
        //                    {
        //                        if (File.Exists(basepath + "/" + rpt.Pictures[index].ImageUrl + "/" + rpt.Pictures[index].FileName))
        //                        {
        //                            MemoryStream memoryStream = new MemoryStream(File.ReadAllBytes(basepath + "/" + rpt.Pictures[index].ImageUrl + "/" + rpt.Pictures[index].FileName));
        //                            switch (index)
        //                            {
        //                                case 0:
        //                                    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(143, 1), new Point(45, 4)).WithSize(347, 178);
        //                                    continue;
        //                                case 2:
        //                                    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(155, 1), new Point(45, 4)).WithSize(347, 178);
        //                                    continue;
        //                                case 3:
        //                                    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(155, 9), new Point(45, 6)).WithSize(347, 178);
        //                                    continue;
        //                                case 4:
        //                                    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(167, 1), new Point(45, 4)).WithSize(347, 178);
        //                                    continue;
        //                                case 5:
        //                                    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(167, 9), new Point(45, 6)).WithSize(347, 178);
        //                                    continue;
        //                                case 6:
        //                                    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(180, 1), new Point(45, 4)).WithSize(347, 178);
        //                                    continue;
        //                                case 7:
        //                                    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(180, 9), new Point(45, 6)).WithSize(347, 178);
        //                                    continue;
        //                                case 8:
        //                                    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(207, 1), new Point(45, 4)).WithSize(347, 178);
        //                                    continue;
        //                                case 9:
        //                                    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(207, 9), new Point(45, 6)).WithSize(347, 178);
        //                                    continue;
        //                                case 10:
        //                                    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(220, 1), new Point(45, 4)).WithSize(347, 178);
        //                                    continue;
        //                                case 11:
        //                                    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(220, 9), new Point(45, 6)).WithSize(347, 178);
        //                                    continue;
        //                                case 12:
        //                                    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(232, 1), new Point(45, 4)).WithSize(347, 178);
        //                                    continue;
        //                                case 13:
        //                                    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(232, 9), new Point(45, 6)).WithSize(347, 178);
        //                                    continue;
        //                                //case 14:
        //                                //    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(247, 1), new Point(45, 4)).WithSize(347, 178);
        //                                //    continue;
        //                                //case 15:
        //                                //    worksheet.AddPicture((Stream)memoryStream).MoveTo(worksheet.Cell(247, 9), new Point(4, 6)).WithSize(347, 178);
        //                                //    continue;
        //                                default:
        //                                    continue;
        //                            }
        //                        }
        //                    }


        //                }
        //                using (var stream = new MemoryStream())
        //                {
        //                    workbook.SaveAs(stream);
        //                    var content = stream.ToArray();
        //                    System.IO.File.Delete(cachefile);
        //                    return content;
        //                }
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
