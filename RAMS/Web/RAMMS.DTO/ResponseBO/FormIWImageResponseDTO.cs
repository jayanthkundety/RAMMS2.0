using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormIWImageResponseDTO
    {
        public int PkRefNo { get; set; }
 
        public string Fw1IwRefNo { get; set; }
        public string ImgRefId { get; set; }
        public string ImageTypeCode { get; set; }
        public int? ImageSrno { get; set; }
        public string ImageFilenameSys { get; set; }
        public string ImageFilenameUpload { get; set; }
        public string ImageUserFilePath { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string Source { get; set; }

        public virtual RmIwFormW1 Fw1PkRefNoNavigation { get; set; }

    }
}
