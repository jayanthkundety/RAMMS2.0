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
    public class FormDEquipDetailsModel
    {
        public FormDSearchGridDTO SearchObj { get; set; }
        public FormDEquipRequestDTO SaveFormDEquipModel { get; set; }

        public IEnumerable<FormDEquipDetailsResponseDTO> FormDEquipDtlList { get; set; }

        public string HeaderNo { get; set; }
        public string EquipmentDesc { get; set; }

        public IEnumerable<SelectListItem> selectList { get; set; }

        public IEnumerable<SelectListItem> UnitList { get; set; }

    }
}
 