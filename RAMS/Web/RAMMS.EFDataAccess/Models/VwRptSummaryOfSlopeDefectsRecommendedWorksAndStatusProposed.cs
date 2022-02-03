using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class VwRptSummaryOfSlopeDefectsRecommendedWorksAndStatusProposed
    {
        public string SlopeId { get; set; }
        public string Section { get; set; }
        public DateTime? InspectionDate { get; set; }
        public string FurtherInvestigation { get; set; }
        public string RemedialWorks { get; set; }
        public string RoutineMaintenance { get; set; }
        public string RemarksRecommendation { get; set; }
        public string Rating { get; set; }
        public string PriorityRating { get; set; }
        public string OverallCond { get; set; }
        public string HazardRiskRating { get; set; }
    }
}
