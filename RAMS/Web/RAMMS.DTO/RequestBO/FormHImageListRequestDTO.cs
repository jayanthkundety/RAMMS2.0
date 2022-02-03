using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class FormHImageListRequestDTO
    {
        [MapTo("FhiPkRefNo")]
        public int ImageId { get; set; }
        [MapTo("FhiFhhPkRefNo")]
        public int AssetId { get; set; }
        [MapTo("FhiImageTypeCode")]
        public string ImageTypeCode { get; set; }
        [MapTo("FhiImageSrno")]
        public int SNO { get; set; }
        [MapTo("FhiImageFilenameSys")]
        public string ImageFilenameSys { get; set; }
        [MapTo("FhiImageFilenameUpload")]
        public string ImageFilename { get; set; }

        [MapTo("FhiImageUserFilePath")]
        public string FileName { get; set; }

        [MapTo("FhiModBy")]
        public string ModifyBy { get; set; }
        [MapTo("FhiModDt")]
        public DateTime? ModifyDate { get; set; }
        [MapTo("FhiCrBy")]
        public string CreatedBy { get; set; }
        [MapTo("FhiCrDt")]
        public DateTime? CreatedDate { get; set; }
        [MapTo("FhiSubmitSts")]
        public bool SubmitStatus { get; set; }
        [MapTo("FhiActiveYn")]
        public bool ActiveYn { get; set; }
    }
}
