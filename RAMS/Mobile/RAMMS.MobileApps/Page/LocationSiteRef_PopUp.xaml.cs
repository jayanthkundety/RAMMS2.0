using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationSiteRef_PopUp : PopupPage
    {
        ObservableCollection<DDListItems> MultiSelectList = new ObservableCollection<DDListItems>();
        public LocationSiteRef_PopUp(ObservableCollection<DDListItems> dDLocationListItems)
        {
            InitializeComponent();
            this.BackgroundColor = Color.White;
            listView.ItemsSource = dDLocationListItems;
        }

        private async void Button_Clicked (object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
            MultiSelectList = (ObservableCollection<DDListItems>)listView.ItemsSource;
            MessagingCenter.Send<object, ObservableCollection<DDListItems>>(this, "IsChecked", MultiSelectList);
        }
    }
}