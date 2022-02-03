using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WorkFlow
    {
        public int Pk { get; set; }
        public int AssetGroup { get; set; }
        public bool IsEmergency { get; set; }
        public int Activity { get; set; }
        public int? L1userRole { get; set; }
        public double? L1responseTime { get; set; }
        public int? L2userRole { get; set; }
        public double? L2responseTime { get; set; }
        public int? L3userRole { get; set; }
        public double? L3responseTime { get; set; }
        public int? L4userRole { get; set; }
        public double? L4responseTime { get; set; }
        public int? L5userRole { get; set; }
        public double? L5responseTime { get; set; }
        public int? L6userRole { get; set; }
        public double? L6responseTime { get; set; }
        public int? L7userRole { get; set; }
        public double? L7responseTime { get; set; }
        public int? L8userRole { get; set; }
        public double? L8responseTime { get; set; }
        public int? L9userRole { get; set; }
        public double? L9responseTime { get; set; }
        public int? L10userRole { get; set; }
        public double? L10responseTime { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
