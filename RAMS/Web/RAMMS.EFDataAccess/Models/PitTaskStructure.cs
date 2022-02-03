using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PitTaskStructure
    {
        public PitTaskStructure()
        {
            PitTask = new HashSet<PitTask>();
        }

        public int Pk { get; set; }
        public int ParentPk { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<PitTask> PitTask { get; set; }
    }
}
