using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class McsActivityScorecardDetail
    {
        public int Pk { get; set; }
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
        public double? ActualCrew { get; set; }
        public int ContractorPk { get; set; }
        public double? AvailableManhourPerManPerDay { get; set; }
        public string Issue { get; set; }
        public string CorrectiveAction { get; set; }
        public string Remark { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string McsActivityName { get; set; }
        public string McsActivityCode { get; set; }
        public string SectionName { get; set; }
        public string SectionCode { get; set; }
        public string ContractorCode { get; set; }
        public string ContractorCompanyName { get; set; }
        public DateTime? McsCalendarWeekJanStartDate { get; set; }
        public string LostTime { get; set; }
        public int? McsGroupPk { get; set; }
        public int? McsCategoryPk { get; set; }
        public string McsGroupName { get; set; }
        public string McsGroupCode { get; set; }
        public string McsCategoryName { get; set; }
        public string McsCategoryCode { get; set; }
        public int? SicPk { get; set; }
        public string SicStatus { get; set; }
        public string SicShift { get; set; }
        public double? SicEditedAtLat { get; set; }
        public double? SicEditedAtLng { get; set; }
        public string SicLastCalculatedBy { get; set; }
        public DateTime? SicLastCalculatedDt { get; set; }
        public string SicUploadedBy { get; set; }
        public DateTime? SicUploadedDt { get; set; }
        public DateTime? SicEditStartDt { get; set; }
        public DateTime? SicEditEndDt { get; set; }
        public string SicFormType { get; set; }
        public string SicInspectedByName { get; set; }
        public string SicInspectedByPosition { get; set; }
        public string SicInspectedByCompany { get; set; }
        public string SicInspectedBySignature { get; set; }
        public string SicConfirmedByName { get; set; }
        public string SicConfirmedByPosition { get; set; }
        public string SicConfirmedByCompany { get; set; }
        public string SicConfirmedBySignature { get; set; }
        public string SicVehicleWorking { get; set; }
        public string SicVehicleRemark { get; set; }
        public double? SicActualCrew { get; set; }
    }
}
