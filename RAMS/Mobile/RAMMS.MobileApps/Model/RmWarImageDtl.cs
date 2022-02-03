using System;
using System.Collections.Generic;

namespace RAMMS.MobileApps
{
    public partial class RmWarImageDtl
    {
        public int FwarPkRefNo { get; set; }
        public int? FwarFddPkRefNo { get; set; }
        public int? FwarFxhPkRefNo { get; set; }
        public string FwarImageTypeCode { get; set; }
        public int? FwarImageSrno { get; set; }
        public string FwarImageFilenameSys { get; set; }
        public string FwarImageFilenameUpload { get; set; }
        public string FwarModBy { get; set; }
        public DateTime? FwarModDt { get; set; }
        public string FwarCrBy { get; set; }
        public DateTime? FwarCrDt { get; set; }
        public bool FwarSubmitSts { get; set; }
        public bool? FwarActiveYn { get; set; }

        public virtual RmFormDDtl FwarFddPkRefNoNavigation { get; set; }
        public virtual RmFormXHdr FwarFxhPkRefNoNavigation { get; set; }
    }
}
