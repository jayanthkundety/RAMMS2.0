using RAMMS.MobileApps.Page.Maps;
using RAMMS.MobileApps.Page.MiriMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoadConditionMapping : ContentPage
    {
        //MiriPage Search

        List<string> names = new List<string>
        {
            "BRIGHTON JUNIOR SERVICE QTRS ROAD	                                                                                          Q651",
            "CUSTOM QUARTERS ACCESS ROAD	                                                                                                    Q681",
            "FEDERAL COMPLEX ACCESS ROAD	                                                                                                     Q1903",
            "FEDERAL QUARTERS ACCESS ROAD	                                                                                                      Q683",
            "GOVERNMENT QUARTERS ACCESS ROAD LORONG 10	                                                                        Q667",
             "JALAN JAMBATAN ASIAN	                                                                                                                      Q3188",
            "KAMPUNG LUAK BARU RURAL ROAD	                                                                                                       Q652",
            "KAMPUNG LUSUT MAIN ROAD	                                                                                                               Q3074",
            "KAMPUNG LUSUT ROAD A (KPG. LUSUT JAYA NO. 3)	                                                                         Q3075",
            "KAMPUNG LUSUT ROAD A1 (KPG. LUSUT JAYA NO. 1)	                                                                         Q3076",
             "KAMPUNG LUSUT ROAD B (KPG. LUSUT JAYA NO. 2)	                                                                         Q3077",
            "KAMPUNG LUSUT ROAD C (KPG. PERPADUAN)	                                                                                  Q3078",
            "KAMPUNG LUSUT ROAD D (KPG LUSUT JAYA NO. 5)	                                                                           Q3079",
            "KAMPUNG LUSUT ROAD D1 (KPG BIDAYUH)	                                                                                       Q3080",
            "KAMPUNG PENGKALAN LUTONG ACCESS ROAD    	                                                                         Q661",
             "LAMBIR VILLAGE SPUR ROAD 3	                                                                                                             Q656",
            "LOPENG RESERVOIR RD	                                                                                                                         Q658",
            "MIRI AIRPORT ACCESSROAD	                                                                                                                   Q653",
            "MIRI AIRPORT ACCESSROAD	                                                                                                                  Q653-1",
            "MIRI BY PASS ROAD	                                                                                                                               Q1902",
             "NEW HOSPITAL ROAD	                                                                                                                           Q353",
            "NEW HOSPITAL/MIRI BY PASS LINK ROAD	                                                                                           Q1901",
            "OLD BAKAM ROAD NO. 1	                                                                                                                        Q3008",
            "OLD BAKAM ROAD NO. 2	                                                                                                                       Q3009",
            "OLD KUALA BARAM FERRY ACCESS ROAD	                                                                                       Q320",
             "OLD RIAM ROAD                                                                                                                                      Q290",
            "PADANG KERBAU ROAD	                                                                                                                        Q645",
            "PENINSULA ROAD - 1.7KM UNDER MARINE PARK CONSTRUCTION	                                                  Q664",
            "PIASAU INDUSTRIAL AREA ROAD	                                                                                                        Q650",
            "PRISON CAMP ACCESS ROAD	                                                                                                               Q657",
             "PUJUT / TUDAN / KUALA BARAM LINK ROAD : NEW	                                                                        Q1231",
            "PUJUT / TUDAN / KUALA BARAM LINK ROAD A	                                                                                   Q1231A",
            "PUJUT / TUDAN / KUALA BARAM LINK ROAD B	                                                                                   Q1231B",
            "RH. AMBAU ROAD	                                                                                                                                   Q675",
            "RIAM / HOSPITAL BARU LINK ROAD	                                                                                                       Q3073",
             "SG. RAIT ROAD	                                                                                                                                         Q288",
            "TANJUNG ROAD	                                                                                                                                      Q292",
            "TG. KIDURONG/KUALA SUAI/KUALA NIAH/KUALA SIBUTI/BAKAM ROAD (KM174+570 TO KM178+130)	                                                                                     Q2007-2d (A)",
            "TG. KIDURONG/KUALA SUAI/KUALA NIAH/KUALA SIBUTI/BAKAM ROAD (KM178+130 TO KM174+570)	                                                                        Q2007-2d (B)",
            "WOMEN & GIRLS PROTECTION HOME ACCESS ROAD                                                                         	Q655"
             
        };
        public RoadConditionMapping()
        {
            InitializeComponent();
            NamesListView.ItemsSource = names;
            BarioNamesListView.ItemsSource = barionames;
            BakongNamesListView.ItemsSource = bakongnames;
            MarudiNamesListView.ItemsSource = marudinames;
            BekenuNamesListView.ItemsSource = bekenunames;

        }

        //private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        //{
        //    var keyword = MainSearchBar.Text;
        //    NamesListView.ItemsSource = names.Where(name => name.ToLower().Contains(keyword.ToLower())); 
        //}


        private void MainSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = MainSearchBar.Text;
            NamesListView.ItemsSource = names.Where(name => name.ToLower().Contains(keyword.ToLower()));
        }






        //Bario Mullu Search


        List<string> barionames = new List<string>
        {
            "BARIO AIRPORT ROAD	                                                                                                                          Q1275",
            "MULU RESORT ROAD	                                                                                                                             Q733",
            "PA RAMAPOH ROAD	                                                                                                                            Q1279",
            "PA UMOR ROAD	                                                                                                                                   Q1278",
            "PENGHULU LAWAI ROAD, BARIO	                                                                                                         Q1086",
             "SK/SMK ULONG PALANG ROAD A	                                                                                                      Q1276",
            "SK/SMK ULONG PALANG ROAD B	                                                                                                      Q1277"


        };
       

        //private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        //{
        //    var keyword = MainSearchBar.Text;
        //    NamesListView.ItemsSource = names.Where(name => name.ToLower().Contains(keyword.ToLower())); 
        //}


        private void BarioMainSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = BarioMainSearchBar.Text;
            BarioNamesListView.ItemsSource = barionames.Where(name => name.ToLower().Contains(keyword.ToLower()));
        }















        //Bakong  Search


        List<string> bakongnames = new List<string>
        {
            "BELURU FEEDER ROAD JUNCTION - SG. BAKONG	                                                                              Q285",
            "BELURU FEEDER ROAD JUNCTION - SG. BAKONG	                                                                              Q21-1",
            "BELURU/LONG TERU ROAD (SG. LANGSAT-RIVER BANK LONG LAMA)	                                             Q21-4",
            "BELURU/LONG TERU TRUNK ROAD	                                                                                                         Q21",
            "BKT. PENINJAU ROAD	                                                                                                                             Q286",
             "KPG. BELURU ROAD, BELURU	                                                                                                                   Q682",
            "RH. JOHN, SELEPIN, BAKONG	                                                                                                                Q732",
            "SELEPIN ROAD, BAKONG	                                                                                                                        Q693",
            "SG LAUNG, BAKONG	                                                                                                                               Q3186",
             "SG. BAKONG - TINJAR BRIDGE                                                                                                              Q21-2",
            "TINJAR BRIDGE - SG. LANGSAT BRIDGE	                                                                                             Q21-3"


        };


        //private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        //{
        //    var keyword = MainSearchBar.Text;
        //    NamesListView.ItemsSource = names.Where(name => name.ToLower().Contains(keyword.ToLower())); 
        //}


        private void BakongMainSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = BakongMainSearchBar.Text;
            BakongNamesListView.ItemsSource = bakongnames.Where(name => name.ToLower().Contains(keyword.ToLower()));
        }











        //Marudi  Search


        List<string> marudinames = new List<string>
        {
            "ACCESS ROAD TO GOVT. QUARTERS	                                                                                                     Q670",
            "GUDANG ROAD	                                                                                                                                         Q672",
            "JALAN MIRI/MARUDI	                                                                                                                                Q3187",
            "KAMPUNG NARUM ROAD	                                                                                                                      Q669",
            "KAMPUNG PASIR ROAD	                                                                                                                          Q676",
             "LUBOK NIBONG PENINSULA RD	                                                                                                             Q649",
            "LUBOK NIBONG ROAD	                                                                                                                              Q643",
            "MARUDI / ULU LINEI ROAD	                                                                                                                        Q289",
            "RH AMBAU ROAD	                                                                                                                                     Q691",
             "SELIJAU SCHEME ROAD                                                                                                                             Q1904",
            "SG. BRIT/LOGAN ENTASAN ROAD	                                                                                                           Q674"


        };


        //private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        //{
        //    var keyword = MainSearchBar.Text;
        //    NamesListView.ItemsSource = names.Where(name => name.ToLower().Contains(keyword.ToLower())); 
        //}


        private void MarudiMainSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = MarudiMainSearchBar.Text;
            MarudiNamesListView.ItemsSource = marudinames.Where(name => name.ToLower().Contains(keyword.ToLower()));
        }












        //Bekenu  Search


        List<string> bekenunames = new List<string>
        {
            "ANGUS / TUSAN ROAD - 5.8KM UNDER CONSTRUCTION	                                                                  Q638",
            "BEKENU FEEDER ROAD	                                                                                                                           Q287",
            "ENTUALANG ROAD	                                                                                                                                  Q641",
            "GATAS ROAD, SIBUTI	                                                                                                                              Q696",
            "JANGALAS / MUMONG / BUNGAI ROAD -1.2KM UNDER CONSTRUCTION	                                    Q637",
             "KAMPUNG KEPAYANG/TIRIS ROAD	                                                                                                    Q1297",
            "KAMPUNG LUBOK BEUNEH ROAD	                                                                                                       Q1294",
            "KAMPUNG RANCHAH-RANCHAH ROAD	                                                                                             Q1295",
            "KAMPUNG SEBALI ROAD	                                                                                                                       Q1296",
             "KAMPUNG TENGAH ROAD                                                                                                                     Q636",

              "KEBULOH AGRICULTURE TRAINING CENTRE ROAD	                                                                           Q756",
            "KEJAPIL / BERAYA / BAKAM ROAD	                                                                                                      Q354",
            "KELURU TENGAH ROAD	                                                                                                                       Q684",
            "KPG BEKENU ASLI ROAD	                                                                                                                      Q1114",
             "KPG HUNAI ROAD                                                                                                                                   Q1117",
              "KPG JANGALAS ROADS (BEKENU)	                                                                                                      Q1953",
            "KPG MEMONG ROAD	                                                                                                                             Q1116",
            "KPG TERHAD ROAD	                                                                                                                            Q1115",
            "KPG. MENJELIN ROAD	                                                                                                                         Q640",
             "PAYA SELANYAU ROAD                                                                                                                         Q685",


             "PELIAU ROAD, SIBUTI	                                                                                                                         Q697",
            "RH. AJAI, SG. LIAM/RH. BUNDAN ROAD	                                                                                           Q695",
            "RH. MENGGONG ROAD, SIBUTI	                                                                                                            Q694",
            "RH. MUNGKIN GUDANG ATAS ROAD, SIBUTI	                                                                                    Q692",
             "RUMAH ANTAU, SELOI, SEBUTI ROAD                                                                                                  Q757",
               "TG. KIDURONG/KUALA SUAI/KUALA NIAH/ KUALA SIBUTI/BAKAM ROAD (KM65+930 TO KM78+200)                                     	Q2007",



            "TG. KIDURONG/KUALA SUAI/KUALA NIAH/KUALA SIBUTI/BAKAM ROAD (KM113+470 TO KM174+570)	          Q2007-2c"


        };


        //private void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        //{
        //    var keyword = MainSearchBar.Text;
        //    NamesListView.ItemsSource = names.Where(name => name.ToLower().Contains(keyword.ToLower())); 
        //}


        private void BekenuMainSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var keyword = BekenuMainSearchBar.Text;
            BekenuNamesListView.ItemsSource = bekenunames.Where(name => name.ToLower().Contains(keyword.ToLower()));
        }






















































































































































        private void Miri_Clicked_2(object sender, EventArgs e)
        {
            {
                Button btn = (Button)sender;
                if (btn.BackgroundColor.Equals(Color.Transparent))
                    //btn.BackgroundColor = Color.FromHex("#d3dade");
                    btn.BackgroundColor = Color.Transparent;

                else
                    btn.BackgroundColor = Color.Transparent;

            }

            miri.IsVisible = false;
            bario.IsVisible = true;
            bakong.IsVisible = true;
            marudi.IsVisible = true;
            bekenu.IsVisible = true;


            mirii.IsVisible = false;
            barioo.IsVisible = true;
            bakongg.IsVisible = true;
            marudii.IsVisible = true;
            bekenuu.IsVisible = true;
























            var button = (Button)sender;


            if (Stack1.IsVisible)
            {
                Stack1.IsVisible = true;
                Stack2.IsVisible = false;
                Stack3.IsVisible = false;
                Stack4.IsVisible = false;
                Stack5.IsVisible = false;




            }
            else
            {
                Stack1.IsVisible = true;
                Stack2.IsVisible = false;
                Stack3.IsVisible = false;
                Stack4.IsVisible = false;
                Stack5.IsVisible = false;


            }









        }
        private void BarioButton_Clicked(object sender, EventArgs e)
        {
            {
                Button btn = (Button)sender;
                if (btn.BackgroundColor.Equals(Color.Transparent))
                    //btn.BackgroundColor = Color.FromHex("#d3dade");
                    btn.BackgroundColor = Color.Transparent;

                else
                    btn.BackgroundColor = Color.Transparent;



                //Navigation.PushModalAsync(new MyProfile_Page());


            }

            miri.IsVisible = true;
            bario.IsVisible = false;
            bakong.IsVisible = true;
            marudi.IsVisible = true;
            bekenu.IsVisible = true;



            mirii.IsVisible = true;
            barioo.IsVisible = false;
            bakongg.IsVisible = true;
            marudii.IsVisible = true;
            bekenuu.IsVisible = true;



























            var button = (Button)sender;


            if (Stack2.IsVisible)
            {
                Stack1.IsVisible = false;
                Stack2.IsVisible = true;
                Stack3.IsVisible = false;
                Stack4.IsVisible = false;
                Stack5.IsVisible = false;






            }
            else
            {
                Stack1.IsVisible = false;
                Stack2.IsVisible = true;
                Stack3.IsVisible = false;
                Stack4.IsVisible = false;
                Stack5.IsVisible = false;


            }







        }

        private void BakongButton_Clicked_1(object sender, EventArgs e)
        {
            {
                Button btn = (Button)sender;
                if (btn.BackgroundColor.Equals(Color.Transparent))
                    //btn.BackgroundColor = Color.FromHex("#d3dade");
                    btn.BackgroundColor = Color.Transparent;

                else
                    btn.BackgroundColor = Color.Transparent;

            }

            miri.IsVisible = true;
            bario.IsVisible = true;
            bakong.IsVisible = false;
            marudi.IsVisible = true;
            bekenu.IsVisible = true;

            mirii.IsVisible = true;
            barioo.IsVisible = true;
            bakongg.IsVisible = false;
            marudii.IsVisible = true;
            bekenuu.IsVisible = true;




























            var button = (Button)sender;


            if (Stack3.IsVisible)
            {
                Stack1.IsVisible = false;
                Stack2.IsVisible = false;
                Stack3.IsVisible = true;
                Stack4.IsVisible = false;
                Stack5.IsVisible = false;






            }
            else
            {
                Stack1.IsVisible = false;
                Stack2.IsVisible = false;
                Stack3.IsVisible = true;
                Stack4.IsVisible = false;
                Stack5.IsVisible = false;


            }





        }

        private void MarudiButton_Clicked_2(object sender, EventArgs e)
        {
            {
                Button btn = (Button)sender;
                if (btn.BackgroundColor.Equals(Color.Transparent))
                    //btn.BackgroundColor = Color.FromHex("#d3dade");
                    btn.BackgroundColor = Color.Transparent;

                else
                    btn.BackgroundColor = Color.Transparent;

            }

            miri.IsVisible = true;
            bario.IsVisible = true;
            bakong.IsVisible = true;
            marudi.IsVisible = false;
            bekenu.IsVisible = true;

            mirii.IsVisible = true;
            barioo.IsVisible = true;
            bakongg.IsVisible = true;
            marudii.IsVisible = false;
            bekenuu.IsVisible = true;




















            var button = (Button)sender;


            if (Stack4.IsVisible)
            {
                Stack1.IsVisible = false;
                Stack2.IsVisible = false;
                Stack3.IsVisible = false;
                Stack4.IsVisible = true;
                Stack5.IsVisible = false;






            }
            else
            {
                Stack1.IsVisible = false;
                Stack2.IsVisible = false;
                Stack3.IsVisible = false;
                Stack4.IsVisible = true;
                Stack5.IsVisible = false;


            }











        }
        private void BekenuButton_Clicked_3(object sender, EventArgs e)
        {
            {
                Button btn = (Button)sender;
                if (btn.BackgroundColor.Equals(Color.Transparent))
                    //btn.BackgroundColor = Color.FromHex("#d3dade");
                    btn.BackgroundColor = Color.Transparent;

                else
                    btn.BackgroundColor = Color.Transparent;

            }

            miri.IsVisible = true;
            bario.IsVisible = true;
            bakong.IsVisible = true;
            marudi.IsVisible = true;
            bekenu.IsVisible = false;

            mirii.IsVisible = true;
            barioo.IsVisible = true;
            bakongg.IsVisible = true;
            marudii.IsVisible = true;
            bekenuu.IsVisible = false;





























            var button = (Button)sender;


            if (Stack5.IsVisible)
            {
                Stack1.IsVisible = false;
                Stack2.IsVisible = false;
                Stack3.IsVisible = false;
                Stack4.IsVisible = false;
                Stack5.IsVisible = true;






            }
            else
            {
                Stack1.IsVisible = false;
                Stack2.IsVisible = false;
                Stack3.IsVisible = false;
                Stack4.IsVisible = false;
                Stack5.IsVisible = true;


            }









        }



        private void Miris(object sender, EventArgs e)
        {

            var button = (Button)sender;
            

            if (Stack1.IsVisible)
            {
                Stack1.IsVisible = true;
                Stack2.IsVisible = false;
                Stack3.IsVisible = false;
                Stack4.IsVisible = false;
                Stack5.IsVisible = false;
               
             
                
          
            }
            else
            {
                Stack1.IsVisible = true;
                Stack2.IsVisible = false;
                Stack3.IsVisible = false;
                Stack4.IsVisible = false;
                Stack5.IsVisible = false;


            }

        }
        private void Barios(object sender, EventArgs e)
        {
            var button = (Button)sender;


            if (Stack2.IsVisible)
            {
                Stack1.IsVisible = false;
                Stack2.IsVisible = true;
                Stack3.IsVisible = false;
                Stack4.IsVisible = false;
                Stack5.IsVisible = false;
               
                
               
               


            }
            else
            {
                Stack1.IsVisible =false;
                Stack2.IsVisible = true;
                Stack3.IsVisible = false;
                Stack4.IsVisible = false;
                Stack5.IsVisible = false;


            }
        }
        private void Bakongs(object sender, EventArgs e)
        {
            var button = (Button)sender;


            if (Stack3.IsVisible)
            {
                Stack1.IsVisible = false;
                Stack2.IsVisible = false;
                Stack3.IsVisible = true;
                Stack4.IsVisible = false;
                Stack5.IsVisible = false;
                
                
               
               


            }
            else
            {
                Stack1.IsVisible = false;
                Stack2.IsVisible = false;
                Stack3.IsVisible = true;
                Stack4.IsVisible = false;
                Stack5.IsVisible = false;


            }
        }
        private void Marudis(object sender, EventArgs e)
        {
            var button = (Button)sender;


            if (Stack4.IsVisible)
            {
                Stack1.IsVisible = false;
                Stack2.IsVisible = false;
                Stack3.IsVisible = false;
                Stack4.IsVisible = true;
                Stack5.IsVisible = false;
               
                
               
               


            }
            else
            {
                Stack1.IsVisible = false;
                Stack2.IsVisible = false;
                Stack3.IsVisible = false;
                Stack4.IsVisible = true;
                Stack5.IsVisible = false;


            }
        }
        private void Bekenus(object sender, EventArgs e)
        {
            var button = (Button)sender;


            if (Stack5.IsVisible)
            {
                Stack1.IsVisible = false;
                Stack2.IsVisible = false;
                Stack3.IsVisible = false;
                Stack4.IsVisible = false;
                Stack5.IsVisible = true;
               
                
                
                


            }
            else
            {
                Stack1.IsVisible = false;
                Stack2.IsVisible = false;
                Stack3.IsVisible = false;
                Stack4.IsVisible = false;
                Stack5.IsVisible = true;


            }
        }

        private void Button_Clicked (object sender, EventArgs e)
        {
            var obj = ((Button)sender).AutomationId;
            Navigation.PushAsync(new MiriMapPage(obj));
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var obj = ((Button)sender).AutomationId;
            Navigation.PushAsync(new BakongMapPage(obj));
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            var obj = ((Button)sender).AutomationId;
            Navigation.PushAsync(new MarudiMapPage(obj));
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            var obj = ((Button)sender).AutomationId;
            Navigation.PushAsync(new BekenuMapPage(obj));
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {
            var obj = ((Button)sender).AutomationId;
            Navigation.PushAsync(new BarioMapPage(obj));
        }
    }
}