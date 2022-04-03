using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormV2Lab
    {
        public int Fv2lPkRefNo { get; set; }
        public int? Fv2lFv2hPkRefNo { get; set; }
        public string Fv2lLabRefCode { get; set; }
        public string Fv2lDesc { get; set; }
        public string Fv2lRemark { get; set; }
        public string Fv2lModBy { get; set; }
        public DateTime? Fv2lModDt { get; set; }
        public string Fv2lCrBy { get; set; }
        public DateTime? Fv2lCrDt { get; set; }
        public bool Fv2lSubmitSts { get; set; }
        public bool Fv2lActiveYn { get; set; }

        public virtual RmFormV2Hdr Fv2lFv2hPkRefNoNavigation { get; set; }
    }
}
