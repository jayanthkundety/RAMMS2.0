using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class KpiGroupL1
    {
        public KpiGroupL1()
        {
            KpiGroupL2 = new HashSet<KpiGroupL2>();
        }

        public int Pk { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public virtual ICollection<KpiGroupL2> KpiGroupL2 { get; set; }
    }
}
