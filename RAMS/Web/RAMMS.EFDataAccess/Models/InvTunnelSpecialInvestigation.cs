using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvTunnelSpecialInvestigation
    {
        public int Pk { get; set; }
        public int InvMasterPk { get; set; }
        public string ReferenceNumber { get; set; }
        public string Title { get; set; }
        public DateTime? InvestigationDate { get; set; }
        public string PreparedBy { get; set; }
        public string FillingLocation { get; set; }
        public string FillingReference { get; set; }
        public string ReportSummary { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public string InvestigationType { get; set; }
    }
}
