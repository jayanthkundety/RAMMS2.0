using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormjImageDtl
    {
        public int FjiPkRefNo { get; set; }
        public int? FjiFjdPkRefNo { get; set; }
        public string FjiImageTypeCode { get; set; }
        public int? FjiImageSrno { get; set; }
        public string FjiImageFilenameSys { get; set; }
        public string FjiImageFilenameUpload { get; set; }
        public string FjiModBy { get; set; }
        public DateTime? FjiModDt { get; set; }
        public string FjiCrBy { get; set; }
        public DateTime? FjiCrDt { get; set; }
        public bool FjiSubmitSts { get; set; }
        public bool? FjiActiveYn { get; set; }
        public string FjiImageUserFilePath { get; set; }

        public virtual RmFormJDtl FjiFjdPkRefNoNavigation { get; set; }
    }
}
