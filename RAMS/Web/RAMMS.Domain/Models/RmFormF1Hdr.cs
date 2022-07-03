using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormF1Hdr
    {
        public RmFormF1Hdr()
        {
            RmFormF1Dtl = new HashSet<RmFormF1Dtl>();
        }

        public int Ff1hPkRefNo { get; set; }
        public string Ff1hPkRefId { get; set; }
        public string Ff1hDivCode { get; set; }
        public string Ff1hAssetId { get; set; }
        public string Ff1hDist { get; set; }
        public string Ff1hRmuCode { get; set; }
        public string Ff1hRdCode { get; set; }
        public string Ff1hRdName { get; set; }
        public string Ff1hSecCode { get; set; }
        public string Ff1hSecName { get; set; }
        public string Ff1hCrewSup { get; set; }
        public string Ff1hCrewName { get; set; }
        public int? Ff1hInspectedYear { get; set; }
        public DateTime? Ff1hInspectedDate { get; set; }
        public int? Ff1hInspectedBy { get; set; }
        public string Ff1hInspectedName { get; set; }
        public string Ff1hInspectedDesg { get; set; }
        public bool? Ff1hInspectedBySign { get; set; }
        public decimal? Ff1hRoadLength { get; set; }
        public int? Ff1hConditionI { get; set; }
        public int? Ff1hConditionIi { get; set; }
        public int? Ff1hConditionIii { get; set; }
        public int? Ff1hModBy { get; set; }
        public DateTime? Ff1hModDt { get; set; }
        public int? Ff1hCrBy { get; set; }
        public DateTime? Ff1hCrDt { get; set; }
        public bool Ff1hSubmitSts { get; set; }
        public bool Ff1hActiveYn { get; set; }
        public string Ff1hStatus { get; set; }
        public string Ff1hAuditLog { get; set; }

        public virtual ICollection<RmFormF1Dtl> RmFormF1Dtl { get; set; }
    }
}
