using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormV3DtlGridDTO
    {
        public int PkRefNo { get; set; }
        public int? Fv3hPkRefNo { get; set; }
        public int? Fv1dPkRefNo { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public int? FrmCh { get; set; }
        public int? FrmChDeci { get; set; }
        public int? ToCh { get; set; }
        public int? ToChDeci { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public string TimetakenFrm { get; set; }
        public string  TimeTakenTo { get; set; }
        public decimal? TimeTakenTotal { get; set; }
        public string TransitTimeFrm { get; set; }
        public string TransitTimeTo { get; set; }
        public decimal? TransitTimeTotal { get; set; }
        public int? Adp { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }

        public string ChainageFrom { get; set; }
        public string ChainageTo { get; set; }


    }
}
