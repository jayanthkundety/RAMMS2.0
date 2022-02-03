using AutoMapper;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Profiles
{
    public class FormFDProfile : Profile
    {
        public FormFDProfile()
        {
            string[] arrPrefix = new string[] { "Fdih", "Fdid", "FdidFdi","RmFormFd" };
            this.RecognizeDestinationPrefixes(arrPrefix);
            this.RecognizePrefixes(arrPrefix);
            this.CreateMap<FormFDDTO, RmFormFdInsHdr>().ReverseMap();
            this.CreateMap<FormFDDetailsDTO, RmFormFdInsDtl>().ReverseMap();
        }
    }
}
