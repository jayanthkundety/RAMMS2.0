using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RAMMS.MobileApps
{
    public class CustomEntryNumber : Entry
    {
        public string EntrySubType { get; set; }

        public string CustomId { get; set; }

        public CustomEntryNumber()
        {
            ClassId = "ClassLogin";
            IsEnabled = true;
            HorizontalTextAlignment = TextAlignment.Center;
            VerticalOptions = LayoutOptions.FillAndExpand;
        }
    }
}
