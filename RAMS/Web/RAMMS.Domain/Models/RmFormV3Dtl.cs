using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormV3Dtl
    {
        public int Fv3dPkRefNo { get; set; }
        public int? Fv3dFv3hPkRefNo { get; set; }
        public string Fv3dRoadCode { get; set; }
        public string Fv3dRoadName { get; set; }
        public int? Fv3dFrmCh { get; set; }
        public int? Fv3dFrmChDeci { get; set; }
        public int? Fv3dToCh { get; set; }
        public int? Fv3dToChDeci { get; set; }
        public int? Fv3dLength { get; set; }
        public int? Fv3dWidth { get; set; }
        public DateTime? Fv3dTimetakenFrm { get; set; }
        public DateTime? Fv3dTimeTakenTo { get; set; }
        public decimal? Fv3dTimeTakenTotal { get; set; }
        public DateTime? Fv3dTransitTimeFrm { get; set; }
        public DateTime? Fv3dTransitTimeTo { get; set; }
        public decimal? Fv3dTransitTimeTotal { get; set; }
        public int? Fv3dAdp { get; set; }
        public int? Fv3dModBy { get; set; }
        public DateTime? Fv3dModDt { get; set; }
        public int? Fv3dCrBy { get; set; }
        public DateTime? Fv3dCrDt { get; set; }
        public bool Fv3dSubmitSts { get; set; }
        public bool Fv3dActiveYn { get; set; }

        public virtual RmFormV3Hdr Fv3dFv3hPkRefNoNavigation { get; set; }
    }
}
