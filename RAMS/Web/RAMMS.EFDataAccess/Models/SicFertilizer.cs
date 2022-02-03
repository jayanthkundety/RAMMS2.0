using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class SicFertilizer
    {
        public int SicPk { get; set; }
        public string FertilizerType { get; set; }
        public string CompositionN { get; set; }
        public string CompositionP { get; set; }
        public string CompositionK { get; set; }
        public string CompositionTracers { get; set; }
        public string BagsUsed { get; set; }

        public virtual Sic SicPkNavigation { get; set; }
    }
}
