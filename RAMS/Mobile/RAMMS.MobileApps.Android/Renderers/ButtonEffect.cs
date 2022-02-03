using System;
using Xamarin.Forms;

using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Xamarin.Android;
using Android.Graphics.Drawables;
using System.Drawing;
using RAMMS.MobileApps.Controls;

[assembly: ResolutionGroupName("RAMMS.MobileApps")]
[assembly: ExportEffect(typeof(RAMMS.MobileApps.Droid.Renderers.ButtonEffect), nameof(ButtonEffect))]

namespace RAMMS.MobileApps.Droid.Renderers
{
    public class ButtonEffect: PlatformEffect
    {
       
        protected override void OnAttached()
        {
            if (Control is Android.Widget.Button)
            {
                ////Android.Text dfdf = new Android.Text()
                var but = (Android.Widget.Button)this.Control;
              //  but.MaxWidth = 0; 
               // Paint paint = new Paint();
               // paint.Color = Android.Graphics.Color.Black; // The underline color
               // paint.Flags = PaintFlags.UnderlineText;
               // but.PaintFlags = (but.PaintFlags | PaintFlags.UnderlineText);
                but.SetPadding(0, 0, 0, 0);
                //Paint paint = new Paint();
                //paint.Color = Android.Graphics.Color.Black; // The underline color
                //paint.Flags = PaintFlags.UnderlineText;
                //but.PaintFlags = paint.Flags;
                //// but.Paint
                //string htmlString = "<u>This text will be underlined</u>";
                // but.SetLayerPaint( paint);
                // but.setpain() | Paint.UNDERLINE_TEXT_FLAG
                //but.sethtm = htmlString;
            }
        }

        protected override void OnDetached()
        {
            
        }
    }
}
