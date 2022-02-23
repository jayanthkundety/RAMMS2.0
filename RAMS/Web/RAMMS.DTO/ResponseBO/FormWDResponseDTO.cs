using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormWDResponseDTO
    {
        public int PkRefNo { get; set; }
        public string RmuCode { get; set; }
        public string SecCode { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public int? Ch { get; set; }
        public int? ChDeci { get; set; }
        public string IwRefNo { get; set; }
        public string IwProjectTitle { get; set; }
        public string OurRefNo { get; set; }
        public string ServRefNo { get; set; }
        public DateTime? DtWd { get; set; }
        public DateTime? DtPervCompl { get; set; }
        public DateTime? DtExtn { get; set; }
        public bool? SignIssu { get; set; }
        public int? UseridIssu { get; set; }
        public string UsernameIssu { get; set; }
        public string DesignationIssu { get; set; }
        public DateTime? DtIssu { get; set; }
        public string OfficeIssu { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }
        
    }
}
