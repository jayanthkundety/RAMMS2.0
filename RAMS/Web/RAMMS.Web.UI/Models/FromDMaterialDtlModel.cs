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
    public class FormDMaterialDetailsModel

    {
        public FormDSearchGridDTO SearchObj { get; set; }
        public FormDMaterialDetailsRequestDTO SaveFormDMaterialModel { get; set; }

        public IEnumerable<FormDMaterialDetailsResponseDTO> FormDMaterialDtlList { get; set; }

        public string HeaderNo { set; get; }
        public string MaterialDesc { get; set; }

        public IEnumerable<SelectListItem> selectList { get; set; }

        public IEnumerable<SelectListItem> UnitList { get; set; }

    }
}
