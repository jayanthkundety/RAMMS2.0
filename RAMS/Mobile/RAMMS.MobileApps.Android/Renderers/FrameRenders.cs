using System;
using System.ComponentModel;
using Android.Content;
using RAMMS.MobileApps;
using RAMMS.MobileApps.Droid.Render;
using RAMMS.MobileApps.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Android.Views;
using Android.Graphics.Drawables;

[assembly: ExportRenderer(typeof(RAMMS.MobileApps.Controls.FrameRenders), typeof(RAMMS.MobileApps.Droid.Render.FrameRenders))]
namespace RAMMS.MobileApps.Droid.Render
{
    
    public class FrameRenders: FrameRenderer
    {
        public  FrameRenders(Context context) : base(context)
        {
            
        }
        public override void Draw(Canvas canvas)
        {
            base.Draw(canvas);
            int padding = 0;
            if(!string.IsNullOrEmpty(((RAMMS.MobileApps.Controls.FrameRenders)Element).BackGroundFrameColor))
            {
                SetBackgroundColor(Android.Graphics.Color.White);
                // SetBackgroundColor(Android.Graphics.Color.ParseColor("#f8f8f8"));
            }
            if (!((RAMMS.MobileApps.Controls.FrameRenders)Element).TwoSideCorner)
            {
                if (((Controls.FrameRenders)Element).FrameCornerRadius > 0)
                {
                    padding = (int)((Controls.FrameRenders)Element).FrameCornerRadius;
                }
                else
                {
                   padding = (int)((Controls.FrameRenders)Element).HeightRequest / 2;

                }
            }
            else
            {
               padding = (int)Element.Height / 2;
            }
            // padding = (int)((school.Render.FrameRenders)Element).HeightRequest / 2;
            DrawOutline(canvas, canvas.Width, canvas.Height, padding);//set corner radius
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

                paint.StrokeWidth = (int)((Controls.FrameRenders)Element).FrameBordrWidth; ;  //set outline stroke
                paint.SetStyle(style);
                string colorName = ((Controls.FrameRenders)Element).ColorName;

                if (colorName!=null && colorName.Equals("White"))
                {
                    paint.Color = Android.Graphics.Color.ParseColor("#FFFFFF");
                }
                //else if (((Controls.FrameRenders)Element).ColorName.Equals("White"))
                //{
                //    paint.Color = Android.Graphics.Color.ParseColor("#FFFFFF");
                //}
                //else if (((Controls.FrameRenders)Element).ColorName.Equals("White"))
                //{
                //    paint.Color = Android.Graphics.Color.ParseColor("#FFFFFF");
                //}
                //else if (((Controls.FrameRenders)Element).ColorName.Equals("White"))
                //{
                //    paint.Color = Android.Graphics.Color.White;
                //}
                //else if (((Controls.FrameRenders)Element).ColorName.Equals("#FFFFFF"))
                //{
                //    paint.Color = Android.Graphics.Color.ParseColor("#FFFFFF");
                //}



                //else if (((school.Render.FrameRenders)Element).ColorName.Equals("LightGray"))
                //{
                //    Layer.BorderColor = UIColor.LightGray.CGColor;
                //}
                //else if (((school.Render.FrameRenders)Element).ColorName.Equals("Green"))
                //{
                //    Layer.BorderColor = AppThemeColor.ThemeColor.CGColor;
                //}
                /// paint.Color = Android.Graphics.Color.ParseColor("#6EB9BC");//set outline color //_frame.OutlineColor.ToAndroid(); 
                canvas.DrawPath(path, paint);
            }
        }
        //protected override bool DrawChild(Canvas canvas, global::Android.Views.View child, long drawingTime)
        //{
        //    if (Element == null) return false;
        //    school.Render.FrameRenders rcv = (school.Render.FrameRenders)Element;
           
        //    this.SetClipChildren(true);
        //   // rcv
        //   // rcv.Padding = new Thickness(12, 12, 12, 12);
        //    //rcv.HasShadow = false;      
        //    int radius = (int)(rcv.FrameCornerRadius);
        //    // Check if make circle is set to true. If so, then we just use the width and      
        //    // height of the control to calculate the radius. RoundedCornerRadius will be ignored      
        //    // in this case.      
        //    if (rcv.MakeCircle)
        //    {
        //        radius = Math.Min(Width, Height) / 2;
        //    }
        //    // When we create a round rect, we will have to double the radius since it is not      
        //    // the same as creating a circle.      
        //    radius *= 2;
        //    try
        //    {
        //        //Create path to clip the child       
        //        var path = new Path();
        //        path.AddRoundRect(new RectF(0, 0, Width, Height), new float[] {
        //            radius,
        //            radius,
        //            radius,
        //            radius,
        //            radius,
        //            radius,
        //            radius,
        //            radius
        //        }, Path.Direction.Ccw);
        //        canvas.Save();
        //        canvas.ClipPath(path);
        //        // Draw the child first so that the border shows up above it.      
        //        var result = base.DrawChild(canvas, child, drawingTime);
        //        canvas.Restore();
        //        /*   
        //         * If a border is specified, we use the same path created above to stroke   
        //         * with the border color.   
        //         * */
        //        if (rcv.BorderWidth > 0)
        //        {
        //            // Draw a filled circle.      
        //            var paint = new Paint();
        //            paint.AntiAlias = true;
        //            paint.StrokeWidth =(float) rcv.FrameBordrWidth;
        //            paint.SetStyle(Paint.Style.Stroke);
        //            if (rcv.ColorName.Equals("Black"))
        //            {
        //                paint.Color = Android.Graphics.Color.ParseColor("#000000");
        //            }
        //            else if (rcv.ColorName.Equals("White"))
        //            {
        //                paint.Color = Android.Graphics.Color.ParseColor("#FFFFFF");
        //            }
                   
        //            canvas.DrawPath(path, paint);
        //            paint.Dispose();
        //        }
        //        //Properly dispose      
        //        path.Dispose();
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Console.Write(ex.Message);
        //    }
        //    return base.DrawChild(canvas, child, drawingTime);
        //}
    }

}

