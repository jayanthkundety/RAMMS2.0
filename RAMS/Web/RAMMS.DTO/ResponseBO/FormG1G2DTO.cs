using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormG1DTO
    {
        //
        public int PkRefNo { get; set; }
        public string RefNo { get; set; }

        public int? AiPkRefNo { get; set; }
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
        public int? RecordNo { get; set; }
        public bool? PrkPosition { get; set; }
        public bool? Accessibility { get; set; }
        public bool? PotentialHazards { get; set; }
        public string InspBarrier { get; set; }
        public string InspGBeam { get; set; }
        public string InspGColumn { get; set; }
        public string InspFootings { get; set; }
        public string InspGPads { get; set; }
        public string InspMaintenance { get; set; }
        public string InspStaticSigns { get; set; }
        public string InspVms { get; set; }
        public int? InspectedBy { get; set; }
        public string InspectedName { get; set; }
        public string InspectedDesig { get; set; }
        public DateTime? InspectedDt { get; set; }
        public bool? InspectedSign { get; set; }
        public int? CondRating { get; set; }
        public bool? IssuesFound { get; set; }
        public int? AuditedBy { get; set; }
        public string AuditedName { get; set; }
        public string AuditedDesig { get; set; }
        public DateTime? AuditedDt { get; set; }
        public bool? AuditedSign { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }

        public FormG2DTO FormG2 { get; set; }   
    }

    //
    public class FormG2DTO
    {
        public int PkRefNo { get; set; }
        public int? Fg1hPkRefNo { get; set; }
        public string DistressSp { get; set; }
        public string DistressEc { get; set; }
        public string GeneralSp { get; set; }
        public string GeneralEc { get; set; }
        public string FeedbackSp { get; set; }
        public string FeedbackEc { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
    }

    //Fgi
    public class FormGImagesDTO
    {
        public int PkRefNo { get; set; }
        public int? Fg1hPkRefNo { get; set; }

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

    public class FormG1G2PhotoTypeDTO
    {
        public int SNO { get; set; }
        public string Type { get; set; }
    }
}
