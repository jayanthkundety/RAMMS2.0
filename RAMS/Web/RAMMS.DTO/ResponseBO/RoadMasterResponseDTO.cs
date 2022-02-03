using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class RoadMasterResponseDTO
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
        public string CategoryName { get; set; }

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

        [MapTo("RdmOwner")]
        public string Owner { get; set; }

        [MapTo("RdmModBy")]
        public string ModBy { get; set; }

        [MapTo("RdmModDt")]
        public DateTime? ModDt { get; set; }

        [MapTo("RdmCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("RdmCrDt")]
        public DateTime? CreatedDt { get; set; }

        [MapTo("RdmActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo("RdmSecCode")]
        public int? SecCode { get; set; }

        [MapTo("RdmRmuName")]
        public string RmuName { get; set; }        

        public string AssetId { get; set; }
    }
}
