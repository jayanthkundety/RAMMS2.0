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
        public FormDHeaderRequestDTO SaveFormV2Model { get; set; }

        public IEnumerable<FormDHeaderResponseDTO> FormV2HeaderList { get; set; }

        public string SectionName { get; set; }

        public string RmuDescription { get; set; }

        public string DivisionName { get; set; }

        public string RoadDescription { get; set; }
        public string RoadCode { get; set; }       

        public FormDMaterialDetailsModel FormV2Material { get; set; }

        public FormDEquipDetailsModel FormV2Equip { get; set; }

        public FormDLabourDtlModel FormV2Labour { get; set; }

        public FormDDetailsDtlModel FormV2Details { get; set; }

        public FormDUserDetailsModel FormV2Users { get; set; }

        public FormDHeaderRequestDTO SaveUserModel { get; set; }

        public string HeaderNo { get; set; }

        public string viewm { get; set; }

        public string MaxNo { get; set; }

    }
}
