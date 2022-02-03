using System;
using System.Collections.Generic;

namespace RAMMS.DTO.Report
{
    public class FormB1B2Rpt
    {
        public int? ChainageKm { get; set; }
        public string ChainageM { get; set; }
        public string StructureCode { get; set; }
        public double? GPSEasting { get; set; }
        public double? GPSNorthing { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public string RiverName { get; set; }
        public string ReferenceNo { get; set; }
        public string Division { get; set; }
        public string Rmu { get; set; }
        public string Superstructure { get; set; }
        public string ParapetType { get; set; }
        public string BearingType { get; set; }
        public string ExpansionType { get; set; }
        public string DeckType { get; set; }
        public string AbutmentType { get; set; }
        public string PierType { get; set; }
        public int? NumberOfExpansion { get; set; }
        public double? LaneWidth { get; set; }
        public double? SpanLength { get; set; }
        public double? BridgeLength { get; set; }
        public double? BridgeWidth { get; set; }
        public int? NoOfLane { get; set; }
        public int? NoOfSpan { get; set; }
        public double? Median { get; set; }
        public double? Walkway { get; set; }

        public string AbutmentWall_Foundation_Material { get; set; }
        public InspectionRpt AbutmentWall_Foundation_Distress_Severity { get; set; }
        public string Piers_Connection_of_primary_components_Material { get; set; }
        public InspectionRpt Piers_Connection_of_primary_components_Distresss_Severity { get; set; }
        public string Bearing_Material { get; set; }
        public InspectionRpt Bearing_Distress_Severity { get; set; }
        public string Beam_Material { get; set; }
        public InspectionRpt Beam_Distress_Severity { get; set; }
        public string Deck_Material { get; set; }
        public InspectionRpt Deck_Distress_Severity { get; set; }

        public string Signboard_Material { get; set; }
        public InspectionRpt Signboard_Distress_Severity { get; set; }

        public string Waterway_Material { get; set; }
        public InspectionRpt Waterway_Distress_Severity { get; set; }

        public string Drainwater_Material { get; set; }
        public InspectionRpt Drainwater_Distress_Severity { get; set; }

        public string Parapet_Material { get; set; }
        public InspectionRpt Parapet_Distress_Severity { get; set; }

        public string Kerb_Material { get; set; }
        public InspectionRpt Kerb_Distress_Severity { get; set; }

        public string Expansion_Material { get; set; }
        public InspectionRpt Expansion_Distress_Severity { get; set; }

        public string Slope_Material { get; set; }
        public InspectionRpt Slope_Distress_Severity { get; set; }

        public string PartB_Serviceprovider { get; set; }

        public string PartC_Serviceprovider { get; set; }
        public string PartD_Feedback { get; set; }

        public string InspectedByName { get; set; }
        public string InspectedByDesignation { get; set; }
        public DateTime? InspectedByDate { get; set; }

        public string AuditedByName { get; set; }
        public string AuditedByDesignation { get; set; }
        public DateTime? AuditedByDate { get; set; }
        public DateTime? DateOfInspection { get; set; }

        public int? BridgeConditionRating { get; set; }
        public string RequireFurtherInvestigation { get; set; }
        public List<Pictures> Pictures { get; set; }
        public int? Year { get; set; }
        public string PartB_Consultant { get; set; }
        public string PartC_Consultant { get; set; }
        public string PartD_Consultant { get; set; }
        public int? RatingRecordNo { get; set; }
        public int? PkRefNo { get; set; }
    }

    public class InspectionRpt
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public string Distress { get; set; }
        public int? Severity { get; set; }

    }

    public class Pictures
    {
        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public string FileName { get; set; }
    }

}
