using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiScheduleMaster
    {
        public PiScheduleMaster()
        {
            PiScheduleMasterAsset = new HashSet<PiScheduleMasterAsset>();
        }

        public int Pk { get; set; }
        public int Frequency { get; set; }
        public DateTime BaseDate { get; set; }
        public DateTime? LastScheduleRunDateTime { get; set; }
        public DateTime? NextScheduleRunDateTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<PiScheduleMasterAsset> PiScheduleMasterAsset { get; set; }
    }
}
