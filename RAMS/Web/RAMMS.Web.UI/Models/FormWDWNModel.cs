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
        public FormW2ResponseDTO FormW2 { get; set; }
        public FormWDResponseDTO FormWD { get; set; }

        public FormWNResponseDTO FormWN { get; set; }
        public string FormName { get; set; }

        public string  ClauseDetails { get; set; }
        public IEnumerable<FormWDDtlResponseDTO> FormWDDtl { get; set; }

        public int View { get; set; }

        public List<string> ImageTypeList { get; set; }
        public IEnumerable<FormIWImageResponseDTO> ImageList { get; set; }
        public IEnumerable<SelectListItem> PhotoType { get; set; }

        public DTO.RequestBO.DivisionRequestDTO Division { get; set; }


    }
}
