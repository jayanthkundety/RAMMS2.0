using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using System.Collections.Generic;
using Xamarin.Forms;
using Android.Database;
using Android.Provider;
using System.IO;
using Plugin.Media;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using Acr.UserDialogs;
using Plugin.CurrentActivity;
using Android;
using FFImageLoading.Forms.Platform;
using Esri.ArcGISRuntime;

namespace RAMMS.MobileApps.Droid
{
    [Activity(Label = "RAMMS", Icon = "@drawable/icon", Theme = "@style/MyTheme",  WindowSoftInputMode = Android.Views.SoftInput.AdjustResize, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        

        protected override void OnCreate(Bundle savedInstanceState)
        {

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            Xamarin.Forms.Forms.SetFlags(new string[] { "RadioButton_Experimental,Shapes_Experimental,CollectionView_Experimental" });
            base.OnCreate(savedInstanceState);

            Xamarin.FormsMaps.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: false);

            CrossMedia.Current.Initialize();

            int requestPermissions = 1;
            string cameraPermission = Android.Manifest.Permission.Camera;

            if (!(ContextCompat.CheckSelfPermission(this, cameraPermission) == (int)Permission.Granted))
            {
                ActivityCompat.RequestPermissions(this, new String[] { cameraPermission, }, requestPermissions);
            }

            string fileName = "photos_db.sqlite";
            string fileLocation = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string full_path = Path.Combine(fileLocation, fileName);
           
            // license with a license key
            ArcGISRuntimeEnvironment.SetLicense("runtimelite,1000,rud4449636536,none,NKMFA0PL4S0DRJE15166");




            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //global::Xamarin.Forms.FormsMaterial.Init(this, bundle);


            SetupNativeDependancies();

            var width = Resources.DisplayMetrics.WidthPixels;
            var height = Resources.DisplayMetrics.HeightPixels;
            var density = Resources.DisplayMetrics.Density;

            // App.ScreenWidth = (width - 0.5f);
            //App.ScreenHeight = (height - 0.5f);

            Window.SetSoftInputMode(Android.Views.SoftInput.StateUnchanged);

            UserDialogs.Init(this);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            //CrossCurrentActivity.Current.Init(this, savedInstanceState);
            LoadApplication(new App());



            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);
            CachedImageRenderer.InitImageViewHandler();
            LoadApplication(new App());




            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) != (int)Permission.Granted)
            {
                RequestPermissions(new string[] { Manifest.Permission.Camera, Manifest.Permission.WriteExternalStorage, Manifest.Permission.ReadExternalStorage }, 0);
            }








        }

        private void SetupNativeDependancies()
        {
            FreshMvvm.FreshIOC.Container.Register<ISQLiteFactory, SQLiteFactoryDroid>();
        }

        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        //{
        //    PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }







        #region Image Picker Implementation
        public static int OPENGALLERYCODE = 100;


        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            //If we are calling multiple image selection, enter into here and return photos and their filepaths.
            if (requestCode == OPENGALLERYCODE && resultCode == Result.Ok)
            {
                List<string> images = new List<string>();

                if (data != null)
                {
                    //Separate all photos and get the path from them all individually.
                    ClipData clipData = data.ClipData;
                    if (clipData != null)
                    {
                        for (int i = 0; i < clipData.ItemCount; i++)
                        {
                            ClipData.Item item = clipData.GetItemAt(i);
                            Android.Net.Uri uri = item.Uri;
                            var path = GetRealPathFromURI(uri);


                            if (path != null)
                            {
                                //String path1 = path;

                                //// it contains your image path...I'm using a temp string...

                                //String filename = path1.Substring(path1.LastIndexOf("/") + 1);

                                //Console.WriteLine(filename + ",");

                                images.Add(path);
                            }
                        }
                    }
                    else
                    {
                        Android.Net.Uri uri = data.Data;
                        var path = GetRealPathFromURI(uri);

                        if (path != null)
                        {
                            images.Add(path);
                        }
                    }

                    //Send our images to the carousel view.
                    MessagingCenter.Send<App, List<string>>((App)Xamarin.Forms.Application.Current, "ImagesSelectedAndroid", images);
                }
            }
        }


        public String GetRealPathFromURI(Android.Net.Uri contentURI)
        {
            try
            {
                ICursor imageCursor = null;
                string fullPathToImage = "";

                imageCursor = ContentResolver.Query(contentURI, null, null, null, null);
                imageCursor.MoveToFirst();
                int idx = imageCursor.GetColumnIndex(MediaStore.Images.ImageColumns.Data);

                if (idx != -1)
                {
                    fullPathToImage = imageCursor.GetString(idx);
                }
                else
                {
                    ICursor cursor = null;
                    var docID = DocumentsContract.GetDocumentId(contentURI);
                    var id = docID.Split(':')[1];
                    var whereSelect = MediaStore.Images.ImageColumns.Id + "=?";
                    var projections = new string[] { MediaStore.Images.ImageColumns.Data };

                    cursor = ContentResolver.Query(MediaStore.Images.Media.InternalContentUri, projections, whereSelect, new string[] { id }, null);
                    if (cursor.Count == 0)
                    {
                        cursor = ContentResolver.Query(MediaStore.Images.Media.ExternalContentUri, projections, whereSelect, new string[] { id }, null);
                    }
                    var colData = cursor.GetColumnIndexOrThrow(MediaStore.Images.ImageColumns.Data);
                    cursor.MoveToFirst();
                    fullPathToImage = cursor.GetString(colData);
                }
                return fullPathToImage;
            }
            catch (Exception ex)
            {
                Toast.MakeText(Xamarin.Forms.Forms.Context, "Unable to get path", ToastLength.Long).Show();
            }
            return null;
        }
        #endregion


    }
}