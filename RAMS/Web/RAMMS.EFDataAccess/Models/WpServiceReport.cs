using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpServiceReport
    {
        public WpServiceReport()
        {
            WpServiceReportAction = new HashSet<WpServiceReportAction>();
            WpServiceReportPart = new HashSet<WpServiceReportPart>();
            WpServiceReportPersonel = new HashSet<WpServiceReportPersonel>();
        }

        public int Pk { get; set; }
        public string ServiceLevel { get; set; }
        public string RefNo { get; set; }
        public string Issue { get; set; }
        public string Customer { get; set; }
        public string Address { get; set; }
        public string PlusItemsNo { get; set; }
        public string Status { get; set; }
        public string EquipmentNo { get; set; }
        public string NotifiedBy { get; set; }
        public string ComplaintDesc { get; set; }
        public DateTime? AttendedDt { get; set; }
        public DateTime? CompletedDt { get; set; }
        public string RequestPriority { get; set; }
        public string ReportedByName { get; set; }
        public string ReportedByPosition { get; set; }
        public string ReportedByCompany { get; set; }
        public string ReportedBySignature { get; set; }
        public string VerifiedByName { get; set; }
        public string VerifiedByPosition { get; set; }
        public string VerifiedByCompany { get; set; }
        public string VerifiedBySignature { get; set; }
        public string CertifiedBySupervisorName { get; set; }
        public string CertifiedBySupervisorPosition { get; set; }
        public string CertifiedBySupervisorCompany { get; set; }
        public string CertifiedBySupervisorSignature { get; set; }
        public string CertifiedByTechnicalName { get; set; }
        public string CertifiedByTechnicalPosition { get; set; }
        public string CertifiedByTechnicalCompany { get; set; }
        public string CertifiedByTechnicalSignature { get; set; }

        public virtual WorkProgress PkNavigation { get; set; }
        public virtual ICollection<WpServiceReportAction> WpServiceReportAction { get; set; }
        public virtual ICollection<WpServiceReportPart> WpServiceReportPart { get; set; }
        public virtual ICollection<WpServiceReportPersonel> WpServiceReportPersonel { get; set; }
    }
}
