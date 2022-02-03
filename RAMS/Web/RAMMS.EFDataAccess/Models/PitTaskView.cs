using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PitTaskView
    {
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string FeatureId { get; set; }
        public string FeatureName { get; set; }
        public string AcNewsFeatureName { get; set; }
        public int Pk { get; set; }
        public string Shift { get; set; }
        public int? SectionPk { get; set; }
        public int? FeaturePk { get; set; }
        public int? PitTaskStructurePk { get; set; }
        public string Description { get; set; }
        public string Lane { get; set; }
        public double? KmFrom { get; set; }
        public double? KmTo { get; set; }
        public string KmLocation { get; set; }
        public DateTime? ResponseDt { get; set; }
        public DateTime? FinishedDt { get; set; }
        public bool? WorkDelayed { get; set; }
        public bool? IsRecuring { get; set; }
        public string Remark { get; set; }
        public string AcEventType { get; set; }
        public DateTime? AcTmcInformDt { get; set; }
        public DateTime? AcSpvisorInformDt { get; set; }
        public string AcNewsLocation { get; set; }
        public int? AcNewsFeaturePk { get; set; }
        public string AcNewsKmLoc { get; set; }
        public DateTime? AcMovingToLocDt { get; set; }
        public DateTime? AcPitArriveDt { get; set; }
        public DateTime? AcRondaArriveDt { get; set; }
        public DateTime? AcInvestigatorArriveDt { get; set; }
        public DateTime? AcAmbulanceArriveDt { get; set; }
        public DateTime? AcBombaArriveDt { get; set; }
        public DateTime? AcSpvisorArriveDt { get; set; }
        public bool? Ac2ndAccident { get; set; }
        public int? AcSelamatCount { get; set; }
        public int? AcRinganCount { get; set; }
        public int? AcParahCount { get; set; }
        public int? AcMautCount { get; set; }
        public string AcLateFactor { get; set; }
        public string AcAccidentSummary { get; set; }
        public bool? AcDanger { get; set; }
        public string AcWeather { get; set; }
        public string AcRoadSurface { get; set; }
        public string AcRoadGeometry { get; set; }
        public string AcLaneClosed { get; set; }
        public string AcDamageLocation { get; set; }
        public string AcPrepByName { get; set; }
        public string AcPrepByPosition { get; set; }
        public DateTime? AcPrepByDt { get; set; }
        public string AcPrepBySign { get; set; }
        public string AcSuppByName { get; set; }
        public string AcSuppByPosition { get; set; }
        public DateTime? AcSuppByDt { get; set; }
        public string AcSuppBySign { get; set; }
        public string AcApprovedByName { get; set; }
        public string AcApprovedByPosition { get; set; }
        public DateTime? AcApprovedByDt { get; set; }
        public string AcApprovedBySign { get; set; }
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDt { get; set; }
        public double? CreatedAtLat { get; set; }
        public double? CreatedAtLng { get; set; }
        public string UploadedBy { get; set; }
        public DateTime? UploadedDt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDt { get; set; }
        public bool? RecuringEmailSent { get; set; }
        public bool? AccidentEmailSent { get; set; }
    }
}
