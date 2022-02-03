using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class EpiResult
    {
        public int Pk { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public string Parameter { get; set; }
        public double Performance { get; set; }
        public int SectionPk { get; set; }
        public bool IsCivil { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
