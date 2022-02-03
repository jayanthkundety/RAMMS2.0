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
    public partial class FormFCAddPage : ContentPage
    {
        public FormFCAddPage()
        {
            InitializeComponent();
        }

        private void CustomeEntryNumberControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                Avg1.Text = Avg1.Text?.Length > 0 ? Avg1.Text : null;
                Avg2.Text = Avg2.Text?.Length > 0 ? Avg2.Text : null;
                Avg3.Text = Avg3.Text?.Length > 0 ? Avg3.Text : null;
                Avg4.Text = Avg4.Text?.Length > 0 ? Avg4.Text : null;
                Avg5.Text = Avg5.Text?.Length > 0 ? Avg5.Text : null;
                Avg6.Text = Avg6.Text?.Length > 0 ? Avg6.Text : null;
                Avg7.Text = Avg7.Text?.Length > 0 ? Avg7.Text : null;
                Avg8.Text = Avg8.Text?.Length > 0 ? Avg8.Text : null;
                Avg9.Text = Avg9.Text?.Length > 0 ? Avg9.Text : null;
                Avg10.Text = Avg10.Text?.Length > 0 ? Avg10.Text : null;
                Avg11.Text = Avg11.Text?.Length > 0 ? Avg11.Text : null;
                Avg12.Text = Avg12.Text?.Length > 0 ? Avg12.Text : null;
                Avg13.Text = Avg13.Text?.Length > 0 ? Avg13.Text : null;
                Avg14.Text = Avg14.Text?.Length > 0 ? Avg14.Text : null;
                Avg15.Text = Avg15.Text?.Length > 0 ? Avg15.Text : null;

            }
        }
    }
}