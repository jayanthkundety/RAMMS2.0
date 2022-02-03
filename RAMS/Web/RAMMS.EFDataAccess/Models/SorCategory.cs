using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class SorCategory
    {
        public SorCategory()
        {
            SorItem = new HashSet<SorItem>();
        }

        public int Pk { get; set; }
        public int OracleId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<SorItem> SorItem { get; set; }
    }
}
