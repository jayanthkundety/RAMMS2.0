using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class SpecialInspection
    {
        public SpecialInspection()
        {
            SpecialInspectionAttachment = new HashSet<SpecialInspectionAttachment>();
        }

        public int Pk { get; set; }
        public int? InvMasterPk { get; set; }
        public DateTime? InspectionDate { get; set; }
        public string InspectionBy { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual InvMaster InvMasterPkNavigation { get; set; }
        public virtual ICollection<SpecialInspectionAttachment> SpecialInspectionAttachment { get; set; }
    }
}
