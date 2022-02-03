using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.RequestBO;

namespace RAMMS.Web.UI.Models
{
    public class FormB1B2ImageModel
    {
        public List<FormB1B2ImgRequestDTO> AssetimageList { get; internal set; }
        public List<string> ImageTypeList { get; internal set; }
        public IEnumerable<SelectListItem> PhotoType { get; internal set; }
    }
}
