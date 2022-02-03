using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PitTaskPhoto
    {
        public int PitTaskPk { get; set; }
        public int RecordIndex { get; set; }
        public string PhotoFileName { get; set; }
        public string PhotoRemarks { get; set; }

        public virtual PitTask PitTaskPkNavigation { get; set; }
    }
}
