using AutoMapper;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormR1DTO
    {
        public int PkRefNo { get; set; }
        public string ContNo { get; set; }
        public string AssetId { get; set; }
        public string DivCode { get; set; }
        public string RmuCode { get; set; }
        public string RmuName { get; set; }
        public string RdCode { get; set; }
        public string RdName { get; set; }
        public int? LocChKm { get; set; }
        public int? LocChM { get; set; }
        public string StrucCode { get; set; }
        public decimal? GpsEasting { get; set; }
        public decimal? GpsNorthing { get; set; }
        public int? YearOfInsp { get; set; }
        public DateTime? DtOfInsp { get; set; }
        public string WallFunction { get; set; }
        public string WallMember { get; set; }
        public string FacingType { get; set; }
        public int? RecordNo { get; set; }
        public string Observed1 { get; set; }
        public string Observed2 { get; set; }
        public string Observed3 { get; set; }
        public int? Severity { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }
    }

    public class FormR2DTO
    {
        public int PkRefNo { get; set; }
        public int? FR1hPkRefNo { get; set; }
        public string DistressSp { get; set; }
        public string DistressEc { get; set; }
        public string GeneralSp { get; set; }
        public string GeneralEc { get; set; }
        public string FeedbackSp { get; set; }
        public string FeedbackEc { get; set; }
        public string SPName { get; set; }
        public string SPDesignation { get; set; }
        public DateTime? SPInspDate { get; set; }
        public string InspectedBy { get; set; }
        public string InspectedBySign { get; set; }
        public string ECName { get; set; }
        public string ECDesignation { get; set; }
        public DateTime? ECInspDate { get; set; }
        public int? Rating { get; set; }
        public bool? IssueFound { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
    }

    //Fgi
    public class FormRImagesDTO
    {
        public int PkRefNo { get; set; }
        public int? FR1hPkRefNo { get; set; }
        public string ImgRefId { get; set; }
        public string ImageTypeCode { get; set; }
        public int? ImageSrno { get; set; }
        public string ImageFilenameSys { get; set; }
        public string ImageFilenameUpload { get; set; }
        public string ImageUserFilePath { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
    }

    public class FormR1R2PhotoTypeDTO
    {
        public int SNO { get; set; }
        public string Type { get; set; }
    }
}
