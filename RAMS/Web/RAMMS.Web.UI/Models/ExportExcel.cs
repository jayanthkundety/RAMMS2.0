using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.Web.UI.Models
{
    public class ExportExcel
    {


        public int No { get; set; }

       
        public string AssetId { get; set; }

      
        public string DivCode { get; set; }

        
        public string Dist { get; set; }


        public string RMUCode { get; set; }

       
        public string RMUAbbrev { get; set; }

       
        public string SecCode { get; set; }

   
        public string SecName { get; set; }

      
        public string RdCode { get; set; }

 
        public string RdName { get; set; }

      
        public string AssetGrpCode { get; set; }

  
        public string GrpType { get; set; }

       
       
        public string LocCh { get; set; }
      
        public string Bound { get; set; }

        
        public string StrucCode { get; set; }

       
        public int? RefNo { get; set; }


        public string FeatureId { get; set; }

       
        public double Diameter { get; set; }

      
        public double Width { get; set; }

      
        public double Height { get; set; }

        
        public string Material { get; set; }

   
        public int? FinRdLevel { get; set; }

      
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

     
        public int? LaneCnt { get; set; }

        public int? LocChKm { get; set; }
      
        public int? LocChM { get; set; }

        public int? SpanCnt { get; set; }

     
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


        //public int? FromCh { get; set; }


        //public int? FromChDesi { get; set; }


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
        public string AssetImgPath { get; set; }
        public string HasImage { get; set; }

        public int? FrmCh { get; set; }

        public int? FrmChDeci { get; set; }

        public string AssetNumber { get; set; }

        public string S8 { get; set; }
        public string SNO { get; set; }
    }
}
