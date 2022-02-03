using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO
{
    public class AssetMaster
    {
        public AssetMaster() { }
        public AssetMaster(string assetCode, string assetName)
        {
            AssetCode = assetCode;
            AssetName = assetName;
        }
        public string AssetCode { get; set; }
        public string AssetName { get; set; }
    }
}
