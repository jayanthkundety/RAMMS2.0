using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.RequestBO
{
    public class RoadMasterRequestDTO
    {
        public int No { get; set; }

        public string FeatureId { get; set; }

        public string DivisionCode { get; set; }

        public string RmuCode { get; set; }

        public string SecName { get; set; }

        public string CategorygName { get; set; }

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

        
    }
}
