using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class RmFormCategory
    {
        public RmFormCategory()
        {
            RmForm = new HashSet<RmForm>();
        }

        public int Pk { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RmForm> RmForm { get; set; }
    }
}
