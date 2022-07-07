using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;


namespace RAMMS.Web.UI.Models
{
    public class FormTModel
    {

        public FormTResponseDTO FormT { get; set; }

        public FormTDtlResponseDTO FormTDtl { get; set; }

        public FormTVehicleResponseDTO FormTVechicle { get; set; }

        public int view { get; set; }


    }

 
}
