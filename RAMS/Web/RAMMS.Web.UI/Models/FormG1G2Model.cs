using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;


namespace RAMMS.Web.UI.Models
{
    public class FormG1G2Model
    {

        public FormQa1SearchGridDTO SearchObj { get; set; }
        public FormQa1HeaderDTO SaveFormQa1Model { get; set; }

        public IEnumerable<FormG1DTO> FormQa1HeaderList { get; set; }

        public DateTime? FormQa1Date { get; set; }
        public string SectionName { get; set; }

        public string RmuDescription { get; set; }

        public string DivisionName { get; set; }

        public string RoadDescription { get; set; }
        public string RoadCode { get; set; }       
       
        public string HeaderNo { get; set; }

        public string viewm { get; set; }

        public string MaxNo { get; set; }

        public CDataTable cDataTable { get; set; }

    }

    public class FormGImageModel
    {
        public string FormG1PKRefNo { get; set; }
        public string View { get; set; }

    }
}
