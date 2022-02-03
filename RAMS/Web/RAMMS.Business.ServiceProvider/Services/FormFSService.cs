using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using System.Linq;
using RAMMS.DTO.Report;
using ClosedXML.Excel;
using System.IO;
using System.Collections.Generic;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class FormFSService : IFormFSService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        public FormFSService(IRepositoryUnit repoUnit, IMapper mapper, ISecurity security)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit)); _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security ?? throw new ArgumentNullException(nameof(security));
        }
        public long LastHeaderInsertedNo()
        {
            var model = _repoUnit.FormFSHeaderRepository.GetAll().OrderByDescending(s => s.FshPkRefNo).FirstOrDefault();
            if (model != null) { return model.FshPkRefNo; } else { return 0; }
        }
        public async Task<FormFSHeaderRequestDTO> GetHeaderById(int id)
        {
            var model = await _repoUnit.FormFSHeaderRepository.FindAsync(s => s.FshPkRefNo == id);
            if (model == null) { return null; }
            var fs = _mapper.Map<Domain.Models.RmFormFsInsHdr, FormFSHeaderRequestDTO>(model);
            var road = _repoUnit.RoadmasterRepository.FindAll(s => s.RdmPkRefNo == fs.RoadId).FirstOrDefault();
            fs.SecCode = road.RdmSecCode.GetValueOrDefault();
            fs.SecName = road.RdmSecName;
            fs.RmuCode = road.RdmRmuCode;
            fs.RmuName = road.RdmRmuName;
            fs.RoadName = road.RdmRdName;
            return fs;
        }
        public async Task<int> SaveHeader(FormFSHeaderRequestDTO model)
        {
            try
            {
                bool IsAdd = false;
                var form = _mapper.Map<Domain.Models.RmFormFsInsHdr>(model);

                var road = _repoUnit.RoadmasterRepository.FindAll(s => s.RdmRdCode == form.FshRoadCode).FirstOrDefault();
                if (road != null)
                {
                    form.FshRoadId = road.RdmPkRefNo;
                    form.FshRoadLength = road.RdmLengthPaved;
                }
                form.FshActiveYn = true;
                if (form.FshPkRefNo != 0)
                {
                    form.FshModBy = _security.UserID;
                    form.FshModDt = DateTime.Now;
                    _repoUnit.FormFSHeaderRepository.Update(form);
                }
                else
                {
                    form.FshCrBy = _security.UserID;
                    form.FshModBy = _security.UserID;
                    form.FshCrDt = DateTime.Now;
                    form.FshModDt = DateTime.Now;
                    _repoUnit.FormFSHeaderRepository.Create(form);
                    IsAdd = true;
                }
                await _repoUnit.CommitAsync();

                if (IsAdd)
                {
                    _repoUnit.FormFSDetailRepository.BulkInsert(form.FshPkRefNo, _security.UserID, form);
                }
                return form.FshPkRefNo;
            }
            catch (Exception ex) { await _repoUnit.RollbackAsync(); throw ex; }
        }
        public async Task<int> FindDetail(FormFSHeaderRequestDTO model)
        {
            try
            {
                bool IsAdd = false;
                var form = _mapper.Map<Domain.Models.RmFormFsInsHdr>(model);

                var exists = _repoUnit.FormFSHeaderRepository.Find(s => s.FshActiveYn == true && s.FshYearOfInsp == model.YearOfInsp && s.FshRoadCode == model.RoadCode);
                if (exists != null)
                    return exists.FshPkRefNo;

                var road = _repoUnit.RoadmasterRepository.FindAll(s => s.RdmRdCode == form.FshRoadCode).FirstOrDefault();
                if (road != null)
                {
                    form.FshRoadId = road.RdmPkRefNo;
                    form.FshRoadLength = road.RdmLengthPaved;
                }
                form.FshActiveYn = true;
                if (form.FshPkRefNo != 0)
                {
                    form.FshModBy = _security.UserID;
                    form.FshModDt = DateTime.Now;
                    _repoUnit.FormFSHeaderRepository.Update(form);
                }
                else
                {
                    form.FshCrBy = _security.UserID;
                    form.FshModBy = _security.UserID;
                    form.FshCrDt = DateTime.Now;
                    form.FshModDt = DateTime.Now;
                    _repoUnit.FormFSHeaderRepository.Create(form);
                    IsAdd = true;
                }
                await _repoUnit.CommitAsync();

                if (IsAdd)
                {
                    _repoUnit.FormFSDetailRepository.BulkInsert(form.FshPkRefNo, _security.UserID, form);
                }
                return form.FshPkRefNo;
            }
            catch (Exception ex) { await _repoUnit.RollbackAsync(); throw ex; }
        }
        public async Task<bool> RemoveHeader(int id)
        {
            var model = _repoUnit.FormFSHeaderRepository.Find(s => s.FshPkRefNo == id);
            if (model != null)
            {
                model.FshActiveYn = false;
                return await _repoUnit.CommitAsync() != 0;
            }
            else { return false; }
        }
        public async Task<PagingResult<FormFSHeaderRequestDTO>> GetHeaderList(FilteredPagingDefinition<FormFSHeaderRequestDTO> filterOptions)
        {
            PagingResult<FormFSHeaderRequestDTO> result = new PagingResult<FormFSHeaderRequestDTO>();
            result.PageResult = await _repoUnit.FormFSHeaderRepository.GetFilteredRecordList(filterOptions);
            result.TotalRecords = await _repoUnit.FormFSHeaderRepository.GetFilteredRecordCount(filterOptions); return result;
        }
        public long LastDetailInsertedNo()
        {
            var model = _repoUnit.FormFSDetailRepository.GetAll().OrderByDescending(s => s.FsdPkRefNo).FirstOrDefault();
            if (model != null) { return model.FsdPkRefNo; } else { return 0; }
        }
        public async Task<FormFSDetailRequestDTO> GetDetailById(int id)
        {
            var model = await _repoUnit.FormFSDetailRepository.FindAsync(s => s.FsdPkRefNo == id); if (model == null) { return null; }
            return _mapper.Map<Domain.Models.RmFormFsInsDtl, FormFSDetailRequestDTO>(model);
        }
        public async Task<int> SaveDetail(FormFSDetailRequestDTO model)
        {
            try
            {
                Domain.Models.RmFormFsInsDtl dtl = _repoUnit.FormFSDetailRepository.FindAll(s => s.FsdPkRefNo == model.PkRefNo).FirstOrDefault();
                //dtl.FsdCondition1 = model.Condition1;
                //dtl.FsdCondition2 = model.Condition2;
                //dtl.FsdCondition3 = model.Condition3;
                dtl.FsdNeeded = model.Needed;
                dtl.FsdRemarks = model.Remarks;
                return await _repoUnit.CommitAsync();

            }
            catch (Exception ex) { await _repoUnit.RollbackAsync(); throw ex; }
        }


        public async Task<bool> RemoveDetail(int id)
        {
            var model = _repoUnit.FormFSDetailRepository.Find(s => s.FsdPkRefNo == id);
            if (model != null) { return await _repoUnit.CommitAsync() != 0; } else { return false; }
        }
        public async Task<PagingResult<FormFSDetailRequestDTO>> GetDetailList(FilteredPagingDefinition<FormFSDetailRequestDTO> filterOptions)
        {
            PagingResult<FormFSDetailRequestDTO> result = new PagingResult<FormFSDetailRequestDTO>();
            result.PageResult = await _repoUnit.FormFSDetailRepository.GetFilteredRecordList(filterOptions);
            result.TotalRecords = await _repoUnit.FormFSDetailRepository.GetFilteredRecordCount(filterOptions); return result;
        }

        public FormFSRpt GetReportData(int headerid)
        {
            return _repoUnit.FormFSHeaderRepository.GetReportData(headerid);
        }

        public byte[] FormDownload(string formname, int id, string basepath, string filepath)
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
                FormFSRpt rpt = this.GetReportData(id);
                System.IO.File.Copy(Oldfilename, cachefile, true);
                using (var workbook = new XLWorkbook(cachefile))
                {

                    IXLWorksheet worksheet = workbook.Worksheet(1);

                    if (worksheet != null)
                    {
                        worksheet.Cell(3, 19).Value = rpt.SummarizedBy;
                        worksheet.Cell(7, 19).Value = rpt.CheckedBy;
                        worksheet.Cell(6, 5).Value = rpt.RMU;
                        worksheet.Cell(6, 11).Value = rpt.District;
                        worksheet.Cell(7, 5).Value = rpt.RoadCode;
                        worksheet.Cell(7, 11).Value = rpt.Division;
                        worksheet.Cell(8, 5).Value = rpt.RoadName;
                        worksheet.Cell(5, 8).Value = rpt.CrewLeader;
                        if (rpt.DateOfInspection.HasValue)
                        {
                            worksheet.Cell(7, 15).Value = rpt.DateOfInspection.Value.Day;
                            worksheet.Cell(7, 17).Value = rpt.DateOfInspection.Value.Month;
                            worksheet.Cell(7, 18).Value = rpt.DateOfInspection.Value.Year;
                        }
                        int i = 12;
                        worksheet.Cell(i, 11).Value = rpt.CWAsphaltic.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.CWAsphaltic.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.CWAsphaltic.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.CWAsphaltic.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.CWAsphaltic.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.CWAsphaltic.Needed;
                        worksheet.Cell(i, 22).Value = rpt.CWAsphaltic.Remarks;

                        i = 13;
                        worksheet.Cell(i, 11).Value = rpt.CWSurfaceDressed.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.CWSurfaceDressed.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.CWSurfaceDressed.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.CWSurfaceDressed.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.CWSurfaceDressed.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.CWSurfaceDressed.Needed;
                        worksheet.Cell(i, 22).Value = rpt.CWSurfaceDressed.Remarks;
                        i = 14;
                        worksheet.Cell(i, 11).Value = rpt.CWConcrete.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.CWConcrete.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.CWConcrete.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.CWConcrete.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.CWConcrete.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.CWConcrete.Needed;
                        worksheet.Cell(i, 22).Value = rpt.CWConcrete.Remarks;
                        i = 15;
                        worksheet.Cell(i, 11).Value = rpt.CWGravel.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.CWGravel.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.CWGravel.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.CWGravel.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.CWGravel.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.CWGravel.Needed;
                        worksheet.Cell(i, 22).Value = rpt.CWGravel.Remarks;
                        i = 16;
                        worksheet.Cell(i, 11).Value = rpt.CWEarth.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.CWEarth.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.CWEarth.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.CWEarth.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.CWEarth.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.CWEarth.Needed;
                        worksheet.Cell(i, 22).Value = rpt.CWEarth.Remarks;
                        i = 17;
                        worksheet.Cell(i, 11).Value = rpt.CWSand.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.CWSand.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.CWSand.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.CWSand.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.CWSand.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.CWSand.Needed;
                        worksheet.Cell(i, 22).Value = rpt.CWSand.Remarks;
                        i = 18;
                        worksheet.Cell(i, 11).Value = rpt.CLMPaint.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.CLMPaint.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.CLMPaint.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.CLMPaint.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.CLMPaint.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.CLMPaint.Needed;
                        worksheet.Cell(i, 22).Value = rpt.CLMPaint.Remarks;
                        i = 19;
                        worksheet.Cell(i, 11).Value = rpt.CLMThermoplastic.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.CLMThermoplastic.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.CLMThermoplastic.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.CLMThermoplastic.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.CLMThermoplastic.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.CLMThermoplastic.Needed;
                        worksheet.Cell(i, 22).Value = rpt.CLMThermoplastic.Remarks;
                        i = 20;
                        worksheet.Cell(i, 11).Value = rpt.LELMPaint.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.LELMPaint.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.LELMPaint.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.LELMPaint.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.LELMPaint.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.LELMPaint.Needed;
                        worksheet.Cell(i, 22).Value = rpt.LELMPaint.Remarks;
                        i = 21;
                        worksheet.Cell(i, 11).Value = rpt.LELMThermoplastic.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.LELMThermoplastic.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.LELMThermoplastic.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.LELMThermoplastic.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.LELMThermoplastic.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.LELMThermoplastic.Needed;
                        worksheet.Cell(i, 22).Value = rpt.LELMThermoplastic.Remarks;
                        i = 22;
                        worksheet.Cell(i, 11).Value = rpt.LDitchGravel.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.LDitchGravel.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.LDitchGravel.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.LDitchGravel.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.LDitchGravel.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.LDitchGravel.Needed;
                        worksheet.Cell(i, 22).Value = rpt.LDitchGravel.Remarks;
                        i = 23;
                        worksheet.Cell(i, 11).Value = rpt.LDrainEarth.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.LDrainEarth.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.LDrainEarth.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.LDrainEarth.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.LDrainEarth.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.LDrainEarth.Needed;
                        worksheet.Cell(i, 22).Value = rpt.LDrainEarth.Remarks;
                        i = 24;
                        worksheet.Cell(i, 11).Value = rpt.LDrainBlockstone.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.LDrainBlockstone.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.LDrainBlockstone.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.LDrainBlockstone.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.LDrainBlockstone.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.LDrainBlockstone.Needed;
                        worksheet.Cell(i, 22).Value = rpt.LDrainBlockstone.Remarks;
                        i = 25;
                        worksheet.Cell(i, 11).Value = rpt.LDrainConcreate.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.LDrainConcreate.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.LDrainConcreate.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.LDrainConcreate.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.LDrainConcreate.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.LDrainConcreate.Needed;
                        worksheet.Cell(i, 22).Value = rpt.LDrainConcreate.Remarks;
                        i = 26;
                        worksheet.Cell(i, 11).Value = rpt.LShoulderAsphalt.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.LShoulderAsphalt.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.LShoulderAsphalt.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.LShoulderAsphalt.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.LShoulderAsphalt.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.LShoulderAsphalt.Needed;
                        worksheet.Cell(i, 22).Value = rpt.LShoulderAsphalt.Remarks;
                        i = 27;
                        worksheet.Cell(i, 11).Value = rpt.LShoulderConcrete.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.LShoulderConcrete.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.LShoulderConcrete.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.LShoulderConcrete.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.LShoulderConcrete.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.LShoulderConcrete.Needed;
                        worksheet.Cell(i, 22).Value = rpt.LShoulderConcrete.Remarks;
                        i = 28;
                        worksheet.Cell(i, 11).Value = rpt.LShoulderEarth.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.LShoulderEarth.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.LShoulderEarth.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.LShoulderEarth.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.LShoulderEarth.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.LShoulderEarth.Needed;
                        worksheet.Cell(i, 22).Value = rpt.LShoulderEarth.Remarks;
                        i = 29;
                        worksheet.Cell(i, 11).Value = rpt.LShoulderGravel.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.LShoulderGravel.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.LShoulderGravel.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.LShoulderGravel.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.LShoulderGravel.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.LShoulderGravel.Needed;
                        worksheet.Cell(i, 22).Value = rpt.LShoulderGravel.Remarks;
                        i = 30;
                        worksheet.Cell(i, 11).Value = rpt.LShoulderFootpathkerb.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.LShoulderFootpathkerb.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.LShoulderFootpathkerb.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.LShoulderFootpathkerb.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.LShoulderFootpathkerb.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.LShoulderFootpathkerb.Needed;
                        worksheet.Cell(i, 22).Value = rpt.LShoulderFootpathkerb.Remarks;


                        i = 31;
                        worksheet.Cell(i, 11).Value = rpt.RELMPaint.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RELMPaint.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.RELMPaint.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.RELMPaint.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.RELMPaint.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.RELMPaint.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RELMPaint.Remarks;
                        i = 32;
                        worksheet.Cell(i, 11).Value = rpt.RELMThermoplastic.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RELMThermoplastic.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.RELMThermoplastic.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.RELMThermoplastic.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.RELMThermoplastic.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.RELMThermoplastic.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RELMThermoplastic.Remarks;
                        i = 33;
                        worksheet.Cell(i, 11).Value = rpt.RDitchGravel.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RDitchGravel.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.RDitchGravel.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.RDitchGravel.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.RDitchGravel.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.RDitchGravel.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RDitchGravel.Remarks;
                        i = 34;
                        worksheet.Cell(i, 11).Value = rpt.RDrainEarth.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RDrainEarth.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.RDrainEarth.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.RDrainEarth.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.RDrainEarth.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.RDrainEarth.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RDrainEarth.Remarks;
                        i = 35;
                        worksheet.Cell(i, 11).Value = rpt.RDrainBlockstone.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RDrainBlockstone.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.RDrainBlockstone.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.RDrainBlockstone.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.RDrainBlockstone.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.RDrainBlockstone.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RDrainBlockstone.Remarks;
                        i = 36;
                        worksheet.Cell(i, 11).Value = rpt.RDrainConcreate.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RDrainConcreate.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.RDrainConcreate.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.RDrainConcreate.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.RDrainConcreate.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.RDrainConcreate.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RDrainConcreate.Remarks;
                        i = 37;
                        worksheet.Cell(i, 11).Value = rpt.RShoulderAsphalt.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RShoulderAsphalt.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.RShoulderAsphalt.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.RShoulderAsphalt.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.RShoulderAsphalt.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.RShoulderAsphalt.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RShoulderAsphalt.Remarks;
                        i = 38;
                        worksheet.Cell(i, 11).Value = rpt.RShoulderConcrete.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RShoulderConcrete.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.RShoulderConcrete.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.RShoulderConcrete.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.RShoulderConcrete.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.RShoulderConcrete.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RShoulderConcrete.Remarks;
                        i = 39;
                        worksheet.Cell(i, 11).Value = rpt.RShoulderEarth.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RShoulderEarth.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.RShoulderEarth.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.RShoulderEarth.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.RShoulderEarth.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.RShoulderEarth.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RShoulderEarth.Remarks;
                        i = 40;
                        worksheet.Cell(i, 11).Value = rpt.RShoulderGravel.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RShoulderGravel.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.RShoulderGravel.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.RShoulderGravel.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.RShoulderGravel.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.RShoulderGravel.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RShoulderGravel.Remarks;
                        i = 41;
                        worksheet.Cell(i, 11).Value = rpt.RShoulderFootpathkerb.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RShoulderFootpathkerb.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.RShoulderFootpathkerb.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.RShoulderFootpathkerb.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.RShoulderFootpathkerb.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.RShoulderFootpathkerb.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RShoulderFootpathkerb.Remarks;
                        i = 42;
                        worksheet.Cell(i, 11).Value = rpt.RSLeft.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RSLeft.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.RSLeft.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.RSLeft.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.RSLeft.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.RSLeft.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RSLeft.Remarks;
                        i = 43;
                        worksheet.Cell(i, 11).Value = rpt.RSCenter.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RSCenter.TotalLength / 10;
                        worksheet.Cell(i, 17).Value = rpt.RSCenter.Condition1 / 10;
                        worksheet.Cell(i, 18).Value = rpt.RSCenter.Condition2 / 10;
                        worksheet.Cell(i, 19).Value = rpt.RSCenter.Condition3 / 10;
                        worksheet.Cell(i, 20).Value = rpt.RSCenter.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RSCenter.Remarks;
                        i = 44;
                        worksheet.Cell(i, 11).Value = rpt.RSRight.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RSRight.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.RSRight.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.RSRight.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.RSRight.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.RSRight.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RSRight.Remarks;
                        i = 45;
                        worksheet.Cell(i, 11).Value = rpt.SignsDelineator.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.SignsDelineator.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.SignsDelineator.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.SignsDelineator.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.SignsDelineator.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.SignsDelineator.Needed;
                        worksheet.Cell(i, 22).Value = rpt.SignsDelineator.Remarks;
                        i = 46;
                        worksheet.Cell(i, 11).Value = rpt.SignsWarning.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.SignsWarning.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.SignsWarning.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.SignsWarning.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.SignsWarning.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.SignsWarning.Needed;
                        worksheet.Cell(i, 22).Value = rpt.SignsWarning.Remarks;
                        i = 47;
                        worksheet.Cell(i, 11).Value = rpt.SignsGantrySign.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.SignsGantrySign.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.SignsGantrySign.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.SignsGantrySign.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.SignsGantrySign.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.SignsGantrySign.Needed;
                        worksheet.Cell(i, 22).Value = rpt.SignsGantrySign.Remarks;
                        i = 48;
                        worksheet.Cell(i, 11).Value = rpt.SignsGuideSign.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.SignsGuideSign.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.SignsGuideSign.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.SignsGuideSign.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.SignsGuideSign.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.SignsGuideSign.Needed;
                        worksheet.Cell(i, 22).Value = rpt.SignsGuideSign.Remarks;
                        i = 49;
                        worksheet.Cell(i, 11).Value = rpt.CVConcreatePipe.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.CVConcreatePipe.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.CVConcreatePipe.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.CVConcreatePipe.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.CVConcreatePipe.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.CVConcreatePipe.Needed;
                        worksheet.Cell(i, 22).Value = rpt.CVConcreatePipe.Remarks;
                        i = 50;
                        worksheet.Cell(i, 11).Value = rpt.CVConcreteBox.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.CVConcreteBox.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.CVConcreteBox.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.CVConcreteBox.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.CVConcreteBox.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.CVConcreteBox.Needed;
                        worksheet.Cell(i, 22).Value = rpt.CVConcreteBox.Remarks;

                        i = 51;
                        worksheet.Cell(i, 11).Value = rpt.CVMetal.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.CVMetal.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.CVMetal.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.CVMetal.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.CVMetal.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.CVMetal.Needed;
                        worksheet.Cell(i, 22).Value = rpt.CVMetal.Remarks;
                        i = 52;
                        worksheet.Cell(i, 11).Value = rpt.CVHDPE.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.CVHDPE.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.CVHDPE.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.CVHDPE.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.CVHDPE.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.CVHDPE.Needed;
                        worksheet.Cell(i, 22).Value = rpt.CVHDPE.Remarks;
                        i = 53;
                        worksheet.Cell(i, 11).Value = rpt.CVOthers.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.CVOthers.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.CVOthers.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.CVOthers.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.CVOthers.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.CVOthers.Needed;
                        worksheet.Cell(i, 22).Value = rpt.CVOthers.Remarks;
                        i = 54;
                        worksheet.Cell(i, 11).Value = rpt.BRConcConc.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.BRConcConc.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.BRConcConc.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.BRConcConc.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.BRConcConc.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.BRConcConc.Needed;
                        worksheet.Cell(i, 22).Value = rpt.BRConcConc.Remarks;
                        i = 55;
                        worksheet.Cell(i, 11).Value = rpt.BRConcSteel.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.BRConcSteel.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.BRConcSteel.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.BRConcSteel.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.BRConcSteel.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.BRConcSteel.Needed;
                        worksheet.Cell(i, 22).Value = rpt.BRConcSteel.Remarks;
                        i = 56;
                        worksheet.Cell(i, 11).Value = rpt.BRSteelTimber.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.BRSteelTimber.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.BRSteelTimber.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.BRSteelTimber.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.BRSteelTimber.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.BRSteelTimber.Needed;
                        worksheet.Cell(i, 22).Value = rpt.BRSteelTimber.Remarks;
                        i = 57;
                        worksheet.Cell(i, 11).Value = rpt.BRSteelSteel.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.BRSteelSteel.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.BRSteelSteel.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.BRSteelSteel.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.BRSteelSteel.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.BRSteelSteel.Needed;
                        worksheet.Cell(i, 22).Value = rpt.BRSteelSteel.Remarks;
                        i = 58;
                        worksheet.Cell(i, 11).Value = rpt.BRTimberTimber.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.BRTimberTimber.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.BRTimberTimber.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.BRTimberTimber.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.BRTimberTimber.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.BRTimberTimber.Needed;
                        worksheet.Cell(i, 22).Value = rpt.BRTimberTimber.Remarks;

                        i = 59;
                        worksheet.Cell(i, 11).Value = rpt.BRTimberSteel.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.BRTimberSteel.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.BRTimberSteel.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.BRTimberSteel.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.BRTimberSteel.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.BRTimberSteel.Needed;
                        worksheet.Cell(i, 22).Value = rpt.BRTimberSteel.Remarks;
                        i = 60;
                        worksheet.Cell(i, 11).Value = rpt.BRMansonry.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.BRMansonry.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.BRMansonry.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.BRMansonry.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.BRMansonry.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.BRMansonry.Needed;
                        worksheet.Cell(i, 22).Value = rpt.BRMansonry.Remarks;
                        i = 61;
                        worksheet.Cell(i, 11).Value = rpt.BRElevatedViaduct.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.BRElevatedViaduct.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.BRElevatedViaduct.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.BRElevatedViaduct.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.BRElevatedViaduct.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.BRElevatedViaduct.Needed;
                        worksheet.Cell(i, 22).Value = rpt.BRElevatedViaduct.Remarks;
                        i = 62;
                        worksheet.Cell(i, 11).Value = rpt.BRLongBridge.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.BRLongBridge.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.BRLongBridge.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.BRLongBridge.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.BRLongBridge.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.BRLongBridge.Needed;
                        worksheet.Cell(i, 22).Value = rpt.BRLongBridge.Remarks;
                        i = 63;
                        worksheet.Cell(i, 11).Value = rpt.GRSteel.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.GRSteel.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.GRSteel.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.GRSteel.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.GRSteel.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.GRSteel.Needed;
                        worksheet.Cell(i, 22).Value = rpt.GRSteel.Remarks;
                        i = 64;
                        worksheet.Cell(i, 11).Value = rpt.GRWire.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.GRWire.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.GRWire.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.GRWire.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.GRWire.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.GRWire.Needed;
                        worksheet.Cell(i, 22).Value = rpt.GRWire.Remarks;
                        i = 65;
                        worksheet.Cell(i, 11).Value = rpt.GRPedestrialRailing.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.GRPedestrialRailing.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.GRPedestrialRailing.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.GRPedestrialRailing.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.GRPedestrialRailing.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.GRPedestrialRailing.Needed;
                        worksheet.Cell(i, 22).Value = rpt.GRPedestrialRailing.Remarks;
                        i = 66;
                        worksheet.Cell(i, 11).Value = rpt.GRParapetWall.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.GRParapetWall.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.GRParapetWall.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.GRParapetWall.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.GRParapetWall.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.GRParapetWall.Needed;
                        worksheet.Cell(i, 22).Value = rpt.GRParapetWall.Remarks;
                        i = 67;
                        worksheet.Cell(i, 11).Value = rpt.GROthers.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.GROthers.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.GROthers.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.GROthers.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.GROthers.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.GROthers.Needed;
                        worksheet.Cell(i, 22).Value = rpt.GROthers.Remarks;
                        i = 68;
                        worksheet.Cell(i, 11).Value = rpt.RWReinforceConc.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RWReinforceConc.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.RWReinforceConc.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.RWReinforceConc.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.RWReinforceConc.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.RWReinforceConc.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RWReinforceConc.Remarks;
                        i = 69;
                        worksheet.Cell(i, 11).Value = rpt.RWSteelMetal.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RWSteelMetal.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.RWSteelMetal.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.RWSteelMetal.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.RWSteelMetal.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.RWSteelMetal.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RWSteelMetal.Remarks;
                        i = 70;
                        worksheet.Cell(i, 11).Value = rpt.RWMasonryGabion.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RWMasonryGabion.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.RWMasonryGabion.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.RWMasonryGabion.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.RWMasonryGabion.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.RWMasonryGabion.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RWMasonryGabion.Remarks;
                        i = 71;
                        worksheet.Cell(i, 11).Value = rpt.RWPrecastPanel.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RWPrecastPanel.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.RWPrecastPanel.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.RWPrecastPanel.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.RWPrecastPanel.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.RWPrecastPanel.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RWPrecastPanel.Remarks;
                        i = 72;
                        worksheet.Cell(i, 11).Value = rpt.RWTimber.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RWTimber.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.RWTimber.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.RWTimber.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.RWTimber.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.RWTimber.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RWTimber.Remarks;
                        i = 73;
                        worksheet.Cell(i, 11).Value = rpt.RWSoliNail.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RWSoliNail.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.RWSoliNail.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.RWSoliNail.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.RWSoliNail.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.RWSoliNail.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RWSoliNail.Remarks;
                        i = 74;
                        worksheet.Cell(i, 11).Value = rpt.RWOthers.AverageWidth;
                        worksheet.Cell(i, 14).Value = rpt.RWOthers.TotalLength;
                        worksheet.Cell(i, 17).Value = rpt.RWOthers.Condition1;
                        worksheet.Cell(i, 18).Value = rpt.RWOthers.Condition2;
                        worksheet.Cell(i, 19).Value = rpt.RWOthers.Condition3;
                        worksheet.Cell(i, 20).Value = rpt.RWOthers.Needed;
                        worksheet.Cell(i, 22).Value = rpt.RWOthers.Remarks;


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

        public async Task<List<FormFSDetailRequestDTO>> GetRecordList(int headerId)
        {
            return await _repoUnit.FormFSDetailRepository.GetRecordList(headerId);
        }
    }
}

