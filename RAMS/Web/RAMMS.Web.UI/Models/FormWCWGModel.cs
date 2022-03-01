using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAMMS.Web.UI.Models
{
    public class FormWCWGModel
    {
        public FormFECMModel FECM { get; set; }

        public FormWCResponseDTO FormWC { get; set; }

        public FormWGResponseDTO FormWG { get; set; }

        public FormW2Model FormW2 { get; set; }

        public string View { get; set; }

        public List<string> ImageTypeList { get; set; }
        public IEnumerable<FormIWImageResponseDTO> ImageList { get; set; }
        public IEnumerable<SelectListItem> PhotoType { get; set; }

        public IEnumerable<Division> Divisions { get; set; }

        public ServiceProvider ServiceProvider { get; set; }

       
    }

    public struct Division
    {
        public string Name;
        public string Adress1;
        public string Adress2;
        public string Adress3;
        public string Phone;
        public string Fax;
    }

    public struct ServiceProvider
    {
        public string Name;
        public string Adress1;
        public string Adress2;
        public string Adress3;
        public string Phone;
        public string Fax;
    }
}
