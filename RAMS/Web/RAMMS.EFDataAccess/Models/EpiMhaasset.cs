using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class EpiMhaasset
    {
        public int Pk { get; set; }
        public int? AssetId { get; set; }
        public string AssetRefNum { get; set; }
        public int? RamsInvMasterPk { get; set; }
        public string MainCategoryCode { get; set; }
        public string SubCategoryCode { get; set; }
    }
}
