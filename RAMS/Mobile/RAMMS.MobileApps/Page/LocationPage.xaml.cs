using System;
using System.Collections.Generic;
using System.Linq;
using RAMMS.MobileApps.PageModel;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace RAMMS.MobileApps.Page
{
    public partial class LocationPage : PopupPage
    {
        IEnumerable<JAssetDropDownData> _jAssetDropDownDatas;
        List<JAssetDropDownData> selectedDropDowndata;
        public printString printStringdelegate;
        public LocationPage(IList<JAssetDropDownData> jAssetDropDownData)
        {
            InitializeComponent();
            _jAssetDropDownDatas = jAssetDropDownData;
            selectedDropDowndata = new List<JAssetDropDownData>();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            listView.ItemsSource = _jAssetDropDownDatas;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddNew");
            selectedDropDowndata = ((List<JAssetDropDownData>)listView.ItemsSource).Where(x => x.isSelected == true).ToList();
            printStringdelegate?.Invoke(selectedDropDowndata);
            await Navigation.PopPopupAsync();
        }
    }
}
