using System;
using Android.Content;
using RAMMS.MobileApps.Controls;
using RAMMS.MobileApps.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedEntry), typeof(ExtendedEntryRenderer))]
namespace RAMMS.MobileApps.Droid.Renderers
{
    public class ExtendedEntryRenderer : EntryRenderer
    {
        private const int DefaultPadding = 0;
        public ExtendedEntryRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {

            base.OnElementChanged(e);
            var element = e.NewElement as ExtendedEntry;

            if (e.OldElement == null)
            {
                Control.Background = null;
                Control.SetPadding(DefaultPadding, DefaultPadding, DefaultPadding, DefaultPadding);
                //Control.Gravity = element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Center ?
                // Android.Views.GravityFlags.CenterHorizontal : (Android.Views.GravityFlags)element.TextAlignment;
              


            }
            
        }
    }
}
