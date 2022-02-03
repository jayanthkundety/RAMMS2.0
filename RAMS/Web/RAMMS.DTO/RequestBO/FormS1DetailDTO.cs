using AutoMapper;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.RequestBO
{
    public class FormS1DetailDTO
    {
        public int PkRefNo { get; set; }
        public int? hPkRefNo { get; set; }
        public int? ActId { get; set; }
        public int? RoadId { get; set; }
        public int? FrmChKm { get; set; }
        public string FrmChM { get; set; }
        public int? ToChKm { get; set; }
        public string ToChM { get; set; }
        public string FormType { get; set; }
        public string RefId { get; set; }
        public int? CrewSupervisor { get; set; }
        public string CrewSupervisorName { get; set; }
        public bool? SentToJkrs { get; set; }
        public bool? RcvFromJkrs { get; set; }
        public DateTime? SentToJkrsDt { get; set; }
        public DateTime? RcvFromJkrsDt { get; set; }
        public string Remarks { get; set; }
        public int? FapRa { get; set; }
        public int? FapMt { get; set; }
        public int? FapQa1 { get; set; }
        public int? FapQa2 { get; set; }
        public int? FapSa { get; set; }
        public int? FapN1 { get; set; }
        public int? FapN2 { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }
        public int? FormTypeRefNo { get; set; }
        public string ActCode { get; set; }
        public string ActName { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public string FormASiteRef { get; set; }
        public string FormAPriority { get; set; }
        public string FormAWorkQty { get; set; }
        public string FormACdr { get; set; }
        public bool IsExist { get; set; }

        public int HdrWeekNo { get; set; }

        public List<ActWeekDtl> actWeekDtls { get; set; }

        //public virtual ICollection<FormS1WkDtlDTO> WkDtl { get; set; }
        public virtual IList<FormS1WkDtlDTO> WkDtl { get; set; }
        public static void Config(Profile profile)
        {
            profile.RecognizeDestinationPrefixes("Fsid", "Fsiid", "RmFormS1", "FsidFsi");
            profile.RecognizePrefixes("Fsid", "Fsiid", "RmFormS1", "FsidFsi");
            profile.CreateMap<FormS1DetailDTO, RmFormS1Dtl>().ReverseMap();
        }
        public class ActWeekDtl
        {
            public string FormDFdhDay { get; set; }
            public string FormDFddWorkSts { get; set; }
            public bool updtWekstus { get; set; }
        }
    }
}
