using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PitTaskDamage
    {
        public int PitTaskPk { get; set; }
        public int RecordIndex { get; set; }
        public int? DamagePitAssetGroupPk { get; set; }
        public int? DamageQty { get; set; }

        public virtual PitAssetGroup DamagePitAssetGroupPkNavigation { get; set; }
        public virtual PitTask PitTaskPkNavigation { get; set; }
    }
}
