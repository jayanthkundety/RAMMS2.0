using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormF4InsHdr
    {
        public RmFormF4InsHdr()
        {
            RmFormF4InsDtl = new HashSet<RmFormF4InsDtl>();
        }

        public int FivahPkRefNo { get; set; }
        public string FivahDivCode { get; set; }
        public string FivahDist { get; set; }
        public string FivahRmuName { get; set; }
        public int? FivahRoadId { get; set; }
        public string FivahRoadCode { get; set; }
        public string FivahRoadName { get; set; }
        public decimal? FivahRoadLength { get; set; }
        public int? FivahYearOfInsp { get; set; }
        public int? FivahUserIdInspBy { get; set; }
        public string FivahUserNameInspBy { get; set; }
        public string FivahUserDesignationInspBy { get; set; }
        public DateTime? FivahDtInspBy { get; set; }
        public string FivahSignpathInspBy { get; set; }
        public string FivahFormRefId { get; set; }
        public int? FivahCrewLeaderId { get; set; }
        public string FivahCrewLeaderName { get; set; }
        public int? FivahModBy { get; set; }
        public DateTime? FivahModDt { get; set; }
        public int? FivahCrBy { get; set; }
        public DateTime? FivahCrDt { get; set; }
        public bool FivahSubmitSts { get; set; }
        public bool? FivahActiveYn { get; set; }
        public string FivahStatus { get; set; }
        public string FivahAuditLog { get; set; }

        public virtual RmRoadMaster FivahRoad { get; set; }
        public virtual ICollection<RmFormF4InsDtl> RmFormF4InsDtl { get; set; }
    }
}
