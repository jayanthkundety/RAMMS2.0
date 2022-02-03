using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RAMMS.MobileApps
{
   public class EntryControl : Entry
    {
        public string EntrySubType { get; set; }

        public string CustomId { get; set; }

        public EntryControl()
        {
            FontSize = 16;
            ClassId = "ClassLogin";
            IsEnabled = true;
            HorizontalTextAlignment = TextAlignment.Center;
            VerticalOptions = LayoutOptions.FillAndExpand;
        }
    }
}