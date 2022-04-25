using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormQa1Mat
    {
        public int Fqa1mPkRefNo { get; set; }
        public int? Fqa1mFqa1hPkRefNo { get; set; }
        public string Fqa1mType { get; set; }
        public decimal? Fqa1mQty { get; set; }
        public string Fqa1mUnit { get; set; }
        public string Fqa1mSpec { get; set; }
        public string Fqa1mRemark { get; set; }
        public int? Fqa1mModBy { get; set; }
        public DateTime? Fqa1mModDt { get; set; }
        public int? Fqa1mCrBy { get; set; }
        public DateTime? Fqa1mCrDt { get; set; }

        public virtual RmFormQa1Hdr Fqa1mFqa1hPkRefNoNavigation { get; set; }
    }
}
