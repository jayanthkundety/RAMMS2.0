using System;
using System.Collections.Generic;
using System.Text;
using RAMMS.Common;
//using RAMMS.Common.ServiceProvider;
using RAMMS.Domain.Models;
using System.Threading.Tasks;
using RAMS.Repository;
using Microsoft.Extensions.Logging;
using System.Linq;
using RAMMS.DTO;
using Microsoft.AspNetCore.Http;

namespace RAMMS.Business.ServiceProvider
{
    public class MltyModelBO
    {
        //private readonly IFormAProvider _FormAProvider;
        //public MltyModelBO(IFormAProvider _FormAProd)
        //{
        //    _FormAProvider = _FormAProd;
        //}
        public RmFormAHdr _FormAHdr { get; set; }
        public IEnumerable<RmFormADtl> _FormADtl { get; set; }

        public RmAllassetInventory _BridgeInvSave { get; set; }

        public IEnumerable<RmAllassetInventory> _BridgeInvGrid { get; set; }

        public IEnumerable<RmAssetImageDtl> _assetImg { get; set; }
        public IEnumerable<SearchBridge> searchObj { get; set; }

        public IFormFile file { get; set; }

        public MultiSelectDropDownViewModel _multiSelectDropDownViewModel { get; set; }

        public RmAllassetInventory _rmAllassetInventory { get; set; }
       
    }
}
