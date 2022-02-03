using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormB1b2BrInsHdr
    {
        public RmFormB1b2BrInsHdr()
        {
            RmFormB1b2BrInsDtl = new HashSet<RmFormB1b2BrInsDtl>();
            RmFormB1b2BrInsImage = new HashSet<RmFormB1b2BrInsImage>();
            RmFormF5InsDtl = new HashSet<RmFormF5InsDtl>();
        }

        public int FbrihPkRefNo { get; set; }
        public int? FbrihAiPkRefNo { get; set; }
        public string FbrihAiAssetId { get; set; }
        public int? FbrihAiLocChKm { get; set; }
        public string FbrihAiLocChM { get; set; }
        public string FbrihAiStrucCode { get; set; }
        public double? FbrihAiGpsEasting { get; set; }
        public double? FbrihAiGpsNorthing { get; set; }
        public string FbrihAiRdCode { get; set; }
        public string FbrihAiRdName { get; set; }
        public string FbrihAiRiverName { get; set; }
        public string FbrihAiDivCode { get; set; }
        public string FbrihAiRmuName { get; set; }
        public string FbrihAiStrucSuper { get; set; }
        public string FbrihAiParapetType { get; set; }
        public string FbrihAiBearingType { get; set; }
        public string FbrihAiExpanType { get; set; }
        public string FbrihAiDeckType { get; set; }
        public string FbrihAiAbutType { get; set; }
        public string FbrihAiPierType { get; set; }
        public int? FbrihAiExpanJointCount { get; set; }
        public double? FbrihAiWidthLane { get; set; }
        public double? FbrihAiLengthSpan { get; set; }
        public double? FbrihAiLength { get; set; }
        public double? FbrihAiWidth { get; set; }
        public int? FbrihAiLaneCnt { get; set; }
        public int? FbrihAiSpanCnt { get; set; }
        public double? FbrihAiMedian { get; set; }
        public double? FbrihAiWalkway { get; set; }
        public string FbrihCInspRefNo { get; set; }
        public int? FbrihYearOfInsp { get; set; }
        public DateTime? FbrihDtOfInsp { get; set; }
        public int? FbrihRecordNo { get; set; }
        public string FbrihSerProviderDefObs { get; set; }
        public string FbrihAuthDefObs { get; set; }
        public string FbrihSerProviderDefGenCom { get; set; }
        public string FbrihAuthDefGenCom { get; set; }
        public string FbrihSerProviderDefFeedback { get; set; }
        public string FbrihAuthDefFeedback { get; set; }
        public int? FbrihSerProviderUserId { get; set; }
        public string FbrihSerProviderUserName { get; set; }
        public string FbrihSerProviderUserDesignation { get; set; }
        public DateTime? FbrihSerProviderInsDt { get; set; }
        public string FbrihSignpathSerProvider { get; set; }
        public int? FbrihUserIdAud { get; set; }
        public string FbrihUserNameAud { get; set; }
        public string FbrihUserDesignationAud { get; set; }
        public DateTime? FbrihDtAud { get; set; }
        public string FbrihSignpathAud { get; set; }
        public int? FbrihBridgeConditionRat { get; set; }
        public bool? FbrihReqFurtherInv { get; set; }
        public int? FbrihModBy { get; set; }
        public DateTime? FbrihModDt { get; set; }
        public int? FbrihCrBy { get; set; }
        public DateTime? FbrihCrDt { get; set; }
        public bool FbrihSubmitSts { get; set; }
        public bool? FbrihActiveYn { get; set; }
        public string FbrihStatus { get; set; }
        public string FbrihAuditLog { get; set; }

        public virtual RmAllassetInventory FbrihAiPkRefNoNavigation { get; set; }
        public virtual ICollection<RmFormB1b2BrInsDtl> RmFormB1b2BrInsDtl { get; set; }
        public virtual ICollection<RmFormB1b2BrInsImage> RmFormB1b2BrInsImage { get; set; }
        public virtual ICollection<RmFormF5InsDtl> RmFormF5InsDtl { get; set; }
    }
}
