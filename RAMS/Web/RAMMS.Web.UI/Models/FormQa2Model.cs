using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;

namespace RAMMS.Web.UI.Models
{
    public class FormQa2Model
    {
        public FormQa2SearchGridDTO SearchObj { get; set; }
        public FormQa2HeaderRequestDTO SaveFormQa2Model { get; set; }

        public FormQa2DtlRequestDTO SaveFormQa2DtlModel { get; set; }

        public string SectionName { get; set; }

        public string RmuDescription { get; set; }

        public string DivisionDescription { get; set; }

        public string RoadDescription { get; set; }

        public string HeaderNo { get; set; }

        public string viewm { get; set; }
    }
}



   

       
