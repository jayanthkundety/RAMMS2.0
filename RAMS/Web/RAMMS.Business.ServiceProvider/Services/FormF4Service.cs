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

    public class FormF4Service : IFormF4Service
    {
        private readonly IRepositoryUnit _repoUint;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        private IFormF4Repository _formF4Repository;
        public FormF4Service(IRepositoryUnit repositoryUnit, IMapper mapper, ISecurity security, IFormF4Repository formF4Repository)
        {
            _repoUint = repositoryUnit;
            _mapper = mapper;
            _security = security;
            _formF4Repository = formF4Repository;
        }
        public async Task<GridWrapper<object>> GetFormF4HeaderGrid(DataTableAjaxPostModel searchData)
        {
            return await _repoUint.formF4Repository.GetFormF4GridHeader(searchData);
        }
        public async Task<GridWrapper<object>> GetFormF4DetailGrid(int headerId, DataTableAjaxPostModel searchData)
        {
            return await _repoUint.formF4Repository.GetFormF4GridDetail(headerId, searchData);
        }

        public async Task<FormF4HeaderRequestDTO> FindDetails(FormF4HeaderRequestDTO headerDTO)
        {
            RmFormF4InsHdr formF4Hdr = _mapper.Map<RmFormF4InsHdr>(headerDTO);
            var obj = await _formF4Repository.FindAsync(x => x.FivahRoadCode == formF4Hdr.FivahRoadCode && x.FivahYearOfInsp == formF4Hdr.FivahYearOfInsp && x.FivahActiveYn == true);
            return _mapper.Map<FormF4HeaderRequestDTO>(obj);
        }
        public async Task<FormF4HeaderRequestDTO> FindHeaderById(int headerId)
        {
            var result = await _repoUint.formF4Repository.FindAsync(x => x.FivahPkRefNo == headerId && x.FivahActiveYn == true);
            return _mapper.Map<FormF4HeaderRequestDTO>(result);
        }
        public async Task<FormF4HeaderRequestDTO> SaveHeader(FormF4HeaderRequestDTO headerDto, bool updateSubmitSts)
        {
            return await SaveHeader<FormF4HeaderRequestDTO>(headerDto, updateSubmitSts);
        }
        public async Task<T> SaveHeader<T>(T header, bool updateSubmitSts)
        {
            RmFormF4InsHdr hdr = _mapper.Map<RmFormF4InsHdr>(header);
            if (hdr.FivahPkRefNo == 0)
            {
                var masterData = await _repoUint.RoadmasterRepository.FindAsync(x => x.RdmRdCode == hdr.FivahRoadCode && x.RdmActiveYn == true);
                if (masterData != null)
                {
                    hdr.FivahRmuName = masterData.RdmRmuName;
                    hdr.FivahDivCode = masterData.RdmDivCode;
                }
            }
            var result = _mapper.Map<T>(await _formF4Repository.saveFormF4Hdr(hdr, updateSubmitSts));
            return result;
        }
        public async Task<int> SaveDetail(FormF4HeaderRequestDTO header)
        {
            FormF4DetailRequestDTO detailReq = new FormF4DetailRequestDTO();
            List<RmFormF4InsDtl> result = new List<RmFormF4InsDtl>();
            var dtlList = await _repoUint.formC1C2Repository.FindAllAsync(d => d.FcvihAiRdCode == header.RoadCode && d.FcvihYearOfInsp == header.YearOfInsp && d.FcvihSubmitSts == true && d.FcvihActiveYn == true);
            if (dtlList != null)
            {
                foreach (var dtl in dtlList)
                {
                    var asset = await _repoUint.AllAssetRepository.FindAsync(a => a.AiAssetId == dtl.FcvihAiAssetId && a.AiActiveYn == true);
                    detailReq.hPkRefNo = header.PkRefNo;
                    detailReq.FcvihPkRefNo = dtl.FcvihPkRefNo;
                    detailReq.LocChKm = dtl.FcvihAiLocChKm;
                    detailReq.LocChM = dtl.FcvihAiLocChM;
                    detailReq.StrucCode = dtl.FcvihAiStrucCode;
                    detailReq.IntelStruc = dtl.FcvihAiIntelStruc != null ? (dtl.FcvihAiIntelStruc.Contains("Headwall") ? true : false || dtl.FcvihAiIntelStruc.Contains("Wingwall") ? true : false) : false;
                    detailReq.OutletStruc = dtl.FcvihAiOutletStruc != null ? (dtl.FcvihAiOutletStruc.Contains("Headwall") ? true : false || dtl.FcvihAiOutletStruc.Contains("Wingwall") ? true : false) : false;
                    detailReq.Length = dtl.FcvihAiLength;
                    detailReq.BarrelNo = dtl.FcvihAiBarrelNo;
                    if (asset != null)
                    {
                        detailReq.Width = asset.AiWidth.HasValue ? asset.AiWidth : null;
                        detailReq.Height = asset.AiHeight.HasValue ? asset.AiHeight : null;
                    }
                    else
                    {
                        detailReq.Width = null;
                        detailReq.Height = null;
                    }
                    detailReq.Condition = dtl.FcvihCulvertConditionRat;
                    detailReq.Remarks = dtl.FcvihSerProviderDefGenCom + dtl.FcvihAuthDefGenCom;//SerProviderDefGenCom AuthDefGenCom
                    detailReq.CrBy = _security.UserID;
                    detailReq.CrDt = DateTime.UtcNow;
                    detailReq.ModBy = _security.UserID;
                    detailReq.ModDt = DateTime.UtcNow;
                    detailReq.SubmitSts = false;
                    detailReq.ActiveYn = true;
                    result.Add(_mapper.Map<RmFormF4InsDtl>(detailReq));
                }
                return await _formF4Repository.saveDetail(result);
            }
            return 0;
        }

        public async Task<int> DeleteFormF4Hdr(int id)
        {
            var rowsAfftected = 0;
            if (id > 0)
            {
                RmFormF4InsHdr header = await _formF4Repository.FindAsync(h => h.FivahPkRefNo == id && h.FivahActiveYn == true);
                List<RmFormF4InsDtl> detailList = new List<RmFormF4InsDtl>();
                header.FivahActiveYn = false;
                header.FivahModBy = _security.UserID;
                header.FivahModDt = DateTime.UtcNow;
                rowsAfftected = await _formF4Repository.DeleteFormF4Hdr(header);
                if (rowsAfftected > 0)
                {
                    var result = await _repoUint.formF4Repository.GetDetailList(id);
                    if (result.Count > 0)
                    {
                        foreach (RmFormF4InsDtl dtl in result)
                        {
                            dtl.FivadActiveYn = false;
                            dtl.FivadModBy = _security.UserID;
                            dtl.FivadModDt = DateTime.UtcNow;
                            detailList.Add(dtl);
                        }
                        rowsAfftected = await _formF4Repository.DeleteFormF4Dtl(detailList);
                    }
                }
            }
            return rowsAfftected;
        }

        public async Task<FORMF4Rpt> GetReportData(int headerid)
        {
            return await _repoUint.formF4Repository.GetReportData(headerid);
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
                FORMF4Rpt rpt = await this.GetReportData(id);
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
                    double condition1 = 0;
                    double condition2 = 0;
                    double condition3 = 0;
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
                            int i = 15;

                            var data = rpt.Details.Skip((sheet - 1) * 24).Take(24);
                            foreach (var r in data)
                            {

                                condition1 += r.OverAllCondition == 1 ? r.Length.GetValueOrDefault() : 0;
                                condition2 += r.OverAllCondition == 2 ? r.Length.GetValueOrDefault() : 0;
                                condition3 += r.OverAllCondition == 3 ? r.Length.GetValueOrDefault() : 0;
                                worksheet.Cell(i, 2).Value = index;
                                switch (r.CentreLineChM.Length)
                                {
                                    case 0:
                                        r.CentreLineChM = "000";
                                        break;
                                    case 2:
                                        r.CentreLineChM = r.CentreLineChM + "0";
                                        break;
                                    case 1:
                                        r.CentreLineChM = r.CentreLineChM + "00";
                                        break;
                                }
                                worksheet.Cell(i, 4).Value = $"{r.CentreLineChKm}+{r.CentreLineChM}";
                                worksheet.Cell(i, 8).Value = r.Code;
                                worksheet.Cell(i, 10).Value = r.InletHeadWall;
                                worksheet.Cell(i, 17).Value = r.Length;
                                worksheet.Cell(i, 24).Value = r.NoOfCell;
                                worksheet.Cell(i, 31).Value = r.Width;
                                worksheet.Cell(i, 38).Value = r.Height;
                                worksheet.Cell(i, 45).Value = r.OutletHeadWall;
                                worksheet.Cell(i, 52).Value = r.OverAllCondition;
                                worksheet.Cell(i, 59).Value = r.Remarks;


                                index++;
                                i++;

                            }
                            //worksheet.Cell(37, 8).Value = condition1;
                            //worksheet.Cell(37, 24).Value = condition2;
                            //worksheet.Cell(37, 45).Value = condition3;
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
