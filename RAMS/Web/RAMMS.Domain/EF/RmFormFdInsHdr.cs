using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormFdInsHdr
    {
        public RmFormFdInsHdr()
        {
            RmFormFdInsDtl = new HashSet<RmFormFdInsDtl>();
        }

        public int FdihPkRefNo { get; set; }
        public string FdihDivCode { get; set; }
        public string FdihDist { get; set; }
        public string FdihRmuName { get; set; }
        public int? FdihRoadId { get; set; }
        public string FdihRoadCode { get; set; }
        public string FdihRoadName { get; set; }
        public decimal? FdihRoadLength { get; set; }
        public int? FdihYearOfInsp { get; set; }
        public int? FdihUserIdInspBy { get; set; }
        public string FdihUserNameInspBy { get; set; }
        public string FdihUserDesignationInspBy { get; set; }
        public DateTime? FdihDtInsBy { get; set; }
        public string FdihSignpathInspBy { get; set; }
        public string FdihFormRefId { get; set; }
        public int? FdihCrewLeaderId { get; set; }
        public string FdihCrewLeaderName { get; set; }
        public string FdihRemarks { get; set; }
        public int? FdihModBy { get; set; }
        public DateTime? FdihModDt { get; set; }
        public int? FdihCrBy { get; set; }
        public DateTime? FdihCrDt { get; set; }
        public bool FdihSubmitSts { get; set; }
        public bool? FdihActiveYn { get; set; }
        public string FdihSecName { get; set; }
        public string FdihAssetTypes { get; set; }
        public decimal? FdihFrmCh { get; set; }
        public decimal? FdihToCh { get; set; }
        public string FdihStatus { get; set; }
        public string FdihAuditLog { get; set; }

        public virtual RmRoadMaster FdihRoad { get; set; }
        public virtual ICollection<RmFormFdInsDtl> RmFormFdInsDtl { get; set; }
    }
}
