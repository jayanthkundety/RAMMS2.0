using FreshEssentials;
using Xamarin.Forms;

namespace RAMMS.MobileApps
{
    public class ExtendedPicker : Picker
    {
        public ExtendedPicker() : base()
        {
            
            HeightRequest = 40d;
           
           // Title = "Please select an option";
            FontFamily = Device.RuntimePlatform == Device.Android ? "ProximaNova-Semibold.ttf#ProximaNova" : "ProximaNova-Semibold";
            FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(ExtendedPicker));
            // BackgroundColor = Color.FromHex("#f8f8f8");
            BackgroundColor = Color.White;
           
        }
    }
}