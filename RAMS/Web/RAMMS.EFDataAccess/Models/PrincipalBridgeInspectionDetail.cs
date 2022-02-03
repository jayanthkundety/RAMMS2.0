using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PrincipalBridgeInspectionDetail
    {
        public int Pk { get; set; }
        public int? InvMasterPk { get; set; }
        public DateTime? InspectionScheduleDate { get; set; }
        public DateTime? InspectionActualDate { get; set; }
        public string InspectionBy { get; set; }
        public string Remarks { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string InvMasterId { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public int? SectionPk { get; set; }
    }
}
