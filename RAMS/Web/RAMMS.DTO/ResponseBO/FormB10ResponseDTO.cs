using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormB10ResponseDTO
    {

        public int B10dpPkRefNo { get; set; }
        public int? B10dpRevisionNo { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? B10dpRevisionDate { get; set; }
        public int? B10dpRevisionYear { get; set; }
        public int? B10dpCrBy { get; set; }
        public DateTime? B10dpCrDt { get; set; }
       
 
    }
}
