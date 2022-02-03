using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmDivisionMaster
    {
        public int DivPkRefNo { get; set; }
        public string DivCode { get; set; }
        public string DivName { get; set; }
        public bool DivIsActive { get; set; }
    }
}
