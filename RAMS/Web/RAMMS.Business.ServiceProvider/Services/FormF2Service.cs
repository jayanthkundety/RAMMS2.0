using System;
using System.Threading.Tasks;
using AutoMapper;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.RequestBO;
using RAMMS.Repository.Interfaces;
using System.Linq;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Wrappers;
using System.Collections.Generic;
using RAMMS.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Report;
using ClosedXML.Excel;
using System.IO;
using RAMMS.Domain.Models;

namespace RAMMS.Business.ServiceProvider.Services
{


    public class FormF2Service : IFormF2Service
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        public FormF2Service(IRepositoryUnit repoUnit, IMapper mapper, ISecurity security)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security ?? throw new ArgumentNullException(nameof(security));
        }

        #region Header
        /// <summary>
        /// To Check Header existence.
        /// </summary>
        /// <param name="rmu"></param>
        /// <param name="activitycode"></param>
        /// <param name="year"></param>
        /// <param name="quarter"></param>
        /// <returns></returns>
        public async Task<(int id, bool aleadyexists, bool isSubmitted)> CheckHeaderExistence(string roadcode)
        {
            var result = await _repoUnit.FormF2Repository.FindAsync(s => s.FgrihRoadCode == roadcode && s.FgrihActiveYn == true);
            if (result != null)
                return (result.FgrihPkRefNo, true, result.FgrihSubmitSts);
            else
                return (LastHeaderInsertedNo(), false, false);
        }
        /// <summary>
        /// To Get LastheaderInsertedNo.
        /// </summary>
        /// <returns></returns>
        public int LastHeaderInsertedNo()
        {
            var model = _repoUnit.FormF2Repository.GetAll().OrderByDescending(s => s.FgrihPkRefNo).FirstOrDefault();
            if (model != null)
            {
                return model.FgrihPkRefNo;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// TO Get header by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FormF2HeaderRequestDTO> GetHeaderById(int id)
        {
            var header = await _repoUnit.FormF2Repository.FindAsync(s => s.FgrihPkRefNo == id && s.FgrihActiveYn == true);
            if (header == null)
            {
                return null;
            }
            return _mapper.Map<Domain.Models.RmFormF2GrInsHdr, FormF2HeaderRequestDTO>(header);
        }

        public async Task<decimal?> TotalLength(string roadcode)
        {
            return await _repoUnit.FormF2Repository.TotalLength(roadcode);
        }

        /// <summary>
        /// To Save Header
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> SaveHeader(FormF2HeaderRequestDTO model)
        {
            int rowsAffected;
            try
            {
                string st = $"CI/Form F2/{model.RoadCode}/{model.YearOfInsp}";
                var formf2 = _mapper.Map<Domain.Models.RmFormF2GrInsHdr>(model);

                var alreadyexists = _repoUnit.FormF2Repository.IsExists(st);
                formf2.FgrihPkRefNo = (alreadyexists == null ? 0 : alreadyexists.FgrihPkRefNo);
                if (alreadyexists != null)
                {
                    //TODO
                    alreadyexists.FgrihCrewLeaderId = formf2.FgrihCrewLeaderId;
                    alreadyexists.FgrihCrewLeaderName = formf2.FgrihCrewLeaderName;
                    alreadyexists.FgrihDist = formf2.FgrihDist;
                    alreadyexists.FgrihDtOfInsp = formf2.FgrihDtOfInsp;
                    alreadyexists.FgrihDtInspBy = formf2.FgrihDtInspBy;
                    alreadyexists.FgrihUserIdInspBy = formf2.FgrihUserIdInspBy;
                    alreadyexists.FgrihUserNameInspBy = formf2.FgrihUserNameInspBy;
                    alreadyexists.FgrihUserDesignationInspBy = formf2.FgrihUserDesignationInspBy;

                    if (formf2.FgrihSubmitSts)
                    {
                        var lst = _repoUnit.FormF2DetailRepository.FindAll(s => s.FgridFgrihPkRefNo == formf2.FgrihPkRefNo && (!s.FgridGrCondition1.HasValue || !s.FgridGrCondition2.HasValue || !s.FgridGrCondition3.HasValue));
                        if (lst.Count > 0)
                        {
                            return -1;
                        }
                        else
                        {
                            var modelDtl = await _repoUnit.FormF2DetailRepository.FindAllAsync(s => s.FgridFgrihPkRefNo == formf2.FgrihPkRefNo && s.FgridActiveYn == true);
                            modelDtl.All(x => x.FgridSubmitSts = true);
                            //modelDtl.FgridSubmitSts = true;
                        }

                    }

                    alreadyexists.FgrihSubmitSts = formf2.FgrihSubmitSts;
                    alreadyexists.FgrihModBy = _security.UserID;
                    alreadyexists.FgrihModDt = DateTime.UtcNow;
                    await _repoUnit.CommitAsync();
                }
                else
                {
                    formf2.FgrihModDt = DateTime.UtcNow;
                    formf2.FgrihActiveYn = true;
                    formf2.FgrihModBy = _security.UserID;
                    formf2.FgrihCrBy = _security.UserID;
                    //if (formf2.FgrihSubmitSts)
                    //{
                    //    var lst = _repoUnit.FormF2DetailRepository.FindAll(s => s.FgridFgrihPkRefNo == formf2.FgrihPkRefNo && (!s.FgridGrCondition1.HasValue || !s.FgridGrCondition2.HasValue || !s.FgridGrCondition3.HasValue));
                    //    if (lst.Count > 0)
                    //        return -1;
                    //}
                    _repoUnit.FormF2Repository.Create(formf2);
                    await _repoUnit.CommitAsync();
                    if (formf2.FgrihPkRefNo > 0)
                    {
                        await _repoUnit.FormF2Repository.BulkDetailInsert(formf2.FgrihPkRefNo, _security.UserID);
                    }

                }

                return formf2.FgrihPkRefNo;

            }
            catch (Exception ex)
            {
                await _repoUnit.RollbackAsync();
                throw ex;
            }

            return rowsAffected;
        }
        /// <summary>
        /// To Remove header
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveHeader(int id)
        {
            var model = _repoUnit.FormF2Repository.Find(s => s.FgrihPkRefNo == id);
            if (model != null)
            {
                model.FgrihActiveYn = false;
                return await _repoUnit.CommitAsync() != 0;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region Details
        /// <summary>
        /// TO Get Last Inserted Detail No.
        /// </summary>
        /// <param name="headerid"></param>
        /// <returns></returns>
        public int LastInsertedDetailNo(int headerid)
        {
            var model = _repoUnit.FormF2DetailRepository.FindAll(s => s.FgridFgrihPkRefNo == headerid).OrderByDescending(s => s.FgridPkRefNo).FirstOrDefault();
            if (model != null)
            {
                return model.FgridPkRefNo;
            }
            else
            {
                return 0;
            }
        }
        /// <summary>
        /// To Save Details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> SaveDetail(FormF2DetailRequestDTO model)
        {
            try
            {

                var rmModel = _repoUnit.FormF2DetailRepository.Find(s => s.FgridPkRefNo == model.PkRefNo);

                if (rmModel != null && rmModel.FgridPkRefNo != 0)
                {
                    rmModel.FgridGrCondition1 = model.GrCondition1;
                    rmModel.FgridGrCondition2 = model.GrCondition2;
                    rmModel.FgridGrCondition3 = model.GrCondition3;
                    rmModel.FgridRemarks = model.Remarks;
                    rmModel.FgridModBy = _security.UserID;
                    rmModel.FgridModDt = DateTime.UtcNow;
                    rmModel.FgridActiveYn = true;
                    rmModel.FgridPostSpac = model.PostSpac;
                }

                await _repoUnit.CommitAsync();
                return rmModel.FgridPkRefNo;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        /// <summary>
        /// To Get Detail by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FormF2DetailRequestDTO> GetDetailById(int id)
        {
            try
            {
                var model = await _repoUnit.FormF2DetailRepository.FindAsync(s => s.FgridPkRefNo == id && s.FgridActiveYn == true);
                if (model != null)
                {
                    var data = _mapper.Map<FormF2DetailRequestDTO>(model);
                    var asset = _repoUnit.AllAssetRepository.Find(s => s.AiPkRefNo == data.FgrihAiPkRefNo);
                    if (asset != null)
                    {
                        //data.Length = asset.AiLength;//*1000;//km to meter conversion 
                        data.Bound = asset.AiBound;
                    }
                    return data;
                }
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        /// <summary>
        /// To Remove detail by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> RemoveDetail(int id)
        {
            try
            {
                var model = await _repoUnit.FormF2DetailRepository.FindAsync(s => s.FgridPkRefNo == id);
                if (model != null)
                {
                    model.FgridActiveYn = false;
                    model.FgridModBy = _security.UserID;
                    model.FgridModDt = DateTime.UtcNow;
                    return await _repoUnit.CommitAsync() != 0;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
        /// <summary>
        /// To Get Header List
        /// </summary>
        /// <param name="filterOptions"></param>
        /// <returns></returns>
        public async Task<PagingResult<FormF2HeaderRequestDTO>> GetHeaderList(FilteredPagingDefinition<FormF2SearchGridDTO> filterOptions)
        {
            PagingResult<FormF2HeaderRequestDTO> result = new PagingResult<FormF2HeaderRequestDTO>();
            List<FormF2HeaderRequestDTO> formAlist = new List<FormF2HeaderRequestDTO>();
            result.PageResult = await _repoUnit.FormF2Repository.GetFilteredRecordList(filterOptions);
            result.TotalRecords = await _repoUnit.FormF2Repository.GetFilteredRecordCount(filterOptions);
            result.PageNo = filterOptions.StartPageNo;
            result.FilteredRecords = result.PageResult != null ? result.PageResult.Count : 0;
            return result;
        }
        /// <summary>
        /// TO Get Detail List
        /// </summary>
        /// <param name="filterOptions"></param>
        /// <returns></returns>
        public async Task<PagingResult<FormF2DetailRequestDTO>> GetDetailList(FilteredPagingDefinition<FormF2DetailRequestDTO> filterOptions)
        {
            PagingResult<FormF2DetailRequestDTO> result = new PagingResult<FormF2DetailRequestDTO>();
            List<FormF2DetailRequestDTO> formAlist = new List<FormF2DetailRequestDTO>();
            result.PageResult = await _repoUnit.FormF2DetailRepository.GetFilteredRecordList(filterOptions);
            result.TotalRecords = await _repoUnit.FormF2DetailRepository.GetFilteredRecordCount(filterOptions);
            result.PageNo = filterOptions.StartPageNo;
            result.FilteredRecords = result.PageResult != null ? result.PageResult.Count : 0;
            return result;
        }

        public async Task<List<SelectListItem>> GetLocationCh(string roadcode)
        {
            return await _repoUnit.FormF2DetailRepository.GetLocationCh(roadcode);
        }

        public async Task<List<SelectListItem>> GetStructureCode(string roadcode, string locationch)
        {
            return await _repoUnit.FormF2DetailRepository.GetStructureCode(roadcode, locationch);
        }

        public async Task<List<SelectListItem>> GetAIBound(string roadcode, string locationch, string structurecode)
        {
            return await _repoUnit.FormF2DetailRepository.GetAIBound(roadcode, locationch, structurecode);
        }

        public async Task<List<SelectListItem>> GetPostSpacing(string roadcode, string locationch, string structurecode, string bound)
        {
            return await _repoUnit.FormF2DetailRepository.GetPostSpacing(roadcode, locationch, structurecode, bound);
        }

        public async Task<AssetDDLResponseDTO> GetAssetDDL(AssetDDLRequestDTO roadMaster)
        {
            try
            {
                AssetDDLResponseDTO roadlist = await _repoUnit.AllAssetRepository.GetFilteredList(roadMaster);

                return roadlist;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        public FORMF2Rpt GetReportData(int headerid)
        {
            return _repoUnit.FormF2Repository.GetReportData(headerid);
        }

        public byte[] FormDownload(string formname, int id, string filepath)
        {
            string Oldfilename = "";
            string filename = "";
            string cachefile = "";
            if (!filepath.Contains(".xlsx"))
            {
                Oldfilename = filepath + formname + ".xlsx";// formdetails.FgdFilePath+"\\" + formdetails.FgdFileName+ ".xlsx";
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
                FORMF2Rpt rpt = this.GetReportData(id);
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
                            //worksheet.Cell(5, 72).Value = rpt.InspectedByName;
                            //worksheet.Cell(6, 72).Value = rpt.InspectedDate.HasValue ? rpt.InspectedDate.Value.ToString("dd-MM-yyyy") : "";
                            worksheet.Cell(5, 57).Value = rpt.InspectedByName;
                            worksheet.Cell(6, 57).Value = rpt.InspectedDate.HasValue ? rpt.InspectedDate.Value.ToString("dd-MM-yyyy") : "";
                            //worksheet.Cell(7, 74).Value = rpt.RoadLength;
                            worksheet.Cell(7, 61).Value = rpt.RoadLength;
                            worksheet.Cell(2, 80).Value = noofsheets;
                            worksheet.Cell(9, 8).Value = condition1.ToString() == "0" ? "" : condition1.ToString();
                            worksheet.Cell(9, 24).Value = condition2.ToString() == "0" ? "" : condition2.ToString();
                            worksheet.Cell(9, 45).Value = condition3.ToString() == "0" ? "" : condition3.ToString();
                            int i = 13;

                            var data = rpt.Details.Skip((sheet - 1) * 24).Take(24);
                            foreach (var r in data)
                            {

                                condition1 += r.Condition1.GetValueOrDefault();
                                condition2 += r.Condition2.GetValueOrDefault();
                                condition3 += r.Condition3.GetValueOrDefault();
                                worksheet.Cell(i, 2).Value = index;
                                switch (r.StartingChM == null ? 0 : r.StartingChM.Length)
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
                                worksheet.Cell(i, 4).Value = $"{r.StartingChKm}+{r.StartingChM}";
                                worksheet.Cell(i, 8).Value = r.Code;
                                worksheet.Cell(i, 10).Value = r.Condition1.ToString() == "0" ? "" : r.Condition1.ToString();
                                worksheet.Cell(i, 17).Value = r.Condition2.ToString() == "0" ? "" : r.Condition2.ToString();
                                worksheet.Cell(i, 24).Value = r.Condition3.ToString() == "0" ? "" : r.Condition3.ToString();
                                worksheet.Cell(i, 31).Value = r.RML;
                                // worksheet.Cell(i, 38).Value = $"{r.PostSpacing} m";
                                worksheet.Cell(i, 38).Value = (r.PostSpacing != null ? r.PostSpacing + " m" : "");
                                worksheet.Cell(i, 45).Value = r.Remarks;

                                
                                index++;
                                i++;

                            }
                            worksheet.Cell(37, 7).Value = condition1.ToString() == "0" ? "" : condition1.ToString();
                            worksheet.Cell(37, 24).Value = condition2.ToString() == "0" ? "" : condition2.ToString();
                            worksheet.Cell(37, 45).Value = condition3.ToString() == "0" ? "" : condition3.ToString();
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

        public async Task<int> UpdateF2Header(FormF2HeaderRequestDTO requestDTO)
        {
            int rowsAffected;
            var getHeader = await _repoUnit.FormF2Repository.GetByIdAsync(requestDTO.PkRefNo);
            getHeader.FgrihSubmitSts = requestDTO.SubmitSts;
            getHeader.FgrihDtInspBy = requestDTO.DtInspBy ?? getHeader.FgrihDtInspBy ?? null;
            getHeader.FgrihDtOfInsp = requestDTO.DtOfInsp ?? getHeader.FgrihDtOfInsp ?? null;
            getHeader.FgrihSignpathInspBy = requestDTO.SignpathInspBy ?? getHeader.FgrihSignpathInspBy ?? null;
            getHeader.FgrihUserIdInspBy = requestDTO.UserIdInspBy ?? getHeader.FgrihUserIdInspBy ?? null;
            getHeader.FgrihUserDesignationInspBy = requestDTO.UserDesignationInspBy ?? getHeader.FgrihUserDesignationInspBy ?? null;
            getHeader.FgrihUserNameInspBy = requestDTO.UserNameInspBy ?? getHeader.FgrihUserNameInspBy ?? null;
            getHeader.FgrihCrewLeaderId = requestDTO.CrewLeaderId ?? getHeader.FgrihCrewLeaderId ?? null;
            getHeader.FgrihCrewLeaderName = requestDTO.CrewLeaderName ?? getHeader.FgrihCrewLeaderName ?? null;

            var formF2 = _mapper.Map<RmFormF2GrInsHdr>(getHeader);

            formF2.FgrihModDt = DateTime.Now;
            formF2.FgrihModBy = _security.UserID;

            _repoUnit.FormF2Repository.Update(formF2);
            rowsAffected = await _repoUnit.CommitAsync();
            return rowsAffected;
        }

        public async Task<List<FormF2DetailRequestDTO>> GetF2DetailList(int headerId)
        {
            var formF2List = await _repoUnit.FormF2DetailRepository.GetF2DetailList(headerId);
            return _mapper.Map<List<FormF2DetailRequestDTO>>(formF2List);
        }
        #endregion


    }
}

