using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmAssetGroupType
    {
        public RmAssetGroupType()
        {
            RmAssetDefectCode = new HashSet<RmAssetDefectCode>();
        }

        public int AgtPkRefNo { get; set; }
        public string AgtAssetGrpName { get; set; }
        public string AgtAssetGrpCode { get; set; }
        public string AgtAssetGrpTypeName { get; set; }
        public string AgtAssetGrpTypeCode { get; set; }
        public string AgtGrpTypeContractCode { get; set; }
        public string AgtRemarks { get; set; }
        public string AgtModBy { get; set; }
        public DateTime? AgtModDt { get; set; }
        public string AgtCrBy { get; set; }
        public DateTime? AgtCrDt { get; set; }
        public bool? AgtActiveYn { get; set; }

        public virtual ICollection<RmAssetDefectCode> RmAssetDefectCode { get; set; }
    }
}
