using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormRImages
    {
        public int FriPkRefNo { get; set; }
        public int? FriFr1hPkRefNo { get; set; }
        public string FriImgRefId { get; set; }
        public string FriImageTypeCode { get; set; }
        public int? FriImageSrno { get; set; }
        public string FriImageFilenameSys { get; set; }
        public string FriImageFilenameUpload { get; set; }
        public string FriImageUserFilePath { get; set; }
        public int? FriModBy { get; set; }
        public DateTime? FriModDt { get; set; }
        public int? FriCrBy { get; set; }
        public DateTime? FriCrDt { get; set; }
        public bool FriSubmitSts { get; set; }
        public bool FriActiveYn { get; set; }
    }
}
