using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WorkPhoto
    {
        public int WorkPk { get; set; }
        public int RecordIndex { get; set; }
        public string PhotoFileName { get; set; }
        public string PhotoRemarks { get; set; }
    }
}
