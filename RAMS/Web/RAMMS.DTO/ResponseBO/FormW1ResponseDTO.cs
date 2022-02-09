using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormW1ResponseDTO
    {

        public int PkRefNo { get; set; }
        public string FomrhRefNo { get; set; }
        public string ServPropRefNo { get; set; }
        public string ServPropName { get; set; }
        public string ReferenceNo { get; set; }
        public string Division { get; set; }
        public string Rmu { get; set; }
        public string ProjectTitle { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? TecmDate { get; set; }
        public string TecmStatus { get; set; }
        public bool? IsBq { get; set; }
        public bool? IsDrawing { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? RmuDate { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public int? FrmCh { get; set; }
        public int? ToCh { get; set; }      
        public string ChainageApprovedIwref { get; set; }
        public string DetailsOfWork { get; set; }
        public decimal? PropDesignDuration { get; set; }
        public decimal? PropCompletionPeriod { get; set; }
        public string RecommendedInstructedWork { get; set; }
        public string RecommendedByDe { get; set; }
        public string RecommendedStatus { get; set; }
        public decimal? PhyWorks { get; set; }
        public decimal? GenPrelims { get; set; }
        public decimal? ConsulTaxPercent { get; set; }
        public decimal? ConsulFee { get; set; }
        public decimal? SurvyWorksPercent { get; set; }
        public decimal? SurvyWorks { get; set; }
        public decimal? SiteInvestPercent { get; set; }
        public decimal? SiteInvest { get; set; }
        public decimal? OtherCost { get; set; }
        public decimal? EstimTotalCost { get; set; }
        public int? ReportedBy { get; set; }
        public string ReportedName { get; set; }
        public string ReportedDesig { get; set; }
        public bool? ReportedSign { get; set; }
        public DateTime? ReportedDate { get; set; }
        public int? RequestedBy { get; set; }
        public bool? RequestedBySign { get; set; }
        public string RequestedByName { get; set; }
        public string RequestedByDesig { get; set; }
        public string RequestedByOffice { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? RequestedByDate { get; set; }
        public int? Fw1VerifiedBy { get; set; }
        public bool? Fw1VerifiedSign { get; set; }
        public string Fw1VerifiedName { get; set; }
        public string Fw1VerifiedDesig { get; set; }
        public string Fw1VerifiedOffice { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? Fw1VerifiedDate { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }


    }
}
