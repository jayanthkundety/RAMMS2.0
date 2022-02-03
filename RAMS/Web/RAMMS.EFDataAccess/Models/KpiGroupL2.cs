using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class KpiGroupL2
    {
        public KpiGroupL2()
        {
            KpiGroupL3 = new HashSet<KpiGroupL3>();
        }

        public int Pk { get; set; }
        public int KpiGroupL1pk { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual KpiGroupL1 KpiGroupL1pkNavigation { get; set; }
        public virtual ICollection<KpiGroupL3> KpiGroupL3 { get; set; }
    }
}
