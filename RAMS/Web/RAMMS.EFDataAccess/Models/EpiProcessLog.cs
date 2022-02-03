using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class EpiProcessLog
    {
        public int Pk { get; set; }
        public int EpiNodPk { get; set; }
        public DateTime ProcessDateTime { get; set; }
        public string Description { get; set; }
    }
}
