using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpMaterialDeliveryItem
    {
        public int WpMaterialDeliveryPk { get; set; }
        public int RecordIndex { get; set; }
        public string TruckNo { get; set; }
        public string ArriveTime { get; set; }
        public double? ArriveTemp { get; set; }
        public double? LayTemp { get; set; }
        public double? CompactTemp { get; set; }
        public double? Qty { get; set; }
        public string Remarks { get; set; }

        public virtual WpMaterialDelivery WpMaterialDeliveryPkNavigation { get; set; }
    }
}
