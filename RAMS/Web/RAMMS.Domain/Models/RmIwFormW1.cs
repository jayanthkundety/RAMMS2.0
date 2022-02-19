using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmIwFormW1
    {
        public RmIwFormW1()
        {
            RmIwFormW2 = new HashSet<RmIwFormW2>();
            RmIwFormWc = new HashSet<RmIwFormWc>();
            RmIwformImage = new HashSet<RmIwformImage>();
        }

        public int Fw1PkRefNo { get; set; }
        public string Fw1FormhRefNo { get; set; }
        public string Fw1ServPropRefNo { get; set; }
        public string Fw1ServPropName { get; set; }
        public string Fw1ServAddress1 { get; set; }
        public string Fw1ServAddress2 { get; set; }
        public string Fw1ServAddress3 { get; set; }
        public string Fw1ServPhone { get; set; }
        public string Fw1ServFax { get; set; }
        public string Fw1IwRefNo { get; set; }
        public string Fw1DivnCode { get; set; }
        public string Fw1RmuCode { get; set; }
        public string Fw1SecCode { get; set; }
        public string Fw1ProjectTitle { get; set; }
        public DateTime? Fw1InitialProposedDate { get; set; }
        public DateTime? Fw1TecmDt { get; set; }
        public bool? Fw1IsBq { get; set; }
        public bool? Fw1IsDrawing { get; set; }
        public DateTime? Fw1Dt { get; set; }
        public string Fw1RoadCode { get; set; }
        public string Fw1RoadName { get; set; }
        public int? Fw1Ch { get; set; }
        public int? Fw1ChDeci { get; set; }
        public string Fw1DetailsOfWork { get; set; }
        public double? Fw1PropDesignDuration { get; set; }
        public double? Fw1PropCompletionPeriod { get; set; }
        public bool? Fw1RecomdYn { get; set; }
        public bool? Fw1RecomdBydeYn { get; set; }
        public short? Fw1RecomdType { get; set; }
        public double? Fw1PhyWorksAmt { get; set; }
        public double? Fw1GenPrelimsAmt { get; set; }
        public double? Fw1ConsulTaxPercent { get; set; }
        public double? Fw1ConsulFeeAmt { get; set; }
        public double? Fw1SurvyWorksPercent { get; set; }
        public double? Fw1SurvyWorksAmt { get; set; }
        public double? Fw1SiteInvestPercent { get; set; }
        public double? Fw1SiteInvestAmt { get; set; }
        public string Fw1OtherCostLabel { get; set; }
        public double? Fw1OtherCostAmt { get; set; }
        public double? Fw1EstimTotalCostAmt { get; set; }
        public int? Fw1UseridRep { get; set; }
        public string Fw1UsernameRep { get; set; }
        public string Fw1DesignationRep { get; set; }
        public bool? Fw1SignRep { get; set; }
        public DateTime? Fw1DtRep { get; set; }
        public int? Fw1UseridReq { get; set; }
        public bool? Fw1SignReq { get; set; }
        public string Fw1UsernameReq { get; set; }
        public string Fw1DesignationReq { get; set; }
        public string Fw1OfficeReq { get; set; }
        public DateTime? Fw1DtReq { get; set; }
        public int? Fw1UseridVer { get; set; }
        public bool? Fw1SignVer { get; set; }
        public string Fw1UsernameVer { get; set; }
        public string Fw1DesignationVer { get; set; }
        public string Fw1OfficeVer { get; set; }
        public DateTime? Fw1DtVer { get; set; }
        public int? Fw1ModBy { get; set; }
        public DateTime? Fw1ModDt { get; set; }
        public int? Fw1CrBy { get; set; }
        public DateTime? Fw1CrDt { get; set; }
        public bool Fw1SubmitSts { get; set; }
        public bool Fw1ActiveYn { get; set; }
        public string Fw1Status { get; set; }
        public string Fw1AuditLog { get; set; }

        public virtual ICollection<RmIwFormW2> RmIwFormW2 { get; set; }
        public virtual ICollection<RmIwFormWc> RmIwFormWc { get; set; }
        public virtual ICollection<RmIwformImage> RmIwformImage { get; set; }
    }
}
