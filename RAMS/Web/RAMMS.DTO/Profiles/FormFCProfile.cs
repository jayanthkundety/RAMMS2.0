using AutoMapper;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Profiles
{
    public class FormFCProfile : Profile
    {
        public FormFCProfile()
        {
            string[] arrPrefix = new string[] { "Fcih", "Fcid", "FcidFci", "RmFormFc" };
            this.RecognizeDestinationPrefixes(arrPrefix);
            this.RecognizePrefixes(arrPrefix);            
            this.CreateMap<FormFCDTO, RmFormFcInsHdr>().ReverseMap();
            this.CreateMap<FormFCDetailsDTO, RmFormFcInsDtl>().ReverseMap();
        }
    }
}
