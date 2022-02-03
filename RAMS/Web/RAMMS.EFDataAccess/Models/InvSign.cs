using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvSign
    {
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string SignPosition { get; set; }
        public double? Width { get; set; }
        public double? PlateThickness { get; set; }
        public double? Height { get; set; }
        public double? Area { get; set; }
        public string RetroReflectivityBackgroundReading { get; set; }
        public string BackFrameMaterial { get; set; }
        public string SheetingType { get; set; }
        public string SheetingManufacturer { get; set; }
        public bool Illumination { get; set; }
        public int Pk { get; set; }
        public double? AssetKmlocation { get; set; }
        public string RetroReflectivityLegendReading { get; set; }

        public virtual InvMaster PkNavigation { get; set; }
    }
}
