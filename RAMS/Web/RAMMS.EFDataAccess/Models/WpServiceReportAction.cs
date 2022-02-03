using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpServiceReportAction
    {
        public int WpServiceReportPk { get; set; }
        public int RecordIndex { get; set; }
        public string ActionTaken { get; set; }
        public string ActionPhoto { get; set; }

        public virtual WpServiceReport WpServiceReportPkNavigation { get; set; }
    }
}
