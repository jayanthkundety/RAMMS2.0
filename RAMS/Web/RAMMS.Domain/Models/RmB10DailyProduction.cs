using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmB10DailyProduction
    {
        public int B10dpPkRefNo { get; set; }
        public int? B10dpRevisionNo { get; set; }
        public DateTime? B10dpRevisionDate { get; set; }
        public int? B10dpRevisionYear { get; set; }
        public int? B10dpCrBy { get; set; }
        public DateTime? B10dpCrDt { get; set; }
    }
}
