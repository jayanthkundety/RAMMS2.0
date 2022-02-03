using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class RoadMasterRequestDTO
    {
        [MapTo("RdmPkRefNo")]
        public int No { get; set; }

        [MapTo("RdmFeatureId")]
        public string FeatureId { get; set; }

        [MapTo("RdmDivCode")]
        public string DivisionCode { get; set; }

        [MapTo("RdmRmuCode")]
        public string RmuCode { get; set; }

        [MapTo("RdmSecName")]
        public string SecName { get; set; }

        [MapTo("RdmRdCatgName")]
        public string CategorygName { get; set; }

        [MapTo("RdmRdCatgCode")]
        public string CategoryCode { get; set; }

        [MapTo("RdmRdCode")]
        public string RoadCode { get; set; }

        [MapTo("RdmRdName")]
        public string RoadName { get; set; }

        [MapTo("RdmFrmLoc")]
        public string FrmLoc { get; set; }

        [MapTo("RdmToLoc")]
        public string ToLoc { get; set; }

        [MapTo("RdmFrmCh")]
        public int? FrmCh { get; set; }

        [MapTo("RdmFrmChDeci")]
        public int? FrmChDeci { get; set; }

        [MapTo("RdmToCh")]
        public int? ToCh { get; set; }

        [MapTo("RdmToChDeci")]
        public int? ToChDeci { get; set; }

        [MapTo("RdmLengthPaved")]
        public decimal? LengthPaved { get; set; }

        [MapTo("RdmLengthUnpaved")]
        public decimal? LengthUnpaved { get; set; }

        [MapTo("RdmRmuName")]
        public string RmuName { get; set; }

        [MapTo("RdmSecCode")]
        public int? SecCode { get; set; }
    }
}
