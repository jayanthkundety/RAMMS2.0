using System;
using System.Collections.Generic;

namespace RAMMS.DTO.Report
{
    public class FormFDRpt
    {
        public string Division { get; set; }
        public string District { get; set; }
        public string RMU { get; set; }
        public string CrewLeader { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public string Remarks { get; set; }
        public IEnumerable<FormFDDetail> Details { get; set; }
        public string InspectedByName { get; set; }
        public DateTime? InspectedDate { get; set; }
        public decimal? RoadLength { get; set; }
        public string AssetTypes { get; set; }
        public decimal? FromCH { get; set; }
        public decimal? ToCH { get; set; }
        public string L_DI_G { get; set; }
        public string L_DR_E { get; set; }
        public string L_DR_B { get; set; }
        public string L_DR_C { get; set; }
        public string L_SH_A { get; set; }
        public string L_SH_C { get; set; }
        public string L_SH_E { get; set; }
        public string L_SH_G { get; set; }
        public string L_SH_F { get; set; }
        public string R_DI_G { get; set; }
        public string R_DR_E { get; set; }
        public string R_DR_B { get; set; }
        public string R_DR_C { get; set; }
        public string R_SH_A { get; set; }
        public string R_SH_C { get; set; }
        public string R_SH_E { get; set; }
        public string R_SH_G { get; set; }
        public string R_SH_F { get; set; }
    }

    public class FormFDDetail
    {
        public int? Left_Ditch_GravelSandEarth { get; set; }
        public int? Left_Drain_Earth { get; set; }
        public int? Left_Drain_Blockstone { get; set; }
        public int? Left_Drain_Concrete { get; set; }
        public int? Left_Shoulder_Asphalt { get; set; }
        public int? Left_Shoulder_Concrete { get; set; }
        public int? Left_Shoulder_Earth { get; set; }
        public int? Left_Shoulder_Gravel { get; set; }
        public int? Left_Shoulder_FootpathKerb { get; set; }
        public int? Right_Ditch_GravelSandEarth { get; set; }
        public int? Right_Drain_Earth { get; set; }
        public int? Right_Drain_Blockstone { get; set; }
        public int? Right_Drain_Concrete { get; set; }
        public int? Right_Shoulder_Asphalt { get; set; }
        public int? Right_Shoulder_Concrete { get; set; }
        public int? Right_Shoulder_Earth { get; set; }
        public int? Right_Shoulder_Gravel { get; set; }
        public int? Right_Shoulder_FootpathKerb { get; set; }
        public string KMTitle { get; set; }
        public decimal FromCh { get; set; }
    }
}
