using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PitTaskUserSign
    {
        public int PitTaskPk { get; set; }
        public int RecordIndex { get; set; }
        public string UserSignName { get; set; }
        public string UserSignPosition { get; set; }
        public DateTime? UserSignDt { get; set; }
        public string UserSignFileName { get; set; }

        public virtual PitTask PitTaskPkNavigation { get; set; }
    }
}
