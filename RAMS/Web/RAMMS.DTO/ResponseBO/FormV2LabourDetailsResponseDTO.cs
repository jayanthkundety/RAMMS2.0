using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RAMMS.DTO.ResponseBO
{
    public class FormV2LabourDetailsResponseDTO
    {

        public int PkRefNo { get; set; }
        public int? Fv2hPkRefNo { get; set; }
        public string LabRefCode { get; set; }
        public string Desc { get; set; }
        public string Remark { get; set; }
        public string ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public string CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool? SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }

        public virtual FormV2HeaderResponseDTO Fv2hPkRefNoNavigation { get; set; }


    }
}
