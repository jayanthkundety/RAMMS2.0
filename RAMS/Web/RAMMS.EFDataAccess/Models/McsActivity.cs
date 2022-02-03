using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class McsActivity
    {
        public int Pk { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string TypeOfArea { get; set; }
        public double? StandardTime { get; set; }
        public string Uom { get; set; }
        public string TeamType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string Interval { get; set; }
        public int? McsGroupPk { get; set; }
        public int McsCategoryPk { get; set; }
    }
}
