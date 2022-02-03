using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps.Components
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomPicker : ContentView
    {
        public event EventHandler<TextChangedEventArgs> TextChanged;
        public event EventHandler PickerTapped;

        public CustomPicker()
        {
            InitializeComponent();
            this.Content.BindingContext = this;
            txtEntry.BindingContext = this;
            txtEntry.SetBinding(Entry.TextProperty,
                                TextProperty.PropertyName,
                                BindingMode.TwoWay);
        }

        #region Binding Properties

        public static readonly BindableProperty AlertTextProperty =
          BindableProperty.Create(nameof(AlertText),
                                  typeof(string),
                                  typeof(CustomPicker),
                                  default(string),
                                  defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty AlertStyleProperty =
          BindableProperty.Create(nameof(AlertStyle),
                                  typeof(Style),
                                  typeof(CustomPicker),
                                  default(Style),
                                  defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty BoxColorProperty =
        BindableProperty.Create(nameof(BoxColor),
                                typeof(Color),
                                typeof(CustomPicker),
                                default(Color),
                               defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty PlaceHolderProperty =
         BindableProperty.Create(nameof(PlaceHolder),
                                 typeof(string),
                                 typeof(CustomPicker),
                                 defaultValue: string.Empty,
                                 defaultBindingMode: BindingMode.TwoWay,
                                 propertyChanged: PlaceHolderPropertyChanged);

        public static readonly BindableProperty CaptionStyleProperty =
            BindableProperty.Create(nameof(CaptionStyle),
                                    typeof(Style),
                                    typeof(CustomPicker),
                                    defaultValue: default(Style),
                                    defaultBindingMode: BindingMode.TwoWay,
                                    propertyChanged: CaptionStylePropertyChanged);

        public static readonly BindableProperty TextStyleProperty =
            BindableProperty.Create(nameof(TextStyle),
                                    typeof(Style),
                                    typeof(CustomPicker),
                                    defaultValue: default(Style),
                                    defaultBindingMode: BindingMode.TwoWay,
                                    propertyChanged: TextStylePropertyChanged);

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text),
                                    typeof(string),
                                    typeof(CustomPicker),
                                    string.Empty,
                                    BindingMode.TwoWay);

        public static readonly BindableProperty DefaultTextStyleProperty =
          BindableProperty.Create(nameof(DefaultTextStyle),
                                  typeof(Style),
                                  typeof(CustomPicker),
                                  defaultValue: default(Style),
                                  defaultBindingMode: BindingMode.TwoWay,
                                  propertyChanged: TextStylePropertyChanged);


        public static BindableProperty TextHorizontalAlignmentProperty =
          BindableProperty.Create(nameof(TextHorizontalAlignment),
                                  typeof(TextAlignment),
                                  typeof(CustomPicker),
                                  TextAlignment.Start,
                                  defaultBindingMode: BindingMode.TwoWay);
        #endregion

        #region Events

        private void EntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!ReferenceEquals(TextChanged, null))
                TextChanged(this, e);
            box.BackgroundColor = (string.IsNullOrEmpty(txtEntry.Text)) ?
                                     (Color)Application.Current.Resources["BlackColor"] : BoxColor;
            lblCaption.IsVisible = (!string.IsNullOrEmpty(txtEntry.Text));
            txtEntry.Style = (string.IsNullOrEmpty(txtEntry.Text)) ?
                                     DefaultTextStyle : TextStyle;
            AlertStyle = default(Style);
            AlertText = default(string);
            dropDownImage.Source = "down.png";

        }

        private static void PlaceHolderPropertyChanged(BindableObject bindable,
                                                object oldValue,
                                                object newValue)
        {
            var control = (CustomPicker)bindable;
            control.txtEntry.Placeholder = newValue.ToString();
            control.lblCaption.Text = newValue.ToString();
        }

        private static void CaptionStylePropertyChanged(BindableObject bindable,
                                                         object oldValue,
                                                         object newValue)
        {
            if (string.IsNullOrEmpty(newValue.ToString())) return;
            var control = (CustomPicker)bindable;
            control.lblCaption.Style = newValue as Style;
        }

        private static void TextStylePropertyChanged(BindableObject bindable,
                                                     object oldValue,
                                                     object newValue)
        {
            if (string.IsNullOrEmpty(newValue.ToString())) return;
            var control = (CustomPicker)bindable;
            control.txtEntry.Style = newValue as Style;
        }

        private void LayoutTapped(object sender, EventArgs args)
        {
            PickerTapped?.Invoke(sender, args);
        }
        #endregion

        #region Public Properties
        public Style CaptionStyle
        {
            get => (Style)GetValue(CaptionStyleProperty);
            set => SetValue(CaptionStyleProperty, value);
        }

        public Style TextStyle
        {
            get => (Style)GetValue(TextStyleProperty);
            set => SetValue(TextStyleProperty, value);
        }

        public string PlaceHolder
        {
            get => GetValue(PlaceHolderProperty).ToString();
            set => SetValue(PlaceHolderProperty, value);
        }


        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public Color BoxAlertColor
        {
            set => box.BackgroundColor = value;

        }
        public Color BoxColor
        {

            get => (Color)GetValue(BoxColorProperty);
            set => SetValue(BoxColorProperty, value);
        }

        public double BoxHeight
        {
            set => box.HeightRequest = value;
        }
        public ImageSource DropDownImage
        {
            set => dropDownImage.Source = value;
        }

        public Entry Entry { get => txtEntry; }

        public string AlertText
        {
            get => (string)GetValue(AlertTextProperty);
            set => SetValue(AlertTextProperty, value);
        }
        public Style AlertStyle
        {
            get => (Style)GetValue(AlertStyleProperty);
            set => SetValue(AlertStyleProperty, value);
        }
        public Style DefaultTextStyle
        {
            get => (Style)GetValue(DefaultTextStyleProperty);
            set => SetValue(DefaultTextStyleProperty, value);
        }

        public TextAlignment TextHorizontalAlignment
        {
            get => (TextAlignment)GetValue(TextHorizontalAlignmentProperty);
            set => SetValue(TextHorizontalAlignmentProperty, value);
        }


        #endregion
    }
}
