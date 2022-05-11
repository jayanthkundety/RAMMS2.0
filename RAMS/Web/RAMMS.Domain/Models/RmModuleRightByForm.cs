using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmModuleRightByForm
    {
        public int MfrPkId { get; set; }
        public int? MfrUserid { get; set; }
        public int? MfrUgPkId { get; set; }
        public string MfrGroupName { get; set; }
        public int? MfrModPkId { get; set; }
        public string MfrModuleName { get; set; }
        public int? MfrMfPkId { get; set; }
        public string MfrModFormName { get; set; }
        public bool? MfrCanAdd { get; set; }
        public bool? MfrCanEdit { get; set; }
        public bool? MfrCanSignature { get; set; }
        public bool? MfrCanView { get; set; }
        public bool? MfrCanDelete { get; set; }
        public bool? MfrCanPrint { get; set; }
        public bool? MfrCanSubmit { get; set; }
        public bool? MfrCanApprove { get; set; }
        public bool? MfrActiveYn { get; set; }
        public int? MfrCrBy { get; set; }
        public DateTime? MfrCrDt { get; set; }
        public int? MfrModBy { get; set; }
        public DateTime? MfrModDt { get; set; }
    }
}
