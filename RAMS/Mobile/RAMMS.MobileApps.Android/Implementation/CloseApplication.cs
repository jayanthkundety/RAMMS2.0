using Android.App;
using RAMMS.MobileApps.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(CloseApplication))]

namespace RAMMS.MobileApps.Droid
{
    public class CloseApplication : ICloseApplication
    {
        public CloseApplication()
        {
        }

        public void closeApplication()
        {
            (Xamarin.Forms.Forms.Context as Activity).Finish();
        }
    }
}