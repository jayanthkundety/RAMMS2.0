using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormF5InsHdr
    {
        public RmFormF5InsHdr()
        {
            RmFormF5InsDtl = new HashSet<RmFormF5InsDtl>();
        }

        public int FvahPkRefNo { get; set; }
        public string FvahDivCode { get; set; }
        public string FvahDist { get; set; }
        public string FvahRmuName { get; set; }
        public int? FvahRoadId { get; set; }
        public string FvahRoadCode { get; set; }
        public string FvahRoadName { get; set; }
        public decimal? FvahRoadLength { get; set; }
        public int? FvahYearOfInsp { get; set; }
        public int? FvahUserIdInspBy { get; set; }
        public string FvahUserNameInspBy { get; set; }
        public string FvahUserDesignationInspBy { get; set; }
        public DateTime? FvahDtInspBy { get; set; }
        public string FvahSignpathInspBy { get; set; }
        public string FvahFormRefId { get; set; }
        public int? FvahCrewLeaderId { get; set; }
        public string FvahCrewLeaderName { get; set; }
        public int? FvahModBy { get; set; }
        public DateTime? FvahModDt { get; set; }
        public int? FvahCrBy { get; set; }
        public DateTime? FvahCrDt { get; set; }
        public bool FvahSubmitSts { get; set; }
        public bool? FvahActiveYn { get; set; }
        public string FvahStatus { get; set; }
        public string FvahAuditLog { get; set; }

        public virtual RmRoadMaster FvahRoad { get; set; }
        public virtual ICollection<RmFormF5InsDtl> RmFormF5InsDtl { get; set; }
    }
}
