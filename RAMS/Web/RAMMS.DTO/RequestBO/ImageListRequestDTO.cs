using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;


namespace RAMMS.DTO.RequestBO
{
    public  class ImageListRequestDTO
    {

        [MapTo("AidPkRefNo")]
        public int ImageId { get; set; }
        [MapTo("AidAiPkRefNo")]
        public int AssetId { get; set; }
        [MapTo("AidImageTypeCode")]
        public string ImageTypeCode { get; set; }
        [MapTo("AidImageSrno")]
        public int  SNO { get; set; }
        [MapTo("AidImageFilenameSys")]
        public string ImageFilenameSys { get; set; }
        [MapTo("AidImageFilenameUpload")]
        public string ImageFilename { get; set; }

        [MapTo("AidImageUserFilePath")]
        public string FileName { get; set; }

        [MapTo("AidModBy")]
        public string ModifyBy { get; set; }
        [MapTo("AidModDt")]
        public DateTime? ModifyDate { get; set; }
        [MapTo("AidCrBy")]
        public string  CreatedBy { get; set; }
        [MapTo("AidCrDt")]
        public DateTime? CreatedDate { get; set; }
        [MapTo("AidSubmitSts")]
        public bool  SubmitStatus { get; set; }
        [MapTo("AidActiveYn")]
        public bool ActiveYn { get; set; }

        public string FileFullPath { get; set; }

    }
}
