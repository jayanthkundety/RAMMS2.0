using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormCvInsHdr
    {
        public RmFormCvInsHdr()
        {
            RmFormCvInsDtl = new HashSet<RmFormCvInsDtl>();
            RmFormCvInsImage = new HashSet<RmFormCvInsImage>();
            RmFormF4InsDtl = new HashSet<RmFormF4InsDtl>();
        }

        public int FcvihPkRefNo { get; set; }
        public int? FcvihAiPkRefNo { get; set; }
        public string FcvihAiAssetId { get; set; }
        public string FcvihAiDivCode { get; set; }
        public string FcvihAiRmuName { get; set; }
        public string FcvihAiRdCode { get; set; }
        public string FcvihAiRdName { get; set; }
        public int? FcvihAiLocChKm { get; set; }
        public string FcvihAiLocChM { get; set; }
        public double? FcvihAiFinRdLevel { get; set; }
        public string FcvihAiStrucCode { get; set; }
        public double? FcvihAiCatchArea { get; set; }
        public double? FcvihAiSkew { get; set; }
        public double? FcvihAiDesignFlow { get; set; }
        public double? FcvihAiLength { get; set; }
        public string FcvihAiPrecastSitu { get; set; }
        public string FcvihAiGrpType { get; set; }
        public int? FcvihAiBarrelNo { get; set; }
        public double? FcvihAiGpsEasting { get; set; }
        public double? FcvihAiGpsNorthing { get; set; }
        public string FcvihAiMaterial { get; set; }
        public double? FcvihAiIntelLevel { get; set; }
        public double? FcvihAiOutletLevel { get; set; }
        public string FcvihAiIntelStruc { get; set; }
        public string FcvihAiOutletStruc { get; set; }
        public string FcvihCInspRefNo { get; set; }
        public int? FcvihYearOfInsp { get; set; }
        public DateTime? FcvihDtOfInsp { get; set; }
        public int? FcvihRecordNo { get; set; }
        public bool? FcvihPrkPosition { get; set; }
        public bool? FcvihAccessibility { get; set; }
        public bool? FcvihPotentialHazards { get; set; }
        public string FcvihSerProviderDefObs { get; set; }
        public string FcvihAuthDefObs { get; set; }
        public string FcvihSerProviderDefGenCom { get; set; }
        public string FcvihAuthDefGenCom { get; set; }
        public string FcvihSerProviderDefFeedback { get; set; }
        public string FcvihAuthDefFeedback { get; set; }
        public int? FcvihSerProviderUserId { get; set; }
        public string FcvihSerProviderUserName { get; set; }
        public string FcvihSerProviderUserDesignation { get; set; }
        public DateTime? FcvihSerProviderInsDt { get; set; }
        public string FcvihSignpathSerProvider { get; set; }
        public int? FcvihUserIdAud { get; set; }
        public string FcvihUserNameAud { get; set; }
        public string FcvihUserDesignationAud { get; set; }
        public DateTime? FcvihDtAud { get; set; }
        public string FcvihSignpathAud { get; set; }
        public int? FcvihCulvertConditionRat { get; set; }
        public bool? FcvihReqFurtherInv { get; set; }
        public int? FcvihModBy { get; set; }
        public DateTime? FcvihModDt { get; set; }
        public int? FcvihCrBy { get; set; }
        public DateTime? FcvihCrDt { get; set; }
        public bool FcvihSubmitSts { get; set; }
        public bool? FcvihActiveYn { get; set; }
        public string FcvihStatus { get; set; }
        public string FcvihAuditLog { get; set; }

        public virtual RmAllassetInventory FcvihAiPkRefNoNavigation { get; set; }
        public virtual ICollection<RmFormCvInsDtl> RmFormCvInsDtl { get; set; }
        public virtual ICollection<RmFormCvInsImage> RmFormCvInsImage { get; set; }
        public virtual ICollection<RmFormF4InsDtl> RmFormF4InsDtl { get; set; }
    }
}
