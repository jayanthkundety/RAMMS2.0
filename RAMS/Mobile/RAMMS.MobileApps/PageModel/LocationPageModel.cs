using System;
using System.Windows.Input;
using Acr.UserDialogs;
using FreshMvvm;
using Xamarin.Forms;

namespace RAMMS.MobileApps.PageModel
{
    
    public class LocationPageModel:FreshBasePageModel
    {
         printString printStringdelegate;
        IUserDialogs _userDialogs;
        public LocationPageModel(IUserDialogs userDialogs, IRestApi restApi)
        {
            _userDialogs = userDialogs;

        }
        public override void Init(object initData)
        {
            base.Init(initData);

           // NavigationPage.SetHasBackButton(this, false);
        }
        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);

        }
        
            public ICommand GoAction
        {
            get
            {
                return new Command(async (obj) =>
                {


                    await CoreMethods.PopPageModel(true, false);
                    ///  ClearData();
                });
            }
        }
    }
}
