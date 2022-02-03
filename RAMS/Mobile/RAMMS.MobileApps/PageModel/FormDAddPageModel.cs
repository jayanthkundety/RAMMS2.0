using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.MobileApps.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using SignaturePad.Forms;
using System.IO;
using RAMMS.MobileApps.Page;
using System.Globalization;

namespace RAMMS.MobileApps.PageModel
{


    public class FormDAddPageModel : FreshBasePageModel
    {

        private IRestApi _restApi;

        public IUserDialogs _userDialogs;

        public ILocalDatabase _localDatabase;

        public bool ViewType { get; set; }

        public int iResultValue { get; set; }

        public string StrRefcode { get; set; }

        public bool IsEmpty { get; set; }

        public float LstViewHeightRequest { get; set; }

        private int? iRet { get; set; }

        public float SearchbarHeightRequest { get; set; }

        public ListView FormAGridListview { get; set; }

        public ListView FormAMaterialGridListview { get; set; }

        public bool HeaderControlEnabled { get; set; }

        public bool IsControlEnabled { get; set; }

        public ListView FormAEquimentGridListview { get; set; }


        public ListView FormADetailGridListview { get; set; }


        public ListView FormAFlatDetailGridListview { get; set; }


        public FormDHeaderRequestDTO SelectedHdrEditItem { get; set; }


        public UserResponseDTO SelectedUserItem { get; set; }


        public UserResponseDTO SelectedVerUserItem { get; set; }


        public FormDHeaderResponseDTO SelectedFormARowItem { get; set; }

        public FileImageSource ImageSource { get; set; }


        //signature Controls
        SignaturePadView recsignview, vetsignview, versignview, soversignview, soprosignview, soagreesignview;


        ExtendedPicker recnamepicker, vetnamepicker, vernamepicker, sovernamepicker, sopronamepicker, soagreenamepicker;


        int? recuserid, vetuserid, veruserid, soveruserid, soprouserid, soagreeuserid;

        private Xamarin.Forms.ImageSource recsignimage { get; set; }
        private Xamarin.Forms.ImageSource vetsignimage { get; set; }

        private Xamarin.Forms.ImageSource versignimage { get; set; }
        private Xamarin.Forms.ImageSource soversignimage { get; set; }

        private Xamarin.Forms.ImageSource soprosignimage { get; set; }
        private Xamarin.Forms.ImageSource soagreesignimage { get; set; }


        public string selectedrecdate { get; set; }

        public string selectedvetdate { get; set; }

        public string selectedverdate { get; set; }


        public string selectedsoverdate { get; set; }


        public string selectedsoprodate { get; set; }


        public string selectedsoagreedate { get; set; }


        public string reccode { get; set; }
        public string recdesg { get; set; }

        public string vetcode { get; set; }
        public string vetdesg { get; set; }

        public string vercode { get; set; }
        public string verdesg { get; set; }

        public string sovercode { get; set; }
        public string soverdesg { get; set; }

        public string soprocode { get; set; }
        public string soprodesg { get; set; }

        public string soagreecode { get; set; }
        public string soagreedesg { get; set; }


        CustomDatePicker recdate, vetdate, verdate, soverdate, soprodate, soagreedate;

        Button btnSubmit, btnSave, btnCancel, btnDetailSave, btnFind;

        EntryControl enctrlreccode, ectrlvetcode, entrlvercode, entrlsovercode,
            entrlsoprocode, entrlsoagreecode, etrlrecdesg, ectrlvetdeg, entrlverdesg, entrlsoverdesg, entrlsoprodesg, soagrees;

        private ObservableCollection<DDListItems> DDRecNameListItems { get; set; }
        private ObservableCollection<DDListItems> DDvetnameListItems { get; set; }
        private ObservableCollection<DDListItems> DDvernameListItems { get; set; }
        private ObservableCollection<DDListItems> DDsovernameListItems { get; set; }
        private ObservableCollection<DDListItems> DDpronameListItems { get; set; }
        private ObservableCollection<DDListItems> DDsoagreenameListItems { get; set; }

        public string SelectedRecName { get; set; }
        public string SelectedVetName { get; set; }
        public string SelectedVerName { get; set; }
        public string SelectedsoverName { get; set; }
        public string SelectedproName { get; set; }
        public string SelectedsoagreeName { get; set; }


        //***********************************************

        public int SelectedSection { get; set; }

        public string strRefNo { get; set; }

        public string Selectedinspuser { get; set; }

        public FormDHeaderRequestDTO objValue { get; set; }

        public ObservableCollection<DDListItems> DDMonthListItems { get; set; }

        public ObservableCollection<DDListItems> DDYearListItems { get; set; }

        public ObservableCollection<DDListItems> DDInspUserListListItems { get; set; }

        public List<DropDown> RMUList { get; set; }

        public int GetHeaderNoCode { get; set; }


        private string totalsize;
        private string pagesize;
        private int startPage;

        public string LbrTotalSize { get; set; }

        public string LbrPageSize { get; set; }

        public int LbrStartPage { get; set; }

        public string MtrlTotalSize { get; set; }

        public string MtrlPageSize { get; set; }

        public int MtrlStartPage { get; set; }

        public string DtlTotalSize { get; set; }

        public string DtlPageSize { get; set; }

        public int DtlStartPage { get; set; }

        public string TotalSize
        {
            get
            {
                return totalsize;
            }
            set
            {
                totalsize = value;
                RaisePropertyChanged("TotalSize");
            }
        }

        public string PageSize
        {
            get
            {
                return pagesize;
            }
            set
            {
                pagesize = value;
                RaisePropertyChanged("PageSize");
            }
        }

        public int StartPage
        {
            get
            {
                return startPage;
            }
            set
            {
                startPage = value;
                RaisePropertyChanged("StartPage");
            }
        }

        public string SelectedRoadCode { get; set; }
        public string SelectedRMU { get; set; }
        public string SelectedWeekNo { get; set; }

        public string SelectedWeekDay { get; set; }

        public string SelectWeekday { get; set; }
        public int? SelectedMonth { get; set; }
        public int? SelectedYear { get; set; }
        public string SmartSearch { get; set; }

        public EditViewModel _editViewModel { get; set; }

        public FilteredPagingDefinition<FormDSearchGridDTO> GridItems { get; set; }

        public ObservableCollection<FormDLabourDetailsResponseDTO> MyLabourBaseFormDList { get; set; }

        public ObservableCollection<FormDEquipDetailsResponseDTO> MyEquimentBaseFormDList { get; set; }

        public ObservableCollection<FormDMaterialDetailsResponseDTO> MyMaterialBaseFormDList { get; set; }

        public ObservableCollection<FormDDetailsResponseDTO> MyDetailBaseFormDList { get; set; }



        public ObservableCollection<DDListItems> WeekListItems { get; set; }
        public ObservableCollection<DDListItems> WeekdayListItems { get; set; }

        private SearchBar searchBar;

        private int lbrpageno = 10;
        private int mtrlpageno = 10;
        private int eqpmtpageno = 10;
        private int dtlpageno = 10;

        private CustomMyPicker cuspicker, custequipicker, custmaterialpicker, custdetailpicker;

        private Frame advanceFrame;

        private ExtendedPicker sectioncode, rmucode, assetype, monthpick, sectionpick, yearpick, weekdaypick, weekpick, crewpick, userinspcode;
        private EntryControl enctrlMonth, enctrlYear, enctrlTKM, enctrlTM, enctrlSection, enctrlRoadName, enctrlDivision, entrlsupervisor;

        DatePicker dtDate;

        Button btnLabourCommand, btnEquipment, btnMaterial, btnDetail, btnDetailX;

        Image btnLabourCommandX, btnEquipmentX, btnMaterialX;

        public string strsectionname { get; set; }
        public string strRMUCode { get; set; }
        public string strDivision { get; set; }
        public string strSupervisor { get; set; }

        public int LbrPageNo { get; set; }
        public int MtrlPageNo { get; set; }
        public int EqpmtPageNo { get; set; }
        public int DtlPageNo { get; set; }


        DateTime dt { get; set; }

        private ObservableCollection<DDListItems> DDRodeCodeListItems { get; set; }

        private ObservableCollection<DDListItems> DDRMUListItems { get; set; }

        public ICommand LbrNextCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    int iRet = (Convert.ToInt32(LbrTotalSize) - 1) / lbrpageno;
                    if (LbrPageNo < iRet)
                    {
                        LbrPageNo = LbrPageNo + 1;
                        await GetMyFormDLabourListReports(GetHeaderNoCode.ToString(), LbrPageNo);
                    }

                });
            }
        }

        public ICommand MtrlNextCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    int iRet = (Convert.ToInt32(MtrlTotalSize) - 1) / mtrlpageno;
                    if (MtrlPageNo < iRet)
                    {
                        MtrlPageNo = MtrlPageNo + 1;
                        await GetMyFormDMaterialListReports(GetHeaderNoCode.ToString(), MtrlPageNo);
                    }

                });
            }
        }

        public ICommand EqmtNextCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    int iRet = (Convert.ToInt32(TotalSize) - 1) / eqpmtpageno;
                    if (EqpmtPageNo < iRet)
                    {
                        EqpmtPageNo = EqpmtPageNo + 1;
                        await GetMyFormDEquipmentListReports(GetHeaderNoCode.ToString(), EqpmtPageNo);
                    }

                });
            }
        }

        public ICommand DtlNextCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    int iRet = (Convert.ToInt32(DtlTotalSize) - 1) / dtlpageno;
                    if (DtlPageNo < iRet)
                    {
                        DtlPageNo = DtlPageNo + 1;
                        await GetMyFormDDetailListReports(GetHeaderNoCode.ToString(), DtlPageNo);
                    }

                });
            }
        }

        public ICommand LbrPrevCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    if (LbrPageNo >= 1)
                    {
                        LbrPageNo = LbrPageNo - 1;
                        await GetMyFormDLabourListReports(GetHeaderNoCode.ToString(), LbrPageNo);
                    }

                });
            }
        }

        public ICommand MtrlPrevCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    if (MtrlPageNo >= 1)
                    {
                        MtrlPageNo = MtrlPageNo - 1;
                        await GetMyFormDMaterialListReports(GetHeaderNoCode.ToString(), MtrlPageNo);
                    }

                });
            }
        }

        public ICommand EqmtPrevCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    if (EqpmtPageNo >= 1)
                    {
                        EqpmtPageNo = EqpmtPageNo - 1;
                        await GetMyFormDEquipmentListReports(GetHeaderNoCode.ToString(), EqpmtPageNo);
                    }

                });
            }
        }

        public ICommand DtlPrevCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    if (DtlPageNo >= 1)
                    {
                        DtlPageNo = DtlPageNo - 1;
                        await GetMyFormDDetailListReports(GetHeaderNoCode.ToString(), DtlPageNo);
                    }

                });
            }
        }

        public FormDAddPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;

            _restApi = restApi;
            _restApi = restApi;

            _localDatabase = localDatabase;


        }

        public override async void Init(object initData)
        {
            base.Init(initData);

            ImageSource = "RoundedAddIcon.png";
            _editViewModel = initData as EditViewModel;

            //if (App.DetailType == "Add")
            //{
            //    _editViewModel.Type = App.DetailType;
            //    _editViewModel.HdrFahPkRefNo = App.DetailHeaderCode;

            //}
            //else
            //{
            //    App.DetailType = "";

            //    App.DetailHeaderCode = 0;

            //    _editViewModel.Type = _editViewModel.Type;

            //    _editViewModel.HdrFahPkRefNo = _editViewModel.HdrFahPkRefNo;


            //}

            btnSave = CurrentPage.FindByName<Button>("btnSave");


            btnCancel = CurrentPage.FindByName<Button>("btnCancel");

            btnSubmit = CurrentPage.FindByName<Button>("btnSubmit");

            if (_editViewModel.Type == "Add")
            {
                btnSave.IsVisible = false;
                btnSubmit.IsVisible = false;
            }

            btnDetailSave = CurrentPage.FindByName<Button>("btnDetailSave");

            dtDate = CurrentPage.FindByName<DatePicker>("datepick");

            enctrlMonth = CurrentPage.FindByName<EntryControl>("enctrlMonth");

            enctrlDivision = CurrentPage.FindByName<EntryControl>("enctrlDivision");

            entrlsupervisor = CurrentPage.FindByName<EntryControl>("entrlsupervisor");


            enctrlYear = CurrentPage.FindByName<EntryControl>("enctrlYear");

            searchBar = CurrentPage.FindByName<SearchBar>("formAsearchBar");

            cuspicker = CurrentPage.FindByName<CustomMyPicker>("scustompicker");

            custequipicker = CurrentPage.FindByName<CustomMyPicker>("equimentcustompicker");

            custmaterialpicker = CurrentPage.FindByName<CustomMyPicker>("materialcustompicker");

            custdetailpicker = CurrentPage.FindByName<CustomMyPicker>("detailcustompicker");

            rmucode = CurrentPage.FindByName<ExtendedPicker>("RMUCodePicker");

            assetype = CurrentPage.FindByName<ExtendedPicker>("assetTypePicker");

            sectioncode = CurrentPage.FindByName<ExtendedPicker>("sectionCodePicker");

            sectionpick = CurrentPage.FindByName<ExtendedPicker>("SectionPicker");

            yearpick = CurrentPage.FindByName<ExtendedPicker>("yearpicker");

            weekdaypick = CurrentPage.FindByName<ExtendedPicker>("weekdaypicker");

            weekpick = CurrentPage.FindByName<ExtendedPicker>("weekpicker");

            crewpick = CurrentPage.FindByName<ExtendedPicker>("crewpicker");

            //Signature Controls
            recnamepicker = CurrentPage.FindByName<ExtendedPicker>("recnamepicker");

            vetnamepicker = CurrentPage.FindByName<ExtendedPicker>("vetnamepicker");

            vernamepicker = CurrentPage.FindByName<ExtendedPicker>("vernamepicker");

            sovernamepicker = CurrentPage.FindByName<ExtendedPicker>("sovernamepicker");

            sopronamepicker = CurrentPage.FindByName<ExtendedPicker>("sopronamepicker");

            soagreenamepicker = CurrentPage.FindByName<ExtendedPicker>("soagreenamepicker");

            recdate = CurrentPage.FindByName<CustomDatePicker>("recdate");

            vetdate = CurrentPage.FindByName<CustomDatePicker>("vetdate");

            verdate = CurrentPage.FindByName<CustomDatePicker>("verdate");

            soverdate = CurrentPage.FindByName<CustomDatePicker>("soverdate");

            soprodate = CurrentPage.FindByName<CustomDatePicker>("soprodate");

            soagreedate = CurrentPage.FindByName<CustomDatePicker>("soagreedate");

            userinspcode = CurrentPage.FindByName<ExtendedPicker>("crewpicker");

            yearpick = CurrentPage.FindByName<ExtendedPicker>("yearPicker");


            btnLabourCommand = CurrentPage.FindByName<Button>("btnLabourCommand");

            btnEquipment = CurrentPage.FindByName<Button>("btnEquipment");

            btnMaterial = CurrentPage.FindByName<Button>("btnMaterial");

            btnDetail = CurrentPage.FindByName<Button>("btnAddCtl");

            btnFind = CurrentPage.FindByName<Button>("btnFind");


            btnLabourCommandX = CurrentPage.FindByName<Image>("btnLabourCommandX");

            btnEquipmentX = CurrentPage.FindByName<Image>("btnEquipmentX");

            btnMaterialX = CurrentPage.FindByName<Image>("btnMaterialX");

            btnDetailX = CurrentPage.FindByName<Button>("btnAddCtlX");

            enctrlreccode = CurrentPage.FindByName<EntryControl>("enctrlreccode");

            ectrlvetcode = CurrentPage.FindByName<EntryControl>("ectrlvetcode");

            ectrlvetdeg = CurrentPage.FindByName<EntryControl>("ectrlvetdeg");

            entrlvercode = CurrentPage.FindByName<EntryControl>("entrlvercode");

            entrlverdesg = CurrentPage.FindByName<EntryControl>("entrlverdesg");

            entrlsovercode = CurrentPage.FindByName<EntryControl>("entrlsovercode");

            entrlsoverdesg = CurrentPage.FindByName<EntryControl>("entrlsoverdesg");

            entrlsoprocode = CurrentPage.FindByName<EntryControl>("entrlsoprocode");

            entrlsoprodesg = CurrentPage.FindByName<EntryControl>("entrlsoprodesg");

            entrlsoagreecode = CurrentPage.FindByName<EntryControl>("entrlsoagreecode");

            soagrees = CurrentPage.FindByName<EntryControl>("soagrees");

            etrlrecdesg = CurrentPage.FindByName<EntryControl>("etrlrecdesg");


            advanceFrame = CurrentPage.FindByName<Frame>("Toggle");

            var page_searchbar = CurrentPage.FindByName<SearchBar>("formAsearchBar");

            FormAGridListview = CurrentPage.FindByName<ListView>("GridlistLabour");

            FormAMaterialGridListview = CurrentPage.FindByName<ListView>("GridlistMaterial");

            FormAEquimentGridListview = CurrentPage.FindByName<ListView>("GridlistEquip");

            FormADetailGridListview = CurrentPage.FindByName<ListView>("GridlistLabour");

            FormAFlatDetailGridListview = CurrentPage.FindByName<ListView>("list");



            DDYearListItems = new ObservableCollection<DDListItems>();

            WeekListItems = new ObservableCollection<DDListItems>();

            WeekdayListItems = new ObservableCollection<DDListItems>();

            DDRodeCodeListItems = new ObservableCollection<DDListItems>();

            DDRMUListItems = new ObservableCollection<DDListItems>();

            DDRecNameListItems = new ObservableCollection<DDListItems>();

            DDvetnameListItems = new ObservableCollection<DDListItems>();

            DDvernameListItems = new ObservableCollection<DDListItems>();

            DDsovernameListItems = new ObservableCollection<DDListItems>();

            DDpronameListItems = new ObservableCollection<DDListItems>();

            DDsoagreenameListItems = new ObservableCollection<DDListItems>();

            DDInspUserListListItems = new ObservableCollection<DDListItems>();
        }


        private async Task<int> Dropdown()
        {

            try

            {
                _userDialogs.ShowLoading("Loading");

                MyLabourBaseFormDList = new ObservableCollection<FormDLabourDetailsResponseDTO>();

                MyEquimentBaseFormDList = new ObservableCollection<FormDEquipDetailsResponseDTO>();

                MyMaterialBaseFormDList = new ObservableCollection<FormDMaterialDetailsResponseDTO>();

                MyDetailBaseFormDList = new ObservableCollection<FormDDetailsResponseDTO>();

                await GetTypeCode("RMU");

                await GetddListDetails("Month");

                await GetddListDetails("Year");

                await GetddListDetails("Day");

                await GetWeekddListDetails();

                await GetInspUserList();

                await GetRecnameUserList();

                await GetVetUserList();

                await GetVerUserList();

                await GetsovernameList();

                await GetsopronameUserList();

                await soagreenameUserList();

                dt = new DateTime();

                rmucode.ItemsSource = DDRMUListItems.Select((DDListItems arg) => arg.Text).ToList();

                rmucode.SelectedIndexChanged += async (s, e) =>
                {
                    if (rmucode.SelectedIndex != -1)
                    {
                        SelectedRMU = DDRMUListItems[rmucode.SelectedIndex].Value.ToString();

                        strRMUCode = DDRMUListItems[rmucode.SelectedIndex].Text.ToString();  //DDRMUListItems[rmucode.SelectedIndex].Text.ToString().Split('-')[1];
                        strsectionname = null;
                        if (_editViewModel.Type == "Add")
                            await GetSectionByRmu(SelectedRMU);

                    }

                };

                sectioncode.SelectedIndexChanged += (s, e) =>
                {
                    if (sectioncode.SelectedIndex != -1)
                    {
                        SelectedRoadCode = DDRodeCodeListItems[sectioncode.SelectedIndex].Value.ToString();
                        strsectionname = DDRodeCodeListItems[sectioncode.SelectedIndex].Text.ToString().Split('-')[1];
                        strDivision = "MIRI";
                    }

                };

                userinspcode.ItemsSource = DDInspUserListListItems.Select((DDListItems arg) => arg.Text).ToList();

                userinspcode.SelectedIndexChanged += (s, e) =>
                {
                    try
                    {
                        // SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                        if (userinspcode.SelectedIndex != -1)
                        {

                            Selectedinspuser = DDInspUserListListItems[userinspcode.SelectedIndex].Value.ToString();

                            strSupervisor = DDInspUserListListItems[userinspcode.SelectedIndex].Text.Split('-')[1];

                            if (strSupervisor.ToLower() == "others")
                            {
                                entrlsupervisor.IsEnabled = true;
                            }
                            else
                            {
                                entrlsupervisor.IsEnabled = false;

                            }

                            if (_editViewModel.Type == "Add")
                            {
                                var today = DateTime.Today;
                                var strValue = GetReferenceNumber(SelectedMonth, GetMonthNumber(today.Month, (int)today.DayOfWeek), SelectedYear, Selectedinspuser);

                                StrRefcode = strValue.ToString();
                            }

                            //int iCode = Convert.ToInt32(Selectedinspuser);

                            //service call for getting user list

                            //var objUser = GetUserDetilsList("inspuser", iCode);

                        }

                    }
                    catch
                    {

                    }

                };



                weekpick.ItemsSource = WeekListItems.Select((DDListItems arg) => arg.Text).ToList();

                weekpick.SelectedIndexChanged += (s, e) =>
                {
                    if (weekpick.SelectedIndex != -1)
                    {
                        SelectedMonth = Convert.ToInt32(WeekListItems[weekpick.SelectedIndex].Value);
                        if (_editViewModel.Type == "Add")
                        {
                            int today = (int)DateTime.Today.DayOfWeek;
                            var strValue = GetReferenceNumber(SelectedMonth, GetMonthNumber(SelectedMonth, today), SelectedYear, Selectedinspuser); ;
                            StrRefcode = strValue.ToString();
                        }
                    }

                };

                weekdaypick.ItemsSource = WeekdayListItems.Select((DDListItems arg) => arg.Text).ToList();

                DateTime dta = DateTime.Now.Date;

                string curDay = dta.DayOfWeek.ToString();

                int weekindex = WeekdayListItems.ToList().FindIndex(a => a.Value == curDay);

                if (weekindex != -1)
                {

                    weekdaypick.SelectedIndex = weekindex;

                    SelectedWeekDay = WeekdayListItems[weekdaypick.SelectedIndex].Value;
                }

                weekdaypick.SelectedIndexChanged += (s, e) =>
                {
                    if (weekdaypick.SelectedIndex != -1)
                    {

                        SelectedWeekDay = WeekdayListItems[weekdaypick.SelectedIndex].Value;
                        if (_editViewModel.Type == "Add")
                        {

                            var strValue = GetReferenceNumber(SelectedMonth, GetMonthNumber(SelectedMonth, weekdaypick.SelectedIndex), SelectedYear, Selectedinspuser); ;

                            StrRefcode = strValue.ToString();
                        }
                    }

                };

                yearpick.ItemsSource = DDYearListItems.Select((DDListItems arg) => arg.Text).ToList();

                DateTime dta1 = DateTime.Now.Date;

                int icurDay = dta1.Year;

                //yearpick.Items.Clear();

                yearpick.ItemsSource = DDYearListItems.Select((DDListItems arg) => arg.Text).ToList();

                int yearindex = DDYearListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == icurDay);

                if (yearindex != -1)
                {
                    yearpick.SelectedIndex = yearindex;
                    SelectedYear = Convert.ToInt32(DDYearListItems[yearpick.SelectedIndex].Value);

                }

                yearpick.SelectedIndexChanged += (s, e) =>
                {

                    if (yearpick.SelectedIndex != -1)
                    {

                        SelectedYear = Convert.ToInt32(DDYearListItems[yearpick.SelectedIndex].Value);

                        if (_editViewModel.Type == "Add")
                        {
                            var strValue = GetReferenceNumber(SelectedMonth, GetMonthNumber(SelectedMonth, weekdaypick.SelectedIndex), SelectedYear, Selectedinspuser); ;

                            StrRefcode = strValue.ToString();
                        }

                    }
                };


                recnamepicker.ItemsSource = DDRecNameListItems.Select((DDListItems arg) => arg.Text).ToList();

                recnamepicker.SelectedIndexChanged += (s, e) =>
                {
                    try
                    {
                        // SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                        if (recnamepicker.SelectedIndex != -1)
                        {
                            SelectedRecName = DDRecNameListItems[recnamepicker.SelectedIndex].Value.ToString();

                            reccode = DDRecNameListItems[recnamepicker.SelectedIndex].Text.Split('-')[1];

                            if (reccode.ToLower() == "others" && _editViewModel.Type != "View")
                            {
                                enctrlreccode.IsEnabled = true;
                                etrlrecdesg.IsEnabled = true;
                            }
                            else
                            {
                                enctrlreccode.IsEnabled = false;
                                etrlrecdesg.IsEnabled = false;
                            }


                            int iCode = Convert.ToInt32(SelectedRecName);
                            var objUser = GetUserDetilsList("recuser", iCode);
                        }
                    }
                    catch
                    {

                    }

                };

                vetnamepicker.ItemsSource = DDvetnameListItems.Select((DDListItems arg) => arg.Text).ToList();

                vetnamepicker.SelectedIndexChanged += (s, e) =>
                {
                    try
                    {
                        // SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                        if (vetnamepicker.SelectedIndex != -1)
                        {

                            SelectedVetName = DDvetnameListItems[vetnamepicker.SelectedIndex].Value.ToString();

                            vetcode = DDvetnameListItems[vetnamepicker.SelectedIndex].Text.Split('-')[1];

                            if (vetcode.ToLower() == "others" && _editViewModel.Type != "View")
                            {
                                ectrlvetcode.IsEnabled = true;
                                ectrlvetdeg.IsEnabled = true;
                            }
                            else
                            {
                                ectrlvetcode.IsEnabled = false;
                                ectrlvetdeg.IsEnabled = false;
                            }

                            int iCode = Convert.ToInt32(SelectedVetName);

                            //service call for getting user list

                            var objUser = GetUserDetilsList("vetuser", iCode);
                        }

                    }
                    catch
                    {

                    }

                };

                vernamepicker.ItemsSource = DDvernameListItems.Select((DDListItems arg) => arg.Text).ToList();

                vernamepicker.SelectedIndexChanged += (s, e) =>
                {
                    try
                    {
                        // SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                        if (vernamepicker.SelectedIndex != -1)
                        {

                            SelectedVerName = DDvernameListItems[vernamepicker.SelectedIndex].Value.ToString();

                            vercode = DDvernameListItems[vernamepicker.SelectedIndex].Text.Split('-')[1];

                            if (vercode.ToLower() == "others" && _editViewModel.Type != "View")
                            {
                                entrlvercode.IsEnabled = true;
                                entrlverdesg.IsEnabled = true;
                            }
                            else
                            {
                                entrlvercode.IsEnabled = false;
                                entrlverdesg.IsEnabled = false;
                            }


                            int iCode = Convert.ToInt32(SelectedVerName);

                            //service call for getting user list
                            var objUser = GetUserDetilsList("veruser", iCode);
                        }

                    }
                    catch
                    {

                    }

                };
                sovernamepicker.ItemsSource = DDsovernameListItems.Select((DDListItems arg) => arg.Text).ToList();

                sovernamepicker.SelectedIndexChanged += (s, e) =>
                {
                    try
                    {
                        // SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                        if (sovernamepicker.SelectedIndex != -1)
                        {

                            SelectedsoverName = DDsovernameListItems[sovernamepicker.SelectedIndex].Value.ToString();

                            sovercode = DDsovernameListItems[sovernamepicker.SelectedIndex].Text.Split('-')[1];

                            if (sovercode.ToLower() == "others" && _editViewModel.Type != "View")
                            {
                                entrlsovercode.IsEnabled = true;
                                entrlsoverdesg.IsEnabled = true;
                            }
                            else
                            {
                                entrlsovercode.IsEnabled = false;
                                entrlsoverdesg.IsEnabled = false;
                            }


                            int iCode = Convert.ToInt32(SelectedsoverName);

                            //service call for getting user list
                            var objUser = GetUserDetilsList("soveruser", iCode);

                        }

                    }
                    catch
                    {

                    }

                };


                sopronamepicker.ItemsSource = DDpronameListItems.Select((DDListItems arg) => arg.Text).ToList();

                sopronamepicker.SelectedIndexChanged += (s, e) =>
                {
                    try
                    {
                        // SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                        if (sopronamepicker.SelectedIndex != -1)
                        {

                            SelectedproName = DDpronameListItems[sopronamepicker.SelectedIndex].Value.ToString();

                            soprocode = DDpronameListItems[sopronamepicker.SelectedIndex].Text.Split('-')[1];

                            if (soprocode.ToLower() == "others" && _editViewModel.Type != "View")
                            {
                                entrlsoprocode.IsEnabled = true;
                                entrlsoprodesg.IsEnabled = true;
                            }
                            else
                            {
                                entrlsoprocode.IsEnabled = false;
                                entrlsoprodesg.IsEnabled = false;
                            }


                            int iCode = Convert.ToInt32(SelectedproName);

                            //service call for getting user list
                            var objUser = GetUserDetilsList("soprouser", iCode);
                        }

                    }
                    catch
                    {

                    }

                };


                soagreenamepicker.ItemsSource = DDsoagreenameListItems.Select((DDListItems arg) => arg.Text).ToList();

                soagreenamepicker.SelectedIndexChanged += (s, e) =>
                {
                    try
                    {
                        // SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                        if (sopronamepicker.SelectedIndex != -1)
                        {

                            SelectedsoagreeName = DDsoagreenameListItems[soagreenamepicker.SelectedIndex].Value.ToString();

                            soagreecode = DDsoagreenameListItems[soagreenamepicker.SelectedIndex].Text.Split('-')[1];

                            if (soagreecode.ToLower() == "others" && _editViewModel.Type != "View")
                            {
                                entrlsoagreecode.IsEnabled = true;
                                soagrees.IsEnabled = true;
                            }
                            else
                            {
                                entrlsoagreecode.IsEnabled = false;
                                soagrees.IsEnabled = false;
                            }

                            int iCode = Convert.ToInt32(SelectedsoagreeName);

                            //service call for getting user list
                            var objUser = GetUserDetilsList("soagreeuser", iCode);
                        }

                    }
                    catch
                    {

                    }

                };



                if (lbrpageno == 0)
                    lbrpageno = 10;


                cuspicker.SelectedIndexChanged += async (s, e) =>
                {
                    try
                    {

                        if (cuspicker.SelectedIndex != -1)
                        {

                            if (cuspicker.SelectedItem.ToString() == "10 rows")
                            {
                                LbrPageNo = 0;
                                lbrpageno = 10;
                                await GetMyFormDLabourListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                                return;
                            }

                            else if (cuspicker.SelectedItem.ToString() == "25 rows")
                            {
                                LbrPageNo = 0;
                                lbrpageno = 25;
                                await GetMyFormDLabourListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;

                                return;

                            }

                            else if (cuspicker.SelectedItem.ToString() == "50 rows")
                            {
                                LbrPageNo = 0;
                                lbrpageno = 50;
                                await GetMyFormDLabourListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;

                                return;
                            }

                            else if (cuspicker.SelectedItem.ToString() == "100 rows")
                            {
                                LbrPageNo = 0;
                                lbrpageno = 100;
                                await GetMyFormDLabourListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                                return;
                            }
                            else if (cuspicker.SelectedItem.ToString() == "500 rows")
                            {
                                LbrPageNo = 0;
                                lbrpageno = 500;
                                await GetMyFormDLabourListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                                return;
                            }
                            else
                            {
                                LbrPageNo = 0;
                                lbrpageno = 1000;
                                await GetMyFormDLabourListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                                return;
                            }
                        }
                    }
                    catch { }


                };

                custequipicker.SelectedIndexChanged += async (s, e) =>
                {
                    try
                    {

                        if (cuspicker.SelectedIndex != -1)
                        {

                            if (cuspicker.SelectedItem.ToString() == "10 rows")
                            {
                                EqpmtPageNo = 0;
                                eqpmtpageno = 10;
                                await GetMyFormDEquipmentListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                                return;
                            }

                            else if (cuspicker.SelectedItem.ToString() == "25 rows")
                            {
                                EqpmtPageNo = 0;
                                eqpmtpageno = 25;
                                await GetMyFormDEquipmentListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;

                                return;

                            }

                            else if (cuspicker.SelectedItem.ToString() == "50 rows")
                            {
                                EqpmtPageNo = 0;
                                eqpmtpageno = 50;
                                await GetMyFormDEquipmentListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;

                                return;
                            }

                            else if (cuspicker.SelectedItem.ToString() == "100 rows")
                            {
                                EqpmtPageNo = 0;
                                eqpmtpageno = 100;
                                await GetMyFormDEquipmentListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                                return;
                            }
                            else if (cuspicker.SelectedItem.ToString() == "500 rows")
                            {
                                EqpmtPageNo = 0;
                                eqpmtpageno = 500;
                                await GetMyFormDEquipmentListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                                return;
                            }
                            else
                            {
                                EqpmtPageNo = 0;
                                eqpmtpageno = 1000;
                                await GetMyFormDEquipmentListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                                return;
                            }
                        }
                    }
                    catch { }

                };

                custmaterialpicker.SelectedIndexChanged += async (s, e) =>
                {
                    try
                    {

                        if (cuspicker.SelectedIndex != -1)
                        {

                            if (cuspicker.SelectedItem.ToString() == "10 rows")
                            {
                                MtrlPageNo = 0;
                                mtrlpageno = 10;
                                await GetMyFormDMaterialListReports(Convert.ToString(GetHeaderNoCode)); ;
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                                return;
                            }

                            else if (cuspicker.SelectedItem.ToString() == "25 rows")
                            {
                                MtrlPageNo = 0;
                                mtrlpageno = 25;
                                await GetMyFormDMaterialListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;

                                return;

                            }

                            else if (cuspicker.SelectedItem.ToString() == "50 rows")
                            {
                                MtrlPageNo = 0;
                                mtrlpageno = 50;
                                await GetMyFormDMaterialListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;

                                return;
                            }

                            else if (cuspicker.SelectedItem.ToString() == "100 rows")
                            {
                                MtrlPageNo = 0;
                                mtrlpageno = 100;
                                await GetMyFormDMaterialListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                                return;
                            }
                            else if (cuspicker.SelectedItem.ToString() == "500 rows")
                            {
                                MtrlPageNo = 0;
                                mtrlpageno = 500;
                                await GetMyFormDMaterialListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                                return;
                            }
                            else
                            {
                                MtrlPageNo = 0;
                                mtrlpageno = 1000;
                                await GetMyFormDMaterialListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                                return;
                            }
                        }
                    }
                    catch { }

                };

                custdetailpicker.SelectedIndexChanged += async (s, e) =>
                {
                    try
                    {

                        if (cuspicker.SelectedIndex != -1)
                        {

                            if (cuspicker.SelectedItem.ToString() == "10 rows")
                            {
                                DtlPageNo = 0;
                                dtlpageno = 10;
                                await GetMyFormDDetailListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                                return;
                            }

                            else if (cuspicker.SelectedItem.ToString() == "25 rows")
                            {
                                DtlPageNo = 0;
                                dtlpageno = 25;
                                await GetMyFormDDetailListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;

                                return;

                            }

                            else if (cuspicker.SelectedItem.ToString() == "50 rows")
                            {
                                DtlPageNo = 0;
                                dtlpageno = 50;
                                await GetMyFormDDetailListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;

                                return;
                            }

                            else if (cuspicker.SelectedItem.ToString() == "100 rows")
                            {
                                DtlPageNo = 0;
                                dtlpageno = 100;
                                await GetMyFormDDetailListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                                return;
                            }
                            else if (cuspicker.SelectedItem.ToString() == "500 rows")
                            {
                                DtlPageNo = 0;
                                dtlpageno = 500;
                                await GetMyFormDDetailListReports(Convert.ToString(GetHeaderNoCode));
                                // FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                                return;
                            }
                            else
                            {
                                DtlPageNo = 0;
                                dtlpageno = 1000;
                                await GetMyFormDDetailListReports(Convert.ToString(GetHeaderNoCode));
                                //FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                                return;
                            }
                        }
                    }
                    catch { }
                };
            }
            catch { }

            return 1;

        }

        public async Task<ObservableCollection<FormDHeaderResponseDTO>> UpdateFormDHeaderList()
        {
            //_userDialogs.ShowLoading("Loading");

            try
            {
                FormDHeaderRequestDTO objRequest = new FormDHeaderRequestDTO();
                if (CrossConnectivity.Current.IsConnected)
                {

                    objRequest = new FormDHeaderRequestDTO()
                    {
                        No = GetHeaderNoCode,

                        ReferenceID = StrRefcode,

                        Rmu = SelectedRMU,

                        RoadCode = SelectedRoadCode,

                        DivisionName = strDivision,

                        CrewUnitName = Selectedinspuser,

                        WeekNo = Convert.ToInt32(SelectedMonth),

                        Day = SelectedWeekDay,

                        Year = SelectedYear,

                        ActiveYn = true


                    };



                    //var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

                    var response = await _restApi.UpdateFormDHdr(objRequest);


                    if (response.success)
                    {
                        try
                        {

                            int iRval = response.data;

                            //UserDialogs.Instance.Toast("Header Details Updated Successfully.");

                        }
                        catch (Exception ex)
                        {
                            //_userDialogs.Alert(ex.Message);
                            //_userDialogs.HideLoading();

                            //UserDialogs.Instance.Alert("Header Details Saved Successfully.");

                        }

                        //return DetailFromAHdrGridListItems;
                    }


                }
                else
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");

            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }

            return new ObservableCollection<FormDHeaderResponseDTO>();
        }



        public async Task<ObservableCollection<FormDHeaderResponseDTO>> SaveFormDHeaderList()
        {
            //_userDialogs.ShowLoading("Loading");

            try
            {
                FormDHeaderRequestDTO objRequest = new FormDHeaderRequestDTO();

                if (CrossConnectivity.Current.IsConnected)
                {

                    objRequest = new FormDHeaderRequestDTO()
                    {

                        ReferenceID = StrRefcode,

                        Rmu = SelectedRMU,

                        RoadCode = SelectedRoadCode,

                        DivisionName = strDivision,

                        CrewUnitName = strSupervisor,

                        WeekNo = Convert.ToInt32(SelectedMonth),

                        Day = SelectedWeekDay,

                        CrewUnit = Selectedinspuser,

                        Month = GetMonthNumber(Convert.ToInt32(SelectedMonth), (int)Enum.Parse(typeof(DayOfWeek), SelectedWeekDay)),

                        Year = SelectedYear

                    };



                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

                    //var response = await _restApi.SaveFormDHdr(objRequest);
                    var response = await _restApi.FindAndSaveFormDHdr(objRequest);

                    if (response.success)
                    {
                        try
                        {

                            GetHeaderNoCode = response.data.No;
                            StrRefcode = response.data.ReferenceID;

                            btnFind.IsVisible = false;

                            _editViewModel.Type = "Edit";

                            isControl(false);

                            UserDialogs.Instance.Alert("Header Details Saved Successfully.", "RAMS");

                        }
                        catch (Exception ex)
                        {
                            //_userDialogs.Alert(ex.Message);
                            //_userDialogs.HideLoading();

                            //UserDialogs.Instance.Alert("Header Details Saved Successfully.");

                        }

                        //return DetailFromAHdrGridListItems;
                    }


                }
                else
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");

            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }

            return new ObservableCollection<FormDHeaderResponseDTO>();
        }



        public async Task<string> GetReferenceNumber(int? WeekNo, int? MonthNo, int? Year, string CrewUnit)
        {

            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {

                    try
                    {
                        var request = new FormDHeaderRequestDTO()
                        {
                            WeekNo = WeekNo,
                            Month = MonthNo,
                            Year = Year,
                            CrewUnit = CrewUnit
                        };

                        var response = await _restApi.GenerateFormDRefNo(request);
                        //var response = await _restApi.GetFormDRefNo(WeekNo, MonthNo, Year, CrewUnit,Day,RMU,SecCode);

                        if (response.success)
                        {

                            StrRefcode = response.data.ToString();

                            //btnOk.IsEnabled = true;
                        }
                        else
                        {
                            _userDialogs.Toast("Unable to connect please check your internet connection.");
                            return "";
                        }
                        //iStrValue = response.ToString();
                    }
                    catch (Exception ex)
                    {
                        _userDialogs.Toast(ex.Message);
                    }
                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return "";
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }
            return StrRefcode;
        }



        public async Task<ObservableCollection<DDListItems>> GetRecnameUserList()
        {
            try
            {
                //_userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.userList();
                    if (response.success)
                    {
                        DDRecNameListItems = new ObservableCollection<DDListItems>(response.data);
                        return DDRecNameListItems;

                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }

                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }


        public async Task<ObservableCollection<DDListItems>> GetVetUserList()
        {
            try
            {
                //_userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.userList();
                    if (response.success)
                    {
                        DDvetnameListItems = new ObservableCollection<DDListItems>(response.data);
                        return DDvetnameListItems;

                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }

                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }


        public async Task<ObservableCollection<DDListItems>> GetVerUserList()
        {
            try
            {
                //_userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.userList();
                    if (response.success)
                    {
                        DDvernameListItems = new ObservableCollection<DDListItems>(response.data);
                        return DDvernameListItems;

                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }
                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }


        public async Task<ObservableCollection<DDListItems>> GetsovernameList()
        {
            try
            {
                //_userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.userList();
                    if (response.success)
                    {
                        DDsovernameListItems = new ObservableCollection<DDListItems>(response.data);
                        return DDsovernameListItems;

                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }
                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }



        public async Task<ObservableCollection<DDListItems>> GetsopronameUserList()
        {
            try
            {
                //_userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.userList();
                    if (response.success)
                    {
                        DDpronameListItems = new ObservableCollection<DDListItems>(response.data);
                        return DDpronameListItems;

                    }
                    else
                    {
                        UserDialogs.Instance.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }
                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }


        public async Task<ObservableCollection<DDListItems>> soagreenameUserList()
        {
            try
            {
                //_userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.userList();
                    if (response.success)
                    {
                        DDsoagreenameListItems = new ObservableCollection<DDListItems>(response.data);
                        return DDsoagreenameListItems;

                    }
                    else
                    {
                        UserDialogs.Instance.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }
                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }

        public int isControl(bool bValue)
        {
            try
            {

                if (_editViewModel.Type == "Add")
                {
                    HeaderControlEnabled = true;

                    IsControlEnabled = true;

                    rmucode.IsEnabled = bValue;

                    sectioncode.IsEnabled = bValue;

                    crewpick.IsEnabled = bValue;

                    enctrlDivision.IsEnabled = false;

                    weekpick.IsEnabled = bValue;

                    weekdaypick.IsEnabled = bValue;

                    yearpick.IsEnabled = bValue;

                    //recsignview
                    recdate.IsEnabled = bValue;

                    recnamepicker.IsEnabled = bValue;

                    // recsignview.IsEnabled = bValue;
                    //vetsignview
                    vetnamepicker.IsEnabled = bValue;

                    vetdate.IsEnabled = bValue;
                    //vetsignview.IsEnabled = bValue;
                    //versignview
                    vernamepicker.IsEnabled = bValue;

                    entrlsupervisor.IsEnabled = bValue;

                    verdate.IsEnabled = bValue;

                    //versignview.IsEnabled = bValue;

                    //soversignview
                    sovernamepicker.IsEnabled = bValue;

                    soverdate.IsEnabled = bValue;

                    //soversignview.IsEnabled = bValue;

                    //soprosignview
                    sopronamepicker.IsEnabled = bValue;

                    soprodate.IsEnabled = bValue;

                    //soprosignview.IsEnabled = bValue;

                    //soagreesignview
                    soagreenamepicker.IsEnabled = bValue;

                    //soagreedate
                    soagreedate.IsEnabled = bValue;

                    //soagreesignview.IsEnabled = bValue;

                    btnSave.IsEnabled = bValue;

                    btnSubmit.IsEnabled = bValue;

                    btnCancel.IsEnabled = bValue;

                    btnDetailSave.IsEnabled = bValue;

                    btnFind.IsVisible = bValue;

                    App.ViewState = false;

                }
                else if (_editViewModel.Type == "Edit")
                {
                    rmucode.IsEnabled = false;

                    sectioncode.IsEnabled = false;

                    crewpick.IsEnabled = false;

                    enctrlDivision.IsEnabled = false;

                    weekpick.IsEnabled = false;

                    weekdaypick.IsEnabled = false;

                    yearpick.IsEnabled = false;

                    //entrlsupervisor.IsEnabled = false;

                    //recsignview
                    recdate.IsEnabled = true;

                    recnamepicker.IsEnabled = true;

                    //recsignview.IsEnabled = true;
                    //vetsignview
                    vetnamepicker.IsEnabled = true;

                    vetdate.IsEnabled = true;
                    //vetsignview.IsEnabled = true;
                    //versignview
                    vernamepicker.IsEnabled = true;

                    verdate.IsEnabled = true;

                    //versignview.IsEnabled = true;

                    //soversignview
                    sovernamepicker.IsEnabled = true;

                    soverdate.IsEnabled = true;

                    //soversignview.IsEnabled = true;

                    //soprosignview
                    sopronamepicker.IsEnabled = true;

                    soprodate.IsEnabled = true;

                    //soprosignview.IsEnabled = true;

                    //soagreesignview
                    soagreenamepicker.IsEnabled = true;

                    //soagreedate
                    soagreedate.IsEnabled = true;

                    //soagreesignview.IsEnabled = true;

                    btnSave.IsEnabled = true;

                    btnSubmit.IsEnabled = true;

                    btnCancel.IsEnabled = true;


                    btnLabourCommand.IsVisible = true;
                    btnEquipment.IsVisible = true;
                    btnMaterial.IsVisible = true;
                    //btnDetail.IsVisible = true;
                    btnDetailX.IsVisible = true;

                    btnLabourCommandX.IsVisible = true;
                    btnEquipmentX.IsVisible = true;
                    btnMaterialX.IsVisible = true;


                    btnDetailSave.IsEnabled = true;

                    App.ViewState = false;

                    btnFind.IsVisible = false;
                }
                else if (_editViewModel.Type == "View")
                {
                    rmucode.IsEnabled = bValue;
                    sectioncode.IsEnabled = bValue;
                    crewpick.IsEnabled = bValue;
                    enctrlDivision.IsEnabled = bValue;
                    weekpick.IsEnabled = bValue;
                    weekdaypick.IsEnabled = bValue;
                    yearpick.IsEnabled = bValue;
                    //recsignview
                    recdate.IsEnabled = bValue;

                    recnamepicker.IsEnabled = bValue;

                    entrlsupervisor.IsEnabled = bValue;

                    //recsignview.IsEnabled = bValue;
                    //recsignview
                    recdate.IsEnabled = bValue;

                    recnamepicker.IsEnabled = bValue;

                    //recsignview.IsEnabled = bValue;
                    //vetsignview
                    vetnamepicker.IsEnabled = bValue;

                    vetdate.IsEnabled = bValue;
                    //vetsignview.IsEnabled = bValue;
                    //versignview
                    vernamepicker.IsEnabled = bValue;

                    verdate.IsEnabled = bValue;

                    //versignview.IsEnabled = bValue;

                    //soversignview
                    sovernamepicker.IsEnabled = bValue;

                    soverdate.IsEnabled = bValue;

                    //soversignview.IsEnabled = bValue;

                    //soprosignview
                    sopronamepicker.IsEnabled = bValue;

                    soprodate.IsEnabled = bValue;

                    //soprosignview.IsEnabled = bValue;

                    //soagreesignview
                    soagreenamepicker.IsEnabled = bValue;

                    //soagreedate
                    soagreedate.IsEnabled = bValue;

                    //soagreesignview.IsEnabled = bValue;


                    btnSave.IsEnabled = bValue;

                    btnSubmit.IsEnabled = bValue;

                    btnCancel.IsEnabled = true;

                    App.ViewState = true;


                    btnDetailSave.IsEnabled = bValue;

                    btnLabourCommand.IsVisible = bValue;
                    btnEquipment.IsVisible = bValue;
                    btnMaterial.IsVisible = bValue;
                    //btnDetail.IsVisible = bValue;
                    btnDetailX.IsVisible = bValue;

                    btnLabourCommandX.IsVisible = bValue;
                    btnEquipmentX.IsVisible = bValue;
                    btnMaterialX.IsVisible = bValue;
                    btnFind.IsVisible = bValue;
                }

            }
            catch { }
            return 1;
        }

        bool Vaildate()
        {
            if (string.IsNullOrEmpty(SelectedRMU))
            {
                _userDialogs.Alert("Select the RMU", "RAMS");
                return false;

            }
            else if (string.IsNullOrEmpty(SelectedRoadCode))
            {
                _userDialogs.Alert("Select the road code", "RAMS");
                return false;
            }
            else if (string.IsNullOrEmpty(Selectedinspuser))
            {
                _userDialogs.Alert("Select the crew supervisor", "RAMS");
                return false;
            }
            else if (string.IsNullOrEmpty(SelectedWeekDay))
            {
                _userDialogs.Alert("Select the week day", "RAMS");
                return false;
            }
            else if (SelectedMonth == null)
            {
                _userDialogs.Alert("Select the week No", "RAMS");
                return false;
            }

            else if (SelectedYear == null)
            {
                _userDialogs.Alert("Select the year", "RAMS");
                return false;
            }

            return true;

        }



        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            App.srecsignview = ""; App.sversignview = ""; App.svetsignview = "";
            App.ssoversignview = ""; App.ssoprosignview = ""; App.ssoagreesignview = "";

            if (_editViewModel.Type == "Add")
            {
                // DropDownMasterSetup(_editViewModel.Type);

                _editViewModel.HdrFahPkRefNo = _editViewModel.HdrFahPkRefNo;

                GetHeaderNoCode = _editViewModel.HdrFahPkRefNo;

                await Dropdown();

                isControl(true);


                return;
            }
            else if (_editViewModel.Type == "Edit" || _editViewModel.Type == "View")
            {

                _editViewModel.HdrFahPkRefNo = _editViewModel.HdrFahPkRefNo;

                GetHeaderNoCode = _editViewModel.HdrFahPkRefNo;

                await Dropdown();

                SelectedHdrEditItem = await GetEditViewHeaderdetails(GetHeaderNoCode);


                MyLabourBaseFormDList = await GetMyFormDLabourListReports(Convert.ToString(GetHeaderNoCode));

                MyMaterialBaseFormDList = await GetMyFormDMaterialListReports(Convert.ToString(GetHeaderNoCode));


                MyEquimentBaseFormDList = await GetMyFormDEquipmentListReports(Convert.ToString(GetHeaderNoCode));

                MyDetailBaseFormDList = await GetMyFormDDetailListReports(Convert.ToString(GetHeaderNoCode));


                isControl(false);


                return;
            }

        }


        public async Task<int?> GetDetailSerialNo(int HeaderID)
        {

            try
            {
                //_userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.FormDDtlSrNo(HeaderID);

                    if (response.success)
                    {
                        iRet = response.data;

                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return 0;
                    }


                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return 0;
                }

            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }
            return iRet;
        }



        public async Task<FormDHeaderRequestDTO> GetEditViewHeaderdetails(int HeaderCode)
        {
            try
            {
                _userDialogs.ShowLoading("loading");

                if (CrossConnectivity.Current.IsConnected)
                {

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(HeaderCode);

                    var response = await _restApi.FormDGetById(HeaderCode);

                    if (response.success)
                    {
                        SelectedHdrEditItem = response.data;

                        SelectedRoadCode = SelectedHdrEditItem.RoadCode;

                        StrRefcode = SelectedHdrEditItem.ReferenceID;

                        strDivision = SelectedHdrEditItem.DivisionName;

                        //rmucode.Items.Clear();

                        rmucode.ItemsSource = DDRMUListItems.Select((DDListItems arg) => arg.Text).ToList();

                        int Rmuindex = DDRMUListItems.ToList().FindIndex(a => a.Value.ToLower() == SelectedHdrEditItem.Rmu.ToLower());

                        if (Rmuindex != -1)
                        {
                            rmucode.SelectedIndex = Rmuindex;

                            SelectedRMU = DDRMUListItems[Rmuindex].Value.ToString();

                            strRMUCode = DDRMUListItems[Rmuindex].Text.ToString();
                        }

                        await GetSectionByRmu(SelectedHdrEditItem.Rmu);

                        //roadcode.Items.Clear();

                        sectioncode.ItemsSource = DDRodeCodeListItems.Select((DDListItems arg) => arg.Text).ToList();

                        int Roadindex = DDRodeCodeListItems.ToList().FindIndex(a => a.Value == SelectedHdrEditItem.RoadCode);

                        if (Roadindex != -1)
                        {
                            sectioncode.SelectedIndex = Roadindex;
                            //sectioncode.SelectedIndex = Roadindex;

                            SelectedRoadCode = DDRodeCodeListItems[sectioncode.SelectedIndex].Value.ToString();

                            strsectionname = DDRodeCodeListItems[sectioncode.SelectedIndex].Text.ToString().Split('-')[1];
                        }

                        //userinspcode.Items.Clear();

                        userinspcode.ItemsSource = DDInspUserListListItems.Select((DDListItems arg) => arg.Text).ToList();

                        int userIndex = DDInspUserListListItems.ToList().FindIndex(a => a.Value == SelectedHdrEditItem.CrewUnit);

                        if (userIndex != -1)
                        {

                            userinspcode.SelectedIndex = userIndex;

                            Selectedinspuser = DDInspUserListListItems[userIndex].Value.ToString();

                            strSupervisor = DDInspUserListListItems[userIndex].Text.ToString().Split('-')[1];

                            if (strSupervisor.ToLower() == "others")
                            {
                                entrlsupervisor.IsEnabled = true;
                            }
                            else
                            {
                                entrlsupervisor.IsEnabled = false;

                            }


                        }

                        //weekpick.Items.Clear();

                        weekpick.ItemsSource = WeekListItems.Select((DDListItems arg) => arg.Text).ToList();

                        int weekindex = WeekListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == SelectedHdrEditItem.WeekNo);

                        if (weekindex != -1)
                        {
                            weekpick.SelectedIndex = weekindex;

                            SelectedWeekNo = WeekListItems[weekindex].Value.ToString();
                        }
                        //weekdaypick.Items.Clear();

                        weekdaypick.ItemsSource = WeekdayListItems.Select((DDListItems arg) => arg.Text).ToList();

                        int WeekDayindex = WeekdayListItems.ToList().FindIndex(a => a.Value == SelectedHdrEditItem.Day);

                        if (WeekDayindex != -1)
                        {
                            weekdaypick.SelectedIndex = WeekDayindex;

                            SelectedWeekDay = WeekdayListItems[WeekDayindex].Value.ToString();

                        }

                        //yearpick.Items.Clear();

                        yearpick.ItemsSource = DDYearListItems.Select((DDListItems arg) => arg.Text).ToList();

                        int yearindex = DDYearListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == SelectedHdrEditItem.Year);

                        if (yearindex != -1)
                        {
                            yearpick.SelectedIndex = yearindex;

                            SelectedYear = Convert.ToInt32(DDYearListItems[yearindex].Value);

                        }


                        //recnamepicker.Items.Clear();

                        recnamepicker.ItemsSource = DDRecNameListItems.Select((DDListItems arg) => arg.Text).ToList();

                        int recordindex = DDRecNameListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == SelectedHdrEditItem.ReportedByUserId);

                        if (recordindex != -1)
                        {
                            recnamepicker.SelectedIndex = recordindex;

                            SelectedRecName = DDRecNameListItems[recordindex].Value.ToString();
                        }

                        reccode = SelectedHdrEditItem.ReportedByUsername;

                        recdesg = SelectedHdrEditItem.ReportedByDesignation;




                        recsignimage = Xamarin.Forms.ImageSource.FromStream(
                        () => new MemoryStream(Convert.FromBase64String(SelectedHdrEditItem.ReportedBySign)));

                        recsignview = CurrentPage.FindByName<SignaturePadView>("recsignview");

                        App.srecsignview = SelectedHdrEditItem.ReportedBySign;

                        DateTime dateTime;

                        if (recsignimage != null)
                            recsignview.BackgroundImage = recsignimage;

                        //DateTime.TryParse(SelectedHdrEditItem.DateReported.ToString(), out dateTime);
                        //recdate.Date = dateTime;
                        recdate.NullableDate = SelectedHdrEditItem.DateReported.HasValue ? SelectedHdrEditItem.DateReported.Value : (DateTime?)null;

                        //vetnamepicker.Items.Clear();

                        vetnamepicker.ItemsSource = DDvetnameListItems.Select((DDListItems arg) => arg.Text).ToList();

                        int vetindex = DDvetnameListItems.ToList().FindIndex(a => a.Value == SelectedHdrEditItem.UseridVet);

                        if (vetindex != -1)
                        {
                            vetnamepicker.SelectedIndex = vetindex;
                            SelectedVetName = DDvetnameListItems[vetindex].Value.ToString();
                        }
                        vetcode = SelectedHdrEditItem.UsernameVet;

                        vetdesg = SelectedHdrEditItem.DesignationVet;

                        vetsignimage = Xamarin.Forms.ImageSource.FromStream(
                        () => new MemoryStream(Convert.FromBase64String(SelectedHdrEditItem.SignVet)));

                        vetsignview = CurrentPage.FindByName<SignaturePadView>("vetsignview");

                        App.svetsignview = SelectedHdrEditItem.SignVet;

                        if (vetsignimage != null)
                            vetsignview.BackgroundImage = vetsignimage;

                        //DateTime.TryParse(SelectedHdrEditItem.DtVet.ToString(), out dateTime);
                        //vetdate.Date = dateTime;
                        vetdate.NullableDate = SelectedHdrEditItem.DtVet.HasValue ? SelectedHdrEditItem.DtVet.Value : (DateTime?)null;


                        //vernamepicker.Items.Clear();

                        vernamepicker.ItemsSource = DDvernameListItems.Select((DDListItems arg) => arg.Text).ToList();

                        int verindex = DDvernameListItems.ToList().FindIndex(a => a.Value == SelectedHdrEditItem.UseridVer);

                        if (verindex != -1)
                        {
                            vernamepicker.SelectedIndex = verindex;

                            SelectedVetName = DDvernameListItems[verindex].Value.ToString();
                        }

                        vercode = SelectedHdrEditItem.UsernameVer;

                        verdesg = SelectedHdrEditItem.DesignationVer;


                        versignimage = Xamarin.Forms.ImageSource.FromStream(
                        () => new MemoryStream(Convert.FromBase64String(SelectedHdrEditItem.SignVer)));
                        versignview = CurrentPage.FindByName<SignaturePadView>("versignview");
                        App.sversignview = SelectedHdrEditItem.SignVer;

                        if (versignimage != null)
                            versignview.BackgroundImage = versignimage;

                        //DateTime.TryParse(SelectedHdrEditItem.DtVer.ToString(), out dateTime);

                        //verdate.Date = dateTime;
                        verdate.NullableDate = SelectedHdrEditItem.DtVer.HasValue ? SelectedHdrEditItem.DtVer.Value : (DateTime?)null;

                        //sovernamepicker.Items.Clear();

                        sovernamepicker.ItemsSource = DDsovernameListItems.Select((DDListItems arg) => arg.Text).ToList();

                        int soverindex = DDsovernameListItems.ToList().FindIndex(a => a.Value == SelectedHdrEditItem.UseridVerSo);

                        if (soverindex != -1)
                        {
                            sovernamepicker.SelectedIndex = soverindex;

                            SelectedsoverName = DDsovernameListItems[soverindex].Value.ToString();
                        }
                        sovercode = SelectedHdrEditItem.UsernameVerSo;

                        recdesg = SelectedHdrEditItem.DesignationVerSo;

                        soversignimage = Xamarin.Forms.ImageSource.FromStream(
                             () => new MemoryStream(Convert.FromBase64String(SelectedHdrEditItem.SignVerSo)));
                        soversignview = CurrentPage.FindByName<SignaturePadView>("soversignview");

                        App.ssoversignview = SelectedHdrEditItem.SignVerSo;

                        if (soversignimage != null)
                            soversignview.BackgroundImage = soversignimage;

                        //DateTime.TryParse(SelectedHdrEditItem.DtVerSo.ToString(), out dateTime);

                        //soverdate.Date = dateTime;
                        soverdate.NullableDate = SelectedHdrEditItem.DtVerSo.HasValue ? SelectedHdrEditItem.DtVerSo.Value : (DateTime?)null;

                        //sopronamepicker.Items.Clear();

                        sopronamepicker.ItemsSource = DDsovernameListItems.Select((DDListItems arg) => arg.Text).ToList();

                        int soproindex = DDpronameListItems.ToList().FindIndex(a => a.Value == SelectedHdrEditItem.UseridPrcdSo);

                        if (soproindex != -1)
                        {
                            sopronamepicker.SelectedIndex = soproindex;

                            SelectedproName = DDpronameListItems[soproindex].Value.ToString();
                        }
                        soprocode = SelectedHdrEditItem.UsernamePrcdSo;

                        soprodesg = SelectedHdrEditItem.DesignationPrcdSo;

                        soprosignimage = Xamarin.Forms.ImageSource.FromStream(
                        () => new MemoryStream(Convert.FromBase64String(SelectedHdrEditItem.SignPrcdSo)));

                        soprosignview = CurrentPage.FindByName<SignaturePadView>("soprosignview");
                        App.ssoprosignview = SelectedHdrEditItem.SignPrcdSo;


                        if (soprosignimage != null)
                            soprosignview.BackgroundImage = soprosignimage;

                        //DateTime.TryParse(SelectedHdrEditItem.DtPrcdSo.ToString(), out dateTime);

                        //soprodate.Date = dateTime;
                        soprodate.NullableDate = SelectedHdrEditItem.DtPrcdSo.HasValue ? SelectedHdrEditItem.DtPrcdSo.Value : (DateTime?)null;

                        //soagreenamepicker.Items.Clear();

                        soagreenamepicker.ItemsSource = DDsovernameListItems.Select((DDListItems arg) => arg.Text).ToList();

                        int Agreeindex = DDsoagreenameListItems.ToList().FindIndex(a => a.Value == SelectedHdrEditItem.UseridAgrdSo);

                        if (Agreeindex != -1)
                        {
                            soagreenamepicker.SelectedIndex = Agreeindex;

                            SelectedsoagreeName = DDsoagreenameListItems[Agreeindex].Value.ToString();
                        }

                        reccode = SelectedHdrEditItem.UsernameAgrdSo;

                        recdesg = SelectedHdrEditItem.DesignationAgrdSo;

                        soagreesignimage = Xamarin.Forms.ImageSource.FromStream(
                        () => new MemoryStream(Convert.FromBase64String(SelectedHdrEditItem.SignAgrdSo)));

                        soagreesignview = CurrentPage.FindByName<SignaturePadView>("soagreesignview");

                        App.ssoagreesignview = SelectedHdrEditItem.SignAgrdSo;

                        if (soagreesignimage != null)
                            soagreesignview.BackgroundImage = soagreesignimage;

                        //DateTime.TryParse(SelectedHdrEditItem.DtAgrdSo?.ToString(), out dateTime);

                        //soagreedate.Date = dateTime;
                        soagreedate.NullableDate = SelectedHdrEditItem.DtAgrdSo.HasValue ? SelectedHdrEditItem.DtAgrdSo.Value : (DateTime?)null;

                        //SelectedYear = SelectedHdrEditItem.Year;

                        //SelectedSection = SelectedHdrEditItem.section;

                        //StrRefcode = SelectedHdrEditItem.Id;

                        //strInspSign = SelectedHdrEditItem.SignPrp;

                        //strVerSign = SelectedHdrEditItem.SignVer;

                        //Selectedinspuser = SelectedHdrEditItem.UseridPrp.ToString();

                        //await GetInspUserList();

                        //userinspcode = CurrentPage.FindByName<ExtendedPicker>("insppicker");

                        //userinspcode.ItemsSource = DDInspUserListListItems.Select((DDListItems arg) => arg.Text).ToList();

                        //int inspindex = DDInspUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == SelectedHdrEditItem.UseridPrp);

                        //if (inspindex == -1) { inspindex = 1; }

                        //userinspcode.SelectedIndex = inspindex;



                        // = SelectedHdrEditItem.UsernamePrp;

                        //strinspDesignation = SelectedHdrEditItem.DesignationPrp;


                        //dtDateinsp = DateTime.Now.Date; //SelectedHdrEditItem.DtPrp.Value;


                        //Selectedverpuser = SelectedHdrEditItem.UseridVer.ToString();


                        //strverName = SelectedHdrEditItem.UsernamePrp;

                        //strverDesignation = SelectedHdrEditItem.DesignationPrp;

                        ////vercode = CurrentPage.FindByName<DatePicker>("verdatepicker");

                        //dtdateever = DateTime.Now.Date; //SelectedHdrEditItem.VerifiedDt.Value;


                        //inspimage = Xamarin.Forms.ImageSource.FromStream(
                        //() => new MemoryStream(Convert.FromBase64String(strInspSign)));

                        //padView = CurrentPage.FindByName<SignaturePadView>("SignatureView");

                        ////if(inspimage != null || inspimage.IsEmpty)
                        ////byte[] bytes = Convert.FromBase64String(strInspSign);

                        //// byte[] resizedImage = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(bytes, 500, 1000);
                        ////float[] dataArray = Enumerable.Range(0, bytes.Length / 4).Select(i => BitConverter.ToSingle(bytes, i * 4)).ToArray();

                        ////Point[] points;
                        ////using (var ms = new MemoryStream(bytes))
                        ////{
                        ////    using (var r = new BinaryReader(ms))
                        ////    {
                        ////        int len = r.ReadInt32();
                        ////        points = new Point[len];
                        ////        for (int i = 0; i != len; i++)
                        ////        {
                        ////            points[i] = new Point(r.ReadInt32(), r.ReadInt32());
                        ////        }
                        ////    }
                        ////}

                        ////padView.Points = ;

                        //padView.BackgroundImage = inspimage;

                        //verimage = Xamarin.Forms.ImageSource.FromStream(
                        //() => new MemoryStream(Convert.FromBase64String(strVerSign)));

                        //vpadview = CurrentPage.FindByName<SignaturePadView>("VPadView");
                        //vpadview.BackgroundImage = verimage;

                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }

                    return SelectedHdrEditItem;
                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                //_userDialogs.Alert(ex.Message);
            }
            finally
            {
                // _userdialogs.hideloading();
            }
            return new FormDHeaderRequestDTO();
        }





        public ICommand recredosign
        {
            get
            {

                return new FreshCommand(async (obj) =>
                {


                    recsignview = CurrentPage.FindByName<SignaturePadView>("recsignview");

                    recsignview.BackgroundImage = null;


                });

            }
        }

        public ICommand verpredosign
        {
            get
            {

                return new FreshCommand(async (obj) =>
                {


                    versignview = CurrentPage.FindByName<SignaturePadView>("versignview");

                    versignview.BackgroundImage = null;


                });

            }
        }

        public ICommand vetpredosign
        {
            get
            {

                return new FreshCommand(async (obj) =>
                {


                    vetsignview = CurrentPage.FindByName<SignaturePadView>("vetsignview");

                    vetsignview.BackgroundImage = null;


                });

            }
        }

        public ICommand soverpredosign
        {
            get
            {

                return new FreshCommand(async (obj) =>
                {


                    soversignview = CurrentPage.FindByName<SignaturePadView>("soversignview");

                    soversignview.BackgroundImage = null;


                });

            }
        }

        public ICommand soproredosign
        {
            get
            {

                return new FreshCommand(async (obj) =>
                {


                    soprosignview = CurrentPage.FindByName<SignaturePadView>("soprosignview");

                    soprosignview.BackgroundImage = null;


                });

            }
        }

        public ICommand soagreeredosign
        {
            get
            {

                return new FreshCommand(async (obj) =>
                {


                    soagreesignview = CurrentPage.FindByName<SignaturePadView>("soagreesignview");

                    soagreesignview.BackgroundImage = null;


                });

            }
        }

        public ICommand ToggleCommand
        {
            get
            {
                return new Command(ToggleBarTapped);
            }
        }

        public async Task<UserResponseDTO> GetUserDetilsList(string usertype, int iUser)
        {
            // _userDialogs.ShowLoading("Loading");

            try
            {

                if (CrossConnectivity.Current.IsConnected)
                {

                    var objUser = new UserRequestDTO()
                    {
                        UserId = iUser

                    };


                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(objUser);

                    var response = await _restApi.UserDtl(objUser);



                    if (response.success)
                    {
                        try
                        {

                            if (usertype == "recuser")
                            {
                                SelectedUserItem = response.data;
                                if (SelectedHdrEditItem != null && SelectedHdrEditItem.ReportedByUsername != null && SelectedHdrEditItem.ReportedByUsername == SelectedUserItem.UserName || SelectedUserItem.UserName.ToLower() == "others")
                                {
                                    reccode = SelectedHdrEditItem.ReportedByUsername;
                                    recdesg = SelectedHdrEditItem.ReportedByDesignation;
                                }
                                else
                                {
                                    reccode = SelectedUserItem.UserName;
                                    recdesg = SelectedUserItem.Position;
                                }


                            }
                            else if (usertype == "vetuser")
                            {
                                SelectedUserItem = response.data;

                                if (SelectedHdrEditItem != null && SelectedHdrEditItem.UsernameVet != null && SelectedHdrEditItem.UsernameVet == SelectedUserItem.UserName || SelectedUserItem.UserName.ToLower() == "others")
                                {
                                    vetcode = SelectedHdrEditItem.UsernameVet;
                                    vetdesg = SelectedHdrEditItem.DesignationVet;
                                }
                                else
                                {
                                    vetcode = SelectedUserItem.UserName;
                                    vetdesg = SelectedUserItem.Position;
                                }

                            }

                            else if (usertype == "veruser")
                            {
                                SelectedUserItem = response.data;

                                if (SelectedHdrEditItem != null && SelectedHdrEditItem.UsernameVer != null && SelectedHdrEditItem.UsernameVer == SelectedUserItem.UserName || SelectedUserItem.UserName.ToLower() == "others")
                                {
                                    vercode = SelectedHdrEditItem.UsernameVer;
                                    verdesg = SelectedHdrEditItem.DesignationVer;
                                }
                                else
                                {
                                    vercode = SelectedUserItem.UserName;
                                    verdesg = SelectedUserItem.Position;
                                }

                            }

                            else if (usertype == "soveruser")
                            {
                                SelectedUserItem = response.data;
                                if (SelectedHdrEditItem != null && SelectedHdrEditItem.UsernameVerSo != null && SelectedHdrEditItem.UsernameVerSo == SelectedUserItem.UserName || SelectedUserItem.UserName.ToLower() == "others")
                                {
                                    sovercode = SelectedHdrEditItem.UsernameVerSo;
                                    soverdesg = SelectedHdrEditItem.DesignationVerSo;
                                }
                                else
                                {
                                    sovercode = SelectedUserItem.UserName;
                                    soverdesg = SelectedUserItem.Position;
                                }

                            }

                            else if (usertype == "soprouser")
                            {
                                SelectedUserItem = response.data;
                                if (SelectedHdrEditItem != null && SelectedHdrEditItem.UsernamePrcdSo != null && SelectedHdrEditItem.UsernamePrcdSo == SelectedUserItem.UserName || SelectedUserItem.UserName.ToLower() == "others")
                                {
                                    soprocode = SelectedHdrEditItem.UsernamePrcdSo;
                                    soprodesg = SelectedHdrEditItem.DesignationPrcdSo;
                                }
                                else
                                {
                                    soprocode = SelectedUserItem.UserName;
                                    soprodesg = SelectedUserItem.Position;
                                }
                            }

                            else if (usertype == "soagreeuser")
                            {
                                SelectedUserItem = response.data;

                                if (SelectedHdrEditItem != null && SelectedHdrEditItem.UsernameAgrdSo != null && SelectedHdrEditItem.UsernameAgrdSo == SelectedUserItem.UserName || SelectedUserItem.UserName.ToLower() == "others")
                                {
                                    soagreecode = SelectedHdrEditItem.UsernameAgrdSo;
                                    soagreedesg = SelectedHdrEditItem.DesignationAgrdSo;
                                }
                                else
                                {
                                    soagreecode = SelectedUserItem.UserName;
                                    soagreedesg = SelectedUserItem.Position;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _userDialogs.Alert(ex.Message);
                            //_userDialogs.HideLoading();
                        }

                        return SelectedUserItem;
                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }

                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }

            return new UserResponseDTO();
        }



        public async Task<ObservableCollection<DDListItems>> GetInspUserList()
        {
            try
            {
                //_userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.GetSupervisor();
                    if (response.success)
                    {
                        DDInspUserListListItems = new ObservableCollection<DDListItems>(response.data);
                        return DDInspUserListListItems;

                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }
                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }

        private void ToggleBarTapped(object obj)
        {
            Frame layout = obj as Frame;

            if (layout != null)
            {
                if (layout.IsVisible)
                {
                    layout.IsVisible = false;
                }
                else
                {
                    layout.IsVisible = true;
                }
            }
            else
            {
                Image image = obj as Image;
                string imgsrc = (image?.Source as FileImageSource).File;
                if (String.Equals(imgsrc, "RoundedAddIcon.png"))
                {
                    image.Source = "minusicon.png";
                }
                else
                {
                    image.Source = "RoundedAddIcon.png";
                }
            }
        }

        //public async Task<ObservableCollection<FormDHeaderResponseDTO>> GetMyFormDListReports(string Type)
        //{
        //    _userDialogs.ShowLoading("Loading");
        //    try
        //    {
        //        if (CrossConnectivity.Current.IsConnected)
        //        {
        //            if (Type == "Smartsearch")
        //            {
        //                GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
        //                {
        //                    StartPageNo = 1,

        //                    RecordsPerPage = pageno,

        //                    Filters = new FormDSearchGridDTO() { SmartInputValue = SmartSearch },
        //                };
        //            }
        //            else if (Type == "Detailsearch")
        //            {
        //                GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
        //                {
        //                    StartPageNo = 1,

        //                    RecordsPerPage = pageno,

        //                    Filters = new FormDSearchGridDTO() { Road_Code = SelectedRoadCode, RMU = SelectedRMU, WeekNo = SelectedWeekNo, Year = SelectedYear, WeekDay = SelectWeekday }
        //                };
        //            }
        //            else
        //            {
        //                GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
        //                {
        //                    StartPageNo = 1,

        //                    RecordsPerPage = pageno,

        //                    Filters = new FormDSearchGridDTO(),
        //                };
        //            }

        //            var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

        //            var response = await _restApi.GetFormDGridList(GridItems);

        //            if (response.success)
        //            {

        //                totalsize = response.data.TotalRecords.ToString();

        //                pagesize = response.data.FilteredRecords.ToString();

        //                MyBaseFormDList = new ObservableCollection<FormDHeaderResponseDTO>(response.data.PageResult);

        //                FormAGridListview.ItemsSource = MyBaseFormDList;

        //                SelectedRoadCode = "";

        //                SelectedRMU = "";

        //                SelectedWeekNo = "";

        //                SelectedYear = null;

        //                SelectWeekday = "";

        //                return MyBaseFormDList;
        //            }
        //            else
        //                _userDialogs.Toast(response.errorMessage);

        //            return MyBaseFormDList;
        //        }
        //        else
        //            UserDialogs.Instance.Alert("Please check your Internet Connection !");


        //        IsEmpty = MyBaseFormDList.Count == 0;

        //        LstViewHeightRequest = MyBaseFormDList.Count * 40;

        //        return MyBaseFormDList;
        //    }
        //    catch (Exception ex)
        //    {
        //        _userDialogs.Alert(ex.Message);
        //    }
        //    finally
        //    {
        //        _userDialogs.HideLoading();
        //    }

        //    return new ObservableCollection<FormDHeaderResponseDTO>();
        //}



        public async Task<ObservableCollection<DDListItems>> GetWeekddListDetails()
        {
            try
            {
                //_userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.GetDDWeekList();
                    if (response.success)
                    {
                        WeekListItems = new ObservableCollection<DDListItems>(response.data);
                        return WeekListItems;
                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }
                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }


        public async Task<ObservableCollection<DDListItems>> GetSectionByRmu(string rmu)
        {
            try
            {
                //_userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.GetSectionByRmu(rmu);
                    if (response.success)
                    {
                        DDRodeCodeListItems = new ObservableCollection<DDListItems>(response.data);
                        sectioncode.ItemsSource = DDRodeCodeListItems.Select((DDListItems arg) => arg.Text).ToList();
                        return DDRodeCodeListItems;
                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return null;

                    }
                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return null;
                }
            }

            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }


        public async Task<ObservableCollection<DDListItems>> GetTypeCode(string ddlType)
        {
            try
            {
                //_userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {

                    var ddlist = new DDLookUpDTO()
                    {
                        Type = ddlType,
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);

                    var response = await _restApi.GetTypeCodeAndValue(ddlist);

                    if (response.success)
                    {

                        DDRMUListItems = new ObservableCollection<DDListItems>(response.data);
                        return DDRMUListItems;
                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }

                }
                else
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }



        public async Task<ObservableCollection<DDListItems>> GetddListDetails(string ddtype)
        {
            try
            {
                //_userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {

                    var ddlist = new DDLookUpDTO()
                    {
                        Type = ddtype,
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);

                    var response = await _restApi.GetDDList(ddlist);

                    if (response.success)
                    {
                        //    if (ddtype == "Section Code")
                        //    {
                        //        DDRodeCodeListItems = new ObservableCollection<DDListItems>(response.data);

                        //        return DDRodeCodeListItems;

                        //    }
                        //    else if (ddtype == "RMU")
                        //    {
                        //        //DDRMUListItems = new ObservableCollection<DDListItems>(response.data);

                        //        //return DDRMUListItems;
                        //    }
                        //    else 

                        if (ddtype == "Month")
                        {
                            DDMonthListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDMonthListItems;
                        }
                        else if (ddtype == "Year")
                        {
                            DDYearListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDYearListItems;
                        }
                        else if (ddtype == "Day")
                        {
                            WeekdayListItems = new ObservableCollection<DDListItems>(response.data);
                            return WeekdayListItems;
                        }
                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }
                }
                else
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }



        public ICommand ClickMeActionCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    var SelectedFormARowItem = (FormDLabourDetailsResponseDTO)obj;
                    var actionResult = "";
                    if (App.ViewState == true)
                        actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "View");
                    else
                    {
                        if (Model.Security.IsDelete(ModuleNameList.Emergency_Response_Team))
                            actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "Edit", "View", "Delete");
                        else
                            actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "Edit", "View");
                    }

                    if (actionResult == "Delete")
                    {
                        var actionResult1 = await UserDialogs.Instance.ConfirmAsync("Are you sure want to delete this record?", "RAMS", "Yes", "No");
                        if (actionResult1)
                        {
                            _editViewModel.HdrFahPkRefNo = SelectedFormARowItem.No;
                            await DeleteLabourHeaderdetails(_editViewModel.HdrFahPkRefNo);
                            MyLabourBaseFormDList = await GetMyFormDLabourListReports(Convert.ToString(GetHeaderNoCode));
                            //FormAGridListview.ItemsSource = MyLabourBaseFormDList;
                            return;
                        }
                    }
                    if (actionResult == "Edit")
                    {
                        var editViewModel = new EditViewModel();
                        editViewModel.Type = "Edit";
                        editViewModel.HdrFahRefNo = SelectedFormARowItem.No;
                        editViewModel.HdrFahPkRefNo = GetHeaderNoCode;
                        await CoreMethods.PushPageModel<FormDAddLabourPageModel>(editViewModel);
                    }
                    else if (actionResult == "View")
                    {
                        var editViewModel = new EditViewModel();
                        editViewModel.Type = "View";
                        editViewModel.HdrFahRefNo = SelectedFormARowItem.No;
                        editViewModel.HdrFahPkRefNo = GetHeaderNoCode;
                        await CoreMethods.PushPageModel<FormDAddLabourPageModel>(editViewModel);
                    }
                });
            }
        }



        public async Task<ObservableCollection<FormDLabourDetailsResponseDTO>> GetMyFormDLabourListReports(string HeaderID, int currentpageno = 0)
        {
            //_userDialogs.ShowLoading("Loading");
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {

                    GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
                    {
                        StartPageNo = currentpageno * lbrpageno,

                        RecordsPerPage = lbrpageno,

                        sortOrder = "0",

                        Filters = new FormDSearchGridDTO(),
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

                    var response = await _restApi.GetFormDLabourGridList(GridItems, HeaderID);

                    if (response.success)
                    {

                        LbrStartPage = response.data.TotalRecords == 0 ? 0 : (response.data.PageNo + 1);

                        LbrTotalSize = response.data.TotalRecords.ToString();

                        if (((currentpageno + 1) * lbrpageno) > Convert.ToInt32(LbrTotalSize))
                            LbrPageSize = LbrTotalSize;
                        else
                            LbrPageSize = ((currentpageno + 1) * lbrpageno).ToString();

                        MyLabourBaseFormDList = new ObservableCollection<FormDLabourDetailsResponseDTO>(response.data.PageResult);

                        //SelectedRoadCode = "";

                        SelectedRMU = "";

                        IsEmpty = MyLabourBaseFormDList.Count == 0;

                        LstViewHeightRequest = MyLabourBaseFormDList.Count * 40;

                        return MyLabourBaseFormDList;
                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }
                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return null;
                }

            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }

            return new ObservableCollection<FormDLabourDetailsResponseDTO>();
        }


        public async Task<int> DeleteLabourHeaderdetails(int HeaderCode)

        {
            try

            {

                //_userDialogs.ShowLoading("loading");

                if (CrossConnectivity.Current.IsConnected)
                {


                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(HeaderCode);

                    var response = await _restApi.DeleteFormDLabourHdr(HeaderCode);


                    if (response.success)
                    {
                        iResultValue = response.data;

                        if (iResultValue != 0)
                        {
                            await UserDialogs.Instance.AlertAsync("Data Deleted Successfully.", "RAMS", "0K");

                            return iResultValue;

                        }
                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return 0;
                    }

                    return iResultValue;
                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                // _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }
            return iResultValue;
        }



        public async void GetUserData()
        {
            try
            {
                App.srecsignview = ""; App.svetsignview = ""; App.sversignview = "";

                App.ssoversignview = ""; App.ssoprosignview = ""; App.ssoagreesignview = "";


                try
                {



                    try
                    {

                        recsignview = CurrentPage.FindByName<SignaturePadView>("recsignview");

                        Stream image = await recsignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);


                        if (!recsignview.IsBlank)
                        {

                            using (BinaryReader binaryReader = new BinaryReader(image))
                            {
                                binaryReader.BaseStream.Position = 0;

                                byte[] Signature = binaryReader.ReadBytes((int)image.Length);
                                //await PCLHelper.SaveImage(Signature, "Inspsign", myCoolFolder);
                            }

                            var signatureMemoryStream = image as System.IO.MemoryStream;

                            var byteArray = signatureMemoryStream.ToArray();

                            string base64String = Convert.ToBase64String(byteArray);

                            App.srecsignview = base64String;
                        }
                    }
                    catch { }


                    vetsignview = CurrentPage.FindByName<SignaturePadView>("vetsignview");

                    Stream vimage = await vetsignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);



                    if (!vetsignview.IsBlank)
                    {


                        using (BinaryReader binaryReader = new BinaryReader(vimage))
                        {
                            binaryReader.BaseStream.Position = 0;

                            byte[] Signature = binaryReader.ReadBytes((int)vimage.Length);
                            //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                        }

                        var vsignatureMemoryStream = vimage as System.IO.MemoryStream;

                        var byteArray = vsignatureMemoryStream.ToArray();

                        string base64String = Convert.ToBase64String(byteArray);

                        //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                        App.svetsignview = base64String;

                    }


                    versignview = CurrentPage.FindByName<SignaturePadView>("versignview");

                    Stream verimage = await versignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);



                    if (!versignview.IsBlank)
                    {


                        using (BinaryReader binaryReader = new BinaryReader(verimage))
                        {
                            binaryReader.BaseStream.Position = 0;

                            byte[] Signature = binaryReader.ReadBytes((int)verimage.Length);
                            //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                        }

                        var vsignatureMemoryStream = verimage as System.IO.MemoryStream;

                        var byteArray = vsignatureMemoryStream.ToArray();

                        string base64String = Convert.ToBase64String(byteArray);

                        //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                        App.sversignview = base64String;

                    }



                    soversignview = CurrentPage.FindByName<SignaturePadView>("soversignview");

                    Stream soverimage = await soversignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                    if (!soversignview.IsBlank)
                    {
                        using (BinaryReader binaryReader = new BinaryReader(soverimage))
                        {
                            binaryReader.BaseStream.Position = 0;

                            byte[] Signature = binaryReader.ReadBytes((int)soverimage.Length);
                            //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                        }

                        var vsignatureMemoryStream = soverimage as System.IO.MemoryStream;

                        var byteArray = vsignatureMemoryStream.ToArray();

                        string base64String = Convert.ToBase64String(byteArray);

                        //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                        App.ssoversignview = base64String;

                    }


                    soprosignview = CurrentPage.FindByName<SignaturePadView>("soprosignview");

                    Stream soproimage = await soprosignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                    if (!soprosignview.IsBlank)
                    {

                        using (BinaryReader binaryReader = new BinaryReader(soproimage))
                        {
                            binaryReader.BaseStream.Position = 0;

                            byte[] Signature = binaryReader.ReadBytes((int)soproimage.Length);
                            //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                        }

                        var vsignatureMemoryStream = soproimage as System.IO.MemoryStream;

                        var byteArray = vsignatureMemoryStream.ToArray();

                        string base64String = Convert.ToBase64String(byteArray);

                        //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                        App.ssoprosignview = base64String;

                    }


                    soagreesignview = CurrentPage.FindByName<SignaturePadView>("soagreesignview");

                    Stream soagreeimage = await soagreesignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                    if (!soagreesignview.IsBlank)
                    {
                        using (BinaryReader binaryReader = new BinaryReader(soagreeimage))
                        {
                            binaryReader.BaseStream.Position = 0;

                            byte[] Signature = binaryReader.ReadBytes((int)soagreeimage.Length);
                            //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                        }

                        var vsignatureMemoryStream = soagreeimage as System.IO.MemoryStream;

                        var byteArray = vsignatureMemoryStream.ToArray();

                        string base64String = Convert.ToBase64String(byteArray);

                        //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                        App.ssoagreesignview = base64String;

                    }
                }
                catch
                {

                }

                //User list to update on header including designation


                if (GetHeaderNoCode == 0 || GetHeaderNoCode == 1)
                    GetHeaderNoCode = App.DetailHeaderCode;

                if (App.srecsignview == null) { App.srecsignview = null; }
                if (App.svetsignview == null) { App.svetsignview = null; }
                if (App.sversignview == null) { App.sversignview = null; }
                if (App.ssoversignview == null) { App.ssoversignview = null; }
                if (App.ssoprosignview == null) { App.ssoprosignview = null; }
                if (App.ssoagreesignview == null) { App.ssoagreesignview = null; }


                await CurrentPage.Navigation.PopAsync();
            }

            catch (Exception ex)
            {
                throw ex;
            }

        }



        public async Task<ObservableCollection<FormDHeaderResponseDTO>> UpdateSignature(string Type)
        {
            //_userDialogs.ShowLoading("Loading");

            try
            {

                if (CrossConnectivity.Current.IsConnected)
                {

                    //if (inspid == 0) { inspid = null; }
                    //if (verid == 0) { verid = null; }

                    if (Type == "Save")
                    {
                        objValue = new FormDHeaderRequestDTO()
                        {

                            No = GetHeaderNoCode,
                            ReportedBySign = App.srecsignview,
                            ReportedByUserId = Convert.ToInt32(SelectedRecName),
                            ReportedByUsername = reccode,
                            ReportedByDesignation = recdesg,
                            DateReported = recdate.NullableDate,
                            SignVer = App.sversignview,
                            UseridVer = SelectedVerName,
                            UsernameVer = vercode,
                            DesignationVer = verdesg,
                            DtVer = verdate.NullableDate,
                            SignVet = App.svetsignview,
                            UseridVet = SelectedVetName,
                            UsernameVet = vetcode,
                            DesignationVet = vetdesg,
                            DtVet = vetdate.NullableDate,
                            SignVerSo = App.ssoversignview,
                            UseridVerSo = SelectedsoverName,
                            UsernameVerSo = sovercode,
                            DesignationVerSo = soverdesg,
                            DtVerSo = soverdate.NullableDate,
                            SignPrcdSo = App.ssoprosignview,
                            UseridPrcdSo = SelectedproName,
                            UsernamePrcdSo = soprocode,
                            DesignationPrcdSo = soprodesg,
                            DtPrcdSo = soprodate.NullableDate,
                            SignAgrdSo = App.ssoagreesignview,
                            UseridAgrdSo = SelectedsoagreeName,
                            UsernameAgrdSo = soagreecode,
                            DesignationAgrdSo = soagreedesg,
                            DtAgrdSo = soagreedate.NullableDate,
                            SubmitSts = false


                        };
                    }
                    else if (Type == "Submit")
                    {
                        objValue = new FormDHeaderRequestDTO()
                        {

                            No = GetHeaderNoCode,
                            ReportedBySign = App.srecsignview,
                            ReportedByUserId = Convert.ToInt32(SelectedRecName),
                            ReportedByUsername = reccode,
                            ReportedByDesignation = recdesg,
                            DateReported = recdate.NullableDate,
                            SignVer = App.sversignview,
                            UseridVer = SelectedVerName,
                            UsernameVer = vercode,
                            DesignationVer = verdesg,
                            DtVer = verdate.NullableDate,
                            SignVet = App.svetsignview,
                            UseridVet = SelectedVetName,
                            UsernameVet = vetcode,
                            DesignationVet = vetdesg,
                            DtVet = vetdate.NullableDate,
                            SignVerSo = App.ssoversignview,
                            UseridVerSo = SelectedsoverName,
                            UsernameVerSo = sovercode,
                            DesignationVerSo = soverdesg,
                            DtVerSo = soverdate.NullableDate,
                            SignPrcdSo = App.ssoprosignview,
                            UseridPrcdSo = SelectedproName,
                            UsernamePrcdSo = soprocode,
                            DesignationPrcdSo = soprodesg,
                            DtPrcdSo = soprodate.NullableDate,
                            SignAgrdSo = App.ssoagreesignview,
                            UseridAgrdSo = SelectedsoagreeName,
                            UsernameAgrdSo = soagreecode,
                            DesignationAgrdSo = soagreedesg,
                            DtAgrdSo = soagreedate.NullableDate,
                            SubmitSts = true


                        };

                    }

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(objValue);

                    var response = await _restApi.UpdateSignatureFormD(objValue);


                    if (response.success)
                    {
                        try

                        {
                            if (response.data > 0)
                            {
                                //_userDialogs.HideLoading();

                                if (Type == "Save")
                                    await UserDialogs.Instance.AlertAsync("Data Saved Successfully.", "RAMS", "OK");
                                else
                                    await UserDialogs.Instance.AlertAsync("Data Submitted Successfully.", "RAMS", "OK");

                            }
                            else
                            {
                                // _userDialogs.HideLoading();

                            }


                        }
                        catch (Exception ex)
                        {
                            //_userDialogs.Alert(ex.Message);
                            //_userDialogs.HideLoading();

                            //UserDialogs.Instance.Alert("Header Details Saved Successfully.");

                        }


                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }

                    //return DetailFromAHdrGridListItems;

                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return null;
                }


            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }

            return new ObservableCollection<FormDHeaderResponseDTO>();


        }





        //Equiment Detials
        public ICommand ClickMeEquimentActionCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    var SelectedFormARowItem = (FormDEquipDetailsResponseDTO)obj;
                    var actionResult = "";

                    if (App.SubmitViewState == true)
                        actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "View");
                    else
                    {
                        if (Model.Security.IsDelete(ModuleNameList.Emergency_Response_Team))
                            actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "Edit", "View", "Delete");
                        else
                            actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "Edit", "View");
                    }

                    if (actionResult == "Delete")
                    {
                        var actionResult1 = await UserDialogs.Instance.ConfirmAsync("Are you sure want to delete this record?", "RAMS", "Yes", "No");
                        if (actionResult1)
                        {
                            _editViewModel.HdrFahPkRefNo = SelectedFormARowItem.No;
                            await DeleteEquipmentHeaderdetails(_editViewModel.HdrFahPkRefNo);
                            MyEquimentBaseFormDList = await GetMyFormDEquipmentListReports(Convert.ToString(GetHeaderNoCode));
                            //FormAEquimentGridListview.ItemsSource = MyEquimentBaseFormDList;
                            return;
                        }
                    }
                    if (actionResult == "Edit")
                    {
                        var editViewModel = new EditViewModel();
                        editViewModel.Type = "Edit";
                        editViewModel.HdrFahRefNo = SelectedFormARowItem.No;
                        editViewModel.HdrFahPkRefNo = GetHeaderNoCode;
                        await CoreMethods.PushPageModel<FormDAddEquipmentPageModel>(editViewModel);
                    }
                    else if (actionResult == "View")
                    {
                        var editViewModel = new EditViewModel();
                        editViewModel.Type = "View";
                        editViewModel.HdrFahRefNo = SelectedFormARowItem.No;
                        editViewModel.HdrFahPkRefNo = GetHeaderNoCode;
                        await CoreMethods.PushPageModel<FormDAddEquipmentPageModel>(editViewModel);
                    }
                });
            }
        }



        public async Task<ObservableCollection<FormDEquipDetailsResponseDTO>> GetMyFormDEquipmentListReports(string HeaderID, int currentpageno = 0)
        {
            //_userDialogs.ShowLoading("Loading");
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    //if (Type == "Smartsearch")
                    //{
                    //    GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
                    //    {
                    //        StartPageNo = 1,

                    //        RecordsPerPage = pageno,

                    //        Filters = new FormDSearchGridDTO() { SmartInputValue = SmartSearch },
                    //    };
                    //}
                    //else if (Type == "Detailsearch")
                    //{
                    //    GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
                    //    {
                    //        StartPageNo = 1StartPageNo = 1,

                    //        RecordsPerPage = pageno,

                    //        Filters = new FormDSearchGridDTO() { Road_Code = SelectedRoadCode, RMU = SelectedRMU, WeekNo = SelectedWeekNo, Year = SelectedYear, WeekDay = SelectWeekday }
                    //    };
                    //}
                    //else
                    {
                        GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
                        {
                            StartPageNo = currentpageno * eqpmtpageno,

                            RecordsPerPage = eqpmtpageno,

                            sortOrder = "0",

                            Filters = new FormDSearchGridDTO(),
                        };
                    }

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

                    var response = await _restApi.GetFormDEqpGridList(GridItems, HeaderID);

                    if (response.success)
                    {
                        //if (response.data.PageResult.Count > 0)
                        //{

                        StartPage = response.data.TotalRecords == 0 ? 0 : (response.data.PageNo + 1);

                        TotalSize = response.data.TotalRecords.ToString();

                        if (((currentpageno + 1) * eqpmtpageno) > Convert.ToInt32(TotalSize))
                            PageSize = TotalSize;
                        else
                            PageSize = ((currentpageno + 1) * eqpmtpageno).ToString();

                        MyEquimentBaseFormDList = new ObservableCollection<FormDEquipDetailsResponseDTO>(response.data.PageResult);

                        //FormAGridListview.ItemsSource = MyBaseFormDList;

                        //SelectedRoadCode = "";

                        SelectedRMU = "";
                        //}
                        //else
                        //{
                        //    return MyEquimentBaseFormDList;
                        //}

                        IsEmpty = MyEquimentBaseFormDList.Count == 0;

                        LstViewHeightRequest = MyEquimentBaseFormDList.Count * 40;
                        return MyEquimentBaseFormDList;
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return MyEquimentBaseFormDList;
                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                //_userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }

            return new ObservableCollection<FormDEquipDetailsResponseDTO>();
        }


        public async Task<int> DeleteEquipmentHeaderdetails(int HeaderCode)

        {
            try

            {

                //_userDialogs.ShowLoading("loading");

                if (CrossConnectivity.Current.IsConnected)
                {


                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(HeaderCode);

                    var response = await _restApi.DeleteFormDEqpHdr(HeaderCode);


                    if (response.success)
                    {
                        iResultValue = response.data;

                        if (iResultValue != 0)
                        {
                            await UserDialogs.Instance.AlertAsync("Data Deleted Successfully.", "RAMS", "0K");

                            return iResultValue;

                        }
                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return 0;
                    }

                    return iResultValue;
                }
                else
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                // _userdialogs.hideloading();
            }
            return iResultValue;
        }


        //Material Details
        public ICommand ClickMeMaterialActionCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    var SelectedFormARowItem = (FormDMaterialDetailsResponseDTO)obj;
                    var actionResult = "";
                    if (App.SubmitViewState == true)
                        actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "View");
                    else
                    {
                        if (Model.Security.IsDelete(ModuleNameList.Emergency_Response_Team))
                            actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "Edit", "View", "Delete");
                        else
                            actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "Edit", "View");
                    }

                    if (actionResult == "Delete")
                    {
                        var actionResult1 = await UserDialogs.Instance.ConfirmAsync("Are you sure want to delete this record?", "RAMS", "Yes", "No");
                        if (actionResult1)
                        {
                            _editViewModel.HdrFahPkRefNo = SelectedFormARowItem.No;
                            await DeleteMaterialHeaderdetails(_editViewModel.HdrFahPkRefNo);
                            MyMaterialBaseFormDList = await GetMyFormDMaterialListReports(Convert.ToString(GetHeaderNoCode));
                            //FormAMaterialGridListview.ItemsSource = MyEquimentBaseFormDList;
                            return;
                        }
                    }
                    if (actionResult == "Edit")
                    {
                        var editViewModel = new EditViewModel();
                        editViewModel.Type = "Edit";
                        editViewModel.HdrFahRefNo = SelectedFormARowItem.No;
                        editViewModel.HdrFahPkRefNo = GetHeaderNoCode;
                        await CoreMethods.PushPageModel<FormDAddMaterialPageModel>(editViewModel);
                    }
                    else if (actionResult == "View")
                    {
                        var editViewModel = new EditViewModel();
                        editViewModel.Type = "View";
                        editViewModel.HdrFahRefNo = SelectedFormARowItem.No;
                        editViewModel.HdrFahPkRefNo = GetHeaderNoCode;
                        await CoreMethods.PushPageModel<FormDAddMaterialPageModel>(editViewModel);
                    }
                });
            }
        }



        public async Task<ObservableCollection<FormDMaterialDetailsResponseDTO>> GetMyFormDMaterialListReports(string HeaderID, int currentpageno = 0)
        {
            //_userDialogs.ShowLoading("Loading");
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    //if (Type == "Smartsearch")
                    //{
                    //    GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
                    //    {
                    //        StartPageNo = 1,

                    //        RecordsPerPage = pageno,

                    //        Filters = new FormDSearchGridDTO() { SmartInputValue = SmartSearch },
                    //    };
                    //}
                    //else if (Type == "Detailsearch")
                    //{
                    //    GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
                    //    {
                    //        StartPageNo = 1,

                    //        RecordsPerPage = pageno,

                    //        Filters = new FormDSearchGridDTO() { Road_Code = SelectedRoadCode, RMU = SelectedRMU, WeekNo = SelectedWeekNo, Year = SelectedYear, WeekDay = SelectWeekday }
                    //    };
                    //}
                    //else

                    GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
                    {
                        StartPageNo = currentpageno * mtrlpageno,

                        RecordsPerPage = mtrlpageno,

                        sortOrder = "0",

                        Filters = new FormDSearchGridDTO(),
                    };


                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

                    var response = await _restApi.GetFormDMaterialGridList(GridItems, HeaderID);

                    if (response.success)
                    {

                        //if (response.data.PageResult.Count > 0)
                        //{
                        MtrlStartPage = response.data.TotalRecords == 0 ? 0 : (response.data.PageNo + 1);

                        MtrlTotalSize = response.data.TotalRecords.ToString();

                        if (((currentpageno + 1) * dtlpageno) > Convert.ToInt32(MtrlTotalSize))
                            MtrlPageSize = MtrlTotalSize;
                        else
                            MtrlPageSize = ((currentpageno + 1) * mtrlpageno).ToString();

                        MyMaterialBaseFormDList = new ObservableCollection<FormDMaterialDetailsResponseDTO>(response.data.PageResult);

                        //FormAGridListview.ItemsSource = MyBaseFormDList;

                        //SelectedRoadCode = "";

                        SelectedRMU = "";
                        //}
                        //else
                        //{
                        //    return MyMaterialBaseFormDList;
                        //}
                        IsEmpty = MyMaterialBaseFormDList.Count == 0;

                        LstViewHeightRequest = MyMaterialBaseFormDList.Count * 40;

                        return MyMaterialBaseFormDList;
                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }
                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return null;

                }
            }
            catch (Exception ex)
            {
                //_userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }

            return new ObservableCollection<FormDMaterialDetailsResponseDTO>();
        }


        public async Task<int> DeleteMaterialHeaderdetails(int HeaderCode)

        {
            try

            {

                //_userDialogs.ShowLoading("loading");

                if (CrossConnectivity.Current.IsConnected)
                {


                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(HeaderCode);

                    var response = await _restApi.DeleteFormDmaterialHdr(HeaderCode);


                    if (response.success)
                    {
                        iResultValue = response.data;

                        if (iResultValue != 0)
                        {
                            await UserDialogs.Instance.AlertAsync("Data Deleted Successfully.", "RAMS", "0K");

                            return iResultValue;

                        }
                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return 0;
                    }

                    return iResultValue;
                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }
            return iResultValue;
        }



        //detail View



        public ICommand ClickMeDetailActionCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    var SelectedFormARowItem = (FormDDetailsResponseDTO)obj;
                    var actionResult = "";
                    if (App.SubmitViewState == true)
                        actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "View");
                    else
                    {
                        if (Model.Security.IsDelete(ModuleNameList.Emergency_Response_Team))
                            actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "Edit", "View", "Delete");
                        else
                            actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "Edit", "View");
                    }
                    if (actionResult == "Delete")
                    {
                        var actionResult1 = await UserDialogs.Instance.ConfirmAsync("Are you sure want to delete this record?", "RAMS", "Yes", "No");
                        if (actionResult1)
                        {
                            _editViewModel.HdrFahPkRefNo = SelectedFormARowItem.No;
                            await DeleteDetailHeaderdetails(_editViewModel.HdrFahPkRefNo);
                            MyDetailBaseFormDList = await GetMyFormDDetailListReports(Convert.ToString(GetHeaderNoCode));
                            //FormAFlatDetailGridListview.ItemsSource = MyDetailBaseFormDList;
                            return;
                        }
                    }
                    if (actionResult == "Edit")
                    {
                        var editViewModel = new EditViewModel();
                        editViewModel.Type = "Edit";
                        editViewModel.HdrFahRefNo = SelectedFormARowItem.No;
                        editViewModel.HdrFahPkRefNo = GetHeaderNoCode;
                        editViewModel.Rmu = StrRefcode;
                        editViewModel.Section = SelectedRoadCode;
                        await CoreMethods.PushPageModel<FormDAddDetailsPageModel>(editViewModel);
                    }
                    else if (actionResult == "View")
                    {
                        var editViewModel = new EditViewModel();
                        editViewModel.Type = "View";
                        editViewModel.HdrFahRefNo = SelectedFormARowItem.No;
                        editViewModel.HdrFahPkRefNo = GetHeaderNoCode;
                        editViewModel.Section = SelectedRoadCode;
                        await CoreMethods.PushPageModel<FormDAddDetailsPageModel>(editViewModel);
                    }
                    else if (actionResult == "U See U Act")
                    {
                        var editViewModel = new EditViewModel();
                        editViewModel.HdrFahRefNo = SelectedFormARowItem.No;
                        editViewModel.HdrFahPkRefNo = GetHeaderNoCode;
                        App.FormDDetailCode = GetHeaderNoCode;
                        //await CoreMethods.PushPageModel<FormDAddDetailsPageModel>(_editViewModel);
                        await CurrentPage.Navigation.PushAsync(new FormDPdfUpload());

                    }
                    else if (actionResult == "War")
                    {
                        var editViewModel = new EditViewModel();
                        editViewModel.HdrFahRefNo = SelectedFormARowItem.No;
                        editViewModel.HdrFahPkRefNo = GetHeaderNoCode;
                        App.FormDDetailCode = GetHeaderNoCode;
                        await CurrentPage.Navigation.PushAsync(new FormDImageUpload());
                    }
                });
            }
        }



        public async Task<ObservableCollection<FormDDetailsResponseDTO>> GetMyFormDDetailListReports(string HeaderID, int currentpageno = 0)
        {
            //_userDialogs.ShowLoading("Loading");
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    //if (Type == "Smartsearch")
                    //{
                    //    GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
                    //    {
                    //        StartPageNo = 1,

                    //        RecordsPerPage = pageno,

                    //        Filters = new FormDSearchGridDTO() { SmartInputValue = SmartSearch },
                    //    };
                    //}
                    //else if (Type == "Detailsearch")
                    //{
                    //    GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
                    //    {
                    //        StartPageNo = 1,

                    //        RecordsPerPage = pageno,

                    //        Filters = new FormDSearchGridDTO() { Road_Code = SelectedRoadCode, RMU = SelectedRMU, WeekNo = SelectedWeekNo, Year = SelectedYear, WeekDay = SelectWeekday }
                    //    };
                    //}
                    //else
                    {
                        GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
                        {
                            StartPageNo = currentpageno * dtlpageno,

                            RecordsPerPage = dtlpageno,

                            sortOrder = "0",

                            Filters = new FormDSearchGridDTO(),
                        };
                    }

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

                    var response = await _restApi.GetFormDDtlGridList(GridItems, HeaderID);

                    if (response.success)
                    {
                        //if (response.data.PageResult.Count > 0)
                        //{
                        DtlStartPage = response.data.TotalRecords == 0 ? 0 : (response.data.PageNo + 1);

                        DtlTotalSize = response.data.TotalRecords.ToString();

                        if (((currentpageno + 1) * dtlpageno) > Convert.ToInt32(DtlTotalSize))
                            DtlPageSize = DtlTotalSize;
                        else
                            DtlPageSize = ((currentpageno + 1) * dtlpageno).ToString();
                        //= response.data.FilteredRecords.ToString();

                        MyDetailBaseFormDList = new ObservableCollection<FormDDetailsResponseDTO>(response.data.PageResult);

                        //FormAGridListview.ItemsSource = MyBaseFormDList;

                        //SelectedRoadCode = "";

                        SelectedRMU = "";
                        //}
                        //else
                        //{
                        //    return MyDetailBaseFormDList;
                        //}
                        IsEmpty = MyMaterialBaseFormDList.Count == 0;

                        LstViewHeightRequest = MyMaterialBaseFormDList.Count * 40;

                        return MyDetailBaseFormDList;
                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }
                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return null;
                }

            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }

            return new ObservableCollection<FormDDetailsResponseDTO>();
        }


        public async Task<int> DeleteDetailHeaderdetails(int HeaderCode)

        {
            try

            {

                //_userDialogs.ShowLoading("loading");

                if (CrossConnectivity.Current.IsConnected)
                {


                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(HeaderCode);

                    var response = await _restApi.DeleteFormDDtl(HeaderCode);


                    if (response.success)
                    {
                        iResultValue = response.data;

                        if (iResultValue != 0)
                        {
                            await UserDialogs.Instance.AlertAsync("Data Deleted Successfully.", "RAMS", "0K");

                            return iResultValue;

                        }
                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return 0;
                    }


                    return iResultValue;
                }
                else
                {
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                //_userDialogs.HideLoading();
            }
            return iResultValue;
        }

        public ICommand FormDSaveCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    if (GetHeaderNoCode == 0)
                    {

                        if (Vaildate())
                        {
                            var response = SaveFormDHeaderList();
                        }


                        btnLabourCommand.IsVisible = true;
                        btnEquipment.IsVisible = true;
                        btnMaterial.IsVisible = true;
                        //btnDetail.IsVisible = true;
                        btnDetailX.IsVisible = true;

                        btnLabourCommandX.IsVisible = true;
                        btnEquipmentX.IsVisible = true;
                        btnMaterialX.IsVisible = true;

                        btnSave.IsEnabled = false;
                    }
                    else
                    {

                        try
                        {
                            App.srecsignview = null; App.svetsignview = null; App.sversignview = null;

                            App.ssoversignview = null; App.ssoprosignview = null; App.ssoagreesignview = null;


                            try
                            {
                                try
                                {

                                    recsignview = CurrentPage.FindByName<SignaturePadView>("recsignview");

                                    Stream image = await recsignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);


                                    if (!recsignview.IsBlank)
                                    {

                                        using (BinaryReader binaryReader = new BinaryReader(image))
                                        {
                                            binaryReader.BaseStream.Position = 0;

                                            byte[] Signature = binaryReader.ReadBytes((int)image.Length);
                                            //await PCLHelper.SaveImage(Signature, "Inspsign", myCoolFolder);
                                        }

                                        var signatureMemoryStream = image as System.IO.MemoryStream;

                                        var byteArray = signatureMemoryStream.ToArray();

                                        string base64String = Convert.ToBase64String(byteArray);

                                        App.srecsignview = base64String;
                                    }
                                }
                                catch { }


                                vetsignview = CurrentPage.FindByName<SignaturePadView>("vetsignview");

                                Stream vimage = await vetsignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);



                                if (!vetsignview.IsBlank)
                                {


                                    using (BinaryReader binaryReader = new BinaryReader(vimage))
                                    {
                                        binaryReader.BaseStream.Position = 0;

                                        byte[] Signature = binaryReader.ReadBytes((int)vimage.Length);
                                        //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                                    }

                                    var vsignatureMemoryStream = vimage as System.IO.MemoryStream;

                                    var byteArray = vsignatureMemoryStream.ToArray();

                                    string base64String = Convert.ToBase64String(byteArray);

                                    //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                                    App.svetsignview = base64String;

                                }


                                versignview = CurrentPage.FindByName<SignaturePadView>("versignview");

                                Stream verimage = await versignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);



                                if (!versignview.IsBlank)
                                {


                                    using (BinaryReader binaryReader = new BinaryReader(verimage))
                                    {
                                        binaryReader.BaseStream.Position = 0;

                                        byte[] Signature = binaryReader.ReadBytes((int)verimage.Length);
                                        //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                                    }

                                    var vsignatureMemoryStream = verimage as System.IO.MemoryStream;

                                    var byteArray = vsignatureMemoryStream.ToArray();

                                    string base64String = Convert.ToBase64String(byteArray);

                                    //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                                    App.sversignview = base64String;

                                }
                                soversignview = CurrentPage.FindByName<SignaturePadView>("soversignview");

                                Stream soverimage = await soversignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                                if (!soversignview.IsBlank)
                                {

                                    using (BinaryReader binaryReader = new BinaryReader(soverimage))
                                    {
                                        binaryReader.BaseStream.Position = 0;

                                        byte[] Signature = binaryReader.ReadBytes((int)soverimage.Length);
                                        //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                                    }

                                    var vsignatureMemoryStream = soverimage as System.IO.MemoryStream;

                                    var byteArray = vsignatureMemoryStream.ToArray();

                                    string base64String = Convert.ToBase64String(byteArray);

                                    //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                                    App.ssoversignview = base64String;

                                }


                                soprosignview = CurrentPage.FindByName<SignaturePadView>("soprosignview");

                                Stream soproimage = await soprosignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);


                                if (!soprosignview.IsBlank)
                                {


                                    using (BinaryReader binaryReader = new BinaryReader(soproimage))
                                    {
                                        binaryReader.BaseStream.Position = 0;

                                        byte[] Signature = binaryReader.ReadBytes((int)soproimage.Length);
                                        //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                                    }

                                    var vsignatureMemoryStream = soproimage as System.IO.MemoryStream;

                                    var byteArray = vsignatureMemoryStream.ToArray();

                                    string base64String = Convert.ToBase64String(byteArray);

                                    //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                                    App.ssoprosignview = base64String;

                                }


                                soagreesignview = CurrentPage.FindByName<SignaturePadView>("soagreesignview");

                                Stream soagreeimage = await soagreesignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                                if (!soagreesignview.IsBlank)
                                {


                                    using (BinaryReader binaryReader = new BinaryReader(soagreeimage))
                                    {
                                        binaryReader.BaseStream.Position = 0;

                                        byte[] Signature = binaryReader.ReadBytes((int)soagreeimage.Length);
                                        //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                                    }

                                    var vsignatureMemoryStream = soagreeimage as System.IO.MemoryStream;

                                    var byteArray = vsignatureMemoryStream.ToArray();

                                    string base64String = Convert.ToBase64String(byteArray);

                                    //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                                    App.ssoagreesignview = base64String;

                                }
                            }
                            catch
                            {

                            }

                            //User list to update on header including designation


                            if (GetHeaderNoCode == 0 || GetHeaderNoCode == 1)
                                GetHeaderNoCode = App.DetailHeaderCode;

                            if (App.srecsignview == null || App.srecsignview == "") { App.srecsignview = null; }
                            if (App.svetsignview == null || App.svetsignview == "") { App.svetsignview = null; }
                            if (App.sversignview == null || App.sversignview == "") { App.sversignview = null; }
                            if (App.ssoversignview == null || App.ssoversignview == "") { App.ssoversignview = null; }
                            if (App.ssoprosignview == null || App.ssoprosignview == "") { App.ssoprosignview = null; }
                            if (App.ssoagreesignview == null || App.ssoagreesignview == "") { App.ssoagreesignview = null; }


                            await UpdateSignature("Save");

                            await CurrentPage.Navigation.PopAsync();
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }
                    }

                });
            }

        }


        public ICommand FormDSubmitedCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    if (GetHeaderNoCode == 0)
                    {

                        if (Vaildate())
                        {
                            var response = SaveFormDHeaderList();

                            btnLabourCommand.IsVisible = true;
                            btnEquipment.IsVisible = true;
                            btnMaterial.IsVisible = true;
                            //btnDetail.IsVisible = true;
                            btnDetailX.IsVisible = true;

                            btnLabourCommandX.IsVisible = true;
                            btnEquipmentX.IsVisible = true;
                            btnMaterialX.IsVisible = true;
                        }
                    }
                    else
                    {

                        try
                        {
                            App.srecsignview = null; App.svetsignview = null; App.sversignview = null;

                            App.ssoversignview = null; App.ssoprosignview = null; App.ssoagreesignview = null;

                            try
                            {

                                try
                                {

                                    recsignview = CurrentPage.FindByName<SignaturePadView>("recsignview");

                                    Stream image = await recsignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);


                                    if (!recsignview.IsBlank)
                                    {

                                        using (BinaryReader binaryReader = new BinaryReader(image))
                                        {
                                            binaryReader.BaseStream.Position = 0;

                                            byte[] Signature = binaryReader.ReadBytes((int)image.Length);
                                            //await PCLHelper.SaveImage(Signature, "Inspsign", myCoolFolder);
                                        }

                                        var signatureMemoryStream = image as System.IO.MemoryStream;

                                        var byteArray = signatureMemoryStream.ToArray();

                                        string base64String = Convert.ToBase64String(byteArray);

                                        App.srecsignview = base64String;
                                    }
                                }
                                catch { }


                                vetsignview = CurrentPage.FindByName<SignaturePadView>("vetsignview");

                                Stream vimage = await vetsignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                                if (!vetsignview.IsBlank)
                                {


                                    using (BinaryReader binaryReader = new BinaryReader(vimage))
                                    {
                                        binaryReader.BaseStream.Position = 0;

                                        byte[] Signature = binaryReader.ReadBytes((int)vimage.Length);
                                        //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                                    }

                                    var vsignatureMemoryStream = vimage as System.IO.MemoryStream;

                                    var byteArray = vsignatureMemoryStream.ToArray();

                                    string base64String = Convert.ToBase64String(byteArray);

                                    //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                                    App.svetsignview = base64String;

                                }


                                versignview = CurrentPage.FindByName<SignaturePadView>("versignview");

                                Stream verimage = await versignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);



                                if (!versignview.IsBlank)
                                {


                                    using (BinaryReader binaryReader = new BinaryReader(verimage))
                                    {
                                        binaryReader.BaseStream.Position = 0;

                                        byte[] Signature = binaryReader.ReadBytes((int)verimage.Length);
                                        //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                                    }

                                    var vsignatureMemoryStream = verimage as System.IO.MemoryStream;

                                    var byteArray = vsignatureMemoryStream.ToArray();

                                    string base64String = Convert.ToBase64String(byteArray);

                                    //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                                    App.sversignview = base64String;

                                }



                                soversignview = CurrentPage.FindByName<SignaturePadView>("soversignview");

                                Stream soverimage = await soversignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);



                                if (!soversignview.IsBlank)
                                {


                                    using (BinaryReader binaryReader = new BinaryReader(soverimage))
                                    {
                                        binaryReader.BaseStream.Position = 0;

                                        byte[] Signature = binaryReader.ReadBytes((int)soverimage.Length);
                                        //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                                    }

                                    var vsignatureMemoryStream = soverimage as System.IO.MemoryStream;

                                    var byteArray = vsignatureMemoryStream.ToArray();

                                    string base64String = Convert.ToBase64String(byteArray);

                                    //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                                    App.ssoversignview = base64String;

                                }


                                soprosignview = CurrentPage.FindByName<SignaturePadView>("soprosignview");

                                Stream soproimage = await soprosignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);



                                if (!soprosignview.IsBlank)
                                {


                                    using (BinaryReader binaryReader = new BinaryReader(soproimage))
                                    {
                                        binaryReader.BaseStream.Position = 0;

                                        byte[] Signature = binaryReader.ReadBytes((int)soproimage.Length);
                                        //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                                    }

                                    var vsignatureMemoryStream = soproimage as System.IO.MemoryStream;

                                    var byteArray = vsignatureMemoryStream.ToArray();

                                    string base64String = Convert.ToBase64String(byteArray);

                                    //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                                    App.ssoprosignview = base64String;

                                }


                                soagreesignview = CurrentPage.FindByName<SignaturePadView>("soagreesignview");

                                Stream soagreeimage = await soagreesignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);



                                if (!soagreesignview.IsBlank)
                                {


                                    using (BinaryReader binaryReader = new BinaryReader(soagreeimage))
                                    {
                                        binaryReader.BaseStream.Position = 0;

                                        byte[] Signature = binaryReader.ReadBytes((int)soagreeimage.Length);
                                        //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                                    }

                                    var vsignatureMemoryStream = soagreeimage as System.IO.MemoryStream;

                                    var byteArray = vsignatureMemoryStream.ToArray();

                                    string base64String = Convert.ToBase64String(byteArray);

                                    //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                                    App.ssoagreesignview = base64String;

                                }
                            }
                            catch
                            {

                            }

                            //User list to update on header including designation


                            if (GetHeaderNoCode == 0 || GetHeaderNoCode == 1)
                                GetHeaderNoCode = App.DetailHeaderCode;

                            if (App.srecsignview == null || App.srecsignview == "") { App.srecsignview = null; }
                            if (App.svetsignview == null || App.svetsignview == "") { App.svetsignview = null; }
                            if (App.sversignview == null || App.sversignview == "") { App.sversignview = null; }
                            if (App.ssoversignview == null || App.ssoversignview == "") { App.ssoversignview = null; }
                            if (App.ssoprosignview == null || App.ssoprosignview == "") { App.ssoprosignview = null; }
                            if (App.ssoagreesignview == null || App.ssoagreesignview == "") { App.ssoagreesignview = null; }

                            //if (Vaildate())
                            //{
                            await UpdateSignature("Submit");
                            await CurrentPage.Navigation.PopAsync();
                        }

                        catch (Exception ex)
                        {
                            throw ex;
                        }


                        //await UpdateFormDHeaderList();

                    }

                    // await CurrentPage.Navigation.PopAsync();
                });
            }


        }


        public ICommand AddLabourCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {

                    //if (GetHeaderNoCode == 0)
                    //{
                    //    UserDialogs.Instance.Alert("Please Fill Header Details.","RAMS");
                    //    return;
                    //}

                    _editViewModel.RoadName = "Add";
                    _editViewModel.Type = "Add";


                    _editViewModel.HdrFahPkRefNo = GetHeaderNoCode;

                    await CoreMethods.PushPageModel<FormDAddLabourPageModel>(_editViewModel);


                });
            }

        }


        public ICommand AddEquipCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {

                    //if (GetHeaderNoCode == 0)
                    //{
                    //    UserDialogs.Instance.Alert("Please Fill Header Details.","RAAMS");
                    //    return;
                    //}

                    _editViewModel.RoadName = "Add";
                    _editViewModel.Type = "Add";

                    _editViewModel.HdrFahPkRefNo = GetHeaderNoCode;

                    await CoreMethods.PushPageModel<FormDAddEquipmentPageModel>(_editViewModel);


                });
            }

        }


        public ICommand AddMaterialCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {

                    //if (GetHeaderNoCode == 0)
                    //{
                    //    UserDialogs.Instance.Alert("Please Fill Header Details.","RAAMS");
                    //    return;
                    //}

                    _editViewModel.RoadName = "Add";
                    _editViewModel.Type = "Add";

                    _editViewModel.HdrFahPkRefNo = GetHeaderNoCode;

                    await CoreMethods.PushPageModel<FormDAddMaterialPageModel>(_editViewModel);


                });
            }

        }


        public ICommand FormDFindDetailCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    if (GetHeaderNoCode == 0)
                    {

                        if (Vaildate())
                        {
                            var response = SaveFormDHeaderList();

                            MyLabourBaseFormDList = await GetMyFormDLabourListReports(Convert.ToString(GetHeaderNoCode));

                            MyMaterialBaseFormDList = await GetMyFormDMaterialListReports(Convert.ToString(GetHeaderNoCode));

                            MyEquimentBaseFormDList = await GetMyFormDEquipmentListReports(Convert.ToString(GetHeaderNoCode));

                            MyDetailBaseFormDList = await GetMyFormDDetailListReports(Convert.ToString(GetHeaderNoCode));

                            btnLabourCommand.IsVisible = true;
                            btnEquipment.IsVisible = true;
                            btnMaterial.IsVisible = true;
                            //btnDetail.IsVisible = true;
                            btnDetailX.IsVisible = true;

                            btnLabourCommandX.IsVisible = true;
                            btnEquipmentX.IsVisible = true;
                            btnMaterialX.IsVisible = true;

                            btnSave.IsVisible = true;
                            btnSubmit.IsVisible = true;
                            btnSave.IsEnabled = true;
                        }



                    }
                });
            }


        }


        public ICommand AddDetailCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {

                    //if (GetHeaderNoCode == 0)
                    //{
                    //    UserDialogs.Instance.Alert("Please Fill Header Details.","RAAMS");
                    //    return;
                    //}

                    _editViewModel.RoadName = "Add";

                    _editViewModel.HdrFahPkRefNo = GetHeaderNoCode;

                    //App.HeaderCode = GetHeaderNoCode;

                    await GetDetailSerialNo(GetHeaderNoCode);

                    _editViewModel.Rmu = StrRefcode + "-" + iRet;

                    _editViewModel.dtlserialNo = iRet;
                    _editViewModel.Section = SelectedRoadCode;

                    await CoreMethods.PushPageModel<FormDAddDetailsPageModel>(_editViewModel);


                });
            }

        }


        public ICommand SaveandUpdateCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {


                    try
                    {
                        try
                        {
                            try
                            {

                                recsignview = CurrentPage.FindByName<SignaturePadView>("recsignview");

                                Stream image = await recsignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);


                                if (!recsignview.IsBlank)
                                {

                                    using (BinaryReader binaryReader = new BinaryReader(image))
                                    {
                                        binaryReader.BaseStream.Position = 0;

                                        byte[] Signature = binaryReader.ReadBytes((int)image.Length);
                                        //await PCLHelper.SaveImage(Signature, "Inspsign", myCoolFolder);
                                    }

                                    var signatureMemoryStream = image as System.IO.MemoryStream;

                                    var byteArray = signatureMemoryStream.ToArray();

                                    string base64String = Convert.ToBase64String(byteArray);

                                    App.srecsignview = base64String;
                                }
                            }
                            catch { }


                            vetsignview = CurrentPage.FindByName<SignaturePadView>("vetsignview");

                            Stream vimage = await vetsignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);



                            if (!vetsignview.IsBlank)
                            {


                                using (BinaryReader binaryReader = new BinaryReader(vimage))
                                {
                                    binaryReader.BaseStream.Position = 0;

                                    byte[] Signature = binaryReader.ReadBytes((int)vimage.Length);
                                    //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                                }

                                var vsignatureMemoryStream = vimage as System.IO.MemoryStream;

                                var byteArray = vsignatureMemoryStream.ToArray();

                                string base64String = Convert.ToBase64String(byteArray);

                                //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                                App.svetsignview = base64String;

                            }


                            versignview = CurrentPage.FindByName<SignaturePadView>("versignview");

                            Stream verimage = await versignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);



                            if (!versignview.IsBlank)
                            {


                                using (BinaryReader binaryReader = new BinaryReader(verimage))
                                {
                                    binaryReader.BaseStream.Position = 0;

                                    byte[] Signature = binaryReader.ReadBytes((int)verimage.Length);
                                    //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                                }

                                var vsignatureMemoryStream = verimage as System.IO.MemoryStream;

                                var byteArray = vsignatureMemoryStream.ToArray();

                                string base64String = Convert.ToBase64String(byteArray);

                                //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                                App.sversignview = base64String;

                            }



                            soversignview = CurrentPage.FindByName<SignaturePadView>("soversignview");

                            Stream soverimage = await soversignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);



                            if (!soversignview.IsBlank)
                            {


                                using (BinaryReader binaryReader = new BinaryReader(soverimage))
                                {
                                    binaryReader.BaseStream.Position = 0;

                                    byte[] Signature = binaryReader.ReadBytes((int)soverimage.Length);
                                    //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                                }

                                var vsignatureMemoryStream = soverimage as System.IO.MemoryStream;

                                var byteArray = vsignatureMemoryStream.ToArray();

                                string base64String = Convert.ToBase64String(byteArray);

                                //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                                App.ssoversignview = base64String;

                            }


                            soprosignview = CurrentPage.FindByName<SignaturePadView>("soprosignview");

                            Stream soproimage = await soprosignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);



                            if (!soprosignview.IsBlank)
                            {


                                using (BinaryReader binaryReader = new BinaryReader(soproimage))
                                {
                                    binaryReader.BaseStream.Position = 0;

                                    byte[] Signature = binaryReader.ReadBytes((int)soproimage.Length);
                                    //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                                }

                                var vsignatureMemoryStream = soproimage as System.IO.MemoryStream;

                                var byteArray = vsignatureMemoryStream.ToArray();

                                string base64String = Convert.ToBase64String(byteArray);

                                //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                                App.ssoprosignview = base64String;

                            }


                            soagreesignview = CurrentPage.FindByName<SignaturePadView>("soagreesignview");

                            Stream soagreeimage = await soagreesignview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);



                            if (!soagreesignview.IsBlank)
                            {


                                using (BinaryReader binaryReader = new BinaryReader(soagreeimage))
                                {
                                    binaryReader.BaseStream.Position = 0;

                                    byte[] Signature = binaryReader.ReadBytes((int)soagreeimage.Length);
                                    //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                                }

                                var vsignatureMemoryStream = soagreeimage as System.IO.MemoryStream;

                                var byteArray = vsignatureMemoryStream.ToArray();

                                string base64String = Convert.ToBase64String(byteArray);

                                //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                                App.ssoagreesignview = base64String;

                            }

                        }
                        catch
                        {

                        }

                        //User list to update on header including designation


                        if (GetHeaderNoCode == 0 || GetHeaderNoCode == 1)
                            GetHeaderNoCode = App.DetailHeaderCode;

                        if (App.srecsignview == null || App.srecsignview == "") { App.srecsignview = null; }
                        if (App.svetsignview == null || App.svetsignview == "") { App.svetsignview = null; }
                        if (App.sversignview == null || App.sversignview == "") { App.sversignview = null; }
                        if (App.ssoversignview == null || App.ssoversignview == "") { App.ssoversignview = null; }
                        if (App.ssoprosignview == null || App.ssoprosignview == "") { App.ssoprosignview = null; }
                        if (App.ssoagreesignview == null || App.ssoagreesignview == "") { App.ssoagreesignview = null; }

                        //await CurrentPage.Navigation.PopAsync();


                    }

                    catch (Exception ex)
                    { }



                    //if(_editViewModel.Type=="Edit")
                    //await UpdateFormDHeaderList();


                    //if (_editViewModel.Type == "Add")
                    //{
                    //    if (Vaildate())
                    //    {
                    //        await UpdateSignature("Save");
                    //    }
                    //}
                    //else
                    await UpdateSignature("Save");

                    await CurrentPage.Navigation.PopAsync();



                });
            }

        }

        public ICommand FormDCancelCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    //var response = SaveFormDHeaderList();
                    if (App.ViewState == false)
                    {
                        var actionResult1 = await UserDialogs.Instance.ConfirmAsync("Unsaved changes might be lost. Are you sure you want to cancel?", "RAMS", "Yes", "No");
                        if (actionResult1)
                        {
                            await CurrentPage.Navigation.PopAsync();
                        }
                    }
                    else
                    {
                        await CurrentPage.Navigation.PopAsync();
                    }
                    //Cancel button


                });
            }


        }

        private int GetMonthNumber(int? weekNumber, int dayOfWeek)
        {
            DateTime yearstart = new DateTime(2021, 1, 1);
            int daysOffset = DayOfWeek.Tuesday - yearstart.DayOfWeek;

            DateTime firstMonday = yearstart.AddDays(daysOffset);

            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(yearstart, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekNumber;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }

            var result = firstMonday.AddDays(weekNum.Value * 7 + dayOfWeek - 1);
            return result.Month;
        }

    }
}
