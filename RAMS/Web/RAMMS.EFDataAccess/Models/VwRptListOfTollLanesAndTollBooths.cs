using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptListOfTollLanesAndTollBooths
    {
        public string Region { get; set; }
        public string Section { get; set; }
        public string PlazaCode { get; set; }
        public string PlazaName { get; set; }
        public string TollSystemType { get; set; }
        public string TransactionMode { get; set; }
        public string LaneCode { get; set; }
        public string LaneDirection { get; set; }
        public string Reversible { get; set; }
        public double? LaneSizeM { get; set; }
        public string LaneType { get; set; }
        public string BoothType { get; set; }
        public string BoothNo { get; set; }
    }
}
