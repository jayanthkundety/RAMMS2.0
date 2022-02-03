using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class FormAImageListRequestDTO
    {
        [MapTo("FaiPkRefNo")]
        public int ImageId { get; set; }
        [MapTo("FaiFadPkRefNo")]
        public int AssetId { get; set; }
        [MapTo("FaiImageTypeCode")]
        public string ImageTypeCode { get; set; }
        [MapTo("FaiImageSrno")]
        public int SNO { get; set; }
        [MapTo("FaiImageFilenameSys")]
        public string ImageFilenameSys { get; set; }
        [MapTo("FaiImageFilenameUpload")]
        public string ImageFilename { get; set; }

        [MapTo("FaiImageUserFilePath")]
        public string FileName { get; set; }

        [MapTo("FaiModBy")]
        public string ModifyBy { get; set; }
        [MapTo("FaiModDt")]
        public DateTime? ModifyDate { get; set; }
        [MapTo("FaiCrBy")]
        public string CreatedBy { get; set; }
        [MapTo("FaiCrDt")]
        public DateTime? CreatedDate { get; set; }
        [MapTo("FaiSubmitSts")]
        public bool SubmitStatus { get; set; }
        [MapTo("FaiActiveYn")]
        public bool ActiveYn { get; set; }
    }
}
