using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormXDownloadUseeUPage : ContentPage
    {
        public FormXDownloadUseeUPage()
        {
            InitializeComponent();
            this.BackgroundColor = new Color(0, 0, 0, 0.6);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(false);
        }

        void Download_Clicked(Object sender, EventArgs args)
        {
            string id = (sender as Button).CommandParameter as string;
            string fileName = (sender as Button).AutomationId as string;
            WebClient client = new WebClient();
            var url = new Uri("http://103.67.154.117:806/ERT/DownloadPdfFormD?id=" + id);

            client.DownloadDataAsync(url);

            client.DownloadDataCompleted += async (s, e) =>
            {
                var data = e.Result; // get the downloaded text
                string documentsPath = "/storage/emulated/0/Android/data/ramms.mobileapps/";
                string localFilename = fileName;
                string localPath = Path.Combine(documentsPath, localFilename);

                if (File.Exists(localPath))
                {
                    File.Delete(localPath);
                }
                using (FileStream fs = File.Create(localPath))
                {
                    fs.Write(data, 0, data.Length);
                }
                await DisplayAlert("Done", "File downloaded and saved in Android/data/ramms.mobileapps/", "OK");
                await Navigation.PopAsync(false);
            };

            //client.DownloadDataCompleted += async (s, e) => {
            //    var data = e.Result; // get the downloaded text
            //    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            //    string localFilename = "download.pdf";
            //    string localPath = Path.Combine(documentsPath, localFilename);
            //    File.WriteAllBytes(localPath, data); // writes to local storage
            //    await DisplayAlert("Done", "File downloaded and saved", "OK");
            //    await Navigation.PopAsync(false);
            //};
        }
    }
}