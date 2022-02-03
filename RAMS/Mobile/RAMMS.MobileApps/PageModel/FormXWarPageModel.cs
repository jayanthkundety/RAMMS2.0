using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps.PageModel
{
    public class FormXWarPageModel : FreshBasePageModel
    {
        private IRestApi _restApi;

        public IUserDialogs _userDialogs;

        public ILocalDatabase _localDatabase;

        private EditViewModel editViewModel;

        public ObservableCollection<WarImageDtlResponseDTO> WarImagesList { get; set; }

        public ICommand CloseCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    await CurrentPage.Navigation.PopAsync();
                });
            }
        }

        public FormXWarPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;

            _restApi = restApi;

            _localDatabase = localDatabase;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            editViewModel = initData as EditViewModel;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            GetImageWarImageList();
        }

        private async void GetImageWarImageList()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.GetFormXWarImageList(editViewModel.HdrFahPkRefNo);
                    if (response.success)
                    {
                        WarImagesList = new ObservableCollection<WarImageDtlResponseDTO>(response.data);

                        //foreach (var listdata in WarImagesList)
                        //{
                        //    string Path = listdata.ImageFilenameUpload.Replace("Uploads/", "");
                        //    //ActivityIndicator indicator = new ActivityIndicator() { BindingContext = image };
                        //    //indicator.SetBinding(ActivityIndicator.IsRunningProperty, new Binding("IsLoading", source: image));
                        //    //indicator.SetBinding(ActivityIndicator.IsVisibleProperty, new Binding("IsLoading", source: image));
                        //    image = new Image
                        //    {
                        //        Source = ImageSource.FromUri(new Uri(AppConst.ImageApiGetFormDDownloadAddress + Path))

                        //    };

                        //    label = new Label
                        //    {
                        //        Text = listdata.ImageFilenameSys,
                        //        HorizontalTextAlignment = TextAlignment.Center

                        //    };
                        //    Grid.SetColumn(indicator, i);
                        //    Grid.SetColumn(image, i);
                        //    Grid.SetRow(label, 1);
                        //    Grid.SetColumn(label, i);
                        //    MainGrid.Children.Add(indicator);
                        //    MainGrid.Children.Add(image);
                        //    MainGrid.Children.Add(label);
                        //    i = i + 1;
                        //}
                    }
                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");

            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }
        }
    }

    public class ImageViewModel
    {
        public ImageSource ImageSrc { get; set; }

        public string LabelText { get; set; }
    }
}
