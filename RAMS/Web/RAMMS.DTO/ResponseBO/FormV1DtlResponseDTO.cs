using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormV1DtlResponseDTO
    {
        public int PkRefNo { get; set; }
        public int? Fv1hPkRefNo { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public int? FrmCh { get; set; }
        public int? FrmChDeci { get; set; }
        public int? ToCh { get; set; }
        public int? ToChDeci { get; set; }
        public string SiteRef { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string StartTime { get; set; }
        public string Remarks { get; set; }
        public string ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public string CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public int S1DPkRefNo { get; set; }

    }
}
