using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PrincipalBridgeInspection
    {
        public PrincipalBridgeInspection()
        {
            PrincipalBridgeInspectionAttachment = new HashSet<PrincipalBridgeInspectionAttachment>();
        }

        public int Pk { get; set; }
        public int? InvMasterPk { get; set; }
        public DateTime? InspectionScheduleDate { get; set; }
        public DateTime? InspectionActualDate { get; set; }
        public string InspectionBy { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual InvMaster InvMasterPkNavigation { get; set; }
        public virtual ICollection<PrincipalBridgeInspectionAttachment> PrincipalBridgeInspectionAttachment { get; set; }
    }
}
