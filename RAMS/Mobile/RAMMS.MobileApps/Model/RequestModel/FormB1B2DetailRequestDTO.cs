using System;

namespace RAMMS.DTO
{
    public class FormB1B2DetailRequestDTO
    {
        public int PkRefNo { get; set; }
        public int? FbrihPkRefNo { get; set; }
        public string AbutFoundMat { get; set; }
        public string AbutFoundMatCode { get; set; }
        public string AbutFoundInspCode { get; set; }
        public string AbutFoundDistress { get; set; }
        public int? AbutFoundSeverity { get; set; }
        public string PiersPrimCompMat { get; set; }
        public string PiersPrimCompMatCode { get; set; }
        public string PiersPrimCompInspCode { get; set; }
        public string PiersPrimCompDistress { get; set; }
        public int? PiersPrimCompSeverity { get; set; }
        public string BearingStDiaphgMat { get; set; }
        public string BearingStDiaphgMatCode { get; set; }
        public string BearingStDiaphgInspCode { get; set; }
        public string BearingStDiaphgDistress { get; set; }
        public int? BearingStDiaphgSeverity { get; set; }
        public string BeamsGridTrusArch { get; set; }
        public string BeamsGridTrusArchCode { get; set; }
        public string BeamsGridTrusArchInspCode { get; set; }
        public string BeamsGridTrusArchDistress { get; set; }
        public int? BeamsGridTrusArchSeverity { get; set; }
        public string DeckPavement { get; set; }
        public string DeckPavementCode { get; set; }
        public string DeckPavementCodeInspCode { get; set; }
        public string DeckPavementDistress { get; set; }
        public int? DeckPavementSeverity { get; set; }
        public string Utilities { get; set; }
        public string UtilitiesCode { get; set; }
        public string UtilitiesInspCode { get; set; }
        public string UtilitiesDistress { get; set; }
        public int? UtilitiesSeverity { get; set; }
        public string Waterway { get; set; }
        public string WaterwayCode { get; set; }
        public string WaterwayInspCode { get; set; }
        public string WaterwayDistress { get; set; }
        public int? WaterwaySeverity { get; set; }
        public string WaterDownpipe { get; set; }
        public string WaterDownpipeCode { get; set; }
        public string WaterDownpipeInspCode { get; set; }
        public string WaterDownpipeDistress { get; set; }
        public int? WaterDownpipeSeverity { get; set; }
        public string ParapetRailing { get; set; }
        public string ParapetRailingCode { get; set; }
        public string ParapetRailingInspCode { get; set; }
        public string ParapetRailingDistress { get; set; }
        public int? ParapetRailingSeverity { get; set; }
        public string SidewalksAppSlab { get; set; }
        public string SidewalksAppSlabCode { get; set; }
        public string SidewalksAppSlabInspCode { get; set; }
        public string SidewalksAppSlabDistress { get; set; }
        public int? SidewalksAppSlabSeverity { get; set; }
        public string ExpanJoint { get; set; }
        public string ExpanJointCode { get; set; }
        public string ExpanJointInspCode { get; set; }
        public string ExpanJointDistress { get; set; }
        public int? ExpanJointSeverity { get; set; }
        public string SlopeRetainWall { get; set; }
        public string SlopeRetainWallCode { get; set; }
        public string SlopeRetainWallInspCode { get; set; }
        public string SlopeRetainWallDistress { get; set; }
        public int? SlopeRetainWallSeverity { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string AbutFoundDistressOthers { get; set; }
        public string PiersPrimCompDistressOthers { get; set; }
        public string BearingStDiaphgDistressOthers { get; set; }
        public string BeamsGridTrusArchDistressOthers { get; set; }
        public string DeckPavementDistressOthers { get; set; }
        public string UtilitiesDistressOthers { get; set; }
        public string WaterwayDistressOthers { get; set; }
        public string WaterDownpipeDistressOthers { get; set; }
        public string ParapetRailingDistressOthers { get; set; }
        public string SidewalksAppSlabDistressOthers { get; set; }
        public string ExpanJointDistressOthers { get; set; }
        public string SlopeRetainWallDistressOthers { get; set; }
    }
}