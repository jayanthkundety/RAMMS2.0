using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class FormFSDetailRequestDTO
    {
        [MapTo("FsdPkRefNo")] public int PkRefNo { get; set; }
        [MapTo("FsdFshPkRefNo")] public int? FshPkRefNo { get; set; }
        [MapTo("FsdFeature")] public string Feature { get; set; }
        [MapTo("FsdGrpType")] public string GrpType { get; set; }
        [MapTo("FsdStrucCode")] public string StrucCode { get; set; }
        [MapTo("FsdWidth")] public double? Width { get; set; }
        [MapTo("FsdLength")] public double? Length { get; set; }
        [MapTo("FsdCondition1")] public decimal? Condition1 { get; set; }
        [MapTo("FsdCondition2")] public decimal? Condition2 { get; set; }
        [MapTo("FsdCondition3")] public decimal? Condition3 { get; set; }
        [MapTo("FsdNeeded")] public string Needed { get; set; }
        [MapTo("FsdUnit")] public string Unit { get; set; }
        [MapTo("FsdRemarks")] public string Remarks { get; set; }
        [MapTo("FsdModBy")] public int? ModBy { get; set; }
        [MapTo("FsdModDt")] public DateTime? ModDt { get; set; }
        [MapTo("FsdCrBy")] public int? CrBy { get; set; }
        [MapTo("FsdCrDt")] public DateTime? CrDt { get; set; }
        [MapTo("FsdSubmitSts")] public bool SubmitSts { get; set; }
        [MapTo("FsdActiveYn")] public bool ActiveYn { get; set; }
        public string GroupCode { get; set; }
    }
}
