using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvRowfence
    {
        public int Pk { get; set; }
        public double? AssetKmlocation { get; set; }
        public double? AssetFromKm { get; set; }
        public double? AssetToKm { get; set; }
        public string RampId { get; set; }
        public string AssetInvType { get; set; }
        public double? Height { get; set; }
        public string Layerofbarbwire { get; set; }
        public string LayerofStrainingWire { get; set; }
        public double? Length { get; set; }
        public string PostType { get; set; }
        public double? Quantity { get; set; }
        public double? QuantityofCornerPost { get; set; }
        public double? QuantityofIntermediatePost { get; set; }
        public double? QuantityofStrainingPost { get; set; }
        public string PostMaterial { get; set; }
        public string PostSpacing { get; set; }
        public double? PostSize { get; set; }
        public string PostSizeThickness { get; set; }
        public double? PostSizeWidth { get; set; }
        public double? PostSizeLength { get; set; }
        public double? ConcretePlinth { get; set; }
        public string SizeofConcretePlinth { get; set; }
        public string MaintenanceGate { get; set; }
        public string MaintenanceGateCattletrap { get; set; }
        public double? MaintenanceGateQuantity { get; set; }
        public double? MaintenanceGateSize { get; set; }
        public double? MaintenanceGateHeight { get; set; }
        public double? MaintenanceGateWidth { get; set; }
        public string MaintenanceGateLocation { get; set; }
        public string AssetLocation { get; set; }
        public double? AssetNumber { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
    }
}
