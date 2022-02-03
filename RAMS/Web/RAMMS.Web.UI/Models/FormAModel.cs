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
    public class FormAModel
    {
        public FormASearchGridDTO SearchObj { get; set; }

        public FormAHeaderRequestDTO SaveFormAModel { get; set; }

        public FormADetailsRequestDTO SaveFormADetails { get; set; }

        public IEnumerable<FormAHeaderResponseDTO> FormAHeaderList { get; set; }
        public IEnumerable<FormAImageListRequestDTO> AssetimageList { get; set; }
        public List<string> ImageTypeList { get; set; }
        public IEnumerable<SelectListItem> PhotoType { get; set; }

        public class SaveFormADetailsList
        {
            public List<FormADetailsRequestDTO> Items { get; set; }
        }

    }

}
