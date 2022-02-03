using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.RequestBO
{
    public class FormJImageListResponseDTO
    {
        public int ImageId { get; set; }

        public int AssetId { get; set; }

        public string ImageTypeCode { get; set; }

        public int SNO { get; set; }

        public string ImageFilenameSys { get; set; }

        public string ImageFilename { get; set; }

        public string FileName { get; set; }

        public string ModifyBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool SubmitStatus { get; set; }

        public bool ActiveYn { get; set; }
    }
}
