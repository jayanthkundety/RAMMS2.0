using RAMMS.MobileApps.Page.BatuNiahMap;
using RAMMS.MobileApps.Page.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BatuniahRoadConditionMappingPage : ContentPage
    {
        public BatuniahRoadConditionMappingPage()
        {
            InitializeComponent();
            BatuNamesListView.ItemsSource = batununames;

        }






        //Bekenu  Search


        List<string> batununames = new List<string>
        {
            "BATU NIAH FEEDER ROAD	                                                                                                                     Q283",
            "DATUK SIM KHENG HONG ROAD	                                                                                                           Q284",
            "JKR MRCU 10 ROAD	                                                                                                                               Q644",
            "KAMPUNG BALIPAT ROAD	                                                                                                                     Q647",
            "KARABUNGAN SCHEME ROAD	                                                                                                               Q1054",
             "KPG. PADANG ROAD	                                                                                                                              Q1056",
            "KUBUR SEPUPOK ROAD - 0.52 UNDER CONSTRUCTION	                                                                   Q1084",
            "LADANG KOKO VILLAGE ROAD	                                                                                                              Q1053",
            "LADANG TIGA PALM MILL ROAD	                                                                                                           Q1055",
             "NIAH - SEPUPOK ROAD - 0.35 UNDER CONSTRUCTION                                                                       Q291",
              "NIAH / SUAI FEEDER ROAD - 11.2KM UNDER CONSTRUCTION	                                                         Q282",
            "RH. IBAI ROAD	                                                                                                                                        Q1085",
            "SEKALOH ROAD	                                                                                                                                     Q646",
            "SEPUPOK BAZAAR ROAD	                                                                                                                      Q648",
             "SUAI FEEDER ROAD                                                                                                                                 Q278-1",
              "SUAI FEEDER ROAD	                                                                                                                               Q278-2",
            "SUAI NO. 1 SCHEME ROAD	                                                                                                                      Q1048",
            "SUBIS 2 & 3 SCHEME ROAD	                                                                                                                  Q1052",
            "TELABIT ROAD	                                                                                                                                          Q279",
             "TG. KIDURONG/KUALA SUAI/KUALA NIAH/ KUALA SIBUTI/BAKAM ROAD (KM65+930 TO KM78+200)                                             Q2007",
                        "TKSB Road (KM78+200 to KM113+470)                                                                                              Q2007-2b",
            "ULU NIAH FEEDER ROAD	                                                                                                                       Q281"


        };


        //private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        //{
        //    var keyword = MainSearchBar.Text;
        //    NamesListView.ItemsSource = names.Where(name => name.ToLower().Contains(keyword.ToLower())); 
        //}


        private void BatuMainSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = BatuMainSearchBar.Text;
            BatuNamesListView.ItemsSource = batununames.Where(name => name.ToLower().Contains(keyword.ToLower()));
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MarudiPage());
        }

        private void Q1084_Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new BekenuPage());
        }




        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var obj = ((Button)sender).AutomationId;
            Navigation.PushAsync(new BatuniahMapSection(obj));
        }




    }
}