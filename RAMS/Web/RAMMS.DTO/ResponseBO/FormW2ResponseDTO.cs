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
        public int? Fw1PkRefNo { get; set; }
        public string Fw1ProjectTitle { get; set; }
        public string JkrRefNo { get; set; }
        public string SerProvRefNo { get; set; }
        public string ServProvName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DateOfInitation { get; set; }
        public string RegionText { get; set; }
        public string RegionName { get; set; }
        public string DivText { get; set; }
        public string DivCode { get; set; }
        public string DivisonName { get; set; }
        public string RmuText { get; set; }
        public string RmuCode { get; set; }
        public string RmuName { get; set; }
        public string SecCode { get; set; }
        public string Attn { get; set; }
        public string Cc { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public int? Ch { get; set; }
        public int? ChDeci { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DtCommence { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DtCompl { get; set; }
        public decimal? IwDuration { get; set; }
        public string Remarks { get; set; }
        public string DetailsOfWorks { get; set; }
        public double? EstCostAmt { get; set; }
        public int? UseridIssu { get; set; }
        public bool? SignIssu { get; set; }
        public string UsernameIssu { get; set; }
        public string DesignationIssu { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DtIssu { get; set; }
        public string OfficeIssu { get; set; }
        public bool? SignReq { get; set; }
        public int? UseridReq { get; set; }
        public string UsernameReq { get; set; }
        public string DesignationReq { get; set; }
        public string OfficeReq { get; set; }
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
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }

        public virtual FormW1ResponseDTO Fw1PkRefNoNavigation { get; set; }
        public virtual ICollection<RmIwFormW2Fecm> RmIwFormW2Fecm { get; set; }
        public virtual ICollection<RmIwFormW2Image> RmIwFormW2Image { get; set; }
    }
}
