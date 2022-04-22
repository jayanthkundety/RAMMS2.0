using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormQa1Lab
    {
        public int Fqa1lPkRefNo { get; set; }
        public int? Fqa1lFqa1hPkRefNo { get; set; }
        public int? Fqa1lLabCsOnSite { get; set; }
        public int? Fqa1lLabCsOnLeave { get; set; }
        public string Fqa1lLabCsPerfStd { get; set; }
        public string Fqa1lLabCsRemark { get; set; }
        public int? Fqa1lLabOpOnSite { get; set; }
        public int? Fqa1lLabOpOnLeave { get; set; }
        public string Fqa1lLabOpPerfStd { get; set; }
        public string Fqa1lLabOpRemark { get; set; }
        public int? Fqa1lLabDrOnSite { get; set; }
        public int? Fqa1lLabDrOnLeave { get; set; }
        public string Fqa1lLabDrPerfStd { get; set; }
        public string Fqa1lLabDrRemark { get; set; }
        public int? Fqa1lLabWmOnSite { get; set; }
        public int? Fqa1lLabWmOnLeave { get; set; }
        public string Fqa1lLabWmPerFStd { get; set; }
        public string Fqa1lLabWmRemark { get; set; }
        public int? Fqa1lLabOthOnSite { get; set; }
        public int? Fqa1lLabOthOnLeave { get; set; }
        public string Fqa1lLabOthPerfStd { get; set; }
        public string Fqa1lLabOthRemark { get; set; }
        public int? Fqa1lModBy { get; set; }
        public DateTime? Fqa1lModDt { get; set; }
        public int? Fqa1lCrBy { get; set; }
        public DateTime? Fqa1lCrDt { get; set; }
        public bool Fqa1lActiveYn { get; set; }

        public virtual RmFormQa1Hdr Fqa1lFqa1hPkRefNoNavigation { get; set; }
    }
}
