using Acr.UserDialogs;
using FreshMvvm;
using PCLStorage;
using Plugin.Connectivity;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.MobileApps.Helpers;
using Rg.Plugins.Popup.Services;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps
{
    public class DashboardPageModel : FreshBasePageModel
    {
        private IRestApi _restApi;

        public IUserDialogs _userDialogs;

        public bool IsEmpty { get; set; }

        public string myLeaveRequestTitle { get; set; }

        public float LstViewHeightRequest { get; set; }

        private StackLayout _myProfileStack;

        public string SelectedRMU { get; set; }

        public string SelectedSection { get; set; }


        public ObservableCollection<DDListItems> DDDeskRMUListItems { get; set; }

        public ObservableCollection<DDListItems> DDSectionListItems { get; set; }


        public DashboardPageModel(IUserDialogs userDialogs, IRestApi restApi)
        {

            _userDialogs = userDialogs;

            _restApi = restApi;

            DDSectionListItems = new ObservableCollection<DDListItems>();

            DDDeskRMUListItems = new ObservableCollection<DDListItems>();

        }

        public override void Init(object initData)
        {
            base.Init(initData);

            SelectedSection = "Please select on option";

            SubscribeMessageCenter();
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);


            DropdownValues();

        }


        public async void DropdownValues()
        {

            await GetddListDetails("RMU");

            await GetddListDetails("Section Code");

            ExtendedPicker rmucode = CurrentPage.FindByName<ExtendedPicker>("rmupicker");

            rmucode.ItemsSource = DDDeskRMUListItems.Select((DDListItems arg) => arg.Text).ToList();

            // ExtendedPicker sectioncode = CurrentPage.FindByName<ExtendedPicker>("sectionpicker");

            //   sectioncode.ItemsSource = DDSectionListItems.Select((DDListItems arg) => arg.Text).ToList();


        }

        public async Task<ObservableCollection<DDListItems>> GetddListDetails(string ddtype)
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

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

                        if (ddtype == "RMU")
                        {
                            DDDeskRMUListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDDeskRMUListItems;
                        }

                        else if (ddtype == "Section Code")
                        {
                            DDSectionListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDSectionListItems;
                        }

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


        public ICommand RMUDDItemsCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    await PopupNavigation.PushAsync(new SearchBarPopupPage(_restApi, _userDialogs, "Section Code", "Dashboard"));
                });
            }
        }
        public void SubscribeMessageCenter()
        {
            MessagingCenter.Subscribe<DDListItems>(this, "Section CodeDashboard", async (obj) =>
            {
                var args = obj as DDListItems;
                SelectedSection = args.Text;
                RaisePropertyChanged();
            });
        }
    }
}