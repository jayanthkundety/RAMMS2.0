using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClosedXML.Excel;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.Repository.Interfaces;
using static RAMMS.DTO.RequestBO.FormS1DetailDTO;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormS1Service : IFormS1Service
    {
        private IFormS1Repository repoFromS1;
        private IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly IProcessService processService;
        public FormS1Service(IFormS1Repository repo, IRepositoryUnit repositoryUnit, IMapper mapper, IProcessService proService)
        {
            repoFromS1 = repo;
            _repoUnit = repositoryUnit;
            _mapper = mapper;
            processService = proService;
        }
        public FormS1HeaderRequestDTO SaveHeader(FormS1HeaderRequestDTO headerDTO, bool updateSubmit)
        {
            return SaveHeader<FormS1HeaderRequestDTO>(headerDTO, updateSubmit);
        }
        public T SaveHeader<T>(T headerDTO, bool updateSubmit)
        {
            RmFormS1Hdr header = _mapper.Map<RmFormS1Hdr>(headerDTO);
            header.FsihStatus = Common.StatusList.S1Init;
            var entity = repoFromS1.SaveFormS1Hdr(header, updateSubmit);
            if (entity.FsihSubmitSts == true)
            {
                int iResult = processService.Save(new ProcessDTO()
                {
                    ApproveDate = DateTime.Now,
                    Form = "FormS1",
                    IsApprove = true,
                    RefId = entity.FsihPkRefNo,
                    Remarks = "",
                    Stage = entity.FsihStatus
                }).Result;

            }
            return _mapper.Map<T>(entity);
        }

        public FormS1DetailDTO SaveDetails(FormS1DetailDTO formS1DetailsDTO)
        {
            RmFormS1Dtl formS1Details = _mapper.Map<RmFormS1Dtl>(formS1DetailsDTO);

            if (formS1DetailsDTO.PkRefNo > 0)
            {
                var objDtl = repoFromS1.SaveDetails(formS1Details);
                var result = _mapper.Map<FormS1DetailDTO>(objDtl);
                result.IsExist = true;
                return result;
            }
            else
            {
                (int id, bool alreadyExists) isExist = repoFromS1.CheckAlreadyExists(formS1Details.FsiidRoadCode, formS1Details.FsidActCode, Convert.ToInt32(formS1Details.FsidFrmChKm), formS1Details.FsidFrmChM, Convert.ToInt32(formS1Details.FsidToChKm), formS1Details.FsidToChM, formS1DetailsDTO.HdrWeekNo);
                if (!isExist.alreadyExists)
                {
                    var objDtl = repoFromS1.SaveDetails(formS1Details);
                    var result = _mapper.Map<FormS1DetailDTO>(objDtl);
                    result.IsExist = true;
                    return result;

                }
                else
                {
                    var result = _mapper.Map<FormS1DetailDTO>(formS1Details);
                    result.IsExist = false;
                    return result;
                }
            }
        }

        public FormS1HeaderRequestDTO FindDetails(FormS1HeaderRequestDTO headerDTO)
        {
            RmFormS1Hdr header = _mapper.Map<RmFormS1Hdr>(headerDTO);
            var obj = repoFromS1.FindAsync(x => x.FsihRmu == header.FsihRmu && x.FsihDt == header.FsihDt && x.FsihWeekNo == header.FsihWeekNo && x.FsihActiveYn == true).Result;
            return _mapper.Map<FormS1HeaderRequestDTO>(obj);
        }
        public FormS1HeaderRequestDTO FindHeaderByID(int headerId)
        {
            return FindHeaderByID<FormS1HeaderRequestDTO>(headerId);
        }
        public T FindHeaderByID<T>(int headerId)
        {
            var obj = repoFromS1.FindAsync(x => x.FsihPkRefNo == headerId).Result;
            return _mapper.Map<RmFormS1Hdr, T>(obj);
        }
        public FormS1DetailDTO FindDetailsById(int detailPKId)
        {
            var obj = repoFromS1.FindDetailsById(detailPKId).Result;
            return _mapper.Map<RmFormS1Dtl, FormS1DetailDTO>(obj);
        }
        public async Task<GridWrapper<object>> GetHeaderGrid(DataTableAjaxPostModel searchData)
        {
            return await repoFromS1.GetHeaderGrid(searchData);
        }
        public async Task<GridWrapper<object>> GetDetailsGrid(int headerId, DataTableAjaxPostModel searchData)
        {
            return await repoFromS1.GetDetailsGrid(headerId, searchData);
        }
        public int DeleteFormS1Hdr(int id)
        {
            if (id > 0)
            {
                id = repoFromS1.DeleteFormS1Hdr(new RmFormS1Hdr() { FsihActiveYn = false, FsihPkRefNo = id });
            }
            return id;
        }
        public int DeleteDetails(int id)
        {
            if (id > 0)
            {
                id = repoFromS1.DeleteDetail(new RmFormS1Dtl() { FsidActiveYn = false, FsidPkRefNo = id });
            }
            return id;
        }

        public FORMS1Rpt GetReportData(int headerId)
        {
            return repoFromS1.GetReportData(headerId);
        }
        private Dictionary<int, string> scheduledVlaue = new Dictionary<int, string>
        {
            { 0, "" },
            { 1, "P" },
            { 2, "⃝" },
            { 3, "P" },
            { 4, "℗" },
            { 5, "R" }
        };

        public Byte[] FormDownload(string formName, int id, string basePath, string filePath)
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
                FORMS1Rpt rpt = this.GetReportData(id);
                System.IO.File.Copy(oldFileName, cacheFile, true);
                using (var workBook = new XLWorkbook(cacheFile))
                {

                    int noOfSheets = (rpt.Details.Count() / 17) + ((rpt.Details.Count() % 17) > 0 ? 1 : 1);
                    for (int sheet = 2; sheet <= noOfSheets; sheet++)
                    {
                        using (var tempWorkBook = new XLWorkbook(cacheFile))
                        {
                            string sheetName = "sheet" + Convert.ToString(sheet) + "(Weekly Sch)";
                            IXLWorksheet copySheet = tempWorkBook.Worksheet(1);
                            copySheet.Worksheet.Name = sheetName;
                            workBook.AddWorksheet(copySheet);
                            copySheet.Worksheet.Name = "sheet" + Convert.ToString(sheet) + "(Weekly Act)"; ;
                            workBook.AddWorksheet(copySheet);
                        }
                    }
                    int index = 1;
                    for (int sheet = 1; sheet <= noOfSheets; sheet += 2)
                    {


                        IXLWorksheet plannedWorkSheet =
                        workBook.Worksheet(sheet);
                        IXLWorksheet actualWorkSheet =
                        workBook.Worksheet(sheet + 1);

                        if (actualWorkSheet != null && plannedWorkSheet != null)
                        {
                            actualWorkSheet.Cell(9, 2).Value = rpt.Header.RMU;
                            actualWorkSheet.Cell(9, 10).Value = rpt.Header.Date.HasValue ? rpt.Header.Date.Value.ToString("dd-MM-yyyy") : "";
                            actualWorkSheet.Cell(11, 3).Value = rpt.Header.WeekNo;
                            actualWorkSheet.Cell(11, 36).Value = sheet;
                            actualWorkSheet.Cell(11, 38).Value = noOfSheets;
                            actualWorkSheet.Cell(34, 31).Value = rpt.Header.PlannedBy;
                            actualWorkSheet.Cell(36, 31).Value = rpt.Header.VettedBy;
                            actualWorkSheet.Cell(38, 31).Value = rpt.Header.AgreedBy;
                            actualWorkSheet.Cell(34, 1).Value = rpt.Header.Remarks;
                            actualWorkSheet.Cell(11, 7).Value = rpt.Header.From;
                            actualWorkSheet.Cell(11, 11).Value = rpt.Header.To;

                            plannedWorkSheet.Cell(9, 2).Value = rpt.Header.RMU;
                            plannedWorkSheet.Cell(9, 10).Value = rpt.Header.Date.HasValue ? rpt.Header.Date.Value.ToString("dd-MM-yyyy") : "";
                            plannedWorkSheet.Cell(11, 3).Value = rpt.Header.WeekNo;
                            plannedWorkSheet.Cell(11, 36).Value = sheet;
                            plannedWorkSheet.Cell(11, 38).Value = noOfSheets;
                            plannedWorkSheet.Cell(34, 31).Value = rpt.Header.PlannedBy;
                            plannedWorkSheet.Cell(36, 31).Value = rpt.Header.VettedBy;
                            plannedWorkSheet.Cell(38, 31).Value = rpt.Header.AgreedBy;
                            plannedWorkSheet.Cell(34, 1).Value = rpt.Header.Remarks;
                            plannedWorkSheet.Cell(11, 7).Value = rpt.Header.From;
                            plannedWorkSheet.Cell(11, 11).Value = rpt.Header.To;
                            int i = 14;

                            var data = rpt.Details.Skip((sheet - 1) * 17).Take(17);
                            foreach (var r in data)
                            {

                                i++;
                                actualWorkSheet.Cell(i, 1).Value = r.ActivityCode;
                                actualWorkSheet.Cell(i, 2).Value = r.RoadCode;
                                actualWorkSheet.Cell(i, 4).Value = r.RoadName;
                                actualWorkSheet.Cell(i, 9).Value = $"{r.ChainageFromKm}+{r.ChainageFromM}";
                                actualWorkSheet.Cell(i, 11).Value = $"{r.ChainageToKm}+{r.ChainageToM}";
                                actualWorkSheet.Cell(i, 12).Value = r.SiteRef;
                                actualWorkSheet.Cell(i, 13).Value = r.Priority;
                                actualWorkSheet.Cell(i, 14).Value = r.Qty;
                                actualWorkSheet.Cell(i, 15).Value = r.CDR;
                                actualWorkSheet.Cell(i, 16).Value = r.CrewSupervisor;
                                string url = "";
                                if (r.IsRA.HasValue)
                                {
                                    url = scheduledVlaue[r.IsRA.Value];
                                    if (!string.IsNullOrEmpty(url))
                                    {
                                        IXLStyle style = null;
                                        switch (r.IsRA.Value)
                                        {
                                            case 1:
                                                style = actualWorkSheet.Cell(33, 25).Style;
                                                break;
                                            case 2:
                                                style = actualWorkSheet.Cell(34, 25).Style;
                                                break;
                                            case 3:
                                                style = actualWorkSheet.Cell(35, 25).Style;
                                                break;
                                            case 4:
                                                style = actualWorkSheet.Cell(36, 25).Style;
                                                break;
                                            case 5:
                                                style = actualWorkSheet.Cell(37, 25).Style;
                                                break;
                                        }
                                        actualWorkSheet.Cell(i, 26).Value = url;
                                        actualWorkSheet.Cell(i, 26).Style = style;
                                        actualWorkSheet.Cell(i, 26).Style.Fill.SetBackgroundColor(XLColor.White);
                                        actualWorkSheet.Cell(i, 26).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        actualWorkSheet.Cell(i, 26).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                        actualWorkSheet.Cell(i, 26).Style.Border.OutsideBorder = XLBorderStyleValues.Dotted;
                                    }
                                }


                                if (r.IsMT.HasValue && r.IsMT.GetValueOrDefault() != 0)
                                {
                                    url = scheduledVlaue[r.IsMT.Value];
                                    if (!string.IsNullOrEmpty(url))
                                    {
                                        IXLStyle style = null;
                                        switch (r.IsMT.Value)
                                        {
                                            case 1:
                                                style = actualWorkSheet.Cell(33, 25).Style;
                                                break;
                                            case 2:
                                                style = actualWorkSheet.Cell(34, 25).Style;
                                                break;
                                            case 3:
                                                style = actualWorkSheet.Cell(35, 25).Style;
                                                break;
                                            case 4:
                                                style = actualWorkSheet.Cell(36, 25).Style;
                                                break;
                                            case 5:
                                                style = actualWorkSheet.Cell(37, 25).Style;
                                                break;
                                        }
                                        actualWorkSheet.Cell(i, 27).Value = url;
                                        actualWorkSheet.Cell(i, 27).Style = style;
                                        actualWorkSheet.Cell(i, 27).Style.Fill.SetBackgroundColor(XLColor.White);
                                        actualWorkSheet.Cell(i, 27).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        actualWorkSheet.Cell(i, 27).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                        actualWorkSheet.Cell(i, 27).Value = url;
                                        actualWorkSheet.Cell(i, 27).Style.Border.OutsideBorder = XLBorderStyleValues.Dotted;
                                    }
                                }

                                if (r.IsQA1.HasValue)
                                {
                                    url = scheduledVlaue[r.IsQA1.Value];
                                    if (!string.IsNullOrEmpty(url))
                                    {
                                        //byte[] buff = File.ReadAllBytes($"{basePath}/{url}");
                                        //System.IO.MemoryStream str = new System.IO.MemoryStream(buff);
                                        //actualWorkSheet.AddPicture(str).MoveTo(actualWorkSheet.Cell(i, 28)).WithSize(15, 15);
                                        actualWorkSheet.Cell(i, 28).Value = url;
                                        IXLStyle style = null;
                                        switch (r.IsQA1.Value)
                                        {
                                            case 1:
                                                style = actualWorkSheet.Cell(33, 25).Style;
                                                break;
                                            case 2:
                                                style = actualWorkSheet.Cell(34, 25).Style;
                                                break;
                                            case 3:
                                                style = actualWorkSheet.Cell(35, 25).Style;
                                                break;
                                            case 4:
                                                style = actualWorkSheet.Cell(36, 25).Style;
                                                break;
                                            case 5:
                                                style = actualWorkSheet.Cell(37, 25).Style;
                                                break;
                                        }
                                        actualWorkSheet.Cell(i, 28).Value = url;
                                        actualWorkSheet.Cell(i, 28).Style = style;
                                        actualWorkSheet.Cell(i, 28).Style.Fill.SetBackgroundColor(XLColor.White);
                                        actualWorkSheet.Cell(i, 28).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        actualWorkSheet.Cell(i, 28).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                        actualWorkSheet.Cell(i, 28).Style.Border.OutsideBorder = XLBorderStyleValues.Dotted;
                                    }
                                }

                                if (r.IsQA2.HasValue)
                                {
                                    url = scheduledVlaue[r.IsQA2.Value];
                                    if (!string.IsNullOrEmpty(url))
                                    {

                                        actualWorkSheet.Cell(i, 29).Value = url;
                                        IXLStyle style = null;
                                        switch (r.IsQA2.Value)
                                        {
                                            case 1:
                                                style = actualWorkSheet.Cell(33, 25).Style;
                                                break;
                                            case 2:
                                                style = actualWorkSheet.Cell(34, 25).Style;
                                                break;
                                            case 3:
                                                style = actualWorkSheet.Cell(35, 25).Style;
                                                break;
                                            case 4:
                                                style = actualWorkSheet.Cell(36, 25).Style;
                                                break;
                                            case 5:
                                                style = actualWorkSheet.Cell(37, 25).Style;
                                                break;
                                        }
                                        actualWorkSheet.Cell(i, 29).Style = style;
                                        actualWorkSheet.Cell(i, 29).Style.Fill.SetBackgroundColor(XLColor.White);
                                        actualWorkSheet.Cell(i, 29).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        actualWorkSheet.Cell(i, 29).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                        actualWorkSheet.Cell(i, 29).Style.Border.OutsideBorder = XLBorderStyleValues.Dotted;
                                    }
                                }

                                if (r.IsSA.HasValue)
                                {
                                    url = scheduledVlaue[r.IsSA.Value];
                                    if (!string.IsNullOrEmpty(url))
                                    {
                                        IXLStyle style = null;
                                        switch (r.IsSA.Value)
                                        {
                                            case 1:
                                                style = actualWorkSheet.Cell(33, 25).Style;
                                                break;
                                            case 2:
                                                style = actualWorkSheet.Cell(34, 25).Style;
                                                break;
                                            case 3:
                                                style = actualWorkSheet.Cell(35, 25).Style;
                                                break;
                                            case 4:
                                                style = actualWorkSheet.Cell(36, 25).Style;
                                                break;
                                            case 5:
                                                style = actualWorkSheet.Cell(37, 25).Style;
                                                break;
                                        }
                                        actualWorkSheet.Cell(i, 30).Value = url;
                                        actualWorkSheet.Cell(i, 30).Style = style;
                                        actualWorkSheet.Cell(i, 30).Style.Fill.SetBackgroundColor(XLColor.White);
                                        actualWorkSheet.Cell(i, 30).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        actualWorkSheet.Cell(i, 30).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                        actualWorkSheet.Cell(i, 30).Style.Border.OutsideBorder = XLBorderStyleValues.Dotted;
                                    }
                                }

                                if (r.N1.HasValue)
                                {
                                    url = scheduledVlaue[r.N1.Value];
                                    if (!string.IsNullOrEmpty(url))
                                    {

                                        IXLStyle style = null;
                                        switch (r.N1.Value)
                                        {
                                            case 1:
                                                style = actualWorkSheet.Cell(33, 25).Style;
                                                break;
                                            case 2:
                                                style = actualWorkSheet.Cell(34, 25).Style;
                                                break;
                                            case 3:
                                                style = actualWorkSheet.Cell(35, 25).Style;
                                                break;
                                            case 4:
                                                style = actualWorkSheet.Cell(36, 25).Style;
                                                break;
                                            case 5:
                                                style = actualWorkSheet.Cell(37, 25).Style;
                                                break;
                                        }
                                        actualWorkSheet.Cell(i, 31).Value = url;
                                        actualWorkSheet.Cell(i, 31).Style = style;
                                        actualWorkSheet.Cell(i, 31).Style.Fill.SetBackgroundColor(XLColor.White);
                                        actualWorkSheet.Cell(i, 31).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        actualWorkSheet.Cell(i, 31).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                        actualWorkSheet.Cell(i, 31).Style.Border.OutsideBorder = XLBorderStyleValues.Dotted;
                                    }
                                }

                                if (r.N2.HasValue)
                                {
                                    url = scheduledVlaue[r.N2.Value];
                                    if (!string.IsNullOrEmpty(url))
                                    {
                                        IXLStyle style = null;
                                        switch (r.N2.Value)
                                        {
                                            case 1:
                                                style = actualWorkSheet.Cell(33, 25).Style;
                                                break;
                                            case 2:
                                                style = actualWorkSheet.Cell(34, 25).Style;
                                                break;
                                            case 3:
                                                style = actualWorkSheet.Cell(35, 25).Style;
                                                break;
                                            case 4:
                                                style = actualWorkSheet.Cell(36, 25).Style;
                                                break;
                                            case 5:
                                                style = actualWorkSheet.Cell(37, 25).Style;
                                                break;
                                        }
                                        actualWorkSheet.Cell(i, 32).Value = url;
                                        actualWorkSheet.Cell(i, 32).Style = style;
                                        actualWorkSheet.Cell(i, 32).Style.Fill.SetBackgroundColor(XLColor.White);
                                        actualWorkSheet.Cell(i, 32).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                        actualWorkSheet.Cell(i, 32).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                                        actualWorkSheet.Cell(i, 32).Style.Border.OutsideBorder = XLBorderStyleValues.Dotted;
                                    }
                                }
                                actualWorkSheet.Cell(i, 33).Value = r.Remarks;

                                plannedWorkSheet.Cell(i, 1).Value = r.ActivityCode;
                                plannedWorkSheet.Cell(i, 2).Value = r.RoadCode;
                                plannedWorkSheet.Cell(i, 4).Value = r.RoadName;
                                plannedWorkSheet.Cell(i, 9).Value = $"{r.ChainageFromKm}+{r.ChainageFromM}";
                                plannedWorkSheet.Cell(i, 11).Value = $"{r.ChainageToKm}+{r.ChainageToM}";
                                plannedWorkSheet.Cell(i, 12).Value = r.SiteRef;
                                plannedWorkSheet.Cell(i, 13).Value = r.Priority;
                                plannedWorkSheet.Cell(i, 14).Value = r.Qty;
                                plannedWorkSheet.Cell(i, 15).Value = r.CDR;
                                plannedWorkSheet.Cell(i, 16).Value = r.CrewSupervisor;
                                if (r.Scheduled.Count() > 0)
                                {
                                    plannedWorkSheet.Cell(i, 19).Value = r.Scheduled.Any(s => s.DayOfTheWeek == 1) ? "P" : "";
                                    plannedWorkSheet.Cell(i, 20).Value = r.Scheduled.Any(s => s.DayOfTheWeek == 2) ? "P" : "";
                                    plannedWorkSheet.Cell(i, 21).Value = r.Scheduled.Any(s => s.DayOfTheWeek == 3) ? "P" : "";
                                    plannedWorkSheet.Cell(i, 22).Value = r.Scheduled.Any(s => s.DayOfTheWeek == 4) ? "P" : "";
                                    plannedWorkSheet.Cell(i, 23).Value = r.Scheduled.Any(s => s.DayOfTheWeek == 5) ? "P" : "";
                                    plannedWorkSheet.Cell(i, 24).Value = r.Scheduled.Any(s => s.DayOfTheWeek == 6) ? "P" : "";
                                    plannedWorkSheet.Cell(i, 25).Value = r.Scheduled.Any(s => s.DayOfTheWeek == 0) ? "P" : "";
                                }
                                if (r.Planned.Count() > 0)
                                {
                                    if (r.Planned.Any(s => s.DayOfTheWeek == 1))
                                    {
                                        url = scheduledVlaue[r.Planned.Where(s => s.DayOfTheWeek == 1).Select(s => s.Value).First()];
                                        if (!string.IsNullOrEmpty(url))
                                        {
                                            actualWorkSheet.Cell(i, 19).Value = url;
                                            IXLStyle style = null;
                                            switch (r.Planned.Where(s => s.DayOfTheWeek == 1).Select(s => s.Value).First())
                                            {
                                                case 1:
                                                    style = actualWorkSheet.Cell(33, 25).Style;
                                                    break;
                                                case 2:
                                                    style = actualWorkSheet.Cell(34, 25).Style;
                                                    break;
                                                case 3:
                                                    style = actualWorkSheet.Cell(35, 25).Style;
                                                    break;
                                                case 4:
                                                    style = actualWorkSheet.Cell(36, 25).Style;
                                                    break;
                                                case 5:
                                                    style = actualWorkSheet.Cell(37, 25).Style;
                                                    break;
                                            }
                                            actualWorkSheet.Cell(i, 19).Style = style;
                                            actualWorkSheet.Cell(i, 19).Style.Fill.SetBackgroundColor(XLColor.White);
                                            actualWorkSheet.Cell(i, 19).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                            actualWorkSheet.Cell(i, 19).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                            actualWorkSheet.Cell(i, 19).Style.Border.OutsideBorder = XLBorderStyleValues.Dotted;
                                        }
                                    }

                                    if (r.Planned.Any(s => s.DayOfTheWeek == 2))
                                    {
                                        url = scheduledVlaue[r.Planned.Where(s => s.DayOfTheWeek == 2).Select(s => s.Value).First()];
                                        if (!string.IsNullOrEmpty(url))
                                        {

                                            actualWorkSheet.Cell(i, 20).Value = url;
                                            IXLStyle style = null;
                                            switch (r.Planned.Where(s => s.DayOfTheWeek == 2).Select(s => s.Value).First())
                                            {
                                                case 1:
                                                    style = actualWorkSheet.Cell(33, 25).Style;
                                                    break;
                                                case 2:
                                                    style = actualWorkSheet.Cell(34, 25).Style;
                                                    break;
                                                case 3:
                                                    style = actualWorkSheet.Cell(35, 25).Style;
                                                    break;
                                                case 4:
                                                    style = actualWorkSheet.Cell(36, 25).Style;
                                                    break;
                                                case 5:
                                                    style = actualWorkSheet.Cell(37, 25).Style;
                                                    break;
                                            }
                                            actualWorkSheet.Cell(i, 20).Style = style;
                                            actualWorkSheet.Cell(i, 20).Style.Fill.SetBackgroundColor(XLColor.White);
                                            actualWorkSheet.Cell(i, 20).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                            actualWorkSheet.Cell(i, 20).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                            actualWorkSheet.Cell(i, 20).Style.Border.OutsideBorder = XLBorderStyleValues.Dotted;
                                        }
                                    }
                                    if (r.Planned.Any(s => s.DayOfTheWeek == 3))
                                    {
                                        url = scheduledVlaue[r.Planned.Where(s => s.DayOfTheWeek == 3).Select(s => s.Value).First()];
                                        if (!string.IsNullOrEmpty(url))
                                        {

                                            actualWorkSheet.Cell(i, 21).Value = url;
                                            IXLStyle style = null;
                                            switch (r.Planned.Where(s => s.DayOfTheWeek == 3).Select(s => s.Value).First())
                                            {
                                                case 1:
                                                    style = actualWorkSheet.Cell(33, 25).Style;
                                                    break;
                                                case 2:
                                                    style = actualWorkSheet.Cell(34, 25).Style;
                                                    break;
                                                case 3:
                                                    style = actualWorkSheet.Cell(35, 25).Style;
                                                    break;
                                                case 4:
                                                    style = actualWorkSheet.Cell(36, 25).Style;
                                                    break;
                                                case 5:
                                                    style = actualWorkSheet.Cell(37, 25).Style;
                                                    break;
                                            }
                                            actualWorkSheet.Cell(i, 21).Style = style;
                                            actualWorkSheet.Cell(i, 21).Style.Fill.SetBackgroundColor(XLColor.White);
                                            actualWorkSheet.Cell(i, 21).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                            actualWorkSheet.Cell(i, 21).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                            actualWorkSheet.Cell(i, 21).Style.Border.OutsideBorder = XLBorderStyleValues.Dotted;

                                        }
                                    }
                                    if (r.Planned.Any(s => s.DayOfTheWeek == 4))
                                    {
                                        url = scheduledVlaue[r.Planned.Where(s => s.DayOfTheWeek == 4).Select(s => s.Value).First()];
                                        if (!string.IsNullOrEmpty(url))
                                        {

                                            actualWorkSheet.Cell(i, 22).Value = url;
                                            IXLStyle style = null;
                                            switch (r.Planned.Where(s => s.DayOfTheWeek == 4).Select(s => s.Value).First())
                                            {
                                                case 1:
                                                    style = actualWorkSheet.Cell(33, 25).Style;
                                                    break;
                                                case 2:
                                                    style = actualWorkSheet.Cell(34, 25).Style;
                                                    break;
                                                case 3:
                                                    style = actualWorkSheet.Cell(35, 25).Style;
                                                    break;
                                                case 4:
                                                    style = actualWorkSheet.Cell(36, 25).Style;
                                                    break;
                                                case 5:
                                                    style = actualWorkSheet.Cell(37, 25).Style;
                                                    break;
                                            }
                                            actualWorkSheet.Cell(i, 22).Style = style;
                                            actualWorkSheet.Cell(i, 22).Style.Fill.SetBackgroundColor(XLColor.White);
                                            actualWorkSheet.Cell(i, 22).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                            actualWorkSheet.Cell(i, 22).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                            actualWorkSheet.Cell(i, 22).Style.Border.OutsideBorder = XLBorderStyleValues.Dotted;
                                        }
                                    }
                                    if (r.Planned.Any(s => s.DayOfTheWeek == 5))
                                    {
                                        url = scheduledVlaue[r.Planned.Where(s => s.DayOfTheWeek == 5).Select(s => s.Value).First()];
                                        if (!string.IsNullOrEmpty(url))
                                        {

                                            actualWorkSheet.Cell(i, 23).Value = url;
                                            IXLStyle style = null;
                                            switch (r.Planned.Where(s => s.DayOfTheWeek == 5).Select(s => s.Value).First())
                                            {
                                                case 1:
                                                    style = actualWorkSheet.Cell(33, 25).Style;
                                                    break;
                                                case 2:
                                                    style = actualWorkSheet.Cell(34, 25).Style;
                                                    break;
                                                case 3:
                                                    style = actualWorkSheet.Cell(35, 25).Style;
                                                    break;
                                                case 4:
                                                    style = actualWorkSheet.Cell(36, 25).Style;
                                                    break;
                                                case 5:
                                                    style = actualWorkSheet.Cell(37, 25).Style;
                                                    break;
                                            }
                                            actualWorkSheet.Cell(i, 23).Style = style;
                                            actualWorkSheet.Cell(i, 23).Style.Fill.SetBackgroundColor(XLColor.White);
                                            actualWorkSheet.Cell(i, 23).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                            actualWorkSheet.Cell(i, 23).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                            actualWorkSheet.Cell(i, 23).Style.Border.OutsideBorder = XLBorderStyleValues.Dotted;
                                        }
                                    }
                                    if (r.Planned.Any(s => s.DayOfTheWeek == 6))
                                    {
                                        url = scheduledVlaue[r.Planned.Where(s => s.DayOfTheWeek == 6).Select(s => s.Value).First()];
                                        if (!string.IsNullOrEmpty(url))
                                        {

                                            actualWorkSheet.Cell(i, 24).Value = url;
                                            IXLStyle style = null;
                                            switch (r.Planned.Where(s => s.DayOfTheWeek == 6).Select(s => s.Value).First())
                                            {
                                                case 1:
                                                    style = actualWorkSheet.Cell(33, 25).Style;
                                                    break;
                                                case 2:
                                                    style = actualWorkSheet.Cell(34, 25).Style;
                                                    break;
                                                case 3:
                                                    style = actualWorkSheet.Cell(35, 25).Style;
                                                    break;
                                                case 4:
                                                    style = actualWorkSheet.Cell(36, 25).Style;
                                                    break;
                                                case 5:
                                                    style = actualWorkSheet.Cell(37, 25).Style;
                                                    break;
                                            }
                                            actualWorkSheet.Cell(i, 24).Style = style;
                                            actualWorkSheet.Cell(i, 24).Style.Fill.SetBackgroundColor(XLColor.White);
                                            actualWorkSheet.Cell(i, 24).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                            actualWorkSheet.Cell(i, 24).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                            actualWorkSheet.Cell(i, 24).Style.Border.OutsideBorder = XLBorderStyleValues.Dotted;
                                        }
                                    }

                                    if (r.Planned.Any(s => s.DayOfTheWeek == 0))
                                    {
                                        url = scheduledVlaue[r.Planned.Where(s => s.DayOfTheWeek == 0).Select(s => s.Value).First()];
                                        if (!string.IsNullOrEmpty(url))
                                        {
                                            IXLStyle style = null;
                                            switch (r.Planned.Where(s => s.DayOfTheWeek == 0).Select(s => s.Value).First())
                                            {
                                                case 1:
                                                    style = actualWorkSheet.Cell(33, 25).Style;
                                                    break;
                                                case 2:
                                                    style = actualWorkSheet.Cell(34, 25).Style;
                                                    break;
                                                case 3:
                                                    style = actualWorkSheet.Cell(35, 25).Style;
                                                    break;
                                                case 4:
                                                    style = actualWorkSheet.Cell(36, 25).Style;
                                                    break;
                                                case 5:
                                                    style = actualWorkSheet.Cell(37, 25).Style;
                                                    break;
                                            }
                                            actualWorkSheet.Cell(i, 25).Style = style;
                                            actualWorkSheet.Cell(i, 25).Style.Fill.SetBackgroundColor(XLColor.White);
                                            actualWorkSheet.Cell(i, 25).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                                            actualWorkSheet.Cell(i, 25).Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                                            actualWorkSheet.Cell(i, 25).Value = url;
                                            actualWorkSheet.Cell(i, 25).Style.Border.OutsideBorder = XLBorderStyleValues.Dotted;
                                        }
                                    }

                                }
                                plannedWorkSheet.Cell(i, 33).Value = r.Remarks;

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

        public async Task<RmFormS1Dtl> FindS1Details(int dtlId)
        {
            return await repoFromS1.GetDetailsById(dtlId);
        }

        public async Task<List<ActWeekDtl>> GetFormDDetails(string roadCode, string actCode, string frmCh, string frmChDeci, string toCh, string toChDeci, string crewSupervisor, string weekNo)
        {
            var Result = await repoFromS1.GetFormDDtls(roadCode, actCode, frmCh, frmChDeci, toCh, toChDeci, crewSupervisor, weekNo);
            return Result;
        }
    }
}
