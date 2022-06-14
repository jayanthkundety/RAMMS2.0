using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormF3Hdr
    {
        public RmFormF3Hdr()
        {
            RmFormF3Dtl = new HashSet<RmFormF3Dtl>();
        }

        public int Ff3hPkRefNo { get; set; }
        public int? Ff3hG1hPkRefNo { get; set; }
        public string Ff3hPkRefId { get; set; }
        public string Ff3hDivCode { get; set; }
        public string Ff3hDist { get; set; }
        public string Ff3hRmuCode { get; set; }
        public string Ff3hRdCode { get; set; }
        public string Ff3hSecCode { get; set; }
        public string Ff3hSecName { get; set; }
        public string Ff3hRdName { get; set; }
        public string Ff3hCrewSup { get; set; }
        public string Ff3hCrewName { get; set; }
        public string Ff3hAssetId { get; set; }
        public int? Ff3hInspectedYear { get; set; }
        public DateTime? Ff3hInspectedDate { get; set; }
        public int? Ff3hInspectedBy { get; set; }
        public string Ff3hInspectedName { get; set; }
        public bool? Ff3hInspectedBySign { get; set; }
        public decimal? Ff3hRoadLength { get; set; }
        public int? Ff3hConditionI { get; set; }
        public int? Ff3hConditionIi { get; set; }
        public int? Ff3hConditionIii { get; set; }
        public int? Ff3hModBy { get; set; }
        public DateTime? Ff3hModDt { get; set; }
        public int? Ff3hCrBy { get; set; }
        public DateTime? Ff3hCrDt { get; set; }
        public bool Ff3hSubmitSts { get; set; }
        public bool Ff3hActiveYn { get; set; }
        public string Ff3hStatus { get; set; }
        public string Ff3hAuditLog { get; set; }

        public virtual ICollection<RmFormF3Dtl> RmFormF3Dtl { get; set; }
    }
}
