using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvSlopeSoilInvestigation
    {
        public int Pk { get; set; }
        public int InvMasterPk { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
        public string Title { get; set; }
        public string Contractor { get; set; }
        public string Consultant { get; set; }
        public DateTime? SoilInvestigationDate { get; set; }
    }
}
