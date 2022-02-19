using AutoMapper;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Profiles
{
    public class IWProfiles : Profile
    {
        public IWProfiles()
        {
            string[] arrPrefix = new string[] { "Fw1", "Fw2", "Fecm", "Fiwi" };
            this.RecognizeDestinationPrefixes(arrPrefix);
            this.RecognizePrefixes(arrPrefix);
            this.CreateMap<FormW1ResponseDTO, RmIwFormW1>().ReverseMap();
            this.CreateMap<FormW2ResponseDTO, RmIwFormW2>().ReverseMap();
            this.CreateMap<FormIWImageResponseDTO, RmIwformImage>().ReverseMap();
            this.CreateMap<FormW2FCEMResponseDTO, RmIwFormW2Fcem>().ReverseMap();
        }
    }
}
