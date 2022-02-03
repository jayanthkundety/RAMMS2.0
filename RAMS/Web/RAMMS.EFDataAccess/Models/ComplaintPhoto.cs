using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class ComplaintPhoto
    {
        public int ComplaintPk { get; set; }
        public int RecordIndex { get; set; }
        public string PhotoFileName { get; set; }
        public string PhotoRemarks { get; set; }

        public virtual Complaint ComplaintPkNavigation { get; set; }
    }
}
