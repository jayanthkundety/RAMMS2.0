using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class EpiFinalPhoto
    {
        public int Pk { get; set; }
        public int? EpiNodPk { get; set; }
        public string PhotoFileName { get; set; }
        public string PhotoRemarks { get; set; }
    }
}
