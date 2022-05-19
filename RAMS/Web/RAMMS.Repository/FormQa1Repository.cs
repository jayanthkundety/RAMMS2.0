using Microsoft.EntityFrameworkCore;
using RAMMS.Common;
using RAMMS.Common.Extensions;
using RAMMS.Common.RefNumber;
using RAMMS.Domain.Models;
using RAMMS.DTO;
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
    public class FormQa1Repository : RepositoryBase<RmFormQa1Hdr>, IFormQa1Repository
    {
        public FormQa1Repository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<int> GetFilteredRecordCount(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions)
        {
            var query = (from x in _context.RmFormQa1Hdr select x);
            PrepareFilterQuery(filterOptions, ref query);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormQa1Hdr>> GetFilteredRecordList(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions)
        {
            List<RmFormQa1Hdr> result = new List<RmFormQa1Hdr>();
            var query = (from x in _context.RmFormQa1Hdr select x);


            PrepareFilterQuery(filterOptions, ref query);

            result = await query.OrderByDescending(s => s.Fqa1hPkRefNo)
                                .Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        private void PrepareFilterQuery(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, ref IQueryable<RmFormQa1Hdr> query)
        {
            query = query.Where(x => x.Fqa1hActiveYn == true);

            if (filterOptions.Filters != null)
            {


                if (!string.IsNullOrEmpty(filterOptions.Filters.RMU))
                {
                    query = query.Where(x => x.Fqa1hRmu == filterOptions.Filters.RMU);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.Section))
                {
                    query = query.Where(x => x.Fqa1hSecCode == filterOptions.Filters.Section);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.RoadCode))
                {
                    query = query.Where(x => x.Fqa1hRoadCode == filterOptions.Filters.RoadCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.Crew))
                {
                    query = query.Where(x => x.Fqa1hCrew.HasValue ? x.Fqa1hCrew == Convert.ToInt32(filterOptions.Filters.Crew) : x.Fqa1hCrew == null);

                }
                if (!string.IsNullOrEmpty(filterOptions.Filters.ActivityCode))
                {
                    query = query.Where(x => x.Fqa1hActCode == filterOptions.Filters.ActivityCode);
                }

                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dtFrom, dtTo;
                    DateTime.TryParseExact(filterOptions.Filters.ByFromdate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtFrom);
                    DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dtTo);

                    {
                        query = query.Where(x => x.Fqa1hDt.HasValue ? x.Fqa1hDt >= dtFrom && x.Fqa1hDt <= dtTo : false);
                    }
                }



                if (!string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.Fqa1hDt.HasValue ? (x.Fqa1hDt.Value.Year == dt.Year && x.Fqa1hDt.Value.Month == dt.Month && x.Fqa1hDt.Value.Day == dt.Day) : false);
                    }
                }

                if (string.IsNullOrEmpty(filterOptions.Filters.ByFromdate) && !string.IsNullOrEmpty(filterOptions.Filters.ByTodate))
                {
                    DateTime dt;
                    if (DateTime.TryParseExact(filterOptions.Filters.ByTodate, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt))
                    {
                        query = query.Where(x => x.Fqa1hDt.HasValue ? (x.Fqa1hDt.Value.Year == dt.Year && x.Fqa1hDt.Value.Month == dt.Month && x.Fqa1hDt.Value.Day == dt.Day) : false);
                    }
                }


                if (!string.IsNullOrEmpty(filterOptions.Filters.SmartInputValue))
                {
                    query = query.Where(x => x.Fqa1hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.Fqa1hRoadCode.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.Fqa1hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.Fqa1hRefId.Contains(filterOptions.Filters.SmartInputValue)
                                        || x.Fqa1hUsernameAssgn.Contains(filterOptions.Filters.SmartInputValue)
                                        || (filterOptions.Filters.SmartInputValue.IsInt() && x.Fqa1hPkRefNo.Equals(filterOptions.Filters.SmartInputValue.AsInt())));

                }
            }
        }

        public async Task<RmFormQa1Hdr> FindSaveFormQa1Hdr(RmFormQa1Hdr formQa1Header, bool updateSubmit)
        {
            bool isAdd = false;
            if (formQa1Header.Fqa1hPkRefNo == 0)
            {
                isAdd = true;
                formQa1Header.Fqa1hActiveYn = true;
                _context.RmFormQa1Hdr.Add(formQa1Header);

            }
            else
            {
                _context.RmFormQa1Hdr.Attach(formQa1Header);
                var entry = _context.Entry(formQa1Header);

                entry.Property(x => x.Fqa1hModBy).IsModified = true;
                entry.Property(x => x.Fqa1hModDt).IsModified = true;

                entry.Property(x => x.Fqa1hUseridAssgn).IsModified = true;
                entry.Property(x => x.Fqa1hInitialAssgn).IsModified = true;
                entry.Property(x => x.Fqa1hUsernameAssgn).IsModified = true;
                entry.Property(x => x.Fqa1hDtAssgn).IsModified = true;

                entry.Property(x => x.Fqa1hUseridExec).IsModified = true;
                entry.Property(x => x.Fqa1hInitialExec).IsModified = true;
                entry.Property(x => x.Fqa1hUsernameExec).IsModified = true;
                entry.Property(x => x.Fqa1hDtExec).IsModified = true;

                entry.Property(x => x.Fqa1hUseridChked).IsModified = true;
                entry.Property(x => x.Fqa1hInitialChked).IsModified = true;
                entry.Property(x => x.Fqa1hUsernameChked).IsModified = true;
                entry.Property(x => x.Fqa1hDtChked).IsModified = true;


                entry.Property(x => x.Fqa1hUseridAudit).IsModified = true;
                entry.Property(x => x.Fqa1hUsernameAudit).IsModified = true;
                entry.Property(x => x.Fqa1hDtAudit).IsModified = true;
                entry.Property(x => x.Fqa1hDesignationAudit).IsModified = true;
                entry.Property(x => x.Fqa1hOfficeAudit).IsModified = true;
                entry.Property(x => x.Fqa1hSignAudit).IsModified = true;

                entry.Property(x => x.Fqa1hUseridWit).IsModified = true;
                entry.Property(x => x.Fqa1hUsernameWit).IsModified = true;
                entry.Property(x => x.Fqa1hDtWit).IsModified = true;
                entry.Property(x => x.Fqa1hDesignationWit).IsModified = true;
                entry.Property(x => x.Fqa1hOfficeWit).IsModified = true;
                entry.Property(x => x.Fqa1hSignWit).IsModified = true;

                if (updateSubmit)
                {
                    entry.Property(x => x.Fqa1hSubmitSts).IsModified = true;
                }
            }
            _context.SaveChanges();
            if (isAdd)
            {
                IDictionary<string, string> lstData = new Dictionary<string, string>();
                lstData.Add("RMU", formQa1Header.Fqa1hRmu.ToString());
                lstData.Add("CrewUnit", formQa1Header.Fqa1hCrew.ToString());
                lstData.Add("ActCode", formQa1Header.Fqa1hActCode.ToString());
                lstData.Add("Year", formQa1Header.Fqa1hDt.Value.Year.ToString());
                lstData.Add("MonthNo", formQa1Header.Fqa1hDt.Value.Month.ToString());
                lstData.Add("Day", formQa1Header.Fqa1hDt.Value.Day.ToString());
                lstData.Add(FormRefNumber.NewRunningNumber, Utility.ToString(formQa1Header.Fqa1hPkRefNo));
                formQa1Header.Fqa1hRefId = FormRefNumber.GetRefNumber(RAMMS.Common.RefNumber.FormType.FormQA1Header, lstData);
                var rmtes = new RmFormQa1Tes
                {
                    Fqa1tesPkRefNo = 0,
                    Fqa1tesFqa1hPkRefNo = formQa1Header.Fqa1hPkRefNo,
                };
                _context.RmFormQa1Tes.Add(rmtes);
                _context.SaveChanges();
            }
            return formQa1Header;
        }

        public async Task<RmFormQa1Wcq> SaveWCQ(RmFormQa1Wcq form)
        {
            if (form.Fqa1wcqPkRefNo == 0)
            {
                _context.RmFormQa1Wcq.Add(form);
                _context.Entry<RmFormQa1Wcq>(form).State = EntityState.Added;
            }
            else
            {
                _context.RmFormQa1Wcq.Attach(form);
                _context.Entry<RmFormQa1Wcq>(form).State = EntityState.Modified;
            }
            _context.SaveChanges();
            return form;
        }

        public async Task<RmFormQa1We> SaveWE(RmFormQa1We form)
        {
            if (form.Fqa1wPkRefNo == 0)
            {
                _context.RmFormQa1We.Add(form);
                _context.Entry<RmFormQa1We>(form).State = EntityState.Added;
            }
            else
            {
                _context.RmFormQa1We.Attach(form);
                _context.Entry<RmFormQa1We>(form).State = EntityState.Modified;
            }
            _context.SaveChanges();
            return form;
        }

        public async Task<RmFormQa1Lab> SaveLabour(RmFormQa1Lab form)
        {
            if (form.Fqa1lPkRefNo == 0)
            {
                _context.RmFormQa1Lab.Add(form);
                _context.Entry<RmFormQa1Lab>(form).State = EntityState.Added;
            }
            else
            {
                _context.RmFormQa1Lab.Attach(form);
                _context.Entry<RmFormQa1Lab>(form).State = EntityState.Modified;
            }
            _context.SaveChanges();
            return form;
        }

        public async Task<RmFormQa1Gc> SaveGC(RmFormQa1Gc form)
        {
            if (form.Fqa1gcPkRefNo == 0)
            {
                _context.RmFormQa1Gc.Add(form);
                _context.Entry<RmFormQa1Gc>(form).State = EntityState.Added;
            }
            else
            {
                _context.RmFormQa1Gc.Attach(form);
                _context.Entry<RmFormQa1Gc>(form).State = EntityState.Modified;
            }
            _context.SaveChanges();
            return form;
        }

        public async Task<RmFormQa1Tes> SaveTES(RmFormQa1Tes form)
        {
            if (form.Fqa1tesPkRefNo == 0)
            {
                _context.RmFormQa1Tes.Add(form);
                _context.Entry<RmFormQa1Tes>(form).State = EntityState.Added;
            }
            else
            {
                _context.RmFormQa1Tes.Attach(form);
                _context.Entry<RmFormQa1Tes>(form).State = EntityState.Modified;
            }
            _context.SaveChanges();
            return form;
        }

        public async Task<RmFormQa1Ssc> SaveSSC(RmFormQa1Ssc form)
        {
            if (form.Fqa1sscPkRefNo == 0)
            {
                _context.RmFormQa1Ssc.Add(form);
                _context.Entry<RmFormQa1Ssc>(form).State = EntityState.Added;
            }
            else
            {
                _context.RmFormQa1Ssc.Attach(form);
                _context.Entry<RmFormQa1Ssc>(form).State = EntityState.Modified;
            }
            _context.SaveChanges();
            return form;
        }


        public int DeleteFormQA1(int id)
        {
            try
            {

                var res = _context.Set<RmFormQa1Hdr>().FindAsync(id).Result;
                res.Fqa1hActiveYn = false;
                _context.Set<RmFormQa1Hdr>().Attach(res);
                _context.Entry<RmFormQa1Hdr>(res).State = EntityState.Modified;

                var lab = _context.Set<RmFormQa1Lab>().FindAsync(id).Result;
                if (lab != null)
                {
                    lab.Fqa1lActiveYn = false;
                    _context.Set<RmFormQa1Lab>().Attach(lab);
                    _context.Entry<RmFormQa1Lab>(lab).State = EntityState.Modified;
                }

                var Gc = _context.Set<RmFormQa1Gc>().FindAsync(id).Result;
                if (Gc != null)
                {
                    Gc.Fqa1gcActiveYn = false;
                    _context.Set<RmFormQa1Gc>().Attach(Gc);
                    _context.Entry<RmFormQa1Gc>(Gc).State = EntityState.Modified;
                }

                var Ssc = _context.Set<RmFormQa1Ssc>().FindAsync(id).Result;
                if (Ssc != null)
                {
                    Ssc.Fqa1sscActiveYn = false;
                    _context.Set<RmFormQa1Ssc>().Attach(Ssc);
                    _context.Entry<RmFormQa1Ssc>(Ssc).State = EntityState.Modified;
                } 
                

                var Tes = _context.Set<RmFormQa1Tes>().FindAsync(id).Result;
                if (Tes != null) 
                {
                    Tes.Fqa1tesActiveYn = false;
                    _context.Set<RmFormQa1Tes>().Attach(Tes);
                    _context.Entry<RmFormQa1Tes>(Tes).State = EntityState.Modified;
                }

                var Wcq = _context.Set<RmFormQa1Wcq>().FindAsync(id).Result;
                if (Wcq != null)
                {
                    Wcq.Fqa1wcqActiveYn = false;
                    _context.Set<RmFormQa1Wcq>().Attach(Wcq);
                    _context.Entry<RmFormQa1Wcq>(Wcq).State = EntityState.Modified;
                }

                var We = _context.Set<RmFormQa1We>().FindAsync(id).Result;
                if (We != null)
                {
                    We.Fqa1wActiveYn = false;
                    _context.Set<RmFormQa1We>().Attach(We);
                    _context.Entry<RmFormQa1We>(We).State = EntityState.Modified;
                }
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public async Task<RmFormQa1Hdr> GetFormQA1(int pkRefNo)
        {
            var result = await _context.RmFormQa1Hdr.Include(m => m.RmFormQa1EqVh)
                .Include(m => m.RmFormQa1Gc)
                .Include(m => m.RmFormQa1Gen)
                .Include(m => m.RmFormQa1Lab)
                .Include(m => m.RmFormQa1Mat)
                .Include(m => m.RmFormQa1Ssc)
                .Include(m => m.RmFormQa1Tes)
                .Include(m => m.RmFormQa1Wcq)
                .Include(m => m.RmFormQa1We)
                .FirstOrDefaultAsync(m => m.Fqa1hPkRefNo == pkRefNo);
            return result;
        }

        #region Equipment
        public async Task<int> GetFilteredEqpRecordCount(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id)
        {
            var query = (from x in _context.RmFormQa1EqVh.Where(x => x.Fqa1evFqa1hPkRefNo == id) select x);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormQa1EqVh>> GetFilteredEqpRecordList(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id)
        {
            List<RmFormQa1EqVh> result = new List<RmFormQa1EqVh>();
            var query = (from x in _context.RmFormQa1EqVh.Where(x => x.Fqa1evFqa1hPkRefNo == id) select x);

            result = await query.OrderByDescending(s => s.Fqa1evPkRefNo)
                                .Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        public async Task<RmFormQa1EqVh> GetEquipDetails(int pkRefNo)
        {
            var result = await _context.RmFormQa1EqVh.FirstOrDefaultAsync(m => m.Fqa1evPkRefNo == pkRefNo);
            return result;
        }

        public int? SaveEquipment(RmFormQa1EqVh formQa1EqVh)
        {
            try
            {
                _context.Entry<RmFormQa1EqVh>(formQa1EqVh).State = formQa1EqVh.Fqa1evPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return formQa1EqVh.Fqa1evPkRefNo;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public async Task<int?> DeleteEquipment(int id)
        {
            try
            {
                var res = await _context.Set<RmFormQa1EqVh>().FindAsync(id);
                _context.Remove(res);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }
        #endregion

        #region Material
        public async Task<int> GetFilteredMatRecordCount(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id)
        {
            var query = (from x in _context.RmFormQa1Mat.Where(x => x.Fqa1mFqa1hPkRefNo == id) select x);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormQa1Mat>> GetFilteredMatRecordList(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id)
        {
            List<RmFormQa1Mat> result = new List<RmFormQa1Mat>();
            var query = (from x in _context.RmFormQa1Mat.Where(x => x.Fqa1mFqa1hPkRefNo == id) select x);

            result = await query.OrderByDescending(s => s.Fqa1mPkRefNo)
                                .Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        public async Task<RmFormQa1Mat> GetMatDetails(int pkRefNo)
        {
            var result = await _context.RmFormQa1Mat.FirstOrDefaultAsync(m => m.Fqa1mPkRefNo == pkRefNo);
            return result;
        }

        public int? SaveMaterial(RmFormQa1Mat formQa1Mat)
        {
            try
            {
                _context.Entry<RmFormQa1Mat>(formQa1Mat).State = formQa1Mat.Fqa1mPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return formQa1Mat.Fqa1mPkRefNo;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public async Task<int?> DeleteMaterial(int id)
        {
            try
            {
                var res = await _context.Set<RmFormQa1Mat>().FindAsync(id);
                _context.Remove(res);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }
        #endregion

        #region General 
        public async Task<int> GetFilteredGenRecordCount(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id)
        {
            var query = (from x in _context.RmFormQa1Gen.Where(x => x.Fqa1genFqa1hPkRefNo == id) select x);
            return await query.CountAsync().ConfigureAwait(false);
        }

        public async Task<List<RmFormQa1Gen>> GetFilteredGenRecordList(FilteredPagingDefinition<FormQa1SearchGridDTO> filterOptions, int id)
        {
            List<RmFormQa1Gen> result = new List<RmFormQa1Gen>();
            var query = (from x in _context.RmFormQa1Gen.Where(x => x.Fqa1genFqa1hPkRefNo == id) select x);

            result = await query.OrderByDescending(s => s.Fqa1genFqa1hPkRefNo)
                                .Skip(filterOptions.StartPageNo)
                                .Take(filterOptions.RecordsPerPage)
                                .ToListAsync();
            return result;
        }

        public async Task<RmFormQa1Gen> GetGenDetails(int pkRefNo)
        {
            var result = await _context.RmFormQa1Gen.FirstOrDefaultAsync(m => m.Fqa1genPkRefNo == pkRefNo);
            return result;

        }

        public int? SaveGeneral(RmFormQa1Gen formQa1Gen)
        {
            try
            {
                _context.Entry<RmFormQa1Gen>(formQa1Gen).State = formQa1Gen.Fqa1genPkRefNo == 0 ? EntityState.Added : EntityState.Modified;
                _context.SaveChanges();

                return formQa1Gen.Fqa1genPkRefNo;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        public async  Task<int?> DeleteGeneral(int id)
        {
            try
            {
                var res = await _context.Set<RmFormQa1Gen>().FindAsync(id);
                _context.Remove(res);
                _context.SaveChanges();
                return 1;
            }
            catch (Exception ex)
            {
                return 500;
            }
        }

        #endregion

        #region Labour
        public async Task<RmFormQa1Lab> GetLabourDetails(int pkRefNo)
        {
            var result = await _context.RmFormQa1Lab.FirstOrDefaultAsync(m => m.Fqa1lPkRefNo == pkRefNo && m.Fqa1lActiveYn == true);
            return result;
        }

        #endregion

        #region Attachment

        public void SaveImage(IEnumerable<RmFormQa1Image> image)
        {

            _context.RmFormQa1Image.AddRange(image);
        }

        public async Task<List<RmFormQa1Image>> GetImages(int tesPkRefNo, int row = 0)
        {
            if (row == 0)
            {
                return await _context.RmFormQa1Image.Where(x => x.Fqa1iFqa1TesPkRefNo == tesPkRefNo && x.Fqa1iActiveYn == true).ToListAsync();
            }
            return await _context.RmFormQa1Image.Where(x => x.Fqa1iFqa1TesPkRefNo == tesPkRefNo && x.Fqa1iImageSrno == row && x.Fqa1iActiveYn == true).ToListAsync();
        }


        public void UpdateImage(RmFormQa1Image image)
        {
            _context.Set<RmFormQa1Image>().Attach(image);
            _context.Entry(image).State = EntityState.Modified;
        }


        public async Task<RmFormQa1Image> GetImageById(int imageId)
        {
            return await _context.RmFormQa1Image.Where(x => x.Fqa1iPkRefNo == imageId && x.Fqa1iActiveYn == true).FirstOrDefaultAsync();
        }
        #endregion


        public async Task<RmFormQa1Tes> GetTes(int tesPkRefNo)
        {
            return await _context.RmFormQa1Tes.Where(x => x.Fqa1tesFqa1hPkRefNo == tesPkRefNo).FirstOrDefaultAsync();
        }

        public async void UpdateTesImage(IEnumerable<RmFormQa1Image> images)
        {

            foreach (var image in images)
            {
                var tes = await GetTes((int)image.Fqa1iFqa1PkRefNo);

                switch (image.Fqa1iImageSrno)
                {
                    case 1:
                        tes.Fqa1tesCtCsA = image.Fqa1iPkRefNo;
                        _context.RmFormQa1Tes.Attach(tes);
                        break;
                    case 2:
                        tes.Fqa1tesDtCsA = image.Fqa1iPkRefNo;
                        _context.RmFormQa1Tes.Attach(tes);
                        break;
                    case 3:
                        tes.Fqa1tesMgtCsA = image.Fqa1iPkRefNo;
                        _context.RmFormQa1Tes.Attach(tes);
                        break;
                    case 4:
                        tes.Fqa1tesCbrCsA = image.Fqa1iPkRefNo;
                        _context.RmFormQa1Tes.Attach(tes);
                        break;
                    case 5:
                        tes.Fqa1tesOtCsA = image.Fqa1iPkRefNo;
                        _context.RmFormQa1Tes.Attach(tes);
                        break;
                }
                //_context.Entry<RmFormQa1Tes>(tes).State = EntityState.Modified;
                //await _context.SaveChangesAsync();
            }
        }

    }


}
