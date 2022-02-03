using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PitTaskDetail
    {
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
        public string Remark { get; set; }
        public string Id { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDt { get; set; }
        public double? CreatedAtLat { get; set; }
        public double? CreatedAtLng { get; set; }
        public string UploadedBy { get; set; }
        public DateTime? UploadedDt { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDt { get; set; }
        public string AcEventType { get; set; }
        public bool? IsRecuring { get; set; }
        public bool? AccidentEmailSent { get; set; }
        public bool? RecuringEmailSent { get; set; }
        public string SectionName { get; set; }
        public string Bound { get; set; }
        public string FeatureId { get; set; }
        public string MainName { get; set; }
        public string MainCode { get; set; }
        public int? MainPk { get; set; }
        public string Level1Name { get; set; }
        public string Level1Code { get; set; }
        public int? Level1Pk { get; set; }
        public string Level2Name { get; set; }
        public string Level2Code { get; set; }
        public int? Level2Pk { get; set; }
        public DateTime? AcTmcInformDt { get; set; }
        public int? TotalDamageAsset { get; set; }
        public int? GroupStructurePk { get; set; }
        public string PitAssetGroupName { get; set; }
        public int? DamageQty { get; set; }
        public string PhotoFileName1 { get; set; }
        public string PhotoFileName2 { get; set; }
        public string PhotoFileName3 { get; set; }
    }
}
