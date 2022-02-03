using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class SicUploadFile
    {
        public int Pk { get; set; }
        public string Ipaddress { get; set; }
        public string Remark { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public string FileName { get; set; }
    }
}
