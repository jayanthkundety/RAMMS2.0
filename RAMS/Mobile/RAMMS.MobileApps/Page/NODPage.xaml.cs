using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NODPage : ContentPage
    {
        public NODPage()
        {
            InitializeComponent();
            this.BackgroundColor = new Color(0, 0, 0, 0.4);
        }

        private async void Button_Clicked(object sender, System.EventArgs e)
        {
           // await ked.RotateYTo(-90, timeout, Easing.SpringIn);
        }
    }
}