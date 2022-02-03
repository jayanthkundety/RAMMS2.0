using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.SearchBO;

namespace RAMMS.Web.UI.Models
{
    public class FormHModel
    {
        public FormHRequestDTO FormHDetail { get; set; }
        public FormHSearchDTO SearchFormH { get; set; }
        public List<FormHImageListRequestDTO> AssetimageList { get; internal set; }
        public List<string> ImageTypeList { get; internal set; }
        public IEnumerable<SelectListItem> PhotoType { get; internal set; }
    }
}
