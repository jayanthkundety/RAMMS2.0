using System;
using RAMMS.MobileApps.Controls;
using RAMMS.MobileApps.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Content;
using System.ComponentModel;
using Java.Interop;
using Android.Views;
using Android.Database;

[assembly: ExportRenderer(typeof(CustomListView), typeof(CustomListViewRenderer))]
namespace RAMMS.MobileApps.Droid.Renderers
{

    
    public class CustomListViewRenderer: ListViewRenderer
    {
        public CustomListViewRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.VerticalScrollBarEnabled = false;
                Control.HorizontalScrollBarEnabled = false;
                Control.ScrollbarFadingEnabled = false;
                Control.FastScrollAlwaysVisible = false;
                Control.SetScrollContainer(false);
                Control.Enabled = false;
                Control.NestedScrollingEnabled = false;
                
              //  Control.Adapter = new Adpaters()
                
                //Control.scrollbar
            }
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Control != null)
            {
                Control.VerticalScrollBarEnabled = false;
                Control.HorizontalScrollBarEnabled = false;
            }
        }
    }
}

