using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class SicVehicle
    {
        public int Pk { get; set; }
        public int SicPk { get; set; }
        public DateTime? Dt { get; set; }
        public string PlatNo { get; set; }
        public string TaggingNo { get; set; }
        public string Location { get; set; }

        public virtual Sic SicPkNavigation { get; set; }
    }
}
