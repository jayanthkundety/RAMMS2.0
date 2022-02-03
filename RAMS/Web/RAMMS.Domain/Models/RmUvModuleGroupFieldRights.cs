using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmUvModuleGroupFieldRights
    {
        public int MgfrPkId { get; set; }
        public int ModPkId { get; set; }
        public int UgPkId { get; set; }
        public string MgfrFieldName { get; set; }
        public bool? MgfrIsDisabled { get; set; }
        public bool? MgfrIsHide { get; set; }
        public string MgfrCreatedBy { get; set; }
        public DateTime MgfrCreatedOn { get; set; }
        public string MgfrModifiedBy { get; set; }
        public DateTime MgfrModifiedOn { get; set; }
        public string GroupCode { get; set; }
        public string ModuleName { get; set; }
    }
}
