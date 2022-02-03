using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;

namespace RAMMS.Web.UI.Models
{
    public class FormN1Model
    {

        public FormN1SearchGridDTO SearchObj { get; set; }

        public FormN1HeaderRequestDTO SaveFormN1Model { get; set; }

        public IEnumerable<FormN1HeaderResponseDTO> FormN1HeaderList { get; set; }

        public string SectionName { get; set; }

        public string RmuDescription { get; set; }

        public string DivisionDescription { get; set; }

        public string RoadDescription { get; set; }

        public string HeaderNo { get; set; }

        public string viewm { get; set; }
    }
}
