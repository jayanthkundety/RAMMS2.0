using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.ResponseBO
{
    public class AssetDefectCodeResponseDTO
    {
        [MapTo("AdcPkRefNo")]
        public int No { get; set; }

        [MapTo("AdcAgtPkRefNo")]
        public int? AdcAgtPkRefNo { get; set; }

        [MapTo("AdcAssetGrpCode")]
        public string GroupCode { get; set; }

        [MapTo("AdcAssetGrpTypeName")]
        public string AdcAssetGrpTypeName { get; set; }

        [MapTo("AdcDefName")]
        public string DefectName { get; set; }

        [MapTo("AdcDefCode")]
        public string DefectCode { get; set; }

        [MapTo("AdcDefContractCode")]
        public string ContractCode { get; set; }

        [MapTo("AdcRemarks")]
        public string Remarks { get; set; }

        [MapTo("AdcFormNo")]
        public string FormNo { get; set; }

        [MapTo("AdcModBy")]
        public string ModBy { get; set; }

        [MapTo("AdcModDt")]
        public DateTime? ModDt { get; set; }

        [MapTo("AdcCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("AdcCrDt")]
        public DateTime? CreatedDt { get; set; }

        [MapTo("AdcActiveYn")]
        public bool? ActiveYN { get; set; }
    }
}
