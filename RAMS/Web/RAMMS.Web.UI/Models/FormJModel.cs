using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;

namespace RAMMS.Web.UI.Models
{
    public class FormJModel
    {
        public FormJSearchGridDTO SearchObj { get; set; }

        public FormJHeaderRequestDTO SaveFormAModel { get; set; }

        public FormJDetailsRequestDTO SaveFormADetails { get; set; }

        public IEnumerable<FormJHeaderResponseDTO> FormAHeaderList { get; set; }
        public IEnumerable<FormJImageListRequestDTO> AssetimageList { get; set; }
        public List<string> ImageTypeList { get; set; }
        public IEnumerable<SelectListItem> PhotoType { get; set; }

        public class SaveFormJDetailsList
        {
            public List<FormJDetailsRequestDTO> Items { get; set; }
        }
    }
}
