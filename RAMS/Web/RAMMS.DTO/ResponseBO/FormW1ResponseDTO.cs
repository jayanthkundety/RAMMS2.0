using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormW1ResponseDTO
    { 
        public int PkRefNo { get; set; }
        public string FormhRefNo { get; set; }
        public string ServPropRefNo { get; set; }
        public string ServPropName { get; set; }
        public string ServAddress1 { get; set; }
        public string ServAddress2 { get; set; }
        public string ServAddress3 { get; set; }
        public string ServPhone { get; set; }
        public string ServFax { get; set; }
        public string IwRefNo { get; set; }
        public string DivnCode { get; set; }
        public string RmuCode { get; set; }
        public string SecCode { get; set; }
        public string ProjectTitle { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? InitialProposedDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? TecmDt { get; set; }
        public bool? IsBq { get; set; }
        public bool? IsDrawing { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Dt { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public int? Ch { get; set; }
        public int? ChDeci { get; set; }
        public string DetailsOfWork { get; set; }
        public double? PropDesignDuration { get; set; }
        public double? PropCompletionPeriod { get; set; }
        public bool RecomdYn { get; set; }
        public bool RecomdBydeYn { get; set; }
        public short? RecomdType { get; set; }
        public double? PhyWorksAmt { get; set; }
        public double? GenPrelimsAmt { get; set; }
        public double? ConsulTaxPercent { get; set; }
        public double? ConsulFeeAmt { get; set; }
        public double? SurvyWorksPercent { get; set; }
        public double? SurvyWorksAmt { get; set; }
        public double? SiteInvestPercent { get; set; }
        public double? SiteInvestAmt { get; set; }
        public string OtherCostLabel { get; set; }
        public double? OtherCostAmt { get; set; }
        public double? EstimTotalCostAmt { get; set; }
        public int? UseridRep { get; set; }
        public string UsernameRep { get; set; }
        public string DesignationRep { get; set; }
        public bool SignRep { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DtRep { get; set; }
        public int? UseridReq { get; set; }
        public bool SignReq { get; set; }
        public string UsernameReq { get; set; }
        public string DesignationReq { get; set; }
        public string OfficeReq { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DtReq { get; set; }
        public int? UseridVer { get; set; }
        public bool SignVer { get; set; }
        public string UsernameVer { get; set; }
        public string DesignationVer { get; set; }
        public string OfficeVer { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DtVer { get; set; }
        public int? ModBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }

    }
}
