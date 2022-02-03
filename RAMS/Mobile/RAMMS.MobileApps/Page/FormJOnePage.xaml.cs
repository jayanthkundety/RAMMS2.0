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
    public partial class FormJOnePage : ContentPage
    {
        public FormJOnePage()
        {
            InitializeComponent();
        }



        private void Button_Clicked(object sender, EventArgs e)
        {
            //if (this.BackgroundColor == Color.Red)
            //{
            //    this.BackgroundColor = Color.Blue;

            //}
            //else
            //{
            //    this.BackgroundColor = Color.Red;
            //}

            if (stack1.IsVisible)
            {
                stack1.IsVisible = false;
                stack2.IsVisible = true;

            }
            else
            {
                stack2.IsVisible = true;
                stack1.IsVisible = false;




            }
        }





        private void Button_Clicked_1(object sender, EventArgs e)
        {
            //var button = (Button)sender;
            //button.BackgroundColor = Color.Red;

            if (stack2.IsVisible)
            {
                stack2.IsVisible = false;
                stack1.IsVisible = true;

            }
            else
            {
                stack1.IsVisible = true;
                stack2.IsVisible = false;


            }
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





        private void TapGestureRecognizer_Tapped1(object sender, EventArgs e)
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

        //private void Button_Clicked_2(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new FormJCameraPopupPage());
        //}

    
    
    
    
    
    
    
    
    
    
    
    
    
    }
}