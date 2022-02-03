using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class AssetFieldDtl
    {
        public string AssetType { get; set; }
        public string Code { get; set; }
        public string FieldName { get; set; }
        public string HdrDisplayName { get; set; }
        public int AssetPkId { get; set; }
    }
}
