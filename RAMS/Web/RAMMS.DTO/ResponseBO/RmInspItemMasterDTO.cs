using AutoMapper;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class RmInspItemMasterDTO
    {
        public int PkRefNo { get; set; }
        public string InspName { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }
        //public static void Config(Profile profile)
        //{
        //    profile.RecognizeDestinationPrefixes("Iim");
        //    profile.RecognizePrefixes("Iim");
        //    profile.CreateMap<RmInspItemMasterDTO, RmInspItemMas>().ReverseMap();
        //}
    }
}
