using System;
using System.Collections.Generic;

namespace RAMMS.MobileApps
{
    public partial class RmFormhImageDtl
    {
        public int FhiPkRefNo { get; set; }
        public int? FhiFhhPkRefNo { get; set; }
        public string FhiImageTypeCode { get; set; }
        public int? FhiImageSrno { get; set; }
        public string FhiImageFilenameSys { get; set; }
        public string FhiImageFilenameUpload { get; set; }
        public string FhiModBy { get; set; }
        public DateTime? FhiModDt { get; set; }
        public string FhiCrBy { get; set; }
        public DateTime? FhiCrDt { get; set; }
        public bool FhiSubmitSts { get; set; }
        public bool? FhiActiveYn { get; set; }

        public virtual RmFormHHdr FhiFhhPkRefNoNavigation { get; set; }
    }
}
