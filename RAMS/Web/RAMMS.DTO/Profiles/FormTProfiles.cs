using AutoMapper;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Profiles
{
    public class FormTProfiles : Profile
    {
        public FormTProfiles()
        {
            string[] arrPrefix = new string[] { "Fmt", "Fmtdi", "Fmtv" };
            this.RecognizeDestinationPrefixes(arrPrefix);
            this.RecognizePrefixes(arrPrefix);

            this.CreateMap<FormTResponseDTO, RmFormTHdr>().ReverseMap();
            this.CreateMap<FormTDtlResponseDTO, RmFormTDailyInspection>().ReverseMap();
            this.CreateMap<FormTVehicleResponseDTO, RmFormTVechicle>().ReverseMap();
        }
    }
}
