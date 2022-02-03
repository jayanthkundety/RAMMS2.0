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
    public partial class FormF2DetailsPage : ContentPage
    {
        public FormF2DetailsPage()
        {
            InitializeComponent();
        }

        //Property change not fired while delete the last digit. So handled here.
        private void DecimalEntryControl_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty( e.NewTextValue))
            {
                cnd1.Text = cnd1.Text?.Length > 0 ? cnd1.Text : null;
                cnd2.Text = cnd2.Text?.Length > 0 ? cnd2.Text : null;
                cnd3.Text = cnd3.Text?.Length > 0 ? cnd3.Text : null;
            }
        }
    }
}