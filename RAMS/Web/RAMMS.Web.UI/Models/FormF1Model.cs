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
    public class FormF1Model
    {

        public FormF1ResponseDTO FormF1 { get; set; }

        public FormF1DtlResponseDTO FormF1Dtl { get; set; }

        public IEnumerable<AssetListItem> AssetDS { get; set; }
      
        public int view { get; set; }


    }

 
}
