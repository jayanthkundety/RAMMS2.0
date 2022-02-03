using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace RAMMS.MobileApps.Controls
{
    public class CustomEntryNumeric : Entry
    {
        public string EntrySubType { get; set; }

        public string CustomId { get; set; }

        public CustomEntryNumeric()
        {
            ClassId = "ClassLogin";
            IsEnabled = true;
            HorizontalTextAlignment = TextAlignment.Center;
            VerticalOptions = LayoutOptions.FillAndExpand;
        }
    }
}
