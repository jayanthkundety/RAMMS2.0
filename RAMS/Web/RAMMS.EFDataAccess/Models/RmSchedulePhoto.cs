using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class RmSchedulePhoto
    {
        public int RmSchedulePk { get; set; }
        public int RecordIndex { get; set; }
        public string PhotoFileName { get; set; }
        public string PhotoRemarks { get; set; }
    }
}
