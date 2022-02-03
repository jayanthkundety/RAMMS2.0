using AutoMapper;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormC1C2DTO
    {
        public int PkRefNo { get; set; }
        public int? AiPkRefNo { get; set; }
        public string AiAssetId { get; set; }
        public string AiDivCode { get; set; }
        public string AiRmuName { get; set; }
        public string AiRdCode { get; set; }
        public string AiRdName { get; set; }
        public int? AiLocChKm { get; set; }
        public string AiLocChM { get; set; }
        public double? AiFinRdLevel { get; set; }
        public string AiStrucCode { get; set; }
        public double? AiCatchArea { get; set; }
        public double? AiSkew { get; set; }
        public double? AiDesignFlow { get; set; }
        public double? AiLength { get; set; }
        public string AiPrecastSitu { get; set; }
        public string AiGrpType { get; set; }
        public int? AiBarrelNo { get; set; }
        public double? AiGpsEasting { get; set; }
        public double? AiGpsNorthing { get; set; }
        public string AiMaterial { get; set; }
        public double? AiIntelLevel { get; set; }
        public double? AiOutletLevel { get; set; }
        public string AiIntelStruc { get; set; }
        public string AiOutletStruc { get; set; }
        public string CInspRefNo { get; set; }
        public int? YearOfInsp { get; set; }
        public DateTime? DtOfInsp { get; set; }
        public int? RecordNo { get; set; }
        public bool? PrkPosition { get; set; }
        public bool? Accessibility { get; set; }
        public bool? PotentialHazards { get; set; }
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
        public int? CulvertConditionRat { get; set; }
        public bool? ReqFurtherInv { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }
        public IList<RmFormCvInsDtDTO> InsDtl { get; set; }
        //public static void Config(Profile profile)
        //{
        //    profile.RecognizeDestinationPrefixes("Fcvih", "RmFormCv");
        //    profile.RecognizePrefixes("Fcvih", "RmFormCv");
        //    profile.CreateMap<FormC1C2DTO, RmFormCvInsHdr>().ReverseMap();
        //}
    }
}
