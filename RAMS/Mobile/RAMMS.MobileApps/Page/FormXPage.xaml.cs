using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormXPage : ContentPage
    {
        public FormXPage()
        {
            InitializeComponent();
            //Gridlist.ItemSelected += (sender, e) =>
            //{
            //    ((ListView)sender).SelectedItem = null;
            //};
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
            Navigation.PushAsync(new FormXAddPage());
        }

    }
}