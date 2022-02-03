using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class NodAttachment
    {
        public int NodPk { get; set; }
        public int RecordIndex { get; set; }
        public string AttachmentFileName { get; set; }
        public string AttachmentRemarks { get; set; }
    }
}
