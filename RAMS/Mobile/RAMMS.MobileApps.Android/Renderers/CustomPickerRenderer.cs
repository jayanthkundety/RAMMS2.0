using Android.App;
using Android.Content.Res;
using Android.Text;
using Android.Views;
using Android.Widget;
using RAMMS.MobileApps;
using RAMMS.MobileApps.Droid;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Object = Java.Lang.Object;

[assembly: ExportRenderer(typeof(ExtendedPicker), typeof(CustomPickerRenderer))]

namespace RAMMS.MobileApps.Droid
{
    [Android.Runtime.Preserve(AllMembers = true)]
    public class CustomPickerRenderer : ViewRenderer<Picker, EditText>
    {
        private AlertDialog _dialog;
        private bool _isDisposed;
        private bool _isOpened;
        private TextColorSwitcher _textColorSwitcher;

        public CustomPickerRenderer()
        {
            AutoPackage = false;
        }

        private IElementController ElementController => Element as IElementController;

        protected override void Dispose(bool disposing)
        {
            if (disposing && !_isDisposed)
            {
                _isDisposed = true;
                //((ObservableCollection<string>)Element.Items).CollectionChanged -= RowsCollectionChanged;
            }

            base.Dispose(disposing);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            //if (e.OldElement != null)
            //((ObservableCollection<string>)e.OldElement.Items).CollectionChanged -= RowsCollectionChanged;

            if (e.NewElement != null)
            {
                //((ObservableCollection<string>)e.NewElement.Items).CollectionChanged += RowsCollectionChanged;
                if (Control == null)
                {
                    var textField = new EditText(Context) { Focusable = false, Clickable = true, Tag = this };
                    textField.SetOnClickListener(PickerListener.Instance);
                    _textColorSwitcher = new TextColorSwitcher(textField.TextColors);
                    textField.Gravity = GravityFlags.Center;
                    textField.TextSize =16;
                    SetNativeControl(textField);
                }
                UpdatePicker();
                UpdateTextColor();
            }

            base.OnElementChanged(e);

            if (Control != null)
            {
                //Control.SetBackgroundColor(Color.FromHex("#EBEDEC").ToAndroid());
                Control.SetTextColor(global::Android.Graphics.Color.Black);
                Control.SetPadding(10, 0, 10, 0);

                Control.LayoutChange += (s, args) =>
                {
                    Control.Ellipsize = TextUtils.TruncateAt.End;
                    Control.SetMaxLines(1);
                    
                    Control.SetLineSpacing (1, (float)0.62);

                    //Control.SetLineSpacing(0.25f, (float)0.6);

                };

                var a = this.ViewGroup;
                //Control.SetHeight(100);

                var view = e.NewElement as ExtendedPicker;
                if (view != null)
                {
                    if (view.ClassId == "ClassPickGrey")
                    {
                       Control.SetBackgroundResource(Resource.Drawable.SpinnerWithWhite);
                        // Control.SetBackgroundColor(Android.Graphics.Color.White);
                    }
                    else if (view.ClassId == "ClassPickTime")
                    {
                        Control.SetBackgroundResource(Resource.Drawable.WithBorderIcon);
                    }
                    else if (view.ClassId == "ClassPickWhite")
                    {
                        Control.SetBackgroundResource(Resource.Drawable.SpinnerWithWhite);
                    }
                    else if (view.ClassId == "ClassPickGreyJ")
                    {
                        Control.SetBackgroundResource(Resource.Drawable.CustomPicker);
                    }
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == Picker.TitleProperty.PropertyName)
                UpdatePicker();
            if (e.PropertyName == Picker.SelectedIndexProperty.PropertyName)
                UpdatePicker();
            if (e.PropertyName == Picker.TextColorProperty.PropertyName)
                UpdateTextColor();
        }

        protected override void OnFocusChanged(bool gainFocus, FocusSearchDirection direction, Android.Graphics.Rect previouslyFocusedRect)
        {
            base.OnFocusChanged(gainFocus, direction, previouslyFocusedRect);

            if (gainFocus)
                OnClick();
            else if (_dialog != null)
            {
                _dialog.Hide();
                ElementController.SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
                Control.ClearFocus();
                _dialog = null;
            }
        }

        private void OnClick()
        {
            if (_isOpened)
                return;

            _isOpened = true;

            Picker model = Element;

            var pickerAdapter = new ArrayAdapter<string>(Context, Android.Resource.Layout.SelectDialogSingleChoice);
            foreach (var item in model.Items)
            {
                pickerAdapter.Add(item);
            }

            ElementController.SetValueFromRenderer(VisualElement.IsFocusedProperty, true);

            var builder = new AlertDialog.Builder(Context);

            builder.SetAdapter(pickerAdapter, (sender, e) =>
            {
                ElementController.SetValueFromRenderer(Picker.SelectedIndexProperty, e.Which);
                // It is possible for the Content of the Page to be changed on SelectedIndexChanged.
                // In this case, the Element & Control will no longer exist.
                if (Element != null)
                {
                    if (model.Items.Count > 0 && Element.SelectedIndex >= 0)
                        Control.Text = model.Items[Element.SelectedIndex];
                    ElementController.SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
                    // It is also possible for the Content of the Page to be changed when Focus is changed.
                    // In this case, we'll lose our Control.
                    Control?.ClearFocus();
                }

                _isOpened = false;
            });

            //builder.SetView(layout);
            builder.SetTitle(model.Title ?? "");
            builder.SetNegativeButton(global::Android.Resource.String.Cancel, (s, a) =>
            {
                ElementController.SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
                // It is possible for the Content of the Page to be changed when Focus is changed.
                // In this case, we'll lose our Control.
                Control?.ClearFocus();
                _dialog = null;
                _isOpened = false;
            });

            _dialog = builder.Create();
            _dialog.DismissEvent += (sender, args) =>
            {
                ElementController.SetValueFromRenderer(VisualElement.IsFocusedProperty, false);
                _isOpened = false;
            };

            _dialog.Show();
        }

        private void RowsCollectionChanged(object sender, EventArgs e)
        {
            UpdatePicker();
        }

        private void UpdatePicker()
        {
            Control.Hint = Element.Title;
            Control.Gravity = GravityFlags.CenterVertical | GravityFlags.FillHorizontal;
            string oldText = Control.Text;

            if (Element.SelectedIndex == -1 || Element.Items == null)
                Control.Text = null;
            else
                Control.Text = Element.Items[Element.SelectedIndex];

            if (oldText != Control.Text)
                ((IVisualElementController)Element).NativeSizeChanged();
        }

        private void UpdateTextColor()
        {
            _textColorSwitcher?.UpdateTextColor(Control, Element.TextColor);
        }

        private class PickerListener : Object, IOnClickListener
        {
            public static readonly PickerListener Instance = new PickerListener();

            public void OnClick(global::Android.Views.View v)
            {
                var renderer = v.Tag as CustomPickerRenderer;
                if (renderer == null)
                    return;

                renderer.OnClick();
            }
        }

        internal class TextColorSwitcher
        {
            private static readonly int[][] s_colorStates = { new[] { global::Android.Resource.Attribute.StateEnabled }, new[] { -global::Android.Resource.Attribute.StateEnabled } };

            private readonly ColorStateList _defaultTextColors;
            private Color _currentTextColor;

            public TextColorSwitcher(ColorStateList textColors)
            {
                _defaultTextColors = textColors;
            }

            public void UpdateTextColor(TextView control, Color color)
            {
                if (color == _currentTextColor)
                    return;

                _currentTextColor = color;

                // Set the new enabled state color, preserving the default disabled state color
                int defaultDisabledColor = _defaultTextColors.GetColorForState(s_colorStates[1], color.ToAndroid());
                control.SetTextColor(new ColorStateList(s_colorStates, new[] { color.ToAndroid().ToArgb(), defaultDisabledColor }));
            }
        }
    }
}