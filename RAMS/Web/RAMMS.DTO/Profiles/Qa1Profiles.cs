using AutoMapper;
using RAMMS.Domain.Models;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Profiles
{
    public class Qa1Profiles : Profile
    {
        public Qa1Profiles()
        {
            string[] arrPrefix = new string[] { "Fqa1h", "Fqa1ev", "Fqa1gc", "Fqa1gen", "Fqa1l", "Fqa1m", "Fqa1ssc", "Fqa1tes", "Fqa1wcq", "Fqa1w" , "Fqa1i" };
            this.RecognizeDestinationPrefixes(arrPrefix);
            this.RecognizePrefixes(arrPrefix);

            this.CreateMap<FormQa1HeaderDTO, RmFormQa1Hdr>().ReverseMap();
            this.CreateMap<FormQa1EqVhDTO, RmFormQa1EqVh>().ReverseMap();
            this.CreateMap<FormQa1GCDTO, RmFormQa1Gc>().ReverseMap();
            this.CreateMap<FormQa1GenDTO, RmFormQa1Gen>().ReverseMap();
            this.CreateMap<FormQa1LabDTO, RmFormQa1Lab>().ReverseMap();
            this.CreateMap<FormQa1MatDTO, RmFormQa1Mat>().ReverseMap();
            this.CreateMap<FormQa1SscDTO, RmFormQa1Ssc>().ReverseMap();
            this.CreateMap<FormQa1TesDTO, RmFormQa1Tes>().ReverseMap();
            this.CreateMap<FormQa1WcqDTO, RmFormQa1Wcq>().ReverseMap();
            this.CreateMap<FormQa1WeDTO, RmFormQa1We>().ReverseMap();
            this.CreateMap<FormQa1AttachmentDTO, RmFormQa1Image>().ReverseMap();
        }
    }
}
