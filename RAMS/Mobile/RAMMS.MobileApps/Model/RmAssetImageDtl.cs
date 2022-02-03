using System;
using System.Collections.Generic;

namespace RAMMS.MobileApps
{
    public partial class RmAssetImageDtl
    {
        public int AidPkRefNo { get; set; }
        public int? AidAiPkRefNo { get; set; }
        public string AidImageTypeCode { get; set; }
        public int? AidImageSrno { get; set; }
        public string AidImageFilenameSys { get; set; }
        public string AidImageFilenameUpload { get; set; }
        public string AidModBy { get; set; }
        public DateTime? AidModDt { get; set; }
        public string AidCrBy { get; set; }
        public DateTime? AidCrDt { get; set; }
        public bool AidSubmitSts { get; set; }
        public bool? AidActiveYn { get; set; }

        public virtual RmAllassetInventory AidAiPkRefNoNavigation { get; set; }
    }
}
