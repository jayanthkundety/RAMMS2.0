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
        public string Fw1ServPropName { get; set; }
        public string Fw1ServOffice { get; set; }
        public string Fw1ServAddress1 { get; set; }
        public string Fw1ServAddress2 { get; set; }
        public string Fw1ServAddress3 { get; set; }
        public string Fw1ServPhone { get; set; }
        public string Fw1ServFax { get; set; }
        public string Fw1ReferenceNo { get; set; }
        public string Fw1Division { get; set; }
        public string Fw1Rmu { get; set; }
        public string Fw1ProjectTitle { get; set; }
        public DateTime? Fw1TecmDate { get; set; }
        public string Fw1TecmStatus { get; set; }
        public bool? Fw1IsBq { get; set; }
        public bool? Fw1IsDrawing { get; set; }
        public DateTime? Fw1RmuDate { get; set; }
        public string Fw1RoadCode { get; set; }
        public string Fw1RoadName { get; set; }
        public int? Fw1FrmChKm { get; set; }
        public int? Fw1FrmChM { get; set; }
        public int? Fw1ToChKm { get; set; }
        public int? Fw1ToChM { get; set; }
        public string Fw1ChainageApprovedIwref { get; set; }
        public string Fw1DetailsOfWork { get; set; }
        public double? Fw1PropDesignDuration { get; set; }
        public double? Fw1PropCompletionPeriod { get; set; }
        public string Fw1RecommendedInstructedWork { get; set; }
        public string Fw1RecommendedByDe { get; set; }
        public string Fw1RecommendedStatus { get; set; }
        public double? Fw1PhyWorks { get; set; }
        public double? Fw1GenPrelims { get; set; }
        public double? Fw1ConsulTaxPercent { get; set; }
        public double? Fw1ConsulFee { get; set; }
        public double? Fw1SurvyWorksPercent { get; set; }
        public double? Fw1SurvyWorks { get; set; }
        public double? Fw1SiteInvestPercent { get; set; }
        public double? Fw1SiteInvest { get; set; }
        public string Fw1OtherCostLabel { get; set; }
        public double? Fw1OtherCost { get; set; }
        public double? Fw1EstimTotalCost { get; set; }
        public int? Fw1ReportedBy { get; set; }
        public string Fw1ReportedName { get; set; }
        public string Fw1ReportedDesig { get; set; }
        public bool? Fw1ReportedSign { get; set; }
        public DateTime? Fw1ReportedDate { get; set; }
        public int? Fw1RequestedBy { get; set; }
        public bool? Fw1RequestedBySign { get; set; }
        public string Fw1RequestedByName { get; set; }
        public string Fw1RequestedByDesig { get; set; }
        public string Fw1RequestedByOffice { get; set; }
        public DateTime? Fw1RequestedByDate { get; set; }
        public int? Fw1VerifiedBy { get; set; }
        public bool? Fw1VerifiedSign { get; set; }
        public string Fw1VerifiedName { get; set; }
        public string Fw1VerifiedDesig { get; set; }
        public string Fw1VerifiedOffice { get; set; }
        public DateTime? Fw1VerifiedDate { get; set; }
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
