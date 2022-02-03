using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Plugin.Media;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using RAMMS.MobileApps.Page;
using Rg.Plugins.Popup.Extensions;
using RAMMS.DTO.RequestBO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections;

namespace RAMMS.MobileApps
{
    public partial class FormAAddPage : ContentPage
    {

        public ObservableCollection<FormAImageListRequestDTO> DetailImageList { get; set; }
        public FormAAddPageModel formAAddPageModel;  
        public FormAAddPage()
        {
            InitializeComponent();
            Device.BeginInvokeOnMainThread(async () => await AskForPermissions());
            
        }



        private async void Entry_Focused(object sender, FocusEventArgs e)
        {
           // await Navigation.PushPopupAsync(new LocationSiteRef_PopUp());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            DetailImageList = new ObservableCollection<FormAImageListRequestDTO>();
        }



        void OnStepperValueChanged(object sender, ValueChangedEventArgs e)
        {
            double value = e.NewValue;
            //_rotatingLabel.Rotation = value;
            //_displayLabel.Text = string.Format("The Stepper value is {0}", value);
        }



        private async void SelectImagesButton_Clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            var action = await DisplayActionSheet("Choose Action", "", "", "Gallery", "Camera");

            if (action == "Gallery")
            {
                //Check users permissions.
                var storagePermissions = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                
                var photoPermissions = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);

                if (storagePermissions == PermissionStatus.Granted && photoPermissions == PermissionStatus.Granted)
                {

                    if (Device.RuntimePlatform == Device.Android)
                    {
                        DependencyService.Get<IMediaService>().OpenGallery();

                        MessagingCenter.Unsubscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectedAndroid");

                        MessagingCenter.Subscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectedAndroid", (s, images) =>
                        {
                            if (images.Count > 0)
                            {

                                //for (int iCount = 0; iCount < images.Count; iCount++)
                                   // lbl1.Text += images[iCount].ToString() + "\n";


                            }
                        });
                    }
                }
                else
                {
                    await DisplayAlert("Permission Denied!", "\nPlease go to your app settings and enable permissions.", "Ok");
                }
            }

            else if (action == "Camera")
            {

                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg"
                });

                if (file == null)
                    return;

                await DisplayAlert("File Location", file.Path, "OK");

                //image.Source = ImageSource.FromStream(() =>
                //{
                //    var stream = file.GetStream();
                //    return stream;
                //});
            }

        }



















            private async Task<bool> AskForPermissions()
            {
                try
                {
                    await CrossMedia.Current.Initialize();

                    var storagePermissions = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);
                    var photoPermissions = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Photos);
                    if (storagePermissions != PermissionStatus.Granted || photoPermissions != PermissionStatus.Granted)
                    {
                        var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Storage, Permission.Photos });
                        storagePermissions = results[Permission.Storage];
                        photoPermissions = results[Permission.Photos];
                    }

                    if (storagePermissions != PermissionStatus.Granted || photoPermissions != PermissionStatus.Granted)
                    {
                        await DisplayAlert("Permissions Denied!", "Please go to your app settings and enable permissions.", "Ok");
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("error. permissions not set. here is the stacktrace: \n" + ex.StackTrace);
                    return false;
                }
            }








        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    //if (this.BackgroundColor == Color.Red)
        //    //{
        //    //    this.BackgroundColor = Color.Blue;
               
        //    //}
        //    //else
        //    //{
        //    //    this.BackgroundColor = Color.Red;
        //    //}

        //    if (stack1.IsVisible)
        //    {

        //        stack1.IsVisible = false;
        //        stack2.IsVisible = true;
        //        btnPhoto.BackgroundColor = Color.FromHex("1D2D50");
        //        btnPhoto.TextColor = Color.White;
        //        btnaddform.BackgroundColor = Color.LightGray;
        //        btnaddform.TextColor = Color.FromHex("1D2D50");
        //    }
        //    else
        //    {
        //        stack2.IsVisible = true;
        //        stack1.IsVisible = false;
                
        //    }
        //}

    



        //private void Button_Clicked_1(object sender, EventArgs e)
        //{
        //    //var button = (Button)sender;
        //    //button.BackgroundColor = Color.Red;
           
        //    if (stack2.IsVisible)
        //    {
        //        stack2.IsVisible = false;
        //        stack1.IsVisible = true;
        //        btnaddform.BackgroundColor = Color.FromHex("1D2D50");
        //        btnaddform.TextColor = Color.White;
        //        btnPhoto.BackgroundColor = Color.LightGray;
        //        btnPhoto.TextColor = Color.FromHex("1D2D50");

        //    }
        //    else
        //    {
        //        stack1.IsVisible = true;
        //        stack2.IsVisible = false;

               
        //    }
        //}

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
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CameraPopUpPage());
        }

    }
}