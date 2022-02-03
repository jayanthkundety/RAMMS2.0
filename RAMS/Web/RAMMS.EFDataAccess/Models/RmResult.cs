using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class RmResult
    {
        public int Pk { get; set; }
        public int RmSchedulePk { get; set; }
        public int? RecordIndex { get; set; }
        public string FieldName { get; set; }
        public string Value { get; set; }

        public virtual RmSchedule RmSchedulePkNavigation { get; set; }
    }
}
