using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmIwFormW1
    {
        public RmIwFormW1()
        {
            RmIwFormW1Image = new HashSet<RmIwFormW1Image>();
            RmIwFormW2 = new HashSet<RmIwFormW2>();
        }

        public int Fw1PkRefNo { get; set; }
        public string Fw1FormhRefNo { get; set; }
        public string Fw1ServPropRefNo { get; set; }
        public string Fw1ReferenceNo { get; set; }
        public string Fw1Division { get; set; }
        public string Fw1Rmu { get; set; }
        public string Fw1ProjectTitle { get; set; }
        public DateTime? Fw1TecmDate { get; set; }
        public string Fw1TecmStatus { get; set; }
        public DateTime? Fw1RmuDate { get; set; }
        public string Fw1RoadCode { get; set; }
        public string Fw1RoadName { get; set; }
        public int? Fw1FrmCh { get; set; }
        public int? Fw1ToCh { get; set; }
        public string Fw1ChainageApprovedIwref { get; set; }
        public string Fw1DetailsOfWork { get; set; }
        public double? Fw1PropDesignDuration { get; set; }
        public double? Fw1PropCompletionPeriod { get; set; }
        public string Fw1RecomondedInstrctedWork { get; set; }
        public double? Fw1PhyWorks { get; set; }
        public double? Fw1GenPrelims { get; set; }
        public double? Fw1ConsulTaxPercent { get; set; }
        public double? Fw1ConsulFee { get; set; }
        public double? Fw1SurvyWorksPercent { get; set; }
        public double? Fw1SurvyWorks { get; set; }
        public double? Fw1SiteInvestPercent { get; set; }
        public double? Fw1SiteInvest { get; set; }
        public double? Fw1OtherCost { get; set; }
        public double? Fw1EstimTotalCost { get; set; }
        public string Fw1ServProvSign { get; set; }
        public string Fw1ServProvName { get; set; }
        public string Fw1ServProvDesig { get; set; }
        public string Fw1ServProvOffice { get; set; }
        public DateTime? Fw1ServProvDate { get; set; }
        public int? Fw1VerifySoBy { get; set; }
        public bool? Fw1VerifySoSign { get; set; }
        public string Fw1VerifySoName { get; set; }
        public string Fw1VerifySoDesig { get; set; }
        public string Fw1VerifySoOffice { get; set; }
        public DateTime? Fw1VerifySoDate { get; set; }
        public int? Fw1ModBy { get; set; }
        public DateTime? Fw1ModDt { get; set; }
        public int? Fw1CrBy { get; set; }
        public DateTime? Fw1CrDt { get; set; }
        public bool Fw1SubmitSts { get; set; }
        public bool? Fw1ActiveYn { get; set; }
        public string Fw1Status { get; set; }
        public string Fw1AuditLog { get; set; }

        public virtual ICollection<RmIwFormW1Image> RmIwFormW1Image { get; set; }
        public virtual ICollection<RmIwFormW2> RmIwFormW2 { get; set; }
    }
}
