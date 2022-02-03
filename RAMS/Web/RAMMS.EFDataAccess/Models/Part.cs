using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class Part
    {
        public int Pk { get; set; }
        public string Sorid { get; set; }
        public string Unit { get; set; }
        public string Sordescription { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
