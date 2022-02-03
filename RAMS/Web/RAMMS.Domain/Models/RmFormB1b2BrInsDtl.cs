using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormB1b2BrInsDtl
    {
        public int FbridPkRefNo { get; set; }
        public int? FbridFbrihPkRefNo { get; set; }
        public string FbridAbutFoundMat { get; set; }
        public string FbridAbutFoundMatCode { get; set; }
        public string FbridAbutFoundInspCode { get; set; }
        public string FbridAbutFoundDistress { get; set; }
        public int? FbridAbutFoundSeverity { get; set; }
        public string FbridPiersPrimCompMat { get; set; }
        public string FbridPiersPrimCompMatCode { get; set; }
        public string FbridPiersPrimCompInspCode { get; set; }
        public string FbridPiersPrimCompDistress { get; set; }
        public int? FbridPiersPrimCompSeverity { get; set; }
        public string FbridBearingStDiaphgMat { get; set; }
        public string FbridBearingStDiaphgMatCode { get; set; }
        public string FbridBearingStDiaphgInspCode { get; set; }
        public string FbridBearingStDiaphgDistress { get; set; }
        public int? FbridBearingStDiaphgSeverity { get; set; }
        public string FbridBeamsGridTrusArch { get; set; }
        public string FbridBeamsGridTrusArchCode { get; set; }
        public string FbridBeamsGridTrusArchInspCode { get; set; }
        public string FbridBeamsGridTrusArchDistress { get; set; }
        public int? FbridBeamsGridTrusArchSeverity { get; set; }
        public string FbridDeckPavement { get; set; }
        public string FbridDeckPavementCode { get; set; }
        public string FbridDeckPavementCodeInspCode { get; set; }
        public string FbridDeckPavementDistress { get; set; }
        public int? FbridDeckPavementSeverity { get; set; }
        public string FbridUtilities { get; set; }
        public string FbridUtilitiesCode { get; set; }
        public string FbridUtilitiesInspCode { get; set; }
        public string FbridUtilitiesDistress { get; set; }
        public int? FbridUtilitiesSeverity { get; set; }
        public string FbridWaterway { get; set; }
        public string FbridWaterwayCode { get; set; }
        public string FbridWaterwayInspCode { get; set; }
        public string FbridWaterwayDistress { get; set; }
        public int? FbridWaterwaySeverity { get; set; }
        public string FbridWaterDownpipe { get; set; }
        public string FbridWaterDownpipeCode { get; set; }
        public string FbridWaterDownpipeInspCode { get; set; }
        public string FbridWaterDownpipeDistress { get; set; }
        public int? FbridWaterDownpipeSeverity { get; set; }
        public string FbridParapetRailing { get; set; }
        public string FbridParapetRailingCode { get; set; }
        public string FbridParapetRailingInspCode { get; set; }
        public string FbridParapetRailingDistress { get; set; }
        public int? FbridParapetRailingSeverity { get; set; }
        public string FbridSidewalksAppSlab { get; set; }
        public string FbridSidewalksAppSlabCode { get; set; }
        public string FbridSidewalksAppSlabInspCode { get; set; }
        public string FbridSidewalksAppSlabDistress { get; set; }
        public int? FbridSidewalksAppSlabSeverity { get; set; }
        public string FbridExpanJoint { get; set; }
        public string FbridExpanJointCode { get; set; }
        public string FbridExpanJointInspCode { get; set; }
        public string FbridExpanJointDistress { get; set; }
        public int? FbridExpanJointSeverity { get; set; }
        public string FbridSlopeRetainWall { get; set; }
        public string FbridSlopeRetainWallCode { get; set; }
        public string FbridSlopeRetainWallInspCode { get; set; }
        public string FbridSlopeRetainWallDistress { get; set; }
        public int? FbridSlopeRetainWallSeverity { get; set; }
        public int? FbridModBy { get; set; }
        public DateTime? FbridModDt { get; set; }
        public int? FbridCrBy { get; set; }
        public DateTime? FbridCrDt { get; set; }
        public bool FbridSubmitSts { get; set; }
        public bool? FbridActiveYn { get; set; }
        public string FbridAbutFoundDistressOthers { get; set; }
        public string FbridPiersPrimCompDistressOthers { get; set; }
        public string FbridBearingStDiaphgDistressOthers { get; set; }
        public string FbridBeamsGridTrusArchDistressOthers { get; set; }
        public string FbridDeckPavementDistressOthers { get; set; }
        public string FbridUtilitiesDistressOthers { get; set; }
        public string FbridWaterwayDistressOthers { get; set; }
        public string FbridWaterDownpipeDistressOthers { get; set; }
        public string FbridParapetRailingDistressOthers { get; set; }
        public string FbridSidewalksAppSlabDistressOthers { get; set; }
        public string FbridExpanJointDistressOthers { get; set; }
        public string FbridSlopeRetainWallDistressOthers { get; set; }

        public virtual RmFormB1b2BrInsHdr FbridFbrihPkRefNoNavigation { get; set; }
    }
}
