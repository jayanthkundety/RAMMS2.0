using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class SorItem
    {
        public SorItem()
        {
            WorkSorItem = new HashSet<WorkSorItem>();
        }

        public int Pk { get; set; }
        public int OracleId { get; set; }
        public int SorCategoryPk { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Uom { get; set; }

        public virtual SorCategory SorCategoryPkNavigation { get; set; }
        public virtual ICollection<WorkSorItem> WorkSorItem { get; set; }
    }
}
