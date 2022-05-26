using AutoMapper;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Profiles
{
    public class FormF3Profiles : Profile
    {
        public FormF3Profiles()
        {
            string[] arrPrefix = new string[] { "FF3h", "FF3d" };
            this.RecognizeDestinationPrefixes(arrPrefix);
            this.RecognizePrefixes(arrPrefix);

            this.CreateMap<FormF3ResponseDTO, RmFormF3Hdr>().ReverseMap();
            this.CreateMap<FormF3DtlResponseDTO, RmFormF3Dtl>().ReverseMap();
        }
    }
}
