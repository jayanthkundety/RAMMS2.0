using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpServiceReportPart
    {
        public int WpServiceReportPk { get; set; }
        public int RecordIndex { get; set; }
        public string PartName { get; set; }
        public int? PartQty { get; set; }

        public virtual WpServiceReport WpServiceReportPkNavigation { get; set; }
    }
}
