using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormF4InsDtl
    {
        public int FivadPkRefNo { get; set; }
        public int? FivadFivahPkRefNo { get; set; }
        public int? FivadFcvihPkRefNo { get; set; }
        public int? FivadLocChKm { get; set; }
        public string FivadLocChM { get; set; }
        public string FivadStrucCode { get; set; }
        public bool? FivadIntelStruc { get; set; }
        public bool? FivadOutletStruc { get; set; }
        public double? FivadLength { get; set; }
        public int? FivadBarrelNo { get; set; }
        public double? FivadWidth { get; set; }
        public double? FivadHeight { get; set; }
        public int? FivadCondition { get; set; }
        public string FivadRemarks { get; set; }
        public int? FivadModBy { get; set; }
        public DateTime? FivadModDt { get; set; }
        public int? FivadCrBy { get; set; }
        public DateTime? FivadCrDt { get; set; }
        public bool FivadSubmitSts { get; set; }
        public bool? FivadActiveYn { get; set; }

        public virtual RmFormCvInsHdr FivadFcvihPkRefNoNavigation { get; set; }
        public virtual RmFormF4InsHdr FivadFivahPkRefNoNavigation { get; set; }
    }
}
