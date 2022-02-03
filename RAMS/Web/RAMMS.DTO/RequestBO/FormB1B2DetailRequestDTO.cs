using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class FormB1B2DetailRequestDTO
    {
        [MapTo("FbridPkRefNo")] public int PkRefNo { get; set; }
        [MapTo("FbridFbrihPkRefNo")] public int? FbrihPkRefNo { get; set; }
        [MapTo("FbridAbutFoundMat")] public string AbutFoundMat { get; set; }
        [MapTo("FbridAbutFoundMatCode")] public string AbutFoundMatCode { get; set; }
        [MapTo("FbridAbutFoundInspCode")] public string AbutFoundInspCode { get; set; }
        [MapTo("FbridAbutFoundDistress")] public string AbutFoundDistress { get; set; }
        [MapTo("FbridAbutFoundSeverity")] public int? AbutFoundSeverity { get; set; }
        [MapTo("FbridPiersPrimCompMat")] public string PiersPrimCompMat { get; set; }
        [MapTo("FbridPiersPrimCompMatCode")] public string PiersPrimCompMatCode { get; set; }
        [MapTo("FbridPiersPrimCompInspCode")] public string PiersPrimCompInspCode { get; set; }
        [MapTo("FbridPiersPrimCompDistress")] public string PiersPrimCompDistress { get; set; }
        [MapTo("FbridPiersPrimCompSeverity")] public int? PiersPrimCompSeverity { get; set; }
        [MapTo("FbridBearingStDiaphgMat")] public string BearingStDiaphgMat { get; set; }
        [MapTo("FbridBearingStDiaphgMatCode")] public string BearingStDiaphgMatCode { get; set; }
        [MapTo("FbridBearingStDiaphgInspCode")] public string BearingStDiaphgInspCode { get; set; }
        [MapTo("FbridBearingStDiaphgDistress")] public string BearingStDiaphgDistress { get; set; }
        [MapTo("FbridBearingStDiaphgSeverity")] public int? BearingStDiaphgSeverity { get; set; }
        [MapTo("FbridBeamsGridTrusArch")] public string BeamsGridTrusArch { get; set; }
        [MapTo("FbridBeamsGridTrusArchCode")] public string BeamsGridTrusArchCode { get; set; }
        [MapTo("FbridBeamsGridTrusArchInspCode")] public string BeamsGridTrusArchInspCode { get; set; }
        [MapTo("FbridBeamsGridTrusArchDistress")] public string BeamsGridTrusArchDistress { get; set; }
        [MapTo("FbridBeamsGridTrusArchSeverity")] public int? BeamsGridTrusArchSeverity { get; set; }
        [MapTo("FbridDeckPavement")] public string DeckPavement { get; set; }
        [MapTo("FbridDeckPavementCode")] public string DeckPavementCode { get; set; }
        [MapTo("FbridDeckPavementCodeInspCode")] public string DeckPavementCodeInspCode { get; set; }
        [MapTo("FbridDeckPavementDistress")] public string DeckPavementDistress { get; set; }
        [MapTo("FbridDeckPavementSeverity")] public int? DeckPavementSeverity { get; set; }
        [MapTo("FbridUtilities")] public string Utilities { get; set; }
        [MapTo("FbridUtilitiesCode")] public string UtilitiesCode { get; set; }
        [MapTo("FbridUtilitiesInspCode")] public string UtilitiesInspCode { get; set; }
        [MapTo("FbridUtilitiesDistress")] public string UtilitiesDistress { get; set; }
        [MapTo("FbridUtilitiesSeverity")] public int? UtilitiesSeverity { get; set; }
        [MapTo("FbridWaterway")] public string Waterway { get; set; }
        [MapTo("FbridWaterwayCode")] public string WaterwayCode { get; set; }
        [MapTo("FbridWaterwayInspCode")] public string WaterwayInspCode { get; set; }
        [MapTo("FbridWaterwayDistress")] public string WaterwayDistress { get; set; }
        [MapTo("FbridWaterwaySeverity")] public int? WaterwaySeverity { get; set; }
        [MapTo("FbridWaterDownpipe")] public string WaterDownpipe { get; set; }
        [MapTo("FbridWaterDownpipeCode")] public string WaterDownpipeCode { get; set; }
        [MapTo("FbridWaterDownpipeInspCode")] public string WaterDownpipeInspCode { get; set; }
        [MapTo("FbridWaterDownpipeDistress")] public string WaterDownpipeDistress { get; set; }
        [MapTo("FbridWaterDownpipeSeverity")] public int? WaterDownpipeSeverity { get; set; }
        [MapTo("FbridParapetRailing")] public string ParapetRailing { get; set; }
        [MapTo("FbridParapetRailingCode")] public string ParapetRailingCode { get; set; }
        [MapTo("FbridParapetRailingInspCode")] public string ParapetRailingInspCode { get; set; }
        [MapTo("FbridParapetRailingDistress")] public string ParapetRailingDistress { get; set; }
        [MapTo("FbridParapetRailingSeverity")] public int? ParapetRailingSeverity { get; set; }
        [MapTo("FbridSidewalksAppSlab")] public string SidewalksAppSlab { get; set; }
        [MapTo("FbridSidewalksAppSlabCode")] public string SidewalksAppSlabCode { get; set; }
        [MapTo("FbridSidewalksAppSlabInspCode")] public string SidewalksAppSlabInspCode { get; set; }
        [MapTo("FbridSidewalksAppSlabDistress")] public string SidewalksAppSlabDistress { get; set; }
        [MapTo("FbridSidewalksAppSlabSeverity")] public int? SidewalksAppSlabSeverity { get; set; }
        [MapTo("FbridExpanJoint")] public string ExpanJoint { get; set; }
        [MapTo("FbridExpanJointCode")] public string ExpanJointCode { get; set; }
        [MapTo("FbridExpanJointInspCode")] public string ExpanJointInspCode { get; set; }
        [MapTo("FbridExpanJointDistress")] public string ExpanJointDistress { get; set; }
        [MapTo("FbridExpanJointSeverity")] public int? ExpanJointSeverity { get; set; }
        [MapTo("FbridSlopeRetainWall")] public string SlopeRetainWall { get; set; }
        [MapTo("FbridSlopeRetainWallCode")] public string SlopeRetainWallCode { get; set; }
        [MapTo("FbridSlopeRetainWallInspCode")] public string SlopeRetainWallInspCode { get; set; }
        [MapTo("FbridSlopeRetainWallDistress")] public string SlopeRetainWallDistress { get; set; }
        [MapTo("FbridSlopeRetainWallSeverity")] public int? SlopeRetainWallSeverity { get; set; }

        [MapTo("FbridAbutFoundDistressOthers")] public string AbutFoundDistressOthers { get; set; }
        [MapTo("FbridPiersPrimCompDistressOthers")] public string PiersPrimCompDistressOthers { get; set; }
        [MapTo("FbridBearingStDiaphgDistressOthers")] public string BearingStDiaphgDistressOthers { get; set; }
        [MapTo("FbridBeamsGridTrusArchDistressOthers")] public string BeamsGridTrusArchDistressOthers { get; set; }
        [MapTo("FbridDeckPavementDistressOthers")] public string DeckPavementDistressOthers { get; set; }
        [MapTo("FbridUtilitiesDistressOthers")] public string UtilitiesDistressOthers { get; set; }
        [MapTo("FbridWaterwayDistressOthers")] public string WaterwayDistressOthers { get; set; }
        [MapTo("FbridWaterDownpipeDistressOthers")] public string WaterDownpipeDistressOthers { get; set; }
        [MapTo("FbridParapetRailingDistressOthers")] public string ParapetRailingDistressOthers { get; set; }
        [MapTo("FbridSidewalksAppSlabDistressOthers")] public string SidewalksAppSlabDistressOthers { get; set; }
        [MapTo("FbridExpanJointDistressOthers")] public string ExpanJointDistressOthers { get; set; }
        [MapTo("FbridSlopeRetainWallDistressOthers")] public string SlopeRetainWallDistressOthers { get; set; }
        [MapTo("FbridModBy")] public int? ModBy { get; set; }
        [MapTo("FbridModDt")] public DateTime? ModDt { get; set; }
        [MapTo("FbridCrBy")] public int? CrBy { get; set; }
        [MapTo("FbridCrDt")] public DateTime? CrDt { get; set; }
        [MapTo("FbridSubmitSts")] public bool SubmitSts { get; set; }
        [MapTo("FbridActiveYn")] public bool ActiveYn { get; set; }
    }
}
