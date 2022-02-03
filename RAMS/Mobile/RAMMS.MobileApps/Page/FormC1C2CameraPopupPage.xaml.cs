using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormC1C2CameraPopupPage : ContentPage
    {
        public FormC1C2CameraPopupPage()
        {
            InitializeComponent();
            this.BackgroundColor = new Color(0, 0, 0, 0.6);
        }
    }
}