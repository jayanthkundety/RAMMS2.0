using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Report
{
    public class FormR1R2Rpt
    {
        public string RefernceNo { get; set; }
        public string StructureCode { get; set; }
        public int? LocationChainageKm { get; set; }
        public string LocationChainageM { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public string Division { get; set; }
        public string RMU { get; set; }
        public double? GPSEasting { get; set; }
        public double? GPSNorthing { get; set; }
        public string WallFunction { get; set; }
        public string WallMember { get; set; }
        public string FacingType { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string DistressObserved { get; set; }
        public int? Severity { get; set; }
        public int? ReportforYear { get; set; }
        public int? RatingRecordNo { get; set; }
        public string PartB2ServiceProvider { get; set; }
        public string PartB2ServicePrvdrCons { get; set; }
        public string PartCGeneralComments { get; set; }
        public string PartCGeneralCommentsCons { get; set; }
        public string PartDFeedback { get; set; }
        public string PartDFeedbackCons { get; set; }
        public string InspectedByName { get; set; }
        public string InspectedByDesignation { get; set; }
        public DateTime? InspectedByDate { get; set; }
        public string AuditedByName { get; set; }
        public string AuditedByDesignation { get; set; }
        public DateTime? AuditedByDate { get; set; }
        public int? RatingWallConditionRate { get; set; }
        public string HaveIssueFound { get; set; }
        public string AssetRefNO { get; set; }
        public List<Pictures> Pictures { get; set; }
        public int PkRefNo { get; set; }
    }
}
