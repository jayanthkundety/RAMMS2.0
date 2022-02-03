using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptListOfTollPlaza
    {
        public string TollPlazaId { get; set; }
        public string Region { get; set; }
        public string Section { get; set; }
        public string Route { get; set; }
        public double? KmLocation { get; set; }
        public string LocalityOfFeature { get; set; }
        public string Abbr { get; set; }
        public string PlazaName { get; set; }
        public string PlazaCode { get; set; }
        public string TollSystemType { get; set; }
        public DateTime? OpeningDate { get; set; }
        public string Owner { get; set; }
    }
}
