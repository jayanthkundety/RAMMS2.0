using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormV1WorkScheduleGridDTO
    {
        public int PkRefNo { get; set; }
        public int? Fv1hPkRefNo { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }

        public string Chainage { get; set; }
        public string SiteRef { get; set; }
        public DateTime? StartTime { get; set; }
        public string Remarks { get; set; }
       

    }
}
