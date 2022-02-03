using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class AssetIdreferenceColumn
    {
        public int Pk { get; set; }
        public int AssetIdreferencePk { get; set; }
        public string ColumnNo { get; set; }
        public string FieldName { get; set; }
        public string Abbreviation { get; set; }
        public string Remark { get; set; }
    }
}
