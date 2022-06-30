using AutoMapper;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Profiles
{
    public class FormF1Profiles : Profile
    {
        public FormF1Profiles()
        {
            string[] arrPrefix = new string[] { "FF1h", "FF1d" };
            this.RecognizeDestinationPrefixes(arrPrefix);
            this.RecognizePrefixes(arrPrefix);

            this.CreateMap<FormF1ResponseDTO, RmFormF1Hdr>().ReverseMap();
            this.CreateMap<FormF1DtlResponseDTO, RmFormF1Dtl>().ReverseMap();
        }
    }
}
