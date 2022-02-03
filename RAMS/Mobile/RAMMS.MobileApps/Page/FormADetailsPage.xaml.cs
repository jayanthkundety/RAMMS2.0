
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormADetailsPage : ContentPage
    {
        public FormADetailsPage()
        {
            InitializeComponent();
            //list.ItemSelected += (sender, e) =>
            //{
            //    ((ListView)sender).SelectedItem = null;
            //};

            //lists.ItemSelected += (sender, e) =>
            //{
            //    ((ListView)sender).SelectedItem = null;
            //};



        }




        ////Capture the user's signature.
        //async void OnSaveSignature(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var signature = PadView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);

        //        //using (FileStream file = new FileStream(file_path, FileMode.Create, System.IO.FileAccess.Write))
        //        //{
        //        //    image.CopyTo(file);
        //        //}


        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}


        //protected override void OnBindingContextChanged()
        //{
        //    base.OnBindingContextChanged();

        //    var vm = (TestViewModel)BindingContext; // Warning, the BindingContext View <-> ViewModel is already set

        //    vm.SignatureFromStream = async () =>
        //    {
        //        if (PadView.Points.ToString()Count() > 0)
        //        {
        //            using (var stream = await SignatureView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png))
        //            {
        //                return await ImageConverter.ReadFully(stream);
        //            }
        //        }

        //        return await Task.Run(() => (byte[])null);
        //    };
        //}


        //public async void OnGetInspSign()
        //{

        //    Stream image = await SignatureView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Jpeg);

        //    if (!SignatureView.IsBlank)
        //    {
        //        var stocks = SignatureView.Strokes;

        //        await this.DisplayAlert("RAMMS", stocks.ToString(),"OK");


                
            
        //        string path = "/mnt/sdcard/RAMMS/FormA/";

        //        string file_path = path + "inspsign" + ".png";

        //        //var pointCount = points.Count();
        //        var imageSize = image.Length / 1000;
        //        //var linesCount = points.Count(p => p == Point.Zero) + (points.Length > 0 ? 1 : 0);

        //        using (BinaryReader binaryReader = new BinaryReader(image))
        //        {
        //            binaryReader.BaseStream.Position = 0;
        //            byte[] Signature = binaryReader.ReadBytes((int)image.Length);
        //        }


        //        //using (FileStream file = new FileStream(file_path, FileMode.Create, System.IO.FileAccess.Write))
        //        //{
        //        //    image.CopyTo(file);
        //        //}


        //        if (Device.OS == TargetPlatform.Android)
        //        {
        //            try
        //            {

        //                var signatureMemoryStream = image as System.IO.MemoryStream;
        //                if (signatureMemoryStream == null)
        //                {
        //                    signatureMemoryStream = new System.IO.MemoryStream();
        //                    image.CopyTo(signatureMemoryStream);
        //                }
        //                var byteArray = signatureMemoryStream.ToArray();
        //                string base64String = Convert.ToBase64String(byteArray);
        //                //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));
        //                App.inspSign = base64String;
        //            }
        //            catch
        //            {

        //            }
        //         }
        //            image.Dispose();
        //    }




        //}



        //public async void OnGetVerifySign()
        //{
        //    //var points = PadView.Points.ToString().ToArray();
        //    var image = await VPadView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png);
        //    //var image = await VPadView.GetImage(Acr.XamForms.SignaturePad.ImageFormatType.Png);

        //    string path = "/mnt/sdcard/RAMMS/FormA/";
            
        //    string FilePath = path + "verfsign" + ".png";


        //    //var pointCount = points.Count();
        //    var imageSize = image.Length / 1000;

        //    using (FileStream file2 = new FileStream(FilePath, FileMode.Create, System.IO.FileAccess.Write))
        //    {
        //        image.CopyTo(file2);
        //    }


        //    //var linesCount = points.Count(p => p == Point.Zero) + (points.Length > 0 ? 1 : 0);

        //    if (Device.OS == TargetPlatform.Android)
        //    {
        //        try
        //        {

        //            var signatureMemoryStream = image as System.IO.MemoryStream;
                    
        //            if (signatureMemoryStream == null)
        //            {
        //                signatureMemoryStream = new System.IO.MemoryStream();

        //                image.CopyTo(signatureMemoryStream);
        //            }
                    
        //            var byteArray = signatureMemoryStream.ToArray();

        //            string base64String = Convert.ToBase64String(byteArray);

        //            //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));
                    
        //            App.versign = base64String;
        //        }
        //        catch
        //        {
        //            //e.InnerException();
        //        }
        //    }
        //    image.Dispose();

        //}


        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FormAAddPage());
            
        }

        public void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            Navigation.PushAsync(new FormAAddPage());
        }

        void rodeCodePicker_PickerTapped(System.Object sender, System.EventArgs e)
        {
            
        }

       // private async void Button_Clicked_1(object sender, EventArgs e)
        //{
        //    Stream image = await SignatureView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Jpeg);

        //    if (!SignatureView.IsBlank)
        //    {
        //        var stocks = SignatureView.Strokes;

        //        await this.DisplayAlert("RAMMS", stocks.ToString(), "OK");




        //        string path = "/mnt/sdcard/RAMMS/FormA/";

        //        string file_path = path + "inspsign" + ".png";

        //        //var pointCount = points.Count();
        //        var imageSize = image.Length / 1000;
        //        //var linesCount = points.Count(p => p == Point.Zero) + (points.Length > 0 ? 1 : 0);

        //        using (BinaryReader binaryReader = new BinaryReader(image))
        //        {
        //            binaryReader.BaseStream.Position = 0;
        //            byte[] Signature = binaryReader.ReadBytes((int)image.Length);
        //        }
        //    }
        //}
    }
}