using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class EmailQueue
    {
        public int Pk { get; set; }
        public short Status { get; set; }
        public string Recipient { get; set; }
        public string Subject { get; set; }
        public string EmailContent { get; set; }
        public int RetryCount { get; set; }
        public string MessageStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CarbonCopy { get; set; }
    }
}
