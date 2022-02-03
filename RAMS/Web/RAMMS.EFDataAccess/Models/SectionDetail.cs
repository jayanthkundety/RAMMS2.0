using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class SectionDetail
    {
        public int Pk { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Kmfrom { get; set; }
        public double Kmto { get; set; }
        public int RegionPk { get; set; }
        public string Route { get; set; }
        public string SectionRouteFrom { get; set; }
        public string SectionRouteTo { get; set; }
        public string AssetOwner { get; set; }
        public string Network { get; set; }
        public bool? IsActive { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
    }
}
