using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvCulvertLanduse
    {
        public int Pk { get; set; }
        public int InvCulvertCatchmentPk { get; set; }
        public string Upstream { get; set; }
        public string Downstream { get; set; }
        public DateTime? InspectionDate { get; set; }
        public string Remark { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}
