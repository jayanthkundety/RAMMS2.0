using RAMMS.DTO.RequestBO;
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
    public partial class FormC1C2AddPage : ContentPage
    {
        public FormC1C2AddPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped123(object sender, EventArgs e)
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

        private void TapGestureRecognizer_Tapped456(object sender, EventArgs e)
        {
            if (Toggle21.IsVisible)
            {
                Toggle21.IsVisible = false;
            }
            else
            {
                Toggle21.IsVisible = true;
            }
        }
    }
    public class C1C2ImageSrcConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var data = value as FormC1C2ImgRequestDTO;
                //var path = data.ImageUserFilepath.Replace("/Uploads/", "");
                var path = data.ImageUserFilepath + "/" + data.ImageFilenameUpload;
                return ImageSource.FromUri(new Uri(AppConst.ImageApiGetDownloadAddress + path));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}