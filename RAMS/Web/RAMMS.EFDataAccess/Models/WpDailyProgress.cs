using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class WpDailyProgress
    {
        public WpDailyProgress()
        {
            WpDailyProgressMachine = new HashSet<WpDailyProgressMachine>();
            WpDailyProgressManPower = new HashSet<WpDailyProgressManPower>();
            WpDailyProgressMaterial = new HashSet<WpDailyProgressMaterial>();
            WpDailyProgressWeather = new HashSet<WpDailyProgressWeather>();
        }

        public int Pk { get; set; }
        public string WorkHourFrom { get; set; }
        public string WorkHourTo { get; set; }
        public string ActivityOnSite { get; set; }
        public string ClientInstruction { get; set; }
        public string ContractorInstruction { get; set; }

        public virtual WorkProgress PkNavigation { get; set; }
        public virtual ICollection<WpDailyProgressMachine> WpDailyProgressMachine { get; set; }
        public virtual ICollection<WpDailyProgressManPower> WpDailyProgressManPower { get; set; }
        public virtual ICollection<WpDailyProgressMaterial> WpDailyProgressMaterial { get; set; }
        public virtual ICollection<WpDailyProgressWeather> WpDailyProgressWeather { get; set; }
    }
}
