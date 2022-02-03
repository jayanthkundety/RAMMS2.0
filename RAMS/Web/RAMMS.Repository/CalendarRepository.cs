using System;
using RAMMS.Domain.Models;
using RAMS.Repository;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using RAMMS.DTO.ResponseBO;

namespace RAMMS.Repository
{
    public class CalendarRepository: RepositoryBase<RmWeekLookup>
    {
        public CalendarRepository(RAMMSContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public List<SelectListItem> GetYears()
        {
            try
            {
                int?[] lst = _context.RmWeekLookup.Select(s => s.ClkYear).Distinct().OrderBy(s=> s.Value).ToArray();
                return lst.Select(s => new SelectListItem
                {
                    Text = s.Value.ToString(),
                    Value = s.Value.ToString()
                }).ToList();
            }
            catch(Exception ex)
            {
                return new List<SelectListItem>();
            }
        }

        public List<SelectListItem> GetQuarter(int year)
        {
            try
            {
                int?[] lst = _context.RmWeekLookup
                    .Where(r => r.ClkYear == year)
                    .Select(s => s.ClkQuarter).Distinct().ToArray();
                return lst.Select(s => new SelectListItem
                {
                    Text = "Q"+s.Value.ToString(),
                    Value = s.Value.ToString()
                }).ToList();
            }
            catch(Exception ex)
            {
                return new List<SelectListItem>();
            }
        }

        public List<SelectListItem> GetMonth(int year,int quarter)
        {
            
            try
            {
                int?[] lst = _context.RmWeekLookup
                                .Where(r => r.ClkYear == year && r.ClkQuarter == quarter)
                                .Select(s => s.ClkMonth).Distinct().ToArray();
                return lst.Select(s => new SelectListItem
                {
                    Text = s.Value.ToString(),
                    Value = s.Value.ToString()
                }).ToList();
            }
            catch (Exception ex)
            {
                return new List<SelectListItem>();
            }
        }

        public List<WeekS2ViewDto> GetWeek(int year, int quarter)
        {
            int?[] lst = _context.RmWeekLookup
                .Where(r => r.ClkYear == year && r.ClkQuarter == quarter)
                .Select(s => s.ClkMonth).Distinct().ToArray();

            List<WeekS2ViewDto> result = new List<WeekS2ViewDto>();

            foreach(var l in lst)
            {
                if (l.HasValue)
                {
                    result.Add(new WeekS2ViewDto
                    {
                        Name = GetMonthName(l.Value),
                        Week = _context.RmWeekLookup
                        .Where(r => r.ClkYear == year && r.ClkQuarter == quarter && r.ClkMonth == l.Value).AsEnumerable()
                        .OrderBy(s=> s.ClkMonth).Select(s=> new DropDown {
                            Text = s.ClkWeekNo.Value.ToString(),
                            Value = s.ClkPkRefNo.ToString()
                        }).ToList()
                    });
                }
            }
            return result;
        }

        public int GetId(int year,int quarter,int month,int week)
        {
          var result =  _context.RmWeekLookup
                .FirstOrDefault(r => r.ClkYear == year && r.ClkQuarter == quarter
                && r.ClkMonth == month
                && r.ClkWeekNo == week);
            if (result != null)
                return result.ClkPkRefNo;
            else
                return 0;
        }

        private string GetMonthName(int id)
        {
            switch (id)
            {
                case 1: return "January";
                case 2: return "Febraury";
                case 3: return "March";
                case 4: return "April";
                case 5: return "May";
                case 6: return "June";
                case 7: return "July";
                case 8: return "August";
                case 9: return "September";
                case 10: return "October";
                case 11: return "November";
                case 12: return "December";
                default: return "";
            }
        }
    }

   
}

