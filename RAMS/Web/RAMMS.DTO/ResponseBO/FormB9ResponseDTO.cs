using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormB9ResponseDTO
    {
        public int B9dsPkRefNo { get; set; }
        public int? B9dsRevisionNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? B9dsRevisionDate { get; set; }
        public int? B9dsRevisionYear { get; set; }
        public int? B9dsCrBy { get; set; }
        public DateTime? B9dsCrDt { get; set; }
 
    }
}
