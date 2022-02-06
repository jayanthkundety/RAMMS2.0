using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormW1ImageResponseDTO
    {
        public int PkRefNo { get; set; }
        public int? Fw1RefNo { get; set; }
        public string ImgRefId { get; set; }
        public string ImageTypeCode { get; set; }
        public int? ImageSrno { get; set; }
        public string ImageFilenameSys { get; set; }
        public string ImageFilenameUpload { get; set; }
        public string ImageUserFilePath { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }

    }
}
