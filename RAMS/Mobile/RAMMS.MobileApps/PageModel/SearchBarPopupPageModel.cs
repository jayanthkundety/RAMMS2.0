using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps
{
    public class SearchBarPopupPageModel : FreshBasePageModel
    {
        public IRestApi _restApi;

        public IUserDialogs _userDialogs;

        public SearchBarPopupPage _searchBarPopupPage;

        public ObservableCollection<DDListItems> ddListItems { get; set; }

        public DDListItems SelectedDDList { get; set; }

        public string DdList { get; set; }

        public string DDPageName { get; set; }

        public SearchBarPopupPageModel(IRestApi restApi, IUserDialogs userDialogs, string DDType, string pageName, SearchBarPopupPage searchBarPopupPage)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;
            _searchBarPopupPage = searchBarPopupPage;
            DdList = DDType;
            DDPageName = pageName;
        }

        public ICommand ddPopupCloseCommand
        {
            get
            {
                return new Command(async (obj) =>
                {

                     PopupNavigation.PopAsync();
                });
            }
        }

        public ICommand ItemTappedCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    SelectedDDList = (DDListItems)obj;

                    MessagingCenter.Send<DDListItems>(SelectedDDList, string.Format("{0}{1}", DdList, DDPageName));

                   await PopupNavigation.PopAsync();
                });
            }
        }

        public async Task<ObservableCollection<DDListItems>> GetDDListDetails(string ddtype)
        {
            try
            {
                //  _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {

                    var ddlist = new DDLookUpDTO()
                    {
                        Type = ddtype,
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);

                    var response = await _restApi.GetDDList(ddlist);

                    if (response.success)
                    {

                        ddListItems = new ObservableCollection<DDListItems>(response.data);

                        return ddListItems;


                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

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
            return new ObservableCollection<DDListItems>();
        }
    }
}