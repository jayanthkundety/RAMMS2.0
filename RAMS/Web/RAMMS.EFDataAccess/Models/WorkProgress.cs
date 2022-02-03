using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WorkProgress
    {
        public int Pk { get; set; }
        public int WorkPk { get; set; }
        public DateTime FormDate { get; set; }
        public string Status { get; set; }
        public string FormType { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string FormUploadedBy { get; set; }
        public DateTime? FormUploadedDate { get; set; }
        public double? CreatedAtLat { get; set; }
        public double? CreatedAtLng { get; set; }

        public virtual Work WorkPkNavigation { get; set; }
        public virtual WpContractorReview WpContractorReview { get; set; }
        public virtual WpDailyProgress WpDailyProgress { get; set; }
        public virtual WpDipping WpDipping { get; set; }
        public virtual WpInspection WpInspection { get; set; }
        public virtual WpLaneClosure WpLaneClosure { get; set; }
        public virtual WpMaterialDelivery WpMaterialDelivery { get; set; }
        public virtual WpPermitToWork WpPermitToWork { get; set; }
        public virtual WpServiceReport WpServiceReport { get; set; }
        public virtual WpToolboxTalk WpToolboxTalk { get; set; }
        public virtual WpVehicle WpVehicle { get; set; }
        public virtual WpWorkPermit WpWorkPermit { get; set; }
    }
}
