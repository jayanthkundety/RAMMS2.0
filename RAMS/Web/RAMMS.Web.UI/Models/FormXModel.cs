using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.SearchBO;

namespace RAMMS.Web.UI.Models
{
    public class FormXModel
    {

        public FormXSearchGridDTO SearchObj { get; set; }

        public FormXSearchDTO FormXSearchObj { get; set; }
        public FormXHeaderRequestDTO SaveFormXModel { get; set; }

        public IEnumerable<FormXHeaderResponseDTO> FormXHeaderList { get; set; }

        public string SectionName { get; set; }

        public List<string> ImageTypeList { get; set; }
        public IEnumerable<WarImageDtlResponseDTO> WarImageimageList { get; set; }
        public IEnumerable<AccUccImageDtlResponseDTO> AccUccImageList { get; set; }
        public AccUccImageDtlResponseDTO AccUccImage { get; set; }

        public WarImageDtlResponseDTO WarDocData { get; set; }

    }
}
