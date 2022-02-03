using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.MobileApps;

namespace RAMMS.Web.UI.Models
{
    public class FormAModel
    {
        public FormAHeaderRequestDTO SearchObj { get; set; }

        public FormAHeaderRequestDTO SaveFormAModel { get; set; }

        public FormADetailsRequestDTO SaveFormADetails { get; set; }

        public IEnumerable<FormAHeaderResponseDTO> FormAHeaderList { get; set; }

        public class SaveFormADetailsList
        {
            public List<FormADetailsRequestDTO> Items { get; set; }
        }

    }

}
