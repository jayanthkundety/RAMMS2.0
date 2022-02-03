using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpDippingChainage
    {
        public int WpDippingPk { get; set; }
        public int RecordIndex { get; set; }
        public double? ChainageAt { get; set; }
        public double? Left { get; set; }
        public double? Centre { get; set; }
        public double? Right { get; set; }
        public double? Width { get; set; }
        public string Remarks { get; set; }

        public virtual WpDipping WpDippingPkNavigation { get; set; }
    }
}
