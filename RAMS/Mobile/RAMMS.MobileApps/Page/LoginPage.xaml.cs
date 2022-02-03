using RAMMS.MobileApps.Page;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace RAMMS.MobileApps
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

            var images = new ObservableCollection<name>
            {
                   new name { images= "AAA.png" },
                   new name { images ="BBB.png"  },
                   new name { images ="CCC.png"  },
                   new name { images ="DDD.png"  },
                   new name { images ="EEE.png"  },
                   new name { images ="FFF.png"  },
                   new name { images ="GGG.png"   },
                   new name { images ="III.png"   }
            };
            MainCarouselView.ItemsSource = images;
            Device.StartTimer(TimeSpan.FromSeconds(5), (Func<bool>)(() =>
            {
                //MainCarouselView. FlowDirection = FlowDirection.LeftToRight;
                //images.FlowDirection = FlowDirection.LeftToRight;
                MainCarouselView.Position = (MainCarouselView.Position + 1) % images.Count;

                return true;

            }));

        }
        public class name
        {
            public string images { get; set; }
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PushAsync(new FormJOnePage());
        //}


        //private void Button_Clicked(object sender, System.EventArgs e)
        //{
        //    Navigation.PushAsync(new FormAPage());
        //    //await CoreMethods.PushPageModel<FormAPageModel>();
        //}

        //private void Button_Clicked(object sender, System.EventArgs e)
        //{
        //    Navigation.PushAsync(new TestPage());
        //}

        //private void Button_Clicked(object sender, System.EventArgs e)
        //{
        //    Navigation.PushAsync(new FormF2DetailsPage());
        //}


        //private void Button_Clicked(object sender, System.EventArgs e)
        //{​​​​
        //        Navigation.PushAsync(new DashboardPage());
        //}​​​​
        //private void Button_Clicked_1(object sender, System.EventArgs e)
        //{
        //    Navigation.PushAsync(new DashboardPage());
        //}
    }
}