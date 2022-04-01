using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO
{
    public class FormV1SearchGridDTO
    {
        public string Reference_No { get; set; }
        public string RMU { get; set; }
        public string Division { get; set; }
        public string Activity_Code { get; set; }
        public string Section_Code { get; set; }
        public string Crew_Supervisor { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string SmartInputValue { get; set; }
        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? Page_No { get; set; }
        public int? pageSize { get; set; }
    }
}
