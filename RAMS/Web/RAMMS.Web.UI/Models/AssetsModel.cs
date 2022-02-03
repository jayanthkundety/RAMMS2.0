using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.RequestBO;
using RAMMS.Business.ServiceProvider;
using RAMMS.Domain.Models;

namespace RAMMS.Web.UI.Models
{
    public class AssetsModel
    {
        public IEnumerable<AssetsListResponseDTO> assetList { get; set; }

        //AssetFieldDtl Grid Model
        public AssetListRequestDTO AssetListRequest { get; set; }

        //Asset Add Model
        public AssetListRequestDTO AddAssetViewModel { get; set; }
        public IEnumerable<ImageListRequestDTO> AssetimageList { get; set; }

        public ImageListRequestDTO AssetDoc { get; set; }
        public List<string> ImageTypeList { get; set; }

        //Asset Other Inventory for Add modal
        public AssetInvOtherReqDto addAssetOtherModel { get; set; }

    }
}
