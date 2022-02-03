using System;
using System.Threading.Tasks;
using AutoMapper;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.RequestBO;
using RAMMS.Repository.Interfaces;
using System.Linq;
using RAMMS.Domain.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.DTO;
using RAMMS.DTO.Report;
using ClosedXML.Excel;
using System.IO;
using RAMMS.Common.RefNumber;
using RAMMS.Common;
using RAMMS.DTO.JQueryModel;


namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormS2Service : IFormS2Service
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly IProcessService processService;
        public FormS2Service(IRepositoryUnit repoUnit, IMapper mapper, IProcessService proService)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            processService = proService;
        }
        public async Task<FormS2DetailRequestDto> GetDetailById(int id)
        {
            try
            {
                var result = await _repoUnit.FormS2DetailRepository.FindAsync(s => s.FsiidPkRefNo == id);
                if (result != null)
                {
                    var _ = _mapper.Map<FormS2DetailRequestDto>(result);
                    _.WeekDetail = _repoUnit.FormS2QuarterDtlRepository.FindAll(s => s.FsiiqdFsiidPkRefNo == id).Select(s => s.FsiiqdClkPkRefNo.Value).ToList();
                    return _;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public async Task<FormS2HeaderRequestDto> GetHeaderById(int id)
        {
            try
            {
                var result = await _repoUnit.FormS2Repository.FindAsync(s => s.FsiihPkRefNo == id);
                if (result != null)
                {
                    var _ = _mapper.Map<FormS2HeaderRequestDto>(result);
                    return _;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public int SaveDetail(FormS2DetailRequestDto request)
        {
            Func<int, List<int>, int> saveClq = (detailId, weeks) =>
              {
                  _repoUnit.FormS2QuarterDtlRepository.Delete(s => s.FsiiqdFsiidPkRefNo == detailId);
                  _repoUnit.Commit();
                  if (weeks.Count > 0)
                  {
                      var toAdd = weeks.Select(s => new RmFormS2QuarDtl
                      {
                          FsiiqdClkPkRefNo = s,
                          FsiiqdFsiidPkRefNo = detailId
                      });
                      _repoUnit.FormS2QuarterDtlRepository.Create(toAdd);
                      return _repoUnit.Commit();
                  }
                  return 0;
              };



            try
            {
                request.WeekDetail = request.WeekDetail ?? new List<int>();
                RmFormS2Dtl rmFormS2Dtl = _mapper.Map<RmFormS2Dtl>(request);
                rmFormS2Dtl.FsiidActiveYn = true;
                if (rmFormS2Dtl.FsiidPkRefNo > 0)
                {
                    rmFormS2Dtl.FsiidModDt = DateTime.UtcNow;
                    _repoUnit.FormS2DetailRepository.Update(rmFormS2Dtl);
                    _repoUnit.Commit();
                    saveClq(rmFormS2Dtl.FsiidPkRefNo, request.WeekDetail);
                    return rmFormS2Dtl.FsiidPkRefNo;
                }
                else
                {
                    rmFormS2Dtl.FsiidCrDt = DateTime.UtcNow;
                    rmFormS2Dtl.FsiidModDt = DateTime.UtcNow;
                    var result = _repoUnit.FormS2DetailRepository.CreateReturnEntity(rmFormS2Dtl);
                    rmFormS2Dtl.FsiidRefId = rmFormS2Dtl.FsiidRefId.Replace("???", (LastDetailInsertedNo(Convert.ToInt32(rmFormS2Dtl.FsiidFsiihPkRefNo))).ToString());
                    _repoUnit.FormS2DetailRepository._context.SaveChanges();
                    if (result != null)
                    {
                        saveClq(rmFormS2Dtl.FsiidPkRefNo, request.WeekDetail);
                        return result.FsiidPkRefNo;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<FormS2HeaderRequestDto> SaveHeader(FormS2HeaderRequestDto request)
        {
            try
            {
                RmFormS2Hdr rmFormS2Hdr = _mapper.Map<RmFormS2Hdr>(request);
                FormS2HeaderRequestDto res = null;
                if (rmFormS2Hdr.FsiihPkRefNo == 0)
                {

                    var result = await CheckHeaderExistence(request.Rmu, Convert.ToInt32(request.ActCode), Convert.ToInt32(request.Year), Convert.ToInt32(request.QuaterId));
                    if (result.aleadyExists)
                    {
                        rmFormS2Hdr.FsiihPkRefNo = result.id;
                    }
                }

                rmFormS2Hdr.FsiihActiveYn = true;
                rmFormS2Hdr.FsiihStatus = StatusList.S2Init;
                if (rmFormS2Hdr.FsiihPkRefNo > 0)
                {
                    rmFormS2Hdr.FsiihModDt = DateTime.UtcNow;
                    _repoUnit.FormS2Repository.Update(rmFormS2Hdr);
                    _repoUnit.Commit();

                    res = _mapper.Map<FormS2HeaderRequestDto>(rmFormS2Hdr);
                }
                else
                {
                    rmFormS2Hdr.FsiihCrDt = DateTime.UtcNow;
                    rmFormS2Hdr.FsiihModDt = DateTime.UtcNow;
                    var result = _repoUnit.FormS2Repository.CreateReturnEntity(rmFormS2Hdr);
                    IDictionary<string, string> lstData = new Dictionary<string, string>();
                    lstData.Add("RMU", rmFormS2Hdr.FsiihRmu);
                    lstData.Add("Quarter", rmFormS2Hdr.FsiihQuaterId.ToString());
                    lstData.Add("Year", rmFormS2Hdr.FsiihYear.ToString());
                    lstData.Add("ActCode", rmFormS2Hdr.FsiihActCode.ToString());
                    lstData.Add(FormRefNumber.NewRunningNumber, Utility.ToString(result.FsiihPkRefNo));
                    rmFormS2Hdr.FsiihRefId = FormRefNumber.GetRefNumber(Common.RefNumber.FormType.FormS2Header, lstData);
                    _repoUnit.FormS2Repository._context.SaveChanges();

                    if (result != null)
                    {
                        result.FsiihRefId = rmFormS2Hdr.FsiihRefId;
                        res = _mapper.Map<FormS2HeaderRequestDto>(result);
                    }
                    //else
                    //{
                    //    return null;
                    //}
                }
                if (res != null)
                {
                    if (res.SubmitSts == true)
                    {
                        int iResult = processService.Save(new ProcessDTO()
                        {
                            ApproveDate = DateTime.Now,
                            Form = "FormS2",
                            IsApprove = true,
                            RefId = res.PkRefNo,
                            Remarks = "",
                            Stage = res.Status
                        }).Result;

                    }
                }
                return res;
            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }
        }

        public List<SelectListItem> GetYears() => _repoUnit.CalendarRepository.GetYears();

        public List<SelectListItem> GetQuarter(int year) => _repoUnit.CalendarRepository.GetQuarter(year);

        public List<SelectListItem> GetMonth(int year, int quarter) => _repoUnit.CalendarRepository.GetMonth(year, quarter);

        public List<WeekS2ViewDto> GetWeek(int year, int quarter) => _repoUnit.CalendarRepository.GetWeek(year, quarter);

        public int GetId(int year, int quarter, int month, int week) => _repoUnit.CalendarRepository.GetId(year, quarter, month, week);
        public int LastHeaderInsertedNo() => _repoUnit.FormS2Repository.LastHeaderInsertedNo();
        public int LastDetailInsertedNo(int headerId) => _repoUnit.FormS2DetailRepository.LastDetailInsertedNo(headerId);


        public async Task<(int id, bool aleadyExists, bool isSubmitted)> CheckHeaderExistence(string rmu, int activityCode, int year, int quarter)
        {

            var data = await _repoUnit.FormS2Repository.FindAllAsync(s => s.FsiihRmu == rmu && s.FsiihQuaterId == quarter
            && s.FsiihActId == activityCode
            && s.FsiihYear == year
            && s.FsiihActiveYn == true);
            var result = data.FirstOrDefault();
            if (result != null)
                return (result.FsiihPkRefNo, true, result.FsiihSubmitSts);
            else
                return (LastHeaderInsertedNo(), false, false);
        }

        public async Task<PagingResult<S2HeaderResponse>> GetHeaderList(FilteredPagingDefinition<S2HeaderSearchRequestDTO> filterOptions)
        {
            PagingResult<S2HeaderResponse> result = new PagingResult<S2HeaderResponse>();
            List<S2HeaderResponse> formAList = new List<S2HeaderResponse>();
            result.PageResult = await _repoUnit.FormS2Repository.GetFilteredRecordList(filterOptions);
            result.TotalRecords = await _repoUnit.FormS2Repository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);
            return result;
        }

        public async Task<PagingResult<FormS2DetailResponseDTO>> GetDetailList(FilteredPagingDefinition<FormS2DetailSearchDto> filterOptions)
        {
            PagingResult<FormS2DetailResponseDTO> result = new PagingResult<FormS2DetailResponseDTO>();
            List<FormS2DetailSearchDto> formAList = new List<FormS2DetailSearchDto>();
            result.PageResult = await _repoUnit.FormS2DetailRepository.GetFilteredRecordList(filterOptions);
            result.TotalRecords = await _repoUnit.FormS2DetailRepository.GetFilteredRecordCount(filterOptions).ConfigureAwait(false);
            return result;
        }

        public FORMS2Rpt GetReportData(int headerId)
        {
            return _repoUnit.FormS2Repository.GetReportData(headerId);
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

            Func<int, int, string> getMonth = (quarter, month) =>
              {
                  switch (quarter, month)
                  {
                      case (1, 1): return "January";
                      case (1, 2): return "Febraury";
                      case (1, 3): return "March";
                      case (2, 1): return "April";
                      case (2, 2): return "May";
                      case (2, 3): return "June";
                      case (3, 1): return "July";
                      case (3, 2): return "August";
                      case (3, 3): return "September";
                      case (4, 1): return "October";
                      case (4, 2): return "November";
                      case (4, 3): return "December";
                      default: return "";
                  }
              };

            try
            {
                FORMS2Rpt rpt = this.GetReportData(id);
                System.IO.File.Copy(oldFileName, cacheFile, true);
                using (var workBook = new XLWorkbook(cacheFile))
                {
                    int noOfSheets = (rpt.Details.Count / 27) + ((rpt.Details.Count % 27) > 0 ? 1 : 1);
                    int weekIndex = 14;
                    for (int sheet = 2; sheet <= noOfSheets; sheet++)
                    {
                        using (var tempWorkBook = new XLWorkbook(cacheFile))
                        {
                            string sheetName = "sheet" + Convert.ToString(sheet);
                            IXLWorksheet copySheet = tempWorkBook.Worksheet(1);
                            copySheet.Worksheet.Name = sheetName;
                            copySheet.Cell(1, 2).Value = rpt.Header.RMU;
                            copySheet.Cell(2, 1).Value = $"TYPE OF ACTIVITY: {rpt.Header.Activity}";
                            copySheet.Cell(3, 74).Value = rpt.Header.PrioritizedBy;
                            copySheet.Cell(4, 74).Value = rpt.Header.PrioritizedDate;
                            copySheet.Cell(3, 81).Value = rpt.Header.ScheduledBy;
                            copySheet.Cell(4, 81).Value = rpt.Header.ScheduledDate;
                            copySheet.Cell(5, 14).Value = $"Year {rpt.Header.Year}";
                            copySheet.Cell(6, 14).Value = getMonth(rpt.Header.Quarter.Value, 1);
                            copySheet.Cell(6, 42).Value = getMonth(rpt.Header.Quarter.Value, 2);
                            copySheet.Cell(6, 51).Value = getMonth(rpt.Header.Quarter.Value, 3);

                            for (int i = 0; i < rpt.Header.WeekNo.Length; i++)
                            {
                                if (i > 0 && copySheet.Cell(8, weekIndex).Value != (object)rpt.Header.WeekNo[i - 1])
                                {
                                    copySheet.Cell(8, weekIndex).Value = rpt.Header.WeekNo[i];
                                    weekIndex += 7;
                                }
                                if (i == 0)
                                {
                                    copySheet.Cell(8, weekIndex).Value = rpt.Header.WeekNo[i];
                                }
                            }
                            copySheet.Cell(5, 79).Value = $"Page: {sheet} - {noOfSheets}";
                            copySheet.Cell(40, 10).Value = rpt.Header.SubmittedBy;
                            copySheet.Cell(42, 10).Value = rpt.Header.SubmittedDate;
                            copySheet.Cell(41, 10).Value = rpt.Header.SubmittedDesignation;
                            copySheet.Cell(40, 31).Value = rpt.Header.VettedBy;
                            copySheet.Cell(42, 31).Value = rpt.Header.VettedDate;
                            copySheet.Cell(41, 31).Value = rpt.Header.VettedDesignation;
                            copySheet.Cell(40, 63).Value = rpt.Header.AgreedBy;
                            copySheet.Cell(42, 63).Value = rpt.Header.AgreedDate;
                            copySheet.Cell(41, 63).Value = rpt.Header.AgreedDesignation;
                            workBook.AddWorksheet(copySheet);
                        }
                    }
                    int index = 1;
                    weekIndex = 14;
                    Dictionary<int, int> weekNo = new Dictionary<int, int>();
                    for (int sheet = 1; sheet <= noOfSheets; sheet++)
                    {


                        IXLWorksheet workSheet = workBook.Worksheet(sheet);

                        if (workSheet != null)
                        {
                            workSheet.Rows("9:47").AdjustToContents();

                            workSheet.Cell(1, 2).Value = rpt.Header.RMU.ToUpper();
                            workSheet.Cell(2, 1).Value = $"TYPE OF ACTIVITY: {rpt.Header.Activity}";
                            workSheet.Cell(3, 74).Value = rpt.Header.PrioritizedBy;
                            workSheet.Cell(4, 74).Value = rpt.Header.PrioritizedDate;
                            workSheet.Cell(3, 114).Value = rpt.Header.ScheduledBy;
                            workSheet.Cell(4, 114).Value = rpt.Header.ScheduledDate;
                            workSheet.Cell(5, 14).Value = $"Year {rpt.Header.Year}";
                            workSheet.Cell(6, 14).Value = getMonth(rpt.Header.Quarter.Value, 1);
                            workSheet.Cell(6, 42).Value = getMonth(rpt.Header.Quarter.Value, 2);
                            workSheet.Cell(6, 77).Value = getMonth(rpt.Header.Quarter.Value, 3);

                            for (int j = 0; j < rpt.Header.WeekNo.Length; j++)
                            {
                                if (j > 0 && workSheet.Cell(8, weekIndex - 7).Value.ToString() != rpt.Header.WeekNo[j].ToString())
                                {
                                    workSheet.Cell(8, weekIndex).Value = rpt.Header.WeekNo[j];
                                    weekNo.Add(rpt.Header.WeekNo[j], weekIndex);
                                    weekIndex += 7;
                                }
                                if (j == 0)
                                {
                                    workSheet.Cell(8, weekIndex).Value = rpt.Header.WeekNo[j];
                                    weekNo.Add(rpt.Header.WeekNo[j], weekIndex);
                                    weekIndex += 7;

                                }
                            }

                            workSheet.Cell(52, 11).Value = rpt.Header.SubmittedBy;
                            workSheet.Cell(53, 11).Value = rpt.Header.SubmittedDesignation;
                            workSheet.Cell(54, 11).Value = rpt.Header.SubmittedDate;

                            workSheet.Cell(52, 31).Value = rpt.Header.VettedBy;
                            workSheet.Cell(53, 31).Value = rpt.Header.VettedDesignation;
                            workSheet.Cell(54, 31).Value = rpt.Header.VettedDate;

                            workSheet.Cell(52, 89).Value = rpt.Header.AgreedBy;
                            workSheet.Cell(53, 89).Value = rpt.Header.AgreedDesignation;
                            workSheet.Cell(54, 89).Value = rpt.Header.AgreedDate;

                            int i = 8;

                            var data = rpt.Details.Skip((sheet - 1) * 27).Take(27);
                            foreach (var r in data)
                            {

                                i++;
                                if (i > 27) { }
                                else
                                {
                                    workSheet.Cell(i, 1).Value = r.RoadCode;
                                    workSheet.Cell(i, 2).Value = r.RoadName;
                                    workSheet.Cell(i, 4).Value = r.PaveLength;
                                    workSheet.Cell(i, 5).Value = r.UnPavedLength;
                                    workSheet.Cell(i, 6).Value = r.RoadLocationSeq;
                                    if (r.CIL.GetValueOrDefault())
                                    {

                                        workSheet.Cell(i, 7).Value = r.WorkQty;
                                    }
                                    if (r.PriorityI.GetValueOrDefault())
                                    {

                                        workSheet.Cell(i, 8).Value = r.WorkQty;
                                    }
                                    if (r.PriorityII.GetValueOrDefault())
                                    {

                                        workSheet.Cell(i, 9).Value = r.WorkQty;
                                    }

                                    workSheet.Cell(i, 11).Value = r.ADP;
                                    workSheet.Cell(i, 11).Value = r.CrewdayRequired;
                                    workSheet.Cell(i, 13).Value = (r.CrewdaysAllocated * r.CrewdayRequired);
                                    for (int week = 0; week < r.week.Length; week++)
                                    {
                                        int s = weekNo[r.week[week]];

                                        workSheet.Cell(i, s).Style.Fill.PatternType = XLFillPatternValues.DarkTrellis;
                                        workSheet.Cell(i, s).Style.Fill.PatternColor = XLColor.Gray;
                                        workSheet.Cell(i, s).Style.Fill.SetBackgroundColor(XLColor.White);

                                        workSheet.Cell(i, s + 1).Style.Fill.PatternType = XLFillPatternValues.DarkTrellis;
                                        workSheet.Cell(i, s + 1).Style.Fill.PatternColor = XLColor.Gray;
                                        workSheet.Cell(i, s + 1).Style.Fill.SetBackgroundColor(XLColor.White);

                                        workSheet.Cell(i, s + 2).Style.Fill.PatternType = XLFillPatternValues.DarkTrellis;
                                        workSheet.Cell(i, s + 2).Style.Fill.PatternColor = XLColor.Gray;
                                        workSheet.Cell(i, s + 2).Style.Fill.SetBackgroundColor(XLColor.White);

                                        workSheet.Cell(i, s + 3).Style.Fill.PatternType = XLFillPatternValues.DarkTrellis;
                                        workSheet.Cell(i, s + 3).Style.Fill.PatternColor = XLColor.Gray;
                                        workSheet.Cell(i, s + 3).Style.Fill.SetBackgroundColor(XLColor.White);

                                        workSheet.Cell(i, s + 4).Style.Fill.PatternType = XLFillPatternValues.DarkTrellis;
                                        workSheet.Cell(i, s + 4).Style.Fill.PatternColor = XLColor.Gray;
                                        workSheet.Cell(i, s + 4).Style.Fill.SetBackgroundColor(XLColor.White);

                                        workSheet.Cell(i, s + 5).Style.Fill.PatternType = XLFillPatternValues.DarkTrellis;
                                        workSheet.Cell(i, s + 5).Style.Fill.PatternColor = XLColor.Gray;
                                        workSheet.Cell(i, s + 5).Style.Fill.SetBackgroundColor(XLColor.White);

                                        workSheet.Cell(i, s + 6).Style.Fill.PatternType = XLFillPatternValues.DarkTrellis;
                                        workSheet.Cell(i, s + 6).Style.Fill.PatternColor = XLColor.Gray;
                                        workSheet.Cell(i, s + 6).Style.Fill.SetBackgroundColor(XLColor.White);


                                    }
                                    workSheet.Cell(i, 112).Value = r.Target;
                                    workSheet.Cell(i, 114).Value = r.Remark;
                                    workSheet.Row(i).ClearHeight();
                                }
                                index++;
                            }
                        }

                    }

                    workBook.FullCalculationOnLoad = true;
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

        public bool RemoveHeader(int id)
        {
            RmFormS2Hdr hdr = _repoUnit.FormS2Repository.Find(s => s.FsiihPkRefNo == id);
            hdr.FsiihActiveYn = false;
            return _repoUnit.Commit() > 0;

        }

        public bool RemoveDetail(int id)
        {
            RmFormS2Dtl dtl = _repoUnit.FormS2DetailRepository.Find(s => s.FsiidPkRefNo == id);
            dtl.FsiidActiveYn = false;
            return _repoUnit.Commit() > 0;
        }
        public async Task<IEnumerable<object>> GetActiveRefId(int activityCode, int roadCodeId)
        {
            return await _repoUnit.FormS2DetailRepository.GetActiveRefId(activityCode, roadCodeId);
        }

        public async Task<bool> CheckS2DtlExistance(int headerId, int rdCode)
        {
            return await _repoUnit.FormS2DetailRepository.CheckExistance(headerId, rdCode);
        }
    }
}
