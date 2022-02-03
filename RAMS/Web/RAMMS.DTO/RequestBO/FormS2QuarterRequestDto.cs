using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class FormS2QuarterRequestDto
    {

        [MapTo("FsiiqdPkRefNo")]
        public int PkRefNo { get; set; }
        [MapTo("FsiiqdFsiidPkRefNo")]
        public int? FsiidPkRefNo { get; set; }
        [MapTo("FsiiqdClkPkRefNo")]
        public int? ClkPkRefNo { get; set; }
        [MapTo("FsiiqdCrBy")]
        public int? CrBy { get; set; }
        [MapTo("FsiiqdCrDt")]
        public DateTime? CrDt { get; set; }

        public string Month { get; set; }
    }
}
