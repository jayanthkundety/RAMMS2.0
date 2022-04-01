using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormV1DtlResponseDTO
    {
        public int Fv1dPkRefNo { get; set; }
        public int? Fv1dFv1hPkRefNo { get; set; }
        public string Fv1dRoadCode { get; set; }
        public string Fv1dRoadName { get; set; }
        public int? Fv1dFrmCh { get; set; }
        public int? Fv1dFrmChDeci { get; set; }
        public int? Fv1dToCh { get; set; }
        public int? Fv1dToChDeci { get; set; }
        public string Fv1dSiteRef { get; set; }
        public DateTime? Fv1dStartTime { get; set; }
        public string Fv1dRemarks { get; set; }
        public string Fv1dModBy { get; set; }
        public DateTime? Fv1dModDt { get; set; }
        public string Fv1dCrBy { get; set; }
        public DateTime? Fv1dCrDt { get; set; }
        public bool? Fv1dSubmitSts { get; set; }
        public bool? Fv1dActiveYn { get; set; }
         
    }
}
