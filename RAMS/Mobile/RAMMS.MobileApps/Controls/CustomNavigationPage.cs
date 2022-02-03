using Xamarin.Forms;

namespace RAMMS.MobileApps
{
    public class CustomNavigationPage : NavigationPage
    {
        public CustomNavigationPage(Xamarin.Forms.Page root) : base(root)
        {
            //BarBackgroundColor = Color.DarkBlue;
            BarTextColor = Color.White;
        }
    }
}