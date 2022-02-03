using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormFcInsHdr
    {
        public RmFormFcInsHdr()
        {
            RmFormFcInsDtl = new HashSet<RmFormFcInsDtl>();
        }

        public int FcihPkRefNo { get; set; }
        public string FcihDivCode { get; set; }
        public string FcihDist { get; set; }
        public string FcihRmuName { get; set; }
        public int? FcihRoadId { get; set; }
        public string FcihRoadCode { get; set; }
        public string FcihRoadName { get; set; }
        public decimal? FcihRoadLength { get; set; }
        public int? FcihYearOfInsp { get; set; }
        public int? FcihUserIdInspBy { get; set; }
        public string FcihUserNameInspBy { get; set; }
        public string FcihUserDesignationInspBy { get; set; }
        public DateTime? FcihDtInspBy { get; set; }
        public string FcihSignpathInspBy { get; set; }
        public string FcihFormRefId { get; set; }
        public int? FcihCrewLeaderId { get; set; }
        public string FcihCrewLeaderName { get; set; }
        public string FcihRemarks { get; set; }
        public int? FcihModBy { get; set; }
        public DateTime? FcihModDt { get; set; }
        public int? FcihCrBy { get; set; }
        public DateTime? FcihCrDt { get; set; }
        public bool FcihSubmitSts { get; set; }
        public bool? FcihActiveYn { get; set; }
        public decimal? FcihFrmCh { get; set; }
        public decimal? FcihToCh { get; set; }
        public string FcihAssetTypes { get; set; }
        public string FcihStatus { get; set; }
        public string FcihAuditLog { get; set; }

        public virtual RmRoadMaster FcihRoad { get; set; }
        public virtual ICollection<RmFormFcInsDtl> RmFormFcInsDtl { get; set; }
    }
}
