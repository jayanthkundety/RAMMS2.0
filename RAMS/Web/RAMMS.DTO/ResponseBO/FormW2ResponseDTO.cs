using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormW2ResponseDTO
    {
        public int PkRefNo { get; set; }
        public string Fw1IwRefNo { get; set; }
        public int? Fw1RefNo { get; set; }
        public string JkrRefNo { get; set; }
        public string SerProviderRefNo { get; set; }
        public string ServiceProvider { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DtInitation { get; set; }
        public string Region { get; set; }
        public string RegionName { get; set; }
        public string Division { get; set; }
        public string DivisonName { get; set; }
        public string Rmu { get; set; }
        public string RmuName { get; set; }
        public string Attn { get; set; }
        public string Cc { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public int? ChKm { get; set; }
        public int? ChM { get; set; }
        public string TitleOfInstructWork { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DtCommence { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DtCompl { get; set; }
        public double? IwDuration { get; set; }
        public string Remarks { get; set; }
        public string DetailsOfWorks { get; set; }
        public double? CeilingEstCost { get; set; }
        public int? UseridIssu { get; set; }
        public bool? SignIssu { get; set; }
        public string UsernameIssu { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DtIssu { get; set; }
        public bool? SignReq { get; set; }
        public int? UseridReq { get; set; }
        public string UsernameReq { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DtReq { get; set; }
        public int? ModBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? CrDt { get; set; }
        public bool? SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }

        public virtual FormW1ResponseDTO Fw1RefNoNavigation { get; set; }
        public virtual ICollection<RmIwFormW2Fcem> RmIwFormW2Fcem { get; set; }
        public virtual ICollection<RmIwFormW2Image> RmIwFormW2Image { get; set; }
    }
}
