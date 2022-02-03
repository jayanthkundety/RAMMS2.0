using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpPermitToWork
    {
        public WpPermitToWork()
        {
            WpPermitToWorkPtw = new HashSet<WpPermitToWorkPtw>();
        }

        public int Pk { get; set; }
        public string PermitNo { get; set; }
        public string ReqName { get; set; }
        public string ReqIdno { get; set; }
        public string ReqCompany { get; set; }
        public string ReqFacility { get; set; }
        public string ReqWorksite { get; set; }
        public string ReqLocation { get; set; }
        public string ReqWork { get; set; }
        public string ReqScope { get; set; }
        public string ReqScopeOther { get; set; }
        public string HazActivities { get; set; }
        public string HazActivitiesOther { get; set; }
        public string HazPrecautions { get; set; }
        public string DocCert { get; set; }
        public string DocCertOther { get; set; }
        public string DocSupport { get; set; }
        public string DocSupportOther { get; set; }
        public string Equipment { get; set; }
        public string EquipmentOther { get; set; }
        public DateTime? PmtGenDate { get; set; }
        public DateTime? PmtGenTimeFrom { get; set; }
        public DateTime? PmtGenTimeTo { get; set; }
        public string PmtGenApplicant { get; set; }
        public string PmtGenAuthority { get; set; }
        public string PmtGenApprove { get; set; }
        public string PmtGenReason { get; set; }
        public string PmtGenComments { get; set; }
        public string PmtStopName { get; set; }
        public string PmtStopPosition { get; set; }
        public string PmtStopReason { get; set; }
        public string PmtRsmName { get; set; }
        public string PmtRsmPosition { get; set; }
        public string PmtRsmRemarks { get; set; }

        public virtual WorkProgress PkNavigation { get; set; }
        public virtual ICollection<WpPermitToWorkPtw> WpPermitToWorkPtw { get; set; }
    }
}
