using Android.Content;
using Android.Views;
using Android.Widget;
using RAMMS.MobileApps.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(SearchBar), typeof(SearchbarIconRenderer))]

namespace RAMMS.MobileApps.Droid
{
    public class SearchbarIconRenderer : SearchBarRenderer
    {
        private readonly Context _context;

        public SearchbarIconRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var searchView = base.Control as SearchView;

                //Get the Id for your search icon
                int searchIconId = Context.Resources.GetIdentifier("android:id/search_mag_icon", null, null);
               // var searchIcon = searchView.FindViewById(searchIconId);
                //(searchIcon as ImageView).SetImageResource(Resource.Drawable.search);

                //ImageView searchViewIcon = (ImageView)searchView.FindViewById<ImageView>(searchIconId);
                //ViewGroup linearLayoutSearchView = (ViewGroup)searchViewIcon.Parent;

                var plateId = Resources.GetIdentifier("android:id/search_plate", null, null);
                var plate = Control.FindViewById(plateId);
                plate.SetBackgroundColor(Android.Graphics.Color.White);

                //searchViewIcon.SetAdjustViewBounds(true);

                //Remove the search icon from the view group and add it once again to place it at the end of the view group elements
               // linearLayoutSearchView.RemoveView(searchViewIcon);
                //linearLayoutSearchView.AddView(searchViewIcon);

                int searchViewCloseButtonId = Control.Resources.GetIdentifier("android:id/search_close_btn", null, null);
                var closeIcon = searchView.FindViewById(searchViewCloseButtonId);
                (closeIcon as ImageView).SetImageResource(Resource.Drawable.close);
            }
        }
    }
}