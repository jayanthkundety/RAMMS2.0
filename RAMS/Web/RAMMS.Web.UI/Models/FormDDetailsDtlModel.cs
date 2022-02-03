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
    public class FormDDetailsDtlModel
    {

        public FormDSearchGridDTO SearchObj { get; set; }
        public FormDDetailsRequestDTO SaveFormDDetailsModel { get; set; }

        public IEnumerable<FormDDetailsRequestDTO> FormDDetailsDtlList { get; set; }

        public string ActivityDescription { get; set; }
        public string RoadDescription { get; set; }

        public string HeaderNo { get; set; }
        public IEnumerable<SelectListItem> siteRefList { get; set; }
        public List<string> SiteRef_multiSelect { get; set; }
        public IEnumerable<SelectListItem> ActCodeList { get; set; }
        public IEnumerable<SelectListItem> RoadCodeList { get; set; }
        public IEnumerable<SelectListItem> WrkStatusList { get; set; }

        public IEnumerable<SelectListItem> UnitList { get; set; }
        public IEnumerable<SelectListItem> sourceTypefList { get; set; }

        public IEnumerable<SelectListItem> sourceFormList { get; set; }

        public bool isFromSource { get; set; }


    }
}
    

