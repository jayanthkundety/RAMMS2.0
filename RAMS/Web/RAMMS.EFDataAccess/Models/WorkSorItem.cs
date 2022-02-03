using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WorkSorItem
    {
        public int Pk { get; set; }
        public int? WorkId { get; set; }
        public int? SorItemPk { get; set; }
        public double? Quantity { get; set; }

        public virtual SorItem SorItemPkNavigation { get; set; }
        public virtual Work Work { get; set; }
    }
}
