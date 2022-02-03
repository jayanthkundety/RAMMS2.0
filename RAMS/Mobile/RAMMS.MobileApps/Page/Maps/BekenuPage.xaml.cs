using System;

using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Portal;
using Esri.ArcGISRuntime.Xamarin.Forms;
using RAMMS.MobileApps.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps.Page.Maps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BekenuPage : ContentPage
    {
        private readonly Envelope _usEnvelope = new Envelope(114.05933598, 4.5481702, 114.05933598, 4.5481702, SpatialReferences.Wgs84);
        private readonly string[] _sources = { "URL", "Local file", "Portal item" };
       
        public BekenuPage()
        {
            InitializeComponent();
            Initialize();
            
        }
        
    private void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            var product = e.Item as Product;
            var vm = BindingContext as MainViewModel;
            vm?.ShowOrHidePoducts(product);
        }
        //private void ListView_OnItemTappeds(object sender, ItemTappedEventArgs e)
        //{
        //    var products = e.Item as Productss;
        //    var vm = BindingContext as MainViewModel1;
        //    vm?.ShowOrHidePoducts(products);
        //}

        private void Initialize()
        {
            
            // Set up the basemap.
            MySceneView.Scene = new Scene(Basemap.CreateImageryWithLabels());

            // Update the UI.
            LayerPicker.IsEnabled = true;
            LayerPicker.ItemsSource = _sources;
            LayerPicker.SelectedIndexChanged += LayerPicker_SelectionChanged;
            LayerPicker.SelectedIndex = 0;
            MySceneView.SetViewpoint(new Viewpoint(new MapPoint(114.05933598, 4.5481702, SpatialReferences.Wgs84), 1500));

        }

        private async void LayerPicker_SelectionChanged(object sender, EventArgs e)
        {
            // Clear existing layers.
            MySceneView.Scene.OperationalLayers.Clear();

            // Get the name of the selected layer.
            string name = _sources[LayerPicker.SelectedIndex];

            try
            {
                // Create the layer using the chosen constructor.
                KmlLayer layer;
                switch (name)
                {
                    case "URL":
                    default:
                        layer = new KmlLayer(new Uri("https://ramms.s3.ap-south-1.amazonaws.com/MIRI+IRI+L1.kmz"));
                        break;
                    //case "Local file":
                    //    string filePath = DataManager.GetDataFolder("324e4742820e46cfbe5029ff2c32cb1f", "US_State_Capitals.kml");
                    //    layer = new KmlLayer(new Uri(filePath));
                    //    break;
                    case "Portal item":
                        ArcGISPortal portal = await ArcGISPortal.CreateAsync();
                        PortalItem item = await PortalItem.CreateAsync(portal, "9fe0b1bfdcd64c83bd77ea0452c76253");
                        layer = new KmlLayer(item);
                        break;
                }
                MySceneView.SetViewpoint(new Viewpoint(new MapPoint(114.05933598, 4.5481702, SpatialReferences.Wgs84), 2000));

                // Add the selected layer to the map.
                MySceneView.Scene.OperationalLayers.Add(layer);

                // Zoom to the extent of the United States.
                await MySceneView.SetViewpointAsync(new Viewpoint(_usEnvelope));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }






            





        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            MySceneView.SetViewpoint(new Viewpoint(new MapPoint(114.05933598, 4.5481702, SpatialReferences.Wgs84), 20000));

            // Clear existing layers.
            MySceneView.Scene.OperationalLayers.Clear();

            // Get the name of the selected layer.
            string name = _sources[LayerPicker.SelectedIndex];

            try
            {
                // Create the layer using the chosen constructor.
                KmlLayer layer;
                switch (name)
                {
                    case "URL":
                    default:
                        layer = new KmlLayer(new Uri("https://ramms.s3.ap-south-1.amazonaws.com/MIRI+IRI+L1.kmz"));
                        MySceneView.SetViewpoint(new Viewpoint(new MapPoint(113.71680735, 4.5481702, SpatialReferences.Wgs84), 20000));

                        break;
                    case "Local file":
                    //string filePath = DataManager.GetDataFolder("324e4742820e46cfbe5029ff2c32cb1f", "US_State_Capitals.kml");
                    //layer = new KmlLayer(new Uri(filePath));
                    //break;
                    case "Portal item":
                        ArcGISPortal portal = await ArcGISPortal.CreateAsync();
                        PortalItem item = await PortalItem.CreateAsync(portal, "9fe0b1bfdcd64c83bd77ea0452c76253");
                        layer = new KmlLayer(item);
                        break;
                }
                MySceneView.SetViewpoint(new Viewpoint(new MapPoint(114.05933598, 4.5481702, SpatialReferences.Wgs84), 20000));

                // Add the selected layer to the map.
                MySceneView.Scene.OperationalLayers.Add(layer);

                // Zoom to the extent of the United States.
                await MySceneView.SetViewpointAsync(new Viewpoint(_usEnvelope));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }





            









           




















        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {

            if (Toggle2.IsVisible)
            {
                Toggle2.IsVisible = false;
            }
            else
            {
                Toggle2.IsVisible = true;
            }

            mani.Text = "IRI";
           
            MySceneView.SetViewpoint(new Viewpoint(new MapPoint(114.05933598, 4.5481702, SpatialReferences.Wgs84), 20000000000));

            // Clear existing layers.
            MySceneView.Scene.OperationalLayers.Clear();

            // Get the name of the selected layer.
            string name = _sources[LayerPicker.SelectedIndex];

            try
            {
                // Create the layer using the chosen constructor.
                KmlLayer layer;
                switch (name)
                {
                    case "URL":
                    default:
                        layer = new KmlLayer(new Uri("https://ramms.s3.ap-south-1.amazonaws.com/MIRI+IRI+L1.kmz"));
                        MySceneView.SetViewpoint(new Viewpoint(new MapPoint(114.05933598, 4.5481702, SpatialReferences.Wgs84), 2000000000));

                        break;
                    case "Local file":
                    //string filePath = DataManager.GetDataFolder("324e4742820e46cfbe5029ff2c32cb1f", "US_State_Capitals.kml");
                    //layer = new KmlLayer(new Uri(filePath));
                    //break;
                    case "Portal item":
                        ArcGISPortal portal = await ArcGISPortal.CreateAsync();
                        PortalItem item = await PortalItem.CreateAsync(portal, "9fe0b1bfdcd64c83bd77ea0452c76253");
                        layer = new KmlLayer(item);
                        break;
                }
                MySceneView.SetViewpoint(new Viewpoint(new MapPoint(114.05933598, 4.5481702, SpatialReferences.Wgs84), 2000000000));

                // Add the selected layer to the map.
                MySceneView.Scene.OperationalLayers.Add(layer);

                // Zoom to the extent of the United States.
                await MySceneView.SetViewpointAsync(new Viewpoint(_usEnvelope));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }




            var button = (Button)sender;


            

           

        }

        private async void Button_Clicked_2(object sender, EventArgs e)
        {

            if (Toggle2.IsVisible)
            {
                Toggle2.IsVisible = false;
            }
            else
            {
                Toggle2.IsVisible = true;
            }

            mani.Text = "MPD";
            MySceneView.SetViewpoint(new Viewpoint(new MapPoint(114.05933598, 4.5481702, SpatialReferences.Wgs84), 20000));

            // Clear existing layers.
            MySceneView.Scene.OperationalLayers.Clear();

            // Get the name of the selected layer.
            string name = _sources[LayerPicker.SelectedIndex];

            try
            {
                // Create the layer using the chosen constructor.
                KmlLayer layer;
                switch (name)
                {
                    case "URL":
                    default:
                        layer = new KmlLayer(new Uri("https://ramms.s3.ap-south-1.amazonaws.com/MIRI+IRI+L1.kmz"));
                        MySceneView.SetViewpoint(new Viewpoint(new MapPoint(114.05933598, 4.5481702, SpatialReferences.Wgs84), 20000));

                        break;
                    case "Local file":
                    //string filePath = DataManager.GetDataFolder("324e4742820e46cfbe5029ff2c32cb1f", "US_State_Capitals.kml");
                    //layer = new KmlLayer(new Uri(filePath));
                    //break;
                    case "Portal item":
                        ArcGISPortal portal = await ArcGISPortal.CreateAsync();
                        PortalItem item = await PortalItem.CreateAsync(portal, "9fe0b1bfdcd64c83bd77ea0452c76253");
                        layer = new KmlLayer(item);
                        break;
                }
                MySceneView.SetViewpoint(new Viewpoint(new MapPoint(114.05933598, 4.5481702, SpatialReferences.Wgs84), 20000));

                // Add the selected layer to the map.
                MySceneView.Scene.OperationalLayers.Add(layer);

                // Zoom to the extent of the United States.
                await MySceneView.SetViewpointAsync(new Viewpoint(_usEnvelope));
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.ToString(), "OK");
            }



            

           

        }

        private void Button_Clicked_3(object sender, EventArgs e)
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

        private void Button_Clicked_4(object sender, EventArgs e)
        {
            

            if (Toggle2.IsVisible)
            {
                Toggle2.IsVisible = false;
            }
            else
            {
                Toggle2.IsVisible = true;
            }
        }

        private void stack3_Clicked(object sender, EventArgs e)
        {
            mani.Text = "Residual Life";
            if (Toggle2.IsVisible)
            {
                Toggle2.IsVisible = false;
            }
            else
            {
                Toggle2.IsVisible = true;
            }
        }

        private void Button_Clicked_5(object sender, EventArgs e)
        {
            prem.Text = "Left";
            if (Toggle1.IsVisible)
            {
                Toggle1.IsVisible = false;
            }
            else
            {
                Toggle1.IsVisible = true;
            }
        }

        private void Button_Clicked_6(object sender, EventArgs e)
        {
            prem.Text = "Right";

            if (Toggle1.IsVisible)
            {
                Toggle1.IsVisible = false;
            }
            else
            {
                Toggle1.IsVisible = true;
            }
        }
    }
}