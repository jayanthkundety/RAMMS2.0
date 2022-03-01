using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAMMS.Web.UI.Models
{
    public class FormIWImageModel
    {
        public string IwRefNo { get; set; }
        public string View { get; set; }
        public List<string> ImageTypeList { get; set; }
        public IEnumerable<SelectListItem> PhotoType { get; set; }
        public IEnumerable<FormIWImageResponseDTO> ImageList { get; set; }

        public FormIWImageResponseDTO IWFormImage { get; set; }
    }
}
