using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAMMS.Web.UI.Models
{
    public class FormFDModel
    {
        public IEnumerable<SelectListItem> AssetType { get; set; }
        public IEnumerable<SelectListItem> Bound { get; set; }
    }
}
