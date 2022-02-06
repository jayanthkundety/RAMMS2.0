using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmIwFormW2Image
    {
        public int Fw2iPkRefNo { get; set; }
        public int? Fw2iFw2RefNo { get; set; }
        public string Fw2iImgRefId { get; set; }
        public string Fw2iImageTypeCode { get; set; }
        public int? Fw2iImageSrno { get; set; }
        public string Fw2iImageFilenameSys { get; set; }
        public string Fw2iImageFilenameUpload { get; set; }
        public string Fw2iImageUserFilePath { get; set; }
        public int? Fw2iModBy { get; set; }
        public DateTime? Fw2iModDt { get; set; }
        public int? Fw2iCrBy { get; set; }
        public DateTime? Fw2iCrDt { get; set; }
        public bool Fw2iSubmitSts { get; set; }
        public bool? Fw2iActiveYn { get; set; }

        public virtual RmIwFormW2 Fw2iFw2RefNoNavigation { get; set; }
    }
}
