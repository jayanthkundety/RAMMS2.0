using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormF2GrInsHdr
    {
        public RmFormF2GrInsHdr()
        {
            RmFormF2GrInsDtl = new HashSet<RmFormF2GrInsDtl>();
        }

        public int FgrihPkRefNo { get; set; }
        public string FgrihDivCode { get; set; }
        public string FgrihDist { get; set; }
        public int? FgrihRoadId { get; set; }
        public string FgrihRoadCode { get; set; }
        public string FgrihRoadName { get; set; }
        public decimal? FgrihRoadLength { get; set; }
        public int? FgrihYearOfInsp { get; set; }
        public DateTime? FgrihDtOfInsp { get; set; }
        public int? FgrihUserIdInspBy { get; set; }
        public string FgrihUserNameInspBy { get; set; }
        public string FgrihUserDesignationInspBy { get; set; }
        public DateTime? FgrihDtInspBy { get; set; }
        public string FgrihSignpathInspBy { get; set; }
        public string FgrihFormRefId { get; set; }
        public int? FgrihCrewLeaderId { get; set; }
        public string FgrihCrewLeaderName { get; set; }
        public int? FgrihModBy { get; set; }
        public DateTime? FgrihModDt { get; set; }
        public int? FgrihCrBy { get; set; }
        public DateTime? FgrihCrDt { get; set; }
        public bool FgrihSubmitSts { get; set; }
        public bool? FgrihActiveYn { get; set; }
        public string FgrihStatus { get; set; }
        public string FgrihAuditLog { get; set; }

        public virtual RmRoadMaster FgrihRoad { get; set; }
        public virtual ICollection<RmFormF2GrInsDtl> RmFormF2GrInsDtl { get; set; }
    }
}
