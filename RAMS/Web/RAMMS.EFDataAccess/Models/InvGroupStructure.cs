using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvGroupStructure
    {
        public InvGroupStructure()
        {
            InvMaster = new HashSet<InvMaster>();
        }

        public int Pk { get; set; }
        public string MainGroup { get; set; }
        public string SubGroup { get; set; }
        public string MainComponent { get; set; }
        public string SubComponent { get; set; }

        public virtual ICollection<InvMaster> InvMaster { get; set; }
    }
}
