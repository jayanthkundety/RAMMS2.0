using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormV2Eqp
    {
        public int Fv2ePkRefNo { get; set; }
        public int? Fv2eFv2hPkRefNo { get; set; }
        public string Fv2eEqpRefCode { get; set; }
        public string Fv2eDesc { get; set; }
        public string Fv2eCapacity { get; set; }
        public int? Fv2eCond { get; set; }
        public string Fv2eModel { get; set; }
        public int? Fv2eModBy { get; set; }
        public DateTime? Fv2eModDt { get; set; }
        public int? Fv2eCrBy { get; set; }
        public DateTime? Fv2eCrDt { get; set; }
        public bool Fv2eSubmitSts { get; set; }
        public bool Fv2eActiveYn { get; set; }

        public virtual RmFormV2Hdr Fv2eFv2hPkRefNoNavigation { get; set; }
    }
}
