using Microsoft.EntityFrameworkCore;
using RAMMS.Common;
using RAMMS.Common.Extensions;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RAMMS.Repository
{
    public class FormQa2Repository : RepositoryBase<RmFormQa2Hdr>, IFormQa2Repository
    {

        public FormQa2Repository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public (int id, bool aleadyExists) CheckExistence(string rdCode, int month, int year)
        {
            var result = _context.RmFormQa2Hdr
              .FirstOrDefault(s =>
         (s.FqaiihCrDt.HasValue ? s.FqaiihCrDt.Value.Month : 0) == month &&
         (s.FqaiihCrDt.HasValue ? s.FqaiihCrDt.Value.Year : 0) == year);
            if (result != null)
            {
                return (result.FqaiihPkRefNo, true);
            }
            else
            {
                var d = _context.RmFormQa2Hdr.OrderByDescending(s => s.FqaiihPkRefNo).FirstOrDefault();
                if (d != null)
                    return (d.FqaiihPkRefNo + 1, false);
                else
                {
                    return (1, false);
                }
            }
        }

        public async Task<int> CheckwithRef(FormQa2HeaderRequestDTO formHeader)
        {
            var data = await _context.RmFormQa2Hdr.Where(x => x.FqaiihPkRefNo == formHeader.No).FirstOrDefaultAsync();
            if (data != null)
            {
                return data.FqaiihPkRefNo;
            }
            else
            {
                return 0;
            }
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormQa2SearchGridDTO> filterOptions)
        {
            var query = (from x in _context.RmFormQa2Hdr select x);
            PrepareFilterQuery(filterOptions, ref query);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormQa2Hdr>> GetFilteredRecordList(FilteredPagingDefinition<FormQa2SearchGridDTO> filterOptions)
        {
            List<RmFormQa2Hdr> result = new List<RmFormQa2Hdr>();
            var query = (from x in _context.RmFormQa2Hdr select x);


            PrepareFilterQuery(filterOptions, ref query);

            result = await query.OrderByDescending(s => s.FqaiihPkRefNo)
                                .Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        public async Task<RmFormQa2Hdr> GetFormWithDetailsByNoAsync(int formNo)
        {
            return await _context.RmFormQa2Hdr
                         .FirstOrDefaultAsync(x => x.FqaiihPkRefNo == formNo);
        }

        public async Task<RmFormQa2Hdr> GetFormQa2WithDetailsAsync(int formNo)
        {
            var data = await _context.RmFormQa2Hdr.Include(x => x.RmFormQa2Dtl).FirstOrDefaultAsync(s => s.FqaiihPkRefNo == formNo);
            return data;
        }

        public async Task<int> GetMaxCount()
        {
            var count = await _context.RmFormQa2Hdr.Select(m => m.FqaiihPkRefNo).CountAsync();
            if (count > 0)
            {
                var query = (from x in _context.RmFormQa2Hdr select x);
                return await query.MaxAsync(m => m.FqaiihPkRefNo);
            }
            return 0;
        }

        public async Task<IEnumerable<RmDdLookup>> GetRMU()
        {
            return await _context.RmDdLookup.Where(x => x.DdlType == "RMU").ToListAsync();
        }

        public async Task<IEnumerable<RmRoadMaster>> GetRoadCodes()
        {
            return await _context.RmRoadMaster.Where(x => x.RdmActiveYn == true).ToListAsync();
        }

        public async Task<IEnumerable<RmRoadMaster>> GetRoadCodesByRMU(string rmu)
        {
            if (rmu == "" || rmu == null)
                return await _context.RmRoadMaster.Where(x => x.RdmActiveYn == true).ToListAsync();
            else
                return await _context.RmRoadMaster.Where(x => x.RdmActiveYn == true && x.RdmRmuCode == rmu).ToListAsync();
        }

        public async Task<List<string>> GetSectionByRMU(string rmu)
        {
            var query = (from section in _context.RmFormAHdr
                         where section.FahRmu == rmu && section.FahActiveYn == true
                         select section.FahSection).ToListAsync().ConfigureAwait(false);
            return await query;
        }

        public async Task<IEnumerable<RmDdLookup>> GetSectionCode()
        {
            return await _context.RmDdLookup.Where(x => x.DdlType == "Section Code").ToListAsync();
        }

        public async Task<RmFormQa2Hdr> SaveFormQa2Hdr(RmFormQa2Hdr rmFormQa2Hdr)
        {
            try
            {
               var rmFormQa2HdrRes = await _context.RmFormQa2Hdr.Where(s => s.FqaiihRoadCode == rmFormQa2Hdr.FqaiihRoadCode && s.FqaiihRmu == rmFormQa2Hdr.FqaiihRmu
                               && s.FqaiihMonth == rmFormQa2Hdr.FqaiihMonth
                               && s.FqaiihYear == rmFormQa2Hdr.FqaiihYear
                               && s.FqaiihActiveYn == true).FirstOrDefaultAsync();
                if (rmFormQa2HdrRes != null)
                {
                    if (rmFormQa2HdrRes.FqaiihPkRefNo != 0)
                    {
                        return rmFormQa2HdrRes;
                    }                   
                }
                else
                {
                    rmFormQa2Hdr.FqaiihActiveYn = true;
                    _context.RmFormQa2Hdr.Add(rmFormQa2Hdr);
                    await _context.SaveChangesAsync();
                    return rmFormQa2Hdr;
                }
                return null;

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        private void PrepareFilterQuery(FilteredPagingDefinition<FormQa2SearchGridDTO> filterOptions, ref IQueryable<RmFormQa2Hdr> query)
        {
            query = query.Where(x => x.FqaiihActiveYn == true);

            if (filterOptions.Filters != null)
            {
                if (!string.IsNullOrEmpty(filterOptions.Filters.Road_Code))
                {
                    query = query.Where(x => x.FqaiihRoadCode == filterOptions.Filters.Road_Code);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RoadName))
                {
                    query = query.Where(x => x.FqaiihRoadName == filterOptions.Filters.RoadName);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
                {
                    query = query.Where(x => x.FqaiihRmu == filterOptions.Filters.RMU);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.FqaiihRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FqaiihRoadCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FqaiihRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FqaiihRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FqaiihUsernameQaIv.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.FqaiihPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }
        }

        public async Task<GridWrapper<object>> GetFormQa2HeaderGrid(DataTableAjaxPostModel searchData)
        {
            var query = (from hdr in _context.RmFormQa2Hdr
                         from rmu in _context.RmDdLookup.Where(rd => rd.DdlType == "RMU" && (rd.DdlTypeCode == hdr.FqaiihRmu || rd.DdlTypeValue == hdr.FqaiihRmu)).DefaultIfEmpty()
                         select new
                         {
                             RefNo = hdr.FqaiihPkRefNo,
                             RefID = hdr.FqaiihRefId,
                             RMU = hdr.FqaiihRmu,
                             RoadCode = hdr.FqaiihRoadCode,
                             RoadName = hdr.FqaiihRoadName,
                             Status = (hdr.FqaiihSubmitSts ? "Submitted" : "Saved"),
                             SubmitSts = hdr.FqaiihSubmitSts,
                             crewSupervisor = hdr.FqaiihCrewSup,
                             CrewSupName =  hdr.FqaiihCrewSupName,
                             Active = hdr.FqaiihActiveYn,
                             RMUCode = rmu.DdlTypeCode,
                             RMUDesc = rmu.DdlTypeDesc
                         });
            query = query.Where(x => x.Active == true);
            if (searchData.filter != null)
            {
                foreach (var item in searchData.filter.Where(x => !string.IsNullOrEmpty(x.Value)))
                {
                    string strVal = Utility.ToString(item.Value).Trim();
                    switch (item.Key)
                    {
                        case "KeySearch":
                            query = query.Where(x =>
                                 (x.RefID ?? "").Contains(strVal)
                                 || (x.RMU ?? "").Contains(strVal)
                                 || (x.RMUDesc ?? "").Contains(strVal)
                                 || (x.RoadCode ?? "").Contains(strVal)
                                 || (x.RoadName ?? "").Contains(strVal)
                                 || (x.crewSupervisor ?? "").Contains(strVal)
                                 || (x.Status ?? "").Contains(strVal)
                                 );
                            break;
                        default:
                            query = query.WhereEquals(item.Key, strVal);
                            break;
                    }
                }

            }
            GridWrapper<object> grid = new GridWrapper<object>();
            grid.recordsTotal = await query.CountAsync();
            grid.recordsFiltered = grid.recordsTotal;
            grid.draw = searchData.draw;
            grid.data = await query.Order(searchData, query.OrderByDescending(s => s.RefNo)).Skip(searchData.start)
                                .Take(searchData.length)
                                .ToListAsync();

            return grid;
        }


        private void PrepareDetailFilterQuery(FilteredPagingDefinition<FormQa2SearchGridDTO> filterOptions, ref IQueryable<RmFormQa2Hdr> query)
        {
            query = query.Where(x => x.FqaiihActiveYn == true);
            if (filterOptions.Filters != null)
            {
                if (!string.IsNullOrEmpty(filterOptions.Filters.Road_Code))
                {
                    query = query.Where(x => x.FqaiihRoadCode == filterOptions.Filters.Road_Code);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RoadName))
                {
                    query = query.Where(x => x.FqaiihRoadName == filterOptions.Filters.RoadName);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
                {
                    query = query.Where(x => x.FqaiihRmu == filterOptions.Filters.RMU);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.FqaiihRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FqaiihRoadCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FqaiihRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FqaiihRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.FqaiihUsernameQaIv.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.FqaiihPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }
        }


        public async Task<int> LastInsertedRecord()
        {
            var data = await _context.RmFormQa2Hdr.OrderByDescending(s => s.FqaiihPkRefNo).FirstOrDefaultAsync();
            if (data != null)
                return data.FqaiihPkRefNo;
            else
                return 0;
        }

        public async Task<(int id, bool aleadyExists)> CheckExistence(string rdCode, string rmu, string month, string year)
        {
            var data = await _context.RmFormQa2Hdr.OrderBy(s => s.FqaiihPkRefNo).FirstOrDefaultAsync(s => s.FqaiihRoadCode == rdCode
            && s.FqaiihRmu == rmu
            && s.FqaiihMonth == Convert.ToInt32(month)
            && s.FqaiihYear == Convert.ToInt32(year) && s.FqaiihActiveYn==true
            );
            if (data != null)
                return (data.FqaiihPkRefNo, true);
            else
                return (await LastInsertedRecord(), false);
        }

        public FORMQA2Rpt GetReportData(int headerId)
        {
            FORMQA2Rpt rpt = new FORMQA2Rpt();
            rpt.Header = (from o in _context.RmFormQa2Hdr
                          where o.FqaiihPkRefNo == headerId
                          select new FORMQAHeaderRpt
                          {
                              Comments = o.FqaiihComments,
                              CrewSupervisor = o.FqaiihCrewSupName,
                              IDesignation = o.FqaiihDesignationQaI,
                              IIDesignation = o.FqaiihDesignationQaIi,
                              IIIDesignation = o.FqaiihDesignationQaIii,
                              IIIName = o.FqaiihUsernameQaIii,
                              IIIRemarks = o.FqaiihRemarksQaIii,
                              IIISignDate = "NA",
                              IIName = o.FqaiihUsernameQaIi,
                              IIRemarks = o.FqaiihRemarksQaIi,
                              IISignDate = "NA",
                              IName = o.FqaiihUsernameQaI,
                              InitialConDesignation = o.FqaiihDesignationQaIni,
                              InitialConName = o.FqaiihUsernameQaIni,
                              InitialRemarks = o.FqaiihRemarksQaIni,
                              InitialConSignDate = "NA",
                              IRemarks = o.FqaiihRemarksQaI,
                              ISignDate = "NA",
                              IVDesignation = o.FqaiihDesignationQaIv,
                              IVName = o.FqaiihUsernameQaIv,
                              IVRemarks = o.FqaiihRemarksQaIv,
                              IVSignDate = "NA",
                              ReferenceNo = o.FqaiihRefId,
                              RMU = o.FqaiihRmu,
                              RoadCode = o.FqaiihRoadCode,
                              RoadName = o.FqaiihRoadName
                          }).FirstOrDefault();
          
            rpt.Detail = (from o in _context.RmFormQa2Dtl
                          where o.FqaiidFqaiihPkRefNo == headerId
                          && o.FqaiidActiveYn == true
                          select new FORMQa2DetailRpt
                          {
                              DateI = o.FqaiidDtInitialCond.HasValue ? o.FqaiidDtInitialCond.Value.ToString("dd-MM-yyyy") : "",
                              DateII = o.FqaiidDtQaI.HasValue ? o.FqaiidDtQaI.Value.ToString("dd-MM-yyyy") : "",
                              DateIII = o.FqaiidDtQaIi.HasValue ? o.FqaiidDtQaIi.Value.ToString("dd-MM-yyyy") : "",
                              DateIV = o.FqaiidDtQaIii.HasValue ? o.FqaiidDtQaIii.Value.ToString("dd-MM-yyyy") : "",
                              DateV = o.FqaiidDtQaIv.HasValue ? o.FqaiidDtQaIv.Value.ToString("dd-MM-yyyy") : "",
                              Defect = o.FqaiidDefCode,
                              DefectDescription = o.FqaiidDefDesc,
                              IIIRating = o.FqaiidQaIii.HasValue ? o.FqaiidQaIii.Value.ToString() : "",
                              IIRating = o.FqaiidQaIi.HasValue ? o.FqaiidQaIi.Value.ToString() : "",
                              InitCondRating = o.FqaiidInitialCond.HasValue ? o.FqaiidInitialCond.ToString() : "",
                              IRating = o.FqaiidQaI.HasValue ? o.FqaiidQaI.Value.ToString() : "",
                              IVRating = o.FqaiidQaIv.HasValue ? o.FqaiidQaIv.Value.ToString() : "",
                              LocationFrom = o.FqaiidFrmCh.GetValueOrDefault().ToString() + "." + o.FqaiidFrmChDeci.GetValueOrDefault().ToString(),
                              LocationTo = o.FqaiidToCh.GetValueOrDefault().ToString() + "." + o.FqaiidToChDeci.GetValueOrDefault().ToString(),
                              RemarksComments = o.FqaiidRemarks,
                              DimLen = o.FqaiidRwrkDimL,
                              DimWid = o.FqaiidRwrkDimW,
                              DimHeight = o.FqaiidRwrkDimH,
                              SiteRef = o.FqaiidSiteRef,
                              WorkActivity = o.FqaiidWrkAct,
                              WWS = o.FqaiidWws13aFol,
                              DtlNo = o.FqaiidPkRefNo
                          }).OrderBy(x => x.DtlNo).ToArray();

            return rpt;
        }
    }
}
