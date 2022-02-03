using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvCulvertCatchmentDocument
    {
        public int Pk { get; set; }
        public int InvCulvertCatchmentPk { get; set; }
        public string ReferenceNumber { get; set; }
        public string Title { get; set; }
        public string FillingReference { get; set; }
        public string FillingLocation { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime Updated { get; set; }
        public string UpdatedBy { get; set; }
    }
}
