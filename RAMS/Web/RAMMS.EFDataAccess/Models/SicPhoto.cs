using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class SicPhoto
    {
        public int Pk { get; set; }
        public int SicPk { get; set; }
        public string PhotoFileName { get; set; }
        public string PhotoRemarks { get; set; }

        public virtual Sic SicPkNavigation { get; set; }
    }
}
