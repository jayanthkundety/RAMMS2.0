using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptBreakdownAndIssueNod
    {
        public string NodId { get; set; }
        public string NodCategory { get; set; }
        public string Lane { get; set; }
        public double? KmStart { get; set; }
        public double? KmEnd { get; set; }
        public DateTime? DateTimeRaise { get; set; }
        public DateTime? DateTimeResponse { get; set; }
        public DateTime? DateTimeWorkStart { get; set; }
        public DateTime? DateTimeWorkEnd { get; set; }
        public string Status { get; set; }
        public string AssetType { get; set; }
        public string AssetId { get; set; }
        public string AssetLocation { get; set; }
        public string DescriptionOfFailure { get; set; }
        public string ActionTaken { get; set; }
        public string Section { get; set; }
        public string Picture1 { get; set; }
        public string Picture2 { get; set; }
        public string Picture3 { get; set; }
    }
}
