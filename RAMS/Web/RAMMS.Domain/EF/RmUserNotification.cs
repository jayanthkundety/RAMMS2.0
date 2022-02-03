using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmUserNotification
    {
        public long RmNotPk { get; set; }
        public string RmNotUrl { get; set; }
        public string RmNotMessage { get; set; }
        public string RmNotGroup { get; set; }
        public string RmNotUserId { get; set; }
        public DateTime RmNotOn { get; set; }
        public string RmNotViewed { get; set; }
        public string RmNotCrBy { get; set; }
    }
}
