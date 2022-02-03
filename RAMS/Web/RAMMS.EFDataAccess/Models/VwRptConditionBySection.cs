using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptConditionBySection
    {
        public int? Year { get; set; }
        public int? Total { get; set; }
        public int? IribadCount { get; set; }
        public int? IripoorCount { get; set; }
        public int? IrifairCount { get; set; }
        public int? IrigoodCount { get; set; }
        public int? RutbadCount { get; set; }
        public int? RutpoorCount { get; set; }
        public int? RutfairCount { get; set; }
        public int? RutgoodCount { get; set; }
        public int? MtdbadCount { get; set; }
        public int? MtdpoorCount { get; set; }
        public int? MtdfairCount { get; set; }
        public int? MtdgoodCount { get; set; }
        public int? PcibadCount { get; set; }
        public int? PcipoorCount { get; set; }
        public int? PcifairCount { get; set; }
        public int? PcigoodCount { get; set; }
        public string Code { get; set; }
    }
}
