using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormTVehicleResponseDTO
    {
        public int PkRefNo { get; set; }
        public int? FmtdiPkRefNo { get; set; }
        public string VechicleType { get; set; }
        public string Axle { get; set; }
        public string Loading { get; set; }
        public string Time { get; set; }
        public int? Count { get; set; }

    }
}
