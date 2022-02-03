using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using RAMMS.MobileApps.Services;

namespace RAMMS.MobileApps
{
    public class RMU
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class Sections
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class RdCode
    {
        public string Text { get; set; }
        public string Value { get; set; }
    }

    public class Data
    {
        public List<RMU> RMU { get; set; }
        public List<Sections> Section { get; set; }
        public List<RdCode> RdCode { get; set; }
    }

    public class FormJDropDownResponseData:ResponseBase
    {
        public Data data { get; set; }
    }
}
