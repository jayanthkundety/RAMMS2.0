using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmModuleRightsCode
    {
        public RmModuleRightsCode()
        {
            RmFieldDisRightsDtl = new HashSet<RmFieldDisRightsDtl>();
        }

        public int MrcPkId { get; set; }
        public string MrcPermLevel { get; set; }
        public string MrcModuleName { get; set; }
        public string MrcScreenName { get; set; }
        public int? MrcAddYn { get; set; }
        public int? MrcEdtYn { get; set; }
        public int? MrcDelYn { get; set; }
        public int? MrcViewYn { get; set; }
        public DateTime? MrcEffFrmDt { get; set; }
        public DateTime? MrcEffToDt { get; set; }
        public string MrcRemarks { get; set; }
        public string MrcModBy { get; set; }
        public DateTime MrcModDt { get; set; }
        public string MrcCrBy { get; set; }
        public DateTime MrcCrDt { get; set; }
        public bool MrcSubmitSts { get; set; }

        public virtual ICollection<RmFieldDisRightsDtl> RmFieldDisRightsDtl { get; set; }
    }
}
