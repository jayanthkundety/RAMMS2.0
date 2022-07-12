using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormTDtlGridDTO
    {
        public int SNo { get; set; }
        public int PkRefNo { get; set; }
        public DateTime? Date { get; set; }
        public string FromTime { get; set; }
        public string ToTime { get; set; }
        public int? PC { get; set; }
        public int? HV { get; set; }
        public int? MC { get; set; }
        public string Description { get; set; }
       

    }
}
