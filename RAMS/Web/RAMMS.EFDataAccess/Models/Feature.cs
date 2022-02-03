using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class Feature
    {
        public Feature()
        {
            InvMaster = new HashSet<InvMaster>();
        }

        public int Pk { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Route { get; set; }
        public double? Kmlocation { get; set; }
        public int SectionPk { get; set; }
        public string LocalityOfFeature { get; set; }
        public string Bound { get; set; }
        public string Mhaoffice { get; set; }
        public DateTime? OpeningDate { get; set; }
        public double? SpeedLimit { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string LofCode { get; set; }
        public string FeatureOwner { get; set; }

        public virtual Section SectionPkNavigation { get; set; }
        public virtual ICollection<InvMaster> InvMaster { get; set; }
    }
}
