using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PavementLongTermRollingProgramDetail
    {
        public int Pk { get; set; }
        public int AssetPk { get; set; }
        public int TreatmentYear { get; set; }
        public int BatchYear { get; set; }
        public string TreatmentScheduled { get; set; }
        public string TreatmentActual { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string AssetId { get; set; }
        public int? SectionPk { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
    }
}
