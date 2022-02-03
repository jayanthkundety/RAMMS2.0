using Plugin.Connectivity;
using RAMMS.MobileApps.PageModel;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using XLabs.Samples.Model;
using Xfx;

namespace RAMMS.MobileApps.Page
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestPage : ContentPage
    {
        public string SelectedRMU { get; set; }

        public string SelectedSection { get; set; }


        public ObservableCollection<DDListItems> DDDeskRMUListItems { get; set; }

        public ObservableCollection<DDListItems> DDSectionListItems { get; set; }


        //IRestApi _restApi;


        private ObservableCollection<TestPerson> _items;
        private Command<string> _searchCommand;
        private Command<TestPerson> _cellSelectedCommand;
        private TestPerson _selectedItem;


        public TestPage()
        {
            InitializeComponent();

            //GetddListDetails("RMU");

            //GetddListDetails("Section Code");

            Items = new ObservableCollection<TestPerson>();
            for (var i = 0; i < 10; i++)
            {
                Items.Add(new TestPerson
                {
                    FirstName = string.Format("FirstName {0}", i),
                    LastName = string.Format("LastName {0}", i)
                });
            }

            //Name = "John Smith Jr.";
            //EmailAddress = "";

        }





        //private static readonly string[] _emails =
        //{
        //    "@gmail.com",
        //    "@hotmail.com",
        //    "@me.com",
        //    "@outlook.com",
        //    "@mail.com",
        //    "@live.com", // does anyone care about this one? haha
        //    "@yahoo.com" // seriously, does anyone use this anymore?
        //};

        //public static readonly BindableProperty EmailAddressProperty = BindableProperty.Create(nameof(EmailAddress),
        //    typeof(string),
        //    typeof(TestPage),
        //    default(string),
        //    propertyChanged: EmailAddressPropertyChanged);

        //public static readonly BindableProperty EmailSuggestionsProperty =
        //    BindableProperty.Create(nameof(EmailSuggestions),
        //        typeof(ObservableCollection<string>),
        //        typeof(TestPage),
        //        new ObservableCollection<string>());

        //public static readonly BindableProperty NameProperty = BindableProperty.Create(nameof(Name),
        //    typeof(string),
        //    typeof(TestPage),
        //    default(string));

        //public static readonly BindableProperty FooProperty = BindableProperty.Create(nameof(Foo),
        //    typeof(string),
        //    typeof(TestPage),
        //    default(string));

        //public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem1),
        //    typeof(object),
        //    typeof(TestPage));

     

        /// <summary>
        ///     Text . This is a bindable property.
        /// </summary>
        //public string EmailAddress
        //{
        //    get => (string)GetValue(EmailAddressProperty);
        //    set => SetValue(EmailAddressProperty, value);
        //}

        /// <summary>
        ///     Email Suggestions collection . This is a bindable property.
        /// </summary>
        //public ObservableCollection<string> EmailSuggestions
        //{
        //    get => (ObservableCollection<string>)GetValue(EmailSuggestionsProperty);
        //    set => SetValue(EmailSuggestionsProperty, value);
        //}

        /// <summary>
        ///     Foo summary. This is a bindable property.
        /// </summary>
        //public string Foo
        //{
        //    get => (string)GetValue(FooProperty);
        //    set
        //    {
        //        SetValue(FooProperty, value);
        //       // ValidateProperty();
        //    }
        //}

        /// <summary>
        ///     Name summary. This is a bindable property.
        /// </summary>
        //public string Name
        //{
        //    get => (string)GetValue(NameProperty);
        //    set
        //    {
        //        SetValue(NameProperty, value);
        //       // ValidateProperty();
        //    }
        //}


        /// <summary>
        ///     SelectedItem summary. This is a bindable property.
        /// </summary>
        //public object SelectedItem1
        //{
        //    get => GetValue(SelectedItemProperty);
        //    set
        //    {
        //        SetValue(SelectedItemProperty, value);
        //        Debug.WriteLine($"Selected Item from ViewModel {value}");
        //    }
        //}

        // you can customize your sorting algorithim to however you want it to work.
        //public Func<string, ICollection<string>, ICollection<string>> SortingAlgorithm { get; } =
        //    (text, values) => values
        //        .Where(x => x.ToLower().StartsWith(text.ToLower()))
        //        .OrderBy(x => x)
        //        .ToList();

        //private static void EmailAddressPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        //{
        //    var model = (TestPage)bindable;
        //    // make sure we have the latest string.
        //    var text = newvalue.ToString();

        //    // if the text is empty or already contains an @ symbol, don't update anything.
        //    if (text.Contains("@")) return;

        //    /*
        //    note, you could use some sort of FastObservableCollection if 
        //    you want the notification to only happen a single time.
        //    i'm not doing that here because I'm trying to K.I.S.S this example
        //    for reference: http://stackoverflow.com/a/13303245/124069
        //   */

        //    // clear the old suggestions, you're starting over. This also can be more efficient, 
        //    // I'll leave that for you to figure out.
        //    model.EmailSuggestions.Clear();

        //    // side note: for loops will add a tiny performance boost over foreach
        //    for (var i = 0; i < _emails.Length; i++)
        //        model.EmailSuggestions.Add($"{text}{_emails[i]}");
        //}

        //private void Email_ItemSelected(object sender, XfxSelectedItemChangedEventArgs args)
        //{
        //    Debug.WriteLine($"Selected Item from Event: {args.SelectedItem}");
        //    Debug.WriteLine($"Selected Item index from Event: {args.SelectedItemIndex}");
        //}

        //public override void ValidateProperty([CallerMemberName] string propertyName = null)
        //{
        //    // I actually recommend using FluentValidation for this
        //    switch (propertyName)
        //    {
        //        case nameof(Name): { ValidateName(); break; }
        //        case nameof(Foo): { ValidateFoo(); break; }
        //    }

        //    IsValid = Errors.Any();
        //    RaiseErrorsChanged(propertyName);
        //}

        //private void ValidateFoo()
        //{
        //    const string nullMessage = "Foo cannot be empty";
        //    var nameMessages = new List<string>();
        //    if (string.IsNullOrEmpty(Foo))
        //    {
        //        nameMessages.Add(nullMessage);
        //        Errors[nameof(Foo)] = nameMessages;
        //    }
        //    else
        //    {
        //        if (Errors.ContainsKey(nameof(Foo)))
        //        {
        //            Errors.Remove(nameof(Foo));
        //        }
        //    }
        //}

        //private void ValidateName()
        //{
        //    const string nullMessage = "Name cannot be empty";
        //    var nameMessages = new List<string>();
        //    if (string.IsNullOrEmpty(Name))
        //    {
        //        nameMessages.Add(nullMessage);
        //        Errors[nameof(Name)] = nameMessages;
        //    }
        //    else
        //    {
        //        if (Errors.ContainsKey(nameof(Name)))
        //        {
        //            Errors.Remove(nameof(Name));
        //        }
        //    }
        //}








        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public ObservableCollection<TestPerson> Items
        {
            
            get
            {
                return _items;
            }
            set
            { 
               _items= value;
            }
}

       

        /// <summary>
        /// Gets the selected cell command.
        /// </summary>
        /// <value>
        /// The selected cell command.
        /// </value>
        public Command<TestPerson> CellSelectedCommand
        {
            get
            {
                return _cellSelectedCommand ?? (_cellSelectedCommand = new Command<TestPerson>(parameter => Debug.WriteLine(parameter.FirstName + parameter.LastName + parameter.Age)));
            }
        }

        /// <summary>
        /// Gets the search command.
        /// </summary>
        /// <value>
        /// The search command.
        /// </value>
        public Command<string> SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command<string>(
                    obj => { },
                    obj => !string.IsNullOrEmpty(obj.ToString())));
            }
        }


        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "<Pending>")]
        public TestPerson SelectedItem
        {
            get
            {
                return _selectedItem;
            }

            set
            {
                _selectedItem = value;
            }

        }

       


        //public async Task<ObservableCollection<DDListItems>> GetddListDetails(string ddtype)
        //{
        //    try
        //    {
        //        //  _userDialogs.ShowLoading("Loading");
        //        if (CrossConnectivity.Current.IsConnected)
        //        {
        //            var ddlist = new DDLookUpDTO()
        //            {
        //                Type = ddtype,
        //            };
        //            var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);



        //            var response = await ;//_restApi.GetDDList(ddlist);

        //            if (response.success)
        //            {

        //                if (ddtype == "RMU")
        //                {
        //                    DDDeskRMUListItems = new ObservableCollection<DDListItems>(response.data);
        //                    return DDDeskRMUListItems;
        //                }

        //                else if (ddtype == "Section Code")
        //                {
        //                    DDSectionListItems = new ObservableCollection<DDListItems>(response.data);
        //                    return DDSectionListItems;
        //                }

        //            }
        //            //else
        //                //_userDialogs.Toast(response.errorMessage);

        //        }
        //        //else
        //            //UserDialogs.Instance.Alert("Please check your Internet Connection !");
        //    }
        //    catch (Exception ex)
        //    {
        //        //_userDialogs.Alert(ex.Message);
        //    }
        //    finally
        //    {
        //        //_userDialogs.HideLoading();
        //    }
        //    return new ObservableCollection<DDListItems>();
        //}





        /// <summary>
        /// Gets the search command.
        /// </summary>
        /// <value>
        /// The search command.
        /// </value>
        //public Command<string> SearchCommand
        //{
        //    get
        //    {
        //        return _searchCommand ?? (_searchCommand = new Command<string>(
        //            obj => { },
        //            obj => !string.IsNullOrEmpty(obj.ToString())));
        //    }
        //}


        //protected override void OnBindingContextChanged()
        //{
        //    base.OnBindingContextChanged();

        //    var vm = (TestViewModel)BindingContext; // Warning, the BindingContext View <-> ViewModel is already set

        //    vm.SignatureFromStream = async () =>
        //    {
        //        if (SignaturePadView.Points.Count() > 0)
        //        {
        //            using (var stream = await SignaturePadView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png))
        //            {
        //                return await ImageConverter.ReadFully(stream);
        //            }
        //        }

        //        return await Task.Run(() => (byte[])null);
        //    };
        //}

    }
}