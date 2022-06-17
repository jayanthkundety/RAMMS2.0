using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormG1Hdr
    {
        public RmFormG1Hdr()
        {
            RmFormG2Hdr = new HashSet<RmFormG2Hdr>();
            RmFormGImages = new HashSet<RmFormGImages>();
        }

        public int Fg1hPkRefNo { get; set; }
        public string Fg1hRefNo { get; set; }
        public int? Fg1hAiPkRefNo { get; set; }
        public string Fg1hAssetId { get; set; }
        public string Fg1hDivCode { get; set; }
        public string Fg1hRmuCode { get; set; }
        public string Fg1hRmuName { get; set; }
        public string Fg1hRdCode { get; set; }
        public string Fg1hRdName { get; set; }
        public int? Fg1hLocChKm { get; set; }
        public int? Fg1hLocChM { get; set; }
        public string Fg1hStrucCode { get; set; }
        public decimal? Fg1hGpsEasting { get; set; }
        public decimal? Fg1hGpsNorthing { get; set; }
        public int? Fg1hYearOfInsp { get; set; }
        public DateTime? Fg1hDtOfInsp { get; set; }
        public int? Fg1hRecordNo { get; set; }
        public bool? Fg1hPrkPosition { get; set; }
        public bool? Fg1hAccessibility { get; set; }
        public bool? Fg1hPotentialHazards { get; set; }
        public string Fg1hInspBarrier { get; set; }
        public string Fg1hInspGBeam { get; set; }
        public string Fg1hInspGColumn { get; set; }
        public string Fg1hInspFootings { get; set; }
        public string Fg1hInspGPads { get; set; }
        public string Fg1hInspMaintenance { get; set; }
        public string Fg1hInspStaticSigns { get; set; }
        public string Fg1hInspVms { get; set; }
        public int? Fg1hInspectedBy { get; set; }
        public string Fg1hInspectedName { get; set; }
        public string Fg1hInspectedDesig { get; set; }
        public DateTime? Fg1hInspectedDt { get; set; }
        public bool? Fg1hInspectedSign { get; set; }
        public int? Fg1hCondRating { get; set; }
        public bool? Fg1hIssuesFound { get; set; }
        public int? Fg1hAuditedBy { get; set; }
        public string Fg1hAuditedName { get; set; }
        public string Fg1hAuditedDesig { get; set; }
        public DateTime? Fg1hAuditedDt { get; set; }
        public bool? Fg1hAuditedSign { get; set; }
        public int? Fg1hModBy { get; set; }
        public DateTime? Fg1hModDt { get; set; }
        public int? Fg1hCrBy { get; set; }
        public DateTime? Fg1hCrDt { get; set; }
        public bool Fg1hSubmitSts { get; set; }
        public bool Fg1hActiveYn { get; set; }
        public string Fg1hStatus { get; set; }
        public string Fg1hAuditLog { get; set; }

        public virtual ICollection<RmFormG2Hdr> RmFormG2Hdr { get; set; }
        public virtual ICollection<RmFormGImages> RmFormGImages { get; set; }
    }
}
