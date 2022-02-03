using System;
using Xamarin.Forms;

namespace RAMMS.MobileApps
{
    public class CustomDatePicker : DatePicker
    {
        public CustomDatePicker()
        {
            FontSize = 16;
            this.HeightRequest = 40d;
            this.DateSelected += CustomDatePicker_DateSelected;
            //this.Format = "dd-MM-yyyy";
            this.BackgroundColor = Color.FromHex("#f8f8f8");
        }

        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create(nameof(NullableDate), typeof(DateTime?), typeof(CustomDatePicker), default(DateTime?), BindingMode.TwoWay, null, new BindableProperty.BindingPropertyChangedDelegate(NullableDateChanged));

        public DateTime? NullableDate
        {
            get
            {
                return (DateTime?)this.GetValue(NullableDateProperty);
            }
            set
            {
                this.SetValue(NullableDateProperty, value);
            }
        }

        public static readonly BindableProperty NullTextProperty = BindableProperty.Create(nameof(NullText), typeof(string), typeof(CustomDatePicker), "Select Date", BindingMode.TwoWay);

        public string NullText
        {
            get
            {
                return (string)this.GetValue(NullTextProperty);
            }
            set
            {
                TextColor = Color.FromHex("#cccccc");
                this.SetValue(NullTextProperty, value);
            }
        }

        private void CustomDatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            NullableDate = e.NewDate.Date;
        }

        private static void NullableDateChanged(BindableObject obj, object oldValue, object newValue)
        {
            var customDatePicker = obj as CustomDatePicker;
            var newDate = (DateTime?)newValue;

            if (customDatePicker != null)
            {
                if (newDate.HasValue)
                {
                    customDatePicker.NullableDate = newDate.Value;
                    customDatePicker.TextColor = Color.Black;
                }
            }
        }
    }
}