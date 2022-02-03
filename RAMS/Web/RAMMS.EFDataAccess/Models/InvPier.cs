using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvPier
    {
        public int Pk { get; set; }
        public double? TotalColumn { get; set; }
        public double? ColumnDiameter { get; set; }
        public double? ColumnWidth { get; set; }
        public double? ColumnLength { get; set; }
        public double? ColumnHeight { get; set; }
        public string Material { get; set; }
        public double? CrossbeamWidth { get; set; }
        public double? CrossbeamLength { get; set; }
        public double? CrossbeamHeight { get; set; }
        public string PierType { get; set; }
        public string FoundationType { get; set; }
    }
}
