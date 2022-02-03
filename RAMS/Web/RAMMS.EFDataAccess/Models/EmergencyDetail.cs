using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class EmergencyDetail
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegNo { get; set; }
        public string PersonInCharge { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public int Pk { get; set; }
        public DateTime? ResponseDateTime { get; set; }
        public string TypeOfEmergencyWork { get; set; }
        public string Status { get; set; }
        public DateTime? ContactEmployerDateTime { get; set; }
        public DateTime? SecureTheSiteDateTime { get; set; }
        public DateTime? RemoveObstructionDateTime { get; set; }
        public string Source { get; set; }
        public int? SectionPk { get; set; }
        public string IncidentRemark { get; set; }
        public bool? IsAccident { get; set; }
        public DateTime? InsuranceDocSubmissionDate { get; set; }
        public int? ContractorPk { get; set; }
        public string AttendedBy { get; set; }
        public double? GeoLat { get; set; }
        public double? GeoLong { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public double? Kmlocation { get; set; }
        public double? ResponseGeoLat { get; set; }
        public double? ResponseGeoLong { get; set; }
        public DateTime? EmergencyDate { get; set; }
        public string ActualDefectCause { get; set; }
        public string ActionTaken { get; set; }
        public string ReplacementItem { get; set; }
    }
}
