using AutoMapper;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Profiles
{
    public class FormR1R2Profile : Profile
    {
        public FormR1R2Profile()
        {
            string[] arrPrefix = new string[] { "Fr1h", "Fr2h", "Fri", "Fr1hAI" };
            this.RecognizeDestinationPrefixes(arrPrefix);
            this.RecognizePrefixes(arrPrefix);
            this.CreateMap<FormR1DTO, RmFormR1Hdr>().ReverseMap();
            this.CreateMap<FormR2DTO, RmFormR2Hdr>().ReverseMap();
            this.CreateMap<FormRImagesDTO, RmFormRImages>().ReverseMap();
        }
    }
}