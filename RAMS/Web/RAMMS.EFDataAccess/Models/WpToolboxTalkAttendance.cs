using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpToolboxTalkAttendance
    {
        public int WpToolboxTalkPk { get; set; }
        public int RecordIndex { get; set; }
        public string FileName { get; set; }

        public virtual WpToolboxTalk WpToolboxTalkPkNavigation { get; set; }
    }
}
