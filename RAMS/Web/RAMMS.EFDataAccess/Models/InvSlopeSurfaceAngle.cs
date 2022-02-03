using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvSlopeSurfaceAngle
    {
        public int Pk { get; set; }
        public int InvSlopePk { get; set; }
        public string Surface { get; set; }
        public string Angle { get; set; }
        public string Remark { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
        public Guid? TempId { get; set; }
    }
}
