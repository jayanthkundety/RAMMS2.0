using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormG1DTO
    {
        //Fg1h
        public int PkRefNo { get; set; }
        public string RefNo { get; set; }
        public int? AiPkRefNo { get; set; }
        public string AssetId { get; set; }
        public string DivCode { get; set; }

        public string RmuCode { get; set; }
        public string RmuName { get; set; }

        //public string SecCode { get; set; }
        //public string SecName { get; set; }

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
        public bool? InspBarrier { get; set; }
        public bool? InspBarrierCritical { get; set; }
        public bool? InspBarrierClosed { get; set; }
        public bool? InspGBeam { get; set; }
        public bool? InspGBeamCritical { get; set; }
        public bool? InspGBeamClosed { get; set; }
        public bool? InspGColumn { get; set; }
        public bool? InspGColumnCritical { get; set; }
        public bool? InspGColumnClosed { get; set; }
        public bool? InspFootings { get; set; }
        public bool? InspFootingsCritical { get; set; }
        public bool? InspFootingsClosed { get; set; }
        public bool? InspGPads { get; set; }
        public bool? InspGPadsCritical { get; set; }
        public bool? InspGPadsClosed { get; set; }
        public bool? InspMaintenance { get; set; }
        public bool? InspMaintenanceCritical { get; set; }
        public bool? InspMaintenanceClosed { get; set; }
        public bool? InspStaticSigns { get; set; }
        public bool? InspStaticSignsCritical { get; set; }
        public bool? InspStaticSignsClosed { get; set; }
        public bool? InspVms { get; set; }
        public bool? InspVmsCritical { get; set; }
        public bool? InspVmsClosed { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }
    }

    //Fg2h
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
        public string SpName { get; set; }
        public string SpDesignation { get; set; }
        public DateTime? SpInspDate { get; set; }
        public int? InspectedBy { get; set; }
        public bool? InspectedBySign { get; set; }
        public string EcName { get; set; }
        public string EcDesignation { get; set; }
        public DateTime? EcInspDate { get; set; }
        public int? Rating { get; set; }
        public bool? IssuesFound { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }
    }

    //Fgi
    public class FormGImagesDTO
    {
        public int PkRefNo { get; set; }
        public int? Fg1hPkRefNo { get; set; }
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
