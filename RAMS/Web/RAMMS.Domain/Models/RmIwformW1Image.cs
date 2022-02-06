using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmIwFormW1Image
    {
        public int Fw1iPkRefNo { get; set; }
        public int? Fw1iFw1RefNo { get; set; }
        public string Fw1iImgRefId { get; set; }
        public string Fw1iImageTypeCode { get; set; }
        public int? Fw1iImageSrno { get; set; }
        public string Fw1iImageFilenameSys { get; set; }
        public string Fw1iImageFilenameUpload { get; set; }
        public string Fw1iImageUserFilePath { get; set; }
        public int? Fw1iModBy { get; set; }
        public DateTime? Fw1iModDt { get; set; }
        public int? Fw1iCrBy { get; set; }
        public DateTime? Fw1iCrDt { get; set; }
        public bool Fw1iSubmitSts { get; set; }
        public bool? Fw1iActiveYn { get; set; }

        public virtual RmIwFormW1 Fw1iFw1RefNoNavigation { get; set; }
    }
}
