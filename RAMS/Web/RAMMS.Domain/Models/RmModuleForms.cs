using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmModuleForms
    {
        public int MfPkId { get; set; }
        public int MfModPkId { get; set; }
        public string MfFormName { get; set; }
        public bool? MfActiveYn { get; set; }
        public int? MfCrBy { get; set; }
        public DateTime? MfCrDt { get; set; }
        public int? MfModBy { get; set; }
        public DateTime? MfModDt { get; set; }
    }
}
