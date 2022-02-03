using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.MobileApps
{
    public class AssetListRequestDTO
    {
        public string SmartInputValue { get; set; }

        //public string AssetGroup { get; set; }

        public int No { get; set; }

        public string AssetId { get; set; }

        public string DivisionCode { get; set; }

        public string District { get; set; }

        public string RMUCode { get; set; }

        public string SectionCode { get; set; }

        public string SectionName { get; set; }

        public string RoadCode { get; set; }

        public string RoadName { get; set; }

        public string GroupCode { get; set; }

        public string GroupType { get; set; }

        public int? LocationChKm { get; set; }

        public int? LocationChM { get; set; }

        public string Bound { get; set; }

        public string StructureCode { get; set; }

        public int? ReferenceNo { get; set; }

        public string FeatureId { get; set; }

        public double Diameter { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public string Material { get; set; }

        public int? FindRoadLevel { get; set; }

        public int? CatchArea { get; set; }

        public int? Skew { get; set; }

        public int? DesignFlow { get; set; }

        public double Length { get; set; }

        public string PrecastSitu { get; set; }

        public int? BarrelNo { get; set; }

        public int? IntelLevel { get; set; }

        public string IntelStruc { get; set; }

        public int? OutletLevel { get; set; }

        public string OutletStruc { get; set; }

        public string Owner { get; set; }

        public string MaintainedBy { get; set; }

        public decimal? GpsEasting { get; set; }

        public decimal? GpsNorthing { get; set; }

        public string RiverName { get; set; }

        public int? WidthLane { get; set; }

        public int? LengthSpan { get; set; }

        public string BridgeName { get; set; }

        public int? LaneCount { get; set; }

        public int? SpanCount { get; set; }

        public int? Median { get; set; }

        public int? Walkway { get; set; }

        public string StrucSuper { get; set; }

        public string ParapetType { get; set; }

        public string BearingType { get; set; }

        public string ExpanType { get; set; }

        public string DeckType { get; set; }

        public string AbutType { get; set; }

        public string PierType { get; set; }

        public int? ExpanJointCount { get; set; }

        public int? ExpanJointSpace { get; set; }

        public string AbutFound { get; set; }

        public string PiersPrimComp { get; set; }

        public string BearingSeatDiaphg { get; set; }

        public string BeamsGridTrusArch { get; set; }

        public string DeckPavement { get; set; }

        public string Utilities { get; set; }

        public string Waterway { get; set; }

        public string WaterDownpipe { get; set; }

        public string ParapetRailing { get; set; }

        public string SidewalksAppSlab { get; set; }

        public string ExpanJoint { get; set; }

        public string SlopeRetainWall { get; set; }

        public int? BuiltYear { get; set; }

        public int? FromCh { get; set; }

        public int? FromChDeci { get; set; }

        public int? ToCh { get; set; }

        public int? ToChDeci { get; set; }

        public string LaneNo { get; set; }

        public int? PostSpacing { get; set; }

        public int? Tier { get; set; }

        public int? BotWidth { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDt { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool SubmitStatus { get; set; }

        public bool? ActiveYn { get; set; }

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
    }
}
