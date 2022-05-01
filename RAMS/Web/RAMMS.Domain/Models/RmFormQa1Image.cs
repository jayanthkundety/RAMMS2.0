using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormQa1Image
    {
        public int Fqa1iPkRefNo { get; set; }
        public int? Fqa1iFqa1PkRefNo { get; set; }
        public int? Fqa1iFqa1TesPkRefNo { get; set; }
        public string Fqa1iImgRefId { get; set; }
        public string Fqa1iImageTypeCode { get; set; }
        public int? Fqa1iImageSrno { get; set; }
        public string Fqa1iImageFilenameSys { get; set; }
        public string Fqa1iImageFilenameUpload { get; set; }
        public string Fqa1iImageUserFilePath { get; set; }
        public int? Fqa1iModBy { get; set; }
        public DateTime? Fqa1iModDt { get; set; }
        public int? Fqa1iCrBy { get; set; }
        public DateTime? Fqa1iCrDt { get; set; }
        public bool Fqa1iSubmitSts { get; set; }
        public bool Fqa1iActiveYn { get; set; }
        public string Fqa1iSource { get; set; }

        public virtual RmFormQa1Tes Fqa1iFqa1TesPkRefNoNavigation { get; set; }
    }
}
