using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace RAMMS.MobileApps.Page.BatuNiahMap
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BatuniahMapSection : ContentPage
    {
        public BatuniahMapSection(object obj)
        {
            InitializeComponent();
            UserDialogs.Instance.ShowLoading("Loading");

            webView.Source = "http://103.67.154.117:806/Map/RoadMap?roadCode=" +obj.ToString() +"&section=BATUNIAH";


            webView.Navigated += WebView_Navigated;
        }

        void WebView_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Result == WebNavigationResult.Success)
            {
                UserDialogs.Instance.HideLoading();
            }
        }
    }
}