using Acr.UserDialogs;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class FormDPdfUpload : ContentPage
    {
        public ObservableCollection<AccUccImageDtlResponseDTO> FormDDetailImageList { get; set; }
        public ObservableCollection<ImageUploadFormATABDTO> ImageList { get; set; }

        private IUserDialogs _userDialogs;

        Image image { get; set; }

        Label label { get; set; }
        public bool NotToEdit { get; set; } = true;
        Grid MainGrid1 { get; set; }
      
        private IRestApi _restApi;
        public FormDPdfUpload()
        {
            InitializeComponent();
            this.BackgroundColor = new Color(0, 0, 0, 0.6);

            var httpClient = new HttpClient(new AuthenticatedHttpClientHandler())
            {
                BaseAddress = new Uri(AppConst.DevApiBaseAddress),

                Timeout = TimeSpan.FromSeconds(60)
            };

            _restApi = Refit.RestService.For<IRestApi>(httpClient);
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


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            //view
            if (App.ViewState == true)
            {
                btnAdd.IsEnabled = false;
                btnDownload.IsEnabled = false;
                NotToEdit = false;
                Images.IsVisible = true;
            }
            //edit
            else
            {
                btnAdd.IsEnabled = true;
                btnDownload.IsEnabled = true;
                NotToEdit= true;

                imagelistss.IsVisible = true;
            }

            string strDetailCode = Convert.ToInt32(App.FormDDetailCode).ToString();

            await GetImageList(strDetailCode);

            return;
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

        private void Close_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(false);
            //Navigation.PushAsync(new FormDAddDetailsPage());
        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FormDUseeUploadPDF());
        }

        private void DownloadButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FormDUseePDFDownload());
        }


        private async Task<int> GetImageList(string AssetID)
        {
            try
            {



                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        if (CrossConnectivity.Current.IsConnected)
                        {
                            var json = Newtonsoft.Json.JsonConvert.SerializeObject(AssetID);
                            var response = await _restApi.GetAccUccFormD(AssetID);
                            if (response.success)
                            {
                                var content = response.data;
                                FormDDetailImageList = new ObservableCollection<AccUccImageDtlResponseDTO>(response.data);
                                Imagelist.ItemsSource = FormDDetailImageList;
                                Imagelist1.ItemsSource = FormDDetailImageList;
                                int i = 0;

                                foreach (var listdata in FormDDetailImageList)
                                {
                                    listdata.ImageSrno = i + 1;
                                    string Path = @"FormD\" + listdata.ImageFilenameUpload;

                                    MainGrid.RowDefinitions.Add(new RowDefinition { Height = 100 });

                                    ActivityIndicator indicator = new ActivityIndicator() { BindingContext = image };
                                    indicator.SetBinding(ActivityIndicator.IsRunningProperty, new Binding("IsLoading", source: image));
                                    indicator.SetBinding(ActivityIndicator.IsVisibleProperty, new Binding("IsLoading", source: image));
                                    image = new Image
                                    {
                                        Source = "Pdf.png"
                                    };

                                    label = new Label
                                    {
                                        Text = listdata.ImageFilenameSys,
                                        HorizontalTextAlignment = TextAlignment.Center
                                        //BackgroundColor=Color.Red

                                    };

                                    Grid.SetColumn(indicator, i);
                                    Grid.SetColumn(image, i);
                                    Grid.SetRow(label, 1);
                                    Grid.SetColumn(label, i);
                                    MainGrid.Children.Add(indicator);
                                    MainGrid.Children.Add(image);
                                    MainGrid.Children.Add(label);
                                    i = i + 1;
                                }
                            }
                        }
                        else
                            await DisplayAlert("Please check your Internet Connection !", "RAMS", "OK");
                    }
                    catch (Exception ex) { }

                }
            }
            catch { }

            return 1;
        }

        public ICommand DeleteImageCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    try
                    {
                        var actionResult = await UserDialogs.Instance.ConfirmAsync("Are you sure want to delete?", "RAMS", "Yes", "No");
                        if (actionResult)
                        {
                            UserDialogs.Instance.ShowLoading("Loading");
                            if (CrossConnectivity.Current.IsConnected)
                            {
                                var imageID = (obj as AccUccImageDtlResponseDTO).NO;

                                var response = await _restApi.DeleteAccUccFormD(imageID);

                                if (response.success)
                                {
                                    await UserDialogs.Instance.AlertAsync("Image deleted successfully.", "RAMS", "0K");

                                    string strDetailCode = Convert.ToInt32(App.HeaderCode).ToString();

                                    await GetImageList(strDetailCode);
                                }
                                else
                                    UserDialogs.Instance.Toast(response.errorMessage);
                            }
                            else
                                UserDialogs.Instance.Alert("Please check your Internet Connection !");
                        }
                    }
                    catch (Exception ex)
                    {
                        UserDialogs.Instance.Alert(ex.Message);
                    }
                    finally
                    {
                        UserDialogs.Instance.HideLoading();
                    }
                });
            }

        }


        public async Task<MediaFile> PickPhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                UserDialogs.Instance.Alert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return null;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return null;
            else
            {
                double filesize = (file.GetStream().Length / 1048576);
                if (filesize > 5)
                {
                    UserDialogs.Instance.Alert("Maximum 5Mb files allowed");
                    return null;
                }
            }
            return file;
        }

        private void DeleteImage_Tapped(object sender, EventArgs e)
        {
            var id = sender as Image;
            var modelData = (id.GestureRecognizers[0] as TapGestureRecognizer).CommandParameter;
            DeleteImageCommand.Execute(modelData);
        }

        private void Imagelist_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}