using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class NodPhoto
    {
        public int NodPk { get; set; }
        public int RecordIndex { get; set; }
        public string PhotoFileName { get; set; }
        public string PhotoRemarks { get; set; }

        public virtual Nod NodPkNavigation { get; set; }
    }
}
