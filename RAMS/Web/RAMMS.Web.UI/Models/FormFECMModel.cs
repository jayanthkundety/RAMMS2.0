using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAMMS.Web.UI.Models
{
    public class FormFECMModel
    {
        public FormW2FECMResponseDTO FECM { get; set; }

        public DateTime? W1Date { get; set; }

        public string View { get; set; }
    }
}
