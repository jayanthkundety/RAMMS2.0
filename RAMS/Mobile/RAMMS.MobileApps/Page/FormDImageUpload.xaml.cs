using Acr.UserDialogs;
using Newtonsoft.Json;
using Plugin.Connectivity;
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
    public partial class FormDImageUpload : ContentPage
    {

        public ObservableCollection<WarImageDtlResponseDTO> FormDDetailImageList { get; set; }
        public ObservableCollection<ImageUploadFormATABDTO> ImageList { get; set; }

        Image image { get; set; }

        Label label { get; set; }

        Grid MainGrid1 { get; set; }

        private IRestApi _restApi;

        public FormDImageUpload()
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


        public ICommand DeleteImageCommand1
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
                                var imageID = (obj as WarImageDtlResponseDTO).NO;
                                var response = await _restApi.DeleteImageFormD(imageID);

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



        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (App.ViewState == true)
            {
                btnAdd.IsEnabled = false;
                Images.IsVisible = true;
            }
            else
            {
                btnAdd.IsEnabled = true;
                imagelistss.IsVisible = true;
            }


            string strDetailCode = Convert.ToInt32(App.FormDDetailCode).ToString();

            await GetImageList(strDetailCode);

            return;
        }


        //if (App.ReturnType == "Edit" || App.ReturnType == "View")
        //{
        //    //await GetMyFormAHeaderEditAndViewListReports(_editViewModel.HdrFahPkRefNo);


        //    GetHeaderNoCode = App.DetailHeaderCode;

        //    SelectedHdrEditItem = await GetEditViewHeaderdetails(GetHeaderNoCode);

        //    DropDownMasterSetup(App.ReturnType);


        //    if (SelectedHdrEditItem.No > 2 && SelectedHdrEditItem.FormADetails.Count > 0)
        //        DetailFromReqADtlGridListItems = await GetDetailNewGridListDetails(SelectedNewHdrItem.No);
        //    else if (SelectedHdrEditItem.No > 2)
        //    {
        //        DetailFromReqADtlGridListItems = await GetDetailNewGridListDetails(GetHeaderNoCode);
        //        //DetailFromADtlGridListItems = await GetMyFormAHeaderEditAndViewListReports(SelectedHdrItem.No);
        //    }

        //    //Find Details button need to work it out 
        //    //Retrieve Header data disable finish button
        //    //Retrieve details and allow user to add, edit and delete
        //    App.ReturnType = "";
        //    return;
        //}


        //if ()
        //{
        //    //await GetMyFormAHeaderEditAndViewListReports(_editViewModel.HdrFahPkRefNo);
        //DropDownMasterSetup(_editViewModel.Type);

        //SelectedHdrItem.No = _editViewModel.HdrFahPkRefNo;

        //if (SelectedNewHdrItem.No > 2 && SelectedNewHdrItem.FormADetails.Count > 0)

        //    DetailFromReqADtlGridListItems = await GetDetailNewGridListDetails(SelectedNewHdrItem.No);

        //else if (SelectedHdrItem.No > 2)
        //{
        //    DetailFromReqADtlGridListItems = await GetDetailNewGridListDetails(SelectedHdrItem.No);
        //}

        //    return;
        //}
        //else
        //{

        //    SelectedHdrItem.No = _editViewModel.HdrFahPkRefNo;

        //    if (SelectedNewHdrItem.No > 2 && SelectedNewHdrItem.FormADetails.Count > 0)

        //        DetailFromReqADtlGridListItems = await GetDetailNewGridListDetails(SelectedNewHdrItem.No);

        //    else if (SelectedHdrItem.No > 2)
        //    {
        //        GetHeaderNoCode = SelectedHdrItem.No;

        //        DetailFromReqADtlGridListItems = await GetDetailNewGridListDetails(SelectedHdrItem.No);
        //    }

        //    return;
        //}



        //ObjFormADetail.OnGetInspSign(null, null);

        //ObjFormADetail.OnGetVerifySign(null, null);












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

        private void Close_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync(false);

        }

        private void AddButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FormDWarImageUpload());
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
                            var response = await _restApi.GetImagesFormD(AssetID);

                            if (response.success)
                            {
                                var content = response.data;
                                FormDDetailImageList = new ObservableCollection<WarImageDtlResponseDTO>(response.data);
                                Imagelist.ItemsSource = FormDDetailImageList;
                                Imagelistss.ItemsSource = FormDDetailImageList;
                                int i = 0;

                                try
                                {
                                    MainGrid.Children.Clear();
                                    foreach (var listdata in FormDDetailImageList)
                                    {
                                        listdata.ImageSrno = i + 1;
                                        string Path = listdata.ImageFilenameUpload.Replace("Uploads/", "");
                                        MainGrid.RowDefinitions.Add(new RowDefinition { Height = 100 });
                                        ActivityIndicator indicator = new ActivityIndicator() { BindingContext = image };
                                        indicator.SetBinding(ActivityIndicator.IsRunningProperty, new Binding("IsLoading", source: image));
                                        indicator.SetBinding(ActivityIndicator.IsVisibleProperty, new Binding("IsLoading", source: image));
                                        image = new Image
                                        {
                                            Source = ImageSource.FromUri(new Uri(AppConst.ImageApiGetFormDDownloadAddress + Path))

                                        };

                                        label = new Label
                                        {
                                            Text = listdata.ImageFilenameSys,
                                            HorizontalTextAlignment = TextAlignment.Center

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
                                catch (Exception ex){ }
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

        private void DeleteImage_Tapped(object sender, EventArgs e)
        {
            var id = sender as Image;
            var modelData = (id.GestureRecognizers[0] as TapGestureRecognizer).CommandParameter;
            DeleteImageCommand1.Execute(modelData);
        }



    }
}