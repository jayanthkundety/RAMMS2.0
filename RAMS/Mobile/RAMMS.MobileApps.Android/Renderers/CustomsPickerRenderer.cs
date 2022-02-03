using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RAMMS.MobileApps.Controls;
using RAMMS.MobileApps.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMyPicker), typeof(CustomsPickerRenderer))]
namespace RAMMS.MobileApps.Droid.Renderers
{
    public class CustomsPickerRenderer : PickerRenderer
    {
        CustomMyPicker element;

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            element = (CustomMyPicker)this.Element;

            if (Control != null)
            {
                Control.Background = AddBorderToPicker();
            }

        }

        public LayerDrawable AddBorderToPicker()
        {
            ShapeDrawable pickerBorder = new ShapeDrawable();
            pickerBorder.Paint.Color = Android.Graphics.Color.Transparent;
            pickerBorder.SetPadding(10, 10, 10, 10);
            pickerBorder.Paint.SetStyle(Paint.Style.Stroke);
            pickerBorder.Paint.StrokeWidth = 1;

            Drawable[] layers = { pickerBorder };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

            return layerDrawable;
        }
    }
}