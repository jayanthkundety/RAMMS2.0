using System;
using System.Collections.Generic;

namespace RAMMS.MobileApps
{
    public partial class RmAccUcuImageDtl
    {
        public int FauPkRefNo { get; set; }
        public int? FauFddPkRefNo { get; set; }
        public int? FauFxhPkRefNo { get; set; }
        public string FauAccUcu { get; set; }
        public int? FauImageSrno { get; set; }
        public string FauImageFilenameSys { get; set; }
        public string FauImageFilenameUpload { get; set; }
        public string FauModBy { get; set; }
        public DateTime? FauModDt { get; set; }
        public string FauCrBy { get; set; }
        public DateTime? FauCrDt { get; set; }
        public bool FauSubmitSts { get; set; }
        public bool? FauActiveYn { get; set; }

        public virtual RmFormDDtl FauFddPkRefNoNavigation { get; set; }
        public virtual RmFormXHdr FauFxhPkRefNoNavigation { get; set; }
    }
}
