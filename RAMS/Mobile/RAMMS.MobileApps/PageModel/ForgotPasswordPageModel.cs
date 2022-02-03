using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace RAMMS.MobileApps.PageModel
{
    public class ForgotPasswordPageModel : FreshBasePageModel
    {
        private IUserDialogs _userDialogs;
        private IRestApi _restApi;
        private ILocalDatabase _localDatabase;
        public string ErrorTextMessage { get; set; }
        public bool IsErrorTextMessageVisible { get; set; } = false;
        public string MailID { get; set; } = "";
        public bool IsValid(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public ForgotPasswordPageModel(IUserDialogs userDialogs, IRestApi restApi, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;
            _localDatabase = localDatabase;
        }
        public override async void Init(object initData)
        {
            base.Init(initData);
        }
        public ICommand ReturnLogin
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    await CoreMethods.PopPageModel();
                });
            }
        } 
        
        public FreshAwaitCommand SubmitCommand
        {
            get
            {
                return new FreshAwaitCommand(async (obj) =>
                {
                    try
                    {
                        if (IsValid(MailID))
                        {
                            if (CrossConnectivity.Current.IsConnected)
                            {
                                try
                                {
                                    _userDialogs.ShowLoading("Loading");
                                    var response = await _restApi.GetPasswordReset(AppConst.WebBaseURL, MailID);
                                    if (response.success)
                                    {
                                        if (response.data == 0)
                                        {
                                            IsErrorTextMessageVisible = true;
                                            ErrorTextMessage = "Invalid User Email Credential";
                                        }
                                        else
                                        {
                                            IsErrorTextMessageVisible = false;
                                            await _userDialogs.AlertAsync("Email sent successfully. Please check your mail", "RAMS", "OK");
                                            await CoreMethods.PopPageModel();
                                        }
                                    }
                                }
                                catch (Exception ex) { }
                                finally
                                {
                                    _userDialogs.HideLoading();
                                }
                            }
                        }
                        else
                        {
                            IsErrorTextMessageVisible = true;
                            ErrorTextMessage = "Invalid Email Address";
                        }
                    }
                    catch (Exception ex) 
                    {
                        IsErrorTextMessageVisible = true;
                        ErrorTextMessage = "Please Enter Email ID";
                    }
                    finally
                    {
                        obj.SetResult(true);
                    }

                });
            }
        }
    }
}
