using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class FormJImageListRequestDTO
    {
        [MapTo("FjiPkRefNo")]
        public int ImageId { get; set; }
        [MapTo("FjiFjdPkRefNo")]
        public int AssetId { get; set; }
        [MapTo("FjiImageTypeCode")]
        public string ImageTypeCode { get; set; }
        [MapTo("FjiImageSrno")]
        public int SNO { get; set; }
        [MapTo("FjiImageFilenameSys")]
        public string ImageFilenameSys { get; set; }
        [MapTo("FjiImageFilenameUpload")]
        public string ImageFilename { get; set; }

        [MapTo("FjiImageUserFilePath")]
        public string FileName { get; set; }

        [MapTo("FjiModBy")]
        public string ModifyBy { get; set; }
        [MapTo("FjiModDt")]
        public DateTime? ModifyDate { get; set; }
        [MapTo("FjiCrBy")]
        public string CreatedBy { get; set; }
        [MapTo("FjiCrDt")]
        public DateTime? CreatedDate { get; set; }
        [MapTo("FjiSubmitSts")]
        public bool SubmitStatus { get; set; }
        [MapTo("FjiActiveYn")]
        public bool ActiveYn { get; set; }
    }
}
