using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormB1b2BrInsImage
    {
        public int FbriPkRefNo { get; set; }
        public int? FbriFbrihPkRefNo { get; set; }
        public string FbriImgRefId { get; set; }
        public string FbriImageTypeCode { get; set; }
        public int? FbriImageSrno { get; set; }
        public string FbriImageFilenameSys { get; set; }
        public string FbriImageFilenameUpload { get; set; }
        public string FbriImageUserFilePath { get; set; }
        public int? FbriModBy { get; set; }
        public DateTime? FbriModDt { get; set; }
        public int? FbriCrBy { get; set; }
        public DateTime? FbriCrDt { get; set; }
        public bool? FbriSubmitSts { get; set; }
        public bool? FbriActiveYn { get; set; }

        public virtual RmFormB1b2BrInsHdr FbriFbrihPkRefNoNavigation { get; set; }
    }
}
