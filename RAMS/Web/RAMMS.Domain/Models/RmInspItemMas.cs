using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmInspItemMas
    {
        public RmInspItemMas()
        {
            RmFormCvInsDtl = new HashSet<RmFormCvInsDtl>();
            RmInspItemMasDtl = new HashSet<RmInspItemMasDtl>();
        }

        public int IimPkRefNo { get; set; }
        public string IimInspName { get; set; }
        public int? IimModBy { get; set; }
        public DateTime? IimModDt { get; set; }
        public int? IimCrBy { get; set; }
        public DateTime? IimCrDt { get; set; }
        public bool IimSubmitSts { get; set; }
        public bool? IimActiveYn { get; set; }

        public virtual ICollection<RmFormCvInsDtl> RmFormCvInsDtl { get; set; }
        public virtual ICollection<RmInspItemMasDtl> RmInspItemMasDtl { get; set; }
    }
}
