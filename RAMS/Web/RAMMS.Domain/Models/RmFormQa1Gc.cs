using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormQa1Gc
    {
        public int Fqa1gcPkRefNo { get; set; }
        public int? Fqa1gcFqa1hPkRefNo { get; set; }
        public bool? Fqa1gcWhs { get; set; }
        public string Fqa1gcWhsRemark { get; set; }
        public string Fqa1gcWhsReason { get; set; }
        public bool? Fqa1gcWis { get; set; }
        public string Fqa1gcWisRemark { get; set; }
        public string Fqa1gcWisReason { get; set; }
        public bool? Fqa1gcWius { get; set; }
        public bool? Fqa1gcWiusMat { get; set; }
        public string Fqa1gcWiusMatRemark { get; set; }
        public string Fqa1gcWiusMatReason { get; set; }
        public bool? Fqa1gcWiusEqp { get; set; }
        public string Fqa1gcWiusEqpReason { get; set; }
        public string Fqa1gcWiusEqpRemark { get; set; }
        public bool? Fqa1gcWiusWrk { get; set; }
        public string Fqa1gcWiusWrkRemark { get; set; }
        public string Fqa1gcWiusWrkReason { get; set; }
        public bool Fqa1gcActiveYn { get; set; }
        public int? Fqa1gcModBy { get; set; }
        public DateTime? Fqa1gcModDt { get; set; }
        public int? Fqa1gcCrBy { get; set; }
        public DateTime? Fqa1gcCrDt { get; set; }

        public virtual RmFormQa1Hdr Fqa1gcFqa1hPkRefNoNavigation { get; set; }
    }
}
