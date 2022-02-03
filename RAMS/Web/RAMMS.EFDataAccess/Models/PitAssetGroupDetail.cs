using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class PitAssetGroupDetail
    {
        public int Pk { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
        public int? GroupStructurePk { get; set; }
        public string GroupStructureDetailMainGroup { get; set; }
        public string GroupStructureDetailMainGroupAttributeName { get; set; }
        public string GroupStructureDetailSubGroup { get; set; }
        public string GroupStructureDetailSubGroupAttributeName { get; set; }
        public string GroupStructureDetailMainComponent { get; set; }
        public string GroupStructureDetailMainComponentAttributeName { get; set; }
        public string GroupStructureDetailSubComponent { get; set; }
        public string GroupStructureDetailSubComponentAttributeName { get; set; }
    }
}
