using Android.Graphics.Drawables;
using Android.Views;
using RAMMS.MobileApps;
using RAMMS.MobileApps.Droid;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]

namespace RAMMS.MobileApps.Droid
{
    [Android.Runtime.Preserve(AllMembers = true)]
    public class CustomDatePickerRenderer : DatePickerRenderer
    {
        private const int DRAWABLE_RIGHT = 2;
        private Drawable _drawable;

        private IElementController ElementController => Element as IElementController;

        public CustomDatePickerRenderer()
        {
            var resourceId = Context.Resources.GetIdentifier("dateofbirth", "drawable", Context.PackageName);
            _drawable = Resources.GetDrawable(resourceId);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var view = e.NewElement as DatePicker;
                if (view != null)
                {
                    if (view.ClassId != "ClassDatePickWhite")
                    {
                        SetRightDrawable();
                    }
                    if (view.ClassId == "ClassDateGray")
                    {
                        Control.SetBackgroundResource(Resource.Drawable.WithBorder);
                    }
                    else if (view.ClassId == "ClassDateWhite")
                    {
                        Control.SetBackgroundResource(Resource.Drawable.WithWhiteBorder);
                    }
                    else if (view.ClassId == "ClassDatePickWhite")
                        Control.SetBackgroundResource(Resource.Drawable.SpinnerWithWhite);
                }

                //  Control.SetBackgroundColor(Color.FromHex("#EBEDEC").ToAndroid());
                Control.SetPadding(20, 0, 20, 0);

                Control.LongClickable = false;
            }

            if (e.NewElement != null)
            {
                e.NewElement.Unfocused += NewElement_Unfocused;

                var customDatePicker = e.NewElement as CustomDatePicker;

                if (customDatePicker != null)
                {
                    this.SetValue(customDatePicker);
                }
            }
            if (e.OldElement != null)
            {
                e.NewElement.Unfocused -= NewElement_Unfocused;
            }
        }

        private void NewElement_Unfocused(object sender, FocusEventArgs e)
        {
            var picker = this.Element as CustomDatePicker;
            if (picker != null)
            {
                if (!e.IsFocused && !picker.NullableDate.HasValue)
                {
                    if (picker.MinimumDate == Convert.ToDateTime("1/1/1900"))
                        ElementController?.SetValueFromRenderer(CustomDatePicker.NullableDateProperty, DateTime.Now.Date);
                    else
                        ElementController?.SetValueFromRenderer(CustomDatePicker.NullableDateProperty, picker.MinimumDate);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Element.ClassId == "ClassDetails")
            {
                var drawable = Control.GetCompoundDrawables()[DRAWABLE_RIGHT];

                if (drawable != null)
                {
                    if (Element.IsEnabled)
                    {
                        drawable.SetAlpha(255);
                    }
                    else
                    {
                        drawable.SetAlpha(0);
                    }
                }
            }

            if (e.PropertyName == CustomDatePicker.NullableDateProperty.PropertyName
                || e.PropertyName == CustomDatePicker.NullTextProperty.PropertyName)
            {
                var customDatePicker = this.Element as CustomDatePicker;
                if (customDatePicker != null)
                    this.SetValue(customDatePicker);
            }
        }

        private void SetValue(CustomDatePicker customDatePicker)
        {
            if (Control == null)
                return;

            if (customDatePicker.NullableDate.HasValue)
            {
                Control.Text = customDatePicker.NullableDate.Value.ToString(customDatePicker.Format);
            }
            else
            {
                Control.Text = customDatePicker.NullText ?? string.Empty;
            }
        }

        private void SetRightDrawable()
        {
            Control.SetCompoundDrawablesWithIntrinsicBounds(null, null, _drawable.Mutate(), null);
            try
            {
                Control.SetPadding(Control.TotalPaddingLeft, Control.TotalPaddingTop, Control.PaddingRight + 12, Control.TotalPaddingBottom);
            }
            catch
            {
                Control.SetPadding(Control.TotalPaddingLeft, 0, Control.PaddingRight + 12, 0);
            }
            Control.Touch += (s, arg) =>
            {
                arg.Handled = false;

                if (arg.Event.GetX() >= (Control.Right - Control.GetCompoundDrawables()[DRAWABLE_RIGHT].Bounds.Width()))
                {
                    if (arg.Event.Action == MotionEventActions.Up)
                    {
                        arg.Handled = true;
                        ((IElementController)Element).SetValueFromRenderer(CustomDatePicker.NullableDateProperty, null);

                        var customDatePicker = this.Element as CustomDatePicker;
                        if (customDatePicker != null)
                            Control.Text = customDatePicker.NullText;
                    }
                }
            };
        }
    }
}