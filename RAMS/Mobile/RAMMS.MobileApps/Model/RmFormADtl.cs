using System;
using System.Collections.Generic;

namespace RAMMS.MobileApps
{
    public partial class RmFormADtl
    {
        public RmFormADtl()
        {
            RmFormASiterefDtl = new HashSet<RmFormASiterefDtl>();
            RmFormHHdr = new HashSet<RmFormHHdr>();
            RmFormaImageDtl = new HashSet<RmFormaImageDtl>();
        }

        public int FadPkRefNo { get; set; }
        public int? FadFahPkRefNo { get; set; }
        public DateTime? FadDt { get; set; }
        public int? FadSrno { get; set; }
        public string FadAssetId { get; set; }
        public string FadSiteRef { get; set; }
        public int? FadFrmCh { get; set; }
        public int? FadFrmChDeci { get; set; }
        public int? FadToCh { get; set; }
        public int? FadToChDeci { get; set; }
        public string FadDefCodeLhs { get; set; }
        public string FadDefCodeCl { get; set; }
        public string FadDefCodeRhs { get; set; }
        public string FadDefCode { get; set; }
        public string FadActCode { get; set; }
        public string FadUnit { get; set; }
        public int? FadLength { get; set; }
        public int? FadWidth { get; set; }
        public int? FadHeight { get; set; }
        public string FadAdp { get; set; }
        public string FadCdr { get; set; }
        public string FadPriority { get; set; }
        public int? FadWi { get; set; }
        public int? FadWs { get; set; }
        public int? FadWtc { get; set; }
        public int? FadWc { get; set; }
        public int? FadSftPs { get; set; }
        public int? FadSftWis { get; set; }
        public int? FadRt { get; set; }
        public string FadRemarks { get; set; }
        public string FadFormhApp { get; set; }
        public string FadModBy { get; set; }
        public DateTime? FadModDt { get; set; }
        public string FadCrBy { get; set; }
        public DateTime? FadCrDt { get; set; }
        public bool FadSubmitSts { get; set; }
        public bool? FadActiveYn { get; set; }
        public string FadDesc { get; set; }

        public virtual RmFormAHdr FadFahPkRefNoNavigation { get; set; }
        public virtual ICollection<RmFormASiterefDtl> RmFormASiterefDtl { get; set; }
        public virtual ICollection<RmFormHHdr> RmFormHHdr { get; set; }
        public virtual ICollection<RmFormaImageDtl> RmFormaImageDtl { get; set; }
    }
}
