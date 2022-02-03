using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class AnnualBridgeInspection
    {
        public AnnualBridgeInspection()
        {
            AnnualBridgeInspectionAttachment = new HashSet<AnnualBridgeInspectionAttachment>();
        }

        public int Pk { get; set; }
        public string BridgeType { get; set; }
        public int? InspectionProgramYear { get; set; }
        public DateTime? ProgramSubmissionDate { get; set; }
        public DateTime? InspectionProgramCompletionDate { get; set; }
        public string Remarks { get; set; }
        public string ProgramPreparedBy { get; set; }
        public string ProgramApprovedBy { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<AnnualBridgeInspectionAttachment> AnnualBridgeInspectionAttachment { get; set; }
    }
}
