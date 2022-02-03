using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptSummaryOfBridgePeriodicInspectionToMha
    {
        public string BridgeId { get; set; }
        public string BridgeName { get; set; }
        public double? KmLocation { get; set; }
        public double? NoOfSpans { get; set; }
        public DateTime? DateOfInspection { get; set; }
        public string DefectObserved { get; set; }
        public string RecommendedAction { get; set; }
        public string Rating { get; set; }
        public string Owner { get; set; }
    }
}
