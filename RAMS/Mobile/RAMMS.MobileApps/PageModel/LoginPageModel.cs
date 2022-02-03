using Acr.UserDialogs;
using FreshMvvm;
using RAMMS.Domain;
using RAMMS.MobileApps.Model;
using RAMMS.MobileApps.Model.Adapter;
using RAMMS.MobileApps.PageModel;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps
{
    public class LoginPageModel : FreshBasePageModel
    {
        public IRestApi _restApi;

        public string txtEmailAddress { get; set; }

        public string txtPassword { get; set; }

        public string IconShowHide { get; set; }

        public IUserDialogs _userDialogs;

        public ILocalDatabase _localDatabase;

        public Entry isPasswordField { get; set; }

        private LoginPage _page;

        public LoginPageModel(IUserDialogs userDialogs, IRestApi restApi, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;
            _localDatabase = localDatabase;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            NavigationPage.SetHasNavigationBar(CurrentPage, false);

           // txtEmailAddress = "AVOWS";
          // txtPassword = "AVOWS";
        }

        public FreshAwaitCommand ForgotCommand
        {
            get
            {
                return new FreshAwaitCommand(async (obj) =>
                {
                    try
                    {
                        await CoreMethods.PushPageModel<ForgotPasswordPageModel>();
                    }
                    catch (Exception ex) { }
                    finally
                    {
                        obj.SetResult(true);
                    }
                });
            }
        }
        public ICommand LoginCommand
        {
            get
            {

                return new Command(async (obj) =>
                {

                    if (LoginValidate())
                    {
                        _userDialogs.ShowLoading("Logging in");

                        try
                        {
                            App.AuthToken = "";


                            ////var login = new RmUserCredential()
                            ////{
                            ////    UsrUserName = txtEmailAddress,

                            ////    UsrPassword = txtPassword,
                            ////};

                            ////var json11l = Newtonsoft.Json.JsonConvert.SerializeObject(login);


                            var userLogin = new RmUserCredential()
                            {
                                UsrUserName = txtEmailAddress.Trim(),

                                UsrPassword = txtPassword.Trim(),
                            };

                            var json1l = Newtonsoft.Json.JsonConvert.SerializeObject(userLogin);

                            var response = await _restApi.SignIn(userLogin);

                            if (response.success)
                            {
                                App.AuthToken = response.data.ToString();

                                if (App.AuthToken != null && App.AuthToken != "")
                                {

                                     Security.userDetails = await _restApi.GetUserDetails();

                                    //if (response1.IsSynchronized)
                                    //{ 

                                    //}


                                    //var userLogin = new RmUserCredential()
                                    //{
                                    //    UsrUserName = txtEmailAddress,
                                    //    UsrPassword = txtPassword,
                                    //};

                                    //var json1l = Newtonsoft.Json.JsonConvert.SerializeObject(userLogin);

                                    //var response1 = await _restApi.GetUserDetails();

                                    //if (response1.IsSynchronized)
                                    //{ 

                                    //}

                                    //AppState.UserCred = response1.data;

                                    //AppState.UserCred.IsLoggedIn = true;

                                    //if (AppState.UserCred != null)
                                    //{
                                    //    var loggedUser = AppState.UserCred;

                                    //    AppState.UserCred.UsrPkId = loggedUser.UsrPkId;

                                    //    AppState.UserCred.UsrContrPkId = loggedUser.UsrContrPkId;

                                    //    AppState.UserCred.UsrUserName = loggedUser.UsrUserName;

                                    //    AppState.UserCred.UsrPassword = loggedUser.UsrPassword;

                                    //    AppState.UserCred.UsrPosition = loggedUser.UsrPosition;

                                    //    AppState.UserCred.UsrDepartment = loggedUser.UsrDepartment;

                                    //    await _localDatabase.InsertOrReplaceAsync(loggedUser);

                                    //}

                                    var dashboardPage = new CustomFreshMasterDetailNavigationContainer("MenuNavigation");

                                    dashboardPage.Init("menu");

                                    dashboardPage.IsPresented = false;

                                    dashboardPage.MasterBehavior = MasterBehavior.Popover;

                                    dashboardPage.AddPage<DashboardPageModel>("Home", true);

                                    dashboardPage.AddPage<FormAPageModel>("Road Feature Condition Register (Form A)", true);

                                    dashboardPage.AddPage<FormDPageModel>("Daily Work Report, DWR (Form D)", true);
                                    dashboardPage.AddPage<FormB1B2PageModel>("Bridge Inspection (Form B1/B2)", true);
                                    dashboardPage.AddPage<FormC1C2PageModel>("Culvert Inspection (Form C1/C2)", null);
                                    dashboardPage.AddPage<FormFCPageModel>("Carriage Inspection (Form FC)", null);
                                    dashboardPage.AddPage<FormFDPageModel>("Drainage & Shoulder Inspection (Form FD)", null);

                                    dashboardPage.AddPage<FormsJPageModel>("Road Safety Audit Report (Form J)", true);
                                    dashboardPage.AddPage<FormF2PageModel>("Guardrail Inspection (Form F2)", null);

                                    dashboardPage.AddPage<FormXPageModel>("Work Request (Form X)", true);
                                    dashboardPage.AddPage<DashboardPageModel>("Settings", true);

                                    dashboardPage.AddPage<LoginPageModel>("Log out", null);

                                    await CoreMethods.PushNewNavigationServiceModal(dashboardPage, null, true);

                                }
                                else
                                {
                                    _userDialogs.Alert(response.errorMessage);
                                }
                            }
                            else
                            {
                                _userDialogs.Alert(response.errorMessage);
                            }
                            //}
                        }
                        catch (Exception ex)
                        {
                            _userDialogs.Alert(ex.Message);
                        }
                        finally
                        {
                            _userDialogs.HideLoading();
                        }
                    }
                });
            }
        }

        public bool LoginValidate()
        {
            if (string.IsNullOrEmpty(txtEmailAddress) && string.IsNullOrEmpty(txtPassword))
            {
                _userDialogs.Alert("Please enter User name & Password");
                return false;
            }
            else if (string.IsNullOrEmpty(txtEmailAddress))
            {
                _userDialogs.Alert("Please enter User name");
                return false;
            }
            else if (string.IsNullOrEmpty(txtPassword))
            {
                _userDialogs.Alert("Please enter Password");
                return false;
            }
            return true;
        }

        public ICommand ForgotPassword
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {

                    await CoreMethods.PushPageModel<ForgotPasswordPageModel>();
                });
            }
        }
    }
}