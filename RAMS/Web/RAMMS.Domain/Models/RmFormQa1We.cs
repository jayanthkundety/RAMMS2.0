using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormQa1We
    {
        public int Fqa1wPkRefNo { get; set; }
        public int? Fqa1wFqa1hPkRefNo { get; set; }
        public decimal? Fqa1wAcwcThinkness { get; set; }
        public int? Fqa1wAcwcThinknessUnit { get; set; }
        public decimal? Fqa1wAcwcTemperature { get; set; }
        public int? Fqa1wAcwcTemperatureUnit { get; set; }
        public int? Fqa1wAcwcPasses { get; set; }
        public string Fqa1wAcwcRemark { get; set; }
        public decimal? Fqa1wTcDRate { get; set; }
        public int? Fqa1wTcDRateUnit { get; set; }
        public string Fqa1wTcType { get; set; }
        public string Fqa1wTcEvenlySpread { get; set; }
        public string Fqa1wTcRemark { get; set; }
        public decimal? Fqa1wAcbcThinkness { get; set; }
        public int? Fqa1wAcbcThinknessUnit { get; set; }
        public decimal? Fqa1wAcbcTemperature { get; set; }
        public int? Fqa1wAcbcTemperatureUnit { get; set; }
        public int? Fqa1wAcbcPasses { get; set; }
        public string Fqa1wAcbcRemark { get; set; }
        public decimal? Fqa1wPcDRate { get; set; }
        public int? Fqa1wPcDRateUnit { get; set; }
        public string Fqa1wPcType { get; set; }
        public string Fqa1wPcEvenlySpread { get; set; }
        public string Fqa1wPcRemark { get; set; }
        public decimal? Fqa1wRbThinkness { get; set; }
        public int? Fqa1wRbThinknessUnit { get; set; }
        public int? Fqa1wRbLayers { get; set; }
        public int? Fqa1wRbPasses { get; set; }
        public string Fqa1wRbRemark { get; set; }
        public decimal? Fqa1wSbThinkness { get; set; }
        public int? Fqa1wSbThinknessUnit { get; set; }
        public int? Fqa1wSbLayers { get; set; }
        public int? Fqa1wSbPasses { get; set; }
        public string Fqa1wSbRemark { get; set; }
        public decimal? Fqa1wSgThinkness { get; set; }
        public int? Fqa1wSgThinknessUnit { get; set; }
        public int? Fqa1wSgLayers { get; set; }
        public int? Fqa1wSgPasses { get; set; }
        public string Fqa1wSgRemark { get; set; }
        public decimal? Fqa1wSsdSb { get; set; }
        public int? Fqa1wSsdSbUnit { get; set; }
        public decimal? Fqa1wSsdPp { get; set; }
        public int? Fqa1wSsdPpUnit { get; set; }
        public string Fqa1wSsdRemark { get; set; }
        public decimal? Fqa1wSsdCh { get; set; }
        public decimal? Fqa1wSsdChDeci { get; set; }
        public decimal? Fqa1wSsdRhsL { get; set; }
        public decimal? Fqa1wSsdRhsW { get; set; }
        public decimal? Fqa1wSsdLhsL { get; set; }
        public decimal? Fqa1wSsdLhsW { get; set; }
        public bool Fqa1wActiveYn { get; set; }
        public int? Fqa1wModBy { get; set; }
        public DateTime? Fqa1wModDt { get; set; }
        public int? Fqa1wCrBy { get; set; }
        public DateTime? Fqa1wCrDt { get; set; }

        public virtual RmFormQa1Hdr Fqa1wFqa1hPkRefNoNavigation { get; set; }
    }
}
