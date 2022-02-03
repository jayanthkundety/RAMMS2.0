using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class RunningNumber
    {
        public int Nod { get; set; }
        public int ActionLog { get; set; }
        public int WorkInstruction { get; set; }
        public int WorkRequest { get; set; }
        public int Pit { get; set; }
    }
}
