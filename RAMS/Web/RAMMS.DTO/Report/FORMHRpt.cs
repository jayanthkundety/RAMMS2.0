using System;
namespace RAMMS.DTO.Report
{
    public class FORMHRpt
    {
        public string InspectionDate { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public string RMU { get; set; }
        public string Chainage { get; set; }
        public string ReferenceNumber { get; set; }
        public string Division { get; set; }
        public string DamageDetail { get; set; }
        public string DamageCausedBy { get; set; }
        public string GeneralComments { get; set; }

        public string ReportedByName { get; set; }
        public string ReportedByDesignation { get; set; }
        public string ReportedByDate { get; set; }
        public string VerifiedBy { get; set; }
        public string VerifiedByDesignation { get; set; }
        public string VerifiedByDate { get; set; }
        public string Remarks { get; set; }
        public string ReceivedBy { get; set; }
        public string ReceivedByDesignation { get; set; }
        public string VettedBy { get; set; }
        public string VettedByDesignation { get; set; }
        public string Recommendation { get; set; }
        public string ReportNumber { get; set; }
    }
}
