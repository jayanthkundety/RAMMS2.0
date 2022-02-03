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
    public partial class FormFDAddPage : ContentPage
    {
        public FormFDAddPage()
        {
            InitializeComponent();
        }

        private void DecimalEntryControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                //LeftDitch.Text = LeftDitch.Text?.Length > 0 ? LeftDitch.Text : null;
                //Left_Drain_Earth.Text = Left_Drain_Earth.Text?.Length > 0 ? Left_Drain_Earth.Text : null;
                //Left_Drain_Concrete.Text = Left_Drain_Concrete.Text?.Length > 0 ? Left_Drain_Concrete.Text : null;
                //Left_Drain_BlockStone.Text = Left_Drain_BlockStone.Text?.Length > 0 ? Left_Drain_BlockStone.Text : null;
                //Left_Shoulder_Asphalt.Text = Left_Shoulder_Asphalt.Text?.Length > 0 ? Left_Shoulder_Asphalt.Text : null;
                //Left_Shoulder_Gravel.Text = Left_Shoulder_Gravel.Text?.Length > 0 ? Left_Shoulder_Gravel.Text : null;
                //Left_Shoulder_Earth.Text = Left_Shoulder_Earth.Text?.Length > 0 ? Left_Shoulder_Earth.Text : null;
                //Left_Shoulder_Concrete.Text = Left_Shoulder_Concrete.Text?.Length > 0 ? Left_Shoulder_Concrete.Text : null;
                //Left_Shoulder_Kerb.Text = Left_Shoulder_Kerb.Text?.Length > 0 ? Left_Shoulder_Kerb.Text : null;
                //Right_Ditch.Text = Right_Ditch.Text?.Length > 0 ? Right_Ditch.Text : null;
                //Right_Drain_Earth.Text = Right_Drain_Earth.Text?.Length > 0 ? Right_Drain_Earth.Text : null;
                //Right_Drain_Concrete.Text = Right_Drain_Concrete.Text?.Length > 0 ? Right_Drain_Concrete.Text : null;
                //Right_Drain_BlockStone.Text = Right_Drain_BlockStone.Text?.Length > 0 ? Right_Drain_BlockStone.Text : null;
                //Right_Shoulder_Asphalt.Text = Right_Shoulder_Asphalt.Text?.Length > 0 ? Right_Shoulder_Asphalt.Text : null;
                //Right_Shoulder_Gravel.Text = Right_Shoulder_Gravel.Text?.Length > 0 ? Right_Shoulder_Gravel.Text : null;
                //Right_Shoulder_Earth.Text = Right_Shoulder_Earth.Text?.Length > 0 ? Right_Shoulder_Earth.Text : null;
                //Right_Shoulder_Concrete.Text = Right_Shoulder_Concrete.Text?.Length > 0 ? Right_Shoulder_Concrete.Text : null;
                //Right_Shoulder_Kerb.Text = Right_Shoulder_Kerb.Text?.Length > 0 ? Right_Shoulder_Kerb.Text : null;
            }
        }
    }
}