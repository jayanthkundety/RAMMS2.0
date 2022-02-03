using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.RequestBO
{
    public class AssetFieldDtlReqDTO
    {
        public string AssetType { get; set; }
        public string Code { get; set; }
        public string FieldName { get; set; }
        public string DisplayName { get; set; }
    }
}
