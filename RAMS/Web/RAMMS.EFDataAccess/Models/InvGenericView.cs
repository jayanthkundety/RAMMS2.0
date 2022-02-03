using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvGenericView
    {
        public string SectionCode { get; set; }
        public int Pk { get; set; }
        public string Id { get; set; }
        public int FeaturePk { get; set; }
        public int GroupStructurePk { get; set; }
        public string BaseType { get; set; }
    }
}
