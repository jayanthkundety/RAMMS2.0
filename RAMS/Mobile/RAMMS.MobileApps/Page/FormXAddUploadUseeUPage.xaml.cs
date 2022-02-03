using Acr.UserDialogs;
using Plugin.Connectivity;
using Plugin.Media;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormXAddUploadUseeUPage : ContentPage
    {
        public ObservableCollection<WarImageDtlResponseDTO> FormDDetailImageList { get; set; }
        public ObservableCollection<ImageUploadFormATABDTO> ImageList { get; set; }
        public ObservableCollection<FileBase> PdfList;


        Image image1 { get; set; }

        Label label { get; set; }

        Grid MainGrid1 { get; set; }

        private IRestApi _restApi;


        public ObservableCollection<DDListItems> DDPhototypeListItems { get; set; }

        List<ImageUploadFormATABDTO> GetImageList { get; set; }

        List<string> _images { get; set; }

        public string SelectedPhotoType { get; set; }

        ObservableCollection<string> camImageCollection { get; set; }

        public ObservableCollection<FormAImageListRequestDTO> DetailImageList { get; set; }


        public string UploadFileName { get; set; }
        public FileData fileDataList { get; set; }

        public FormXAddUploadUseeUPage()
        {
            InitializeComponent();
            this.BackgroundColor = new Color(0, 0, 0, 0.6);


            var httpClient = new HttpClient(new AuthenticatedHttpClientHandler())
            {
                BaseAddress = new Uri(AppConst.DevApiBaseAddress),

                Timeout = TimeSpan.FromSeconds(60)
            };

            _restApi = Refit.RestService.For<IRestApi>(httpClient);

            fileDataList = new FileData();

            PdfList = new ObservableCollection<FileBase>();


            _images = new List<string>();
        }

        private void CanceButton_Clicked(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new FormDPdfUpload());
            _images.Clear();
            App.lstImage.Clear();
            PdfList.Clear();
            Navigation.PopAsync(false);
        }

        protected override void OnAppearing()
        {

            camImageCollection = new ObservableCollection<string>();

            GetImageList = new List<ImageUploadFormATABDTO>();

            DDPhototypeListItems = new ObservableCollection<DDListItems>();

            DetailImageList = new ObservableCollection<FormAImageListRequestDTO>();

            GetddListDetails("Photo Type");

            Deletefiles();

            //phototypepicker.ItemsSource = DDPhototypeListItems.Select((DDListItems arg) => arg.Text).ToList();

            int photoindex = DDPhototypeListItems.ToList().FindIndex(a => a.Value == App.PhotoType);
            //if (photoindex == -1) { photoindex = 1; }
            phototypepicker.SelectedIndex = photoindex;

            if (App.PhotoType != null || App.PhotoType != "")
            {
                phototypepicker.SelectedIndexChanged += (s, e) =>
                {
                    if (phototypepicker.SelectedIndex != -1)
                    {
                        SelectedPhotoType = DDPhototypeListItems[phototypepicker.SelectedIndex].Value.ToString();
                        App.PhotoType = SelectedPhotoType;
                    }


                };

            }




        }

        private async void SelectImagesButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (phototypepicker.SelectedIndex == -1)
                {
                    await UserDialogs.Instance.AlertAsync("Please select PDF type ", "RAMS", "OK");
                    return;
                }

                await CrossMedia.Current.Initialize();

                if (Device.RuntimePlatform == Device.Android)
                {

                    var customFileType =
                        new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
                        {
                                { DevicePlatform.iOS, new[] { "com.adobe.pdf" } },
                                { DevicePlatform.Android, new[] { "application/pdf" } },
                                { DevicePlatform.UWP, new[] { ".pdf" } }
                        });

                    var pickOption = new PickOptions { PickerTitle = "Please select PDF file", FileTypes = customFileType };

                    var result = await FilePicker.PickMultipleAsync(pickOption);

                    var pdfList = result.ToList();
                    foreach (var item in pdfList)
                    {
                        PdfList.Add(item);
                        //App.lstImage.Add(item.FileName);
                    }
                    listItemsCam.FlowItemsSource = PdfList;
                    //listItemsCam.FlowItemsSource = App.lstImage;
                }
            }
            catch (Exception ex) { }
        }


        public void Deletefiles()
        {
            try
            {
                var path1 = "/storage/emulated/0/Pictures/FormD/";
                var mp3Files = Directory.EnumerateFiles(path1, "*.jpg", SearchOption.AllDirectories);
                foreach (string currentFile in mp3Files)
                {
                    phyle = currentFile;
                    File.Delete(phyle);
                }

            }
            catch (Exception e9)
            {

            }
        }


        public async Task<ObservableCollection<DDListItems>> GetddListDetails(string ddtype)
        {
            try
            {


                //  _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    var ddlist = new DDLookUpDTO()
                    {
                        Type = ddtype,
                        TypeCode = "FormX_useeu"
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);
                    var response = await _restApi.GetDDList(ddlist);
                    if (response.success)
                    {
                        if (ddtype == "Photo Type")
                        {
                            DDPhototypeListItems = new ObservableCollection<DDListItems>(response.data);
                            phototypepicker.ItemsSource = DDPhototypeListItems.Select((DDListItems arg) => arg.Text).ToList();
                            return DDPhototypeListItems;
                        }


                    }


                }
                else
                    await DisplayAlert("Please check your Internet Connection !", "RAMS", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert(ex.Message, "RAMS", "OK");

            }

            return new ObservableCollection<DDListItems>();
        }



        string phyle;
        public void listfiles(List<string> ImageList)
        {
            try
            {
                var path1 = "/storage/emulated/0/Pictures/FormD/";
                var mp3Files = Directory.EnumerateFiles(path1, "*.jpg", SearchOption.AllDirectories);
                foreach (string currentFile in mp3Files)
                {
                    phyle = currentFile;
                    _images.Add(phyle);
                    App.lstImage.Add(phyle);
                }



                listItemsCam.FlowItemsSource = ImageList;

            }
            catch (Exception e9)
            {

            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (App.FormDDetailCode == 0)
                {
                    await DisplayAlert("RAMS", "Asset value is not found. Please add data and try again.", "OK");
                    return;
                }


                //_userDialogs.ShowLoading("Loading");
                UserDialogs.Instance.ShowLoading("Loading");


                if (CrossConnectivity.Current.IsConnected)
                {

                    try
                    {
                        if (PdfList.Count == 0)
                        {
                            await DisplayAlert("RAMS", "Please select PDF.", "OK");
                            return;
                        }

                        using (var client = new HttpClient())

                        using (var formData = new MultipartFormDataContent())
                        {
                            foreach (var item in PdfList)
                            {
                                HttpContent fileStreamContent = new StreamContent(File.OpenRead(item.FullPath));

                                fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "files", FileName = Path.GetFileName(item.FullPath) };

                                fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                                formData.Add(fileStreamContent);

                            }

                            formData.Add(new StringContent(App.FormDDetailCode.ToString()), "Id");

                            formData.Add(new StringContent(SelectedPhotoType), "PhotoType");
                            //formData.Add(new StringContent("Barier"), "photoType");

                            try
                            {
                                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", App.AuthToken);

                                var res1 = await client.PostAsync(AppConst.ImageApiFormDPDFBaseAddress, formData);


                                if (res1.IsSuccessStatusCode)
                                {
                                    listItemsCam.FlowItemsSource = null;

                                    PdfList.Clear();
                                    App.lstImage.Clear();
                                    _images.Clear();

                                    await DisplayAlert("RAMS", "PDF Uploaded Successfully.", "OK");

                                    //string strDetailCode = Convert.ToInt32(App.HeaderCode).ToString();

                                    //GetImageList(strDetailCode);

                                    await Navigation.PopAsync(false);
                                }
                                else
                                {
                                    await DisplayAlert("RAMS", "Internet Connection Failed. Please check try again.", "OK");

                                }
                            }
                            catch (Exception ex)
                            {

                                await DisplayAlert("RAMS", ex.Message, "OK");
                            }
                        }









                        /*ListImage.Clear();

                        TabImageUpload bImageValue = new TabImageUpload();

                        foreach (string path in _images)
                        {


                            //FileStream fs = new FileStream(imagedata, FileMode.Open, FileAccess.Read);
                            //StreamReader r = new StreamReader(fs);
                            //System.IO.File.re
                            HttpContent fileStreamContent = new StreamContent(File.OpenRead(path));
                            fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "file", FileName = "UploadImage" };
                            fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
                            using (var client = new HttpClient())
                            using (var formData = new MultipartFormDataContent())
                            {
                                formData.Add(fileStreamContent);
                                await client.PostAsync(url, formData);

                            }


                            string base64String = Convert.ToBase64String(ReadAllBytes(r.BaseStream));
                            bImageValue.ImageFile = base64String;
                            bImageValue.Filename = imagedata.ToString();
                            //bImageValue.FileContentType = getMimeFromFile(bImageValue.Filename);
                            ListImage.Add(bImageValue);

                        }

                        var json = Newtonsoft.Json.JsonConvert.SerializeObject(ListImage);
                        var response = await _restApi.ImageUploaded(ListImage, App.HeaderCode, App.PhotoType);

                        if (response.success)
                        {
                            App.PhotoType = "";
                        }
                        else { }
                        */
                        //_userDialogs.Toast(response.errorMessage);
                        //iStrValue = response.ToSt//ring();
                    }
                    catch (Exception ex)
                    {
                        //_userDialogs.Toast(ex.Message);
                    }



                }
                else { }
                //UserDialogs.Instance.Alert("Please check your Internet Connection !");
            }
            catch (Exception ex)
            {
                //_userDialogs.Alert(ex.Message);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
                //_userDialogs.HideLoading();
            }
            return;
        }
    }
}