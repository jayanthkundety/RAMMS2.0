using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO; 
using RAMMS.DTO.ResponseBO;

namespace RAMMS.Web.UI.Models
{
    public class FormV2UserDetailsModel
    {

        public string UserIdSch { get; set; }
        public string UsernameSch { get; set; }
        public string DesignationSch { get; set; }
        public string DateSch { get; set; }

        public string UserIdAck { get; set; }
        public string UsernameAck { get; set; }
        public string DesignationAck { get; set; }
        public string DateAck { get; set; }

        public string UserIdAgr { get; set; }
        public string UsernameAgr { get; set; }
        public string DesignationAgr { get; set; }
        public string DateAgr { get; set; }

        public string ServiceProvider { get; set; }
        public string Verifier { get; set; }
        public string Facilator { get; set; }

        public string  V2PkRefNo { get; set; }
    }
}
