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
    public partial class FormXUSeeUPage : ContentPage
    {
        public FormXUSeeUPage()
        {
            InitializeComponent();
            this.BackgroundColor = new Color(0, 0, 0, 0.6);
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
            if (Toggle.IsVisible)
            {
                Toggle.IsVisible = false;
            }
            else
            {
                Toggle.IsVisible = true;
            }


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
}