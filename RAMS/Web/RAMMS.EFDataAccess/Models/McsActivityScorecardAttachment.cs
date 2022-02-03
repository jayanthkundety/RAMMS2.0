using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class McsActivityScorecardAttachment
    {
        public int McsActivityScorecardPk { get; set; }
        public int RecordIndex { get; set; }
        public string AttachmentFileName { get; set; }
        public string AttachmentRemarks { get; set; }
    }
}
