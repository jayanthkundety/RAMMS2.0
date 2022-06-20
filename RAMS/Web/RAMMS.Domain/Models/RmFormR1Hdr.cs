using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormR1Hdr
    {
        public RmFormR1Hdr()
        {
            RmFormR2Hdr = new HashSet<RmFormR2Hdr>();
            RmFormRImages = new HashSet<RmFormRImages>();
        }
        public int Fr1hPkRefNo { get; set; }
        public string Fr1hContNo { get; set; }
        public string Fr1hAssetId { get; set; }
        public string Fr1hAiDivCode { get; set; }
        public string Fr1hAiRmuCode { get; set; }
        public string Fr1hAiRmuName { get; set; }
        public string Fr1hAiRdCode { get; set; }
        public string Fr1hAiRdName { get; set; }
        public int? Fr1hAiLocChKm { get; set; }
        public string Fr1hAiLocChM { get; set; }
        public string Fr1hAiStrucCode { get; set; }
        public double? Fr1hAiGpsEasting { get; set; }
        public double? Fr1hAiGpsNorthing { get; set; }
        public int? Fr1hYearOfInsp { get; set; }
        public DateTime? Fr1hDtOfInsp { get; set; }
        public string Fr1hWallFunction { get; set; }
        public string Fr1hWallMember { get; set; }
        public string Fr1hFacingType { get; set; }
        public int? Fr1hRecordNo { get; set; }
        public string Fr1hDistressObserved1 { get; set; }
        public string Fr1hDistressObserved2 { get; set; }
        public string Fr1hDistressObserved3 { get; set; }
        public int? Fr1hSeverity { get; set; }
        public int? Fr1hModBy { get; set; }
        public DateTime? Fr1hModDt { get; set; }
        public int? Fr1hCrBy { get; set; }
        public DateTime? Fr1hCrDt { get; set; }
        public bool Fr1hSubmitSts { get; set; }
        public bool Fr1hActiveYn { get; set; }

        public virtual ICollection<RmFormR2Hdr> RmFormR2Hdr { get; set; }
        public virtual ICollection<RmFormRImages> RmFormRImages { get; set; }
    }
}
