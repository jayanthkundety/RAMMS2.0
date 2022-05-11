using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class ModuleRightsByFormDTO
    {
        public int PkId { get; set; }
        public int? Userid { get; set; }
        public int? UgPkId { get; set; }
        public string GroupName { get; set; }
        public int? ModPkId { get; set; }
        public string ModuleName { get; set; }
        public int? MfPkId { get; set; }
        public string ModFormName { get; set; }
        public bool? CanAdd { get; set; }
        public bool? CanEdit { get; set; }
        public bool? CanSignature { get; set; }
        public bool? CanView { get; set; }
        public bool? CanDelete { get; set; }
        public bool? CanPrint { get; set; }
        public bool? CanSubmit { get; set; }
        public bool? CanApprove { get; set; }
        public bool? ActiveYn { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
    }
}
