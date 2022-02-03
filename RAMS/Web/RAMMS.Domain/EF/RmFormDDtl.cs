using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormDDtl
    {
        public RmFormDDtl()
        {
            RmAccUcuImageDtl = new HashSet<RmAccUcuImageDtl>();
            RmFormXHdr = new HashSet<RmFormXHdr>();
            RmWarImageDtl = new HashSet<RmWarImageDtl>();
        }

        public int FddPkRefNo { get; set; }
        public int? FddFdhPkRefNo { get; set; }
        public string FddRoadCode { get; set; }
        public int? FddFrmCh { get; set; }
        public int? FddFrmChDeci { get; set; }
        public int? FddToCh { get; set; }
        public int? FddToChDeci { get; set; }
        public string FddSiteRef { get; set; }
        public int? FddActCode { get; set; }
        public string FddTimeArr { get; set; }
        public string FddTimeDep { get; set; }
        public double? FddLength { get; set; }
        public double? FddWidth { get; set; }
        public double? FddHeight { get; set; }
        public string FddUnit { get; set; }
        public int? FddProdQty { get; set; }
        public string FddProdUnit { get; set; }
        public string FddWorkSts { get; set; }
        public string FddGenRemarks { get; set; }
        public string FddRemarks { get; set; }
        public string FddModBy { get; set; }
        public DateTime? FddModDt { get; set; }
        public string FddCrBy { get; set; }
        public DateTime? FddCrDt { get; set; }
        public bool FddSubmitSts { get; set; }
        public bool? FddActiveYn { get; set; }
        public string FddSourceType { get; set; }
        public int? FddFxhPkRefNo { get; set; }
        public string FddRefId { get; set; }
        public string FddSourceRefId { get; set; }
        public int? FddSrno { get; set; }

        public virtual RmFormDHdr FddFdhPkRefNoNavigation { get; set; }
        public virtual ICollection<RmAccUcuImageDtl> RmAccUcuImageDtl { get; set; }
        public virtual ICollection<RmFormXHdr> RmFormXHdr { get; set; }
        public virtual ICollection<RmWarImageDtl> RmWarImageDtl { get; set; }
    }
}
