using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class NodConversion
    {
        public int NodPk { get; set; }
        public int SourcePk { get; set; }
        public int SourceType { get; set; }
    }
}
