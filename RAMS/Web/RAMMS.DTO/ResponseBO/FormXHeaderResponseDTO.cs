using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;

namespace RAMMS.DTO.RequestBO
{
    public class FormXHeaderResponseDTO
    {
        public FormXHeaderResponseDTO()
        {
         
        }

        public string SmartInputValue { get; set; }

        [MapTo("FxhPkRefNo")] // table primary key
        public int? No { get; set; }

        [MapTo("FxhFddPkRefNo")]  // Form D Pk id ref as fK id
        public int? FddNo { get; set; }

        [MapTo("FxhContNo")]
        public string ContNo { get; set; }  // Contract no

        [MapTo("FxhDate")]
        public DateTime? Date { get; set; }

        [MapTo("FxhTime")]
        public TimeSpan? Time { get; set; }

        [MapTo("FxhName")]
        public string Name { get; set; }

        [MapTo("FxhHandPh")]
        public string HandPhone { get; set; }

        [MapTo("FxhWorkPh")]
        public string WorkPhone { get; set; }

        [MapTo("FxhEmailId")]
        public string EmailId { get; set; }

        [MapTo("FxhLoc")]
        public string Location { get; set; }

        [MapTo("FxhLocReportedDesc")]
        public string LocRepDesc { get; set; }


        [MapTo("FxhDesc")]
        public string Description { get; set; }

        [MapTo("FxhRmu")]
        public string Rmu { get; set; }

        [MapTo("FxhUsernameAttnTo")]
        public string AttentionTo { get; set; }

        [MapTo("FxhComments")]
        public string Comments { get; set; }

        [MapTo("FxhAssgnWrk")]
        public string AssignWork { get; set; }

        [MapTo("FxhActMainCode")]
        public int? ActMainCode { get; set; }

        [MapTo("FxhActMainName")]
        public string ActMainName { get; set; }

        [MapTo("FxhEstDays")]
        public string EstDays { get; set; }

        [MapTo("FxhWrkSc")]
        public DateTime? WorkSc { get; set; }

        [MapTo("FxhWrkCmpld")]
        public DateTime? WorkCompleted { get; set; }

        [MapTo("FxhClsd")]//Case closed on
        public DateTime? CaseClosedOn { get; set; }

        [MapTo("FxhSignPrp")] // Reported by 
        public string ReportedBySign { get; set; }

        [MapTo("FxhUseridPrp")] //Reported by
        public int? ReportedByUserId { get; set; }

        [MapTo("FxhUsernamePrp")]
        public string ReportedByUsername { get; set; }

        [MapTo("FxhDesignationPrp")]
        public string ReportedByDesignation { get; set; }

        [MapTo("FxhDtPrp")]
        public DateTime? DateReported { get; set; }

        [MapTo("FxhSignVer")]
        public string SignVer { get; set; }

        [MapTo("FxhUseridVer")]
        public int? UseridVer { get; set; }

        [MapTo("FxhUsernameVer")]
        public string UsernameVer { get; set; }

        [MapTo("FxhDesignationVer")]
        public string DesignationVer { get; set; }

        [MapTo("FxhDtVer")]
        public DateTime? DtVer { get; set; }

        [MapTo("FxhSignVet")]
        public string SignVet { get; set; }

        [MapTo("FxhUseridVet")]
        public int? UseridVet { get; set; }

        [MapTo("FxhUsernameVet")]
        public string UsernameVet { get; set; }

        [MapTo("FxhDesignationVet")]
        public string DesignationVet { get; set; }

        [MapTo("FxhDtVet")]
        public DateTime? DtVet { get; set; }

        [MapTo("FxhRemarks")]
        public string Remarks { get; set; }

        [MapTo("FxhModBy")]
        public string ModifeidBy { get; set; }

        [MapTo("FxhModDt")]
        public DateTime? ModifiedDate { get; set; }

        [MapTo("FxhCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("FxhCrDt")]
        public DateTime? CreatedDate { get; set; }

        [MapTo("FxhSubmitSts")]
        public bool SubmitSts { get; set; }

        [MapTo("FxhActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo("FxhModComType")]
        public string ModComType { get; set; }

        [MapTo("FxhModComDesc")]
        public string ModComDesc { get; set; }

        [MapTo("FxhModComUpload")]
        public string ModComUpload { get; set; }

        [MapTo("FxhRoadCode")]
        public string RoadCode { get; set; }

        [MapTo("FxhActSubCode")]
        public string ActSubCode { get; set; }

        [MapTo("FxhActSubName")]
        public string ActSubName { get; set; }

        [MapTo("FxhEstDate")]
        public DateTime? EstDate { get; set; }

        [MapTo("FxhUseridAssgn")]
        public int? UseridAssgn { get; set; }

        [MapTo("FxhUsernameAssgn")]
        public string UsernameAssgn { get; set; }

        [MapTo("FxhDtAssgn")]
        public DateTime? DtAssgn { get; set; }

        [MapTo("FxhDtJkrSent")]
        public DateTime? DtJkrSent { get; set; }

        [MapTo("FxhDtJkrRcvdFrm")]
        public DateTime? DtJkrRcvdFrm { get; set; }

        [MapTo("FxhJkrRemarks")]
        public string JkrRemarks { get; set; }

        [MapTo("FxhStsJkr")]
        public string StsJkr { get; set; }

        [MapTo("FxhSection")]
        public string Section { get; set; }

        [MapTo("FxhRefId")]
        public string ReferenceId { get; set; }

        [MapTo("FxhSignSchdVer")]
        public string SignSchdVer { get; set; }

        [MapTo("FxhUseridSchdVer")]
        public int? UseridSchdVer { get; set; }

        [MapTo("FxhUsernameSchdVer")]
        public string UsernameSchdVer { get; set; }

        [MapTo("FxhDesignationSchdVer")]
        public string DesignationSchdVer { get; set; }

        [MapTo("FxhDtSchdVer")]
        public DateTime? DtSchdVer { get; set; }

        [MapTo("FxhRoadName")]
        public string RoadName { get; set; }

        [MapTo("FxhUseridAttnTo")]
        public int? UseridAttnTo { get; set; }

        //public string sortOrder { get; set; }
        //public string currentFilter { get; set; }
        //public string searchString { get; set; }
        //public int? Page_No { get; set; }
        //public int? pageSize { get; set; }

    }
}
