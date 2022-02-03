using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormDPage : ContentPage
    {
        public FormDPage()
        {
            InitializeComponent();
            Gridlist.ItemSelected += (sender, e) =>
            {
                ((ListView)sender).SelectedItem = null;
            };
        }




        public void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Navigation.PushAsync(new FormADetailsPage());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (Toggle.IsVisible)
            {
                Toggle.IsVisible = false;
            }
            else
            {
                Toggle.IsVisible = true;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FormDAddPage());
        }
    }
}