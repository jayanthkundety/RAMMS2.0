using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpServiceReportPersonel
    {
        public int RecordIndex { get; set; }
        public int WpServiceReportPk { get; set; }
        public string PersonelStaffNo { get; set; }
        public string PersonelName { get; set; }
        public DateTime? PersonelTimeIn { get; set; }
        public DateTime? PersonelTimeOut { get; set; }

        public virtual WpServiceReport WpServiceReportPkNavigation { get; set; }
    }
}
