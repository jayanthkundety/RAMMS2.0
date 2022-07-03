using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormF1Dtl
    {
        public int Ff1dPkRefNo { get; set; }
        public int? Ff1dFf1hPkRefNo { get; set; }
        public int? Ff1dR1hPkRefNo { get; set; }
        public string Ff1dAssetId { get; set; }
        public int? Ff1dLocCh { get; set; }
        public int? Ff1dLocChDeci { get; set; }
        public string Ff1dCode { get; set; }
        public int? Ff1dTier { get; set; }
        public decimal? Ff1dTotalLength { get; set; }
        public decimal? Ff1dTopWidth { get; set; }
        public decimal? Ff1dBottomWidth { get; set; }
        public decimal? Ff1dHeight { get; set; }
        public int? Ff1dOverallCondition { get; set; }
        public string Ff1dDescription { get; set; }
        public int? Ff1dModBy { get; set; }
        public DateTime? Ff1dModDt { get; set; }
        public int? Ff1dCrBy { get; set; }
        public DateTime? Ff1dCrDt { get; set; }
        public string Ff1dStatus { get; set; }
        public string Ff1dAuditLog { get; set; }

        public virtual RmFormF1Hdr Ff1dFf1hPkRefNoNavigation { get; set; }
    }
}
