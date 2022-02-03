using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PiResultCulvert
    {
        public PiResultCulvert()
        {
            PiResultCulvertFurtherInvestigation = new HashSet<PiResultCulvertFurtherInvestigation>();
            PiResultCulvertPhoto = new HashSet<PiResultCulvertPhoto>();
            PiResultCulvertRemedialWork = new HashSet<PiResultCulvertRemedialWork>();
            PiResultCulvertRoutineWork = new HashSet<PiResultCulvertRoutineWork>();
        }

        public int PiSchedulePk { get; set; }
        public string GenInfoTypeOfInspection { get; set; }
        public string GenInfoIf3rdParty { get; set; }
        public string GenInfoWeather { get; set; }
        public string GenInfoCulvertMarker { get; set; }
        public string GenInfoPurpose { get; set; }
        public string BasicType { get; set; }
        public string BasicTypePhoto { get; set; }
        public string BasicSize { get; set; }
        public string BasicSizePhoto { get; set; }
        public string BasicCellNo { get; set; }
        public string BasicCellNoPhoto { get; set; }
        public string BasicInlet { get; set; }
        public string BasicInletPhoto { get; set; }
        public string BasicOutlet { get; set; }
        public string BasicOutletPhoto { get; set; }
        public string FeatureLanduseUp { get; set; }
        public string FeatureLanduseDown { get; set; }
        public string FeatureAccessibilityUp { get; set; }
        public string FeatureAccessibilityDown { get; set; }
        public string SummaryComments { get; set; }
        public string SummaryPriorityRating { get; set; }
        public string HydraulicFloodIn { get; set; }
        public string HydraulicFloodOut { get; set; }
        public string HydraulicPondingIn { get; set; }
        public string HydraulicPondingOut { get; set; }
        public string SiltationIn { get; set; }
        public string SiltationOut { get; set; }
        public string VegetationIn { get; set; }
        public string VegetationOut { get; set; }
        public string ScouringIn { get; set; }
        public string ScouringOut { get; set; }
        public string SummaryOverallService { get; set; }
        public string BarrelSettlement { get; set; }
        public string BarrelSettlementRem { get; set; }
        public string BarrelAbrasion { get; set; }
        public string BarrelAbrasionRem { get; set; }
        public string BarrelCracking { get; set; }
        public string BarrelCrackingRem { get; set; }
        public string BarrelCorosion { get; set; }
        public string BarrelCorosionRem { get; set; }
        public string VbcDamage { get; set; }
        public string VbcDamageRem { get; set; }
        public string HdpeShape { get; set; }
        public string HdpeShapeRem { get; set; }
        public string HdpeAbrasion { get; set; }
        public string HdpeAbrasionRem { get; set; }
        public string HdpePaint { get; set; }
        public string HdpePaintRem { get; set; }
        public string HdpeCorrosion { get; set; }
        public string HdpeCorrosionRem { get; set; }
        public string HdpeBolts { get; set; }
        public string HdpeBoltsRem { get; set; }
        public string InHeadwallMovement { get; set; }
        public string InHeadwallMovementRem { get; set; }
        public string InHeadwallCracking { get; set; }
        public string InHeadwallCrackingRem { get; set; }
        public string InHeadwallCorrosion { get; set; }
        public string InHeadwallCorrosionRem { get; set; }
        public string InHeadwallDeterioration { get; set; }
        public string InHeadwallDeteriorationRem { get; set; }
        public string InApronCracking { get; set; }
        public string InApronCrackingRem { get; set; }
        public string InApronRipRap { get; set; }
        public string InApronRipRapRem { get; set; }
        public string InApronGabions { get; set; }
        public string InApronGabionsRem { get; set; }
        public string InEmbankCondition { get; set; }
        public string InEmbankConditionRem { get; set; }
        public string InEmbankDistingration { get; set; }
        public string InEmbankDistingrationRem { get; set; }
        public string InEmbankDrain { get; set; }
        public string InEmbankDrainRem { get; set; }
        public string OutHeadwallMovement { get; set; }
        public string OutHeadwallMovementRem { get; set; }
        public string OutHeadwallCracking { get; set; }
        public string OutHeadwallCrackingRem { get; set; }
        public string OutHeadwallCorrosion { get; set; }
        public string OutHeadwallCorrosionRem { get; set; }
        public string OutHeadwallDeterioration { get; set; }
        public string OutHeadwallDeteriorationRem { get; set; }
        public string OutApronCracking { get; set; }
        public string OutApronCrackingRem { get; set; }
        public string OutApronRipRap { get; set; }
        public string OutApronRipRapRem { get; set; }
        public string OutApronGabions { get; set; }
        public string OutApronGabionsRem { get; set; }
        public string OutEmbankCondition { get; set; }
        public string OutEmbankConditionRem { get; set; }
        public string OutEmbankDistingration { get; set; }
        public string OutEmbankDistingrationRem { get; set; }
        public string OutEmbankDrain { get; set; }
        public string OutEmbankDrainRem { get; set; }
        public string SummaryOverallCondition { get; set; }

        public virtual PiSchedule PiSchedulePkNavigation { get; set; }
        public virtual ICollection<PiResultCulvertFurtherInvestigation> PiResultCulvertFurtherInvestigation { get; set; }
        public virtual ICollection<PiResultCulvertPhoto> PiResultCulvertPhoto { get; set; }
        public virtual ICollection<PiResultCulvertRemedialWork> PiResultCulvertRemedialWork { get; set; }
        public virtual ICollection<PiResultCulvertRoutineWork> PiResultCulvertRoutineWork { get; set; }
    }
}
