using RAMMS.MobileApps.Page;
using Rg.Plugins.Popup.Services;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormAPage : ContentPage
    {
        public FormAPage()
        {
            InitializeComponent();
            //Gridlist.ItemSelected += (sender, e) =>
            //{
            //    ((ListView)sender).SelectedItem = null;
            //};


            //Gridlists.ItemSelected += (sender, e) =>
            //{
            //    ((ListView)sender).SelectedItem = null;
            //};


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

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    PopupNavigation.Instance.PushAsync(new AddPopupPage());
        //}
    }
}