using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmIwformImage
    {
        public int FiwiPkRefNo { get; set; }
        public int? FiwiFw1PkRefNo { get; set; }
        public string FiwiFw1IwRefNo { get; set; }
        public string FiwiImgRefId { get; set; }
        public string FiwiImageTypeCode { get; set; }
        public int? FiwiImageSrno { get; set; }
        public string FiwiImageFilenameSys { get; set; }
        public string FiwiImageFilenameUpload { get; set; }
        public string FiwiImageUserFilePath { get; set; }
        public int? FiwiModBy { get; set; }
        public DateTime? FiwiModDt { get; set; }
        public int? FiwiCrBy { get; set; }
        public DateTime? FiwiCrDt { get; set; }
        public bool FiwiSubmitSts { get; set; }
        public bool FiwiActiveYn { get; set; }
        public string FiwiSource { get; set; }

        public virtual RmIwFormW1 FiwiFw1PkRefNoNavigation { get; set; }
    }
}
