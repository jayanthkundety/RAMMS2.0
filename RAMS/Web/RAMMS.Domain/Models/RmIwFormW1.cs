using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmIwFormW1
    {
        public int Fw1PkRefNo { get; set; }
        public string Fw1FomrhRefNo { get; set; }
        public string Fw1ServPropRefNo { get; set; }
        public string Fw1DivisionFw1Division { get; set; }
        public byte[] Fw1Rmu { get; set; }
        public DateTime? Fw1RmuDate { get; set; }
        public string Fw1RoadCode { get; set; }
        public string Fw1RoadName { get; set; }
        public int? Fw1FrmCh { get; set; }
        public int? Fw1ToCh { get; set; }
        public string Fw1ChainageApprovedIwref { get; set; }
        public string Fw1DetailsOfWork { get; set; }
        public decimal? Fw1PropDesignDuration { get; set; }
        public decimal? Fw1PropCompletionPeriod { get; set; }
        public string Fw1RecomondedInstrctedWork { get; set; }
        public string Fw1InstructedWorkName { get; set; }
        public string Fw1InstructedWorkDesingation { get; set; }
        public DateTime? Fw1InstructedWorkDate { get; set; }
        public string Fw1InstructedWorkSignature { get; set; }
        public decimal? Fw1PhyWorks { get; set; }
        public decimal? Fw1GenPrelims { get; set; }
        public decimal? Fw1ConsulTaxPercent { get; set; }
        public decimal? Fw1ConsulFee { get; set; }
        public decimal? Fw1SurvyWorksPercent { get; set; }
        public decimal? Fw1SurvyWorks { get; set; }
        public decimal? Fw1SiteInvestPercent { get; set; }
        public decimal? Fw1SiteInvest { get; set; }
        public decimal? Fw1OtherCost { get; set; }
        public decimal? Fw1EstimTotalCost { get; set; }
        public string Fw1ServProvSign { get; set; }
        public string Fw1ServProvName { get; set; }
        public string Fw1ServProvDesig { get; set; }
        public string Fw1ServProvOffice { get; set; }
        public DateTime? Fw1ServProvDate { get; set; }
        public string Fw1VerifySoSign { get; set; }
        public string Fw1VerifySoName { get; set; }
        public string Fw1VerifySoDesig { get; set; }
        public string Fw1VerifySoOffice { get; set; }
        public DateTime? Fw1VerifySoDate { get; set; }
        public int? Fw1ModBy { get; set; }
        public DateTime? Fw1ModDt { get; set; }
        public int? Fw1CrBy { get; set; }
        public DateTime? Fw1CrDt { get; set; }
        public bool? Fw1SubmitSts { get; set; }
        public bool? Fw1ActiveYn { get; set; }
    }
}
