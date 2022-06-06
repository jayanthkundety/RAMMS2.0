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
    public class FormF3Model
    {


        public FormF3ResponseDTO FormF3 { get; set; }

        public FormF3DtlResponseDTO FormF3Dtl { get; set; }

        public List<SelectListItem> RefNoDS { get; set; }

        public int view { get; set; }

        public FormF3Model()
        {
            RefNoDS = new List<SelectListItem>();
        }


    }
}
