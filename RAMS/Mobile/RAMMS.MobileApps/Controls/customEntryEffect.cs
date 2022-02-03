using System;
using Xamarin.Forms;

namespace school
{
    public class customEntryEffect: Entry
    {

        public static readonly BindableProperty HasNoPaddingProperty =
           BindableProperty.Create(nameof(HasNoPadding), typeof(bool),
               typeof(customEntryEffect));

        public bool HasNoPadding
        {
            get => (bool)GetValue(HasNoPaddingProperty);
            set => SetValue(HasNoPaddingProperty, value);
        }
        public static readonly BindableProperty CustomHeightProperty =
          BindableProperty.Create(nameof(CustomHeight), typeof(bool),
              typeof(customEntryEffect));

        public double CustomHeight
        {
            get => (double)GetValue(CustomHeightProperty);
            set => SetValue(CustomHeightProperty, value);
        }
        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(FrameBordrWidth), typeof(double),
                typeof(customEntryEffect));

        public double FrameBordrWidth
        {
            get => (double)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }
    }
}
