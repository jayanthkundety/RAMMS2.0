using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PitAssetGroup
    {
        public PitAssetGroup()
        {
            PitTaskDamage = new HashSet<PitTaskDamage>();
        }

        public int Pk { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
        public int? GroupStructurePk { get; set; }

        public virtual ICollection<PitTaskDamage> PitTaskDamage { get; set; }
    }
}
