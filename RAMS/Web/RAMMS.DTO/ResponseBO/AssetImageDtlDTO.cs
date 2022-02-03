using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class AssetImageDtlDTO
    {


        [MapTo("AidPkRefNo")]
        public int RefNo { get; set; }

        [MapTo("AidAiPkRefNo")]
        public int? No { get; set; }

        [MapTo("AidImageTypeCode")]
        public string ImageTypeCode { get; set; }

        [MapTo("AidImageSrno")]
        public int? ImageSrno { get; set; }
    }
}
