using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvBuildingCommercial
    {
        public int Pk { get; set; }
        public string RampId { get; set; }
        public string BuildingCode { get; set; }
        public string AssetName { get; set; }
        public string AssetLocation { get; set; }
        public string FloorLevel { get; set; }
        public string AssetLocality { get; set; }
        public string AssetNumbering { get; set; }
        public string Equipment { get; set; }
        public string AssetType { get; set; }
        public int? AssetQty { get; set; }
        public double? AssetArea { get; set; }
        public string AssetCapacity { get; set; }
        public double? AssetWidth { get; set; }
        public double? AssetLength { get; set; }
        public double? AssetHeight { get; set; }
        public string Aging { get; set; }
        public string DrawingNo { get; set; }
        public string DrawingTitle { get; set; }
        public string DrawingFile { get; set; }
        public string ContractorVendorNo { get; set; }
        public string ContractorCompanyName { get; set; }
        public string ContractorRegNo { get; set; }
        public string ContractorRptno { get; set; }
        public string ConsultantVendorNo { get; set; }
        public string ConsultantCompanyName { get; set; }
        public string ConsultantRegNo { get; set; }
        public string ConsultantRptno { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
