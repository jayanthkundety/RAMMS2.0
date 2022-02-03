using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormHImageResponseDTO
    {
        [MapTo("FhiPkRefNo")]
        public int No { get; set; }

        [MapTo("FhiFhhPkRefNo")]
        public int? HeaderNo { get; set; }

        [MapTo("FhiImageTypeCode")]
        public string ImageTypeCode { get; set; }

        [MapTo("FhiImageSrno")]
        public int? ImageSrno { get; set; }

        [MapTo("FhiImageFilenameSys")]
        public string ImageFilenameSys { get; set; }

        [MapTo("FhiImageFilenameUpload")]
        public string ImageFilenameUpload { get; set; }

        [MapTo("FhiModBy")]
        public string ModBy { get; set; }

        [MapTo("FhiModDt")]
        public DateTime? ModDt { get; set; }

        [MapTo("FhiCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("FhiCrDt")]
        public DateTime? CreatedDt { get; set; }

        [MapTo("FhiSubmitSts")]
        public bool SubmitSts { get; set; }

        [MapTo("FhiActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo("FhiImageUserFilePath")]
        public string ImageUserFilePath { get; set; }
    }
}
