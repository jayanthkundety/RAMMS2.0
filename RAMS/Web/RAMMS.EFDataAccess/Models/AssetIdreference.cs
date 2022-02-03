using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class AssetIdreference
    {
        public int Pk { get; set; }
        public string FlagCode { get; set; }
        public string FlagType { get; set; }
        public string Example { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string Asset { get; set; }
    }
}
