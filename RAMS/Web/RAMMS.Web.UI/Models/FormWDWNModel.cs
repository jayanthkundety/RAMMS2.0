using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.ResponseBO;

namespace RAMMS.Web.UI.Models
{
    public class FormWDWNModel
    {

        public FormFECMModel FECM { get; set; }
       
        public FormW1ResponseDTO FormW1 { get; set; }
        //public FormWDResponseDTO FormWD { get; set; }

        //public FormWNResponseDTO FormWN { get; set; }

        //public FormWDDtlResponseDTO FormWDDtl { get; set; }

        public string View { get; set; }

        public List<string> ImageTypeList { get; set; }
        public IEnumerable<FormIWImageResponseDTO> ImageList { get; set; }
        public IEnumerable<SelectListItem> PhotoType { get; set; }


    }
}
