using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;


namespace RAMMS.Web.UI.Models
{
    public class FormV2Model
    {

        public FormV2SearchGridDTO SearchObj { get; set; }
        public FormV2HeaderResponseDTO SaveFormV2Model { get; set; }

        public IEnumerable<FormV2HeaderResponseDTO> FormV2HeaderList { get; set; }

        public DateTime? formV2Date { get; set; }
        public string SectionName { get; set; }

        public string RmuDescription { get; set; }

        public string DivisionName { get; set; }

        public string RoadDescription { get; set; }
        public string RoadCode { get; set; }       

        public FormV2MaterialDetailsModel FormV2Material { get; set; }

        public FormV2EquipDetailsModel FormV2Equip { get; set; }

        public FormV2LabourDtlModel FormV2Labour { get; set; }

        //public FormV2UserDetailsModel FormV2Users { get; set; }

        public FormV2HeaderResponseDTO SaveUserModel { get; set; }

        public string HeaderNo { get; set; }

        public string viewm { get; set; }

        public string MaxNo { get; set; }

    }
}
