using System;
using System.Collections.Generic;

namespace RAMMS.Domain
{
    public partial class RmUvModuleGroupRights
    {
        public int MgrPkId { get; set; }
        public int? ModPkId { get; set; }
        public int? UgPkId { get; set; }
        public bool? DvIsView { get; set; }
        public bool? DvIsModify { get; set; }
        public bool? DvIsDelete { get; set; }
        public bool? DvIsAdd { get; set; }
        public bool? PcIsView { get; set; }
        public bool? PcIsModify { get; set; }
        public bool? PcIsDelete { get; set; }
        public bool? PcIsAdd { get; set; }
        public string MgrCreatedBy { get; set; }
        public DateTime MgrCreatedOn { get; set; }
        public string MgrModifiedBy { get; set; }
        public DateTime MgrModifiedOn { get; set; }
        public string GroupCode { get; set; }
        public string ModuleName { get; set; }
    }
}
