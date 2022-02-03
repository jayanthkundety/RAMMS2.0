using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class McsActivitySection
    {
        public int Pk { get; set; }
        public int McsActivityPk { get; set; }
        public int SectionPk { get; set; }
    }
}
