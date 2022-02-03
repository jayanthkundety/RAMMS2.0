using AutoMapper;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
namespace RAMMS.DTO.RequestBO
{
    public class FormF5DetailRequestDTO
    {
        public int PkRefNo { get; set; }
        public int? hPkRefNo { get; set; }
        public int? FbrihPkRefNo { get; set; }
        public int? LocChKm { get; set; }
        public string LocChM { get; set; }
        public string StrucCode { get; set; }
        public string RiverName { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public int? SpanCnt { get; set; }
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
            profile.RecognizeDestinationPrefixes("Fvad", "Fvad", "RmFormF5", "FvadFva");
            profile.RecognizePrefixes("Fvad", "Fvad", "RmFormF5", "FvadFva");
            profile.CreateMap<FormF5DetailRequestDTO, RmFormF5InsDtl>().ReverseMap();
        }
    }
}
