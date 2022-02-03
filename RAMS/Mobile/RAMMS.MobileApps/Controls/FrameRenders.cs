using System;
using Xamarin.Forms;
namespace RAMMS.MobileApps.Controls
{
    public class FrameRenders:Frame
    {
        public static readonly BindableProperty HasPaddingCornerProperty =
           BindableProperty.Create(nameof(HasPaddingCornerProperty), typeof(bool),
               typeof(FrameRenders));

        public bool HasPadding
        {
            get => (bool)GetValue(HasPaddingCornerProperty);
            set => SetValue(HasPaddingCornerProperty, value);
        }
        public static readonly BindableProperty TwoSideCornerProperty =
           BindableProperty.Create(nameof(TwoSideCornerProperty), typeof(bool),
               typeof(FrameRenders));

        public bool TwoSideCorner
        {
            get => (bool)GetValue(TwoSideCornerProperty);
            set => SetValue(TwoSideCornerProperty, value);
        }
        public static readonly BindableProperty BorderWidthProperty =
            BindableProperty.Create(nameof(FrameBordrWidth), typeof(double),
                typeof(FrameRenders));

        public double FrameBordrWidth
        {
            get => (double)GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        public static readonly BindableProperty BordeCornerRadiusProperty =
            BindableProperty.Create(nameof(FrameCornerRadius), typeof(double),
                typeof(FrameRenders));

        public double FrameCornerRadius
        {
            get => (double)GetValue(BordeCornerRadiusProperty);
            set => SetValue(BordeCornerRadiusProperty, value);
        }


        public static readonly BindableProperty BackGroundFrameColorPropertys =
            BindableProperty.Create(nameof(BackGroundFrameColor), typeof(string),
                typeof(FrameRenders));

        public string BackGroundFrameColor
        {
            get => (string)GetValue(BackGroundFrameColorPropertys);
            set => SetValue(BackGroundFrameColorPropertys, value);
        }

        public static readonly BindableProperty BorderColorPropertys =
            BindableProperty.Create(nameof(FrameBoderColor), typeof(Color),
                typeof(FrameRenders));

        public Color FrameBoderColor
        {
            get => (Color)GetValue(BorderColorPropertys);
            set => SetValue(BorderColorProperty, value);
        }
        public static readonly BindableProperty ColorNamePropertys =
          BindableProperty.Create(nameof(ColorName), typeof(string),
              typeof(FrameRenders));

        public string ColorName
        {
            get => (string)GetValue(ColorNamePropertys);
            set => SetValue(ColorNamePropertys, value);
        }
        public static readonly BindableProperty FillColorProperty = BindableProperty.Create<FrameRenders, Color>(w => w.FillColor, Color.White);
        public Color FillColor
        {
            get
            {
                return (Color)GetValue(FillColorProperty);
            }
            set
            {
                SetValue(FillColorProperty, value);
            }
        }
        public static readonly BindableProperty RoundedCornerRadiusProperty = BindableProperty.Create<FrameRenders, double>(w => w.RoundedCornerRadius, 3);
        public double RoundedCornerRadius
        {
            get
            {
                return (double)GetValue(RoundedCornerRadiusProperty);
            }
            set
            {
                SetValue(RoundedCornerRadiusProperty, value);
            }
        }
        public static readonly BindableProperty MakeCircleProperty = BindableProperty.Create<FrameRenders, Boolean>(w => w.MakeCircle, false);
        public Boolean MakeCircle
        {
            get
            {
                return (Boolean)GetValue(MakeCircleProperty);
            }
            set
            {
                SetValue(MakeCircleProperty, value);
            }
        }
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create<FrameRenders, Color>(w => w.BorderColor, Color.White);
        public Color BorderColor
        {
            get
            {
                return (Color)GetValue(BorderColorProperty);
            }
            set
            {
                SetValue(BorderColorProperty, value);
            }
        }
        public static readonly BindableProperty BorderWidthPropertys = BindableProperty.Create<FrameRenders, int>(w => w.BorderWidth, 1);
        public int BorderWidth
        {
            get
            {
                return (int)GetValue(BorderWidthPropertys);
            }
            set
            {
                SetValue(BorderWidthPropertys, value);
            }
        }

    }
}
