using AutoMapper;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.RequestBO
{
    public class FormF4DetailRequestDTO
    {
        public int PkRefNo { get; set; }
        public int? hPkRefNo { get; set; }
        public int? FcvihPkRefNo { get; set; }
        public int? LocChKm { get; set; }
        public string LocChM { get; set; }
        public string StrucCode { get; set; }
        public bool? IntelStruc { get; set; }
        public bool? OutletStruc { get; set; }
        public double? Length { get; set; }
        public int? BarrelNo { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public int? Condition { get; set; }
        public string Remarks { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }
        public static void Config(Profile profile)
        {
            profile.RecognizeDestinationPrefixes("Fivad", "Fivad", "RmFormF4", "FivadFiva");
            profile.RecognizePrefixes("Fivad", "Fivad", "RmFormF4", "FivadFivah");
            profile.CreateMap<FormF4DetailRequestDTO, RmFormF4InsDtl>().ReverseMap();
        }
    }
}
