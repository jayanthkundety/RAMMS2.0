using Acr.UserDialogs;
using Expandable;

//using CachedImageCircle.Forms.Plugin.Abstractions;
using FreshMvvm;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
//using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;

namespace RAMMS.MobileApps
{
    public class CustomFreshMasterDetailNavigationContainer : FreshMasterDetailNavigationContainer
    {
        private ContentPage _menuPage;
        public ObservableCollection<DashboardMenuItem> dashboardmenuitems = new ObservableCollection<DashboardMenuItem>();
        private string currentuserImage;
        private Label _pagenameLabel;

        //private CircleCachedImage _CurrentUserImage;
        public ListView listView;

        private ILocalDatabase _localDatabase;

        protected override void CreateMenuPage(string menuPageTitle, string menuIcon)
        {
            _localDatabase = FreshIOC.Container.Resolve<LocalDatabase>();
            _pagenameLabel = new Label();
            base.CreateMenuPage(menuPageTitle,  menuIcon);
            // _CurrentUserImage = new CircleCachedImage();
            dashboardmenuitems = new ObservableCollection<DashboardMenuItem>();
            _menuPage = new ContentPage();

            MainDesign();

            // SubscribeMessageCenter();
        }

        public void MainDesign()
        {
            var mainstack = new StackLayout
            {
                Padding = new Thickness(0, 10, 0, 10),
                Spacing = 10,
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.FromHex("#232F3E"),
            };

            var headerstack = new StackLayout
            {
                Padding = new Thickness(15, 10, 15, 10),
                Spacing = 10,
                Orientation = StackOrientation.Horizontal,
                // VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowSpacing = 3
            };

            grid.RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = 10 },
                new RowDefinition { Height = GridLength.Auto },
            };

            grid.ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(8, GridUnitType.Star) }
            };

            int _photo = Device.OnPlatform(60, 60, 80);

            //_CurrentUserImage = new CircleCachedImage
            //{
            //    Source = "user.png",
            //    HorizontalOptions = LayoutOptions.Center,
            //    Aspect = Aspect.AspectFill,
            //    HeightRequest = _photo,
            //    WidthRequest = _photo,
            //};
            //Grid.SetRow(_CurrentUserImage, 0);

            //Grid.SetColumn(_CurrentUserImage, 0);

            //grid.Children.Add(_CurrentUserImage);

            _pagenameLabel = new Label
            {
                Text = "RAMS",
               // FontFamily = Device.RuntimePlatform == Device.Android ? "ProximaNova-Semibold.ttf#ProximaNova" : "ProximaNova-Semibold",
                LineBreakMode = LineBreakMode.WordWrap,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                TextColor = Color.FromHex("#cbd2e7"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Start
            };

            Grid.SetRow(_pagenameLabel, 0);

            Grid.SetColumn(_pagenameLabel, 1);

            grid.Children.Add(_pagenameLabel);

            var boxview = new BoxView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Color = Color.Gray,
                HeightRequest = 1
            };

            Grid.SetRow(boxview, 3);
            Grid.SetColumn(boxview, 0);
            Grid.SetColumnSpan(boxview, 2);
            grid.Children.Add(boxview);

            headerstack.Children.Add(grid);

            var Detailsstack = new StackLayout
            {
                Padding = new Thickness(0, 2, 5, 5),
                Spacing = 8,
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            StackLayout expandableList = new StackLayout();

            dashboardmenuitems = new ObservableCollection<DashboardMenuItem>();

            dashboardmenuitems.Add(new DashboardMenuItem()
            {
                MenuName = "Home",
                MenuIcon = "Home.PNG",
                MenuItems = new ObservableCollection<MenuItem>()
            });

            if (Model.Security.IsView(ModuleNameList.NOD))
            {
                dashboardmenuitems.Add(new DashboardMenuItem()
                {
                    MenuName = "NOD",
                    MenuIcon = "NOD.PNG",
                    MenuItems = new ObservableCollection<MenuItem>()
                {
                    new MenuItem() { Name = "Road Feature Condition Register (Form A)" },
                    new MenuItem() { Name = "Road Safety Audit Report (Form J)" }
                }
                });
            }

            if (Model.Security.IsView(ModuleNameList.Emergency_Response_Team))
            {
                dashboardmenuitems.Add(new DashboardMenuItem()
                {
                    MenuName = "ERT",
                    MenuIcon = "ERT.PNG",
                    MenuItems = new ObservableCollection<MenuItem>()
                {
                    new MenuItem() { Name = "Daily Work Report, DWR (Form D)" },
                    new MenuItem() { Name = "Work Request (Form X)" }
                }
                });
            }

            if (Model.Security.IsView(ModuleNameList.Condition_Inspection))
            {
                dashboardmenuitems.Add(new DashboardMenuItem()
                {
                    MenuName = "Condition Inspection",
                    MenuIcon = "Conditon.PNG",
                    MenuItems = new ObservableCollection<MenuItem>()
                    {
                        new MenuItem() { Name = "Culvert Inspection (Form C1/C2)" },
                        //new MenuItem() { Name = "Culvert Summary (Form F4)" },
                        new MenuItem() { Name = "Bridge Inspection (Form B1/B2)" },
                       // new MenuItem() { Name = "Bridge Summary (Form F5)" },
                        new MenuItem() { Name = "Guardrail Inspection (Form F2)" },
                        new MenuItem() { Name = "Carriage Inspection (Form FC)" },
                        new MenuItem() { Name = "Drainage & Shoulder Inspection (Form FD)" },
                       // new MenuItem() { Name = "Road Inventory & Condition Summary (Form FS)" }
                    }
                });
            }

            dashboardmenuitems.Add(new DashboardMenuItem( )
            {
                MenuName = "Log out",
                MenuIcon = "logouticon.png",
            });

            BindableLayout.SetItemsSource(expandableList, dashboardmenuitems);

            var listItemTemplate = new DataTemplate(() =>
            {
                ExpandableView expander = new ExpandableView();
                expander.ExpandAnimationLength = 100;
                expander.Command = new Command<object>(Expander_Tapped);
                expander.CommandParameter = expander;
                var stack = new StackLayout
                {
                    Padding = new Thickness(12, 0, 5, 0),
                    Spacing = 10,
                    Orientation = StackOrientation.Horizontal,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };

                var pageLabel = new Label
                {
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    FontFamily = Device.RuntimePlatform == Device.Android ? "ProximaNova-Semibold.ttf#ProximaNova" : "ProximaNova-Semibold",
                    TextColor = Color.FromHex("#cbd2e7"),
                    HeightRequest=60,
                    VerticalTextAlignment = TextAlignment.Center,
                    HorizontalTextAlignment = TextAlignment.Start
                };
                pageLabel.SetBinding(Label.TextProperty, new Binding("MenuName"));
                var image = new Image
                {
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalOptions = LayoutOptions.Center,
                    HeightRequest = 30,
                    WidthRequest = 30
                };
                image.SetBinding(Image.SourceProperty, new Binding("MenuIcon"));
                stack.Children.Add(image);
                stack.Children.Add(pageLabel);

                expander.PrimaryView = stack;

                expander.SecondaryViewTemplate = new DataTemplate(() =>
                {
                    Grid listStack = new Grid();
                    
                    listView = new ListView();
                    listView.BackgroundColor = Color.FromHex("#232F3E");
                    listView.SetBinding(ListView.ItemsSourceProperty, new Binding("MenuItems"));
                    
                    listView.RowHeight = 60;

                    listView.SeparatorColor = Color.FromHex("#d4d4d4");

                    listView.HasUnevenRows = false;
                    listView.VerticalOptions = LayoutOptions.Start;
                    var pageDataTemplate = new DataTemplate(() =>
                    {
                        var menuStack = new StackLayout
                        {
                            Padding = new Thickness(12, 0, 5, 0),
                            Spacing = 10,
                            Orientation = StackOrientation.Horizontal,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            HorizontalOptions = LayoutOptions.FillAndExpand
                        };

                        var label = new Label
                        {
                            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                           // FontFamily = Device.RuntimePlatform == Device.Android ? "ProximaNova-Semibold.ttf#ProximaNova" : "ProximaNova-Semibold",
                            TextColor = Color.FromHex("#cbd2e7"),
                            VerticalTextAlignment = TextAlignment.Center,
                            HorizontalTextAlignment = TextAlignment.Start,
                            Padding = new Thickness(20, 0, 0, 0)
                        };
                        label.SetBinding(Label.TextProperty, new Binding("Name"));

                        var image1 = new Image
                        {
                            VerticalOptions = LayoutOptions.Center,
                            HorizontalOptions = LayoutOptions.End,
                            HeightRequest = 30,
                            WidthRequest = 30
                        };
                        image1.SetBinding(Image.SourceProperty, new Binding("MenuIcon"));


                        menuStack.Children.Add(image1);
                        menuStack.Children.Add(label);

                        return new ViewCell { View = menuStack };
                    });
                    listView.ItemTemplate = pageDataTemplate;
                    listView.ItemTapped += ListView_ItemTapped;

                    listView.ItemSelected += (sender, e) =>
                    {
                        ((ListView)sender).SelectedItem = null;
                    };
                    listStack.Children.Add(listView);
                    return listStack;
                });

                return expander;
            });

            BindableLayout.SetItemTemplate(expandableList, listItemTemplate);

            expandableList.BackgroundColor = Color.FromHex("#232F3E");


            ScrollView flyoutScroll = new ScrollView();
            flyoutScroll.Content = expandableList;

            Detailsstack.Children.Add(flyoutScroll);

            mainstack.Children.Add(headerstack);
            mainstack.Children.Add(Detailsstack);

            _menuPage.Content = mainstack;

            var navPage = new NavigationPage(_menuPage) { Title = "Menu" };

            Master = navPage;
            NavigationPage.SetHasNavigationBar(_menuPage, false);
            NavigationPage.SetHasBackButton(_menuPage, false);
        }

        private async void Expander_Tapped(object e)
        {
            var selectedItem = ((e as ExpandableView).BindingContext as DashboardMenuItem).MenuName;
            if (selectedItem == "Log out")
            {
                try
                {
                    if (await UserDialogs.Instance.ConfirmAsync("Do you want to logout?", string.Empty, "Yes", "No"))
                    {
                        // await UserLoggedOut();
                        //await DeleteUserLocalDb(AppState.UserCred);
                        Xamarin.Forms.Page startPage = null;
                        FreshNavigationContainer pageContainer = null;
                        startPage = FreshPageModelResolver.ResolvePageModel<LoginPageModel>();
                        pageContainer = new FreshNavigationContainer(startPage, AppConst.AppNavigation);
                        App.Current.MainPage = pageContainer;
                        UserDialogs.Instance.Toast("Logged out successfully");
                    }
                    else
                    {
                        IsPresented = false;
                    }
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.Toast(ex.Message);
                }
                finally
                {
                    UserDialogs.Instance.HideLoading();
                }
            }
            else
            {
                if (Pages.ContainsKey(selectedItem))
                {
                    Detail = Pages[selectedItem];
                    IsPresented = false;
                }
            }
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            MenuItem selectedItem = (MenuItem)e.Item;

            if (Pages.ContainsKey(selectedItem.Name))
            {
                Detail = Pages[selectedItem.Name];
            }

            IsPresented = false;
            listView.SelectedItem = null;
        }

        public async Task UserLoggedOut()
        {
            var user = await _localDatabase.QuerySingle<RmUsers>(u => u.UsrUserName == AppState.UserCred.UsrUserName);

            if (user != null)
            {
                user.IsLoggedIn = false;
                await _localDatabase.InsertOrReplaceAsync(user);
            }
        }

        public async Task DeleteUserLocalDb(RmUsers user)
        {
            try
            {
                var result = await _localDatabase.Delete<RmUsers>(user.UsrUserName);
                await _localDatabase.InsertOrReplaceAsync(result);
            }
            catch (Exception ex)
            {
            }
        }

        public CustomFreshMasterDetailNavigationContainer(string navigationServiceName) : base(navigationServiceName)
        {
        }
    }

    public class DashboardMenuItem : ObservableCollection<MenuItem>
    {
        public string MenuName
        {
            get; set;
        }

        public string MenuIcon
        {
            get; set;
        }

        public ObservableCollection<MenuItem> MenuItems { get; set; }

    }

    public class MenuItem
    {
        public string Name
        {
            get; set;
        }

        public string Icon
        {
            get; set;
        }
    }
}