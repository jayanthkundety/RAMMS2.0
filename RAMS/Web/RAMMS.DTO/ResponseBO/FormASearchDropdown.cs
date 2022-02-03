using System;
using System.Collections.Generic;

namespace RAMMS.DTO.ResponseBO
{
    public class FormASearchDropdown
    {
        public List<DropDown> RMU { get; set; }
        public List<DropDown> Section { get; set; }
        public List<DropDown> RoadCode { get; set; }
        public List<DropDown> AssetGroup { get; set; }
    }

    public class DropDown
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public string CValue { get; set; }
    }

    public class RequestDropdownFormA
    {
        public string RMU { get; set; }
        public string Section { get; set; }
        public string RoadCode { get; set; }
        public string AssetGroup { get; set; }
    }
}
