using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormQa1EqVh
    {
        public int Fqa1evPkRefNo { get; set; }
        public int? Fqa1evFqa1hPkRefNo { get; set; }
        public string Fqa1evType { get; set; }
        public string Fqa1evVNo { get; set; }
        public decimal? Fqa1evCapacity { get; set; }
        public int? Fqa1evUnit { get; set; }
        public string Fqa1evCondition { get; set; }
        public string Fqa1evLabourRemark { get; set; }
        public int? Fqa1evModBy { get; set; }
        public DateTime? Fqa1evModDt { get; set; }
        public int? Fqa1evCrBy { get; set; }
        public DateTime? Fqa1evCrDt { get; set; }

        public virtual RmFormQa1Hdr Fqa1evFqa1hPkRefNoNavigation { get; set; }
    }
}
