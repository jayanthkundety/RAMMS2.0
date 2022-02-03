using Acr.UserDialogs;
using FreshMvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps.PageModel
{
    public class ERTPageModel: FreshBasePageModel
    {
        private IUserDialogs _userDialogs;
        private IRestApi _restApi;
        public ILocalDatabase _localDatabase;

        public ERTPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;
            _localDatabase = localDatabase;
        }

        //public ICommand FormDCommand
        //{
        //    get
        //    {
        //        return new Command(async (obj) =>
        //        {

        //            await CoreMethods.PushPageModel<FormDPageModel>();
        //        });
        //    }
        //}

        //public ICommand FormXCommand
        //{
        //    get
        //    {
        //        return new Command(async (obj) =>
        //        {
        //            await CoreMethods.PushPageModel<FormDPageModel>();
        //        });
        //    }
        //}
    }
}
