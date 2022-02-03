using System;
using System.Collections.Generic;
using RAMMS.MobileApps.Services;

namespace RAMMS.MobileApps
{
    public class JAssetDropDownData
    {
        public bool Disabled { get; set; }
        public object Group { get; set; }
        public bool Selected { get; set; }
        public string Text { get; set; }
        public string Value { get; set; }
        public bool isSelected { get; set; }
    }

    public class JAssetDropDown : ResponseBase
    {
        public List<JAssetDropDownData> data { get; set; }
    }
}
