using AutoMapper;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Profiles
{
    public class FormMProfile : Profile
    {
        public FormMProfile()
        {
            string[] arrPrefix = new string[] { "FMH", "FMAD" };
            this.RecognizeDestinationPrefixes(arrPrefix);
            this.RecognizePrefixes(arrPrefix);
            this.CreateMap<FormMDTO, RmFormMHdr>().ReverseMap();
            this.CreateMap<FormMAuditDetailsDTO, RmFormMAuditDetails>().ReverseMap();            
        }
    }
}
