using Acr.UserDialogs;
using Plugin.Connectivity;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormXWarPage : ContentPage
    {
        public FormXWarPage()
        {
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (Toggle.IsVisible)
                {
                    Toggle.IsVisible = false;
                }
                else
                {
                    Toggle.IsVisible = true;
                }
                Image image = sender as Image;
                string source = image.Source as FileImageSource;  //Getting the name of source as string
                if (String.Equals(source, "RoundedAddIcon.png"))
                {
                    image.Source = "minusicon.png";
                }
                else
                {
                    image.Source = "RoundedAddIcon.png";
                }
            }
            catch { }


        }


        private void LabourTapGestureRecognizer_Tapped1(object sender, EventArgs e)
        {
            try
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
            catch { }


        }



        private void PDFTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (Toggle1.IsVisible)
                {
                    Toggle1.IsVisible = false;
                }
                else
                {
                    Toggle1.IsVisible = true;
                }

                Image image = sender as Image;

                string source = image.Source as FileImageSource;  //Getting the name of source as string

                if (String.Equals(source, "RoundedAddIcon.png"))
                {
                    image.Source = "minusicon.png";
                }
                else
                {
                    image.Source = "RoundedAddIcon.png";
                }

            }
            catch { }

        }


        private void PDFViewTapGestureRecognizer_Tapped1(object sender, EventArgs e)
        {
            try
            {
                if (Toggle1.IsVisible)
                {
                    Toggle1.IsVisible = false;
                }
                else
                {
                    Toggle1.IsVisible = true;
                }
            }
            catch { }
        }
    }

    public class ImageSrcConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var data = value as WarImageDtlResponseDTO;
                var path = data.ImageFilenameUpload.Replace("Uploads\\\\", "");
                //var path = "formx/" + data.ImageFilenameUpload;
                return ImageSource.FromUri(new Uri(AppConst.ImageApiGetFormDDownloadAddress + path));
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}