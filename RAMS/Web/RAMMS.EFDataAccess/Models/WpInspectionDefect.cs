using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpInspectionDefect
    {
        public int WpInspectionPk { get; set; }
        public int RecordIndex { get; set; }
        public string DefectSubject { get; set; }
        public DateTime? DefectRectifiedDate { get; set; }

        public virtual WpInspection WpInspectionPkNavigation { get; set; }
    }
}
