using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class SubmissionRequestMonitoringDetail
    {
        public int Pk { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public int AssignedToUserPk { get; set; }
        public DateTime ExpectedDate { get; set; }
        public DateTime? SubmittedDate { get; set; }
        public string SubmissionFor { get; set; }
        public string Status { get; set; }
        public int CreatedByUserPk { get; set; }
        public string Remark { get; set; }
        public string SubmissionName { get; set; }
        public int? CarbonCopyUserPk { get; set; }
        public string CreatedByUsername { get; set; }
        public string AssignedToUsername { get; set; }
        public string CreatedByName { get; set; }
        public string AssignedToName { get; set; }
        public string CreatedByEmail { get; set; }
        public string AssignedToEmail { get; set; }
        public string CarbonCopyEmail { get; set; }
        public string CarbonCopyUsername { get; set; }
        public string CarbonCopyName { get; set; }
    }
}
