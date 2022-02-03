using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.MobileApps
{
    public class DDLookUpDTO
    {
        public int No { get; set; }

        public string Type { get; set; }

        public string TypeCode { get; set; }

        public string TypeDesc { get; set; }

        public string TypeValue { get; set; }

        public string TypeRemarks { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreateDate { get; set; }

        public bool? Active { get; set; }
    }
}
