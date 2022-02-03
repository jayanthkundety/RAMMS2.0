using AutoMapper;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class RmFormCvInsDtDTO
    {
        public int PkRefNo { get; set; }
        public int? hPkRefNo { get; set; }
        public int? mdPkRefNo { get; set; }
        public string Distress { get; set; }
        public string[] ArDistress { get; set; }
        //{
        //    get { return Distress != null ? Distress.Split(",") : new string[0]; }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            Distress = "";
        //            foreach (var v in value)
        //            {
        //                if (Distress != "")
        //                {
        //                    Distress += ",";
        //                }
        //                Distress += value;
        //            }
        //        }
        //    }
        //}
        public int? Severity { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime CrDt { get; set; }
        public bool? SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }
        public string InspCode { get; set; }
        public string InspCodeDesc { get; set; }
        public int? mPkRefNo { get; set; }

        public string DistressOthers { get; set; }
        public RmInspItemMasterDTO mPkRefNoNavigation { get; set; }
        //public static void Config(Profile profile)
        //{
        //    profile.RecognizeDestinationPrefixes("Fcvid", "FcvidFcvi", "FcvidIi");
        //    profile.RecognizePrefixes("Fcvid", "FcvidFcvi", "FcvidIi");
        //    profile.CreateMap<RmFormCvInsDtDTO, RmFormCvInsDtl>().ReverseMap();                
        //        //.ForMember(x => x.IimdPkRefNo, options => options.MapFrom(input => input.FcvidIimdPkRefNo));
        //    //profile.CreateMap<RmFormCvInsDtl, RmFormCvInsDtDTO>().ForMember(x => x.PkRefNo, options => options.MapFrom(input => input.FcvidPkRefNo));
        //}
    }
}
