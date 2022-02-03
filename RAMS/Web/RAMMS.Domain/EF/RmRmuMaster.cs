using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmRmuMaster
    {
        public int RmuPkRefNo { get; set; }
        public string DivCode { get; set; }
        public string RmuCode { get; set; }
        public string RmuName { get; set; }
        public bool RmuIsActive { get; set; }
    }
}
