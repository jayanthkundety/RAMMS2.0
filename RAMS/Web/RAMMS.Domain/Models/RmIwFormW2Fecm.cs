using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmIwFormW2Fecm
    {
        public int FecmPkRefNo { get; set; }
        public int? FecmFw2PkRefNo { get; set; }
        public string FecmIwRefNo { get; set; }
        public string FecmProjectTitle { get; set; }
        public DateTime? FecmDtTecm { get; set; }
        public DateTime? FecmDt { get; set; }
        public string FecmSts { get; set; }
        public string FecmStsRemarks { get; set; }
        public bool? FecmIsBq { get; set; }
        public bool? FecmIsDrawing { get; set; }
        public bool? FecmAgreedNegoLetrYn { get; set; }
        public DateTime? FecmDtAgreedNego { get; set; }
        public double? FecmProgressPerc { get; set; }
        public string FecmRemark { get; set; }
        public int? FecmModBy { get; set; }
        public DateTime? FecmModDt { get; set; }
        public int? FecmCrBy { get; set; }
        public DateTime? FecmCrDt { get; set; }
        public bool FecmSubmitSts { get; set; }
        public bool FecmActiveYn { get; set; }

        public virtual RmIwFormW2 FecmFw2PkRefNoNavigation { get; set; }
    }
}
