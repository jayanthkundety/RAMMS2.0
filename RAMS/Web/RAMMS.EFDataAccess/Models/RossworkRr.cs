using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class RossworkRr
    {
        public int Pk { get; set; }
        public string RossuniqueId { get; set; }
        public string Drnumber { get; set; }
        public DateTime? Drdate { get; set; }
        public string ContractorCode { get; set; }
        public DateTime? NotificationDate { get; set; }
        public string SectionCode { get; set; }
        public string WorkDescription { get; set; }
        public string WorkMaintenanceType { get; set; }
        public double? Kmfrom { get; set; }
        public double? Kmto { get; set; }
        public string Bound { get; set; }
        public string InitialBq { get; set; }
        public string FinalBq { get; set; }
        public int? WorkPk { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsCancelled { get; set; }
    }
}
