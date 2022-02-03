using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class SicView
    {
        public int Pk { get; set; }
        public string Shift { get; set; }
        public string FormType { get; set; }
        public string Status { get; set; }
        public string VehicleWorking { get; set; }
        public string VehicleRemark { get; set; }
        public double? SicActualCrew { get; set; }
        public string InspectedByName { get; set; }
        public string InspectedByPosition { get; set; }
        public string InspectedByCompany { get; set; }
        public string InspectedBySignature { get; set; }
        public string ConfirmedByName { get; set; }
        public string ConfirmedByPosition { get; set; }
        public string ConfirmedByCompany { get; set; }
        public string ConfirmedBySignature { get; set; }
        public DateTime? UploadedDt { get; set; }
        public string UploadedBy { get; set; }
        public double? EditedAtLat { get; set; }
        public double? EditedAtLng { get; set; }
        public string LastCalculatedBy { get; set; }
        public DateTime? LastCalculatedDt { get; set; }
        public DateTime? EditStartDt { get; set; }
        public DateTime? EditEndDt { get; set; }
        public int? SicPk { get; set; }
        public string FertilizerType { get; set; }
        public string CompositionN { get; set; }
        public string CompositionP { get; set; }
        public string CompositionK { get; set; }
        public string CompositionTracers { get; set; }
        public string BagsUsed { get; set; }
        public string McsActivityName { get; set; }
        public string McsActivityUom { get; set; }
        public string McsActivityTypeOfArea { get; set; }
        public int? McsCategoryPk { get; set; }
        public string BoundName { get; set; }
        public string ContractorName { get; set; }
        public int McsActivityPk { get; set; }
        public int SectionPk { get; set; }
        public DateTime ActivityDate { get; set; }
        public string Location { get; set; }
        public string Interchange { get; set; }
        public double? Kmfrom { get; set; }
        public double? Kmto { get; set; }
        public string Bound { get; set; }
        public double? PlannedQuantity { get; set; }
        public double? ActualQuantity { get; set; }
        public double? PlannedCrew { get; set; }
        public double? ScorecardActualCrew { get; set; }
        public int ContractorPk { get; set; }
        public double? AvailableManhourPerManPerDay { get; set; }
        public string Issue { get; set; }
        public string CorrectiveAction { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
