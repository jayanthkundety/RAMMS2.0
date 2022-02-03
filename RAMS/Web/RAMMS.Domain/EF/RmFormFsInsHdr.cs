using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormFsInsHdr
    {
        public RmFormFsInsHdr()
        {
            RmFormFsInsDtl = new HashSet<RmFormFsInsDtl>();
        }

        public int FshPkRefNo { get; set; }
        public string FshDivCode { get; set; }
        public string FshDist { get; set; }
        public string FshRmuName { get; set; }
        public int? FshRoadId { get; set; }
        public string FshRoadCode { get; set; }
        public string FshRoadName { get; set; }
        public decimal? FshRoadLength { get; set; }
        public int? FshYearOfInsp { get; set; }
        public int? FshUserIdInspBy { get; set; }
        public string FshUserNameInspBy { get; set; }
        public string FshUserDesignationInspY { get; set; }
        public DateTime? FshDtInspBy { get; set; }
        public string FshSignpathInspBy { get; set; }
        public string FshFormRefId { get; set; }
        public int? FshCrewLeaderId { get; set; }
        public string FshCrewLeaderName { get; set; }
        public int? FshUserIdSmzdBy { get; set; }
        public string FshUserNameSmzdBy { get; set; }
        public string FshUserDesignationSmzdY { get; set; }
        public DateTime? FshDtSmzdBy { get; set; }
        public string FshSignpathSmzdBy { get; set; }
        public int? FshUserIdChckdBy { get; set; }
        public string FshUserNameChckdBy { get; set; }
        public string FshUserDesignationChckdBy { get; set; }
        public DateTime? FshDtChckdBy { get; set; }
        public string FshSignpathChckdBy { get; set; }
        public int? FshModBy { get; set; }
        public DateTime? FshModDt { get; set; }
        public int? FshCrBy { get; set; }
        public DateTime? FshCrDt { get; set; }
        public bool FshSubmitSts { get; set; }
        public bool? FshActiveYn { get; set; }
        public string FshStatus { get; set; }
        public string FshAuditLog { get; set; }

        public virtual RmRoadMaster FshRoad { get; set; }
        public virtual ICollection<RmFormFsInsDtl> RmFormFsInsDtl { get; set; }
    }
}
