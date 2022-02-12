using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RAMMS.DTO.ResponseBO;

namespace RAMMS.Web.UI.Models
{
    public class FormIWModel
    {

        public FormIWSearchGridDTO SearchObj { get; set; }

        public IEnumerable<FormIWResponseDTO> FormIWHeaderList { get; set; }

    }
}
