using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO
{
    public class FormN2SearchGridDTO
    {
        public string Reference_No { get; set; }
        public string RMU { get; set; }
      
        public string Road_Code { get; set; }

        public string RoadName { get; set; }

        public int? IssueMonth { get; set; }

        public DateTime? IssueFrom { get; set; }

        public DateTime? IssueTo { get; set; }
        public string Owner { get; set; }
        public string Verified_By { get; set; }
        public string Section { get; set; }
        public int? ChinageFromKm { get; set; }
        public int? ChinageToKm { get; set; }
        public int? ChinageFromM { get; set; }
        public int? ChinageToM { get; set; }

        public string SmartInputValue { get; set; }
        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? Page_No { get; set; }
        public int? pageSize { get; set; }
    }
}
