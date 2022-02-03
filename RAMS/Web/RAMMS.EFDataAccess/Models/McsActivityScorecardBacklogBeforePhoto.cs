using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class McsActivityScorecardBacklogBeforePhoto
    {
        public int McsActivityScorecardBacklogPk { get; set; }
        public int RecordIndex { get; set; }
        public string PhotoFileName { get; set; }
        public string PhotoRemarks { get; set; }
    }
}
