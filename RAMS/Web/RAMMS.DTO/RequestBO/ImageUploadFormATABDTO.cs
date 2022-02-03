using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.RequestBO
{
    public class ImageUploadFormATABDTO
    {
        public byte[] FileContent { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
    }
}
