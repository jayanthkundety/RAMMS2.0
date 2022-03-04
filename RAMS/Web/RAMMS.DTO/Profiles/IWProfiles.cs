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
            string[] arrPrefix = new string[] { "Fw1", "Fw2", "Fecm", "Fiwi", "Fwd", "Fwn", "Fwdd" , "Fwc", "Fwg" };
            this.RecognizeDestinationPrefixes(arrPrefix);
            this.RecognizePrefixes(arrPrefix);
            this.CreateMap<FormW1ResponseDTO, RmIwFormW1>().ReverseMap();
            this.CreateMap<FormW2ResponseDTO, RmIwFormW2>().ReverseMap();
            this.CreateMap<FormIWImageResponseDTO, RmIwformImage>().ReverseMap();
            this.CreateMap<FormW2FECMResponseDTO, RmIwFormW2Fecm>().ReverseMap();

            this.CreateMap<FormWDResponseDTO, RmIwFormWd>().ReverseMap();
            this.CreateMap<FormWDDtlResponseDTO, RmIwFormWdDtl>().ReverseMap();
            this.CreateMap<FormWNResponseDTO, RmIwFormWn>().ReverseMap();

            this.CreateMap<FormWCResponseDTO, RmIwFormWc>().ReverseMap();
            this.CreateMap<FormWGResponseDTO, RmIwFormWg>().ReverseMap();

        }
    }
}
