using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps
{
    public class TextChangedEventToCommandEntryControl : Entry
    {
        public static readonly BindableProperty TextChangedCommandProperty = BindableProperty.Create(nameof(TextChangedCommand), typeof(ICommand), typeof(TextChangedEventToCommandEntryControl));
        public ICommand TextChangedCommand
        {
            get => (ICommand)GetValue(TextChangedCommandProperty);
            set => SetValue(TextChangedCommandProperty, value);
        }
        public string EntrySubType { get; set; }

        public string CustomId { get; set; }

        public TextChangedEventToCommandEntryControl() : base()
        {
            ClassId = "ClassLogin";
            IsEnabled = true;
            HorizontalTextAlignment = TextAlignment.Center;
            VerticalOptions = LayoutOptions.FillAndExpand;

            TextChanged += (sender, args) =>
            {
                TextChangedCommand.Execute((string)ReturnCommandParameter);
            };
        }
    }
}
