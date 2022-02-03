using System;
namespace RAMMS.MobileApps
{
    public class FormXSearchGridDTO
    {
        public int? No { get; set; }
        public string SmartInputValue { get; set; }
        public int? ActMainCode { get; set; }
        public string ActMainName { get; set; }
        public string Rmu { get; set; }
        public string RoadCode { get; set; }
        public string Type { get; set; }

        public string ActSubCode { get; set; }

        public DateTime? WorkScheduleDt { get; set; }
        public DateTime? WorkCompltDt { get; set; }

        public DateTime? CaseClosedDt { get; set; }
        public string Section { get; set; }
    }
}
