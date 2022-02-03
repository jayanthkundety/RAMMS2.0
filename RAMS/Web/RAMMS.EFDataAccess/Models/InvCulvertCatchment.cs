using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvCulvertCatchment
    {
        public int Pk { get; set; }
        public int InvMasterPk { get; set; }
        public string ReportReferenceNumber { get; set; }
        public string CatchmentArea { get; set; }
        public string DesignCapacity { get; set; }
        public string UpstreamLanduse { get; set; }
        public string DownstreamLanduse { get; set; }
        public string LanduseOriginal { get; set; }
        public string HighWaterLevel { get; set; }
        public string Remark { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}
