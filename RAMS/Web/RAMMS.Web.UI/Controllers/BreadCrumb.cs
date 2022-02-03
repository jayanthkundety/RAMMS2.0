using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RAMMS.Web.UI.Models
{
    public class BreadCrumb
    {
        public string Title { get; set; }
        public IList<BreadCrumbNavigation> Navigation { get; set; } = new List<BreadCrumbNavigation>();
        public string SimpleNavigation
        {
            set
            {
                Array.ForEach(value.Split(','), nav => Navigation.Add(new BreadCrumbNavigation(nav)));
            }
        }
    }
    public class BreadCrumbNavigation
    {
        public BreadCrumbNavigation()
        {

        }
        public BreadCrumbNavigation(string title)
        {
            Title = title;
        }
        public BreadCrumbNavigation(string title, BreadCrumbNavType type)
        {
            Title = title;
            Type = type;
        }
        public string Title { get; set; }
        public BreadCrumbNavType Type { get; set; } = BreadCrumbNavType.Label;
    }
    public enum BreadCrumbNavType
    {
        Label,
        Link
    }
}
