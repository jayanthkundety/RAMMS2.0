using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RAMMS.Common;
using RAMMS.Common.Extensions;
using RAMMS.Common.RefNumber;
using RAMMS.Domain.Models;
using RAMMS.DTO;
using RAMMS.DTO.Report;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository
{
    public class FormW2Repository : RepositoryBase<RmIwFormW2>, IFormW2Repository
    {
        public FormW2Repository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public RmIwFormW2 SaveFormW2(RmIwFormW2 formW2, bool updateSubmit)
        {
            return formW2;
        }

        public async Task<RmIwFormW2> FindW2ByID(int Id)
        {
            return await _context.RmIwFormW2.Include(x => x.Fw2Fw1RefNoNavigation).Where(x => x.Fw2PkRefNo == Id && x.Fw2ActiveYn == true && x.Fw2Fw1RefNoNavigation.Fw1PkRefNo == x.Fw2Fw1RefNo).FirstOrDefaultAsync();
        }

        public void SaveImage(IEnumerable<RmIwFormW2Image> image)
        {
            _context.RmIwFormW2Image.AddRange(image);
        }

        public void UpdateImage(RmIwFormW2Image image)
        {
            _context.Set<RmIwFormW2Image>().Attach(image);
            _context.Entry(image).State = EntityState.Modified;
        }

        public Task<List<RmIwFormW2Image>> GetImagelist(int formW2Id)
        {
            return _context.RmIwFormW2Image.Where(x => x.Fw2iFw2RefNo == formW2Id && x.Fw2iActiveYn == true).ToListAsync();
        }

        public Task<RmIwFormW2Image> GetImageById(int imageId)
        {
            return _context.RmIwFormW2Image.Where(x => x.Fw2iPkRefNo == imageId).FirstOrDefaultAsync();
        }

        public async Task<int> GetImageId(int formW2Id, string type)
        {
            int? result = await _context.RmIwFormW2Image.Where(x => x.Fw2iFw2RefNo == formW2Id && x.Fw2iImageTypeCode == type).Select(x => x.Fw2iImageSrno).MaxAsync();
            return result.HasValue ? result.Value : 0;
        }

        public async Task<List<RmIwFormW1>> GetFormW1List()
        {
            return await _context.RmIwFormW1.Where(x => x.Fw1ActiveYn == true).ToListAsync();
        }

        public async Task<RmIwFormW1> GetFormW1ById(int formW1Id)
        {
            return await _context.RmIwFormW1.Where(x => x.Fw1ActiveYn == true && x.Fw1PkRefNo == formW1Id).FirstOrDefaultAsync();
        }

        public async Task<RmIwFormW1> GetFormW1ByRoadCode(string roadCode)
        {
            return await _context.RmIwFormW1.Where(x => x.Fw1ActiveYn == true && x.Fw1RoadCode == roadCode).FirstOrDefaultAsync();
        }


        public async Task<List<FormIWResponseDTO>> GetFilteredFormIWGrid(FilteredPagingDefinition<FormIWSearchGridDTO> filterOptions)
        {
            var w1Form = await _context.RmIwFormW1.Include(w => w.RmIwFormW2).Where(x => x.Fw1ActiveYn == true).ToListAsync();
            List<FormIWResponseDTO> lstIWForm = new List<FormIWResponseDTO>();
            foreach (RmIwFormW1 rmw1form in w1Form)
            {
                //var iwform = new FormIWResponseDTO();
                //iwform.W1RefNo = rmw1form
                //iwform.projectTitle = rmw1form.Fw1ProjectTitle;
                //iwform.agreedNego = "";
                //iwform.commenDt = "01/01/2020";
                //iwform.compDt = "";
                //iwform.ContractPeriod = "";
                //iwform.dlpPeriod = "";
                //iwform.finalAmt =
                //iwform.financeDt = "";
                //iwform.initialPropDt = "";
                //iwform.issueW2Ref = "";
                //iwform.recommd = "";
                //iwform.recommdDE = "";
                //iwform.sitePhy = "";
                //iwform.status = "";
                //iwform.technicalDt = "";
                //iwform.w1dt = "";
            }

            return lstIWForm;
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormIWSearchGridDTO> filterOptions)
        {
            var query = (from x in _context.RmIwFormW1
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.Fw1RmuCode || s.DdlTypeDesc == x.Fw1RmuCode))
                         let w2Form = _context.RmIwFormW2.FirstOrDefault(s => s.Fw2Fw1RefNo == x.Fw1PkRefNo)
                         select new { rmu, x, w2Form });



            query = query.Where(x => x.x.Fw1ActiveYn == true).OrderByDescending(x => x.x.Fw1ModDt);


            if (!string.IsNullOrEmpty(filterOptions.Filters.RoadCode))
            {
                query = query.Where(x => x.x.Fw1RoadCode == filterOptions.Filters.RoadCode);
            }



            if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
            {
                query = query.Where(x => x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc == filterOptions.Filters.RMU);

            }


            if (filterOptions.Filters.CommencementFrom != null && filterOptions.Filters.CommencementTo != null)
            {
                DateTime dt;
                if (DateTime.TryParseExact(filterOptions.Filters.CommencementFrom, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(x => x.w2Form.Fw2DtCommence.HasValue ? (x.w2Form.Fw2DtCommence.Value.Year == dt.Year && x.w2Form.Fw2DtCommence.Value.Month == dt.Month && x.w2Form.Fw2DtCommence.Value.Day == dt.Day) : false);
                }
            }

            if (filterOptions.Filters.CommencementTo != null)
            {
                DateTime dt;
                if (DateTime.TryParseExact(filterOptions.Filters.CommencementTo, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(x => x.w2Form.Fw2DtCommence.HasValue ? (x.w2Form.Fw2DtCommence.Value.Year == dt.Year && x.w2Form.Fw2DtCommence.Value.Month == dt.Month && x.w2Form.Fw2DtCommence.Value.Day == dt.Day) : false);
                }
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.RoadCode))
            {
                query = query.Where(x => x.x.Fw1RoadCode == filterOptions.Filters.RoadCode);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.ProjectTitle))
            {
                query = query.Where(x => x.x.Fw1ProjectTitle.Contains(filterOptions.Filters.ProjectTitle));
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.Status))
            {
                query = query.Where(x => x.x.Fw1Status == filterOptions.Filters.Status);
            }

            if (filterOptions.Filters.Percentage.HasValue)
            {
                query = query.Where(x => x.x.Fw1SurvyWorksPercent >= filterOptions.Filters.Percentage && x.x.Fw1SurvyWorksPercent <= filterOptions.Filters.Percentage);
            }

            if (filterOptions.Filters.Months.HasValue)
            {
                query = query.Where(x => x.x.Fw1PropDesignDuration >= filterOptions.Filters.Months && x.x.Fw1PropDesignDuration <= filterOptions.Filters.Months);
            }


            if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
            {

                DateTime dt;
                if (DateTime.TryParseExact(filterOptions.Filters.SmartInputValue, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(x =>
                                   (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                    || (x.x.Fw1IwRefNo ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1ProjectTitle ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1RmuCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1RoadCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameRep ?? "").Contains(filterOptions.Filters.SmartInputValue)

                                    || (x.x.Fw1UsernameReq ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameVer ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.w2Form.Fw2DtInitation.HasValue ? (x.w2Form.Fw2DtInitation.Value.Year == dt.Year && x.w2Form.Fw2DtInitation.Value.Month == dt.Month && x.w2Form.Fw2DtInitation.Value.Day == dt.Day) : true)
                                    || (x.x.Fw1SubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue));
                }
                else
                {
                    query = query.Where(x =>
                                    (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                    || (x.x.Fw1IwRefNo ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1ProjectTitle ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1RmuCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1RoadCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameRep ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameReq ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameVer ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1SubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue));
                }
            }



            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<FormIWResponseDTO>> GetFilteredRecordList(FilteredPagingDefinition<FormIWSearchGridDTO> filterOptions)
        {
            List<FormIWResponseDTO> result = new List<FormIWResponseDTO>();
            var query = (from x in _context.RmIwFormW1
                         let rmu = _context.RmDdLookup.FirstOrDefault(s => s.DdlType == "RMU" && (s.DdlTypeCode == x.Fw1RmuCode || s.DdlTypeDesc == x.Fw1RmuCode))
                         let w2Form = _context.RmIwFormW2.FirstOrDefault(s => s.Fw2Fw1RefNo == x.Fw1PkRefNo)
                         select new { rmu, w2Form, x });



            query = query.Where(x => x.x.Fw1ActiveYn == true).OrderByDescending(x => x.x.Fw1ModDt);

            if (!string.IsNullOrEmpty(filterOptions.Filters.RoadCode))
            {
                query = query.Where(x => x.x.Fw1RoadCode == filterOptions.Filters.RoadCode);
            }



            if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
            {
                query = query.Where(x => x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc == filterOptions.Filters.RMU);

            }


            if (!string.IsNullOrEmpty(filterOptions.Filters.CommencementFrom) && !string.IsNullOrEmpty(filterOptions.Filters.CommencementTo))
            {
                DateTime dt;
                if (DateTime.TryParseExact(filterOptions.Filters.CommencementFrom, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(x => x.w2Form.Fw2DtCommence.HasValue ? (x.w2Form.Fw2DtCommence.Value.Year == dt.Year && x.w2Form.Fw2DtCommence.Value.Month == dt.Month && x.w2Form.Fw2DtCommence.Value.Day == dt.Day) : false);
                }
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.CommencementTo))
            {
                DateTime dt;
                if (DateTime.TryParseExact(filterOptions.Filters.CommencementTo, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(x => x.w2Form.Fw2DtCommence.HasValue ? (x.w2Form.Fw2DtCommence.Value.Year == dt.Year && x.w2Form.Fw2DtCommence.Value.Month == dt.Month && x.w2Form.Fw2DtCommence.Value.Day == dt.Day) : false);
                }
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.RoadCode))
            {
                query = query.Where(x => x.x.Fw1RoadCode == filterOptions.Filters.RoadCode);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.ProjectTitle))
            {
                query = query.Where(x => x.x.Fw1ProjectTitle.Contains(filterOptions.Filters.ProjectTitle));
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.Status))
            {
                // query = query.Where(x => x.x.Fw1Status == filterOptions.Filters.Status);
            }

            if (filterOptions.Filters.Percentage.HasValue)
            {
                query = query.Where(x => x.x.Fw1SurvyWorksPercent >= filterOptions.Filters.Percentage && x.x.Fw1SurvyWorksPercent <= filterOptions.Filters.Percentage);
            }

            if (filterOptions.Filters.Months.HasValue)
            {
                query = query.Where(x => x.x.Fw1PropDesignDuration >= filterOptions.Filters.Months && x.x.Fw1PropDesignDuration <= filterOptions.Filters.Months);
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
            {

                DateTime dt;
                if (DateTime.TryParseExact(filterOptions.Filters.SmartInputValue, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(x =>
                                   (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                    || (x.x.Fw1IwRefNo ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1ProjectTitle ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1RmuCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1RoadCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameRep ?? "").Contains(filterOptions.Filters.SmartInputValue)

                                    || (x.x.Fw1UsernameReq ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameVer ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.w2Form.Fw2DtInitation.HasValue ? (x.w2Form.Fw2DtInitation.Value.Year == dt.Year && x.w2Form.Fw2DtInitation.Value.Month == dt.Month && x.w2Form.Fw2DtInitation.Value.Day == dt.Day) : true)
                                    || (x.x.Fw1SubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue));
                }
                else
                {
                    query = query.Where(x =>
                                    (x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.SmartInputValue))
                                    || (x.x.Fw1IwRefNo ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1ProjectTitle ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1RmuCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1RoadCode ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameRep ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameReq ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameVer ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1SubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue));
                }
            }

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(s => s.x.Fw1IwRefNo);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(s => s.x.Fw1ProjectTitle);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(s => s.x.Fw1Dt);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(s => s.x.Fw1RecomdBydeYn);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(s => s.x.Fw1TecmDt);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(s => s.x.Fw1Status);
                //if (filterOptions.ColumnIndex == 8)
                //    query = query.OrderBy(s => s.x.Fw1InspDt);
                //if (filterOptions.ColumnIndex == 9)
                //    query = query.OrderBy(s => s.x.Fw1AssetGroupCode);
                //if (filterOptions.ColumnIndex == 10)
                //    query = query.OrderBy(s => s.x.Fw1SubmitSts);
                //if (filterOptions.ColumnIndex == 11)
                //    query = query.OrderBy(s => s.x.Fw1UsernameVer);
                //if (filterOptions.ColumnIndex == 12)
                //    query = query.OrderBy(s => s.x.Fw1UsernameRcvdAuth);
                //if (filterOptions.ColumnIndex == 13)
                //    query = query.OrderBy(s => s.x.Fw1UsernameVetAuth);

            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderByDescending(s => s.x.Fw1IwRefNo);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(s => s.x.Fw1ProjectTitle);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(s => s.x.Fw1Dt );
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(s => s.x.Fw1RecomdBydeYn);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(s => s.x.Fw1TecmDt);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(s => s.x.Fw1Status);
                //if (filterOptions.ColumnIndex == 8)
                //    query = query.OrderByDescending(s => s.x.Fw1InspDt);
                //if (filterOptions.ColumnIndex == 9)
                //    query = query.OrderByDescending(s => s.x.Fw1AssetGroupCode);
                //if (filterOptions.ColumnIndex == 10)
                //    query = query.OrderByDescending(s => s.x.Fw1SubmitSts);
                //if (filterOptions.ColumnIndex == 11)
                //    query = query.OrderByDescending(s => s.x.Fw1UsernameVer);
                //if (filterOptions.ColumnIndex == 12)
                //    query = query.OrderByDescending(s => s.x.Fw1UsernameRcvdAuth);
                //if (filterOptions.ColumnIndex == 13)
                //    query = query.OrderByDescending(s => s.x.Fw1UsernameVetAuth);

            }
            result = await (from form in query
                            let w1Form = form.x
                            let w2Form = form.w2Form
                            select new FormIWResponseDTO
                            {
                                W1RefNo = w1Form.Fw1PkRefNo.ToString(),
                                W2RefNo = w2Form.Fw2PkRefNo.ToString(),
                                WCRefNo = "0",
                                WGRefNo = "0",
                                WDRefNo = "0",
                                WNRefNo = "0",
                                projectTitle = w1Form.Fw1ProjectTitle,
                                initialPropDt = Convert.ToString(w1Form.Fw1InitialProposedDate),
                                recommdDE = w1Form.Fw1RecomdBydeYn,
                                w1dt = Convert.ToString(w1Form.Fw1Dt),
                                status = "-",
                                technicalDt = "",
                                financeDt = "",
                                agreedNego = "-",
                                issueW2Ref = "-",
                                commenDt = "01/01/2020",
                                compDt = "",
                                ContractPeriod = "10",
                                dlpPeriod = "10",
                                finalAmt = "",
                                sitePhy = "",

                            }).Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync().ConfigureAwait(false);
            return result;
        }

    }
}
