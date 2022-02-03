using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.RequestBO
{
    public class FormASearchDTO
    {
        public string Sec_Code { get; set; }
        public string Rd_Code { get; set; }
        public string RMU_Code { get; set; }
        public string Asset_Type { get; set; }
        public string FRM_CH { get; set; }
        public string To_CH { get; set; }
        public string Month { get; set; }
        public string SearchInput { get; set; }
    }
}
