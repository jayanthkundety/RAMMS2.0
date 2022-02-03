using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class DivRmuSectionRequestDTO
    {
        [MapTo("RdsmPkRefNo")] public int RefNo { get; set; }
        [MapTo("RdsmDivCode")] public string DivCode { get; set; }
        [MapTo("RdsmDivision")] public string Division { get; set; }
        [MapTo("RdsmRmuCode")] public string RmuCode { get; set; }
        [MapTo("RdsmRmuName")] public string RmuName { get; set; }
        [MapTo("RdsmSectionCode")] public string SectionCode { get; set; }
        [MapTo("RdsmSectionName")] public string SectionName { get; set; }
        [MapTo("RdsmModBy")] public string ModBy { get; set; }
        [MapTo("RdsmModDt")] public DateTime? ModDt { get; set; }
        [MapTo("RdsmCrBy")] public string CrBy { get; set; }
        [MapTo("RdsmCrDt")] public DateTime? CrDt { get; set; }
        [MapTo("RdsmActiveYn")] public bool? ActiveYn { get; set; }
    }
}
