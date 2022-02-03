using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmUserGroupRights
    {
        public int UgrPkId { get; set; }
        public string UgrRightsCode { get; set; }
        public int? UgrRightLevel { get; set; }
        public string UgrRemarks { get; set; }
        public string UgrModBy { get; set; }
        public DateTime UgrModDt { get; set; }
        public string UgrCrBy { get; set; }
        public DateTime UgrCrDt { get; set; }
        public bool UgrSubmitSts { get; set; }
        public bool? UgrActiveYn { get; set; }
    }
}
