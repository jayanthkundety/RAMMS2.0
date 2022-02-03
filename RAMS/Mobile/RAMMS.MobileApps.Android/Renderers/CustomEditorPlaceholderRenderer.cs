using RAMMS.MobileApps;
using RAMMS.MobileApps.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EditorPlaceholder), typeof(CustomEditorPlaceholderRenderer))]

namespace RAMMS.MobileApps.Droid
{
    public class CustomEditorPlaceholderRenderer : EditorRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                Control.SetBackgroundColor(Android.Graphics.Color.White);
                var element = e.NewElement as EditorPlaceholder;
                this.Control.Hint = element.Placeholder;
            }
        }
    }
}