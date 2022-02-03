using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class KpiBucketSizeParam
    {
        public int PbContractMonthPk { get; set; }
        public int RegionPk { get; set; }
        public int BucketSize { get; set; }

        public virtual PbContractMonth PbContractMonthPkNavigation { get; set; }
        public virtual Region RegionPkNavigation { get; set; }
    }
}
