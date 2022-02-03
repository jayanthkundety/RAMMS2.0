using System;
using System.Collections;
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
using System.Diagnostics;
using System.Threading.Tasks;
using Rg.Plugins.Popup;
using Rg.Plugins.Popup.Services;
using RAMMS.MobileApps.Page;
using RAMMS.MobileApps.Controls;
using Plugin.Connectivity;
using System.Net.Http;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.RequestBO;

namespace RAMMS.MobileApps.PageModel
{
    public delegate void printString(List<JAssetDropDownData> s);
    public class FormJOnePageModel : FreshBasePageModel, IFormJOne
    {
        //ExtendedPicker SaCodePciker;
        private IRestApi _restApi;
        ExtendedPicker saCodePciker, prioritypicker, locationpicker;
        IUserDialogs _userDialogs;
        JAssetDropDownData SelectedAsset;
        FormJFindDetailResponse CurrentselectedRoadCodeResponse;
        FormJSearchGridDTO _CurrentselectedRoadCodeResponse1;
        int? CurentSerailNumber, FromKmInt, FromMInt, ToKmInt, ToMInt; 
        bool FromEdit;
        PageResult pageResult;
        CustomDatePicker datePicker;
        bool IsUpdateTrue,IsSAveandExit;
        public IList<JAssetDropDownData> SectedLocationItem;
        private bool _isPhotoTabVisible;
        Grid MainGrid;
        public ObservableCollection<FormJImageListResponseDTO> DetailImageList { get; set; }
        Image image { get; set; }
        Label label { get; set; }

        public bool IsPhotoTabVisible
        {
            get
            {
                return _isPhotoTabVisible;
            }
            set
            {
                _isPhotoTabVisible = value;
                RaisePropertyChanged("IsPhotoTabVisible");
            }
        }
        IList<JAssetDropDownData> _LocationItem;
        public IList<JAssetDropDownData> LocationItem
        {
            get
            {
                return _LocationItem;
            }
            set
            {
                if(_LocationItem != value)
                {
                    _LocationItem = value;
                }
            }
        }
        public IList<JAssetDropDownData> PriorityItem { get; set; }

        IList<JAssetDropDownData> _SAPickerBinding;
        public IList<JAssetDropDownData> JAssetDropDownData
        {
            get
            {
                return _SAPickerBinding;
            }
            set
            {
                if (_SAPickerBinding != value)
                {

                    _SAPickerBinding = value;
                }
            }
        }
        string _FromKm;
        public string FromKm
        {
            get
            {
                return _FromKm;
            }
            set
            {
                if (_FromKm != value)
                {
                    _FromKm = value;

                    if (FromKm.Length > 0 && !FromKm.Contains("."))
                    {
                        FromKmInt = Convert.ToInt32(_FromKm);
                    }
                }
            }
        }
        string _FromM;
        public string FromM
        {
            get
            {
                return _FromM;
            }
            set
            {
                if (_FromM != value)
                {
                    _FromM = value;
                    if (FromM.Length > 0 && !FromM.Contains("."))
                    {
                        FromMInt = Convert.ToInt32(FromM);
                    }
                }
            }
        }
        string _ToKm;
        public string ToKm
        {
            get
            {
                return _ToKm;
            }
            set
            {
                if (_ToKm != value)
                {
                    _ToKm = value;
                    if (ToKm.Length > 0 && !ToKm.Contains("."))
                    {
                        ToKmInt = Convert.ToInt32(ToKm);
                    }
                }
            }
        }

        string _ToM;
        public string ToM
        {
            get
            {
                return _ToM;
            }
            set
            {
                if (_ToM != value)
                {
                    _ToM = value;
                    if (ToM.Length > 0 && !ToM.Contains("."))
                    {
                        ToMInt = Convert.ToInt32(ToM);
                    }
                }
            }
        }
        string _ProblemDetail;
        public string ProblemDetail
        {
            get
            {
                return _ProblemDetail;
            }
            set
            {
                if (_ProblemDetail != value)
                {
                    _ProblemDetail = value;

                }
            }
        }
        string _Needed;
        public string Needed
        {
            get
            {
                return _Needed;
            }
            set
            {
                if (_Needed != value)
                {
                    _Needed = value;
                }
            }
        }

        private int? _Length = null;
        public int? Length
        {
            get { return _Length; }
            set
            {
                if (value == null || value == 0)
                {
                    _Length = null;
                }
                else if (!_Length.HasValue || !_Length.Equals(value))
                {

                    if (value.ToString().Length <= 10)
                        _Length = value;
                }
                RaisePropertyChanged();
            }
        }

        private int? _Width = null;
        public int? Width
        {
            get { return _Width; }
            set
            {
                if (value == null || value == 0)
                {
                    _Width = null;
                }
                else if (!_Width.HasValue || !_Width.Equals(value))
                {
                    if (value.ToString().Length <= 10)
                        _Width = value;
                }
                RaisePropertyChanged();
            }
        }

        private int? _Height = null;
        public int? Height
        {
            get { return _Height; }
            set
            {
                if (value == null || value == 0)
                {
                    _Height = null;
                }
                else if (!_Height.HasValue || !_Height.Equals(value))
                {
                    if (value.ToString().Length <= 10)
                        _Height = value;
                }
                RaisePropertyChanged();
            }
        }

        private int? _selectedWI = null;
        private int? _selectedWS = null;
        private int? _selectedWTC = null;
        private int? _selectedWC = null;
        private int? _selectedRT = null;
        public int? WIInt
        {
            get { return _selectedWI; }
            set
            {
                if (value == null || value == 0)
                {
                    _selectedWI = null;
                }
                else if (!_selectedWI.HasValue || (!_selectedWI.Equals(value) && value.Value <= 52))
                {
                    _selectedWI = value;

                }
                else
                {
                    UserDialogs.Instance.Alert("Please enter the value ranges between 0-52");
                }

                RaisePropertyChanged();
            }
        }
        public int? WSInt
        {
            get { return _selectedWS; }
            set
            {
                if (value == null || value == 0)
                {
                    _selectedWS = null;
                }
                else if (!_selectedWS.HasValue || (!_selectedWS.Equals(value) && value.Value <= 52))
                {
                    _selectedWS = value;

                }
                else
                {
                    UserDialogs.Instance.Alert("Please enter the value ranges between 0-52");
                }

                RaisePropertyChanged();
            }
        }

        public int? WTCInt
        {
            get { return _selectedWTC; }
            set
            {
                if (value == null || value == 0)
                {
                    _selectedWTC = null;
                }
                else if (!_selectedWTC.HasValue || (!_selectedWTC.Equals(value) && value.Value <= 52))
                {
                    _selectedWTC = value;

                }
                else
                {
                    UserDialogs.Instance.Alert("Please enter the value ranges between 0-52");
                }

                RaisePropertyChanged();
            }
        }

        public int? WCInt
        {
            get { return _selectedWC; }
            set
            {
                if (value == null || value == 0)
                {
                    _selectedWC = null;
                }
                else if (!_selectedWC.HasValue || (!_selectedWC.Equals(value) && value.Value <= 52))
                {
                    _selectedWC = value;

                }
                else
                {
                    UserDialogs.Instance.Alert("Please enter the value ranges between 0-52");
                }

                RaisePropertyChanged();
            }
        }
        public int? RTInt
        {
            get { return _selectedRT; }
            set
            {
                if (value == null || value == 0)
                {
                    _selectedRT = null;
                }
                else if (!_selectedRT.HasValue || (!_selectedRT.Equals(value) && value.Value <= 52))
                {
                    _selectedRT = value;

                }
                else
                {
                    UserDialogs.Instance.Alert("Please enter the value ranges between 0-52");
                }

                RaisePropertyChanged();
            }
        }
        
        string _Remark;
        public string Remark
        {
            get
            {
                return _Remark;
            }
            set
            {
                if (_Remark != value)
                {
                    _Remark = value;
                }
            }
        }
        string _RefrenceNumber;
        public string RefrenceNumber
        {
            get
            {
                return _RefrenceNumber;
            }
            set
            {
                if (_RefrenceNumber != value)
                {
                    _RefrenceNumber = value;
                }
            }
        }

        public DateTime? SelectedDate { get; set; } = null;

        //SelectedLocation
        string _SelectedLocation;
        public string SelectedLocation
        {
            get
            {
                return _SelectedLocation;
            }
            set
            {
                if (_SelectedLocation != value)
                {
                    Console.WriteLine(_SelectedLocation);
                    _SelectedLocation = value;
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

        bool _ExitEnabled;
        public bool ExitEnabled
        {
            get
            {
                return _ExitEnabled;
            }
            set
            {
                if (_ExitEnabled != value)
                {
                    _ExitEnabled = value;
                }
            }
        }

        bool _ClearEnabled;
        public bool ClearEnabled
        {
            get
            {
                return _ClearEnabled;
            }
            set
            {
                if (_ClearEnabled != value)
                {
                    _ClearEnabled = value;
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

        public DateTime? MinimumDate { get; set; } = null;
        public DateTime? MaximumDate { get; set; } = null;

        List<JAssetDropDownData> selectedlocationsiteref;
        JAssetDropDownData selectedsacode, selectedpriority;
        Button btnCancel1;
        ExtendedEntry EntryWI, EntryWS, EntryWTC, EntryWC, EntryRT;
        EntryControl EntryFromKm, EntryFromM, EntryTokm, EntryToM, EntryLength, EntryWidth, EntryHeight;
        StackLayout LocationButtonDrop;
        Editor EditorProblemDetail, EditorRemark, EditorNeeded;
        public bool FromView { get; set; }
        public FormJOnePageModel(IUserDialogs userDialogs, IRestApi restApi)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;

        }
        public override void Init(object initData)
        {
            base.Init(initData);

            try
            {
                DetailImageList = new ObservableCollection<FormJImageListResponseDTO>();
                MainGrid = CurrentPage.FindByName<Grid>("MainGrid");
                EntryFromKm = CurrentPage.FindByName<EntryControl>("EntryFromKm");
                EntryFromM = CurrentPage.FindByName<EntryControl>("EntryFromM");

                EntryTokm = CurrentPage.FindByName<EntryControl>("EntryTokm");
                EntryToM = CurrentPage.FindByName<EntryControl>("EntryToM");

                EntryLength = CurrentPage.FindByName<EntryControl>("EntryLength");
                EntryWidth = CurrentPage.FindByName<EntryControl>("EntryWidth");
                EntryHeight = CurrentPage.FindByName<EntryControl>("EntryHeight");

                EntryWI = CurrentPage.FindByName<ExtendedEntry>("EntryWI");
                EntryWS = CurrentPage.FindByName<ExtendedEntry>("EntryWS");
                EntryWTC = CurrentPage.FindByName<ExtendedEntry>("EntryWTC");
                EntryWC = CurrentPage.FindByName<ExtendedEntry>("EntryWC");
                EntryRT = CurrentPage.FindByName<ExtendedEntry>("EntryRT");
                LocationButtonDrop = CurrentPage.FindByName<StackLayout>("LocationButtonDrop");
                btnCancel1 = CurrentPage.FindByName<Button>("btnCancel1");

                EditorProblemDetail = CurrentPage.FindByName<Editor>("EditorProblemDetail");
                EditorRemark = CurrentPage.FindByName<Editor>("EditorRemark");
                EditorNeeded = CurrentPage.FindByName<Editor>("EditorNeeded");

                saCodePciker = CurrentPage.FindByName<ExtendedPicker>("sacodepicker");
                prioritypicker = CurrentPage.FindByName<ExtendedPicker>("priorityPicker");
                //locationpicker = CurrentPage.FindByName<ExtendedPicker>("locationPicker");
                datePicker = CurrentPage.FindByName<CustomDatePicker>("datePicker");
                //datePicker.DateSelected += DatePicker_DateSelected;
                //SelectedDate = datePicker.Date; //.ToString("yyyy-MM-dd");
                Setup();
                IsPhotoTabVisible = false;
                var data = initData as Dictionary<string, object>;

                if (!data.ContainsKey("From"))
                {
                    SelectedAsset = (JAssetDropDownData)data["asset"];
                    if (!data.ContainsKey("refrencenumberEdit"))
                    {
                        CurrentselectedRoadCodeResponse = (FormJFindDetailResponse)data["refrencenumber"];
                    }
                    else
                    {
                       // FromEdit = true;
                        var datas = (FormJSearchGridDTO)data["refrencenumberEdit"];

                        var findetails = new FormJFindDetailResponseData();
                        findetails.ContNo = datas.ContNo;
                        findetails.CreatedBy = datas.CreatedBy;
                        findetails.ActiveYn = datas.ActiveYn;
                        findetails.AssetGroupCode = datas.AssetGroupCode;
                        findetails.CreatedDt = datas.CreatedDt;
                        findetails.DesignationPrp = datas.DesignationPrp;
                        findetails.DesignationVer = datas.DesignationVer;
                        findetails.DesignationVet = datas.DesignationVet;
                        findetails.DtPrp = datas.DtPrp;
                        //CurrentselectedRoadCodeResponse.data.FormADetails = datas.FormADetails;
                        findetails.Id = datas.Id;
                        findetails.ModBy = datas.ModBy;
                        findetails.ModDt = datas.ModDt;
                        findetails.Month = datas.Month;
                        findetails.MonthYear = datas.MonthYear;
                        findetails.No = datas.No;
                        findetails.Remarks = datas.Remarks;
                        findetails.Rmu = datas.Rmu;
                        findetails.RmuName = datas.RmuName;
                        findetails.RoadCode = datas.RoadCode;
                        findetails.RoadName = datas.RoadName;
                        findetails.section = datas.section;
                        findetails.SectionCode = datas.SectionCode;
                        findetails.SignPrp = datas.SignPrp;
                        findetails.SignVer = datas.SignVer;
                        findetails.Status = datas.Status;
                        findetails.SubmitSts = datas.SubmitSts;
                        findetails.UseridPrp = datas.UseridPrp;
                        findetails.UseridVer = datas.UseridVer;
                        findetails.UsernamePrp = datas.UsernamePrp;
                        findetails.UsernameVer = datas.UsernameVer;
                        findetails.UsernameVet = datas.UsernameVet;
                        findetails.VerifiedDt = datas.VerifiedDt;
                        findetails.VerifiedVDt = datas.VerifiedVDt;
                        findetails.Year = datas.Year;
                        findetails.MonthYear = datas.MonthYear;
                        var formjs = new FormJFindDetailResponse();
                        formjs.data = findetails;
                        CurrentselectedRoadCodeResponse = formjs;
                      
                    }

                    CancelEnabled = true;
                    SaveEnabled = true;
                    ExitEnabled = true;
                    ClearEnabled = true;


                    if (CurrentselectedRoadCodeResponse != null && SelectedAsset != null)
                    {

                        //int? iobjData = await GetLastRecordReferenceNumber(CurrentselectedRoadCodeResponse.data.No);

                        //CurentSerailNumber = (int)iobjData;

                        //RefrenceNumber = CurrentselectedRoadCodeResponse.data.Id + "/" + CurentSerailNumber;


                        //ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));
                        RestRequest resetRequest = new RestRequest();
                        resetRequest.AddQueryParameter("headerId", CurrentselectedRoadCodeResponse.data.No.ToString());
                        ThreadPool.QueueUserWorkItem(o => new FormJOne(this).GetSerialNo(resetRequest));
                        ThreadPool.QueueUserWorkItem(o => new FormJOne(this).GetLocationNo(CreateAssetParameter(DropdownEnum.locationref)));
                        ThreadPool.QueueUserWorkItem(o => new FormJOne(this).GetPrioritylNo(CreateAssetParameter(DropdownEnum.Prioitry)));
                        RestRequest resetRequest1 = new RestRequest();
                        resetRequest1.AddQueryParameter("name", SelectedAsset.Value);
                        ThreadPool.QueueUserWorkItem(o => new FormJOne(this).GetAssetNameJ(resetRequest1));
                    }

                    Device.BeginInvokeOnMainThread( async () =>
                    {
                        if (CurrentselectedRoadCodeResponse != null)
                        {
                            var datas = CurrentselectedRoadCodeResponse;
                            int selectedMonth = Convert.ToInt32(datas.data.Month);
                            int selectedYear = Convert.ToInt32(datas.data.Year);
                            var minDate = new DateTime(selectedYear, selectedMonth, 1);
                            var maxDate = minDate.AddMonths(1).AddDays(-1);
                            MinimumDate = minDate;
                            MaximumDate = maxDate;
                            await Task.Delay(2000);
                            SelectedDate = null;
                        }
                        else
                        {
                            var datas = (FormJSearchGridDTO)data["refrencenumberEdit"];
                            int selectedMonth = Convert.ToInt32(datas.Month);
                            int selectedYear = Convert.ToInt32(datas.Year);
                            var minDate = new DateTime(selectedYear, selectedMonth, 1);
                            var maxDate = minDate.AddMonths(1).AddDays(-1);
                            MinimumDate = minDate;
                            MaximumDate = maxDate;
                            await Task.Delay(2000);
                            SelectedDate = null;
                        }


                    });
                    

                }
                else
                {
                    FromEdit = true;


                    if (data.ContainsKey("From") && data["From"].Equals("view"))
                    {
                        
                        saCodePciker.IsEnabled = false;
                        prioritypicker.IsEnabled = false;
                       //locationpicker.IsEnabled = false;
                        datePicker.IsEnabled = false;
                        EntryFromKm.IsEnabled = false;
                        EntryFromM.IsEnabled = false;
                        EntryTokm.IsEnabled = false;
                        EntryToM.IsEnabled = false;
                        EntryLength.IsEnabled = false;
                        EntryWidth.IsEnabled = false;
                        EntryHeight.IsEnabled = false;
                        EntryWI.IsEnabled = false;
                        EntryWS.IsEnabled = false;
                        EntryWTC.IsEnabled = false;
                        EntryWC.IsEnabled = false;
                        EntryRT.IsEnabled = false;
                        EditorProblemDetail.IsEnabled = false;
                        EditorRemark.IsEnabled = false;
                        EditorNeeded.IsEnabled = false;
                        LocationButtonDrop.IsEnabled = false;

                        CancelEnabled = true;
                        SaveEnabled = false;
                        ExitEnabled = false;
                        ClearEnabled = false;

                        FromView = true;

                    }
                    else
                    {
                        CancelEnabled = true;
                        SaveEnabled = true;
                        ExitEnabled = true;
                        ClearEnabled = true;

                    }
                    

                    pageResult = (PageResult)data["Data"];
                    SelectedAsset = (JAssetDropDownData)data["asset"];
                    CurentSerailNumber = pageResult.Srno;
                    //  ThreadPool.QueueUserWorkItem(o => _userDialogs.ShowLoading("Loading"));
                    //param.Add("No", 0);

                    // param.Add("HeaderNo", Convert.ToInt32(CurrentselectedRoadCodeResponse.data.No));

                    Needed = pageResult.WorkNeed;
                    //Width = pageResult.Width.ToString();
                    Width = pageResult.Width;
                    //Length = pageResult.Length.ToString();
                    Length = pageResult.Length;
                    //Height = pageResult.Height.ToString();
                    Height = pageResult.Height;
                    ToM = pageResult.ToChDeci.ToString();
                    ToKm = pageResult.ToCh.ToString();
                    FromM = pageResult.FromChDeci.ToString();
                    FromKm = pageResult.FromCh.ToString();
                    CurentSerailNumber = pageResult.Srno;
                    WIInt = Convert.ToInt32(pageResult.Wi);
                    RTInt = Convert.ToInt32(pageResult.Rt);
                    WCInt = Convert.ToInt32(pageResult.Wc);
                    WTCInt = Convert.ToInt32(pageResult.Wtc);
                    WSInt = Convert.ToInt32(pageResult.Ws);
                    Remark = pageResult.Remarks;
                    ProblemDetail = pageResult.Problem;
                    //SelectedDate = Convert.ToDateTime(pageResult.Dt);
                    RefrenceNumber = pageResult.FadRefNO;
                    selectedlocationsiteref = (List<JAssetDropDownData>)pageResult.SiteRef_multiSelect;
                    SelectedLocation = pageResult.SiteRef;

                    //RestRequest resetRequest = new RestRequest();
                    //resetRequest.AddQueryParameter("formJDtl", pageResult.No.ToString());
                    //ThreadPool.QueueUserWorkItem(o => new FormJOne(this).GetSerialNo(resetRequest));
                    ThreadPool.QueueUserWorkItem(o => new FormJOne(this).GetLocationNo(CreateAssetParameter(DropdownEnum.locationref)));
                    ThreadPool.QueueUserWorkItem(o => new FormJOne(this).GetPrioritylNo(CreateAssetParameter(DropdownEnum.Prioitry)));
                    RestRequest resetRequest1 = new RestRequest();
                    resetRequest1.AddQueryParameter("name", SelectedAsset.Value);
                    ThreadPool.QueueUserWorkItem(o => new FormJOne(this).GetAssetNameJ(resetRequest1));
                    // GetFormAssetCodeNo();

                    var datas = (FormJSearchGridDTO)data["HeaderData"];

                    if (datas != null)
                    {
                        int selectedMonth = Convert.ToInt32(datas.Month);
                        int selectedYear = Convert.ToInt32(datas.Year);
                        var minDate = new DateTime(selectedYear, selectedMonth, 1);
                        var maxDate = minDate.AddMonths(1).AddDays(-1);
                        MinimumDate = minDate;
                        MaximumDate = maxDate;
                        var array = pageResult.Dt.Split('-');
                        //var date = Convert.ToDateTime(pageResult.Dt); 
                        var date = Convert.ToDateTime(array[1] + "-" + array[0] + "-" + array[2]);
                        SelectedDate = date;
                    }
                    else
                    {
                        var array = pageResult.Dt.Split('-');
                        int selectedMonth = Convert.ToInt32(array[1]);
                        int selectedYear = Convert.ToInt32(array[2]);
                        var minDate = new DateTime(selectedYear, selectedMonth, 1);
                        var maxDate = minDate.AddMonths(1).AddDays(-1);
                        MinimumDate = minDate;
                        MaximumDate = maxDate;
                        var date = Convert.ToDateTime(array[1] + "-" + array[0] + "-" + array[2]);
                        SelectedDate = date;
                    }
                }


            }
            catch (Exception ex)
            {
                _userDialogs.HideLoading();
            }

        }

        void FromLandingGridEdit(object datas)
        {

        }
        private void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {

            var dates = sender as DatePicker;
            SelectedDate = dates.Date; //.ToString("yyyy-MM-dd");


        }

        public async Task<int> GetLastRecordReferenceNumber(int iRefCode)
        {
            int iStrValue = 0;

            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {

                    try
                    {
                        var response = await _restApi.GetDetailSerialNoFormJ(iRefCode);

                        if(response.data.HasValue)
                        iStrValue = response.data.Value + 1;

                        return iStrValue;

                    }
                    catch (Exception ex)
                    {
                        _userDialogs.Toast(ex.Message);
                    }

                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }
            return iStrValue;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            MessagingCenter.Subscribe<FormJOnePage>(this, "AddNew", (expense) =>
            {
               // Expenses.Add(expense);
            });
            //MessagingCenter.Subscribe<LocationPage, string>(this, "Frompopup",  (sender, arg) =>
            //{

            //});

            string strDetailCode = Convert.ToInt32(App.HeaderCode).ToString();

            GetImageList(strDetailCode);


            MessagingCenter.Unsubscribe<object, string>(this, "Uploaded");

            MessagingCenter.Subscribe<object, string>(this, "Uploaded", (obj, s) =>
            {
                GetImageList(strDetailCode);
            });

        }
        private async void GetImageList(string AssetID)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        var hdrresponse = await _restApi.GetImagesFormJ(App.HeaderCode.ToString());

                        if(hdrresponse.success == true)
                        {
                             DetailImageList = new  ObservableCollection<FormJImageListResponseDTO>(hdrresponse.data);
                            int i = 0;

                            try
                            {
                                MainGrid.Children.Clear();
                                foreach (var listdata in DetailImageList)
                                {
                                    listdata.SNO = i + 1;
                                    string Path = listdata.ImageFilename;

                                    try
                                    {
                                        ActivityIndicator indicator = new ActivityIndicator() { BindingContext = image };
                                        indicator.SetBinding(ActivityIndicator.IsRunningProperty, new Binding("IsLoading", source: image));
                                        indicator.SetBinding(ActivityIndicator.IsVisibleProperty, new Binding("IsLoading", source: image));
                                        image = new Image
                                        {
                                            Source = ImageSource.FromUri(new Uri(AppConst.WebBaseURL + Path))
                                        };

                                        label = new Label
                                        {
                                            Text = listdata.ImageFilenameSys,
                                            HorizontalTextAlignment = TextAlignment.Center,
                                            Margin = -10
                                        };
                                        MainGrid.RowDefinitions.Add(new RowDefinition { Height = 100 });
                                        Grid.SetColumn(indicator, i);
                                        Grid.SetColumn(image, i);
                                        Grid.SetRow(label, 1);
                                        Grid.SetColumn(label, i);

                                        MainGrid.Children.Add(indicator);
                                        MainGrid.Children.Add(image);
                                        MainGrid.Children.Add(label);

                                        i = i + 1;
                                    }
                                    catch (Exception ex)
                                    {

                                    }

                                }
                            }
                            catch (Exception ex)
                            { }

                        }
                    }
                    catch (Exception ex) { }
                }
            }
            catch { }
        }

        public ICommand AddImageCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    try
                    {
                        if (!Vaildate())
                        {
                            return;
                        }
                        await CurrentPage.Navigation.PushAsync(new FormJCameraPopupPage());
                        //await PopupNavigation.Instance.PushAsync(new CameraPopUpPage());
                    }
                    catch { }
                });
            }
        }
        public ICommand DeleteImageCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    try
                    {
                        var actionResult = await UserDialogs.Instance.ConfirmAsync("Are you sure want to delete?", "RAMS", "Yes", "No");
                        if (actionResult)
                        {
                            _userDialogs.ShowLoading("Loading");
                            if (CrossConnectivity.Current.IsConnected)
                            {
                                var imageID = (obj as FormJImageListResponseDTO).ImageId;
                                var response = await _restApi.DeleteImageFormJ(imageID);

                                if (response.success)
                                {
                                    await UserDialogs.Instance.AlertAsync("Image deleted successfully.", "RAMS", "0K");

                                    string strDetailCode = Convert.ToInt32(App.HeaderCode).ToString();

                                    GetImageList(strDetailCode);
                                }
                                else
                                    _userDialogs.Toast(response.errorMessage);
                            }
                            else
                                UserDialogs.Instance.Alert("Please check your Internet Connection !");
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
                });
            }

        }

        void Setup()
        {
            saCodePciker.SelectedIndexChanged += SaCodePciker_SelectedIndexChanged;
            prioritypicker.SelectedIndexChanged += Prioritypicker_SelectedIndexChanged;
           // locationpicker.SelectedIndexChanged += Locationpicker_SelectedIndexChanged;
        }

        private void Locationpicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (locationpicker.SelectedIndex == -1)
            {

            }
            else
            {
               // selectedlocationsiteref = LocationItem[locationpicker.SelectedIndex];
                //   var colorName = locationpicker.Items[locationpicker.SelectedIndex];

            }
        }

        private void Prioritypicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (prioritypicker.SelectedIndex == -1)
            {

            }
            else
            {

                selectedpriority = PriorityItem[prioritypicker.SelectedIndex];
                //  var colorName = locationpicker.Items[locationpicker.SelectedIndex];

            }
        }

        private void SaCodePciker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (saCodePciker.SelectedIndex == -1)
            {

            }
            else
            {

                selectedsacode = JAssetDropDownData[saCodePciker.SelectedIndex];
                //var colorName = locationpicker.Items[locationpicker.SelectedIndex];
                ProblemDetail = selectedsacode.Text.ToString();

            }
        }

        void GetSerialNo()
        {

            RestRequest resetRequest = new RestRequest();
            resetRequest.AddQueryParameter("formJDtl", CurrentselectedRoadCodeResponse.data.No.ToString());
            ThreadPool.QueueUserWorkItem(o => new FormJOne(this).GetSerialNo(resetRequest));
        }
        void GetLocation()
        {
            ThreadPool.QueueUserWorkItem(o => new FormJOne(this).GetLocationNo(CreateAssetParameter(DropdownEnum.locationref)));
        }
        void GetPriority()
        {
            ThreadPool.QueueUserWorkItem(o => new FormJOne(this).GetPrioritylNo(CreateAssetParameter(DropdownEnum.Prioitry)));
        }
        public string CreateAssetParameter(DropdownEnum dropdownEnum)
        {
            var param = new Dictionary<string, string>();
            if (dropdownEnum == DropdownEnum.locationref)
            {
                param.Add("type", "Site Ref");
            }

            else
            {
                param.Add("type", "Priority");
            }

            return JsonConvert.SerializeObject(param);
        }
        public void APIHitFailed(string error)
        {
            _userDialogs.HideLoading();
        }


        public void GetFormSerialNo(GetSerialNo getFormJDtlById)
        {
            CurentSerailNumber = Convert.ToInt32(getFormJDtlById.data) + 1; ;
            RefrenceNumber = CurrentselectedRoadCodeResponse.data.Id + "/" + CurentSerailNumber;

        }

        public void GetDropDownLocationDetailResponse(JAssetDropDown responseBase)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _userDialogs.HideLoading();
                LocationItem = responseBase.data;
                try
                {
                    if (FromEdit)
                    {
                        if (!string.IsNullOrEmpty(pageResult.SiteRef))
                        {
                            var multiItem = pageResult.SiteRef.Split(',').OfType<string>().ToList();
                            foreach(var item in multiItem)
                            {
                                LocationItem.FirstOrDefault(x => x.Value == item).isSelected = true;
                            }
                        }
                    }
                }
                catch(Exception ex)
                {

                }
                
            });
        }

        public void GetDropDownPriorityResponse(JAssetDropDown responseBase)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                //PriorityItem = responseBase.data;
                PriorityItem = new List<JAssetDropDownData>();
                foreach (var item in responseBase.data)
                {
                    JAssetDropDownData listItems = new JAssetDropDownData();
                    listItems = item;
                    if (item.Text == "Critical Intervention Lev.")
                    {
                        listItems.Value = "CIL";
                        PriorityItem.Add(listItems);
                    }
                    if (item.Text == "High Priority")
                    {
                        listItems.Value = "1";
                        PriorityItem.Add(listItems);
                    }
                    if (item.Text == "Normal Priority")
                    {
                        listItems.Value = "2";
                        PriorityItem.Add(listItems);
                    }
                }

                prioritypicker.ItemsSource = PriorityItem.Select((JAssetDropDownData arg) => arg.Value).ToList();
                if (FromEdit)
                {
                    if (pageResult.Priority != null)
                    {
                        var prity = (from s in PriorityItem where s.Value.Equals(pageResult.Priority) select s).ToList()[0];
                        var indexs = PriorityItem.IndexOf(prity);
                        prioritypicker.SelectedIndex = indexs;
                        selectedpriority = prity;
                    }
                }
            });
           
        }

        public void GetFormAssetCodeNo(GetSerialNo getFormJDtlById)
        {

            RestRequest resetRequest1 = new RestRequest();
            if (getFormJDtlById.data == "CWR" || getFormJDtlById.data == "CWU" || getFormJDtlById.data == "CWF")
            {
                resetRequest1.AddQueryParameter("assetGroup", "");
            }
            else
            {
                resetRequest1.AddQueryParameter("assetGroup", getFormJDtlById.data);
            }
            ThreadPool.QueueUserWorkItem(o => new FormJOne(this).GetDefectCodeNameJ(resetRequest1));

            _userDialogs.HideLoading();
        }

        public void GetFormAssetDefectCode(JAssetDropDown getFormJDtlById)
        {
            //SAPickerBinding = new List<JAssetDropDownData>();
            Device.BeginInvokeOnMainThread(() =>
            {
                JAssetDropDownData = getFormJDtlById.data;
                saCodePciker.ItemsSource = JAssetDropDownData.Select((JAssetDropDownData arg) => arg.Text).ToList();
                if (FromEdit)
                {
                    // var prtiy1 = JAssetDropDownData.IndexOf(item => item.)
                    var prity = (from s in JAssetDropDownData where s.Value.Equals(pageResult.DefCode) select s).ToList()[0];
                    var indexs = JAssetDropDownData.IndexOf(prity);
                    saCodePciker.SelectedIndex = indexs;
                    selectedsacode = prity;
                }
                _userDialogs.HideLoading();
            });
            
            // saCodePciker.ItemsSource = JAssetDropDownData;
        }
        bool Vaildate()
        {
            //if (string.IsNullOrEmpty(SelectedDate))
            if(SelectedDate ==null)
            {
                _userDialogs.Alert("Please select the Date of Inspection", "Error");
                return false;

            }
            else if (string.IsNullOrEmpty(SelectedLocation) )
            {
                _userDialogs.Alert("Please select the Location Site Ref", "Error");
                return false;
            }
            else if (selectedsacode == null)
            {
                _userDialogs.Alert(" Please select the SA code", "Error");
                return false;
            }
            //else if (selectedpriority == null)
            //{
            //    _userDialogs.Alert("Selecte the priority", "Error");
            //    return false;
            //}
            else if (string.IsNullOrEmpty(FromKm))
            {
                _userDialogs.Alert("Please enter the Location Chainage From Km", "Error");
                return false;
            }
            else if (string.IsNullOrEmpty(FromM))
            {
                _userDialogs.Alert("Please enter the Location Chainage From m", "Error");
                return false;
            }
            else if (string.IsNullOrEmpty(ToKm))
            {
                _userDialogs.Alert("Please enter the Location Chainage To Km", "Error");
                return false;
            }
            else if (string.IsNullOrEmpty(ToM))
            {
                _userDialogs.Alert("Please enter the Location Chainage To m", "Error");
                return false;
            }
            //else if (string.IsNullOrEmpty(ProblemDetail))
            //{
            //    _userDialogs.Alert("Enter the ProblemDetail", "Error");
            //    return false;
            //}
            //else if (string.IsNullOrEmpty(Needed))
            //{
            //    _userDialogs.Alert("Enter the Needed", "Error");
            //    return false;
            //}
            //else if(selectedlocationsiteref ==null)
            //{
            //    _userDialogs.Alert("Select Location Site Ref", "Error");
            //    return false;
            //}
            return true;

        }

        int pkNo = 0;
        string CreateParam(string savevalue)
        {
            //
            if (FromEdit)
            {
                var param = new Dictionary<string, object>();
                param.Add("No", pageResult.No);
                param.Add("HeaderNo", pageResult.HeaderNo);
                param.Add("Wc", WCInt);
                param.Add("Wi", WIInt);
                param.Add("Ws", WSInt);
                param.Add("Wtc", WTCInt);
                param.Add("WorkNeed", Needed);
                param.Add("Width", Width);
                param.Add("Length", Length);
                param.Add("Height", Height);
                param.Add("ToChDeci", ToMInt);
                param.Add("ToCh", ToKmInt);
                param.Add("FromCh", FromKm);
                param.Add("FromChDeci", FromM);
                param.Add("Srno", CurentSerailNumber);
                ArrayList arrayList = new ArrayList();
                if (selectedlocationsiteref != null)
                {
                    foreach (var obj in selectedlocationsiteref)
                    {
                        arrayList.Add(obj.Text);
                    }
                    param.Add("SiteRef_multiSelect", arrayList);
                }
                else
                {
                    var objs = SelectedLocation.Split(',');
                    foreach (var obj in objs)
                    {
                        arrayList.Add(obj.ToString());
                    }
                  //  param.Add("SiteRef_multiSelect", arrayList);
                    param.Add("SiteRef_multiSelect", arrayList);
                }
               
                param.Add("Rt", RTInt);
                param.Add("Remarks", Remark);
                param.Add("Problem", ProblemDetail);
                if (selectedpriority != null)
                {
                    param.Add("Priority", selectedpriority.Value);
                }
                else
                {
                    param.Add("Priority", pageResult.Priority);
                }

                param.Add("HeaderRefNo", pageResult.HeaderRefNo);
                param.Add("FadRefNO", pageResult.FadRefNO);


                //if (string.IsNullOrEmpty(SelectedDate))
                if(SelectedDate ==null)
                {
                    param.Add("Dt", pageResult.Dt);
                }
                else
                {
                    param.Add("Dt", SelectedDate?.Date.ToString("yyyy/MM/dd"));
                }
                if (selectedsacode != null)
                {
                    param.Add("DefCode", selectedsacode.Value);
                }
                else
                {
                    param.Add("DefCode", pageResult.DefCode);
                }

                param.Add("CallFrom", savevalue);

                return JsonConvert.SerializeObject(param);
            }
            else
            {
                //var dates = DateTime.Parse(SelectedDate).ToString("dd/MM/yyyy HH:mm:ss");
                var param = new Dictionary<string, object>();
                param.Add("No", pkNo);
                param.Add("HeaderNo", Convert.ToInt32(CurrentselectedRoadCodeResponse.data.No));
                param.Add("Wc", WCInt);
                param.Add("Wi", WIInt);
                param.Add("Ws", WSInt);
                param.Add("Wtc", WTCInt);
                param.Add("WorkNeed", Needed);
                param.Add("Width", Width);
                param.Add("Length", Length);
                param.Add("Height", Height);
                param.Add("ToChDeci", ToMInt);
                param.Add("ToCh", ToKmInt);
                param.Add("FromCh", FromKm);
                param.Add("FromChDeci", FromM);
                param.Add("Srno", CurentSerailNumber);
                ArrayList arrayList = new ArrayList();
                if (selectedlocationsiteref != null)
                {
                    foreach(var obj in selectedlocationsiteref)
                    {
                        arrayList.Add(obj.Text);
                    }
                }
                param.Add("SiteRef_multiSelect", arrayList);
                param.Add("Rt", RTInt);
                param.Add("Remarks", Remark);
                param.Add("Problem", ProblemDetail);
                if (selectedpriority != null)
                {
                    param.Add("Priority", selectedpriority.Value);
                }
                else
                {
                    param.Add("Priority",selectedpriority);
                }
                param.Add("HeaderRefNo", CurrentselectedRoadCodeResponse.data.Id);
                param.Add("FadRefNO", RefrenceNumber);
                param.Add("Dt", SelectedDate?.Date.ToString("yyyy/MM/dd"));
                param.Add("DefCode", selectedsacode.Value);
                param.Add("CallFrom", savevalue);

                return JsonConvert.SerializeObject(param);
            }

        }

        public async void GetSaveDetil(FormJAddRefrenceNumber formJAddRefrenceNumber)
        {
            if (formJAddRefrenceNumber.success)
            {
                App.HeaderCode = Convert.ToInt32(formJAddRefrenceNumber.data);
                Debug.WriteLine(formJAddRefrenceNumber.success);

                 Device.BeginInvokeOnMainThread(async () => {
                    _userDialogs.HideLoading();
                    if (FromEdit)
                    {
                        _userDialogs.Alert("Data Updated successfully", "Alert");
                         if (IsSAveandExit)
                             await CoreMethods.PopPageModel(false, false);
                     }
                    else
                    {
                         pkNo = int.Parse(formJAddRefrenceNumber.data);
                         var confirms = new ConfirmConfig()
                         {
                             Message = "Data Saved successfully",
                             OkText = "Ok",
                             CancelText=null,
                             OnAction = async (result) =>
                             {
                                 if (result)
                                 {
                                     // IsUpdateTrue = true;
                                     AppConst.IsUpdateTrue = true;
                                     if (IsSAveandExit)
                                     {

                                         await CoreMethods.PopPageModel(false, false);
                                     }

                                 }
                                 else
                                 {
                                     // TagListViewModel.RevertSelectedItemChanged();
                                 }
                             }
                         };
                         _userDialogs.Confirm(confirms);
                         
                         //_userDialogs.Alert("Data Saved successfully", "Alert");
                     }
                   
                     
                 });
                

            }
            else
            {
                _userDialogs.Alert("Something went wrong", "Alert");
               
            }
        }
        public ICommand AddButtonCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    IsPhotoTabVisible = false;
                });
            }
        }

        public ICommand PhotoButtonCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    IsPhotoTabVisible = true;
                });
            }
        }
        public ICommand Cancel
        {
            get
            {
                return new Command(async (obj) =>
                {
                    await CoreMethods.PopPageModel(false, false);
                });
            }
        }

        public FreshAwaitCommand Save
        {
            get
            {
                return new FreshAwaitCommand(async (obj) =>
                {
                    try
                    {
                        if (Vaildate())
                        {
                            _userDialogs.ShowLoading("Loading");
                            var getParam = CreateParam("Save & Continue");
                            ThreadPool.QueueUserWorkItem(o => new FormJOne(this).SaveResponse(getParam));
                            IsPhotoTabVisible = true;
                        }
                    }
                    catch (Exception ex)
                    { }
                    finally
                    {
                        obj.SetResult(true);
                    }

                });
            }
        }
        public FreshAwaitCommand Exit
        {
            get
            {
                return new FreshAwaitCommand(async (obj) =>
                {
                    try
                    {
                        if (Vaildate())
                        {
                            _userDialogs.ShowLoading("Loading");
                            var getParam = CreateParam("Save & Exit");
                            ThreadPool.QueueUserWorkItem(o => new FormJOne(this).SaveResponse(getParam));
                            IsSAveandExit = true;
                        }
                    }
                    catch (Exception ex)
                    { }
                    finally
                    {
                        obj.SetResult(true);
                    }


                    ///  ClearData();
                });
            }
        }
        void ClearData()
        {
            //.SelectedIndex = -1;
            saCodePciker.SelectedIndex = -1;
         //   locationpicker.SelectedIndex = -1;
            prioritypicker.SelectedIndex = -1;
            FromKm = "";
            FromKmInt = 0;
            FromM = "";
            FromMInt = 0;
            ToKm = "";
            ToKmInt = 0;
            ToM = "";
            ToMInt = 0;
            ProblemDetail = "";
            Needed = "";
            //Length = "";
            //Width = "";
            //Height = "";
            //LenghtInt = null;
            //WidthInt = null;
            //HeightInt = null;
            Length = null;
            Width = null;
            Height = null;
            WIInt = null;
            WSInt = null;
            WTCInt = null;
            WCInt = null;
            RTInt = null;

            SelectedDate = null;
            SelectedLocation = "";
            //selectedlocationsiteref.Clear();
            Remark = "";
        }
        public ICommand Clear
        {
            get
            {
                return new Command(async (obj) =>
                {
                    var actionResult = await UserDialogs.Instance.ConfirmAsync(" Are you sure want to clear?", "RAMS", "Yes", "No");
                    if (actionResult)
                        ClearData();
                   // await CoreMethods.PopPageModel(false, false);


                });
            }
        }
        public ICommand CategoryCommand
        {
            get
            {
                return new Command(async (obj) =>
                {



                    ///  ClearData();
                });
            }
        }
        public  ICommand LocationDropClicked
        {
            get
            {
                return new Command(async (obj) =>
                {
                    if (!FromView)
                    {
                        var dict = new Dictionary<string, object>();
                        dict.Add("FullItem", LocationItem);

                        var loc = new LocationPage(LocationItem);
                        loc.printStringdelegate += GetItme;
                        await PopupNavigation.Instance.PushAsync(loc, true);
                    }
                });
            }
        }
        void GetItme(List<JAssetDropDownData> jAssetDropDownDatas)
        {
            if (jAssetDropDownDatas != null)
            {
                //if (jAssetDropDownDatas.Count > 0)
                {
                    selectedlocationsiteref = jAssetDropDownDatas;
                    var temp = "";
                    foreach (var objs in jAssetDropDownDatas)
                    {
                        temp = temp + objs.Text + ",";
                    }
                    SelectedLocation = temp;
                }
               
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
    }
}
