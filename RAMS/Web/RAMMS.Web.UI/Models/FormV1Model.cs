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
    public class FormV1Model
    {
        public FormV1ResponseDTO FormV1 { get; set; }

        public FormV1DtlResponseDTO FormV1Dtl { get; set; }

        public List<SelectListItem> RefNoDS { get; set; }

        public string SectionName { get; set; }

        public string RmuDescription { get; set; }

        public string DivisionName { get; set; }

        public string SecDescription { get; set; }
        public string SecCode { get; set; }

        public int view { get; set; }

        public FormV1Model()
        {
            RefNoDS = new List<SelectListItem>();
        }


    }
}
