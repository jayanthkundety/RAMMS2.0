using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormMAuditActivities
    {
        public int FmaaPkRefNo { get; set; }
        public int? FmaaCategory { get; set; }
        public string FmaaActivityCode { get; set; }
        public string FmaaActivityName { get; set; }
        public string FmaaActivityFor { get; set; }
        public bool? FmaaIsEditable { get; set; }
        public int? FmaaModBy { get; set; }
        public DateTime? FmaaModDt { get; set; }
        public int? FmaaCrBy { get; set; }
        public DateTime? FmaaCrDt { get; set; }
    }
}
