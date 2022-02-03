using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class Complaint
    {
        public Complaint()
        {
            ComplaintPhoto = new HashSet<ComplaintPhoto>();
        }

        public int Pk { get; set; }
        public DateTime? ResponseDateTime { get; set; }
        public string Source { get; set; }
        public int SectionPk { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public string Severity { get; set; }
        public string IncidentRemark { get; set; }
        public int ContractorPk { get; set; }
        public string AttendedBy { get; set; }
        public double? GeoLat { get; set; }
        public double? GeoLong { get; set; }
        public bool NonConformance { get; set; }
        public int? KpiItemPk { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime ComplaintDate { get; set; }
        public double? ResponseGeoLat { get; set; }
        public double? ResponseGeoLon { get; set; }
        public string ActualDefectCause { get; set; }
        public string ActionTaken { get; set; }
        public string ReplacementItem { get; set; }

        public virtual Contractor ContractorPkNavigation { get; set; }
        public virtual KpiItem KpiItemPkNavigation { get; set; }
        public virtual Section SectionPkNavigation { get; set; }
        public virtual ICollection<ComplaintPhoto> ComplaintPhoto { get; set; }
    }
}
