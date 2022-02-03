using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class Section
    {
        public Section()
        {
            AuditSession = new HashSet<AuditSession>();
            Complaint = new HashSet<Complaint>();
            Emergency = new HashSet<Emergency>();
            Feature = new HashSet<Feature>();
            PavementCondition = new HashSet<PavementCondition>();
            RmSchedule = new HashSet<RmSchedule>();
            UserScopeBak = new HashSet<UserScopeBak>();
            Work = new HashSet<Work>();
        }

        public int Pk { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public double Kmfrom { get; set; }
        public double Kmto { get; set; }
        public int RegionPk { get; set; }
        public string Route { get; set; }
        public string SectionRouteFrom { get; set; }
        public string SectionRouteTo { get; set; }
        public string AssetOwner { get; set; }
        public string Network { get; set; }
        public bool? IsActive { get; set; }

        public virtual Region RegionPkNavigation { get; set; }
        public virtual ICollection<AuditSession> AuditSession { get; set; }
        public virtual ICollection<Complaint> Complaint { get; set; }
        public virtual ICollection<Emergency> Emergency { get; set; }
        public virtual ICollection<Feature> Feature { get; set; }
        public virtual ICollection<PavementCondition> PavementCondition { get; set; }
        public virtual ICollection<RmSchedule> RmSchedule { get; set; }
        public virtual ICollection<UserScopeBak> UserScopeBak { get; set; }
        public virtual ICollection<Work> Work { get; set; }
    }
}
