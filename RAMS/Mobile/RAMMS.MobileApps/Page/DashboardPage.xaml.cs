using Acr.UserDialogs;
using Plugin.Connectivity;
using RAMMS.DTO.RequestBO;
using RAMMS.MobileApps.Page;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        private long lastPress;

        private IRestApi _restApi;
        ObservableCollection<DDListItems> DDSectionListItems { get; set; }

        List<string> rmulist = new List<string>();

        int? iRetNODValue { get; set; }
        int? iRetNCNValue { get; set; }
        int? iRetNCRValue { get; set; }

        public string selectSection { get; set; } = null;

        public DashboardPage()
        {
            InitializeComponent();
            DDSectionListItems = new ObservableCollection<DDListItems>();

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var httpClient = new HttpClient(new AuthenticatedHttpClientHandler())
            {
                BaseAddress = new Uri(AppConst.DevApiBaseAddress),

                Timeout = TimeSpan.FromSeconds(60)
            };

            _restApi = Refit.RestService.For<IRestApi>(httpClient);

            GetListCountDetails(rmulist);

            //  base.OnAppearing();

            MessagingCenter.Subscribe<object, string>(this, "Hi", (obj, s) =>
            {
                rmulist.Clear();

                locationpicker.Text = s;

                string[] strVal = s.Split(',');

                foreach(var item in strVal)
                {
                    if (item == "Miri")
                        rmulist.Add("MRI");
                    if (item == "Batu Niah")
                        rmulist.Add("BTN");
                }
                //if (strVal[0].ToString() == "Miri")
                //    strVal[0] = "MRI";
                //if (strVal[1].ToString() == "Batu Niah")
                //    strVal[1] = "BTN";

                //foreach (var strValue in strVal)
                //{

                //    rmulist.Add(strValue);
                //}

                GetddListDetails(rmulist);

            });


            sectionpicker.SelectedIndexChanged += (s, e) =>
            {
                try
                {
                    // SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                    if (sectionpicker.SelectedIndex != -1)
                    {
                        selectSection = DDSectionListItems[sectionpicker.SelectedIndex].Value.ToString();
                        //service call for getting user list
                        //var objUser = GetUserDetilsList("soagreeuser", iCode);
                        GetListCountDetails(rmulist);
                    }

                }
                catch
                {

                }

            };



        }


        public async Task<ObservableCollection<DDListItems>> GetddListDetails(List<string> ddtype)
        {
            try
            {

                UserDialogs.Instance.HideLoading();

                LandingHomeRequestDTO ddlist;

                if (CrossConnectivity.Current.IsConnected)
                {
                    if (ddtype.Count == 1)
                    {
                        ddlist = new LandingHomeRequestDTO()
                        {
                            RMU = new List<string> { ddtype[0].ToString() },

                            Section = null
                        };
                    }
                    else if (ddtype.Count == 2)
                    {
                        ddlist = new LandingHomeRequestDTO()
                        {
                            RMU = new List<string> { ddtype[0].ToString(), ddtype[1].ToString() },

                            Section = null
                        };
                    }
                    else
                    {
                        ddlist = new LandingHomeRequestDTO()
                        {
                            RMU = null, 

                            Section =null
                        };
                    }
                    //var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);

                    var response = await _restApi.GetSectionbyRMU(ddlist);

                    if (response.success)
                    {
                        DDSectionListItems = new ObservableCollection<DDListItems>(response.data);

                        sectionpicker.ItemsSource = DDSectionListItems.Select((DDListItems arg) => arg.Text).ToList();

                        var response1 = await _restApi.GetNodClosedResult(ddlist);

                        if (response1.success)
                        {
                            lblNOD.Text = response1.data.LandingNodCount.ToString();
                            lblNCN.Text = response1.data.LandingNcnCount.ToString();
                            lblNCR.Text = response1.data.LandingNcrCount.ToString();
                        }
                        else
                            await DisplayAlert("RAMS", "Unable to connect please check your internet connection!", "OK");

                        return DDSectionListItems;
                    }
                    else
                        await DisplayAlert("RAMS", "Unable to connect please check your internet connection!", "OK");

                    

                }
                else
                    await DisplayAlert("RAMS", "Unable to connect please check your internet connection!", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("RAMS", ex.Message, "OK");
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }


        public async Task<ObservableCollection<DDListItems>> GetListCountDetails(List<string> ddtype1)
        {
            try
            {

                UserDialogs.Instance.HideLoading();

                LandingHomeRequestDTO ddlist1;

                if (CrossConnectivity.Current.IsConnected)
                {
                    if (ddtype1.Count == 1)
                    {
                        ddlist1 = new LandingHomeRequestDTO()
                        {
                            RMU = new List<string> { ddtype1[0].ToString() },

                            Section = selectSection
                        };
                    }
                    else if(ddtype1.Count ==2)
                    {
                        ddlist1 = new LandingHomeRequestDTO()
                        {
                            RMU = new List<string> { ddtype1[0].ToString(), ddtype1[1].ToString() },

                            Section = selectSection
                        };
                    }
                    else
                    {
                        ddlist1 = new LandingHomeRequestDTO()
                        {
                            RMU = null ,

                            Section =  selectSection
                        };
                    }

                    //var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);

                    var response = await _restApi.GetNodClosedResult(ddlist1);

                    if (response.success)
                    {
                        lblNOD.Text = response.data.LandingNodCount.ToString();
                        lblNCN.Text = response.data.LandingNcnCount.ToString();
                        lblNCR.Text = response.data.LandingNcrCount.ToString();
                    }
                    else
                        await DisplayAlert("RAMS", "Unable to connect please check your internet connection!", "OK");

                }
                else
                    await DisplayAlert("RAMS","Unable to connect please check your internet connection!","OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("RAMS", ex.Message, "OK");
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }



        protected override bool OnBackButtonPressed()
        {
            long currentTime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;

            if (currentTime - lastPress > 2000)
            {
                UserDialogs.Instance.Toast("Press back again to exit");
                lastPress = currentTime;
                return true;
            }
            else
            {
                if (Device.RuntimePlatform == Device.Android)
                    DependencyService.Get<ICloseApplication>().closeApplication();
                return base.OnBackButtonPressed();
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RoadConditionMapping());
        }















        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (Toggle.IsVisible)
            {
                Toggle.IsVisible = false;
            }
            else
            {
                Toggle.IsVisible = true;
            }
            Image image = sender as Image;
            string source = image.Source as FileImageSource;  //Getting the name of source as string
            if (String.Equals(source, "RoundedAddIcon.png"))
            {
                image.Source = "minusicon.png";
            }
            else
            {
                image.Source = "RoundedAddIcon.png";
            }


        }


        private void FormDTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (Toggle.IsVisible)
            {
                Toggle.IsVisible = false;
            }
            else
            {
                Toggle.IsVisible = true;
            }
        }



        private void FormSTapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (Toggle12.IsVisible)
            {
                Toggle12.IsVisible = false;
            }
            else
            {
                Toggle12.IsVisible = true;
            }
        }


        private void TapGestureRecognizer_Tapped12(object sender, EventArgs e)
        {
            if (Toggle12.IsVisible)
            {
                Toggle12.IsVisible = false;
            }
            else
            {
                Toggle12.IsVisible = true;
            }

            Image image = sender as Image;
            string source = image.Source as FileImageSource;  //Getting the name of source as string
            if (String.Equals(source, "RoundedAddIcon.png"))
            {
                image.Source = "minusicon.png";
            }
            else
            {
                image.Source = "RoundedAddIcon.png";
            }

        }




        private void TapGestureRecognizer_Tapped1(object sender, EventArgs e)
        {
            if (Toggle1.IsVisible)
            {
                Toggle1.IsVisible = false;
            }
            else
            {
                Toggle1.IsVisible = true;
            }

            Image image = sender as Image;
            string source = image.Source as FileImageSource;  //Getting the name of source as string
            if (String.Equals(source, "RoundedAddIcon.png"))
            {
                image.Source = "minusicon.png";
            }
            else
            {
                image.Source = "RoundedAddIcon.png";
            }

        }

        private void LabourTapGestureRecognizer_Tapped1(object sender, EventArgs e)
        {

            if (Toggle1.IsVisible)
            {
                Toggle1.IsVisible = false;
            }
            else
            {
                Toggle1.IsVisible = true;
            }

        }





        private async void Entry_Focused(object sender, FocusEventArgs e)
        {
            await Navigation.PushPopupAsync(new RMUDashboard_Popup());
        }



        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BatuniahRoadConditionMappingPage());
        }




    }
}