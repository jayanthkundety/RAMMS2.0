using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO
{
    public class FormMSearchGridDTO
    {
        public string HeaderNo { get; set; }
        public string Reference_No { get; set; }
        public string RMU { get; set; }

        public string Section { get; set; }
        public string RoadCode { get; set; }

        public string RoadName { get; set; }

        public string Crew { get; set; }

        public string ActivityCode { get; set; }

        public string ByFromdate { get; set; }

        public string ByTodate { get; set; }

        public string SmartInputValue { get; set; }
        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? Page_No { get; set; }
        public int? pageSize { get; set; }
    }
}
