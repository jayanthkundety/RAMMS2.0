using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class RoadMasterResponseDTO
    {
       
        public int No { get; set; }

       
        public string FeatureId { get; set; }

       
        public string DivisionCode { get; set; }

       
        public string RmuCode { get; set; }

       
        public string SecName { get; set; }

       
        public string CategoryName { get; set; }

       
        public string CategoryCode { get; set; }

       
        public string RoadCode { get; set; }

       
        public string RoadName { get; set; }

       
        public string FrmLoc { get; set; }

       
        public string ToLoc { get; set; }

       
        public int? FrmCh { get; set; }

       
        public int? FrmChDeci { get; set; }

       
        public int? ToCh { get; set; }

       
        public int? ToChDeci { get; set; }

       
        public decimal? LengthPaved { get; set; }

       
        public decimal? LengthUnpaved { get; set; }

       
        public string Owner { get; set; }
               
        public string ModBy { get; set; }
               
        public DateTime? ModDt { get; set; }
               
        public string CreatedBy { get; set; }
               
        public DateTime? CreatedDt { get; set; }
               
        public bool? ActiveYn { get; set; }
               
        public int? SecCode { get; set; }

        public string AssetId { get; set; }
    }
}
