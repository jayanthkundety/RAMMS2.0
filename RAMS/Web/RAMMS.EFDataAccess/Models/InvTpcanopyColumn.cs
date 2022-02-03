using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvTpcanopyColumn
    {
        public int Pk { get; set; }
        public string TollPlazaId { get; set; }
        public string ColumnType { get; set; }
        public double? Diameter { get; set; }
        public double? Height { get; set; }
        public string Location { get; set; }
    }
}
