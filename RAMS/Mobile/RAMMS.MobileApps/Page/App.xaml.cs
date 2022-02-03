using Acr.UserDialogs;
using FreshMvvm;
using RAMMS.MobileApps.Page;
using RAMMS.MobileApps.PageModel;
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
    public partial class App : Application
    {
        private IRestApi _restApi;

        

        private ILocalDatabase _localDatabase;

        public static double ScreenHeight;

        public static double ScreenWidth;

        public static string IReferenceNo;
        public static string InspReferenceNo;

        public static string DetailType;

        public static string ReturnType;

        public static int DetailHeaderCode;

        public static int HeaderCode;

        public static string inspSign;

        public static string versign;
        public static string PhotoType;

        public static string AuthToken;

        public static int FormDHeaderCode;

        public static int FormDDetailCode;

        public static string srecsignview;
        public static string svetsignview;
        public static string sversignview;
        public static string ssoversignview;
        public static string ssoprosignview;
        public static string ssoagreesignview;

        public static string AssetGroupSelection;

        public static bool ViewState;

        public static bool SubmitViewState;


        public static ObservableCollection<string> lstImage = new ObservableCollection<string>();
        public static List<ImageSource> lstsImage = new List<ImageSource>();

        public App()
        {
            InitializeComponent();

            SetUpIOC();

            SetupDatabase();

            SetUpMainPage();
        }

        private void SetupDatabase()
        {
            LocalDataSetup.CreateTables(FreshIOC.Container.Resolve<ISQLiteFactory>());
        }

        private void SetUpIOC()
        {
            try
            {
                var httpClient = new HttpClient(new AuthenticatedHttpClientHandler())
                {
                    BaseAddress = new Uri(AppConst.DevApiBaseAddress),
                    Timeout = TimeSpan.FromSeconds(60)
                };

                _restApi = Refit.RestService.For<IRestApi>(httpClient);

                FreshIOC.Container.Register(_restApi);
            }
            catch (Exception ex)
            {
                var error = ex.Message;
            }

            FreshIOC.Container.Register(UserDialogs.Instance);

            _localDatabase = FreshIOC.Container.Resolve<LocalDatabase>();
            FreshIOC.Container.Register(_localDatabase);
        }

        private async Task SetUpMainPage()
        {
            UserDialogs.Instance.ShowLoading("please wait");
            try
            {
                Xamarin.Forms.Page startPage = null;

                FreshNavigationContainer pageContainer = null;

                startPage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();

                pageContainer = new FreshNavigationContainer(startPage, AppConst.AppNavigation);

                MainPage = pageContainer;

                var user = await _localDatabase.GetAll<RmUsers>();

                if (user != null && user.Count > 0)
                {
                    AppState.UserCred = user.FirstOrDefault();

                    if (user.FirstOrDefault().IsLoggedIn)
                    {
                        var dashboardPage = new CustomFreshMasterDetailNavigationContainer("RAMMS");
                        dashboardPage.Init("menu");
                        dashboardPage.IsPresented = false;
                        dashboardPage.MasterBehavior = MasterBehavior.Popover;
                        dashboardPage.AddPage<DashboardPageModel>("Home", true);
                        dashboardPage.AddPage<FormAPageModel>("Road Feature Condition Register (Form A)", null);
                        dashboardPage.AddPage<FormDPageModel>("Daily Work Report, DWR (Form D)", null);
                        dashboardPage.AddPage<FormsJPageModel>("Road Safety Audit Report (Form J)", null);
                        dashboardPage.AddPage<FormXPageModel>("Work Request (Form X)", null);
                        dashboardPage.AddPage<FormB1B2PageModel>("Bridge Inspection (Form B1/B2)", null);
                        dashboardPage.AddPage<FormFCPageModel>("Carriage Inspection (Form FC)", null);

                        dashboardPage.AddPage<FormC1C2PageModel>("Culvert Inspection (Form C1/C2)", null);
                        dashboardPage.AddPage<FormF2PageModel>("Guardrail Inspection (Form F2)", null);
                        dashboardPage.AddPage<FormFDPageModel>("Drainage & Shoulder Inspection (Form FD)", null);

                        dashboardPage.AddPage<DashboardPageModel>("Settings", null);
                        dashboardPage.AddPage<LoginPageModel>("Log out", null);
                        MainPage = dashboardPage;
                    }

                    return;
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Toast(ex.Message);
            }
            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}