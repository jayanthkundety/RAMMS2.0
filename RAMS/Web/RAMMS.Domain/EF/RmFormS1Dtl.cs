using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormS1Dtl
    {
        public RmFormS1Dtl()
        {
            RmFormN1Hdr = new HashSet<RmFormN1Hdr>();
            RmFormQa2Dtl = new HashSet<RmFormQa2Dtl>();
            RmFormS1WkDtl = new HashSet<RmFormS1WkDtl>();
        }

        public int FsidPkRefNo { get; set; }
        public int? FsidFsihPkRefNo { get; set; }
        public int? FsidActId { get; set; }
        public int? FsiidRoadId { get; set; }
        public int? FsidFrmChKm { get; set; }
        public string FsidFrmChM { get; set; }
        public int? FsidToChKm { get; set; }
        public string FsidToChM { get; set; }
        public string FsidFormType { get; set; }
        public string FsidRefId { get; set; }
        public int? FsidCrewSupervisor { get; set; }
        public string FsidCrewSupervisorName { get; set; }
        public bool? FsidSentToJkrs { get; set; }
        public bool? FsidRcvFromJkrs { get; set; }
        public DateTime? FsidSentToJkrsDt { get; set; }
        public DateTime? FsidRcvFromJkrsDt { get; set; }
        public string FsidRemarks { get; set; }
        public int? FsidFapRa { get; set; }
        public int? FsidFapMt { get; set; }
        public int? FsidFapQa1 { get; set; }
        public int? FsidFapQa2 { get; set; }
        public int? FsidFapSa { get; set; }
        public int? FsidFapN1 { get; set; }
        public int? FsidFapN2 { get; set; }
        public int? FsidModBy { get; set; }
        public DateTime? FsidModDt { get; set; }
        public int? FsidCrBy { get; set; }
        public DateTime? FsidCrDt { get; set; }
        public bool FsidSubmitSts { get; set; }
        public bool? FsidActiveYn { get; set; }
        public int? FsidFormTypeRefNo { get; set; }
        public string FsidActCode { get; set; }
        public string FsidActName { get; set; }
        public string FsiidRoadCode { get; set; }
        public string FsiidRoadName { get; set; }
        public string FsidFormASiteRef { get; set; }
        public string FsidFormAPriority { get; set; }
        public string FsidFormAWorkQty { get; set; }
        public string FsidFormACdr { get; set; }

        public virtual RmFormS1Hdr FsidFsihPkRefNoNavigation { get; set; }
        public virtual ICollection<RmFormN1Hdr> RmFormN1Hdr { get; set; }
        public virtual ICollection<RmFormQa2Dtl> RmFormQa2Dtl { get; set; }
        public virtual ICollection<RmFormS1WkDtl> RmFormS1WkDtl { get; set; }
    }
}
