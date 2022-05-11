using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;


namespace RAMMS.Web.UI.Models
{
    public class FormV5Model
    {

        public FormV1SearchGridDTO SearchObj { get; set; }
        public FormV5ResponseDTO FormV5 { get; set; }
        public FormV5DtlResponseDTO FormV5Dtl { get; set; }

        public int view { get; set; }

       
    }
}
