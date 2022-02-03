using System;
using System.Collections.Generic;

namespace RAMMS.DTO
{
    public class FORMJRpt
    {
        public FORMJHeaderRpt Header { get; set; }
        public List<FORMJDetailRpt> Details { get; set; }
    }

    public class FORMJHeaderRpt
    {
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public string RMUCode { get; set; }
        public string PageIndex { get; set; }
        public string PageCount { get; set; }
        public string RMUName { get; set; }
        public string CheckedBY { get; set; }
        public DateTime? CheckedDate { get; set; }
        public string CheckedByDesignation { get; set; }
        public string InspectedBy { get; set; }
        public DateTime? InspectedDate { get; set; }
        public string InspectedByDesignation { get; set; }
        public string AuditedBy { get; set; }
        public DateTime? AuditedDate { get; set; }
        public string AuditedByDesignation { get; set; }
        public string RefId { get; set; }
    }

    public class FORMJDetailRpt
    {
        public string Date { get; set; }
        public string RefId { get; set; }
        public string OfficeRef { get; set; }
        public string SiteRef { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string SACode { get; set; }
        public string Dificiencies { get; set; }
        public string WorkInstallation { get; set; }
        public string Dimention { get; set; }
        public string Pr { get; set; }
        public string WI { get; set; }
        public string WS { get; set; }
        public string WTC { get; set; }
        public string WC { get; set; }
        public string RT { get; set; }
        public string Remarks { get; set; }
    }
}
