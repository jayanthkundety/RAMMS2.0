using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormQa2Dtl
    {
        public RmFormQa2Dtl()
        {
            RmFormN1Hdr = new HashSet<RmFormN1Hdr>();
        }

        public int FqaiidPkRefNo { get; set; }
        public int? FqaiidFqaiihPkRefNo { get; set; }
        public string FqaiidSourceType { get; set; }
        public string FqaiidRefId { get; set; }
        public int? FqaiidSrno { get; set; }
        public string FqaiidSiteRef { get; set; }
        public int? FqaiidFrmCh { get; set; }
        public int? FqaiidFrmChDeci { get; set; }
        public int? FqaiidToCh { get; set; }
        public int? FqaiidToChDeci { get; set; }
        public string FqaiidDefCode { get; set; }
        public string FqaiidWrkAct { get; set; }
        public int? FqaiidInitialCond { get; set; }
        public DateTime? FqaiidDtInitialCond { get; set; }
        public int? FqaiidQaI { get; set; }
        public DateTime? FqaiidDtQaI { get; set; }
        public int? FqaiidQaIi { get; set; }
        public DateTime? FqaiidDtQaIi { get; set; }
        public int? FqaiidQaIii { get; set; }
        public DateTime? FqaiidDtQaIii { get; set; }
        public int? FqaiidQaIv { get; set; }
        public DateTime? FqaiidDtQaIv { get; set; }
        public string FqaiidDefDesc { get; set; }
        public string FqaiidWws13aFol { get; set; }
        public string FqaiidRemarks { get; set; }
        public string FqaiidModBy { get; set; }
        public DateTime? FqaiidModDt { get; set; }
        public string FqaiidCrBy { get; set; }
        public DateTime? FqaiidCrDt { get; set; }
        public bool FqaiidSubmitSts { get; set; }
        public bool? FqaiidActiveYn { get; set; }
        public bool? FqaiihNcnYn { get; set; }
        public string FqaiidIniCycType { get; set; }
        public string FqaiidQaiCycType { get; set; }
        public string FqaiidQaiiCycType { get; set; }
        public string FqaiidQaiiiCycType { get; set; }
        public string FqaiidQaivCycType { get; set; }
        public double? FqaiidRwrkDimH { get; set; }
        public double? FqaiidRwrkDimW { get; set; }
        public double? FqaiidRwrkDimL { get; set; }
        public int? FqaiidFsidPkRefNo { get; set; }

        public virtual RmFormQa2Hdr FqaiidFqaiihPkRefNoNavigation { get; set; }
        public virtual RmFormS1Dtl FqaiidFsidPkRefNoNavigation { get; set; }
        public virtual ICollection<RmFormN1Hdr> RmFormN1Hdr { get; set; }
    }
}
