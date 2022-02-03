using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvMaster
    {
        public InvMaster()
        {
            PiScheduleMasterAsset = new HashSet<PiScheduleMasterAsset>();
            PrincipalBridgeInspection = new HashSet<PrincipalBridgeInspection>();
            RmSchedule = new HashSet<RmSchedule>();
            SpecialInspection = new HashSet<SpecialInspection>();
            WorkProgram = new HashSet<WorkProgram>();
        }

        public int Pk { get; set; }
        public string Id { get; set; }
        public string BaseType { get; set; }
        public int FeaturePk { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Remark { get; set; }
        public int GroupStructurePk { get; set; }
        public string TemanId { get; set; }
        public bool IsActive { get; set; }
        public int? ParentPk { get; set; }

        public virtual Feature FeaturePkNavigation { get; set; }
        public virtual InvGroupStructure GroupStructurePkNavigation { get; set; }
        public virtual InvCulvert Pk1 { get; set; }
        public virtual InvSlope Pk2 { get; set; }
        public virtual InvTunnel Pk3 { get; set; }
        public virtual InvBridge PkNavigation { get; set; }
        public virtual InvSign InvSign { get; set; }
        public virtual ICollection<PiScheduleMasterAsset> PiScheduleMasterAsset { get; set; }
        public virtual ICollection<PrincipalBridgeInspection> PrincipalBridgeInspection { get; set; }
        public virtual ICollection<RmSchedule> RmSchedule { get; set; }
        public virtual ICollection<SpecialInspection> SpecialInspection { get; set; }
        public virtual ICollection<WorkProgram> WorkProgram { get; set; }
    }
}
