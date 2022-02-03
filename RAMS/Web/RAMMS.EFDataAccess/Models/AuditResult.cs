using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class AuditResult
    {
        public AuditResult()
        {
            AuditResultPhoto = new HashSet<AuditResultPhoto>();
        }

        public int Pk { get; set; }
        public int AuditSessionPk { get; set; }
        public int KpiItemPk { get; set; }
        public string Condition { get; set; }
        public bool Unconform { get; set; }
        public bool Unreported { get; set; }
        public string Remarks { get; set; }
        public string MobileLastModifiedBy { get; set; }
        public DateTime MobileLastModifiedDate { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedDate { get; set; }
        public bool Approved { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }

        public virtual AuditSession AuditSessionPkNavigation { get; set; }
        public virtual KpiItem KpiItemPkNavigation { get; set; }
        public virtual ICollection<AuditResultPhoto> AuditResultPhoto { get; set; }
    }
}
