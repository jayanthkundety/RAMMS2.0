using AutoMapper;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.JQueryModel;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using RAMMS.DTO.RequestBO;
using RAMMS.Domain.Models;
using RAMMS.DTO.Report;
using ClosedXML.Excel;
using System.Linq;
using System.IO;


namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormF5Service : IFormF5Service
    {
        private readonly IRepositoryUnit _repoUint;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private IFormF5Repository _formF5Repository;
        public FormF5Service(IRepositoryUnit repositoryUnit, IMapper mapper, ISecurity security, IFormF5Repository formF5Repository)
        {
            _repoUint = repositoryUnit;
            _mapper = mapper;
            _security = security;
            _formF5Repository = formF5Repository;
        }
        public async Task<GridWrapper<object>> GetFormF5HeaderGrid(DataTableAjaxPostModel searchData)
        {
            return await _repoUint.formF5Repository.GetFormF5GridHeader(searchData);
        }
        public async Task<GridWrapper<object>> GetFormF5DetailGrid(int headerId, DataTableAjaxPostModel searchData)
        {
            return await _repoUint.formF5Repository.GetFormF5GridDetail(headerId, searchData);
        }

        public async Task<FormF5HeaderRequestDTO> FindDetails(FormF5HeaderRequestDTO headerDTO)
        {
            RmFormF5InsHdr formF5Hdr = _mapper.Map<RmFormF5InsHdr>(headerDTO);
            var obj = await _formF5Repository.FindAsync(x => x.FvahRoadCode == formF5Hdr.FvahRoadCode && x.FvahYearOfInsp == formF5Hdr.FvahYearOfInsp && x.FvahActiveYn == true);
            return _mapper.Map<FormF5HeaderRequestDTO>(obj);
        }
        public async Task<FormF5HeaderRequestDTO> FindHeaderById(int headerId)
        {
            var result = await _repoUint.formF5Repository.FindAsync(x => x.FvahPkRefNo == headerId && x.FvahActiveYn == true);
            return _mapper.Map<FormF5HeaderRequestDTO>(result);
        }
        public async Task<FormF5HeaderRequestDTO> SaveHeader(FormF5HeaderRequestDTO headerDto, bool updateSubmitSts)
        {
            return await SaveHeader<FormF5HeaderRequestDTO>(headerDto, updateSubmitSts);
        }
        public async Task<T> SaveHeader<T>(T header, bool updateSubmitSts)
        {
            RmFormF5InsHdr hdr = _mapper.Map<RmFormF5InsHdr>(header);
            if (hdr.FvahPkRefNo == 0)
            {
                var masterData = await _repoUint.RoadmasterRepository.FindAsync(x => x.RdmRdCode == hdr.FvahRoadCode && x.RdmActiveYn == true);
                if (masterData != null)
                {
                    hdr.FvahRmuName = masterData.RdmRmuName;
                    hdr.FvahDivCode = masterData.RdmDivCode;
                }
            }
            var result = _mapper.Map<T>(await _formF5Repository.saveFormF5Hdr(hdr, updateSubmitSts));
            return result;
        }
        public async Task<int> SaveDetail(FormF5HeaderRequestDTO header)
        {
            FormF5DetailRequestDTO detailReq = new FormF5DetailRequestDTO();
            List<RmFormF5InsDtl> result = new List<RmFormF5InsDtl>();
            var dtlList = await _repoUint.FormB1B2HeaderRepository.FindAllAsync(d => d.FbrihAiRdCode == header.RoadCode && d.FbrihYearOfInsp == header.YearOfInsp && d.FbrihSubmitSts == true && d.FbrihActiveYn == true);
            if (dtlList != null)
            {
                foreach (var dtl in dtlList)
                {
                    var asset = await _repoUint.AllAssetRepository.FindAsync(a => a.AiAssetId == dtl.FbrihAiAssetId && a.AiActiveYn == true);
                    detailReq.hPkRefNo = header.PkRefNo;
                    detailReq.FbrihPkRefNo = dtl.FbrihPkRefNo;
                    detailReq.LocChKm = dtl.FbrihAiLocChKm;
                    detailReq.LocChM = dtl.FbrihAiLocChM;
                    detailReq.StrucCode = dtl.FbrihAiStrucCode;
                    if (asset != null)
                    {
                        var brName = string.IsNullOrEmpty(asset.AiBridgeName) ? asset.AiBridgeName : "";
                        detailReq.RiverName = dtl.FbrihAiRiverName + " " + brName;
                    }
                    else
                    {
                        detailReq.RiverName = null;
                    }
                    detailReq.Length = dtl.FbrihAiLength;
                    detailReq.Width = dtl.FbrihAiWidth;
                    detailReq.SpanCnt = dtl.FbrihAiSpanCnt;
                    detailReq.Condition = dtl.FbrihBridgeConditionRat;
                    detailReq.Remarks = dtl.FbrihSerProviderDefGenCom + " " + dtl.FbrihSerProviderDefFeedback;
                    detailReq.CrBy = _security.UserID;
                    detailReq.CrDt = DateTime.UtcNow;
                    detailReq.ModBy = _security.UserID;
                    detailReq.ModDt = DateTime.UtcNow;
                    detailReq.SubmitSts = false;
                    detailReq.ActiveYn = true;
                    result.Add(_mapper.Map<RmFormF5InsDtl>(detailReq));
                }
                return await _formF5Repository.saveDetail(result);
            }
            return 0;
        }

        public async Task<int> DeleteFormF5Hdr(int id)
        {
            var rowsAfftected = 0;
            if (id > 0)
            {
                RmFormF5InsHdr header = await _formF5Repository.FindAsync(h => h.FvahPkRefNo == id && h.FvahActiveYn == true);
                List<RmFormF5InsDtl> detailList = new List<RmFormF5InsDtl>();
                header.FvahActiveYn = false;
                header.FvahModBy = _security.UserID;
                header.FvahModDt = DateTime.UtcNow;
                rowsAfftected = await _formF5Repository.DeleteFormF5Hdr(header);
                if (rowsAfftected > 0)
                {
                    var result = await _repoUint.formF5Repository.GetDetailList(id);
                    if (result.Count > 0)
                    {
                        foreach (RmFormF5InsDtl dtl in result)
                        {
                            dtl.FvadActiveYn = false;
                            dtl.FvadModBy = _security.UserID;
                            dtl.FvadModDt = DateTime.UtcNow;
                            detailList.Add(dtl);
                        }
                        rowsAfftected = await _formF5Repository.DeleteFormF5Dtl(detailList);
                    }
                }
            }
            return rowsAfftected;
        }

        public async Task<FORMF5Rpt> GetReportData(int headerid)
        {
            return await _repoUint.formF5Repository.GetReportData(headerid);
        }

        public async Task<byte[]> FormDowload(string formName, int id, string filePath)
        {
            string oldFileName = "";
            string fileName = "";
            string cacheFile = "";
            if (!filePath.Contains(".xlsx"))
            {
                oldFileName = filePath;
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
                FORMF5Rpt rpt = await this.GetReportData(id);
                System.IO.File.Copy(oldFileName, cacheFile, true);
                using (var workbook = new XLWorkbook(cacheFile))
                {
                    int noofsheets = (rpt.Details.Count() / 24) + ((rpt.Details.Count() % 24) > 0 ? 1 : 1);
                    for (int sheet = 2; sheet <= noofsheets; sheet++)
                    {
                        using (var tempWorkBook = new XLWorkbook(cacheFile))
                        {
                            string sheetName = "sheet" + Convert.ToString(sheet);
                            IXLWorksheet copysheet = tempWorkBook.Worksheet(1);
                            copysheet.Worksheet.Name = sheetName;
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
                    double condition1 = 0;
                    double condition2 = 0;
                    double condition3 = 0;
                    for (int sheet = 1; sheet <= noofsheets; sheet++)
                    {
                        IXLWorksheet workSheet;
                        workbook.Worksheets.TryGetWorksheet($"sheet{sheet}", out workSheet);
                        if (workSheet != null)
                        {
                            workSheet.Cell(5, 7).Value = (rpt.Division == "MIRI" ? "Miri" : rpt.Division);
                            workSheet.Cell(5, 26).Value = (rpt.District == "MIRI" ? "Miri" : rpt.District);
                            workSheet.Cell(5, 47).Value = (rpt.RMU == "MIRI" ? "Miri" : rpt.RMU);
                            workSheet.Cell(6, 7).Value = rpt.RoadCode;
                            workSheet.Cell(7, 7).Value = rpt.RoadName;
                            workSheet.Cell(6, 26).Value = rpt.CrewLeader;
                            workSheet.Cell(5, 72).Value = rpt.InspectedByName;
                            workSheet.Cell(6, 72).Value = rpt.InspectedDate.HasValue ? rpt.InspectedDate.Value.ToString("dd-MM-yyyy") : "";
                            workSheet.Cell(7, 74).Value = rpt.RoadLength;
                            workSheet.Cell(2, 80).Value = noofsheets;
                            //workSheet.Cell(9, 8).Value = condition1.ToString() == "0" ? "" : condition1.ToString();
                            //workSheet.Cell(9, 24).Value = condition2.ToString() == "0" ? "" : condition1.ToString();
                            //workSheet.Cell(9, 45).Value = condition3.ToString() == "0" ? "" : condition1.ToString();
                            int i = 15;
                            var data = rpt.Details.Skip((sheet - 1) * 24).Take(24);
                            foreach (var r in data)
                            {
                                //condition1 += r.OverAllCondition == 1 ? r.TOTLength.GetValueOrDefault() : 0;
                                //condition2 += r.OverAllCondition == 2 ? r.TOTLength.GetValueOrDefault() : 0;
                                //condition3 += r.OverAllCondition == 3 ? r.TOTLength.GetValueOrDefault() : 0;
                                workSheet.Cell(i, 2).Value = index;
                                switch (r.StartingChM.Length)
                                {
                                    case 0:
                                        r.StartingChM = "000";
                                        break;
                                    case 2:
                                        r.StartingChM = r.StartingChM + "0";
                                        break;
                                    case 1:
                                        r.StartingChM = r.StartingChM + "00";
                                        break;
                                }
                                workSheet.Cell(i, 4).Value = $"{r.StartingChKm}+{r.StartingChM}";
                                workSheet.Cell(i, 8).Value = r.Code;
                                workSheet.Cell(i, 10).Value = r.BridgeRiverName;
                                workSheet.Cell(i, 17).Value = r.TOTLength;
                                workSheet.Cell(i, 24).Value = r.AvgWidth;
                                workSheet.Cell(i, 31).Value = r.NoOfSpan;
                                workSheet.Cell(i, 38).Value = r.OverAllCondition;
                                workSheet.Cell(i, 45).Value = r.Remarks;


                                index++;
                                i++;
                            }
                            //workSheet.Cell(37, 8).Value = condition1;
                            //workSheet.Cell(37, 24).Value = condition2;
                            //workSheet.Cell(37, 45).Value = condition3;
                        }
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        System.IO.File.Delete(cacheFile);
                        return content;
                    }
                }

            }
            catch (Exception Ex)
            {
                System.IO.File.Copy(oldFileName, cacheFile, true);
                using (var workbook = new XLWorkbook(cacheFile))
                {
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        System.IO.File.Delete(cacheFile);
                        return content;
                    }
                }
            }
        }
    }
}
