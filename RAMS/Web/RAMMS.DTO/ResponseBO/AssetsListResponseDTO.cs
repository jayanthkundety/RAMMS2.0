using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class AssetsListResponseDTO
    {
        public int No { get; set; }

        [MapTo("AiAssetId")]
        public string AssetId { get; set; }

        [MapTo("AiDivCode")]
        public string DivCode { get; set; }

        [MapTo("AiDist")]
        public string Dist { get; set; }

        [MapTo("AiRmuCode")]
        public string RMUCode { get; set; }

        //[MapTo("AiRmuAbb")]
        //public string RMUAbbrev { get; set; }

        [MapTo("AiSecCode")]
        public string SecCode { get; set; }

        [MapTo("AiSecName")]
        public string SecName { get; set; }

        [MapTo("AiRdCode")]
        public string RdCode { get; set; }

        [MapTo("AiRdName")]
        public string RdName { get; set; }

        [MapTo("AiAssetGrpCode")]
        public string AssetGrpCode { get; set; }

        [MapTo("AiGrpType")]
        public string GrpType { get; set; }

        [MapTo("AiLocChKm")]
        public int? LocChKm { get; set; }

        [MapTo("AiLocChm")]
        public string LocChM { get; set; }

      

        public string LocCh
        {
            get
            {
                var locCh="";
                if (LocChKm != null || LocChM != null)
                {
                    locCh = (LocChKm.HasValue?LocChKm:0 )+ "." +( LocChM ?? "0");
                    
                }
                return locCh;
            }
        }

        [MapTo("AiBound")]
        public string Bound { get; set; }

        [MapTo("AiStrucCode")]
        public string StrucCode { get; set; }

        [MapTo("AIRefNo")]
        public string RefNo { get; set; }

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
        public double? FinRdLevel { get; set; }

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
        public string IntelStruc { get; set; }

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
        public int? LaneCnt { get; set; }

        [MapTo("AiSpanCnt")]
        public int? SpanCnt { get; set; }

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
        public string FromChDeci { get; set; }

        public string FrmChKmM
        {
            get
            {
                var FrmChKmM = "";
                if (FromCh != null || FromChDeci != null)
                {
                    FrmChKmM = (FromCh.HasValue ? FromCh : 0) + "." + (FromChDeci ?? "0");

                }
                return FrmChKmM;
            }
        }

        [MapTo("AiToCh")]
        public int? ToCh { get; set; }

        [MapTo("AiToChDeci")]
        public string ToChDeci { get; set; }

        public string ToChKmM
        {
            get
            {
                var ToChKmM = "";
                if (ToCh != null || ToChDeci != null)
                {
                    ToChKmM = (ToCh.HasValue ? ToCh : 0) + "." + (ToChDeci ?? "0");

                }
                return ToChKmM;
            }
        }

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

        public string AssetTypeName { get; set; }
        public string AssetImgPath { get; set; }

        [MapTo("AiFrmCh")]
        public int? FrmCh { get; set; }

        [MapTo("AiFrmChDeci")]
        public int? FrmChDeci { get; set; }

        [MapTo("AiAssetNumber")]
        public string AssetNumber { get; set; }


        [MapTo("AiCulvertType")]
        public string CulvertType { get; set; }

        public AssetInvOtherResDTO AssetInvOther { get; set; }
    }
}
