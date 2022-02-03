using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;
using RAMMS.DTO.ResponseBO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace RAMMS.MobileApps.PageModel
{
    class FormXUSeeUPageModel : FreshBasePageModel
    {

        private IRestApi _restApi;

        public IUserDialogs _userDialogs;

        public ILocalDatabase _localDatabase;

        private EditViewModel editViewModel;

        public ObservableCollection<AccUccImageDtlResponseDTO> USeeUImagesList { get; set; }

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

        public FormXUSeeUPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
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
            GetUseeUImageList();
        }

        private async void GetUseeUImageList()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                   
                    var response = await _restApi.GetFormXUseeUImageList(editViewModel.HdrFahPkRefNo);
                    if (response.success)
                    {
                        USeeUImagesList = null;
                        USeeUImagesList = new ObservableCollection<AccUccImageDtlResponseDTO>(response.data);
                    }
              
                
                
                
                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");

            }
            catch(Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }
        }
    }
}
