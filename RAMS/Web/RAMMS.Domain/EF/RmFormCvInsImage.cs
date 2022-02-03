using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormCvInsImage
    {
        public int FcviPkRefNo { get; set; }
        public int? FcviFcvihPkRefNo { get; set; }
        public string FcviImgRefId { get; set; }
        public string FcviImageTypeCode { get; set; }
        public int? FcviImageSrno { get; set; }
        public string FcviImageFilenameSys { get; set; }
        public string FcviImageFilenameUpload { get; set; }
        public string FcviImageUserFilePath { get; set; }
        public int? FcviModBy { get; set; }
        public DateTime? FcviModDt { get; set; }
        public int? FcviCrBy { get; set; }
        public DateTime? FcviCrDt { get; set; }
        public bool FcviSubmitSts { get; set; }
        public bool? FcviActiveYn { get; set; }

        public virtual RmFormCvInsHdr FcviFcvihPkRefNoNavigation { get; set; }
    }
}
