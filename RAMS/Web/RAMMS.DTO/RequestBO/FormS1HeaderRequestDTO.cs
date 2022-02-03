using AutoMapper;
using AutoMapper.Configuration.Conventions;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;

namespace RAMMS.DTO.RequestBO
{
    public class FormS1HeaderRequestDTO
    {
        public FormS1HeaderRequestDTO()
        {

        }                        
        public int PkRefNo { get; set; }
        public string RefId { get; set; }
        public string Rmu { get; set; }
        public DateTime? Dt { get; set; }
        public int? WeekNo { get; set; }
        public DateTime? FromDt { get; set; }
        public DateTime? ToDt { get; set; }
        public int? UseridPlan { get; set; }
        public DateTime? DtPlan { get; set; }
        public int? UseridVet { get; set; }
        public DateTime? DtVet { get; set; }
        public int? UseridAgrd { get; set; }
        public DateTime? DtAgrd { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool? SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }
        public string Remarks { get; set; }
        public string UserNamePlan { get; set; }
        public string UserDesignationPlan { get; set; }
        public string UserNameVet { get; set; }
        public string UserDesignationVet { get; set; }
        public string UserNameAgrd { get; set; }
        public string UserDesignationAgrd { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }
        public static void Config(Profile profile)
        {
            profile.RecognizeDestinationPrefixes("Fsih", "Fsiih");
            profile.RecognizePrefixes("Fsih", "Fsiih");
            profile.CreateMap<FormS1HeaderRequestDTO, RmFormS1Hdr>()
                /*.ForMember(x => x.FsihActiveYn, opt => opt.MapFrom(x => x.ActiveYn))
                .ForMember(x => x.FsihCrBy, opt => opt.MapFrom(x => x.CrBy))
                .ForMember(x => x.FsihCrDt, opt => opt.MapFrom(x => x.CrDt))
                .ForMember(x => x.FsihDt, opt => opt.MapFrom(x => x.Dt))
                .ForMember(x => x.FsihFromDt, opt => opt.MapFrom(x => x.FromDt))
                .ForMember(x => x.FsihModBy, opt => opt.MapFrom(x => x.ModBy))
                .ForMember(x => x.FsihModDt, opt => opt.MapFrom(x => x.ModDt))
                .ForMember(x => x.FsihPkRefNo, opt => opt.MapFrom(x => x.PkRefNo))
                .ForMember(x => x.FsihRefId, opt => opt.MapFrom(x => x.RefId))
                .ForMember(x => x.FsihRemarks, opt => opt.MapFrom(x => x.Remarks))
                .ForMember(x => x.FsihRmu, opt => opt.MapFrom(x => x.Rmu))
                .ForMember(x => x.FsihSubmitSts, opt => opt.MapFrom(x => x.SubmitSts))
                .ForMember(x => x.FsihToDt, opt => opt.MapFrom(x => x.ToDt))
                .ForMember(x => x.FsihWeekNo, opt => opt.MapFrom(x => x.WeekNo))
                .ForMember(x => x.FsiihDtAgrd, opt => opt.MapFrom(x => x.DtAgrd))
                .ForMember(x => x.FsiihDtPlan, opt => opt.MapFrom(x => x.ActiveYn))
                .ForMember(x => x.FsiihDtVet, opt => opt.MapFrom(x => x.PkRefNo))
                .ForMember(x => x.FsiihUserDesignationAgrd, opt => opt.MapFrom(x => x.PkRefNo))
                .ForMember(x => x.FsiihUserDesignationPlan, opt => opt.MapFrom(x => x.ActiveYn))
                .ForMember(x => x.FsiihUserDesignationVet, opt => opt.MapFrom(x => x.PkRefNo))
                .ForMember(x => x.FsiihUseridAgrd, opt => opt.MapFrom(x => x.PkRefNo))
                .ForMember(x => x.FsiihUseridPlan, opt => opt.MapFrom(x => x.ActiveYn))
                .ForMember(x => x.FsiihUseridVet, opt => opt.MapFrom(x => x.PkRefNo))
                .ForMember(x => x.FsiihUserNameAgrd, opt => opt.MapFrom(x => x.PkRefNo))
                .ForMember(x => x.FsiihUserNamePlan, opt => opt.MapFrom(x => x.ActiveYn))
                .ForMember(x => x.FsiihUserNameVet, opt => opt.MapFrom(x => x.PkRefNo))  */              
                .ReverseMap();
        }
    }
}
