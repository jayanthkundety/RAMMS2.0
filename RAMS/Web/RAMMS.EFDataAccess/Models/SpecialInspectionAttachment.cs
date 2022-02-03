using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class SpecialInspectionAttachment
    {
        public int SpecialInspectionPk { get; set; }
        public int RecordIndex { get; set; }
        public string AttachmentFileName { get; set; }
        public string AttachmentRemarks { get; set; }

        public virtual SpecialInspection SpecialInspectionPkNavigation { get; set; }
    }
}
