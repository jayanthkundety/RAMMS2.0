using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFieldDisRightsDtl
    {
        public int FrdPkId { get; set; }
        public int? FrdMrcPkId { get; set; }
        public string FrdModuleName { get; set; }
        public string FrdScreenName { get; set; }
        public string FrdFieldName { get; set; }
        public string FrdFieldObjId { get; set; }
        public DateTime? FrdEffFrmDt { get; set; }
        public DateTime? FrdEffToDt { get; set; }
        public string FrdRemarks { get; set; }
        public string FrdModBy { get; set; }
        public DateTime FrdModDt { get; set; }
        public string FrdCrBy { get; set; }
        public DateTime FrdCrDt { get; set; }
        public bool FrdSubmitSts { get; set; }

        public virtual RmModuleRightsCode FrdMrcPk { get; set; }
    }
}
