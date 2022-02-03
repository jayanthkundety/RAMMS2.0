using Acr.UserDialogs;
using FreshMvvm;
using Newtonsoft.Json;
using PCLStorage;
using Plugin.Connectivity;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.MobileApps;
using RAMMS.MobileApps.Controls;
using RAMMS.MobileApps.Helpers;
using RAMMS.MobileApps.Interface;
using RestSharp;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace RAMMS.MobileApps.PageModel
{

    public class Number
    {
        public string numberdata { get; set; }
        //public Color ISBackGroundColor;

        private Color selectedTextColor = Color.White;
        public Color SelectedTextColor
        {
            get
            {
                return selectedTextColor;
            }
            set
            {
                if (selectedTextColor == null)
                {
                    selectedTextColor = Color.Black;
                }
                else
                {
                    selectedTextColor = value;
                }



            }
        }
        public bool Isselcted
        {
            get
            {
                return Isselcted;
            }
            set
            {
                Isselcted = value;

            }
        }


    }


    public class FormsJPageModel : FreshBasePageModel, IFromJDropDown, IFromJAssetDrop, IFromJLandingGrid, IFormDelete

    {
        bool _expandFilter;
        public bool ExpandFilter
        {
            get
            {
                return _expandFilter;
            }
            set
            {
                if (_expandFilter != value)
                {
                    _expandFilter = value;
                }
            }
        }
        int _height;
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                if (_height != value)
                {
                    _height = value;
                }
            }
        }

        /// <summary>
        /// Binding section name label
        /// </summary>
        string _sectionName;
        public string SectionName
        {
            get
            {
                return _sectionName;
            }
            set
            {
                if (_sectionName != value)
                {
                    _sectionName = value;
                }
            }
        }
        /// <summary>
        /// Binding label Roadname
        /// </summary>
        string _roadName;
        public string RoadName
        {
            get
            {
                return _roadName;
            }
            set
            {
                if (_roadName != value)
                {
                    _roadName = value;
                }
            }
        }
        ObservableCollection<FormJSearchGridDTO> _landingGrid;
        public ObservableCollection<FormJSearchGridDTO> LandingGrid
        {
            get
            {
                return _landingGrid;
            }
            set
            {
                if (_landingGrid != value)
                {
                    _landingGrid = value;
                }
            }
        }
        FormJSearchGridDTO _formJSearchGridDTOs;
        public FormJSearchGridDTO FormJSearchGridDTOSelecteItem
        {
            get
            {
                return _formJSearchGridDTOs;
            }
            set
            {
                if (_formJSearchGridDTOs != value)
                {
                    _formJSearchGridDTOs = value;
                    RaisePropertyChanged(nameof(FormJSearchGridDTOSelecteItem));
                }
            }
        }
        List<Number> _pageCount;
        public List<Number> PageCount
        {
            get
            {
                return _pageCount;
            }
            set
            {
                if (_pageCount != value)
                {
                    _pageCount = value;
                }
            }
        }
        Number _PageSelected;
        public Number PageSelected
        {
            get
            {
                return _PageSelected;
            }
            set
            {
                if (_PageSelected != value)
                {
                    // _PageSelected.SelectedTextColor = Color.Blue;
                    _PageSelected = value;
                    if (_PageSelected.numberdata.Equals("Prev"))
                    {
                        if (PageNumber < 1)
                        {
                            GetListBasedOnPageSelection(PageNumber--);
                        }

                    }
                    else if (_PageSelected.numberdata.Equals("Next"))
                    {
                        if (PageNumber > 1)
                        {
                            GetListBasedOnPageSelection(PageNumber++);
                        }
                    }
                    else
                    {
                        PageNumber = Convert.ToInt32(_PageSelected.numberdata);
                        GetListBasedOnPageSelection(PageNumber);

                    }

                }
            }
        }
        string _searchTextItem;
        public string SearchTextItem
        {
            get
            {
                return _searchTextItem;
            }
            set
            {
                if (_searchTextItem != value)
                {
                    _searchTextItem = value;
                }

            }
        }

        string _chinageFromKm;
        public string ChinageFromKm
        {
            get
            {
                return _chinageFromKm;
            }
            set
            {
                if (_chinageFromKm != value)
                {
                    _chinageFromKm = value;
                }

            }
        }
        string _ChinageToKm;
        public string ChinageToKm
        {
            get
            {
                return _ChinageToKm;
            }
            set
            {
                if (_ChinageToKm != value)
                {
                    _ChinageToKm = value;
                }

            }
        }
        string _ChinageFromM;
        public string ChinageFromM
        {
            get
            {
                return _ChinageFromM;
            }
            set
            {
                if (_ChinageFromM != value)
                {
                    _ChinageFromM = value;
                }
            }
        }
        string _ChinageToM;
        public string ChinageToM
        {
            get
            {
                return _ChinageToM;
            }
            set
            {
                if (_ChinageToM != value)
                {
                    _ChinageToM = value;
                }
            }
        }

        public int CollectionViewwidth { get; set; }

        ObservableCollection<RdCode> _dDListItems;
        DropdownEnum G_DropEnum;
        public ObservableCollection<RdCode> DDRodeCodeListItems
        {
            get
            {
                return _dDListItems;
            }
            set
            {
                if (_dDListItems != value)
                {
                    _dDListItems = value;
                    RaisePropertyChanged(nameof(DDRodeCodeListItems));
                }
            }
        }
        RMU rMUselected;
        RdCode rdCodeselected;
        Sections sectionsselected;
        JAssetDropDownData jAssetDropDownDataselected;
        JAssetDropDownData monthDropDownDataselected;
        JAssetDropDownData yearDropDownDataselected;

        private bool isModify;

        private bool isDelete;

        private bool isView;

        public bool IsAdd { get; set; }

        public ObservableCollection<RMU> DDRMUListItems { get; set; }
        public ObservableCollection<Sections> SectionListITems { get; set; }
        public ObservableCollection<JAssetDropDownData> DDAssetTypeListItems { get; set; }


        public ObservableCollection<JAssetDropDownData> MonthListItems { get; set; }
        public ObservableCollection<JAssetDropDownData> YEARListItems { get; set; }
        IRestApi _restApi;
        private ExtendedPicker roadcode, rmucode, assetype, monthpick, yearpicker, sectionpicker;
        private CustomMyPicker scustompicker;
        // ExtendedPicker rmucodeextendedPicker;
        IUserDialogs _userDialogs;
        public FilteredPagingDefinition<FormJSearchGridDTO> GridItems { get; set; }
        int TotalPageCount;
        int RecordPerPage = 10;
        int PageNumber = 0;
        Label lblfirst, lblpagesize, lblTotalSize;
        public FormsJPageModel(IUserDialogs userDialogs, IRestApi restApi)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;

        }
        public string CreateLoginParameter(string rmu, string section, string rdcode)
        {
            var param = new Dictionary<string, string>();
            param.Add("RMU", rmu);
            param.Add("Section", section);
            param.Add("RdCode", rdcode);
            return JsonConvert.SerializeObject(param);

            //  return  string.Format("email={0}&password={1}&device_id={2}", Email, Password, "fdsfsdfsdfsfsdfsdfdsfsdfsdfsdfsdfsdfsdf");
        }
        public string CreateAssetParameter(DropdownEnum dropdownEnum)
        {
            var param = new Dictionary<string, string>();
            if (dropdownEnum == DropdownEnum.Asset)
            {
                param.Add("type", "FormJ_Assets");
            }
            else if (dropdownEnum == DropdownEnum.Month)
            {
                param.Add("type", "Month");
            }
            else
            {
                param.Add("type", "Year");
            }

            return JsonConvert.SerializeObject(param);
        }


        public string CreateSearchParamter()
        {
            var formJSearchBase = new FormJSearchBase
            {
                StartPageNo = 0,
                RecordsPerPage = RecordPerPage
            };
            var filter = new FormJSearchDTO
            {
                Id = null
            };
            formJSearchBase.Filters = filter;

            return JsonConvert.SerializeObject(formJSearchBase);
        }
        void GetListBasedOnPageSelection(int selectedPagenumber)
        {
            var formJSearchBase = new FormJSearchBase
            {
                StartPageNo = selectedPagenumber,
                RecordsPerPage = RecordPerPage
            };
            var filter = new FormJSearchDTO
            {
                Id = null
            };
            formJSearchBase.Filters = filter;

            var param = JsonConvert.SerializeObject(formJSearchBase);
            //ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));

            ThreadPool.QueueUserWorkItem(o => new FormLandingGridIR(this).LandingGridDropDownSucess(param));
        }
        void GetRmuAndsection(string param)
        {
            ThreadPool.QueueUserWorkItem(o => new FormJRequestIR(this).LandingDropDownSucess(param));
        }
        void GetAsset(DropdownEnum dropdownEnum)
        {
            if (DropdownEnum.Asset == dropdownEnum)
            {
                ThreadPool.QueueUserWorkItem(o => new FormAssetIR(this).LandingAssetDropDownSucess(CreateAssetParameter(dropdownEnum)));
            }
            else if (DropdownEnum.Month == dropdownEnum)
            {
                ThreadPool.QueueUserWorkItem(o => new FormAssetIR(this).LandingMonthDropDownSucess(CreateAssetParameter(dropdownEnum)));
            }
            else
            {
                ThreadPool.QueueUserWorkItem(o => new FormAssetIR(this).LandingYearDropDownSucess(CreateAssetParameter(dropdownEnum)));
            }

        }
        void GetLandingGridData()
        {
            if (LandingGrid != null)
            {
                LandingGrid.Clear();
            }
            FullSearch();
            // ThreadPool.QueueUserWorkItem(o => new FormLandingGridIR(this).LandingGridDropDownSucess(CreateSearchParamter()));

        }
        public override void Init(object initData)
        {
            isView = Model.Security.IsView(ModuleNameList.NOD);
            IsAdd = isModify = Model.Security.IsModify(ModuleNameList.NOD);
            isDelete = Model.Security.IsDelete(ModuleNameList.NOD);

        }
        public override void ReverseInit(object returnedData)
        {


        }
        void Setup()
        {
            sectionpicker = CurrentPage.FindByName<ExtendedPicker>("sectionCodePicker");
            roadcode = CurrentPage.FindByName<ExtendedPicker>("rodeCodePicker");
            rmucode = CurrentPage.FindByName<ExtendedPicker>("RMUCodePicker");
            assetype = CurrentPage.FindByName<ExtendedPicker>("assetTypePicker");
            monthpick = CurrentPage.FindByName<ExtendedPicker>("monthPicker");
            yearpicker = CurrentPage.FindByName<ExtendedPicker>("yearpicker");
            scustompicker = CurrentPage.FindByName<CustomMyPicker>("scustompicker");
            lblfirst = CurrentPage.FindByName<Label>("lblfirst");
            lblpagesize = CurrentPage.FindByName<Label>("lblpagesize");
            lblTotalSize = CurrentPage.FindByName<Label>("lblTotalSize");

            roadcode.SelectedIndexChanged += Roadcode_SelectedIndexChanged;
            rmucode.SelectedIndexChanged += Rmucode_SelectedIndexChanged;
            assetype.SelectedIndexChanged += Assetype_SelectedIndexChanged;
            sectionpicker.SelectedIndexChanged += Sectionpicker_SelectedIndexChanged;
            monthpick.SelectedIndexChanged += Monthpick_SelectedIndexChanged;
            yearpicker.SelectedIndexChanged += Yearpicker_SelectedIndexChanged;
            scustompicker.SelectedIndexChanged += Scustompicker_SelectedIndexChanged;
        }

        private void Scustompicker_SelectedIndexChanged(object sender, EventArgs e)
        {

            var objs = sender as CustomMyPicker;
            if (objs.SelectedIndex != -1)
            {
                iValueRet = 1;
            }
            if (objs.SelectedIndex == 0)
            {
                RecordPerPage = 10;
            }
            else if (objs.SelectedIndex == 1)
            {
                RecordPerPage = 25;
            }
            else if (objs.SelectedIndex == 2)
            {
                RecordPerPage = 50;
            }
            else if (objs.SelectedIndex == 3)
            {
                RecordPerPage = 100;
            }
            else if (objs.SelectedIndex == 4)
            {
                RecordPerPage = 500;
            }
            else
            {
                RecordPerPage = TotalPageCount;
            }

            FullSearch();
        }

        private void Yearpicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var items = sender as ExtendedPicker;
            if (items.SelectedIndex == -1)
            {

            }
            else
            {
                yearDropDownDataselected = YEARListItems[items.SelectedIndex];
            }


        }

        private void Monthpick_SelectedIndexChanged(object sender, EventArgs e)
        {
            var items = sender as ExtendedPicker;
            if (items.SelectedIndex == -1)
            {

            }
            else
            {
                monthDropDownDataselected = MonthListItems[items.SelectedIndex];
            }
            // var index = items.SelectedIndex == 0 ? 0 : items.SelectedIndex - 1;

        }

        private void Sectionpicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var items = sender as ExtendedPicker;
            //var index = items.SelectedIndex == 0 ? 0:items.SelectedIndex-1;
            //sectionsselected = SectionListITems[index];


            if (items.SelectedIndex == -1)
            {

            }
            else
            {

                sectionsselected = SectionListITems[items.SelectedIndex];
                SectionName = (string)items.SelectedItem.ToString().Split('-')[1];

            }
        }

        private void Roadcode_SelectedIndexChanged(object sender, EventArgs e)
        {

            var items = sender as ExtendedPicker;
            if (items.SelectedIndex == -1)
            {
            }
            else
            {
                rdCodeselected = DDRodeCodeListItems[items.SelectedIndex];
                RoadName = (string)items.SelectedItem.ToString().Split('-')[1];
            }
        }

        private void Assetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            //  var items = sender as ExtendedPicker;
            //  var index = items.SelectedIndex == 0 ? 0 : items.SelectedIndex - 1;
            //jAssetDropDownDataselected = DDAssetTypeListItems[index];

            var items = sender as ExtendedPicker;
            if (items.SelectedIndex == -1)
            {
            }
            else
            {
                jAssetDropDownDataselected = DDAssetTypeListItems[items.SelectedIndex];
                // RoadName = (string)items.SelectedItem.ToString().Split('-')[1];
            }
        }

        private void Rmucode_SelectedIndexChanged(object sender, EventArgs e)
        {
            // var items = sender as ExtendedPicker;
            // var index = items.SelectedIndex == 0 ? 0 : items.SelectedIndex - 1;

            //RoadName = (string)items.SelectedItem.ToString().Split('-')[1];

            var items = sender as ExtendedPicker;
            if (items.SelectedIndex == -1)
            {
            }
            else
            {
                rMUselected = DDRMUListItems[items.SelectedIndex];
                //jAssetDropDownDataselected = DDAssetTypeListItems[items.SelectedIndex];
                _userDialogs.HideLoading();
                rdCodeselected = null;
                sectionsselected = null;
                monthDropDownDataselected = null;
                yearDropDownDataselected = null;
                roadcode.SelectedIndex = -1;
                sectionpicker.SelectedIndex = -1;
                monthpick.SelectedIndex = -1;
                yearpicker.SelectedIndex = -1;
                SectionName = "";
                RoadName = "";

                //ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));
                GetRmuAndsection(CreateLoginParameter(items.SelectedItem.ToString(), null, ""));
                // RoadName = (string)items.SelectedItem.ToString().Split('-')[1];
            }
        }


        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            //GetRmuAndsection();
            // GetAsset();

            Setup();
            ClearAction.Execute(null);
            // if(AppConst.IsUpdateTrue)
            //  {
            //ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));
            ThreadPool.QueueUserWorkItem(o => GetLandingGridData());
            //  }

        }


        public void GetDropDownDetailResponse(FormJDropDownResponseData responseBase)
        {
            //sectionCodePicker
            if (responseBase.data.Section != null)
            {
                SectionListITems = new ObservableCollection<Sections>(responseBase.data.Section.ToList() as List<Sections>);
                sectionpicker.ItemsSource = SectionListITems.Select((Sections arg) => arg.Text).ToList();
            }
            if (responseBase.data.RMU != null)
            {
                DDRMUListItems = new ObservableCollection<RMU>(responseBase.data.RMU.ToList() as List<RMU>);
                rmucode.ItemsSource = DDRMUListItems.Select((RMU arg) => arg.Text).ToList();
            }

            if (responseBase.data.RdCode != null)
            {
                DDRodeCodeListItems = new ObservableCollection<RdCode>(responseBase.data.RdCode.ToList() as List<RdCode>);
                roadcode.ItemsSource = DDRodeCodeListItems.Select((RdCode arg) => arg.Text).ToList();
            }
            GetAsset(DropdownEnum.Month);
            _userDialogs.HideLoading();


        }
        public void GetDropDownJDetailResponse(JAssetDropDown responseBase)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                DDAssetTypeListItems = new ObservableCollection<JAssetDropDownData>(responseBase.data.ToList() as List<JAssetDropDownData>);
                assetype.ItemsSource = DDAssetTypeListItems.Select((JAssetDropDownData arg) => arg.Text).ToList();

            });

        }
        public void GetDropDownJMonthResponse(JAssetDropDown responseBase)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                MonthListItems = new ObservableCollection<JAssetDropDownData>(responseBase.data.ToList() as List<JAssetDropDownData>);
                monthpick.ItemsSource = MonthListItems.Select((JAssetDropDownData arg) => arg.Text).ToList();
                GetAsset(DropdownEnum.Year);
            });

        }

        public void GetDropDownJYearResponse(JAssetDropDown responseBase)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                YEARListItems = new ObservableCollection<JAssetDropDownData>(responseBase.data.ToList() as List<JAssetDropDownData>);
                yearpicker.ItemsSource = YEARListItems.Select((JAssetDropDownData arg) => arg.Text).ToList();
                GetAsset(DropdownEnum.Asset);
            });

        }
        public void GetFromJLandingGridResponse(FormJLadingGridList responseBase)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    LandingGrid = new ObservableCollection<FormJSearchGridDTO>(responseBase.data.PageResult.ToList() as List<FormJSearchGridDTO>);
                    TotalPageCount = responseBase.data.TotalRecords;
                    lblTotalSize.Text = TotalPageCount.ToString();
                    lblfirst.Text = LandingGrid.Count == 0 ? "0" : (responseBase.data.PageNo + 1).ToString();

                    lblpagesize.Text = (responseBase.data.PageNo + responseBase.data.FilteredRecords).ToString();





                    _userDialogs.HideLoading();
                }
                catch (Exception ex)
                {

                }
            });


        }
        public void APIHitFailed(string error)
        {
            _userDialogs.HideLoading();
        }



        //public ICommand FormAListItemTappedCommand
        //{
        //    //get
        //    //{
        //    //    //return new Command(async (obj) =>
        //    //    //{
        //    //    //    SelectedFormARowItem = (FormAHeaderResponseDTO)obj;
        //    //    //});
        //    //}
        //}
        public ICommand FilterAction
        {
            get
            {
                return new Command(async (obj) =>
                {
                    ExpandFilter = ExpandFilter == true ? false : true;
                    if (YEARListItems == null)
                    {

                        _userDialogs.ShowLoading("Loading");
                        GetRmuAndsection(CreateLoginParameter("", null, ""));


                    }

                });
            }
        }
        public ICommand ClearAction
        {
            get
            {
                return new Command(async (obj) =>
                {
                    // ExpandFilter = ExpandFilter == true ? false : true;
                    roadcode.SelectedIndex = -1;
                    rmucode.SelectedIndex = -1;
                    assetype.SelectedIndex = -1;
                    monthpick.SelectedIndex = -1;
                    yearpicker.SelectedIndex = -1;
                    sectionpicker.SelectedIndex = -1;
                    rMUselected = null;
                    jAssetDropDownDataselected = null;
                    rdCodeselected = null;
                    sectionsselected = null;
                    monthDropDownDataselected = null;
                    yearDropDownDataselected = null;

                    RoadName = "";
                    ChinageFromKm = "";
                    ChinageFromM = "";
                    ChinageToM = "";
                    ChinageToKm = "";
                    SectionName = "";
                    SearchTextItem = "";

                    GetRmuAndsection(CreateLoginParameter("", null, ""));
                    FullSearch();
                });
            }
        }
        public ICommand AddAction
        {
            get
            {
                return new Command(async (obj) =>
                {
                    await CoreMethods.PushPageModel<FormJAddPageModel>();
                });
            }
        }
        void FullSearch()
        {
            // PageNumber = 0;
            //  iValueRet = 1;
            var values = (iValueRet - 1) * RecordPerPage > 0 ? (iValueRet - 1) * RecordPerPage : 0;
            var formJSearchBase = new FormJSearchBase
            {
                StartPageNo = values,
                RecordsPerPage = RecordPerPage,
                sortOrder = SortOrder,
                ColumnIndex = ColumnIndex
            };
            if (SearchTextItem != null && SearchTextItem != "")
                SearchTextItem = SearchTextItem.Trim();
            var filter = new FormJSearchDTO();
            filter.SmartInputValue = SearchTextItem ?? "";
            filter.Id = null;

            string rmuSearchcode = string.Empty;
            if (rMUselected != null && rMUselected.Value == "MIRI")
                rmuSearchcode = "MRI";
            if (rMUselected != null && rMUselected.Value == "Batu Niah")
                rmuSearchcode = "BTN";

            filter.RMU = rMUselected == null ? null : rmuSearchcode;
            filter.Road_Code = rdCodeselected == null ? null : rdCodeselected.Value;
            filter.Section = sectionsselected == null ? null : sectionsselected.Value ?? "";
            filter.Asset_GroupCode = jAssetDropDownDataselected == null ? null : jAssetDropDownDataselected.Value ?? "";
            if (monthDropDownDataselected == null)
            {
                filter.Month = null;
            }
            else
            {
                filter.Month = Convert.ToInt32(monthDropDownDataselected.Value);
            }
            if (yearDropDownDataselected == null)
            {
                filter.Year = null;
            }
            else
            {
                filter.Year = Convert.ToInt32(yearDropDownDataselected.Value);
            }

            if (string.IsNullOrEmpty(ChinageFromKm))
            {
                filter.ChinageFromKm = null;
            }
            else
            {
                filter.ChinageFromKm = Convert.ToInt32(ChinageFromKm);
            }

            if (string.IsNullOrEmpty(ChinageToKm))
            {
                filter.ChinageToKm = null;
            }
            else
            {
                filter.ChinageToKm = Convert.ToInt32(ChinageToKm);
            }

            if (string.IsNullOrEmpty(ChinageFromM))
            {
                filter.ChinageFromM = null;
            }
            else
            {
                filter.ChinageFromM = Convert.ToInt32(ChinageFromM);
            }
            if (string.IsNullOrEmpty(ChinageToM))
            {
                filter.ChinageToM = null;
            }
            else
            {
                filter.ChinageToM = Convert.ToInt32(ChinageToM);
            }


            //filter.ChinageToKm = ChinageToKm == null ? null :Convert.ToInt32(ChinageToKm);
            //filter.ChinageFromM = ChinageFromM == null ? null : Convert.ToInt32( ChinageFromM);
            //filter.ChinageToM = ChinageToM == null ? null : Convert.ToInt32(ChinageToM );
            //filter.searchString = SearchTextItem ?? "";


            formJSearchBase.Filters = filter;

            var param = JsonConvert.SerializeObject(formJSearchBase);
            //ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));

            ThreadPool.QueueUserWorkItem(o => new FormLandingGridIR(this).LandingGridDropDownSucess(param));
        }

        public void DeleteHeader(GetSerialNo getSerialNo)
        {
            if (getSerialNo.success)
            {

                LandingGrid.Remove(FormJSearchGridDTOSelecteItem);
                _userDialogs.HideLoading();
            }
        }

        public ICommand SearchAction
        {
            get
            {
                return new Command(async (obj) =>
                {
                    iValueRet = 1;
                    FullSearch();
                    // _userDialogs.ShowLoading("Loading");
                    //if (!string.IsNullOrEmpty(SmartSearch))
                    //{
                    //    await GetMyFormAListReports("Smartsearch");
                    //}
                    //else
                    //{
                    //    await GetMyFormAListReports("Detailsearch");
                    //}
                });
            }
        }
        public ICommand btnPreviousCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    if (iValueRet > 1)
                        iValueRet = iValueRet - 1;

                    FullSearch();
                    //await ButtonPagination("Smartsearch", iValueRet);
                });
            }
        }
        int iValueRet = 1;
        public ICommand btnNextCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    if (iValueRet <= (Convert.ToInt32(TotalPageCount) / RecordPerPage))

                        iValueRet = iValueRet + 1;

                    FullSearch();
                    // lblfirst.Text =((iValueRet*RecordPerPage)+1).ToString();
                    // lblpagesize.Text = (iValueRet+1 * RecordPerPage).ToString();
                    //  await ButtonPagination("Smartsearch", iValueRet);

                });
            }
        }

        public ICommand ClickMeActionCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    var SelectedFormARowItem = (FormJSearchGridDTO)obj;
                    var actionResult = "";

                    string view = isView ? "View" : "";
                    string delete = isDelete ? "Delete" : "";
                    string edit = isModify && SelectedFormARowItem.Status.ToString().ToLower() != "submitted" ? "Edit" : "";

                    string[] permissions = new string[] { edit, view, delete };
                    permissions = permissions.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                    actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, permissions);

                    if (actionResult == "Delete")
                    {
                        var confirms = new ConfirmConfig()
                        {
                            Message = AppConst.AreYouSureWantToDelete,
                            OkText = "Delete",
                            CancelText = "Cancel",
                            OnAction = (result) =>
                            {
                                if (result)
                                {
                                    // _mapper.Map(TagListViewModel.SelectedItem, ActiveTag);
                                    FormJSearchGridDTOSelecteItem = SelectedFormARowItem;
                                    //ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));
                                    RestRequest restRequest = new RestRequest();
                                    restRequest.AddQueryParameter("formJHdr", SelectedFormARowItem.No.ToString());
                                    ThreadPool.QueueUserWorkItem(o => new FormDelete(this).DeteleHeaderAndDetail(restRequest));
                                }
                                else
                                {
                                    // TagListViewModel.RevertSelectedItemChanged();
                                }
                            }

                        };
                        _userDialogs.Confirm(confirms);


                    }
                    if (actionResult == "Edit")
                    {
                        var formj = new Dictionary<string, object>();
                        formj.Add("data", SelectedFormARowItem);
                        await CoreMethods.PushPageModel<FormJAddPageModel>(formj);
                    }
                    else if (actionResult == "View")
                    {
                        var formj = new Dictionary<string, object>();
                        formj.Add("data", SelectedFormARowItem);
                        formj.Add("view", "view");
                        await CoreMethods.PushPageModel<FormJAddPageModel>(formj);
                        //editViewModel.Type = "View";
                        //editViewModel.HdrFahPkRefNo = SelectedFormARowItem.No;
                        ////editViewModel.HdrFahPkRefNo = SelectedFormARowItem.FahRmu;

                        //await CoreMethods.PushPageModel<FormADetailsPageModel>(editViewModel);
                    }
                });
            }
        }

        public string SortOrder { get; set; } = "0";
        public bool IsRefNoAssending { get; set; } = false;
        public bool IsRMUAssending { get; set; } = false;
        public bool IsRMUNameAssending { get; set; } = false;
        public bool IsSecCodeAssending { get; set; } = false;
        public bool IsSecNameAssending { get; set; } = false;
        public bool IsMonthYearAssending { get; set; } = false;
        public bool IsRoadCodeAssending { get; set; } = false;
        public bool IsAssetGrpAssending { get; set; } = false;
        public bool IsStatusAssending { get; set; } = false;
        public bool IsOwnerAssending { get; set; } = false;
        public bool IsVerifiedByAssending { get; set; } = false;

        private void GetSortOrder(int columnIndex)
        {
            if (columnIndex == 2)
                SortOrder = IsRefNoAssending ? "1" : "0";
            if (columnIndex == 3)
                SortOrder = IsRMUAssending ? "1" : "0";
            if (columnIndex == 4)
                SortOrder = IsRMUNameAssending ? "1" : "0";
            if (columnIndex == 5)
                SortOrder = IsSecCodeAssending ? "1" : "0";
            if (columnIndex == 6)
                SortOrder = IsSecNameAssending ? "1" : "0";
            if (columnIndex == 7)
                SortOrder = IsMonthYearAssending ? "1" : "0";
            if (columnIndex == 8)
                SortOrder = IsRoadCodeAssending ? "1" : "0";
            if (columnIndex == 9)
                SortOrder = IsAssetGrpAssending ? "1" : "0";
            if (columnIndex == 10)
                SortOrder = IsStatusAssending ? "1" : "0";
            if (columnIndex == 11)
                SortOrder = IsOwnerAssending ? "1" : "0";
            if (columnIndex == 12)
                SortOrder = IsVerifiedByAssending ? "1" : "0";
        }
        private void SetSortOrder(int columnIndex)
        {
            if (columnIndex == 2)
            {
                IsRefNoAssending = !IsRefNoAssending;

                IsRMUAssending = false;
                IsRMUNameAssending = false;
                IsSecCodeAssending = false;
                IsSecNameAssending = false;
                IsMonthYearAssending = false;
                IsRoadCodeAssending = false;
                IsAssetGrpAssending = false;
                IsStatusAssending = false;
                IsOwnerAssending = false;
                IsVerifiedByAssending = false;
            }
            else if (columnIndex == 3)
            {
                IsRMUAssending = !IsRMUAssending;

                IsRefNoAssending = false;
                IsRMUNameAssending = false;
                IsSecCodeAssending = false;
                IsSecNameAssending = false;
                IsMonthYearAssending = false;
                IsRoadCodeAssending = false;
                IsAssetGrpAssending = false;
                IsStatusAssending = false;
                IsOwnerAssending = false;
                IsVerifiedByAssending = false;
            }
            else if (columnIndex == 4)
            {
                IsRMUNameAssending = !IsRMUNameAssending;

                IsRefNoAssending = false;
                IsRMUAssending = false;
                IsSecCodeAssending = false;
                IsSecNameAssending = false;
                IsMonthYearAssending = false;
                IsRoadCodeAssending = false;
                IsAssetGrpAssending = false;
                IsStatusAssending = false;
                IsOwnerAssending = false;
                IsVerifiedByAssending = false;
            }
            else if (columnIndex == 5)
            {
                IsSecCodeAssending = !IsSecCodeAssending;

                IsRefNoAssending = false;
                IsRMUAssending = false;
                IsRMUNameAssending = false;
                IsSecNameAssending = false;
                IsMonthYearAssending = false;
                IsRoadCodeAssending = false;
                IsAssetGrpAssending = false;
                IsStatusAssending = false;
                IsOwnerAssending = false;
                IsVerifiedByAssending = false;
            }
            else if (columnIndex == 6)
            {
                IsSecNameAssending = !IsSecNameAssending;

                IsRefNoAssending = false;
                IsRMUAssending = false;
                IsRMUNameAssending = false;
                IsSecCodeAssending = false;
                IsMonthYearAssending = false;
                IsRoadCodeAssending = false;
                IsAssetGrpAssending = false;
                IsStatusAssending = false;
                IsOwnerAssending = false;
                IsVerifiedByAssending = false;
            }
            else if (columnIndex == 7)
            {
                IsMonthYearAssending = !IsMonthYearAssending;

                IsRefNoAssending = false;
                IsRMUAssending = false;
                IsRMUNameAssending = false;
                IsSecCodeAssending = false;
                IsSecNameAssending = false;
                IsRoadCodeAssending = false;
                IsAssetGrpAssending = false;
                IsStatusAssending = false;
                IsOwnerAssending = false;
                IsVerifiedByAssending = false;
            }
            else if (columnIndex == 8)
            {
                IsRoadCodeAssending = !IsRoadCodeAssending;

                IsRefNoAssending = false;
                IsRMUAssending = false;
                IsRMUNameAssending = false;
                IsSecCodeAssending = false;
                IsSecNameAssending = false;
                IsMonthYearAssending = false;
                IsAssetGrpAssending = false;
                IsStatusAssending = false;
                IsOwnerAssending = false;
                IsVerifiedByAssending = false;
            }
            else if (columnIndex == 9)
            {
                IsAssetGrpAssending = !IsAssetGrpAssending;

                IsRefNoAssending = false;
                IsRMUAssending = false;
                IsRMUNameAssending = false;
                IsSecCodeAssending = false;
                IsSecNameAssending = false;
                IsMonthYearAssending = false;
                IsRoadCodeAssending = false;
                IsStatusAssending = false;
                IsOwnerAssending = false;
                IsVerifiedByAssending = false;
            }
            else if (columnIndex == 10)
            {
                IsStatusAssending = !IsStatusAssending;

                IsRefNoAssending = false;
                IsRMUAssending = false;
                IsRMUNameAssending = false;
                IsSecCodeAssending = false;
                IsSecNameAssending = false;
                IsMonthYearAssending = false;
                IsRoadCodeAssending = false;
                IsAssetGrpAssending = false;
                IsOwnerAssending = false;
                IsVerifiedByAssending = false;
            }
            else if (columnIndex == 11)
            {
                IsOwnerAssending = !IsOwnerAssending;

                IsRefNoAssending = false;
                IsRMUAssending = false;
                IsRMUNameAssending = false;
                IsSecCodeAssending = false;
                IsSecNameAssending = false;
                IsMonthYearAssending = false;
                IsRoadCodeAssending = false;
                IsAssetGrpAssending = false;
                IsStatusAssending = false;
                IsVerifiedByAssending = false;
            }
            else if (columnIndex == 12)
            {
                IsVerifiedByAssending = !IsVerifiedByAssending;

                IsRefNoAssending = false;
                IsRMUAssending = false;
                IsRMUNameAssending = false;
                IsSecCodeAssending = false;
                IsSecNameAssending = false;
                IsMonthYearAssending = false;
                IsRoadCodeAssending = false;
                IsAssetGrpAssending = false;
                IsStatusAssending = false;
                IsOwnerAssending = false;
            }
            else
            {
                IsRefNoAssending = false;
                IsRMUAssending = false;
                IsRMUNameAssending = false;
                IsSecCodeAssending = false;
                IsSecNameAssending = false;
                IsMonthYearAssending = false;
                IsRoadCodeAssending = false;
                IsAssetGrpAssending = false;
                IsStatusAssending = false;
                IsOwnerAssending = false;
                IsVerifiedByAssending = false;
            }
        }
        public int ColumnIndex { get; set; }
        public ICommand SortingCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    try
                    {
                        if (CrossConnectivity.Current.IsConnected)
                        {
                            ColumnIndex = Convert.ToInt32(obj);
                            GetSortOrder(ColumnIndex);
                            var values = (iValueRet - 1) * RecordPerPage > 0 ? (iValueRet - 1) * RecordPerPage : 0;
                            var formJSearchBase = new FormJSearchBase
                            {
                                StartPageNo = 0,
                                RecordsPerPage = RecordPerPage,
                                sortOrder = SortOrder,
                                ColumnIndex = ColumnIndex
                            };
                            var filter = new FormJSearchDTO();
                            if (SearchTextItem != null && SearchTextItem != "")
                                SearchTextItem = SearchTextItem.Trim();
                            filter.SmartInputValue = SearchTextItem ?? "";
                            filter.Id = null;

                            string rmuSearchcode = string.Empty;
                            if (rMUselected != null && rMUselected.Value == "MIRI")
                                rmuSearchcode = "MRI";
                            if (rMUselected != null && rMUselected.Value == "Batu Niah")
                                rmuSearchcode = "BTN";

                            filter.RMU = rMUselected == null ? null : rmuSearchcode;
                            filter.Road_Code = rdCodeselected == null ? null : rdCodeselected.Value;
                            filter.Section = sectionsselected == null ? null : sectionsselected.Value ?? "";
                            filter.Asset_GroupCode = jAssetDropDownDataselected == null ? null : jAssetDropDownDataselected.Value ?? "";
                            if (monthDropDownDataselected == null)
                            {
                                filter.Month = null;
                            }
                            else
                            {
                                filter.Month = Convert.ToInt32(monthDropDownDataselected.Value);
                            }
                            if (yearDropDownDataselected == null)
                            {
                                filter.Year = null;
                            }
                            else
                            {
                                filter.Year = Convert.ToInt32(yearDropDownDataselected.Value);
                            }

                            if (string.IsNullOrEmpty(ChinageFromKm))
                            {
                                filter.ChinageFromKm = null;
                            }
                            else
                            {
                                filter.ChinageFromKm = Convert.ToInt32(ChinageFromKm);
                            }

                            if (string.IsNullOrEmpty(ChinageToKm))
                            {
                                filter.ChinageToKm = null;
                            }
                            else
                            {
                                filter.ChinageToKm = Convert.ToInt32(ChinageToKm);
                            }

                            if (string.IsNullOrEmpty(ChinageFromM))
                            {
                                filter.ChinageFromM = null;
                            }
                            else
                            {
                                filter.ChinageToKm = Convert.ToInt32(ChinageFromM);
                            }
                            if (string.IsNullOrEmpty(ChinageToM))
                            {
                                filter.ChinageToM = null;
                            }
                            else
                            {
                                filter.ChinageToM = Convert.ToInt32(ChinageToM);
                            }

                            formJSearchBase.Filters = filter;
                            var param = JsonConvert.SerializeObject(formJSearchBase);
                            ThreadPool.QueueUserWorkItem(o => new FormLandingGridIR(this).LandingGridDropDownSucess(param));
                            SetSortOrder(ColumnIndex);
                            iValueRet = 1;
                        }
                    }
                    catch (Exception ex)
                    {
                        _userDialogs.Alert(ex.Message);
                    }
                    finally
                    {
                    }

                });
            }
        }

    }
}
