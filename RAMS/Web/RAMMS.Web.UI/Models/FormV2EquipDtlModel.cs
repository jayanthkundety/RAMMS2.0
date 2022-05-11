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
    public class FormV2EquipDetailsModel
    {
        public FormV2SearchGridDTO SearchObj { get; set; }
        public FormV2EquipDetailsResponseDTO SaveFormV2EquipModel { get; set; }

        public IEnumerable<FormV2EquipDetailsResponseDTO> FormV2EquipDtlList { get; set; }

        public string HeaderNo { get; set; }

        public string EquipmentDesc { get; set; }

        public IEnumerable<SelectListItem> selectList { get; set; }

        public IEnumerable<SelectListItem> ModelList { get; set; }

    }
}
 