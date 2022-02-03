using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvTpisland
    {
        public int Pk { get; set; }
        public string TollPlazaId { get; set; }
        public string IslandType { get; set; }
        public string IslandKerbType { get; set; }
        public double? IslandKerbWidth { get; set; }
        public double? IslandKerbLength { get; set; }
        public double? IslandKerbHeight { get; set; }
        public string IslandNosingType { get; set; }
        public double? IslandNosingQuantity { get; set; }
        public double? IslandNosingWidthMax { get; set; }
        public double? IslandNosingLength { get; set; }
        public double? IslandNosingHeightMax { get; set; }
        public string SideSwipePostType { get; set; }
        public int? SideSwipePostQuantity { get; set; }
        public string SideSwipePostMaterial { get; set; }
        public string SideSwipePostDimension { get; set; }
        public double? SideSwipePostPlateThickness { get; set; }
        public double? SideSwipePostHeight { get; set; }
        public int? RhsrailingQuantity { get; set; }
        public string RhsrailingBeamDimension { get; set; }
        public double? RhsrailingBeamThickness { get; set; }
        public string RhsrailingBeamLength { get; set; }
        public int? RhsrailingPostQuantity { get; set; }
        public string RhsrailingPostSpacing { get; set; }
        public string RhsrailingPostDimension { get; set; }
        public double? RhsrailingPostThickness { get; set; }
        public double? RhsrailingPostHeightaboveBasePlate { get; set; }
    }
}
