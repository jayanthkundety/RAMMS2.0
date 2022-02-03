using System;
using System.Collections.Generic;

namespace RAMMS.DTO.Report
{
    public class FormC1C2Rpt
    {
        public string RefernceNo { get; set; }
        public string Division { get; set; }
        public string RMU { get; set; }
        public double? FinishedRoadLevel { get; set; }
        public double? CatchmentArea { get; set; }
        public double? DesignFlow { get; set; }
        public string Precast { get; set; }
        public int? BarrelNumber { get; set; }
        public double? InletLevel { get; set; }
        public double? OutletLevel { get; set; }
        public string RoadName { get; set; }
        public string RoadCode { get; set; }
        public int? LocationChainageKm { get; set; }
        public string LocationChainageM { get; set; }
        public string StructureCode { get; set; }
        public double? CulverSkew { get; set; }
        public double? CulvertLength { get; set; }
        public string CulvertType { get; set; }
        public string Culvertmaterial { get; set; }
        public string InletStructure { get; set; }
        public string OutletStructure { get; set; }
        public string ParkingPosition { get; set; }
        public string Accessiblity { get; set; }
        public string PotentialHazards { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string CulvertDistress { get; set; }
        public int? CulvertSeverity { get; set; }
        public string WaterwayDistress { get; set; }
        public int? WaterwaySeverity { get; set; }
        public string EmbankmentDistress { get; set; }
        public int? EmbankmentSeverity { get; set; }
        public string HeadwallInletDistress { get; set; }
        public int? HeadwallInletSeverity { get; set; }
        public string WingwallInletDistress { get; set; }
        public int? WingwalInletSeverity { get; set; }
        public string ApronInletDistress { get; set; }
        public int? ApronInletSeverity { get; set; }
        public string RiprapInletDistress { get; set; }
        public int? RiprapInletSeverity { get; set; }
        public string HeadwallOutletDistress { get; set; }
        public int? HeadwallOutletSeverity { get; set; }
        public string WingwallOutletDistress { get; set; }
        public int? WingwallOutletSeverity { get; set; }
        public string ApronOutletDistress { get; set; }
        public int? ApronOutletSeverity { get; set; }
        public string RiprapOutletDistress { get; set; }
        public int? RiprapOutletSeverity { get; set; }
        public string Barrel_1_Distress { get; set; }
        public int? Barrel_1_Severity { get; set; }
        public string Barrel_2_Distress { get; set; }
        public int? Barrel_2_Severity { get; set; }
        public string Barrel_3_Distress { get; set; }
        public int? Barrel_3_Severity { get; set; }
        public string Barrel_4_Distress { get; set; }
        public int? Barrel_4_Severity { get; set; }
        public double? GPSEasting { get; set; }
        public double? GPSNorthing { get; set; }
        public int? ReportforYear { get; set; }
        public string CulvertReference { get; set; }
        public int RatingRecordNo { get; set; }
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
        public string AssetRefNO { get; set; }
        public DateTime? AuditedByDate { get; set; }
        public int? CulverConditionRate { get; set; }
        public string HaveIssueFound { get; set; }
        public List<Pictures> Pictures { get; set; }
        public object CulvertApproachDistress { get; set; }
        public object CulvertApproachSeverity { get; set; }
        public List<BarrelDistressSeverity> BarrelList { get; set; }
        public int PkRefNo { get; set; }
    }

    public class BarrelDistressSeverity
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public int? Severity { get; set; }
        public string Distress { get; set; }
    }
}
