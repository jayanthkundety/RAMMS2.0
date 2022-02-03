using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class EmergencyPhoto
    {
        public int EmergencyPk { get; set; }
        public int RecordIndex { get; set; }
        public string PhotoFileName { get; set; }
        public string PhotoRemarks { get; set; }

        public virtual Emergency EmergencyPkNavigation { get; set; }
    }
}
