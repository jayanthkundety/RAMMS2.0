using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvBridgeUtility
    {
        public int Pk { get; set; }
        public int InvBridgePk { get; set; }
        public string UtilityName { get; set; }
        public string Description { get; set; }
        public double? Quantity { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
        public Guid? TempId { get; set; }
    }
}
