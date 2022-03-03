using Microsoft.EntityFrameworkCore;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository;
using RAMMS.Repository.Interfaces;
using RAMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return await _context.RmIwFormW2.Include(x => x.Fw2Fw1PkRefNoNavigation).Where(x => x.Fw2PkRefNo == Id && x.Fw2ActiveYn == true && x.Fw2Fw1PkRefNoNavigation.Fw1PkRefNo == x.Fw2Fw1PkRefNo).FirstOrDefaultAsync();
        }       

        public void UpdateImage(RmIwFormW2Image image)
        {
            _context.Set<RmIwFormW2Image>().Attach(image);
            _context.Entry(image).State = EntityState.Modified;
        }

        public Task<List<RmIwformImage>> GetImagelist(int formW2Id)
        {
            return _context.RmIwformImage.Where(x => x.FiwiFw1PkRefNo == formW2Id && x.FiwiActiveYn == true).ToListAsync();
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

        public async Task<IEnumerable<RmRoadMaster>> GetRoadCodesByRMU(string rmu)
        {
            if (rmu == "" || rmu == null)
                return await _context.RmRoadMaster.Where(x => x.RdmActiveYn == true).ToListAsync();
            else
                return await _context.RmRoadMaster.Where(x => x.RdmActiveYn == true && x.RdmRmuCode == rmu).ToListAsync();
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
                         let w2Form = _context.RmIwFormW2.FirstOrDefault(s => s.Fw2Fw1PkRefNo == x.Fw1PkRefNo)
                         let fecm = _context.RmIwFormW2Fecm.FirstOrDefault(s => s.FecmFw2PkRefNo == w2Form.Fw2PkRefNo)
                         select new { rmu, w2Form, fecm, x });



            query = query.Where(x => x.x.Fw1ActiveYn == true).OrderByDescending(x => x.x.Fw1ModDt);

            if (!string.IsNullOrEmpty(filterOptions.Filters.IWRefNo))
            {
                query = query.Where(x => x.x.Fw1IwRefNo == filterOptions.Filters.IWRefNo);
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

            if (!string.IsNullOrEmpty(filterOptions.Filters.ProjectTitle))
            {
                query = query.Where(x => x.x.Fw1ProjectTitle.Contains(filterOptions.Filters.ProjectTitle));
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.Status))
            {
                query = query.Where(x => x.x.Fw1Sts.Contains(filterOptions.Filters.Status) || x.x.Fw1StsRemarks.Contains(filterOptions.Filters.Status));
            }

            if (filterOptions.Filters.PercentageFrom.HasValue && filterOptions.Filters.PercentageTo.HasValue)
            {
                query = query.Where(x => x.fecm.FecmProgressPerc >= filterOptions.Filters.PercentageFrom && x.fecm.FecmProgressPerc <= filterOptions.Filters.PercentageTo);
            }

            if (filterOptions.Filters.PercentageFrom.HasValue && !filterOptions.Filters.PercentageTo.HasValue)
            {
                query = query.Where(x => x.fecm.FecmProgressPerc >= filterOptions.Filters.PercentageFrom && x.fecm.FecmProgressPerc <= filterOptions.Filters.PercentageFrom);
            }

            if (!filterOptions.Filters.PercentageFrom.HasValue && filterOptions.Filters.PercentageTo.HasValue)
            {
                query = query.Where(x => x.fecm.FecmProgressPerc >= filterOptions.Filters.PercentageTo && x.fecm.FecmProgressPerc <= filterOptions.Filters.PercentageTo);
            }

            if (filterOptions.Filters.Months.HasValue)
            {
                query = query.Where(x => (x.x.Fw1PropDesignDuration >= filterOptions.Filters.Months && x.x.Fw1PropDesignDuration <= filterOptions.Filters.Months)
                || (x.x.Fw1PropCompletionPeriod >= filterOptions.Filters.Months && x.x.Fw1PropCompletionPeriod <= filterOptions.Filters.Months));
            }


            if (!string.IsNullOrEmpty(filterOptions.Filters.RoadCode))
            {
                query = query.Where(x => x.x.Fw1RoadCode == filterOptions.Filters.RoadCode);
            }


            if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
            {
                query = query.Where(x => x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc == filterOptions.Filters.RMU);
            }


            if (!string.IsNullOrEmpty(filterOptions.Filters.RoadCode))
            {
                query = query.Where(x => x.x.Fw1RoadCode == filterOptions.Filters.RoadCode);
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
                                    || (x.x.Fw1Sts ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1StsRemarks ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameRep ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.fecm.FecmProgressPerc.HasValue ? x.fecm.FecmProgressPerc.ToString() : "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameReq ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameVer ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.w2Form.Fw2DateOfInitation.HasValue ? (x.w2Form.Fw2DateOfInitation.Value.Year == dt.Year && x.w2Form.Fw2DateOfInitation.Value.Month == dt.Month && x.w2Form.Fw2DateOfInitation.Value.Day == dt.Day) : true)
                                    || (x.x.Fw1Dt.HasValue ? (x.x.Fw1Dt.Value.Year == dt.Year && x.x.Fw1Dt.Value.Month == dt.Month && x.x.Fw1Dt.Value.Day == dt.Day) : true)
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
                                    || (x.x.Fw1Sts ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1StsRemarks ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameRep ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.fecm.FecmProgressPerc.HasValue ? x.fecm.FecmProgressPerc.ToString() : "").Contains(filterOptions.Filters.SmartInputValue)
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
                         let w2Form = _context.RmIwFormW2.FirstOrDefault(s => s.Fw2Fw1PkRefNo == x.Fw1PkRefNo)
                         let fecm = _context.RmIwFormW2Fecm.FirstOrDefault(s => s.FecmFw2PkRefNo == w2Form.Fw2PkRefNo)
                         select new { rmu, w2Form, fecm, x });



            query = query.Where(x => x.x.Fw1ActiveYn == true).OrderByDescending(x => x.x.Fw1ModDt);

            if (!string.IsNullOrEmpty(filterOptions.Filters.IWRefNo))
            {
                query = query.Where(x => x.x.Fw1IwRefNo.Contains(filterOptions.Filters.IWRefNo));
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.PrjTitle))
            {
                query = query.Where(x => x.x.Fw1ProjectTitle.Contains(filterOptions.Filters.PrjTitle));
            }


            if (!string.IsNullOrEmpty(filterOptions.Filters.CommencementFrom) && !string.IsNullOrEmpty(filterOptions.Filters.CommencementTo))
            {
                DateTime dtFrom, dtTo;
                DateTime.TryParseExact(filterOptions.Filters.CommencementFrom, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtFrom);
                DateTime.TryParseExact(filterOptions.Filters.CommencementTo, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtTo);
                
                {
                    query = query.Where(x => x.w2Form.Fw2DtCommence.HasValue ? x.w2Form.Fw2DtCommence >= dtFrom && x.w2Form.Fw2DtCommence <= dtTo : false);
                }
            }

            if (!string.IsNullOrEmpty(filterOptions.Filters.CommencementFrom) && string.IsNullOrEmpty(filterOptions.Filters.CommencementTo))
            {
                DateTime dt;
                if (DateTime.TryParseExact(filterOptions.Filters.CommencementTo, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(x => x.w2Form.Fw2DtCommence.HasValue ? (x.w2Form.Fw2DtCommence.Value.Year == dt.Year && x.w2Form.Fw2DtCommence.Value.Month == dt.Month && x.w2Form.Fw2DtCommence.Value.Day == dt.Day) : false);
                }
            }

             if (string.IsNullOrEmpty(filterOptions.Filters.CommencementFrom) && !string.IsNullOrEmpty(filterOptions.Filters.CommencementTo))
            {
                DateTime dt;
                if (DateTime.TryParseExact(filterOptions.Filters.CommencementTo, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                {
                    query = query.Where(x => x.w2Form.Fw2DtCommence.HasValue ? (x.w2Form.Fw2DtCommence.Value.Year == dt.Year && x.w2Form.Fw2DtCommence.Value.Month == dt.Month && x.w2Form.Fw2DtCommence.Value.Day == dt.Day) : false);
                }
            }

           

            if (!string.IsNullOrEmpty(filterOptions.Filters.Status) )
            {
                switch (filterOptions.Filters.Status)
                {
                    case "Received":
                        query = query.Where(x => x.w2Form.Fw2Status.Contains(filterOptions.Filters.Status));
                        break;
                    case "Saved":
                        query = query.Where(x => x.w2Form.Fw2Status.Contains(filterOptions.Filters.Status) || x.x.Fw1Status.Contains(filterOptions.Filters.Status));
                        break;
                    case "Submitted":
                        query = query.Where(x => x.w2Form.Fw2Status.Contains(filterOptions.Filters.Status) || x.x.Fw1Status.Contains(filterOptions.Filters.Status));
                        break;
                    case "Approved":
                        query = query.Where(x => x.x.Fw1Status.Contains(filterOptions.Filters.Status));
                        break;
                    case "Rejected":
                        query = query.Where(x => x.x.Fw1Status.Contains(filterOptions.Filters.Status));
                        break;
                    default:
                        break;
                }

                
            }

            if (filterOptions.Filters.PercentageFrom.HasValue && filterOptions.Filters.PercentageTo.HasValue)
            {
                query = query.Where(x => x.fecm.FecmProgressPerc >= filterOptions.Filters.PercentageFrom && x.fecm.FecmProgressPerc <= filterOptions.Filters.PercentageTo);
            }

            if (filterOptions.Filters.PercentageFrom.HasValue && !filterOptions.Filters.PercentageTo.HasValue)
            {
                query = query.Where(x => x.fecm.FecmProgressPerc >= filterOptions.Filters.PercentageFrom && x.fecm.FecmProgressPerc <= filterOptions.Filters.PercentageFrom);
            }

            if (!filterOptions.Filters.PercentageFrom.HasValue && filterOptions.Filters.PercentageTo.HasValue)
            {
                query = query.Where(x => x.fecm.FecmProgressPerc >= filterOptions.Filters.PercentageTo && x.fecm.FecmProgressPerc <= filterOptions.Filters.PercentageTo);
            }

            if (filterOptions.Filters.Months.HasValue)
            {
                query = query.Where(x => (x.x.Fw1PropDesignDuration >= filterOptions.Filters.Months && x.x.Fw1PropDesignDuration <= filterOptions.Filters.Months)
                || (x.x.Fw1PropCompletionPeriod >= filterOptions.Filters.Months && x.x.Fw1PropCompletionPeriod <= filterOptions.Filters.Months));
            }


            if (!string.IsNullOrEmpty(filterOptions.Filters.RoadCode))
            {
                query = query.Where(x => x.x.Fw1RoadCode == filterOptions.Filters.RoadCode);
            }


            if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
            {
                query = query.Where(x => x.x.Fw1RmuCode == filterOptions.Filters.RMU || x.rmu.DdlTypeCode == filterOptions.Filters.RMU || x.rmu.DdlTypeDesc.Contains(filterOptions.Filters.RMU));
            }


            if (!string.IsNullOrEmpty(filterOptions.Filters.RoadCode))
            {
                query = query.Where(x => x.x.Fw1RoadCode == filterOptions.Filters.RoadCode);
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
                                    || (x.x.Fw1Sts ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1StsRemarks ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameRep ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.fecm.FecmProgressPerc.HasValue ? x.fecm.FecmProgressPerc.ToString() : "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameReq ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameVer ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.w2Form.Fw2DateOfInitation.HasValue ? (x.w2Form.Fw2DateOfInitation.Value.Year == dt.Year && x.w2Form.Fw2DateOfInitation.Value.Month == dt.Month && x.w2Form.Fw2DateOfInitation.Value.Day == dt.Day) : true)
                                    || (x.x.Fw1Dt.HasValue ? (x.x.Fw1Dt.Value.Year == dt.Year && x.x.Fw1Dt.Value.Month == dt.Month && x.x.Fw1Dt.Value.Day == dt.Day) : true)
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
                                    || (x.x.Fw1Sts ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1StsRemarks ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameRep ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.fecm.FecmProgressPerc.HasValue ? x.fecm.FecmProgressPerc.ToString() : "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameReq ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1UsernameVer ?? "").Contains(filterOptions.Filters.SmartInputValue)
                                    || (x.x.Fw1SubmitSts ? "Submitted" : "Saved").Contains(filterOptions.Filters.SmartInputValue));
                }
            }

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 2)
                    query = query.OrderBy(s => s.x.Fw1ProjectTitle);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderBy(s => s.x.Fw1InitialProposedDate);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderBy(s => s.x.Fw1RecomdBydeYn);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderBy(s => s.x.Fw1Dt);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderBy(s => s.x.Fw1RecomdYn);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderBy(s => s.w2Form.Fw2EstCostAmt);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderBy(s => s.fecm.FecmDtTecm);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderBy(s => s.fecm.FecmDt);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderBy(s => s.fecm.FecmAgreedNegoLetrYn);
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
                    query = query.OrderByDescending(s => s.x.Fw1ProjectTitle);
                if (filterOptions.ColumnIndex == 3)
                    query = query.OrderByDescending(s => s.x.Fw1InitialProposedDate);
                if (filterOptions.ColumnIndex == 4)
                    query = query.OrderByDescending(s => s.x.Fw1RecomdBydeYn);
                if (filterOptions.ColumnIndex == 5)
                    query = query.OrderByDescending(s => s.x.Fw1Dt);
                if (filterOptions.ColumnIndex == 6)
                    query = query.OrderByDescending(s => s.x.Fw1RecomdYn);
                if (filterOptions.ColumnIndex == 7)
                    query = query.OrderByDescending(s => s.w2Form.Fw2EstCostAmt);
                if (filterOptions.ColumnIndex == 8)
                    query = query.OrderByDescending(s => s.fecm.FecmDtTecm);
                if (filterOptions.ColumnIndex == 9)
                    query = query.OrderByDescending(s => s.fecm.FecmDt);
                if (filterOptions.ColumnIndex == 10)
                    query = query.OrderByDescending(s => s.fecm.FecmAgreedNegoLetrYn);
                //if (filterOptions.ColumnIndex == 11)
                //    query = query.OrderByDescending(s => s.x.Fw1UsernameVer);
                //if (filterOptions.ColumnIndex == 12)
                //    query = query.OrderByDescending(s => s.x.Fw1UsernameRcvdAuth);
                //if (filterOptions.ColumnIndex == 13)
                //    query = query.OrderByDescending(s => s.x.Fw1UsernameVetAuth);

            }
            result = await (from form in query.OrderByDescending(o => o.x.Fw1PkRefNo)
                            let w1Form = form.x
                            let w2Form = form.w2Form
                            let fecm = form.fecm
                            select new FormIWResponseDTO
                            {
                                W1RefNo = w1Form.Fw1PkRefNo.ToString(),
                                W2RefNo = w2Form.Fw2PkRefNo.ToString(),
                                WCRefNo = "0",
                                WGRefNo = "0",
                                WDRefNo = "0",
                                WNRefNo = "0",
                                W1Status = w1Form.Fw1Status,
                                W1SubStatus = w1Form.Fw1SubmitSts,
                                W2Status = w2Form.Fw2Status,
                                W2SubStatus = w2Form.Fw2SubmitSts ,
                                iWReferenceNo = w1Form.Fw1IwRefNo,
                                projectTitle = w1Form.Fw1ProjectTitle,
                                overAllStatus = w2Form.Fw2Status != "" ? "W2 - " + w2Form.Fw2Status : "W1 - " + w1Form.Fw1Status ,
                                initialPropDt = w1Form.Fw1InitialProposedDate != null ? DateTime.Parse(Convert.ToString(w1Form.Fw1InitialProposedDate)).ToString("dd/MM/yyyy") : "-",
                                recommdDEYN = w1Form.Fw1RecomdBydeYn != null && w1Form.Fw1RecomdBydeYn == true ? "Yes" : "No",
                                w1dt = w1Form.Fw1Dt != null ?  DateTime.Parse(Convert.ToString(w1Form.Fw1Dt)).ToString("dd/MM/yyyy") : "-",
                                recommdYN = w1Form.Fw1RecomdYn != null && w1Form.Fw1RecomdYn == true ? "Yes" : "No",
                                estimatedCost =  w1Form.Fw1EstimTotalCostAmt.HasValue  ? String.Format("{0:N}",w1Form.Fw1EstimTotalCostAmt) : "0.00",
                                w2dt = w2Form.Fw2DateOfInitation != null ? DateTime.Parse(Convert.ToString(w2Form.Fw2DateOfInitation)).ToString("dd/MM/yyyy") : "-",
                                tecmDt = fecm.FecmDtTecm != null ?  DateTime.Parse(Convert.ToString(fecm.FecmDtTecm)).ToString("dd/MM/yyyy") : "-",
                                fecmDt = fecm.FecmDt != null ?  DateTime.Parse(Convert.ToString(fecm.FecmDt)).ToString("dd/MM/yyyy") : "-",
                                agreedNegoYN = fecm.FecmAgreedNegoLetrYn != null && fecm.FecmAgreedNegoLetrYn == true ? "Yes" : "No",
                                agreedNegoPriceDt = fecm.FecmDtAgreedNego  != null ? DateTime.Parse(Convert.ToString(fecm.FecmDtAgreedNego)).ToString("dd/MM/yyyy") : "-",
                                issueW2Ref = w2Form.Fw2JkrRefNo , 
                                commenceDt = w2Form.Fw2DtCommence != null ? DateTime.Parse(Convert.ToString(w2Form.Fw2DtCommence)).ToString("dd/MM/yyyy") : "-",
                                compDt = w2Form.Fw2DtCompl != null ? DateTime.Parse(Convert.ToString(w2Form.Fw2DtCompl)).ToString("dd/MM/yyyy") : "-",
                                wdDt = "-",
                                newCompDt = "-",
                                wnDt = "-",
                                ContractPeriod = w2Form.Fw2IwDuration.HasValue ? String.Format("{0:N}", w2Form.Fw2IwDuration) : "0",
                                wcDt = "-",
                                dlpPeriod = "-",
                                finalAmt = "0.00",
                                sitePhy = fecm.FecmProgressPerc.HasValue ? String.Format("{0:N}", fecm.FecmProgressPerc) : "0",
                                wgDate = "-",

                            }).Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync().ConfigureAwait(false);
            return result;
        }
    }
}
