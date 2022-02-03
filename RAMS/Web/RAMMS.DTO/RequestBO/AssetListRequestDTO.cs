using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.RequestBO
{
    public class AssetListRequestDTO
    {
        public string SmartInputValue { get; set; }

        //public string AssetGroup { get; set; }

        [MapTo("AiPkRefNo")]
        public int No { get; set; }

        [MapTo("AiAssetId")]
        public string AssetId { get; set; }

        [MapTo("AiDivCode")]
        public string DivisionCode { get; set; }

        [MapTo("AiDist")]
        public string District { get; set; }

        [MapTo("AiRmuCode")]
        public string RMUCode { get; set; }

        //[MapTo("AiRmuAbb")]
        //public string RMUAbbrev { get; set; }

        [MapTo("AiSecCode")]
        [Required(ErrorMessage = "Section Code is not provided")]
        public string SectionCode { get; set; }

        [MapTo("AiSecName")]
        public string SectionName { get; set; }

        [MapTo("AiRdCode")]
        public string RoadCode { get; set; }

        [MapTo("AiRdName")]
        public string RoadName { get; set; }

        [MapTo("AiAssetGrpCode")]
        public string GroupCode { get; set; }

        [MapTo("AiGrpType")]
        public string GroupType { get; set; }

        [MapTo("AiLocChKm")]
        public int? LocationChKm { get; set; }

        [MapTo("AiLocChm")]
        public string LocationChM { get; set; }

        [MapTo("AiBound")]
        public string Bound { get; set; }

        [MapTo("AiStrucCode")]
        public string StructureCode { get; set; }

        [MapTo("AIRefNo")]
        [Required(ErrorMessage = "Reference No is not provided")]
        public string ReferenceNo { get; set; }

        [MapTo("AiFeatureId")]
        public string FeatureId { get; set; }

        [MapTo("AiDiameter")]
        public double? Diameter { get; set; }

        [MapTo("AiWidth")]
        public double? Width { get; set; }

        [MapTo("AiHeight")]
        public double? Height { get; set; }

        [MapTo("AiMaterial")]
        public string Material { get; set; }

        [MapTo("AiFinRdLevel")]
        public double? FindRoadLevel { get; set; }

        [MapTo("AiCatchArea")]
        public double? CatchArea { get; set; }

        [MapTo("AiSkew")]
        public double? Skew { get; set; }

        [MapTo("AiDesignFlow")]
        public double? DesignFlow { get; set; }

        [MapTo("AiLength")]
        public double? Length { get; set; }

        [MapTo("AiPrecastSitu")]
        public string PrecastSitu { get; set; }

        [MapTo("AiBarrelNo")]
        public int? BarrelNo { get; set; }

        [MapTo("AiIntelLevel")]
        public double? IntelLevel { get; set; }

        [MapTo("AiIntelStruc")]
        public string InletStruc { get; set; }

        [MapTo("AiOutletLevel")]
        public double? OutletLevel { get; set; }

        [MapTo("AiOutletStruc")]
        public string OutletStruc { get; set; }

        [MapTo("AiOwner")]
        public string Owner { get; set; }

        [MapTo("AiMaintainedBy")]
        public string MaintainedBy { get; set; }

        [MapTo("AiGpsEasting")]
        public double? GpsEasting { get; set; }

        [MapTo("AiGpsNorthing")]
        public double? GpsNorthing { get; set; }

        [MapTo("AiRiverName")]
        public string RiverName { get; set; }

        [MapTo("AiWidthLane")]
        public double? WidthLane { get; set; }

        [MapTo("AiLengthSpan")]
        public double? LengthSpan { get; set; }

        [MapTo("AiBridgeName")]
        public string BridgeName { get; set; }

        [MapTo("AiLaneCnt")]
        public int? LaneCount { get; set; }

        [MapTo("AiSpanCnt")]
        public int? SpanCount { get; set; }

        [MapTo("AiMedian")]
        public double? Median { get; set; }

        [MapTo("AiWalkway")]
        public double? Walkway { get; set; }

        [MapTo("AiStrucSuper")]
        public string StrucSuper { get; set; }

        [MapTo("AiParapetType")]
        public string ParapetType { get; set; }

        [MapTo("AiBearingType")]
        public string BearingType { get; set; }

        [MapTo("AiExpanType")]
        public string ExpanType { get; set; }

        [MapTo("AiDeckType")]
        public string DeckType { get; set; }

        [MapTo("AiAbutType")]
        public string AbutType { get; set; }

        [MapTo("AiPierType")]
        public string PierType { get; set; }

        [MapTo("AiExpanJointCount")]
        public int? ExpanJointCount { get; set; }

        [MapTo("AiExpanJointSpace")]
        public double? ExpanJointSpace { get; set; }

        [MapTo("AiAbutFound")]
        public string AbutFound { get; set; }

        [MapTo("AiPiersPrimComp")]
        public string PiersPrimComp { get; set; }

        [MapTo("AiBearingSeatDiaphg")]
        public string BearingSeatDiaphg { get; set; }

        [MapTo("AiBeamsGridTrusArch")]
        public string BeamsGridTrusArch { get; set; }

        [MapTo("AiDeckPavement")]
        public string DeckPavement { get; set; }

        [MapTo("AiUtilities")]
        public string Utilities { get; set; }

        [MapTo("AiWaterway")]
        public string Waterway { get; set; }

        [MapTo("AiWaterDownpipe")]
        public string WaterDownpipe { get; set; }

        [MapTo("AiParapetRailing")]
        public string ParapetRailing { get; set; }

        [MapTo("AiSidewalksAppSlab")]
        public string SidewalksAppSlab { get; set; }

        [MapTo("AiExpanJoint")]
        public string ExpanJoint { get; set; }

        [MapTo("AiSlopeRetainWall")]
        public string SlopeRetainWall { get; set; }

        [MapTo("AiBuiltYear")]
        public int? BuiltYear { get; set; }

        [MapTo("AiFrmCh")]
        public int? FromCh { get; set; }

        [MapTo("AiFrmChDeci")]
        public string FromChDesi { get; set; }

        [MapTo("AiToCh")]
        public int? ToCh { get; set; }

        [MapTo("AiToChDeci")]
        public string ToChDeci { get; set; }

        [MapTo("AiLaneNo")]
        public string LaneNo { get; set; }

        [MapTo("AiPostSpacing")]
        public double? PostSpacing { get; set; }

        [MapTo("AiTier")]
        public double? Tier { get; set; }

        [MapTo("AiBotWidth")]
        public double? BotWidth { get; set; }

        [MapTo("AiModBy")]
        public string ModifiedBy { get; set; }

        [MapTo("AiModDt")]
        public DateTime? ModifiedDt { get; set; }

        [MapTo("AiCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("AiCrDt")]
        public DateTime? CreatedDate { get; set; }

        [MapTo("AiSubmitSts")]
        public bool SubmitStatus { get; set; }

        [MapTo("AiActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo("AiRmuName")]
        public string RmuName { get; set; }

        [MapTo("AiAssetNumber")]
        public string AssetNumber { get; set; }

        [MapTo("AiRdmPkRefNo")]
        public int? RoadMasterNo { get; set; }

        [MapTo("AiCulvertType")]
        public string CulvertType { get; set; }


        public string AssetTypeName { get; set; }

        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? Page_No { get; set; }
        public int? pageSize { get; set; }


        public List<string> StrucSuper_multiSelect { get; set; }
        public List<string> ParapetType_multiSelect { get; set; }
        public List<string> BearingType_multiSelect { get; set; }
        public List<string> ExpanType_multiSelect { get; set; }
        public List<string> DeckType_multiSelect { get; set; }
        public List<string> AbutType_multiSelect { get; set; }

        public List<string> PierType_multiSelect { get; set; }
        public List<string> AbutFound_multiSelect { get; set; }
        public List<string> PiersPrimComp_multiSelect { get; set; }
        public List<string> BearingSeatDiaphg_multiSelect { get; set; }
        public List<string> BeamsGridTrusArch_multiSelect { get; set; }
        public List<string> DeckPavement_multiSelect { get; set; }

        public List<string> Utilities_multiSelect { get; set; }
        public List<string> Waterway_multiSelect { get; set; }
        public List<string> WaterDownpipe_multiSelect { get; set; }
        public List<string> ParapetRailing_multiSelect { get; set; }
        public List<string> SidewalksAppSlab_multiSelect { get; set; }
        public List<string> ExpanJoint_multiSelect { get; set; }
        public List<string> SlopeRetainWall_multiSelect { get; set; }
        public List<string> InletStruc_MultiSelect { get; set; }
        public List<string> OutletStruc_MultiSelect { get; set; }

        public string AssetImageType { get; set; }


    }
}
