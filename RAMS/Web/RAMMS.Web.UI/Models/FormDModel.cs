using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;


namespace RAMMS.Web.UI.Models
{
    public class FormDModel
    {

        public FormDSearchGridDTO SearchObj { get; set; }
        public FormDHeaderRequestDTO SaveFormDModel { get; set; }

        public IEnumerable<FormDHeaderResponseDTO> FormDHeaderList { get; set; }

        public string SectionName { get; set; }

        public string RmuDescription { get; set; }

        public string DivisionName { get; set; }

        public string RoadDescription { get; set; }
        public string RoadCode { get; set; }

        public List<string> ImageTypeList { get; set; }
        public IEnumerable<WarImageDtlResponseDTO> WarImageimageList { get; set; }
        public IEnumerable<AccUccImageDtlResponseDTO> AccUccImageList { get; set; }
        public AccUccImageDtlResponseDTO AccUccImage { get; set; }

        public FormDMaterialDetailsModel FormDMaterial { get; set; }

        public FormDEquipDetailsModel FormDEquip { get; set; }

        public FormDLabourDtlModel FormDLabour { get; set; }

        public FormDDetailsDtlModel FormDDetails { get; set; }

        public FormDUserDetailsModel FormDUsers { get; set; }

        public FormDHeaderRequestDTO SaveUserModel { get; set; }

        public string HeaderNo { get; set; }

        public string viewm { get; set; }

        public string MaxNo { get; set; }

    }
}
