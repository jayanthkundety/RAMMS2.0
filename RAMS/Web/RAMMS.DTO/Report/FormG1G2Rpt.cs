using System;
using System.Collections.Generic;

namespace RAMMS.DTO.Report
{
    public class FormG1G2Rpt
    {
        public string RefernceNo { get; set; }
        public string StructureCode { get; set; }
        public int? LocationChainageKm { get; set; }
        public int? LocationChainageM { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }        
        public string Division { get; set; }
        public string RMU { get; set; }
        public decimal? GPSEasting { get; set; }
        public decimal? GPSNorthing { get; set; }
        public string ParkingPosition { get; set; }
        public string Accessiblity { get; set; }
        public string PotentialHazards { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int BarriersYes { get; set; }
        public int BarriersNo { get; set; }
        public int BarriersCritical { get; set; }
        public int BarriersClosed { get; set; }
        public int GantryBeamsYes { get; set; }
        public int GantryBeamsNo { get; set; }
        public int GantryBeamsCritical { get; set; }
        public int GantryBeamsClosed { get; set; }
        public int GantryColsYes { get; set; }
        public int GantryColsNo { get; set; }
        public int GantryColsCritical { get; set; }
        public int GantryColsClosed { get; set; }
        public int FootingYes { get; set; }
        public int FootingNo { get; set; }
        public int FootingCritical { get; set; }
        public int FootingClosed { get; set; }
        public int AnchorYes { get; set; }
        public int AnchorNo { get; set; }
        public int AnchorCritical { get; set; }
        public int AnchorClosed { get; set; }
        public int MaintenanceAccessYes { get; set; }
        public int MaintenanceAccessNo { get; set; }
        public int MaintenanceAccessCritical { get; set; }
        public int MaintenanceAccessClosed { get; set; }
        public int StaticSignsYes { get; set; }
        public int StaticSignsNo { get; set; }
        public int StaticSignsCritical { get; set; }
        public int StaticSignsClosed { get; set; }       
        public int VariableMessagYes { get; set; }
        public int VariableMessagNo { get; set; }
        public int VariableMessagCritical { get; set; }
        public int VariableMessagClosed { get; set; }
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
        public int? GantrySignConditionRate { get; set; }
        public string HaveIssueFound { get; set; }    
        public string AssetRefNO { get; set; }        
        public List<Pictures> Pictures { get; set; }
        public int PkRefNo { get; set; }
    }
}
