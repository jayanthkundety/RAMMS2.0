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
    public class FormW1Model
    {
        public FormW1Model()
        {
            FormW1 = new FormW1ResponseDTO();
        }

        public IEnumerable<FormIWImageResponseDTO> ImageList { get; set; }
        public FormW1ResponseDTO FormW1 { get; set; }
        public int View { get; set; }


    }
}
