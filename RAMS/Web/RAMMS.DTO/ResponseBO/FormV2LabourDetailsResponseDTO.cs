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
        public decimal? Qty { get; set; }
        public string Unit { get; set; }
        public string Remark { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }

        public virtual FormV2HeaderResponseDTO FormV2 { get; set; }


    }
}
