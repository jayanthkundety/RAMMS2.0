using System;
using Xamarin.Forms;

namespace RAMMS.MobileApps
{
    public class EditorPlaceholder : Editor
    {
        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create<EditorPlaceholder, string>(view => view.Placeholder, String.Empty);

        public EditorPlaceholder() : base()
        {
        }

        public string Placeholder
        {
            get
            {
                return (string)GetValue(PlaceholderProperty);
            }

            set
            {
                SetValue(PlaceholderProperty, value);
            }
        }
    }
}