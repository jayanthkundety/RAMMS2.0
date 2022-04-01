using AutoMapper;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Profiles
{
    public class MAMVProfiles : Profile
    {
        public MAMVProfiles()
        {
            string[] arrPrefix = new string[] { "Fv1h", "Fv1d", "Fv2h", "Fv2e", "Fv2l", "Fv2m" };
            this.RecognizeDestinationPrefixes(arrPrefix);
            this.RecognizePrefixes(arrPrefix);

            this.CreateMap<FormV2HeaderResponseDTO, RmFormV2Hdr>().ReverseMap();
            this.CreateMap<FormV2EquipDetailsResponseDTO, RmFormV2Eqp>().ReverseMap();
            this.CreateMap<FormV2LabourDetailsResponseDTO, RmFormV2Lab>().ReverseMap();
            this.CreateMap<FormV2MaterialDetailsResponseDTO, RmFormV2Mat>().ReverseMap();

        }
    }
}
