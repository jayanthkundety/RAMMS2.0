using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormJDtl
    {
        public RmFormJDtl()
        {
            RmFormHHdr = new HashSet<RmFormHHdr>();
            RmFormjImageDtl = new HashSet<RmFormjImageDtl>();
        }

        public int FjdPkRefNo { get; set; }
        public int? FjdFjhPkRefNo { get; set; }
        public DateTime? FjdDt { get; set; }
        public int? FjdSrno { get; set; }
        public string FjdAssetId { get; set; }
        public string FjdSiteRef { get; set; }
        public int? FjdFrmCh { get; set; }
        public string FjdFrmChDeci { get; set; }
        public int? FjdToCh { get; set; }
        public string FjdToChDeci { get; set; }
        public string FjdDefCode { get; set; }
        public string FjdPrblmDesc { get; set; }
        public string FjdWrkNeed { get; set; }
        public double? FjdLength { get; set; }
        public double? FjdWidth { get; set; }
        public double? FjdHeight { get; set; }
        public string FjdPriority { get; set; }
        public int? FjdWi { get; set; }
        public int? FjdWs { get; set; }
        public int? FjdWtc { get; set; }
        public int? FjdWc { get; set; }
        public int? FjdRt { get; set; }
        public string FjdRemarks { get; set; }
        public string FjdFormhApp { get; set; }
        public string FjdModBy { get; set; }
        public DateTime? FjdModDt { get; set; }
        public string FjdCrBy { get; set; }
        public DateTime? FjdCrDt { get; set; }
        public bool FjdSubmitSts { get; set; }
        public bool? FjdActiveYn { get; set; }
        public string FjdRefId { get; set; }

        public virtual RmFormJHdr FjdFjhPkRefNoNavigation { get; set; }
        public virtual ICollection<RmFormHHdr> RmFormHHdr { get; set; }
        public virtual ICollection<RmFormjImageDtl> RmFormjImageDtl { get; set; }
    }
}
