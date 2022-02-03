using AutoMapper;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.RequestBO
{
    public class FormS1WkDtlDTO
    {
        public int PkRefNo { get; set; }
        public int? dPkRefNo { get; set; }
        public int? SchldDayOfWeek { get; set; }
        public int? Planned { get; set; }
        public int? Actual { get; set; }
        public DateTime? SchldDate { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }

        public string FormDFdhDay { get; set; }
        public string FormDFddWorkSts { get; set; }
        public static void Config(Profile profile)
        {
            profile.RecognizeDestinationPrefixes("Fsiwd", "FsiwdFsi");
            profile.RecognizePrefixes("Fsiwd", "FsiwdFsi");
            profile.CreateMap<FormS1WkDtlDTO, RmFormS1WkDtl>().ReverseMap();
        }


    }
}
