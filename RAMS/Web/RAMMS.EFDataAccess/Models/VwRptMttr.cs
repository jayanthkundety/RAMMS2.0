using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptMttr
    {
        public string Customer { get; set; }
        public string SectionCode { get; set; }
        public string AssetSubGroup { get; set; }
        public string Nod { get; set; }
        public string PlusITemsNo { get; set; }
        public DateTime? ReceiptOfComplaintNodIncidentDateTimeDate { get; set; }
        public TimeSpan? ReceiptOfComplaintNodIncidentDateTimeTime { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public TimeSpan? ActualStartTime { get; set; }
        public DateTime? ActualCompletionDate { get; set; }
        public TimeSpan? ActualCompletionTime { get; set; }
        public decimal? ResponseTimeHr { get; set; }
        public decimal? RepairTimeHr { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
    }
}
