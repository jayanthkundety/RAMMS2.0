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
        public int Fw1RefNo { get; set; }
        public string Fw1ProjectTitle { get; set; }
        public string JkrRefNo { get; set; }
        public string SerProviderRefNo { get; set; }
        public string ServiceProvider { get; set; }
        [DisplayFormat(DataFormatString = "{​​​​​0:yyyy-MM-dd}​​​​​", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DateOfInitation { get; set; }
        public string Region { get; set; }
        public string Division { get; set; }
        public string Rmu { get; set; }
        public int? Attn { get; set; }
        public string Cc { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public int? FrmCh { get; set; }
        public int? ToCh { get; set; }
        public string TitleOfInstructWork { get; set; }
        [DisplayFormat(DataFormatString = "{​​​​​0:yyyy-MM-dd}​​​​​", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DateOfCommencement { get; set; }

        [DisplayFormat(DataFormatString = "{​​​​​0:yyyy-MM-dd}​​​​​", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DateOfCompletion { get; set; }
        public decimal? InstructWorkDuration { get; set; }
        public string Remarks { get; set; }
        public string DetailsOfWorks { get; set; }
        public decimal? CeilingEstCost { get; set; }
        public string IssuedBy { get; set; }
        public bool? IssuedSignature { get; set; }
        public string IssuedName { get; set; }

        [DisplayFormat(DataFormatString = "{​​​​​0:yyyy-MM-dd}​​​​​", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? IssuedDate { get; set; }

        public string ReceivedBy { get; set; }
        public bool? ReceivedSignature { get; set; }
        public string ReceivedName { get; set; }

        [DisplayFormat(DataFormatString = "{​​​​​0:yyyy-MM-dd}​​​​​", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? ReceivedDate { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }

        public string Status { get; set; }

        public string AuditLog { get; set; }

        public virtual FormW1ResponseDTO Fw1RefNoNavigation { get; set; }
    }
}
