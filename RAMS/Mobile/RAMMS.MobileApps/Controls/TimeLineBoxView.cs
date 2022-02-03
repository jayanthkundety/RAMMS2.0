using Xamarin.Forms;

namespace RAMMS.MobileApps
{
    public class TimeLineBoxView : BoxView
    {
        public TimeLineBoxView()
        {
            HeightRequest = 3;
            HorizontalOptions = LayoutOptions.FillAndExpand;
            Color = Color.FromHex("#808080");
        }
    }
}