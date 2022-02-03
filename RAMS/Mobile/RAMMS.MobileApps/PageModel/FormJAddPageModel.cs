using System;
using System.Windows.Input;
using Acr.UserDialogs;
using FreshMvvm;
using Xamarin.Forms;
using RAMMS.MobileApps;
using System.Collections.ObjectModel;
using RAMMS.MobileApps.Interface;
using System.Threading;
using RAMMS.MobileApps;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using RestSharp;
using System.IO;
using Xamarin.Essentials;
using RAMMS.MobileApps.Controls;
using System.Threading.Tasks;
using RAMMS.DTO.ResponseBO;
using Plugin.Connectivity;
using RAMMS.DTO.RequestBO;

namespace RAMMS.MobileApps.PageModel
{
    public class FormJAddPageModel : FreshBasePageModel, IFormJAdd, IFromJAssetDrop, IFromJSelectedRoadCode, IFormDelete, IFormJOne
    {
        public ObservableCollection<JAssetDropDownData> DDRodeCodeListItems { get; set; }



        public ObservableCollection<JAssetDropDownData> DDAssetTypeListItems { get; set; }

        public ObservableCollection<JAssetDropDownData> MonthListItems { get; set; }
        public ObservableCollection<JAssetDropDownData> YEarListITems { get; set; }

        public ObservableCollection<JAssetDropDownData> usernameItem { get; set; }
        public ObservableCollection<JAssetDropDownData> checkedITem { get; set; }
        public ObservableCollection<JAssetDropDownData> AuditedITem { get; set; }


        public JAssetDropDownData SeelectedRoadcode, SelectedAsset, SelectedMonth, SelectedYear, SelectedInspected, SelectedChecked, SelectedAudited;

        IUserDialogs _userDialogs;
        IRestApi _restApi;
        ExtendedPicker rmucodepicker, assetpicker, monthpicker, yearpicker, usernamepicker, chkpicker, AudPciker;
        FormJAddRefrenceNumber currentformJAddRefrenceNumber;
        Button AddButton, FindButton;
        SelectedRoadCodeResponseData CurrentselectedRoadCodeResponse;
        FormJFindDetailResponse _FormJFindDetailResponse;
        string _RoadName;
        bool isAuditedselected, isCheckedisselected, isInspected;
        SignaturePad.Forms.SignaturePadView SignChk, SignIns, SignAudi;
        string SelectedfomjDefectCode;
        public int startPage { get; set; } = 0;
        public string totalsize { get; set; } = "0";
        public string pagesize { get; set; } = "0";
        private CustomMyPicker cuspicker;
        public DateTime? DtDateInsp { get; set; } = null;
        public DateTime? DtDateVer { get; set; } = null;
        public DateTime? DtDateVet { get; set; } = null;
        public string RoadName
        {
            get
            {
                return _RoadName;
            }
            set
            {
                if (_RoadName != value)
                {
                    _RoadName = value;
                }

            }
        }
        string _Rmu;
        public string Rmu
        {
            get
            {
                return _Rmu;
            }
            set
            {
                if (_Rmu != value)
                {
                    _Rmu = value;
                }

            }
        }
        string _Sections;
        public string Sections
        {
            get
            {
                return _Sections;
            }
            set
            {
                if (_Sections != value)
                {
                    _Sections = value;
                }

            }
        }
        string _Refrencestring;
        public string Refrencestring
        {
            get
            {
                return _Refrencestring;
            }
            set
            {
                if (_Refrencestring != value)
                {
                    _Refrencestring = value;
                }

            }
        }

        string _InsName;
        public string InsName
        {
            get
            {
                return _InsName;
            }
            set
            {
                if (_InsName != value)
                {
                    _InsName = value;
                }
            }
        }
        bool _GridEnbaled;
        public bool GridEnbaled
        {
            get
            {
                return _GridEnbaled;
            }
            set
            {
                if (_GridEnbaled != value)
                {
                    _GridEnbaled = value;
                }
            }
        }
        string _InsDesgination;
        public string InsDesgination
        {
            get
            {
                return _InsDesgination;
            }
            set
            {
                if (_InsDesgination != value)
                {
                    _InsDesgination = value;
                }
            }
        }
        string _ChkName;
        public string ChkName
        {
            get
            {
                return _ChkName;
            }
            set
            {
                if (_ChkName != value)
                {
                    _ChkName = value;
                }
            }
        }
        string _ChkNameDesignation;
        public string ChkNameDesignation
        {
            get
            {
                return _ChkNameDesignation;
            }
            set
            {
                if (_ChkNameDesignation != value)
                {
                    _ChkNameDesignation = value;
                }
            }
        }

        string _AudName;
        public string AudName
        {
            get
            {
                return _AudName;
            }
            set
            {
                if (_AudName != value)
                {
                    _AudName = value;
                }
            }
        }
        string _AudDesgination;
        public string AudDesgination
        {
            get
            {
                return _AudDesgination;
            }
            set
            {
                if (_AudDesgination != value)
                {
                    _AudDesgination = value;
                }
            }
        }

        bool _CancelEnabled;
        public bool CancelEnabled
        {
            get
            {
                return _CancelEnabled;
            }
            set
            {
                if (_CancelEnabled != value)
                {
                    _CancelEnabled = value;
                }
            }
        }
        bool _SaveEnabled;
        public bool SaveEnabled
        {
            get
            {
                return _SaveEnabled;
            }
            set
            {
                if (_SaveEnabled != value)
                {
                    _SaveEnabled = value;
                }
            }
        }
        bool _SubmitEnabled;
        public bool SubmitEnabled
        {
            get
            {
                return _SubmitEnabled;
            }
            set
            {
                if (_SubmitEnabled != value)
                {
                    _SubmitEnabled = value;
                }
            }
        }

        FormJGridListResponseData _FormJAddGridRowSelectedRecords;
        public FormJGridListResponseData FormJAddGridRowSelectedRecords
        {
            get
            {
                return _FormJAddGridRowSelectedRecords;
            }
            set
            {
                if (_FormJAddGridRowSelectedRecords != value)
                {
                    _FormJAddGridRowSelectedRecords = value;
                }
            }

        }
        ObservableCollection<PageResult> _MyBaseFormJList;
        public ObservableCollection<PageResult> MyBaseFormJList
        {

            get
            {
                return _MyBaseFormJList;
            }
            set
            {
                if (_MyBaseFormJList != value)
                {
                    _MyBaseFormJList = value;
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
                        if (iValueRet > 1)
                            iValueRet = iValueRet - 1;
                        GetListBasedOnPageSelection(PageNumber++);

                    }
                    else if (_PageSelected.numberdata.Equals("Next"))
                    {
                        //if (PageNumber > 1)
                        //{
                        //    GetListBasedOnPageSelection(PageNumber++);
                        //}

                        if (iValueRet <= (Convert.ToInt32(TotalPageCount) / RecordPerPage))

                            iValueRet = iValueRet + 1;
                        GetListBasedOnPageSelection(PageNumber++);
                    }
                    

                }
            }
        }
        int iValueRet = 1;
        int TotalPageCount;
        int RecordPerPage = 10;
        int PageNumber = 1;
        bool FromEdit;
        public bool IsHeaderEnable { get; set; } = true;
        FormJSearchGridDTO formJSearchGridDTOglobal;
        DatePicker ChkDate, InsDate, AudDate;
        string StrInsDate, StrChkDate, StrAudDate;
        PageResult SelectedPageResult;
        bool FromView;
        ExtendedEntry EntryAudName, EntryAudDesgination, EntryChkName, EntryChkNameDesignation, EntryInsName, EntryInsDesgination;

        private Xamarin.Forms.ImageSource inspimage { get; set; }
        private Xamarin.Forms.ImageSource verimage { get; set; }
        private Xamarin.Forms.ImageSource vetimage { get; set; }


        public FormJAddPageModel(IUserDialogs userDialogs, IRestApi restApi)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;

        }
        public override void Init(object initData)
        {
            base.Init(initData);

            GridEnbaled = true;
            EntryAudName = CurrentPage.FindByName<ExtendedEntry>("EntryAudName");
            EntryAudDesgination = CurrentPage.FindByName<ExtendedEntry>("EntryAudDesgination");

            EntryChkName = CurrentPage.FindByName<ExtendedEntry>("EntryChkName");
            EntryChkNameDesignation = CurrentPage.FindByName<ExtendedEntry>("EntryChkNameDesignation");
           
            EntryInsName = CurrentPage.FindByName<ExtendedEntry>("EntryInsName");
            EntryInsDesgination = CurrentPage.FindByName<ExtendedEntry>("EntryInsDesgination");

            EntryAudName.IsEnabled = false;
            EntryAudDesgination.IsEnabled = false;

            EntryChkName.IsEnabled = false;
            EntryChkNameDesignation.IsEnabled = false;

            EntryInsName.IsEnabled = false;
            EntryInsDesgination.IsEnabled = false;

            rmucodepicker = CurrentPage.FindByName<ExtendedPicker>("RMUCodePicker");
            assetpicker = CurrentPage.FindByName<ExtendedPicker>("assetPicker");
            monthpicker = CurrentPage.FindByName<ExtendedPicker>("monthpicker");
            yearpicker = CurrentPage.FindByName<ExtendedPicker>("yearPicker");
            AddButton = CurrentPage.FindByName<Button>("AddButton");
            FindButton = CurrentPage.FindByName<Button>("FindButton");
            usernamepicker = CurrentPage.FindByName<ExtendedPicker>("usernamepicker");
            chkpicker = CurrentPage.FindByName<ExtendedPicker>("chkpicker");
            AudPciker = CurrentPage.FindByName<ExtendedPicker>("AudPciker");
            SignAudi = CurrentPage.FindByName<SignaturePad.Forms.SignaturePadView>("SignAudi");
            SignIns = CurrentPage.FindByName<SignaturePad.Forms.SignaturePadView>("SignIns");
            SignChk = CurrentPage.FindByName<SignaturePad.Forms.SignaturePadView>("SignChk");
            ChkDate = CurrentPage.FindByName<DatePicker>("ChkDate");
            AudDate = CurrentPage.FindByName<DatePicker>("AudDate");
            InsDate = CurrentPage.FindByName<DatePicker>("InsDate");
            cuspicker = CurrentPage.FindByName<CustomMyPicker>("scustompicker");

            SaveEnabled = false;
            //  AddButton.IsVisible = false;
            SubmitEnabled = false;
            // ThreadPool.QueueUserWorkItem(o => new FormAssetIR(this).LandingAssetDropDownSucess(CreateAssetParameter(DropdownEnum.Asset)));
            if (initData != null)
            {
                var obj = initData as Dictionary<string, object>;
                if (obj.ContainsKey("view"))
                {
                    FromView = true;
                    rmucodepicker.IsEnabled = false;
                    assetpicker.IsEnabled = false;
                    monthpicker.IsEnabled = false;
                    yearpicker.IsEnabled = false;
                    AddButton.IsEnabled = false;
                    FindButton.IsVisible = false;
                    usernamepicker.IsEnabled = false;
                    chkpicker.IsEnabled = false;
                    AudPciker.IsEnabled = false;
                    SignAudi.IsEnabled = false;
                    SignIns.IsEnabled = false;
                    SignChk.IsEnabled = false;
                    ChkDate.IsEnabled = false;
                    AudDate.IsEnabled = false;
                    InsDate.IsEnabled = false;
                    
                    CancelEnabled = true;
                    //  usernamepicker, chkpicker, AudPciker;
                    SaveEnabled = false;
                    AddButton.IsVisible = false;
                    SubmitEnabled = false;
                    IsHeaderEnable = false;
                }
                else
                {
                    CancelEnabled = true;
                    //  usernamepicker, chkpicker, AudPciker;
                    SaveEnabled = true;

                    SubmitEnabled = true;
                    FindButton.IsVisible = false;
                    AddButton.IsVisible = true;
                    IsHeaderEnable = false;
                }
                var data = (FormJSearchGridDTO)obj["data"];
                formJSearchGridDTOglobal = (FormJSearchGridDTO)obj["data"];

                inspimage = Xamarin.Forms.ImageSource.FromStream(
                        () => new MemoryStream(Convert.FromBase64String(formJSearchGridDTOglobal.SignPrp.ToString())));

                verimage = Xamarin.Forms.ImageSource.FromStream(
                        () => new MemoryStream(Convert.FromBase64String(formJSearchGridDTOglobal.SignVer.ToString())));

                vetimage = Xamarin.Forms.ImageSource.FromStream(
                        () => new MemoryStream(Convert.FromBase64String(formJSearchGridDTOglobal.SignVet.ToString())));

                SignIns.BackgroundImage = inspimage;
                SignChk.BackgroundImage = verimage;
                SignAudi.BackgroundImage = vetimage;

                DtDateInsp = formJSearchGridDTOglobal.DtPrp.HasValue ? formJSearchGridDTOglobal.DtPrp : (DateTime?)null;
                DtDateVer = formJSearchGridDTOglobal.VerifiedDt.HasValue ? formJSearchGridDTOglobal.VerifiedDt : (DateTime?)null;
                DtDateVet = formJSearchGridDTOglobal.VerifiedVDt.HasValue ? formJSearchGridDTOglobal.VerifiedVDt : (DateTime?)null;

                rmucodepicker.IsEnabled = false;
                assetpicker.IsEnabled = false;
                monthpicker.IsEnabled = false;
                yearpicker.IsEnabled = false;

                FromEdit = true;
                Refrencestring = data.Id;
                RoadName = data.RoadName;
                Rmu = data.Rmu;
                Sections = data.section;


            }
            else
            {
                CancelEnabled = true;
                //  usernamepicker, chkpicker, AudPciker;
                // SaveEnabled = true;

                // SubmitEnabled = true;
            }
            AddButton.Clicked += AddButton_Clicked;
            FindButton.Clicked += FindButton_Clicked;

            rmucodepicker.SelectedIndexChanged += Rmucodepicker_SelectedIndexChanged;
            assetpicker.SelectedIndexChanged += Assetpicker_SelectedIndexChanged;
            monthpicker.SelectedIndexChanged += Monthpicker_SelectedIndexChanged;
            yearpicker.SelectedIndexChanged += Yearpicker_SelectedIndexChanged;
            AudPciker.SelectedIndexChanged += AudPciker_SelectedIndexChanged;
            chkpicker.SelectedIndexChanged += Chkpicker_SelectedIndexChanged;
            usernamepicker.SelectedIndexChanged += Usernamepicker_SelectedIndexChanged;
            AudDate.DateSelected += AudDate_DateSelected;
            ChkDate.DateSelected += ChkDate_DateSelected;
            InsDate.DateSelected += InsDate_DateSelected;
            StrAudDate = AudDate.Date.ToString("yyyy-MM-dd");
            StrChkDate = ChkDate.Date.ToString("yyyy-MM-dd");
            StrInsDate = InsDate.Date.ToString("yyyy-MM-dd");

            cuspicker.SelectedIndexChanged += async (s, e) =>
            {
                try
                {
                    if (cuspicker.SelectedIndex != -1)
                    {
                        iValueRet = 1;
                        if (cuspicker.SelectedItem.ToString() == "10 rows")
                        {
                            RecordPerPage = 10;
                            GetAllGridData();
                            return;

                        }

                        else if (cuspicker.SelectedItem.ToString() == "25 rows")
                        {
                            RecordPerPage = 25;
                            GetAllGridData();
                            return;

                        }

                        else if (cuspicker.SelectedItem.ToString() == "50 rows")
                        {
                            RecordPerPage = 50;
                            GetAllGridData();
                            return;
                        }

                        else if (cuspicker.SelectedItem.ToString() == "100 rows")
                        {
                            RecordPerPage = 100;
                            GetAllGridData();
                            return;
                        }
                        else if (cuspicker.SelectedItem.ToString() == "500 rows")
                        {
                            RecordPerPage = 500;
                            GetAllGridData();
                            return;
                        }
                        else
                        {
                            RecordPerPage = 1000;
                            GetAllGridData();
                            return;
                        }
                    }
                }
                catch { }

            };
        }
        void UpdateData()
        {

        }
        public override void ReverseInit(object returnedData)
        {
            base.ReverseInit(returnedData);
            if (AppConst.IsUpdateTrue)
            {
                //ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));
                GetAllData(_FormJFindDetailResponse.data.No);
            }
        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            if (AppConst.IsUpdateTrue)
            {
                if (_FormJFindDetailResponse != null)
                {
                    //ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));
                    GetAllData(_FormJFindDetailResponse.data.No);
                    //AppConst.IsUpdateTrue = false;
                }
                AppConst.IsUpdateTrue = false;
            }
            else
            {
                if (AuditedITem == null)
                {
                    //ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));
                    if (formJSearchGridDTOglobal != null)
                    {
                        RestRequest restRequest1 = new RestRequest();
                        restRequest1.AddQueryParameter("formJHdr", formJSearchGridDTOglobal.No.ToString());
                        ////ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));
                        ThreadPool.QueueUserWorkItem(o => new FormJAdd(this).FormJAddHeaderid(restRequest1));
                    }
                   
                    ThreadPool.QueueUserWorkItem(o => new FormJAdd(this).FormJAddRoadCodeDropDownSucess(""));
                    ThreadPool.QueueUserWorkItem(o => new FormJAdd(this).FormJUsertListResponse());
                }

            }

            if (formJSearchGridDTOglobal != null)
            {
                var filtet = new ADDFilters();
                filtet.StartPageNo = (iValueRet - 1) * RecordPerPage > 0 ? (iValueRet - 1) * RecordPerPage : 0;
                filtet.RecordsPerPage = RecordPerPage;
                filtet.sortOrder = 0;
                filtet.Filters = new FormJAddFilters() { HeaderNo = formJSearchGridDTOglobal.No.ToString() };
                string json = JsonConvert.SerializeObject(filtet);
                Device.BeginInvokeOnMainThread(() =>
                {
                    // _userDialogs.ShowLoading("Loading");
                    ThreadPool.QueueUserWorkItem(o => new FormJSelectedRoadCode(this).FormJGridDetailREsponse(json));
                });
            }

        }
        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);

            _userDialogs.HideLoading();

        }

        private void InsDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            StrInsDate = ((DatePicker)sender).Date.ToString("yyyy-MM-dd");
        }

        private void ChkDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            StrChkDate = ((DatePicker)sender).Date.ToString("yyyy-MM-dd");
        }

        private void AudDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            StrAudDate = ((DatePicker)sender).Date.ToString("yyyy-MM-dd");
        }

        private void Usernamepicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            isInspected = true;
            isCheckedisselected = false;
            isAuditedselected = false;
            // var objs = (JAssetDropDownData)AuditedITem[AudPciker.SelectedIndex];

            if(usernamepicker.SelectedIndex ==-1)
            {

            }
            else
            {
                //  v = objs.Text;
                var objs = usernameItem[usernamepicker.SelectedIndex];
                SelectedInspected = objs;
                InsName = objs.Text;
                if (!objs.Text.Equals("99999999-others"))
                {
                    EntryInsName.IsEnabled = false;
                    EntryInsDesgination.IsEnabled = false;
                    //ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));
                    //var dict = new Dictionary<string, object>();
                    //dict.Add("UserId", objs.Value);
                    //ThreadPool.QueueUserWorkItem(o => new FormJAdd(this).FormJUserDetailResponse(JsonConvert.SerializeObject(dict)));

                    
                }
                else
                {
                    EntryInsName.Text = "";
                    EntryInsDesgination.Text = "";
                    EntryInsName.IsEnabled = true;
                    EntryInsDesgination.IsEnabled = true;
                }

                int iCode = Convert.ToInt32(objs.Value.ToString());
                var objUser = GetUserDetilsList("inspuser", iCode);

            }
           

            //AudName = objs.Text;
        }

        private void Chkpicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            isAuditedselected = false;
            isInspected = false;
            isCheckedisselected = true;

            if (chkpicker.SelectedIndex == -1)
            {

            }
            else
            {
                var objs = (JAssetDropDownData)checkedITem[chkpicker.SelectedIndex];
                SelectedChecked = objs;
                ChkName = objs.Text;
                if (!objs.Text.Equals("99999999-others"))
                {
                    EntryChkName.IsEnabled = false;
                    EntryChkNameDesignation.IsEnabled = false;
                   
                }
                else
                {
                    EntryChkName.Text = "";
                    EntryChkNameDesignation.Text = "";
                    EntryChkName.IsEnabled = true;
                    EntryChkNameDesignation.IsEnabled = true;
                   
                }

                int iCode = Convert.ToInt32(objs.Value.ToString());
                var objUser = GetUserDetilsList("veruser", iCode);

                //AudName = objs.Text;
                //   var colorName = locationpicker.Items[locationpicker.SelectedIndex];

            }
        }

        private void AudPciker_SelectedIndexChanged(object sender, EventArgs e)
        {
            isAuditedselected = true;
            isInspected = false;
            isCheckedisselected = false;
            if (AudPciker.SelectedIndex == -1)
            {

            }
            else
            {

                var objs = AuditedITem[AudPciker.SelectedIndex];
                SelectedAudited = objs;
                AudName = objs.Text;
                if (!objs.Text.Equals("99999999-others"))
                {
                    EntryAudName.IsEnabled = false;
                    EntryAudDesgination.IsEnabled = false;
                }
                else
                {
                    EntryAudName.Text = "";
                    EntryAudDesgination.Text = "";
                    EntryAudName.IsEnabled = true;
                    EntryAudDesgination.IsEnabled = true;
                }

                int iCode = Convert.ToInt32(objs.Value.ToString());
                var objUser = GetUserDetilsList("audituser", iCode);

                //   var colorName = locationpicker.Items[locationpicker.SelectedIndex];

            }

        }
        public UserResponseDTO SelectedUserItem { get; set; }


        public async Task<int> GetUserDetilsList(string usertype, int iUser)
        {
            _userDialogs.ShowLoading("Loading");

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

                            if (usertype == "inspuser")
                            {
                                SelectedUserItem = response.data;
                                if (formJSearchGridDTOglobal != null && formJSearchGridDTOglobal.UsernamePrp != null && formJSearchGridDTOglobal.UsernamePrp == SelectedUserItem.UserName || SelectedUserItem.UserName.ToLower() == "others")
                                {
                                    InsName = formJSearchGridDTOglobal.UsernamePrp;
                                    InsDesgination = formJSearchGridDTOglobal.DesignationPrp;
                                }
                                else
                                {
                                    InsName = response.data.UserName;
                                    InsDesgination = response.data.Position;
                                }

                            }
                            else if (usertype == "veruser")
                            {
                                SelectedUserItem = response.data;
                                if (formJSearchGridDTOglobal != null && formJSearchGridDTOglobal.UsernamePrp != null && formJSearchGridDTOglobal.UsernamePrp == SelectedUserItem.UserName || SelectedUserItem.UserName.ToLower() == "others")
                                {
                                    ChkName = formJSearchGridDTOglobal.UsernameVer;
                                    ChkNameDesignation = formJSearchGridDTOglobal.DesignationVer;
                                }
                                else
                                {
                                    ChkName = response.data.UserName;
                                    ChkNameDesignation = response.data.Position;
                                }
                            }
                            else if(usertype == "audituser")
                            {
                                SelectedUserItem = response.data;
                                if (formJSearchGridDTOglobal != null && formJSearchGridDTOglobal.UsernamePrp != null && formJSearchGridDTOglobal.UsernamePrp == SelectedUserItem.UserName || SelectedUserItem.UserName.ToLower() == "others")
                                {
                                    AudName = formJSearchGridDTOglobal.UsernameVet;
                                    AudDesgination = formJSearchGridDTOglobal.DesignationVet;
                                }
                                else
                                {
                                    AudName = response.data.UserName;
                                    AudDesgination = response.data.Position;
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            //_userDialogs.Alert(ex.Message);
                            _userDialogs.HideLoading();



                        }

                        return 1;
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return 1;

                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");

                return 1;
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }

            return 1;
        }


        bool Vaildation()
        {
            if (SeelectedRoadcode == null)
            {
                _userDialogs.Alert(AppConst.EnterRoadCode, "Alert");
                return false;
            }
            else if (SelectedAsset == null)
            {
                _userDialogs.Alert(AppConst.EnterAssetCode, "Alert");
                return false;
            }
            else if (SelectedMonth == null)
            {
                _userDialogs.Alert(AppConst.EntermonthCode, "Alert");
                return false;
            }
            else if (SelectedYear == null)
            {
                _userDialogs.Alert(AppConst.EnteryearCode, "Alert");
                return false;
            }
            return true;
        }
        private void FindButton_Clicked(object sender, EventArgs e)
        {
            if (Vaildation())
            {
                AddButton.IsVisible = true;
                FindButton.IsVisible = false;
                SaveEnabled = true;
                SubmitEnabled = true;
                CancelEnabled = true;
                IsHeaderEnable = false;

                if (currentformJAddRefrenceNumber != null)
                {
                    var param = new Dictionary<string, object>();
                    param.Add("No", "0");
                    param.Add("Id", currentformJAddRefrenceNumber.data);
                    param.Add("RoadCode", CurrentselectedRoadCodeResponse.RoadCode);
                    param.Add("Rmu", CurrentselectedRoadCodeResponse.RmuCode);
                    param.Add("RoadName", CurrentselectedRoadCodeResponse.RoadName);
                    param.Add("AssetGroupCode", SelectedAsset.Value);
                    param.Add("Month", SelectedMonth.Value);
                    param.Add("Year", SelectedYear.Value);
                    param.Add("section", Sections);
                    param.Add("ActiveYn", CurrentselectedRoadCodeResponse.ActiveYn);

                    var json = JsonConvert.SerializeObject(param);

                    ////ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));

                    ThreadPool.QueueUserWorkItem(o => new FormJSelectedRoadCode(this).FormJFindDetailREsponse(json));

                }
            }
        }
        void GetListBasedOnPageSelection(int selectedPagenumber)
        {

            // var param = JsonConvert.SerializeObject(formJSearchBase);
            //GetAllGridData(selectedPagenumber);
            ////ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));

            //ThreadPool.QueueUserWorkItem(o => new FormLandingGridIR(this).LandingGridDropDownSucess(param));
        }
        private void AddButton_Clicked(object sender, EventArgs e)
        {

        }

        private void Yearpicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (yearpicker.SelectedIndex == -1)
            {

            }
            else
            {
                //  var items = sender as ExtendedPicker;
                // var index = items.SelectedIndex == 0 ? 0 : items.SelectedIndex - 1;
                SelectedYear = YEarListITems[yearpicker.SelectedIndex];
                GetRefrenceNumber();
            }

        }

        private void Monthpicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (monthpicker.SelectedIndex == -1)
            {

            }
            else
            {
                SelectedMonth = MonthListItems[monthpicker.SelectedIndex];
                GetRefrenceNumber();
            }
            //if (!FromEdit)
            //{
            //    var items = sender as ExtendedPicker;
            //    var index = items.SelectedIndex == 0 ? 0 : items.SelectedIndex - 1;
            //    SelectedMonth = MonthListItems[index];
            //    GetRefrenceNumber();
            //}

        }

        private void Assetpicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!FromEdit)
            {
                var items = sender as ExtendedPicker;


                if (items.SelectedIndex == -1)
                {

                }
                else
                {
                    SelectedAsset = DDAssetTypeListItems[items.SelectedIndex];
                    RestRequest resetRequest1 = new RestRequest();
                    resetRequest1.AddQueryParameter("name", SelectedAsset.Value);
                    //ThreadPool.QueueUserWorkItem(o => new FormJOne(this).GetAssetNameJ(resetRequest1));
                    SelectedfomjDefectCode = SelectedAsset.Value;

                    try
                    {
                      Device.BeginInvokeOnMainThread(async () =>
                      {
                          var response = await _restApi.GetAssetCodeByNameFormJ(SelectedAsset.Value);

                          if (response.success)
                          {
                              App.AssetGroupSelection = response.data;
                          }
                      });
                    }
                    catch (Exception ex)
                    {

                    }


                }
                GetRefrenceNumber();
            }

        }

        private void Rmucodepicker_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (rmucodepicker.SelectedIndex == -1)
            {

            }
            else
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    _userDialogs.ShowLoading("Loading");
                    var param = new Dictionary<string, string>();
                    param.Add("RoadCode", DDRodeCodeListItems[rmucodepicker.SelectedIndex].Value);
                    var parms = JsonConvert.SerializeObject(param);
                    SeelectedRoadcode = DDRodeCodeListItems[rmucodepicker.SelectedIndex];
                    ThreadPool.QueueUserWorkItem(o => new FormJSelectedRoadCode(this).LandingFormJSelectedRoadCode(parms));

                    GetRefrenceNumber();
                });


            }
        }
        void GetRefrenceNumber()
        {
            if (!FromEdit)
            {
                if (SeelectedRoadcode != null && SelectedAsset != null && SelectedMonth != null && SelectedYear != null)
                {
                    //ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));
                    RestRequest restRequest = new RestRequest();
                    restRequest.AddQueryParameter("roadCode", SeelectedRoadcode.Value);
                    restRequest.AddQueryParameter("month", SelectedMonth.Value);
                    restRequest.AddQueryParameter("year", SelectedYear.Value);
                    restRequest.AddQueryParameter("assetGroup", SelectedfomjDefectCode ?? "");

                    ThreadPool.QueueUserWorkItem(o => new FormJSelectedRoadCode(this).FormJGetRefrenceNumber(restRequest));
                }
            }

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
        public ICommand AddAction
        {
            get
            {
                return new Command(async (obj) =>
                {
                    AppConst.IsUpdateTrue = false;
                    var objects = new Dictionary<string, object>();
                    objects.Add("asset", SelectedAsset);
                    if (!FromEdit)
                    {
                        objects.Add("refrencenumber", _FormJFindDetailResponse);
                    }
                    else
                    {
                        objects.Add("refrencenumberEdit", formJSearchGridDTOglobal);
                    }


                    await CoreMethods.PushPageModel<FormJOnePageModel>(objects);



                });
            }

        }
        async void EditSaveSignature(string type)
        {
            if (formJSearchGridDTOglobal != null)
            {
                var insimage = await SignIns.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                var chkimage = await SignChk.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                var audimage = await SignAudi.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                var param = new Dictionary<string, object>();
                param.Add("No", formJSearchGridDTOglobal.No);
                param.Add("SubmitSts", type == "submit" ? true : false);

                if (insimage != null)
                {
                    param.Add("SignPrp", ImageConvertBase64.ConvertToBase64(insimage));
                }
                else
                {
                    param.Add("SignPrp", null);
                }

                if (chkimage != null)
                {
                    param.Add("SignVer", ImageConvertBase64.ConvertToBase64(chkimage));
                }
                else
                {

                    param.Add("SignVer", null);
                }

                if (audimage != null)
                {
                    param.Add("SignVet", ImageConvertBase64.ConvertToBase64(audimage));
                }
                else
                {
                    param.Add("SignVet", null);
                }

                var UseridPrp = SelectedInspected != null ? SelectedInspected.Value : "";

                param.Add("UseridPrp", UseridPrp);
                param.Add("UsernamePrp", InsName ?? "");
                param.Add("DesignationPrp", InsDesgination ?? "");
                param.Add("DtPrp", DtDateInsp.HasValue ? DtDateInsp.Value : (DateTime?)null);

                var UseridVer = SelectedChecked != null ? SelectedChecked.Value : "";

                param.Add("UseridVer", UseridVer);
                param.Add("UsernameVer", ChkName ?? "");
                param.Add("DesignationVer", ChkNameDesignation ?? "");
                param.Add("VerifiedDt", DtDateVer.HasValue ? DtDateVer.Value : (DateTime?)null);
                var UseridVet = SelectedAudited != null ? SelectedAudited.Value : "";

                param.Add("UseridVet", UseridVet);
                param.Add("UsernameVet", AudName ?? "");
                param.Add("DesignationVet", AudDesgination ?? "");
                param.Add("AuditedDt", DtDateVet.HasValue ? DtDateVet.Value : (DateTime?)null);
                _userDialogs.ShowLoading("Loading...");
                ThreadPool.QueueUserWorkItem(o => new FormJAdd(this).SaveSignature(JsonConvert.SerializeObject(param)));

            }
            else
            {
                _userDialogs.Alert("Fill all details", "Alert");
            }
        }
        async void SaveSignature(string type)
        {
            if (_FormJFindDetailResponse != null)
            {
                var insimage = await SignIns.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                var chkimage = await SignChk.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                var audimage = await SignAudi.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                var param = new Dictionary<string, object>();
                param.Add("No", _FormJFindDetailResponse.data.No);
                param.Add("SubmitSts", type == "submit" ? true : false);

                if (insimage != null)
                {
                    param.Add("SignPrp", ImageConvertBase64.ConvertToBase64(insimage));
                }
                else
                {
                    param.Add("SignPrp", null);
                }

                if (chkimage != null)
                {
                    param.Add("SignVer", ImageConvertBase64.ConvertToBase64(chkimage));
                }
                else
                {

                    param.Add("SignVer", null);
                }

                if (audimage != null)
                {
                    param.Add("SignVet", ImageConvertBase64.ConvertToBase64(audimage));
                }
                else
                {
                    param.Add("SignVet", null);
                }

                var UseridPrp = SelectedInspected != null ? SelectedInspected.Value : "";

                param.Add("UseridPrp", UseridPrp);
                param.Add("UsernamePrp", InsName ?? "");
                param.Add("DesignationPrp", InsDesgination ?? "");
                param.Add("DtPrp", DtDateInsp.HasValue ? DtDateInsp.Value : (DateTime?)null);

                var UseridVer = SelectedChecked != null ? SelectedChecked.Value : "";

                param.Add("UseridVer", UseridVer);
                param.Add("UsernameVer", ChkName ?? "");
                param.Add("DesignationVer", ChkNameDesignation ?? "");
                param.Add("VerifiedDt", DtDateVer.HasValue ? DtDateVer.Value : (DateTime?)null);
                var UseridVet = SelectedAudited != null ? SelectedAudited.Value : "";

                param.Add("UseridVet", UseridVet);
                param.Add("UsernameVet", AudName ?? "");
                param.Add("DesignationVet", AudDesgination ?? "");
                param.Add("AuditedDt", DtDateVet.HasValue ? DtDateVet.Value : (DateTime?)null);
                _userDialogs.ShowLoading("Loading...");
                ThreadPool.QueueUserWorkItem(o => new FormJAdd(this).SaveSignature(JsonConvert.SerializeObject(param)));

            }
            else
            {
                _userDialogs.Alert("Fill all details", "Alert");
            }
        }
        public ICommand SaveAction
        {
            get
            {
                return new Command(async (obj) =>
                {
                    if (!FromEdit)
                    {
                        SaveSignature("save");
                    }
                    else
                    {
                        EditSaveSignature("save");
                    }
                   
                });
            }
        }
        public ICommand CancelAction
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    if (FromView)
                    {
                        await CoreMethods.PopPageModel();
                    }
                    else
                    {
                        var actionResult = await UserDialogs.Instance.ConfirmAsync(" Unsaved changes will be lost. Are you sure want to cancel?", "RAMS", "Yes", "No");
                        if (actionResult)
                            await CoreMethods.PopPageModel();
                    }
                });
            }
        }
        public ICommand SubmitAction
        {
            get
            {
                return new Command(async (obj) =>
                {
                    if (!FromEdit)
                    {
                        SaveSignature("submit");
                    }
                    else
                    {
                        EditSaveSignature("submit");
                    }

                });
            }
        }

        public void GetFormJAddRoadCodeResponse(JAssetDropDown responseBase)
        {
            //RunOnUiThread(() => {

            //});

            Device.BeginInvokeOnMainThread(() =>
            {
                DDRodeCodeListItems = new ObservableCollection<JAssetDropDownData>(responseBase.data.ToList());
                rmucodepicker.ItemsSource = DDRodeCodeListItems.Select((JAssetDropDownData arg) => arg.Text).ToList();

                // Code to run if this is the main thread
                if (FromEdit)
                {
                    var prity = (from s in DDRodeCodeListItems where s.Value.Equals(formJSearchGridDTOglobal.RoadCode) select s).ToList()[0];
                    var indexs = DDRodeCodeListItems.IndexOf(prity);
                    rmucodepicker.SelectedIndex = indexs;
                    SeelectedRoadcode = prity;
                }
                _userDialogs.HideLoading();
                _userDialogs.ShowLoading("Loading...");
                ThreadPool.QueueUserWorkItem(o => new FormAssetIR(this).LandingAssetDropDownSucess(CreateAssetParameter(DropdownEnum.Asset)));
            });

        }

        public void APIHitFailed(string error)
        {
            _userDialogs.HideLoading();
        }

        public void GetDropDownJDetailResponse(JAssetDropDown responseBase)
        {

            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    DDAssetTypeListItems = new ObservableCollection<JAssetDropDownData>(responseBase.data.ToList() as List<JAssetDropDownData>);
                    assetpicker.ItemsSource = DDAssetTypeListItems.Select((JAssetDropDownData arg) => arg.Text).ToList();

                    if (FromEdit)
                    {

                        var prity = (from s in DDAssetTypeListItems where s.Value.Equals(formJSearchGridDTOglobal.AssetGroupCode) select s).ToList()[0];

                        if (prity != null)
                        {
                            var indexs = DDAssetTypeListItems.IndexOf(prity);
                            assetpicker.SelectedIndex = indexs;
                            SelectedAsset = prity;
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                var response = await _restApi.GetAssetCodeByNameFormJ(SelectedAsset.Value);

                                if (response.success)
                                {
                                    App.AssetGroupSelection = response.data;
                                }
                            });
                        }
                    }
                }
                catch (Exception ex)
                {

                }

                _userDialogs.HideLoading();
                _userDialogs.ShowLoading("Loading...");
                ThreadPool.QueueUserWorkItem(o => new FormAssetIR(this).LandingMonthDropDownSucess(CreateAssetParameter(DropdownEnum.Month)));
            });



        }

        public void GetDropDownJMonthResponse(JAssetDropDown responseBase)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                MonthListItems = new ObservableCollection<JAssetDropDownData>(responseBase.data.ToList() as List<JAssetDropDownData>);
                monthpicker.ItemsSource = MonthListItems.Select((JAssetDropDownData arg) => arg.Text).ToList();

                if (FromEdit)
                {
                    var lengths = formJSearchGridDTOglobal.Month.ToString().Length;
                    var totalstring = "";
                    if (lengths == 1)
                    {
                        totalstring = "0" + formJSearchGridDTOglobal.Month.ToString();
                    }
                    else
                    {
                        totalstring = formJSearchGridDTOglobal.Month.ToString();

                    }
                    var prity = (from s in MonthListItems where s.Text.Equals(totalstring) select s).ToList()[0];
                    var indexs = MonthListItems.IndexOf(prity);
                    monthpicker.SelectedIndex = indexs;
                    SelectedMonth = prity;
                }
                _userDialogs.HideLoading();
                _userDialogs.ShowLoading("Loading...");
                ThreadPool.QueueUserWorkItem(o => new FormAssetIR(this).LandingYearDropDownSucess(CreateAssetParameter(DropdownEnum.Year)));
            });



        }

        public void GetDropDownJYearResponse(JAssetDropDown responseBase)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                YEarListITems = new ObservableCollection<JAssetDropDownData>(responseBase.data.ToList() as List<JAssetDropDownData>);
                yearpicker.ItemsSource = YEarListITems.Select((JAssetDropDownData arg) => arg.Text).ToList();
                if (FromEdit)
                {
                    var prity = (from s in YEarListITems where s.Value.Equals(formJSearchGridDTOglobal.Year.ToString()) select s).ToList()[0];
                    var indexs = YEarListITems.IndexOf(prity);
                    yearpicker.SelectedIndex = indexs;
                    SelectedYear = prity;
                }
                _userDialogs.HideLoading();
                //_userDialogs.ShowLoading("Loading...");
            });



        }

        public void GetFromJLandingGridResponse(SelectedRoadCodeResponse responseBase)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                CurrentselectedRoadCodeResponse = responseBase.data;
                Sections = responseBase.data.SecName;
                RoadName = responseBase.data.RoadName;
                Rmu = responseBase.data.RmuName;
                _userDialogs.HideLoading();
            });

        }

        public void GetRefrenceNumber(FormJAddRefrenceNumber formJAddRefrenceNumber)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                currentformJAddRefrenceNumber = formJAddRefrenceNumber;
                Refrencestring = currentformJAddRefrenceNumber.data;
                _userDialogs.HideLoading();
            });

        }

        public void GetFindDetails(FormJFindDetailResponse formJFindDetailResponse)
        {
            _FormJFindDetailResponse = formJFindDetailResponse;
            GetAllData(formJFindDetailResponse.data.No);

        }
        void GetAllData(int number)
        {
            //ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));
            var filtet = new ADDFilters();
            filtet.StartPageNo = (iValueRet - 1) * RecordPerPage > 0 ? (iValueRet - 1) * RecordPerPage : 0;
            filtet.RecordsPerPage = RecordPerPage;
            filtet.sortOrder = 0;
            filtet.Filters = new FormJAddFilters() { HeaderNo = number.ToString() };
            string json = JsonConvert.SerializeObject(filtet);
            ThreadPool.QueueUserWorkItem(o => new FormJSelectedRoadCode(this).FormJGridDetailREsponse(json));
        }
        void GetAllGridData()
        {
            var values = (iValueRet - 1) * RecordPerPage > 0 ? (iValueRet - 1) * RecordPerPage : 0;
            //ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));
            var filtet = new ADDFilters();
            filtet.StartPageNo = values;
            filtet.RecordsPerPage = RecordPerPage;
            filtet.sortOrder = 0;
            if(FromEdit == false && FromView == false)
            {
                filtet.Filters = new FormJAddFilters() { HeaderNo = _FormJFindDetailResponse.data.No.ToString() };
            }
            else
            {
                filtet.Filters = new FormJAddFilters() { HeaderNo = formJSearchGridDTOglobal.No.ToString() };
            }
         
            string json = JsonConvert.SerializeObject(filtet);
            ThreadPool.QueueUserWorkItem(o => new FormJSelectedRoadCode(this).FormJGridDetailREsponse(json));
        }
        public void GetFormjADDGridDEtail(FormJGridListResponse formJGridListResponse)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _userDialogs.HideLoading();
                MyBaseFormJList = new ObservableCollection<PageResult>(formJGridListResponse.data.PageResult.ToList() as List<PageResult>);

                TotalPageCount = formJGridListResponse.data.TotalRecords;

                //startPage = (iValueRet - 1) * RecordPerPage > 0 ? ((iValueRet - 1) * RecordPerPage) + 1 : 0;
                startPage = (formJGridListResponse.data.TotalRecords == 0) ? 0 : formJGridListResponse.data.PageNo + 1;

                totalsize = TotalPageCount.ToString();

                if ((iValueRet * RecordPerPage) > Convert.ToInt32(totalsize))
                    pagesize = totalsize;
                else
                    pagesize = (iValueRet * RecordPerPage).ToString();

                var num = (double)TotalPageCount / RecordPerPage;
                var count = new List<Number>();
                //if (num.ToString().Contains("."))
                //{
                //    roundvalue = (int)(num + 1);
                //}
                //else
                //{
                //    roundvalue = (int)num;
                //}
              

                //for (int i = 1; i <= roundvalue; i++)
                //{
                //    if (i == 1)
                //    {
                //        count.Add(new Number() { numberdata = i.ToString(), SelectedTextColor = Color.Blue });
                //    }
                //    else
                //    {
                //        count.Add(new Number() { numberdata = i.ToString() });
                //    }

                //}
                count.Add(new Number() { numberdata = "Prev" });
                count.Add(new Number() { numberdata = "Next" });
                PageCount = count;
            });

        }

        public void GetFormJHeaderCodeResponse(FormJByHeader responseBase)
        {
            // _userDialogs.HideLoading();

            var filtet = new ADDFilters();
            filtet.StartPageNo = (iValueRet - 1) * RecordPerPage > 0 ? (iValueRet - 1) * RecordPerPage : 0;
            filtet.RecordsPerPage = RecordPerPage;
            filtet.sortOrder = 0;
            filtet.Filters = new FormJAddFilters() { HeaderNo = formJSearchGridDTOglobal.No.ToString() };
            string json = JsonConvert.SerializeObject(filtet);
            Device.BeginInvokeOnMainThread(() =>
            {
                // _userDialogs.ShowLoading("Loading");
                ThreadPool.QueueUserWorkItem(o => new FormJSelectedRoadCode(this).FormJGridDetailREsponse(json));
            });



        }

        //int inspindex, checkindex, auditindex;
        public void GetFormJUserList(JAssetDropDown responseBase)
        {
            //  string json = JsonConvert.SerializeObject(filtet);
            //  ThreadPool.QueueUserWorkItem(o => new FormJSelectedRoadCode(this).FormJGridDetailREsponse(json));

            usernameItem = new ObservableCollection<JAssetDropDownData>(responseBase.data.ToList() as List<JAssetDropDownData>);
            usernamepicker.ItemsSource = usernameItem.Select((JAssetDropDownData arg) => arg.Text).ToList();

            AuditedITem = new ObservableCollection<JAssetDropDownData>(responseBase.data.ToList() as List<JAssetDropDownData>);
            AudPciker.ItemsSource = AuditedITem.Select((JAssetDropDownData arg) => arg.Text).ToList();

            checkedITem = new ObservableCollection<JAssetDropDownData>(responseBase.data.ToList() as List<JAssetDropDownData>);
            chkpicker.ItemsSource = checkedITem.Select((JAssetDropDownData arg) => arg.Text).ToList();

            Device.BeginInvokeOnMainThread(() =>
            {
                if (formJSearchGridDTOglobal != null)
                {
                    int inspindex = usernameItem.ToList().FindIndex(a => Convert.ToInt32(a.Value) == formJSearchGridDTOglobal.UseridPrp);
                    int checkindex = checkedITem.ToList().FindIndex(a => Convert.ToInt32(a.Value) == formJSearchGridDTOglobal.UseridVer);
                    int auditindex = AuditedITem.ToList().FindIndex(a => Convert.ToInt32(a.Value) == formJSearchGridDTOglobal.UseridVet);

                    usernamepicker.SelectedIndex = inspindex;
                    chkpicker.SelectedIndex = checkindex;
                    AudPciker.SelectedIndex = auditindex;
                }
            });
            
        }

        public void GetFormJUserDetail(UserDataResponse responseBase)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _userDialogs.HideLoading();
                if (isAuditedselected)
                {
                    AudName = responseBase.data.UserName;
                    AudDesgination = responseBase.data.Position;
                    isAuditedselected = false;
                }
                else if (isCheckedisselected)
                {
                    ChkName = responseBase.data.UserName;
                    ChkNameDesignation = responseBase.data.Position;
                    isCheckedisselected = false;
                }
                else if (isInspected)
                {
                    InsName = responseBase.data.UserName;
                    InsDesgination = responseBase.data.Position;
                    isAuditedselected = false;
                }
            });

        }

        public void GetSerialNo(GetSerialNo getSerialNo)
        {
            Device.BeginInvokeOnMainThread( async() =>
            {
                if (getSerialNo.success)
                {
                    _userDialogs.HideLoading();
                    //_userDialogs.Alert(AppConst.DataaddedSucessFully, "Alert");
                    await CurrentPage.DisplayAlert("RAMMS", AppConst.DataaddedSucessFully, "OK");
                    await CoreMethods.PopPageModel();
                }
            });

        }

        public void DeleteHeader(GetSerialNo getSerialNo)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _userDialogs.HideLoading();
                if (getSerialNo.success)
                {
                    MyBaseFormJList.Remove(SelectedPageResult);
                    _userDialogs.Alert(AppConst.DataRemovedSucessFully, "Alert");
                }
                else
                {
                    _userDialogs.Alert("Some thing went wrong", "Alert");
                }
            });

        }

        public void GetFormSerialNo(GetSerialNo getFormJDtlById)
        {

        }

        public void GetFormAssetCodeNo(GetSerialNo getFormJDtlById)
        {
            SelectedfomjDefectCode = App.AssetGroupSelection = getFormJDtlById.data;
        }

        public void GetFormAssetDefectCode(JAssetDropDown getFormJDtlById)
        {

        }

        public void GetDropDownLocationDetailResponse(JAssetDropDown responseBase)
        {

        }

        public void GetDropDownPriorityResponse(JAssetDropDown responseBase)
        {

        }

        public void GetSaveDetil(FormJAddRefrenceNumber formJAddRefrenceNumber)
        {

        }

        public ICommand btnPreviousCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    if (iValueRet > 1)
                        iValueRet = iValueRet - 1;

                    GetAllGridData();
                });
            }
        }
        public ICommand btnNextCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    if (iValueRet <= (Convert.ToInt32(TotalPageCount) / RecordPerPage))

                        iValueRet = iValueRet + 1;

                    GetAllGridData();
                    
                });
            }
        }

        public ICommand ClickMeActionCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    var SelectedFormARowItem = (PageResult)obj;
                    if (!FromView)
                    {
                        string actionResult;
                        if (Model.Security.IsDelete(ModuleNameList.NOD))
                            actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "Edit", "View", "Delete");
                        else
                            actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "Edit", "View");

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
                                        SelectedPageResult = SelectedFormARowItem;
                                        //ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));
                                        RestRequest restRequest = new RestRequest();
                                        restRequest.AddQueryParameter("formJDtl", SelectedFormARowItem.No.ToString());
                                        ThreadPool.QueueUserWorkItem(o => new FormDelete(this).DeteleDetail(restRequest));
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
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                var objects = new Dictionary<string, object>();
                                objects.Add("From", "edit");
                                objects.Add("asset", SelectedAsset);
                                objects.Add("Data", SelectedFormARowItem);
                                objects.Add("HeaderData", formJSearchGridDTOglobal);

                                App.HeaderCode = SelectedFormARowItem.No;

                                await CoreMethods.PushPageModel<FormJOnePageModel>(objects);
                            });
                           
                        }
                        else if (actionResult == "View")
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                var objects = new Dictionary<string, object>();
                                objects.Add("From", "view");
                                objects.Add("asset", SelectedAsset);
                                objects.Add("Data", SelectedFormARowItem);
                                objects.Add("HeaderData", formJSearchGridDTOglobal);

                                App.HeaderCode = SelectedFormARowItem.No;

                                await CoreMethods.PushPageModel<FormJOnePageModel>(objects);
                            });
                            
                        }
                    }
                    else
                    {

                        var actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "View");

                        //var actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, null, "View", null);

                        if (actionResult == "View")
                        {
                            Device.BeginInvokeOnMainThread(async () =>
                            {
                                var objects = new Dictionary<string, object>();
                                objects.Add("From", "view");
                                objects.Add("asset", SelectedAsset);
                                objects.Add("Data", SelectedFormARowItem);
                                objects.Add("HeaderData", formJSearchGridDTOglobal);

                                await CoreMethods.PushPageModel<FormJOnePageModel>(objects);
                            });
                          

                        }
                    }

                });
            }

        }
    }
        public static class ImageConvertBase64
        {
            public static string ConvertToBase64(this Stream stream)
            {
                byte[] bytes;
                using (var memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);
                    bytes = memoryStream.ToArray();
                }

                string base64 = Convert.ToBase64String(bytes);
                return base64;
            }

        }
    }


