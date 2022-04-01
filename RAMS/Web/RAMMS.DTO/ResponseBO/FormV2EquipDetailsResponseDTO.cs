using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RAMMS.DTO.ResponseBO
{
    public class FormV2EquipDetailsResponseDTO
    {

        public int PkRefNo { get; set; }
        public int? Fv2hPkRefNo { get; set; }
        public string EqpRefCode { get; set; }
        public string Desc { get; set; }
        public string Capacity { get; set; }
        public int? Cond { get; set; }
        public string Model { get; set; }
        public string ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public string CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }

        public virtual FormV2SearchGridDTO Fv2hPkRefNoNavigation { get; set; }


    }
}
