using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class GroupStructureDetail
    {
        public int Pk { get; set; }
        public string MainGroup { get; set; }
        public string SubGroup { get; set; }
        public string MainComponent { get; set; }
        public string SubComponent { get; set; }
        public string MainGroupAttributeName { get; set; }
        public string SubComponentAttributeName { get; set; }
        public string SubGroupAttributeName { get; set; }
        public string MainComponentAttributeName { get; set; }
        public string MainComponentAttributeType { get; set; }
        public string SubGroupAttributeType { get; set; }
        public string MainGroupAttributeType { get; set; }
    }
}
