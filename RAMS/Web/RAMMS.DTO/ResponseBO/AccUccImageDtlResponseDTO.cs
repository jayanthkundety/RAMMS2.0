using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class AccUccImageDtlResponseDTO
    {
        [MapTo("FauPkRefNo")]
        public int NO { get; set; }

        [MapTo("FauFddPkRefNo")]
        public int? HeaderId { get; set; }

        [MapTo("FauFxhPkRefNo")]
        public int? FxhPkRefNo { get; set; }

        [MapTo("FauAccUcu")]
        public string AccUcu { get; set; }

        [MapTo("FauImageSrno")]
        public int? ImageSrno { get; set; }

        [MapTo("FauImageFilenameSys")]
        public string ImageFilenameSys { get; set; }

        [MapTo("FauImageFilenameUpload")]
        public string ImageFilenameUpload { get; set; }

        [MapTo("FauModBy")]
        public string ModBy { get; set; }

        [MapTo("FauModDt")]
        public DateTime? ModDt { get; set; }

        [MapTo("FauCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("FauCrDt")]
        public DateTime? CreatedDt { get; set; }

        [MapTo("FauSubmitSts")]
        public bool SubmitSts { get; set; }

        [MapTo("FauActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo("FauImageUserFilename")]
        public string ImageUserFilename { get; set; }
    }
}
