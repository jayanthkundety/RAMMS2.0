using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormV5Dtl
    {
        public int Fv5dPkRefNo { get; set; }
        public int? Fv5dFv5hPkRefNo { get; set; }
        public string Fv5dFileNameFrm { get; set; }
        public string Fv5dFileNameTo { get; set; }
        public string Fv5dDesc { get; set; }
        public string Fv5dImageFilenameSys { get; set; }
        public string Fv5dImageFilenameUpload { get; set; }
        public string Fv5dImageUserFilePath { get; set; }
        public int? Fv5dModBy { get; set; }
        public DateTime? Fv5dModDt { get; set; }
        public int? Fv5dCrBy { get; set; }
        public DateTime? Fv5dCrDt { get; set; }
        public bool Fv5dSubmitSts { get; set; }
        public bool Fv5dActiveYn { get; set; }
    }
}
