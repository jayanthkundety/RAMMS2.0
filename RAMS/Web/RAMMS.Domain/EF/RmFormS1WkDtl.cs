using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormS1WkDtl
    {
        public int FsiwdPkRefNo { get; set; }
        public int? FsiwdFsidPkRefNo { get; set; }
        public int? FsiwdSchldDayOfWeek { get; set; }
        public int? FsiwdPlanned { get; set; }
        public int? FsiwdActual { get; set; }
        public DateTime? FsiwdSchldDate { get; set; }
        public int? FsiwdCrBy { get; set; }
        public DateTime? FsiwdCrDt { get; set; }

        public virtual RmFormS1Dtl FsiwdFsidPkRefNoNavigation { get; set; }
    }
}
