using AutoMapper;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormC1C2ImageDTO
    {
        public int PkRefNo { get; set; }
        public int? hPkRefNo { get; set; }
        public string ImgRefId { get; set; }
        public string ImageTypeCode { get; set; }
        public int? ImageSrno { get; set; }
        public string ImageFilenameSys { get; set; }
        public string ImageFilenameUpload { get; set; }
        public string ImageUserFilePath { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }
        //public static void Config(Profile profile)
        //{
        //    profile.RecognizeDestinationPrefixes("Fcvi", "FcviFcvi");
        //    profile.RecognizePrefixes("Fcvi", "FcviFcvi");
        //    profile.CreateMap<FormC1C2ImageDTO, RmFormCvInsImage>().ReverseMap();
        //}
    }
}
