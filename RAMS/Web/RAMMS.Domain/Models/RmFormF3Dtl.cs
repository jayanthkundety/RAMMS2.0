using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormF3Dtl
    {
        public int Ff3dPkRefNo { get; set; }
        public int Ff3G1hPkRefNo { get; set; }
        public int? Ff3dFf3hPkRefNo { get; set; }
        public string Ff3dAssetId { get; set; }
        public int? Ff3dLocCh { get; set; }
        public int? Ff3dLocChDeci { get; set; }
        public string Ff3dCode { get; set; }
        public string Ff3dBound { get; set; }
        public int? Ff3dConditionI { get; set; }
        public int? Ff3dConditionIi { get; set; }
        public int? Ff3dConditionIii { get; set; }
        public string Ff3dSide { get; set; }
        public decimal? Ff3dWidth { get; set; }
        public decimal? Ff3dHeight { get; set; }
        public string Ff3dDescription { get; set; }
        public int? Ff3dModBy { get; set; }
        public DateTime? Ff3dModDt { get; set; }
        public int? Ff3dCrBy { get; set; }
        public DateTime? Ff3dCrDt { get; set; }
        public string Ff3dStatus { get; set; }
        public string Ff3dAuditLog { get; set; }

        public virtual RmFormF3Hdr Ff3dFf3hPkRefNoNavigation { get; set; }
    }
}
