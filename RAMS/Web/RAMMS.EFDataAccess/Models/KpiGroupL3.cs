using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class KpiGroupL3
    {
        public KpiGroupL3()
        {
            KpiItem = new HashSet<KpiItem>();
        }

        public int Pk { get; set; }
        public int KpiGroupL2pk { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual KpiGroupL2 KpiGroupL2pkNavigation { get; set; }
        public virtual ICollection<KpiItem> KpiItem { get; set; }
    }
}
