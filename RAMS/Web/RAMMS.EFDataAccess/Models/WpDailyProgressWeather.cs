using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpDailyProgressWeather
    {
        public int Pk { get; set; }
        public int WpDailyProgressPk { get; set; }
        public int Hour { get; set; }
        public string Weather { get; set; }

        public virtual WpDailyProgress WpDailyProgressPkNavigation { get; set; }
    }
}
