using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiSchedule
    {
        public int Pk { get; set; }
        public int InvMasterPk { get; set; }
        public DateTime ScheduleInspectionDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public string Remarks { get; set; }
        public DateTime? InspectionStartDate { get; set; }
        public DateTime? InspectionEndDate { get; set; }
        public string InspectedBy { get; set; }
        public double? InspectionLat { get; set; }
        public double? InspectionLng { get; set; }
        public DateTime? ResultUploadedDate { get; set; }
        public string ResultUploadedBy { get; set; }
        public DateTime? ResultModifiedDate { get; set; }
        public string ResultModifiedBy { get; set; }

        public virtual PiResultBridge PiResultBridge { get; set; }
        public virtual PiResultCulvert PiResultCulvert { get; set; }
        public virtual PiResultSlope PiResultSlope { get; set; }
        public virtual PiResultTunnel PiResultTunnel { get; set; }
    }
}
