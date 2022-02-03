using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvTunnelTechnical
    {
        public int Pk { get; set; }
        public string AssetName { get; set; }
        public double? AssetKmlocation { get; set; }
        public double? Length { get; set; }
        public double? PanelCount { get; set; }
        public double? Clearance { get; set; }
        public double? AssetLatitude { get; set; }
        public double? AssetLongitude { get; set; }
        public string Owner { get; set; }
        public string YearOpen { get; set; }
        public string MaintainedBy { get; set; }
        public string SourceInfo { get; set; }
        public string GeneralStructureType { get; set; }
        public string Crossing { get; set; }
        public string TunnelType { get; set; }
        public string CarriagewayLayout { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public string PortalDescriptionEnd { get; set; }
        public string PortalDescriptionInternal { get; set; }
        public int InvMasterPk { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
    }
}
