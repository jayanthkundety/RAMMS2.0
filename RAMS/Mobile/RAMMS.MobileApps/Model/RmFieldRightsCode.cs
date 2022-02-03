using System;
using System.Collections.Generic;

namespace RAMMS.MobileApps
{
    public partial class RmFieldRightsCode
    {
        public int FrcPkId { get; set; }
        public int? FrcMrcPkId { get; set; }
        public string FrcPermLevel { get; set; }
        public string FrcScreenName { get; set; }
        public string FrcFieldName { get; set; }
        public string FrcRemarks { get; set; }
        public string FrcModBy { get; set; }
        public DateTime FrcModDt { get; set; }
        public string FrcCrBy { get; set; }
        public DateTime FrcCrDt { get; set; }
        public bool FrcSubmitSts { get; set; }
        public bool? FrcActiveYn { get; set; }

        //public virtual RmModuleRightsCode FrcMrcPk { get; set; }
    }
}
