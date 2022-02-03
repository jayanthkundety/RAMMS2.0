using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class FormB1B2HeaderRequestDTO
    {
        [MapTo("FbrihPkRefNo")] public int PkRefNo { get; set; }
        [MapTo("FbrihAiPkRefNo")] public int? AiPkRefNo { get; set; }
        [MapTo("FbrihAiAssetId")] public string AiAssetId { get; set; }
        [MapTo("FbrihAiLocChKm")] public int? AiLocChKm { get; set; }
        [MapTo("FbrihAiLocChM")] public string AiLocChM { get; set; }
        [MapTo("FbrihAiStrucCode")] public string AiStrucCode { get; set; }
        [MapTo("FbrihAiGpsEasting")] public double? AiGpsEasting { get; set; }
        [MapTo("FbrihAiGpsNorthing")] public double? AiGpsNorthing { get; set; }
        [MapTo("FbrihAiRdCode")] public string AiRdCode { get; set; }
        [MapTo("FbrihAiRdName")] public string AiRdName { get; set; }
        [MapTo("FbrihAiRiverName")] public string AiRiverName { get; set; }
        [MapTo("FbrihAiDivCode")] public string AiDivCode { get; set; }
        [MapTo("FbrihAiRmuName")] public string AiRmuName { get; set; }
        [MapTo("FbrihAiStrucSuper")] public string AiStrucSuper { get; set; }
        [MapTo("FbrihAiParapetType")] public string AiParapetType { get; set; }
        [MapTo("FbrihAiBearingType")] public string AiBearingType { get; set; }
        [MapTo("FbrihAiExpanType")] public string AiExpanType { get; set; }
        [MapTo("FbrihAiDeckType")] public string AiDeckType { get; set; }
        [MapTo("FbrihAiAbutType")] public string AiAbutType { get; set; }
        [MapTo("FbrihAiPierType")] public string AiPierType { get; set; }
        [MapTo("FbrihAiExpanJointCount")] public int? AiExpanJointCount { get; set; }
        [MapTo("FbrihAiWidthLane")] public double? AiWidthLane { get; set; }
        [MapTo("FbrihAiLengthSpan")] public double? AiLengthSpan { get; set; }
        [MapTo("FbrihAiLength")] public double? AiLength { get; set; }
        [MapTo("FbrihAiWidth")] public double? AiWidth { get; set; }
        [MapTo("FbrihAiLaneCnt")] public int? AiLaneCnt { get; set; }
        [MapTo("FbrihAiSpanCnt")] public int? AiSpanCnt { get; set; }
        [MapTo("FbrihAiMedian")] public double? AiMedian { get; set; }
        [MapTo("FbrihAiWalkway")] public double? AiWalkway { get; set; }
        [MapTo("FbrihCInspRefNo")] public string CInspRefNo { get; set; }
        [MapTo("FbrihYearOfInsp")] public int? YearOfInsp { get; set; }
        [MapTo("FbrihDtOfInsp")] public DateTime? DtOfInsp { get; set; }
        [MapTo("FbrihRecordNo")] public int? RecordNo { get; set; }
        [MapTo("FbrihSerProviderDefObs")] public string SerProviderDefObs { get; set; }
        [MapTo("FbrihAuthDefObs")] public string AuthDefObs { get; set; }
        [MapTo("FbrihSerProviderDefGenCom")] public string SerProviderDefGenCom { get; set; }
        [MapTo("FbrihAuthDefGenCom")] public string AuthDefGenCom { get; set; }
        [MapTo("FbrihSerProviderDefFeedback")] public string SerProviderDefFeedback { get; set; }
        [MapTo("FbrihAuthDefFeedback")] public string AuthDefFeedback { get; set; }
        [MapTo("FbrihSerProviderUserId")] public int? SerProviderUserId { get; set; }
        [MapTo("FbrihSerProviderUserName")] public string SerProviderUserName { get; set; }
        [MapTo("FbrihSerProviderUserDesignation")] public string SerProviderUserDesignation { get; set; }
        [MapTo("FbrihSerProviderInsDt")] public DateTime? SerProviderInsDt { get; set; }
        [MapTo("FbrihSignpathSerProvider")] public string SignpathSerProvider { get; set; }
        [MapTo("FbrihUserIdAud")] public int? UserIdAud { get; set; }
        [MapTo("FbrihUserNameAud")] public string UserNameAud { get; set; }
        [MapTo("FbrihUserDesignationAud")] public string UserDesignationAud { get; set; }
        [MapTo("FbrihDtAud")] public DateTime? DtAud { get; set; }
        [MapTo("FbrihSignpathAud")] public string SignpathAud { get; set; }
        [MapTo("FbrihBridgeConditionRat")] public int? BridgeConditionRat { get; set; }
        [MapTo("FbrihReqFurtherInv")] public bool? ReqFurtherInv { get; set; }
        [MapTo("FbrihModBy")] public int? ModBy { get; set; }
        [MapTo("FbrihModDt")] public DateTime? ModDt { get; set; }
        [MapTo("FbrihCrBy")] public int? CrBy { get; set; }
        [MapTo("FbrihCrDt")] public DateTime? CrDt { get; set; }
        [MapTo("FbrihSubmitSts")] public bool SubmitSts { get; set; }
        [MapTo("FbrihActiveYn")] public bool ActiveYn { get; set; }
        [MapTo("FbrihStatus")] public string Status { get; set; }
        [MapTo("FbrihAuditLog")] public string AuditLog { get; set; }
        public FormB1B2DetailRequestDTO Detail { get; set; }
        public bool IsView { get; set; }
        public string RmuCode { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string DisplayAssetId { get; set; }
    }
}
