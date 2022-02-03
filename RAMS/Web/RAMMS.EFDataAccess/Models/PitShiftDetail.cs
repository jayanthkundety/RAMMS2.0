using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PitShiftDetail
    {
        public int Pk { get; set; }
        public DateTime InspectionDate { get; set; }
        public int SectionPk { get; set; }
        public string Shift { get; set; }
        public string VehiclePlateNo { get; set; }
        public string SpeedometerStart { get; set; }
        public string SpeedometerEnd { get; set; }
        public string TeamLeadName { get; set; }
        public string TeamLeadMobile { get; set; }
        public string WorkerName1 { get; set; }
        public string WorkerName2 { get; set; }
        public string WorkerName3 { get; set; }
        public string WorkerTmcname { get; set; }
        public string WorkerTmcmobile { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string Code { get; set; }
    }
}
