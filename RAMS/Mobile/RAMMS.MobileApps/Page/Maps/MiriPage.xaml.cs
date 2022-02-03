using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps.Page.Maps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MiriPage : ContentPage
    {
        public MiriPage()
        {
            InitializeComponent();

        }
           

        private void Button_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new MiriMapPage());
        }
    }
}