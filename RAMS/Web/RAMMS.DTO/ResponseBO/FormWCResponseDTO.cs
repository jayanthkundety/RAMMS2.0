using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormWCResponseDTO
    {
        public int PkRefNo { get; set; }
        public int? Fw1PkRefNo { get; set; }

        public int? IwWrksDeptId { get; set; }
        public string RmuCode { get; set; }
        public string SecCode { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public int? Ch { get; set; }
        public int? ChDeci { get; set; }
        public string IwRefNo { get; set; }
        public string IwProjectTitle { get; set; }
        public string OurRefNo { get; set; }
        public string YourRefNo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DtWc { get; set; }
       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DtCompl { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DtDlpExtn { get; set; }

        public double? DlpPeriod { get; set; }
        public bool? SignIssu { get; set; }
        public int? UseridIssu { get; set; }
        public string UsernameIssu { get; set; }
        public string DesignationIssu { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
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

        public virtual FormW1ResponseDTO Fw1PkRefNoNavigation { get; set; }
    }
}
