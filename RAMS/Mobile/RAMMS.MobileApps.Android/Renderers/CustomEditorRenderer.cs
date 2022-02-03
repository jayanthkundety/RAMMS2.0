using Android.Content;
using Android.Graphics.Drawables;
using RAMMS.MobileApps;
using RAMMS.MobileApps.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEditor), typeof(CustomEditorRenderer))]

namespace RAMMS.MobileApps.Droid
{
    public class CustomEditorRenderer : EditorRenderer
    {
        public CustomEditorRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(6f);
                gradientDrawable.SetShape(ShapeType.Rectangle);
                gradientDrawable.SetStroke(3, Android.Graphics.Color.Rgb(179, 179, 179));
                Control.SetBackground(gradientDrawable);
                //Control.SetPadding(0, Control.PaddingTop, Control.PaddingRight, Control.PaddingBottom);
                //Background = new Android.Graphics.Drawables.ColorDrawable(Android.Graphics.Color.White);
            }
        }
    }
}