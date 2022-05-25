using AutoMapper;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Profiles
{
    public class FormG1G2Profile : Profile
    {
        public FormG1G2Profile()
        {
            string[] arrPrefix = new string[] { "Fg1h", "Fg2h", "Fgi" };
            this.RecognizeDestinationPrefixes(arrPrefix);
            this.RecognizePrefixes(arrPrefix);
            this.CreateMap<FormG1DTO, RmFormG1Hdr>().ReverseMap();
            this.CreateMap<FormG2DTO, RmFormG2Hdr>().ReverseMap();            
            this.CreateMap<FormGImagesDTO, RmFormGImages>().ReverseMap();            
        }
    }
}
