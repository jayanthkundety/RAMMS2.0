using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvTpbooth
    {
        public int Pk { get; set; }
        public string TollPlazaId { get; set; }
        public string LaneCode { get; set; }
        public string BoothNo { get; set; }
        public double? Width { get; set; }
        public double? Length { get; set; }
        public double? Height { get; set; }
        public string BoothType { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
