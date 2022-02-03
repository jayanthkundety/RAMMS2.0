using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormaImageDtl
    {
        public int FaiPkRefNo { get; set; }
        public int? FaiFadPkRefNo { get; set; }
        public string FaiImageTypeCode { get; set; }
        public int? FaiImageSrno { get; set; }
        public string FaiImageFilenameSys { get; set; }
        public string FaiImageFilenameUpload { get; set; }
        public string FaiModBy { get; set; }
        public DateTime? FaiModDt { get; set; }
        public string FaiCrBy { get; set; }
        public DateTime? FaiCrDt { get; set; }
        public bool FaiSubmitSts { get; set; }
        public bool? FaiActiveYn { get; set; }
        public string FaiImageUserFilePath { get; set; }

        public virtual RmFormADtl FaiFadPkRefNoNavigation { get; set; }
    }
}
