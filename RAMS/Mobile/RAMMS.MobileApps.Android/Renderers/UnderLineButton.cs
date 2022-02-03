
using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using Android.Util;

using school;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using RAMMS.MobileApps.Droid.Renderers;

[assembly: ExportRenderer(typeof(customEntryEffect), typeof(UnderLineButton))]
namespace RAMMS.MobileApps.Droid.Renderers
{
    public class UnderLineButton: EntryRenderer
    {
        public UnderLineButton(Context context) : base(context)
        {
           
        }
        GradientDrawable _gradientBackground;
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || e.NewElement == null)
                return;
            Control.Background = null;
            Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
            Control.SetForegroundGravity(Android.Views.GravityFlags.Center);
           
        }
        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            var padding = (int)((customEntryEffect)Element).HeightRequest;
            if (((customEntryEffect)Element).HasNoPadding)
            {
                Control.SetPadding(padding - 2, 0, padding - 2, 0);
            }
            else
            {
                Control.SetPadding(padding - 5, 0, padding - 5, 0);
                DrawOutline(canvas, canvas.Width, canvas.Height, padding/2);
            }
           // var padding = (int)((customEntryEffect)Element).HeightRequest/2;
           //set corner radius
        }
        void DrawOutline(Canvas canvas, int width, int height, float cornerRadius)
        {
            using (var paint = new Paint { AntiAlias = true })
            using (var path = new Path())
            using (Path.Direction direction = Path.Direction.Cw)
            using (Paint.Style style = Paint.Style.Stroke)
            using (var rect = new RectF(0, 0, width, height))
            {
                float rx = Context.ToPixels(cornerRadius);
                float ry = Context.ToPixels(cornerRadius);
                path.AddRoundRect(rect, rx, ry, direction);

                paint.StrokeWidth = (int)((customEntryEffect)Element).FrameBordrWidth; ;  //set outline stroke
                paint.SetStyle(style);
                paint.Color =Android.Graphics.Color.ParseColor("#6EB9BC");//set outline color //_frame.OutlineColor.ToAndroid(); 
                canvas.DrawPath(path, paint);
            }
        }

        private void NewElement_SizeChanged(object sender, System.EventArgs e)
        {
            //if(_gradientBackground != null)
            //{
            //    var padding = (int)((customEntryEffect)Element).HeightRequest;
            //    // Radius for the curves  
            //    _gradientBackground.SetCornerRadius(padding/2);
            //    Control.SetPadding(padding-5, 0, padding-5, 0);
            //}
          

        }

        
    }
}
