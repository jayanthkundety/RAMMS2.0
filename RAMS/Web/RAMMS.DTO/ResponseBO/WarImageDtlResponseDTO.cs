using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class WarImageDtlResponseDTO
    {
        [MapTo("FwarPkRefNo")]
        public int NO { get; set; }

        [MapTo("FwarFddPkRefNo")]
        public int? HeaderId { get; set; }

        [MapTo("FwarFxhPkRefNo")]
        public int? FxhPkRefNo { get; set; }

        [MapTo("FwarImageTypeCode")]
        public string ImageTypeCode { get; set; }

        [MapTo("FwarImageSrno")]
        public int? ImageSrno { get; set; }

        [MapTo("FwarImageFilenameSys")]
        public string ImageFilenameSys { get; set; }

        [MapTo("FwarImageFilenameUpload")]
        public string ImageFilenameUpload { get; set; }

        [MapTo("FwarModBy")]
        public string ModBy { get; set; }

        [MapTo("FwarModDt")]
        public DateTime? ModDt { get; set; }

        [MapTo("FwarCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("FwarCrDt")]
        public DateTime? CreatedDt { get; set; }

        [MapTo("FwarSubmitSts")]
        public bool SubmitSts { get; set; }

        [MapTo("FwarActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo("FwarImageUserFilename")]
        public string ImageUserFilename { get; set; }

        public string FileFullPath { get; set; }
    }
}
