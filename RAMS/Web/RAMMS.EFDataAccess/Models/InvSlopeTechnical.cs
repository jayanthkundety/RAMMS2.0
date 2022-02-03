using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvSlopeTechnical
    {
        public int Pk { get; set; }
        public double? AssetKmfrom { get; set; }
        public double? AssetKmto { get; set; }
        public double? LatitudeStart { get; set; }
        public double? LongitudeStart { get; set; }
        public double? LatitudeEnd { get; set; }
        public double? LongitudeEnd { get; set; }
        public double? Height { get; set; }
        public double? Length { get; set; }
        public double? BermCount { get; set; }
        public string SlopeAngle { get; set; }
        public string SlopeType { get; set; }
        public string HazardRiskRanking { get; set; }
        public string SlopeMaterial { get; set; }
        public string Owner { get; set; }
        public double? AssetKmlocation { get; set; }
        public string SlopeNumber { get; set; }
        public string AssetName { get; set; }
        public string Alias { get; set; }
        public string MaintainedBy { get; set; }
        public string Abbreviation { get; set; }
        public int InvMasterPk { get; set; }
        public string Remark { get; set; }
        public string LithologyRockType { get; set; }
        public string OtherLithologyRockType { get; set; }
        public string WeatheringGrade { get; set; }
        public string AdverseGeologicalStructure { get; set; }
        public string RockSoilInterface { get; set; }
        public string MajorGeologicalStructure { get; set; }
        public string OtherMajorGeologicalStructure { get; set; }
        public string NoOfDiscontinuitySet { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }
}
