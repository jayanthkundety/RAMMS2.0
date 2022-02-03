using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PitTaskDamageView
    {
        public string AssetGroupCode { get; set; }
        public string AssetGroupName { get; set; }
        public int PitTaskPk { get; set; }
        public int RecordIndex { get; set; }
        public int? DamagePitAssetGroupPk { get; set; }
        public int? DamageQty { get; set; }
        public int? GroupStructurePk { get; set; }
    }
}
