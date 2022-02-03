using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class AssetDefectCodeRequestDTO
    {
        [MapTo("AdcPkRefNo")]
        public int No { get; set; }

        [MapTo("AdcAssetGrpCode")]
        public string GroupCode { get; set; }

        [MapTo("AdcDefName")]
        public string DefectName { get; set; }

        [MapTo("AdcDefCode")]
        public string DefectCode { get; set; }

        [MapTo("AdcDefContractCode")]
        public string ContractCode { get; set; }
        [MapTo("AdcFormNo")]
        public string FormNo { get; set; }
        [MapTo("AdcActiveYn")]
        public bool? ActiveYN { get; set; }
    }
}
