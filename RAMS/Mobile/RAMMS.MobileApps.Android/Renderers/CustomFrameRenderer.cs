using Android.Graphics;
using RAMMS.MobileApps;
using RAMMS.MobileApps.Droid;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CornerFrame), typeof(CustomFrameRenderer))]

namespace RAMMS.MobileApps.Droid
{
    public class CustomFrameRenderer : FrameRenderer, IDisposable
    {
        public Xamarin.Forms.Color outlineColor;

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
                outlineColor = e.NewElement.OutlineColor;
        }

        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);

            DrawOutline(canvas, canvas.Width, canvas.Height, 10f, outlineColor.ToAndroid());//set corner radius
        }

        private void DrawOutline(Canvas canvas, int width, int height, float cornerRadius, Android.Graphics.Color outlineColor)
        {
            using (var paint = new Paint { AntiAlias = true })
            using (var path = new Path())
            using (Path.Direction direction = Path.Direction.Cw)
            using (Paint.Style style = Paint.Style.Stroke)
            using (var rect = new RectF(0, 0, width, height))
            {
                float rx = Forms.Context.ToPixels(cornerRadius);
                float ry = Forms.Context.ToPixels(cornerRadius);
                path.AddRoundRect(rect, rx, ry, direction);

                paint.StrokeWidth = 2f;  //set outline stroke
                paint.SetStyle(style);
                paint.Color = outlineColor;
                //paint.Color = Android.Graphics.Color.ParseColor("#7B9639");//set outline color //_frame.OutlineColor.ToAndroid();
                canvas.DrawPath(path, paint);
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}