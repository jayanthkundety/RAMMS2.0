using System;
using System.Collections;
using System.Collections.Generic;

namespace RAMMS.DTO.Report
{
    public class FormFCRpt
    {
        public string Division { get; set; }
        public string District { get; set; }
        public string RMU { get; set; }
        public string CrewLeader { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public string Remarks { get; set; }
        public IEnumerable<FormFCDetail> Details { get; set; }
        public string InspectedByName { get; set; }
        public DateTime? InspectedDate { get; set; }
        public decimal? RoadLength { get; set; }
        public string AssetTypes { get; set; }
        public string L_E_P { get; set; }
        public string L_E_T { get; set; }
        public string L_R { get; set; }
        public string C_P_A { get; set; }
        public string C_P_D { get; set; }
        public string C_P_G { get; set; }
        public string C_P_E { get; set; }
        public string C_P_C { get; set; }
        public string C_P_S { get; set; }
        public string C_R { get; set; }
        public string C_C_P { get; set; }
        public string C_C_T { get; set; }
        public string R_R { get; set; }
        public string R_E_P { get; set; }
        public string R_E_T { get; set; }
        public decimal? FromCH { get; set; }
        public decimal? ToCH { get; set; }
    }

    public class FormFCDetail
    {
        public int? Left_EdgeLine_Paint { get; set; }
        public int? Left_EdgeLine_Thermoplastic { get; set; }
        public int? Left_RoadStuds { get; set; }
        public int? CarriageWay_Pavment_Asphalt { get; set; }
        public int? CarriageWay_Pavment_SurfaceDressed { get; set; }
        public int? CarriageWay_Pavment_Gravel { get; set; }
        public int? CarriageWay_Pavement_Earth { get; set; }
        public int? CarriageWay_Pavment_Concrete { get; set; }
        public int? CarriageWay_Pavment_Sand { get; set; }
        public int? CarriageWay_CentreRoadStuds { get; set; }
        public int? CarriageWay_CentreLine_Paint { get; set; }
        public int? CarriageWay_CentreLine_Thermoplastic { get; set; }
        public int? Right_EdgeLine_Paint { get; set; }
        public int? Right_Thermoplastic { get; set; }
        public int? Right_RoadStuds { get; set; }
        public string KMTitle { get; set; }
        public decimal FromCh { get; set; }
    }
}
