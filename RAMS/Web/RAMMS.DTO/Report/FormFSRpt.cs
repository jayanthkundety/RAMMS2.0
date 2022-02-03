using System;
namespace RAMMS.DTO.Report
{
    public class FormFSRpt
    {
        public string RMU { get; set; }
        public string District { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public string Division { get; set; }
        public DateTime? DateOfInspection { get; set; }
        public string CrewLeader { get; set; }
        public string SummarizedBy { get; set; }
        public string CheckedBy { get; set; }
        public FormFSDetailRpt CWAsphaltic { get; set; }
        public FormFSDetailRpt CWSurfaceDressed { get; set; }
        public FormFSDetailRpt CWConcrete { get; set; }
        public FormFSDetailRpt CWGravel { get; set; }
        public FormFSDetailRpt CWEarth { get; set; }
        public FormFSDetailRpt CWSand { get; set; }
        public FormFSDetailRpt CLMPaint { get; set; }
        public FormFSDetailRpt CLMThermoplastic { get; set; }
        public FormFSDetailRpt LELMPaint { get; set; }
        public FormFSDetailRpt LELMThermoplastic { get; set; }
        public FormFSDetailRpt LDitchGravel { get; set; }
        public FormFSDetailRpt LDrainEarth { get; set; }
        public FormFSDetailRpt LDrainBlockstone { get; set; }
        public FormFSDetailRpt LDrainConcreate { get; set; }
        public FormFSDetailRpt LShoulderAsphalt { get; set; }
        public FormFSDetailRpt LShoulderConcrete { get; set; }
        public FormFSDetailRpt LShoulderEarth { get; set; }
        public FormFSDetailRpt LShoulderGravel { get; set; }
        public FormFSDetailRpt LShoulderFootpathkerb { get; set; }
        public FormFSDetailRpt RELMPaint { get; set; }
        public FormFSDetailRpt RELMThermoplastic { get; set; }
        public FormFSDetailRpt RDitchGravel { get; set; }
        public FormFSDetailRpt RDrainEarth { get; set; }
        public FormFSDetailRpt RDrainBlockstone { get; set; }
        public FormFSDetailRpt RDrainConcreate { get; set; }
        public FormFSDetailRpt RShoulderAsphalt { get; set; }
        public FormFSDetailRpt RShoulderConcrete { get; set; }
        public FormFSDetailRpt RShoulderEarth { get; set; }
        public FormFSDetailRpt RShoulderGravel { get; set; }
        public FormFSDetailRpt RShoulderFootpathkerb { get; set; }
        public FormFSDetailRpt RSLeft { get; set; }
        public FormFSDetailRpt RSCenter { get; set; }
        public FormFSDetailRpt RSRight { get; set; }
        public FormFSDetailRpt SignsDelineator { get; set; }
        public FormFSDetailRpt SignsWarning { get; set; }
        public FormFSDetailRpt SignsGantrySign { get; set; }
        public FormFSDetailRpt SignsGuideSign { get; set; }
        public FormFSDetailRpt CVConcreatePipe { get; set; }
        public FormFSDetailRpt CVConcreteBox { get; set; }
        public FormFSDetailRpt CVMetal { get; set; }
        public FormFSDetailRpt CVHDPE { get; set; }
        public FormFSDetailRpt CVOthers { get; set; }
        public FormFSDetailRpt BRConcConc { get; set; }
        public FormFSDetailRpt BRConcSteel { get; set; }
        public FormFSDetailRpt BRSteelTimber { get; set; }
        public FormFSDetailRpt BRSteelSteel { get; set; }
        public FormFSDetailRpt BRTimberTimber { get; set; }
        public FormFSDetailRpt BRTimberSteel { get; set; }
        public FormFSDetailRpt BRMansonry { get; set; }
        public FormFSDetailRpt BRElevatedViaduct { get; set; }
        public FormFSDetailRpt BRLongBridge { get; set; }
        public FormFSDetailRpt GRSteel { get; set; }
        public FormFSDetailRpt GRWire { get; set; }
        public FormFSDetailRpt GRPedestrialRailing { get; set; }
        public FormFSDetailRpt GRParapetWall { get; set; }
        public FormFSDetailRpt GROthers { get; set; }
        public FormFSDetailRpt RWReinforceConc { get; set; }
        public FormFSDetailRpt RWSteelMetal { get; set; }
        public FormFSDetailRpt RWMasonryGabion { get; set; }
        public FormFSDetailRpt RWPrecastPanel { get; set; }
        public FormFSDetailRpt RWTimber { get; set; }
        public FormFSDetailRpt RWSoliNail { get; set; }
        public FormFSDetailRpt RWOthers { get; set; }
    }

    public class FormFSDetailRpt
    {
        public double? AverageWidth { get; set; }
        public double? TotalLength { get; set; }
        public decimal? Condition1 { get; set; }
        public decimal? Condition2 { get; set; }
        public decimal? Condition3 { get; set; }
        public string Needed { get; set; }
        public string Remarks { get; set; }
    }
}
