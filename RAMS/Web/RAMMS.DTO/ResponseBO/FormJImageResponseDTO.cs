using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormJImageResponseDTO
    {
        [MapTo("FjiPkRefNo")]
        public int No { get; set; }

        [MapTo("FjiFjdPkRefNo")]
        public int? DetailNo { get; set; }

        [MapTo("FjiImageTypeCode")]
        public string ImageTypeCode { get; set; }

        [MapTo("FjiImageSrno")]
        public int? ImageSrno { get; set; }

        [MapTo("FjiImageFilenameSys")]
        public string ImageFilenameSys { get; set; }

        [MapTo("FjiImageFilenameUpload")]
        public string ImageFilenameUpload { get; set; }

        [MapTo("FjiModBy")]
        public string ModBy { get; set; }

        [MapTo("FjiModDt")]
        public DateTime? ModDt { get; set; }

        [MapTo("FjiCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("FjiCrDt")]
        public DateTime? CreatedDt { get; set; }

        [MapTo("FjiSubmitSts")]
        public bool SubmitSts { get; set; }

        [MapTo("FjiActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo("FjiImageUserFilePath")]
        public string ImageUserFilePath { get; set; }
    }
}
