using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class FormF2DetailRequestDTO
    {
        [MapTo("FgridPkRefNo")] public int PkRefNo { get; set; }
        [MapTo("FgridFgrihPkRefNo")] public int? FgrihPkRefNo { get; set; }
        [MapTo("FgrihAiPkRefNo")] public int? FgrihAiPkRefNo { get; set; }
        [MapTo("FgridStartingChKm")] public int? StartingChKm { get; set; }
        [MapTo("FgridStartingChM")] public string StartingChM { get; set; }
        [MapTo("FgridGrCode")] public string GrCode { get; set; }
        [MapTo("FgridGrCondition1")] public double? GrCondition1 { get; set; }
        [MapTo("FgridGrCondition2")] public double? GrCondition2 { get; set; }
        [MapTo("FgridGrCondition3")] public double? GrCondition3 { get; set; }
        [MapTo("FgridRhsMLhs")] public string RhsMLhs { get; set; }
        [MapTo("FgridPostSpac")] public double? PostSpac { get; set; }
        [MapTo("FgridRemarks")] public string Remarks { get; set; }
        [MapTo("FgridModBy")] public int? ModBy { get; set; }
        [MapTo("FgridModDt")] public DateTime? ModDt { get; set; }
        [MapTo("FgridCrBy")] public int? CrBy { get; set; }
        [MapTo("FgridCrDt")] public DateTime? CrDt { get; set; }
        [MapTo("FgridSubmitSts")] public bool SubmitSts { get; set; }
        [MapTo("FgridActiveYn")] public bool ActiveYn { get; set; }
        [MapTo("FgridLength")] public double? Length { get; set; }
        public string Bound { get; set; }
        public string AssetId { get; set; }
    }
}
