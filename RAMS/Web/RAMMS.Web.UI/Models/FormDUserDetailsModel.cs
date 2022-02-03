using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;

namespace RAMMS.Web.UI.Models
{
    public class FormDUserDetailsModel
    {

        public string RecordedByName { get; set; }
        public string RecordedByDesign { get; set; }
        public string RecordedByDate { get; set; }
        public string VettedByName { get; set; }
        public string VettedDesign { get; set; }
        public string VettedByDate { get; set; }

        public string VerifiedByName { get; set; }
        public string VerifiedDesign { get; set; }
        public string VerifiedDate { get; set; }

        public string EnggVerifiedByName { get; set; }
        public string EnggVerifiedDesign { get; set; }
        public string EnggVerifiedDate { get; set; }

        public string EnggAgreedByName { get; set; }
        public string EnggAgreedByDesign { get; set; }
        public string EnggAgreedByDate { get; set; }

        public string EnggProcessedByName { get; set; }
        public string EnggProcessedByDesign { get; set; }
        public string EnggProcessedByDate { get; set; }

        
        public string  HeaderNo { get; set; }
    }
}
