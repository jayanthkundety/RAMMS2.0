using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps.PageModel
{
    public class FormB1B2CameraPopupPageModel : FreshBasePageModel
    {
        private IRestApi _restApi;
        private IUserDialogs _userDialogs;
        private ILocalDatabase _localDatabase;

        public ObservableCollection<DDListItems> DDPhototypeListItems { get; set; }
        public ObservableCollection<string> FlowImageList { get; set; }
        List<string> _images { get; set; }
        public int SelectedPhotoTypeIndex { get; set; } = -1;

        public FormB1B2CameraPopupPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;
            _localDatabase = localDatabase;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            _images = new List<string>();
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await GetddListDetails("Photo Type");
            Deletefiles();
        }
        public async Task<int> GetddListDetails(string ddtype)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var ddlist = new DDLookUpDTO()
                    {
                        Type = ddtype,
                        TypeCode = "BR"
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);
                    var response = await _restApi.GetDDList(ddlist);
                    if (response.success)
                    {
                        if (ddtype == "Photo Type")
                        {
                            DDPhototypeListItems = new ObservableCollection<DDListItems>(response.data);
                        }
                    }
                }
                else
                    await _userDialogs.AlertAsync("Please check your Internet Connection !", "RAMS", "OK");
            }
            catch (Exception ex)
            {
                await _userDialogs.AlertAsync(ex.Message, "RAMS", "OK");
            }
            return 1;
        }

        public ICommand AddCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    await CoreMethods.PushPageModel<FormB1B2AddPageModel>();
                });
            }
        }

        public  ICommand SelectTakeButton_Clicked
        {
            get
            {
                return new Command(async (obje) =>
                {
                    try
                    {
                        if (SelectedPhotoTypeIndex == -1)
                        {
                            await UserDialogs.Instance.AlertAsync("Please select photograph type", "RAMS", "OK");
                            return;
                        }

                        await CrossMedia.Current.Initialize();

                        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                        {
                            await _userDialogs.AlertAsync(":( No camera available.", "No Camera", "OK");
                            return;
                        }


                        var file = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions { SaveToAlbum = true, Name = "Test", Directory = "FormB1B2", CompressionQuality = 40 });

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

                                await _userDialogs.AlertAsync("Maximum 5Mb files allowed", "RAMMS",  "OK");
                                return;
                            }

                        }

                        listfiles(App.lstImage);
                    }
                    catch (Exception ex) { }
                });
            }
        }

        string phyle;
        public void listfiles(ObservableCollection<string> ImageList)
        {
            try
            {
                var path1 = "/storage/emulated/0/Pictures/FormB1B2/";
                var mp3Files = Directory.EnumerateFiles(path1, "*.jpg", SearchOption.TopDirectoryOnly);

                foreach (string currentFile in mp3Files)
                {
                    phyle = currentFile;
                    if ((_images.Where(x => x == phyle).Count() == 0))
                    {
                        _images.Add(phyle);
                        App.lstImage.Add(phyle);
                    }
                }

                FlowImageList = null;
                FlowImageList = App.lstImage;
            }
            catch (Exception ex)
            {
            }
        }

        public void Deletefiles()
        {
            try
            {
                var path1 = "/storage/emulated/0/Pictures/FormB1B2/";
                var mp3Files = Directory.EnumerateFiles(path1, "*.jpg", SearchOption.AllDirectories);
                foreach (string currentFile in mp3Files)
                {
                    phyle = currentFile;
                    File.Delete(phyle);
                }
            }
            catch (Exception ex)
            {
            }
        }




        public ICommand SelectImagesButton_Clicked
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {

                    try
                    {
                        if (SelectedPhotoTypeIndex == -1)
                        {
                            await _userDialogs.AlertAsync("Please select photograph type ", "RAMS", "OK");
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
                                            FlowImageList = null;
                                            FlowImageList = App.lstImage;
                                        }
                                        catch (Exception ex)
                                        {
                                        }
                                    }
                                });
                            }
                        }
                        else
                        {
                            await _userDialogs.AlertAsync("\nPlease go to your app settings and enable permissions.", "Permission Denied!", "Ok");
                        }
                    }
                    catch (Exception ex) { }
                });
            }
        }

        public ICommand UploadCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    try
                    {
                        if (App.HeaderCode == 0)
                        {
                            await _userDialogs.AlertAsync("Asset value is not found. Please add data and try again.", "RAMMS", "OK");
                            return;
                        }

                        UserDialogs.Instance.ShowLoading("Loading");
                        if (CrossConnectivity.Current.IsConnected)
                        {
                            try
                            {
                                if (App.lstImage.Count == 0)
                                {
                                    await _userDialogs.AlertAsync("Please select an image.", "RAMMS",  "OK");
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

                                    formData.Add(new StringContent(App.HeaderCode.ToString()), "AssetId");
                                    formData.Add(new StringContent(DDPhototypeListItems[SelectedPhotoTypeIndex].Value.ToString()), "PhotoType");

                                    try
                                    {
                                        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", App.AuthToken);

                                        var res1 = await client.PostAsync(AppConst.ImageApiBaseFormB1B2Address, formData);
                                        if (res1.IsSuccessStatusCode)
                                        {
                                            FlowImageList = null;
                                            App.lstImage.Clear();
                                            _images.Clear();

                                            await _userDialogs.AlertAsync("Image Uploaded Successfully.", "RAMS", "OK");

                                           //   await CoreMethods.PopPageModel();
                                            CurrentPage.Navigation.PopAsync();

                                            MessagingCenter.Send<object, string>(this, "Uploaded", "");
                                        }
                                        else
                                        {
                                            await _userDialogs.AlertAsync("Internet Connection Failed. Please check try again.", "RAMMS", "OK");
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
                });
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    FlowImageList = null;

                    App.lstImage.Clear();
                    _images.Clear();
                    await CoreMethods.PopPageModel();
                });
            }
        }

    }
}
