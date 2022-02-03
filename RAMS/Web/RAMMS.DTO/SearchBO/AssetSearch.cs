using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.SearchBO
{
    public class AssetSearch
    {
        public string SmartInputValue { get; set; }
        public string GroupCode { get; set; }
        public string AssetId { get; set; }
        public int? FromCh { get; set; }
        public int? ToCh { get; set; }
        public string FromChDesi { get; set; }
        public string ToChDeci { get; set; }
        public string RMUCode { get; set; }
         public string RMUName { get; set; }
        public string GroupType { get; set; }
        public string SectionName { get; set; }
        public string SectionCode { get; set; }
        public string RoadName { get; set; }
        public string RoadCode { get; set; }
        public string Bound { get; set; }
        public string AssetImageType { get; set; }
        public string sortOrder { get; set; }
    }
}
