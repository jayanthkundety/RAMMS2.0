using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps
{
    public class CustomGridStackLayout : StackLayout
    {
        public static readonly BindableProperty TappedCommandProperty = BindableProperty.Create<CustomGridStackLayout, ICommand>(x => x.TappedCommand, null);

        public ICommand TappedCommand
        {
            get { return (ICommand)GetValue(TappedCommandProperty); }
            set { SetValue(TappedCommandProperty, value); }
        }

        public static readonly BindableProperty TappedCommandParameterProperty = BindableProperty.Create<CustomGridStackLayout, string>(x => x.TappedCommandParameter, "0_0");

        public string TappedCommandParameter
        {
            get { return (string)GetValue(TappedCommandParameterProperty); }
            set { SetValue(TappedCommandParameterProperty, value); }
        }

        public string DefaultTappedParameter { get; set; }

        public CustomGridStackLayout()
        {
            var gestureRecognizer = new TapGestureRecognizer();

            gestureRecognizer.Tapped += (s, e) =>
            {
                if (TappedCommand != null && TappedCommand.CanExecute(TappedCommandParameter))
                {
                    TappedCommand.Execute(TappedCommandParameter);
                }
            };

            GestureRecognizers.Add(gestureRecognizer);
        }
    }
}