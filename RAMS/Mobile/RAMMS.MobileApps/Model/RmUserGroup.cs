using System;
using System.Collections.Generic;

namespace RAMMS.MobileApps
{
    public partial class RmUserGroup
    {
        public int UgPkId { get; set; }
        public int? UgUsrPkId { get; set; }
        public string UgGroupName { get; set; }
        public string UgGroupCode { get; set; }
        public bool? UgDfltYn { get; set; }
        public DateTime? UgEffFrmDt { get; set; }
        public DateTime? UgEffToDt { get; set; }
        public string UgRemarks { get; set; }
        public string UgModBy { get; set; }
        public DateTime UgModDt { get; set; }
        public string UgCrBy { get; set; }
        public DateTime UgCrDt { get; set; }
        public bool UgSubmitSts { get; set; }

        public virtual RmUsers UgUsrPk { get; set; }
    }
}
