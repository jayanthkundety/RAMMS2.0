using System;
using System.Collections.Generic;

namespace RAMMS.DTO
{
    public class FORMARpt
    {
        public FORMAHeaderRpt Header { get; set; }
        public List<FORMADetailRpt> Detail { get; set; }
    }

    public class FORMAHeaderRpt
    {
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public string RMUCode { get; set; }
        public string PageIndex { get; set; }
        public string PageCount { get; set; }
        public string RMUName { get; set; }
        public string VerifiedBY { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public string VerifiedByDesignation { get; set; }
        public string InspectedBy { get; set; }
        public DateTime? InspectedDate { get; set; }
        public string InspectedByDesignation { get; set; }
        public string RefId { get; set; }
    }

    public class FORMADetailRpt
    {
        public string Date { get; set; }
        public string RefId { get; set; }
        public string OfficeRef { get; set; }
        public string SiteRef { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string L_R { get; set; }
        public string L_D { get; set; }
        public string L_Sh { get; set; }
        public string L_EL { get; set; }
        public string L_P { get; set; }
        public string CL { get; set; }
        public string R_P { get; set; }
        public string R_EL { get; set; }
        public string R_Sh { get; set; }
        public string Cul { get; set; }
        public string Br { get; set; }
        public string Description { get; set; }
        public string ActivityCode { get; set; }
        public string Unit { get; set; }
        public string Dimention { get; set; }
        public string ADP { get; set; }
        public string CDR { get; set; }
        public string Pr { get; set; }
        public string WI { get; set; }
        public string WS { get; set; }
        public string WTC { get; set; }
        public string WC { get; set; }
        public string PS { get; set; }
        public string WIS { get; set; }
        public string RT { get; set; }
        public string Remarks { get; set; }
        public string R_D { get; set; }
        public string R_R { get; set; }
        public string DefCode { get; set; }
    }
}
