using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.Business.ServiceProvider
{
    public class CSelectListItem<T> : SelectListItem
    {
        public T Key { get; set; }
        public string Code { get; set; }
        public string CValue { get; set; }
        public string Group { get; set; }
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Item3 { get; set; }
        public int PKId { get; set; }

        public int FromKm { get; set; }
        public string FromM { get; set; }

        public int ToKm { get; set; }
        public string ToM { get; set; }

    }
    public class CSelectListItem : CSelectListItem<string>
    {
    }
}
