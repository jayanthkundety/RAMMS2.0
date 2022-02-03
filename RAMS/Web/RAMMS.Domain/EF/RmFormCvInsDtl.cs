using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormCvInsDtl
    {
        public int FcvidPkRefNo { get; set; }
        public int? FcvidFcvihPkRefNo { get; set; }
        public int? FcvidIimdPkRefNo { get; set; }
        public string FcvidDistress { get; set; }
        public int? FcvidSeverity { get; set; }
        public int? FcvidModBy { get; set; }
        public DateTime? FcvidModDt { get; set; }
        public int? FcvidCrBy { get; set; }
        public DateTime FcvidCrDt { get; set; }
        public bool? FcvidSubmitSts { get; set; }
        public bool? FcvidActiveYn { get; set; }
        public string FcvidInspCode { get; set; }
        public string FcvidInspCodeDesc { get; set; }
        public int? FcvidIimPkRefNo { get; set; }
        public string FcvidDistressOthers { get; set; }

        public virtual RmFormCvInsHdr FcvidFcvihPkRefNoNavigation { get; set; }
        public virtual RmInspItemMas FcvidIimPkRefNoNavigation { get; set; }
        public virtual RmInspItemMasDtl FcvidIimdPkRefNoNavigation { get; set; }
    }
}
