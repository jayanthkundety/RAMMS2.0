using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormGImages
    {
        public int FgiPkRefNo { get; set; }
        public int? FgiFg1hPkRefNo { get; set; }
        public string FgiImgRefId { get; set; }
        public string FgiImageTypeCode { get; set; }
        public int? FgiImageSrno { get; set; }
        public string FgiImageFilenameSys { get; set; }
        public string FgiImageFilenameUpload { get; set; }
        public string FgiImageUserFilePath { get; set; }
        public int? FgiModBy { get; set; }
        public DateTime? FgiModDt { get; set; }
        public int? FgiCrBy { get; set; }
        public DateTime? FgiCrDt { get; set; }
        public bool FgiSubmitSts { get; set; }
        public bool FgiActiveYn { get; set; }

        public virtual RmFormG1Hdr FgiFg1hPkRefNoNavigation { get; set; }
    }
}
