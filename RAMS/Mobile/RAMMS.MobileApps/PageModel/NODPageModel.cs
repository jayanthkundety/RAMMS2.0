using Acr.UserDialogs;
using FreshMvvm;
using RAMMS.MobileApps.PageModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps
{
    public class NODPageModel : FreshBasePageModel
    {
        private IUserDialogs _userDialogs;
        private IRestApi _restApi;
        public ILocalDatabase _localDatabase;

        public NODPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;
            _localDatabase = localDatabase;
        }

        public ICommand FormACommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    await CoreMethods.PushPageModel<FormAPageModel>();
                });
            }
        }

        public ICommand FormJCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    await CoreMethods.PushPageModel<FormsJPageModel>();
                });
            }
        }
    }
}