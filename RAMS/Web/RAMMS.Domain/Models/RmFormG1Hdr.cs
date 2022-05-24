using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormG1Hdr
    {
        public int Fg1hPkRefNo { get; set; }
        public string Fg1hContNo { get; set; }
        public string Fg1hAiDivCode { get; set; }
        public string Fg1hAiRmuName { get; set; }
        public string Fg1hAiRdCode { get; set; }
        public string Fg1hAiRdName { get; set; }
        public int? Fg1hAiLocChKm { get; set; }
        public int? Fg1hAiLocChM { get; set; }
        public string Fg1hAiStrucCode { get; set; }
        public decimal? Fg1hAiGpsEasting { get; set; }
        public decimal? Fg1hAiGpsNorthing { get; set; }
        public int? Fg1hYearOfInsp { get; set; }
        public DateTime? Fg1hDtOfInsp { get; set; }
        public int? Fg1hRecordNo { get; set; }
        public bool? Fg1hPrkPosition { get; set; }
        public bool? Fg1hAccessibility { get; set; }
        public bool? Fg1hPotentialHazards { get; set; }
        public bool? Fg1hInspBarrier { get; set; }
        public bool? Fg1hInspBarrierCritical { get; set; }
        public bool? Fg1hInspBarrierClosed { get; set; }
        public bool? Fg1hInspGBeam { get; set; }
        public bool? Fg1hInspGBeamCritical { get; set; }
        public bool? Fg1hInspGBeamClosed { get; set; }
        public bool? Fg1hInspGColumn { get; set; }
        public bool? Fg1hInspGColumnCritical { get; set; }
        public bool? Fg1hInspGColumnClosed { get; set; }
        public bool? Fg1hInspFootings { get; set; }
        public bool? Fg1hInspFootingsCritical { get; set; }
        public bool? Fg1hInspFootingsClosed { get; set; }
        public bool? Fg1hInspGPads { get; set; }
        public bool? Fg1hInspGPadsCritical { get; set; }
        public bool? Fg1hInspGPadsClosed { get; set; }
        public bool? Fg1hInspMaintenance { get; set; }
        public bool? Fg1hInspMaintenanceCritical { get; set; }
        public bool? Fg1hInspMaintenanceClosed { get; set; }
        public bool? Fg1hInspStaticSigns { get; set; }
        public bool? Fg1hInspStaticSignsCritical { get; set; }
        public bool? Fg1hInspStaticSignsClosed { get; set; }
        public bool? Fg1hInspVms { get; set; }
        public bool? Fg1hInspVmsCritical { get; set; }
        public bool? Fg1hInspVmsClosed { get; set; }
        public int? Fg1hModBy { get; set; }
        public DateTime? Fg1hModDt { get; set; }
        public int? Fg1hCrBy { get; set; }
        public DateTime? Fg1hCrDt { get; set; }
        public bool Fg1hSubmitSts { get; set; }
        public bool Fg1hActiveYn { get; set; }
        public string Fg1hStatus { get; set; }
        public string Fg1hAuditLog { get; set; }
    }
}
