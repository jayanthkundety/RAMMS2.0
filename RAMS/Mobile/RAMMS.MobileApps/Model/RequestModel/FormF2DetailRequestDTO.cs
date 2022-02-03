using System;

namespace RAMMS.DTO
{
    public class FormF2DetailRequestDTO
    {
        public int PkRefNo { get; set; }
        public int? FgrihPkRefNo { get; set; }
        public int? FgrihAiPkRefNo { get; set; }
        public int? StartingChKm { get; set; }
        public string StartingChM { get; set; }
        public string GrCode { get; set; }
        public double? GrCondition1 { get; set; }
        public double? GrCondition2 { get; set; }
        public double? GrCondition3 { get; set; }
        public string RhsMLhs { get; set; }
        public double? PostSpac { get; set; }
        public string Remarks { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string Bound { get; set; }
        public double? Length { get; set; }
        public string AssetId { get; set; }
        public int SNo { get; set; }
    }
}