using Newtonsoft.Json;
using Plugin.Connectivity;
using RAMMS.DTO.RequestBO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormDAddDetailsPage : ContentPage
    {
        


        public FormDAddDetailsPage()
        {
            InitializeComponent();
            this.BackgroundColor = new Color(0, 0, 0, 0.6);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
           
           Navigation.PushAsync(new FormDPdfUpload());

        }

        private void WarButton_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FormDImageUpload());
        }



    }
}