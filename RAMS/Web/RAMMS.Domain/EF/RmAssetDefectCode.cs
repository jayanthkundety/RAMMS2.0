using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmAssetDefectCode
    {
        public int AdcPkRefNo { get; set; }
        public int? AdcAgtPkRefNo { get; set; }
        public string AdcAssetGrpCode { get; set; }
        public string AdcAssetGrpTypeName { get; set; }
        public string AdcDefName { get; set; }
        public string AdcDefCode { get; set; }
        public string AdcDefContractCode { get; set; }
        public string AdcRemarks { get; set; }
        public string AdcFormNo { get; set; }
        public string AdcModBy { get; set; }
        public DateTime? AdcModDt { get; set; }
        public string AdcCrBy { get; set; }
        public DateTime? AdcCrDt { get; set; }
        public bool? AdcActiveYn { get; set; }

        public virtual RmAssetGroupType AdcAgtPkRefNoNavigation { get; set; }
    }
}
