using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormV2Mat
    {
        public int Fv2mPkRefNo { get; set; }
        public int? Fv2mFv2hPkRefNo { get; set; }
        public string Fv2mMatRefCode { get; set; }
        public string Fv2mDesc { get; set; }
        public decimal? Fv2mQnty { get; set; }
        public int? Fv2mUnit { get; set; }
        public string Fv2mModBy { get; set; }
        public DateTime? Fv2mModDt { get; set; }
        public string Fv2mCrBy { get; set; }
        public DateTime? Fv2mCrDt { get; set; }
        public bool Fv2mSubmitSts { get; set; }
        public bool Fv2mActiveYn { get; set; }
    }
}
