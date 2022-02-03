using System;
using System.Collections.Generic;

namespace RAMMS.DTO.Report
{
    public class FORMS1Rpt
    {
        public FORMS1HeaderRpt Header { get; set; }
        public IEnumerable<FORMS1DetailRpt> Details { get; set; }
    }

    public class FORMS1HeaderRpt
    {
        public string RMU { get; set; }
        public DateTime? Date { get; set; }
        public int WeekNo { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public string PlannedBy { get; set; }
        public string VettedBy { get; set; }
        public string AgreedBy { get; set; }
        public string Remarks { get; set; }
    }

    public class FORMS1DetailRpt
    {
        public string ActivityCode { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public int? ChainageFromKm { get; set; }
        public string ChainageFromM { get; set; }
        public int? ChainageToKm { get; set; }
        public string ChainageToM { get; set; }
        public string SiteRef { get; set; }
        public string Priority { get; set; }
        public string Qty { get; set; }
        public string CDR { get; set; }
        public string CrewSupervisor { get; set; }
        public bool IsMonday { get; set; }
        public bool IsTuesday { get; set; }
        public bool IsWednesday { get; set; }
        public bool IsThursday { get; set; }
        public bool IsFriday { get; set; }
        public bool IsSaturday { get; set; }
        public bool IsSunday { get; set; }
        public int? IsRA { get; set; }
        public int? IsMT { get; set; }
        public int? IsQA1 { get; set; }
        public int? IsQA2 { get; set; }
        public int? IsSA { get; set; }
        public int? N1 { get; set; }
        public int? N2 { get; set; }
        public string Remarks { get; set; }
        public IEnumerable<Planned> Planned { get; set; }
        public IEnumerable<Planned> Scheduled { get; set; }
        public int Id { get; set; }
    }

    public class Planned
    {
        public int DayOfTheWeek { get; set; }
        public int Value { get; set; }
    }
}
