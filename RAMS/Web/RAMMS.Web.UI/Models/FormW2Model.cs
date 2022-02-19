using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.ResponseBO;

namespace RAMMS.Web.UI.Models
{
    public class FormW2Model
    {
        public FormW2ResponseDTO SaveFormW2Model { get; set; }

        public FormIWSearchGridDTO SearchObj { get; set; }

        public string View { get; set; }

        public List<string> ImageTypeList { get; set; }
        public IEnumerable<FormIWImageResponseDTO> ImageList { get; set; } 
        public IEnumerable<SelectListItem> PhotoType { get; set; }

        public FormW1ResponseDTO FormW1 { get; set; }

        public FormW2FCEMResponseDTO Fcem { get; set; }
    }
}
