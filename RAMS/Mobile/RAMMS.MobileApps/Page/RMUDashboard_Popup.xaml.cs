using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RAMMS.MobileApps.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class Models : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Models()
        {
            IsChecked = false;
        }

        public string Text { get; set; }

        private bool isChecked;
        public bool IsChecked
        {
            get
            {
                return isChecked;
            }
            set
            {
                isChecked = value;
                NotifyPropertyChanged();
            }
        }
    }

    public partial class RMUDashboard_Popup : PopupPage
    {
        List<Models> list = new List<Models>();
        public RMUDashboard_Popup()
        {
            InitializeComponent();


            this.BackgroundColor = Color.Transparent;


            list.Add(new Models() { Text = "Miri" });
            list.Add(new Models() { Text = "Batu Niah" });
          


            listView.ItemsSource = list;

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();

            var result = list.Where(w => w.IsChecked == true).ToList();

            string s = "";

            int index = 0;
            foreach (var model in result)
            {
                s = s + model.Text;
                if (index < result.Count - 1)
                {
                    s = s + ",";
                }
                index++;
            }

            MessagingCenter.Send<object, string>(this, "Hi", s);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}