using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using RAMMS.MobileApps;
using RAMMS.MobileApps.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryControl), typeof(EntryControlRenderer))]

namespace RAMMS.MobileApps.Droid
{
    public class EntryControlRenderer : EntryRenderer
    {
        public EntryControlRenderer(Context context) : base(context)
        {
        }

        private const int DRAWABLE_RIGHT = 2;
        private Drawable _drawable;

        public EntryControlRenderer()
        {
            var resourceId = Context.Resources.GetIdentifier("cancel", "drawable", Context.PackageName);
            _drawable = Resources.GetDrawable(resourceId);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var view = e.NewElement as Entry;
                if (view != null)
                    //view.HeightRequest = 40d;

                    //Control.SetBackgroundResource(LavanyaTestApp.Droid.Resource.Drawable.WithBorder);
                    //Control.SetTextColor(global::Android.Graphics.Color.Black);
                    //  Control.SetHintTextColor(global::Android.Graphics.Color.Gray);
                    //  Control.SetBackgroundColor(global::Android.Graphics.Color.White);
                    Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                Control.SetRawInputType(Android.Text.InputTypes.TextFlagCapSentences | Android.Text.InputTypes.ClassText);
                Control.SetPadding(10, 0, 10, 0);

                IntPtr IntPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                IntPtr mCursorDrawableResProperty = JNIEnv.GetFieldID(IntPtrtextViewClass, "mCursorDrawableRes", "I");
                JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, 0);

                if (e.NewElement.Keyboard == Keyboard.Numeric || e.NewElement.Keyboard == Keyboard.Telephone)
                {
                    if (e.NewElement.IsPassword)
                    {
                        Control.SetRawInputType(Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagSigned | Android.Text.InputTypes.NumberVariationPassword);
                    }
                    else
                    {
                        Control.SetRawInputType(Android.Text.InputTypes.ClassNumber | Android.Text.InputTypes.NumberFlagSigned);
                    }
                }
                if (e.NewElement.IsPassword)
                {
                    Control.TransformationMethod = new Android.Text.Method.PasswordTransformationMethod();
                    Control.SetRawInputType(Android.Text.InputTypes.TextVariationPassword);
                }
                if (view != null)
                {
                    if (view.ClassId == "ClassLogin")
                    {
                        Control.Gravity = GravityFlags.CenterVertical;
                        SetRightDrawable();
                    }
                    else if (view.ClassId == "ClassDetails")
                    {
                        Control.Gravity = GravityFlags.CenterVertical;
                        SetRightDrawable();
                    }
                }
            }
        }

        private void SetRightDrawable()
        {
            //Control.SetCompoundDrawablesWithIntrinsicBounds(null, null, _drawable.Mutate(), null);
            Control.SetCompoundDrawablesWithIntrinsicBounds(null, null, null, null);
            try
            {
                Control.SetPadding(Control.TotalPaddingLeft, Control.TotalPaddingTop, Control.PaddingRight + 12, Control.TotalPaddingBottom);
            }
            catch
            {
                Control.SetPadding(Control.TotalPaddingLeft, 0, Control.PaddingRight + 12, 0);
            }

            //Control.Touch += (s, arg) =>
            //{
            //    arg.Handled = false;

            //    if (arg.Event.GetX() >= (Control.Right - Control.GetCompoundDrawables()[DRAWABLE_RIGHT].Bounds.Width()))
            //    {
            //        if (arg.Event.Action == MotionEventActions.Up)
            //        {
            //            arg.Handled = true;
            //            ((IElementController)Element).SetValueFromRenderer(Entry.TextProperty, string.Empty);
            //            //Element.Focus();
            //        }
            //    }
            //};
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Element.ClassId == "ClassLogin" || Element.ClassId == "ClassDetails")
            {
                var drawable = Control.GetCompoundDrawables()[DRAWABLE_RIGHT];

                if (drawable != null)
                {
                    if (Element.IsEnabled && !string.IsNullOrEmpty(Element.Text))
                    {
                        drawable.SetAlpha(255);
                    }
                    else
                    {
                        drawable.SetAlpha(0);
                    }
                }
            }
        }


    }
}