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
    public class FormF3Model
    {


        public FormF3ResponseDTO FormF3 { get; set; }

        public FormF3DtlResponseDTO FormF3Dtl { get; set; }

        public IEnumerable<AssetListItem> AssetDS { get; set; }
      
        public int view { get; set; }

       

    }


    public class AssetListItem 
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public string Code { get; set; }
        public string CValue { get; set; }
        public string Group { get; set; }
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Item3 { get; set; }
        public int PKId { get; set; }

        public int FromKm { get; set; }
        public string FromM { get; set; }

        public int ToKm { get; set; }
        public string ToM { get; set; }
        public string Office { get; set; }

    }
}
