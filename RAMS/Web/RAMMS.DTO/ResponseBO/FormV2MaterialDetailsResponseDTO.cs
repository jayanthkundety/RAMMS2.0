using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RAMMS.DTO.ResponseBO
{
    public class FormV2MaterialDetailsResponseDTO
    {

        public int PkRefNo { get; set; }
        public int? Fv2hPkRefNo { get; set; }
        public string MatRefCode { get; set; }
        public string Desc { get; set; }
        public decimal? Qnty { get; set; }
        public int? Unit { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }

        public virtual FormV2HeaderResponseDTO Fv2hPkRefNoNavigation { get; set; }


    }
}
