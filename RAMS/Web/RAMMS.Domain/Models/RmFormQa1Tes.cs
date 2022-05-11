using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormQa1Tes
    {
        public RmFormQa1Tes()
        {
            RmFormQa1Image = new HashSet<RmFormQa1Image>();
        }

        public int Fqa1tesPkRefNo { get; set; }
        public int? Fqa1tesFqa1hPkRefNo { get; set; }
        public string Fqa1tesCtCs { get; set; }
        public int? Fqa1tesCtCsA { get; set; }
        public string Fqa1tesCtCsRemark { get; set; }
        public string Fqa1tesDtCs { get; set; }
        public int? Fqa1tesDtCsA { get; set; }
        public string Fqa1tesDtCsRemark { get; set; }
        public string Fqa1tesMgtCs { get; set; }
        public int? Fqa1tesMgtCsA { get; set; }
        public string Fqa1tesMgtCsRemark { get; set; }
        public string Fqa1tesCbrCs { get; set; }
        public int? Fqa1tesCbrCsA { get; set; }
        public string Fqa1tesCbrCsRemark { get; set; }
        public string Fqa1tesOtCs { get; set; }
        public int? Fqa1tesOtCsA { get; set; }
        public string Fqa1tesOtCsRemark { get; set; }
        public bool Fqa1tesActiveYn { get; set; }
        public int? Fqa1tesModBy { get; set; }
        public DateTime? Fqa1tesModDt { get; set; }
        public int? Fqa1tesCrBy { get; set; }
        public DateTime? Fqa1tesCrDt { get; set; }

        public virtual RmFormQa1Hdr Fqa1tesFqa1hPkRefNoNavigation { get; set; }
        public virtual ICollection<RmFormQa1Image> RmFormQa1Image { get; set; }
    }
}
