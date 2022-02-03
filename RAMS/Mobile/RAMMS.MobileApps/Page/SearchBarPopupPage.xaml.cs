using Acr.UserDialogs;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchBarPopupPage : PopupPage
    {
        private IUserDialogs _userDialogs;

        private IRestApi _restApi;

        public SearchBarPopupPageModel _searchBarPopupPageModel;

        public ObservableCollection<DDListItems> GetDDListItems { get; set; }

        public string FilterDropDownItem { get; set; }

        public string DDText { get; set; }

        public bool IsEmpty { get; set; }

        public string DDlblTitle { get; set; }

        public string DDType { get; set; }

        public string DDPageName { get; set; }

        public SearchBarPopupPage(IRestApi restApi, IUserDialogs userDialogs, string dType, string pageName)
        {
            _restApi = restApi;
            _userDialogs = userDialogs;
            DDType = dType;
            DDPageName = pageName;
            _searchBarPopupPageModel = new SearchBarPopupPageModel(restApi, userDialogs, DDType, DDPageName, this);
            BindingContext = _searchBarPopupPageModel;
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            lblTitle.Text = string.Format("{0 } {1}", "Select in", DDType);
            GetDDListItems = await _searchBarPopupPageModel.GetDDListDetails(DDType);
            list.ItemsSource = GetDDListItems;
            list.HeightRequest = GetDDListItems.Count * 45;

            formAsearchBar.TextChanged += SearchBar_TextChanged;
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {

                GetDDListItems = await _searchBarPopupPageModel.GetDDListDetails(DDType);
                list.ItemsSource = GetDDListItems;
            }
            else
            {
                list.ItemsSource = GetDDListItems.Where(x => x.Text.ToLower().Contains(e.NewTextValue.ToLower()) || x.Value.ToLower().Contains(e.NewTextValue.ToLower())).ToList();
            }
        }

        public void ClosePopup()
        {
            PopupNavigation.PopAsync();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<SearchBarPopupPage>(this, string.Format("{0}{1}", DDType, DDPageName));
        }
    }
}