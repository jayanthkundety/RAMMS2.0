using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvTunnelDrawing
    {
        public int Pk { get; set; }
        public int InvTunnelConstructionPk { get; set; }
        public string ContractNumber { get; set; }
        public string DrawingNumber { get; set; }
        public string DrawingType { get; set; }
        public DateTime? DrawingDate { get; set; }
        public string DrawingTitle { get; set; }
        public string ProjectTitle { get; set; }
        public string Client { get; set; }
        public string FillingReference { get; set; }
        public string FillingLocation { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
