using Plugin.FilePicker;
using Plugin.Media;
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
    public partial class FormXAddPage : ContentPage
    {
        string filePath = string.Empty;
        Queue<string> paths = new Queue<string>();
        public FormXAddPage()
        {
            InitializeComponent();
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


        private void FormDTapGestureRecognizer_Tapped(object sender, EventArgs e)
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

        private void LabourTapGestureRecognizer_Tapped1(object sender, EventArgs e)
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

        private async void Button_Clicked(object sender, EventArgs e)
        {



            var files = await CrossFilePicker.Current.PickFile();

            if (files != null)
            {
                //lbl.Text = files.FileName;
            }







            //await CrossMedia.Current.Initialize();

            //if (!CrossMedia.Current.IsPickPhotoSupported)
            //{
            //    await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
            //    return;
            //}
            //var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            //{
            //    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium

            //});


            //if (file == null)
            //    return;

            //filePath = file.Path;
            //paths.Enqueue(filePath);

            //image.Source = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    file.Dispose();

            //    return stream;
            //});
        }

        //private async void Useu(object sender, EventArgs e)
        //{
        //  await  Navigation.PushAsync(new FormXUSeeUPage());
        //}

        private async void war(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new FormXWarPage());
        }

        //private void Button_Clicked_1(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new FormXUSeeUPage());
        //}




        //private void AddLabour(object sender, EventArgs e)
        //{
        //   Navigation.PushAsync(new FormDAddLabourPage());
        //}

        //private void AddEquipment(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new FormDAddEquipmentPage());
        //}

        //private void AddMaterial(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new FormDAddMaterialPage());

        //}

        //private void AddDetails(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new FormDAddDetailsPage());
        //}




    }
}