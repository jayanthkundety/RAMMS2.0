using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiResultTunnelPanel
    {
        public int Pk { get; set; }
        public int PiSchedulePk { get; set; }
        public int PanelNo { get; set; }
        public string SideType { get; set; }
        public string DrainageStructureCode { get; set; }
        public string DrainageStructureCondition { get; set; }
        public string DrainageStructureSeverity { get; set; }
        public string DrainageStructureExtent { get; set; }
        public string DrainageStructureRemarks { get; set; }
        public string DrainageServiceabilityCode { get; set; }
        public string DrainageServiceabilityCondition { get; set; }
        public string DrainageServiceabilitySeverity { get; set; }
        public string DrainageServiceabilityExtent { get; set; }
        public string DrainageServiceabilityRemarks { get; set; }
        public string DrainageOtherRemarks { get; set; }
        public string DrainageRating { get; set; }
        public string ServiceDuctStructureCode { get; set; }
        public string ServiceDuctStructureCondition { get; set; }
        public string ServiceDuctStructureSeverity { get; set; }
        public string ServiceDuctStructureExtent { get; set; }
        public string ServiceDuctStructureRemarks { get; set; }
        public string ServiceDuctServiceabilityCode { get; set; }
        public string ServiceDuctServiceabilityCondition { get; set; }
        public string ServiceDuctServiceabilitySeverity { get; set; }
        public string ServiceDuctServiceabilityExtent { get; set; }
        public string ServiceDuctServiceabilityRemarks { get; set; }
        public string ServiceDuctOtherRemarks { get; set; }
        public string ServiceDuctRating { get; set; }
        public string FinishingCrackingCode { get; set; }
        public string FinishingCrackingCondition { get; set; }
        public string FinishingCrackingSeverity { get; set; }
        public string FinishingCrackingExtent { get; set; }
        public string FinishingCrackingRemarks { get; set; }
        public string FinishingSpallingCode { get; set; }
        public string FinishingSpallingCondition { get; set; }
        public string FinishingSpallingSeverity { get; set; }
        public string FinishingSpallingExtent { get; set; }
        public string FinishingSpallingRemarks { get; set; }
        public string FinishingStainingCode { get; set; }
        public string FinishingStainingCondition { get; set; }
        public string FinishingStainingSeverity { get; set; }
        public string FinishingStainingExtent { get; set; }
        public string FinishingStainingRemarks { get; set; }
        public string FinishingOtherRemarks { get; set; }
        public string FinishingRating { get; set; }
        public string ConcreteCrackingCode { get; set; }
        public string ConcreteCrackingCondition { get; set; }
        public string ConcreteCrackingSeverity { get; set; }
        public string ConcreteCrackingExtent { get; set; }
        public string ConcreteCrackingRemarks { get; set; }
        public string ConcreteSpallingCode { get; set; }
        public string ConcreteSpallingCondition { get; set; }
        public string ConcreteSpallingSeverity { get; set; }
        public string ConcreteSpallingExtent { get; set; }
        public string ConcreteSpallingRemarks { get; set; }
        public string ConcreteOtherRemarks { get; set; }
        public string ConcreteRating { get; set; }
        public string SealantCrackingCode { get; set; }
        public string SealantCrackingCondition { get; set; }
        public string SealantCrackingSeverity { get; set; }
        public string SealantCrackingExtent { get; set; }
        public string SealantCrackingRemarks { get; set; }
        public string SealantBulgingCode { get; set; }
        public string SealantBulgingCondition { get; set; }
        public string SealantBulgingSeverity { get; set; }
        public string SealantBulgingExtent { get; set; }
        public string SealantBulgingRemarks { get; set; }
        public string SealantLeakageCode { get; set; }
        public string SealantLeakageCondition { get; set; }
        public string SealantLeakageSeverity { get; set; }
        public string SealantLeakageExtent { get; set; }
        public string SealantLeakageRemarks { get; set; }
        public string SealantOtherRemarks { get; set; }
        public string SealantRating { get; set; }

        public virtual PiResultTunnel PiSchedulePkNavigation { get; set; }
    }
}
