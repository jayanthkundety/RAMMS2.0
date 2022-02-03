using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PavementLongTermRollingProgram
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
    }
}
