using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO
{
    public class FormB1B2HeaderRequestDTO
    {
        public int PkRefNo { get; set; }
        public int? AiPkRefNo { get; set; }
        public string AiAssetId { get; set; }
        public int? AiLocChKm { get; set; }
        public string AiLocChM { get; set; }
        public string AiStrucCode { get; set; }
        public double? AiGpsEasting { get; set; }
        public double? AiGpsNorthing { get; set; }
        public string AiRdCode { get; set; }
        public string AiRdName { get; set; }
        public string AiRiverName { get; set; }
        public string AiDivCode { get; set; }
        public string AiRmuName { get; set; }
        public string AiStrucSuper { get; set; }
        public string AiParapetType { get; set; }
        public string AiBearingType { get; set; }
        public string AiExpanType { get; set; }
        public string AiDeckType { get; set; }
        public string AiAbutType { get; set; }
        public string AiPierType { get; set; }
        public int? AiExpanJointCount { get; set; }
        public double? AiWidthLane { get; set; }
        public double? AiLengthSpan { get; set; }
        public double? AiLength { get; set; }
        public double? AiWidth { get; set; }
        public int? AiLaneCnt { get; set; }
        public int? AiSpanCnt { get; set; }
        public double? AiMedian { get; set; }
        public double? AiWalkway { get; set; }
        public string CInspRefNo { get; set; }
        public int? YearOfInsp { get; set; }
        public DateTime? DtOfInsp { get; set; }
        public int? RecordNo { get; set; }
        public string SerProviderDefObs { get; set; }
        public string AuthDefObs { get; set; }
        public string SerProviderDefGenCom { get; set; }
        public string AuthDefGenCom { get; set; }
        public string SerProviderDefFeedback { get; set; }
        public string AuthDefFeedback { get; set; }
        public int? SerProviderUserId { get; set; }
        public string SerProviderUserName { get; set; }
        public string SerProviderUserDesignation { get; set; }
        public DateTime? SerProviderInsDt { get; set; }
        public string SignpathSerProvider { get; set; }
        public int? UserIdAud { get; set; }
        public string UserNameAud { get; set; }
        public string UserDesignationAud { get; set; }
        public DateTime? DtAud { get; set; }
        public string SignpathAud { get; set; }
        public int? BridgeConditionRat { get; set; }
        public bool? ReqFurtherInv { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public FormB1B2DetailRequestDTO Detail { get; set; }
        public bool IsView { get; set; }
        public string RmuCode { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string Status { get; set; }
        public string DisplayAssetId { get; set; }
        public int SNo { get; set; }

        public int? Month { get; set; }

        public int? Year { get; set; }
    }
}
