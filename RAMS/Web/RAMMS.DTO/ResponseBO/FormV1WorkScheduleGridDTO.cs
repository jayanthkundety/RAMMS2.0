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
        public int? Fs1dPkRefNo { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }

        public string Chainage { get; set; }

        public string ChainageFrom { get; set; }
        public string ChainageFromDec { get; set; }
        public string ChainageTo { get; set; }
        public string ChainageToDec { get; set; }
        public string SiteRef { get; set; }
        public string StartTime { get; set; }
        public string Remarks { get; set; }
       

    }
}
