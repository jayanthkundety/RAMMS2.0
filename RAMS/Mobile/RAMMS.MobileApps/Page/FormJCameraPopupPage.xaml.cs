using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using RAMMS.DTO.RequestBO;
using RAMMS.MobileApps.Interface;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Windows.Input;
using Acr.UserDialogs;

namespace RAMMS.MobileApps.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormJCameraPopupPage : ContentPage
    {
        private IRestApi _restApi;
        public ObservableCollection<DDListItems> DDPhototypeListItems { get; set; }

        //List<ImageUploadFormATABDTO> GetImageList { get; set; }

        List<string> _images { get; set; }

        public string SelectedPhotoType { get; set; }

        ObservableCollection<string> camImageCollection { get; set; }

        //public ObservableCollection<FormAImageListRequestDTO> DetailImageList { get; set; }


        public string UploadFileName { get; set; }
        public FileData fileDataList { get; set; }

        //private string url = "http://192.168.1.6:58764/api/imageUploadFormA";

        private MediaFile _image;


        public FormJCameraPopupPage()
        {

            InitializeComponent();
            listItemsCam.BindingContext = this;
            this.BackgroundColor = new Color(0, 0, 0, 0.6);

            var httpClient = new HttpClient(new AuthenticatedHttpClientHandler())
            {
                BaseAddress = new Uri(AppConst.DevApiBaseAddress),

                Timeout = TimeSpan.FromSeconds(60)
            };

            _restApi = Refit.RestService.For<IRestApi>(httpClient);

            fileDataList = new FileData();




            _images = new List<string>();

        }


        //userinspcode = CurrentPage.FindByName<ExtendedPicker>("insppicker");

        ///       userinspcode.ItemsSource = DDInspUserListListItems.Select((DDListItems arg) => arg.Text).ToList();

        protected override void OnAppearing()
        {

            camImageCollection = new ObservableCollection<string>();

            //GetImageList = new List<ImageUploadFormATABDTO>();

            DDPhototypeListItems = new ObservableCollection<DDListItems>();

            //DetailImageList = new ObservableCollection<FormAImageListRequestDTO>();

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



        public ICommand GalleryCommand
        {

            get
            {
                return new FreshCommand(async (obj) =>
                {







                });
            }


        }


        public ICommand CameraCommand
        {

            get
            {
                return new FreshCommand(async (obj) =>
                {




                });
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
                        TypeCode = App.AssetGroupSelection
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


        protected override void OnDisappearing()
        {
            base.OnDisappearing();



        }

        private async void SelectImagesButton_Clicked(object sender, EventArgs e)
        {
            try
            {

                await CrossMedia.Current.Initialize();


                if (SelectedPhotoType.Length == 0 || SelectedPhotoType == null)
                {
                    await DisplayAlert("RAMS", "Please select Photo Type", "OK");
                    return;
                }


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
                                    try
                                    {
                                        _images = images;

                                        foreach (var image in _images)
                                        {
                                            App.lstImage.Add(image);
                                        }

                                        ///listItemsCam.FlowItemsSource = _images;


                                    }
                                    catch (Exception ex)
                                    {

                                    }

                                }
                            });
                            listfiles(App.lstImage);

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


                    var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { SaveToAlbum = true, Name = "Test", Directory = "FormA" });

                    if (file != null)
                    {
                        var obj = ImageSource.FromStream(() =>
                        {
                            var stream = file.GetStream();

                            return stream;
                        });


                    }

                    //if (fileDataList.Files == null)
                    //{
                    //    fileDataList.Files = new List<MediaFile>();

                    //    fileDataList.FileNames = new ObservableCollection<string>();
                    //}

                    //fileDataList.Files.Add(file);

                    //fileDataList.FileNames.Add(Path.GetFileName(file.Path));

                    //UploadFileName = Path.GetFileName(file.Path);

                    //byte[] b = ReadAllBytes(file.GetStream());




                    ////            StreamReader r = new StreamReader(fs);


                    ////content.Add(new StreamContent(file.GetStream()), "\"file\"", $"\"{file.Path}\"");

                    //var httpClient = new System.Net.Http.HttpClient();

                    //var url = "http://192.168.1.6:58764/api/imageUploadFormA";

                    //var responseMsg = await httpClient.PostAsync(url, b);

                    //var remotePath = await responseMsg.Content.ReadAsStringAsync();



                    listfiles(App.lstImage);


                }
            }
            catch { }


        }






        public static byte[] ReadAllBytes(Stream instream)
        {
            if (instream is MemoryStream)
                return ((MemoryStream)instream).ToArray();

            using (var memoryStream = new MemoryStream())
            {
                instream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }



        string phyle;
        public void listfiles(ObservableCollection<string> ImageList)
        {
            try
            {
                var path1 = "/storage/emulated/0/Pictures/FormA/";
                var mp3Files = Directory.EnumerateFiles(path1, "*.jpg", SearchOption.AllDirectories);
                foreach (string currentFile in mp3Files)
                {
                    phyle = currentFile;
                    if ((_images.Where(x => x == phyle).Count() == 0))
                    {
                        _images.Add(phyle);
                        App.lstImage.Add(phyle);
                    }
                }



                listItemsCam.FlowItemsSource = null;
                listItemsCam.FlowItemsSource = App.lstImage;

            }
            catch (Exception e9)
            {

            }
        }





        public void Deletefiles()
        {
            try
            {
                var path1 = "/storage/emulated/0/Pictures/FormA/";
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






        //var aPpath = file.AlbumPath;
        //var path = file.Path;

        //if (file == null)
        //    return;

        //await DisplayAlert("File Location", file.Path, "OK");

        //image.Source = ImageSource.FromStream(() =>
        //{
        //    var stream = file.GetStream();
        //    return stream;
        //});



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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //MessagingCenter.Unsubscribe<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectedAndroid");
            //GC.Collect();
            _images.Clear();
            listItemsCam.FlowItemsSource = null;
            App.lstImage.Clear();
            await Navigation.PopAsync(false);

        }


        //private static byte[] GetBytesFromImage(string imagePath)
        //{
        //    var imgFile = new File(imagePath);
        //    var stream = new FileInputStream(imgFile);
        //    var bytes = new byte[imgFile.Length()];
        //    stream.Read(bytes);
        //    return bytes;
        //}

        private async void Button_Clicked_1(object sender, EventArgs e)
        {

            try
            {

                if (App.HeaderCode == 0)
                {
                    await DisplayAlert("RAMS", "Asset value is not found. Please add data and try again.", "OK");
                    return;
                }


                UserDialogs.Instance.ShowLoading("Loading");


                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        if (App.lstImage.Count == 0)
                        {
                            await DisplayAlert("RAMS", "Please select an image.", "OK");
                            return;
                        }
                        using (var client = new HttpClient())

                        using (var formData = new MultipartFormDataContent())
                        {
                            foreach (string path in App.lstImage)
                            {
                                HttpContent fileStreamContent = new StreamContent(File.OpenRead(path));

                                fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "file", FileName = Path.GetFileName(path) };

                                fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

                                formData.Add(fileStreamContent);

                            }

                            formData.Add(new StringContent(App.HeaderCode.ToString()), "AssetID");

                            formData.Add(new StringContent(SelectedPhotoType), "photoType");

                            try
                            {
                                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", App.AuthToken);

                                var res1 = await client.PostAsync(AppConst.ImageApiBaseFormJAddress, formData);

                                if (res1.IsSuccessStatusCode)
                                {
                                    listItemsCam.FlowItemsSource = null;

                                    App.lstImage.Clear();
                                    _images.Clear();

                                    await DisplayAlert("RAMS", "Image Uploaded Successfully.", "OK");

                                    await Navigation.PopAsync(false);
                                    //MessagingCenter.Send<object, string>(this, "Uploaded", "");
                                }
                                else
                                {
                                    await DisplayAlert("RAMS", "Internet Connection Failed. Please check try again.", "OK");

                                }
                            }
                            catch { }
                        }
                    }
                    catch (Exception ex)
                    {
                        UserDialogs.Instance.Toast(ex.Message);
                    }
                }
                else { }
                //UserDialogs.Instance.Alert("Please check your Internet Connection !");
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Alert(ex.Message);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
            return;
        }

        public static string getMimeFromFile(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException(filename + " not found");

            byte[] buffer = new byte[256];
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                if (fs.Length >= 256)
                    fs.Read(buffer, 0, 256);
                else
                    fs.Read(buffer, 0, (int)fs.Length);
            }
            try
            {
                System.UInt32 mimetype;
                FindMimeFromData(0, null, buffer, 256, null, 0, out mimetype, 0);
                System.IntPtr mimeTypePtr = new IntPtr(mimetype);
                string mime = Marshal.PtrToStringUni(mimeTypePtr);
                Marshal.FreeCoTaskMem(mimeTypePtr);
                return mime;
            }
            catch (Exception e)
            {
                return "unknown/unknown";
            }
        }


        [DllImport(@"urlmon.dll", CharSet = CharSet.Auto)]
        private extern static System.UInt32 FindMimeFromData(
        System.UInt32 pBC,
        [MarshalAs(UnmanagedType.LPStr)] System.String pwzUrl,
        [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
        System.UInt32 cbSize,
        [MarshalAs(UnmanagedType.LPStr)] System.String pwzMimeProposed,
        System.UInt32 dwMimeFlags,
        out System.UInt32 ppwzMimeOut,
        System.UInt32 dwReserverd
    );



        //// code here to assign image to _image
        //var content = new MultipartFormDataContent();
        //content.Add(new StreamContent(_image.GetStream()), "\"file\"", $"\"{_image.Path}\"");
        //var httpClient = new System.Net.Http.HttpClient();
        //var url = "http://upload.here.io/folder/subdir";
        //var responseMsg = await httpClient.PostAsync(url, content);
        //var remotePath = await responseMsg.Content.ReadAsStringAsync();




        //try
        //{

        //    FileStream fs = new FileStream(_images[0], FileMode.Open, FileAccess.Read);

        //    StreamReader r = new StreamReader(fs);

        //    //HttpContent fileStreamContent = new StreamContent(r.BaseStream);

        //    StreamContent scontent = new StreamContent(r.BaseStream);

        //    scontent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "file", FileName = "Test" };

        //    scontent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");


        //    //scontent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
        //    //{
        //    //    FileName = "newimage",
        //    //    Name = "image"
        //    //};
        //    //scontent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");



        //    var client = new HttpClient();
        //    var multi = new MultipartFormDataContent();
        //    multi.Add(scontent);
        //   // client.BaseAddress = new Uri(Constants.API_ROOT_URL);
        //    var result = client.PostAsync(url, multi).Result;
        //    //Debug.WriteLine(result.ReasonPhrase);
        //}
        //catch (Exception)
        //{
        //   // Debug.WriteLine(e);
        //}


        //    try
        //    {

        //        var formData = new MultipartFormDataContent();

        //        foreach ( var strImage in _images)
        //        {
        //            FileStream fs = new FileStream(strImage, FileMode.Open, FileAccess.Read);

        //            StreamReader r = new StreamReader(fs);

        //            HttpContent fileStreamContent = new StreamContent(r.BaseStream);

        //            fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "file", FileName = "Test" };

        //            fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

        //            using (var client = new HttpClient())

        //            using (formData = new MultipartFormDataContent())
        //            {
        //                formData.Add(fileStreamContent, "FormFile");

        //                var response = await client.PostAsync(url, formData);

        //                return;
        //            }

        //        }





        //    //    if (CrossConnectivity.Current.IsConnected)
        //    //    {

        //    //        //var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);
        //    //        var response = _restApi.ImageUploaded(formData,0,"");
        //    //        if (response.success)
        //    //        {
        //    //            if (ddtype == "Photo Type")
        //    //            {
        //    //                DDPhototypeListItems = new ObservableCollection<DDListItems>(response.data);
        //    //                phototypepicker.ItemsSource = DDPhototypeListItems.Select((DDListItems arg) => arg.Text).ToList();
        //    //                return DDPhototypeListItems;
        //    //            }


        //    //        }


        //    //    }
        //    //    else
        //    //        await DisplayAlert("Please check your Internet Connection !", "RAMS", "OK");
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    await DisplayAlert(ex.Message, "RAMS", "OK");

        //    //}



        //    //HttpContent fileStreamContent = new StreamContent(Stream);
        //    //fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "file", FileName = fileName };
        //    //fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");
        //    //using (var client = new HttpClient())
        //    //using (var formData = new MultipartFormDataContent())
        //    //{
        //    //    formData.Add(fileStreamContent);
        //    //    var response = await client.PostAsync(url, formData);
        //    //    return response.IsSuccessStatusCode;
        //    //}




        //}            
        //catch(Exception ex) 
        //{

        //}




        public class FileData
        {
            public string UploadFileName { get; set; }

            public List<MediaFile> Files { get; set; }

            public ObservableCollection<string> FileNames { get; set; }
        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            try
            {
                if (phototypepicker.SelectedIndex == -1)
                {
                    await UserDialogs.Instance.AlertAsync("Please select photograph type", "RAMS", "OK");
                    return;
                }

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
                                try
                                {
                                    //_images = images;

                                    foreach (var image in images)
                                    {
                                        App.lstImage.Add(image);
                                    }

                                    listItemsCam.FlowItemsSource = null;
                                    listItemsCam.FlowItemsSource = App.lstImage;

                                    //listfiles(App.lstImage);
                                }
                                catch (Exception ex)
                                {

                                }


                            }


                        });


                    }
                    //
                }
                else
                {
                    await DisplayAlert("Permission Denied!", "\nPlease go to your app settings and enable permissions.", "Ok");
                }
            }
            catch (Exception ex) { }
        }


        public async Task<MediaFile> TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No camera available", ":Permission not granted to photos.", "OK");
                return null;
            }

            var options = new StoreCameraMediaOptions()
            {
                Name = DateTime.Now.ToShortDateString() + "Form_A",
                SaveToAlbum = false,
            };
            var file = await CrossMedia.Current.TakePhotoAsync(options);
            if (file == null)
                return null;
            else
            {
                double filesize = (file.GetStream().Length / 1048576);
                if (filesize > 5)
                {

                    //await DisplayAlert("Maximum 5Mb files allowed","OK");
                    return null;
                }
            }
            return file;
        }


        private async void Button_Clicked_3(object sender, EventArgs e)
        {
            try
            {
                if (phototypepicker.SelectedIndex == -1)
                {
                    await UserDialogs.Instance.AlertAsync("Please select photograph type", "RAMS", "OK");
                    return;
                }

                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }


                var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { SaveToAlbum = true, Name = "Test", Directory = "FormA" });

                if (file != null)
                {
                    var obj = ImageSource.FromStream(() =>
                    {
                        var stream = file.GetStream();

                        return stream;
                    });
                    double filesize = (file.GetStream().Length / 1048576);
                    if (filesize > 5)
                    {

                        await DisplayAlert("RAMS", "Maximum 5Mb files allowed", "OK");
                        return;
                    }

                }

                listfiles(App.lstImage);
            }
            catch (Exception ex) { }
        }



        public ICommand DeleteImageCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    var item = obj as string;
                    App.lstImage.Remove(item);
                    listItemsCam.ItemsSource = null;
                    listItemsCam.ItemsSource = App.lstImage;
                });
            }
        }
    }
}