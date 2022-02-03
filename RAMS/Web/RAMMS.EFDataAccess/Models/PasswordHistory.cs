using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PasswordHistory
    {
        public int UserPk { get; set; }
        public string UserPassword { get; set; }
        public DateTime CommenceDate { get; set; }
        public int Pk { get; set; }
    }
}
