using System;
using System.Collections.Generic;

namespace RAMMS.DTO.Report
{
    public class FORMQA2Rpt
    {
        public FORMQAHeaderRpt Header { get; set; }
        public IEnumerable<FORMQa2DetailRpt> Detail { get; set; }
    }

    public class FORMQAHeaderRpt
    {
        public int No { get; set; }
        public string RMU { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public string CrewSupervisor { get; set; }
        public string ReferenceNo { get; set; }
        public string InitialConSignDate { get; set; }
        public string InitialConName { get; set; }
        public string InitialConDesignation { get; set; }
        public string InitialRemarks { get; set; }
        public string ISignDate { get; set; }
        public string IName { get; set; }
        public string IDesignation { get; set; }
        public string IRemarks { get; set; }
        public string IISignDate { get; set; }
        public string IIName { get; set; }
        public string IIDesignation { get; set; }
        public string IIRemarks { get; set; }
        public string IIISignDate { get; set; }
        public string IIIName { get; set; }
        public string IIIDesignation { get; set; }
        public string IIIRemarks { get; set; }
        public string IVSignDate { get; set; }
        public string IVName { get; set; }
        public string IVDesignation { get; set; }
        public string IVRemarks { get; set; }
        public string Comments { get; set; }
    }

    public class FORMQa2DetailRpt
    {

        public int Item { get; set; }
        public string DateI { get; set; }
        public string DateII { get; set; }
        public string DateIII { get; set; }
        public string DateIV { get; set; }
        public string DateV { get; set; }
        public string SiteRef { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string Defect { get; set; }
        public string WorkActivity { get; set; }
        public string InitCondRating { get; set; }
        public string IRating { get; set; }
        public string IIRating { get; set; }
        public string IIIRating { get; set; }
        public string IVRating { get; set; }
        public string DefectDescription { get; set; }
        public string ReworkDimention { get; set; }
        public string WWS { get; set; }
        public string RemarksComments { get; set; }

        public double? DimLen { get; set; }
        public double? DimWid { get; set; }
        public double? DimHeight { get; set; }
        public int? DtlNo { get; set; }
    }
}
