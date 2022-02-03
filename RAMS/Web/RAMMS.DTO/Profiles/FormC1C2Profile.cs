using AutoMapper;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Profiles
{
    public class FormC1C2Profile : Profile
    {
        public FormC1C2Profile()
        {
            string[] arrPrefix = new string[] { "Fcvih", "RmFormCv", "Fcvi", "FcviFcvi", "Fcvid", "FcvidFcvi", "FcvidIi", "Iim" };
            this.RecognizeDestinationPrefixes(arrPrefix);
            this.RecognizePrefixes(arrPrefix);
            this.CreateMap<FormC1C2DTO, RmFormCvInsHdr>().ReverseMap();
            this.CreateMap<FormC1C2ImageDTO, RmFormCvInsImage>().ReverseMap();            
            this.CreateMap<RmFormCvInsDtDTO, RmFormCvInsDtl>().ReverseMap();            
            this.CreateMap<RmInspItemMasterDTO, RmInspItemMas>().ReverseMap();
        }
    }
}
