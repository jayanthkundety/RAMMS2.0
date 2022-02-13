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

        public string View { get; set; }
        public List<string> ImageTypeList { get; set; }
        public IEnumerable<FormW1ImageResponseDTO> ImageList { get; set; }
        public IEnumerable<SelectListItem> PhotoType { get; set; }
        public FormW1ResponseDTO FormW1 { get; set; }


    }
}
