using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class TempUpload
    {
        public int Pk { get; set; }
        public int RecordIndex { get; set; }
        public string FileName { get; set; }
        public string Remark { get; set; }
        public Guid TempId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
