using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class Region
    {
        public Region()
        {
            KpiBucketSizeParam = new HashSet<KpiBucketSizeParam>();
            Section = new HashSet<Section>();
        }

        public int Pk { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Kmfrom { get; set; }
        public double Kmto { get; set; }

        public virtual ICollection<KpiBucketSizeParam> KpiBucketSizeParam { get; set; }
        public virtual ICollection<Section> Section { get; set; }
    }
}
