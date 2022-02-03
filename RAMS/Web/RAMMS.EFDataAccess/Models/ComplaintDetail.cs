using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class ComplaintDetail
    {
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
        public string CompanyName { get; set; }
        public string CompanyRegNo { get; set; }
        public string PersonInCharge { get; set; }
        public string ContactNumber { get; set; }
        public string ContractorEmail { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string KpiitemCode { get; set; }
        public string KpiItemName { get; set; }
        public int? KpiGroupL3pk { get; set; }
        public int? KpiGroupL2pk { get; set; }
        public string KpiGroupL3code { get; set; }
        public string KpiGroupL3name { get; set; }
        public int? KpiGroupL1pk { get; set; }
        public string KpiGroupL2code { get; set; }
        public string KpiGroupL2name { get; set; }
        public string KpiGroupL1code { get; set; }
        public string KpiGroupL1name { get; set; }
        public DateTime ComplaintDate { get; set; }
        public double? ResponseGeoLat { get; set; }
        public double? ResponseGeoLon { get; set; }
        public string ActualDefectCause { get; set; }
        public string ActionTaken { get; set; }
        public string ReplacementItem { get; set; }
    }
}
