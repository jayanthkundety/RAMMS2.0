using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvTollPlaza
    {
        public int Pk { get; set; }
        public string RampId { get; set; }
        public string PlazaCode { get; set; }
        public string PlazaAbbr { get; set; }
        public string PlazaName { get; set; }
        public string TollSystemType { get; set; }
        public DateTime? OpeningDate { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string ContractNo { get; set; }
        public string ContractTitle { get; set; }
        public DateTime? ContractDate { get; set; }
        public string PackageNo { get; set; }
        public string ConstructionRemarks { get; set; }
    }
}
