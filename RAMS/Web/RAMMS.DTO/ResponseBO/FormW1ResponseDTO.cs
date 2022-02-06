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
        public string IWReferenceNo { get; set; }
        public string DivisionFw1Division { get; set; }
        public string Rmu { get; set; }
        public DateTime? RmuDate { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public int? FrmCh { get; set; }
        public int? ToCh { get; set; }
        public string ProjectTitle { get; set; }
        public DateTime? TECMDate { get; set; }
        public string TECMStatus { get; set; }
        public string ChainageApprovedIwref { get; set; }
        public string DetailsOfWork { get; set; }
        public decimal? PropDesignDuration { get; set; }
        public decimal? PropCompletionPeriod { get; set; }
        public string RecomondedInstrctedWork { get; set; }
        public string InstructedWorkName { get; set; }
        public string InstructedWorkDesingation { get; set; }
        public DateTime? InstructedWorkDate { get; set; }
        public string InstructedWorkSignature { get; set; }
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
        public string ServProvSign { get; set; }
        public string ServProvName { get; set; }
        public string ServProvDesig { get; set; }
        public string ServProvOffice { get; set; }
        public DateTime? ServProvDate { get; set; }
        public string VerifySOBy { get; set; }
        public string VerifySoSign { get; set; }
        public string VerifySoName { get; set; }
        public string VerifySoDesig { get; set; }
        public string VerifySoOffice { get; set; }
        public DateTime? VerifySoDate { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool? SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }

    }
}
