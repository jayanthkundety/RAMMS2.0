using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class AuditResultPhoto
    {
        public int Pk { get; set; }
        public int AuditResultPk { get; set; }
        public string FileName { get; set; }
        public double? GeoLat { get; set; }
        public double? GeoLong { get; set; }
        public double? GeoAccuracy { get; set; }
        public string Remarks { get; set; }
        public DateTime TakenDate { get; set; }
        public string TakenBy { get; set; }

        public virtual AuditResult AuditResultPkNavigation { get; set; }
    }
}
