using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;
using RAMMS.DTO.RequestBO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps.PageModel
{
    public class FormDAddDetailsPageModel : FreshBasePageModel
    {
        private IRestApi _restApi;

        public IUserDialogs _userDialogs;

        public ILocalDatabase _localDatabase;

        public FormDDetailsRequestDTO SelectedNewHdrItem { get; set; }

        public FormDDetailsResponseDTO SelectedFormARowItem { get; set; }

        public int iResultValue { get; set; }

        public int? iRet { get; set; }

        public int iDTL { get; set; }

        public bool IsControlEnabled { get; set; }

        public EditViewModel _editViewModel { get; set; }

        public bool IsEmpty { get; set; }

        public float LstViewHeightRequest { get; set; }

        public ListView FormAGridListview { get; set; }

        public string strLabourValue { get; set; }

        public ObservableCollection<DDListItems> DDSiterefListItems { get; set; }

        public ObservableCollection<DDListItems> DDUnitListItems { get; set; }

        public ObservableCollection<DDListItems> DDSourceTypeListItems { get; set; }


        public ObservableCollection<DDListItems> DDERTWorkListItems { get; set; }

        public ObservableCollection<DDListItems> DDRoadCodeListItems { get; set; }

        public ObservableCollection<DDListItems> DDActivtyListItems { get; set; }

        public ObservableCollection<DDListItems> DDFormXListItems { get; set; }

        public string strSourceCode { get; set; }
        public string SelectedWork { get; set; }

        public string SelectedFormx { get; set; }
        public int? SelectedFormxId { get; set; }

        public string SelectedRoadCode { get; set; }

        public string SelectedActivtiy { get; set; }

        public string SelectedSource { get; set; }

        public string SelectedSiteRef { get; set; }

        public string SelectedUnit { get; set; }

        public int? strLength { get; set; }
        public int? strWidth { get; set; }
        public int? strHeight { get; set; }

        public string strRefNo { get; set; }

        public string strRemarksValue { get; set; }

        public int pageno { get; set; }

        public ExtendedPicker unitpick, siterefpicker, activitypicker, roadpicker, sourcepicker, productionpicker, workpicker, formxpicker;

        public EntryControl enctrlQty, entrlTKM, entrlTM, enctrlactcode, entrlroadcode, entrlsourcecode, enctrllength, enctrlwidth, enctrlheight;

        public EntryControl entrlFKM, entrlFM;

        public CustomEditor enctrlRemarks;

        public Button btnSave, btnSaveandExit, btnCancel, btnuseeuaccident, btnWar;

        TimePicker tArrival, tDeparture;


        string strDepartureTime, strArrivalTime;
        public int? iProductionQty { get; set; }

        public int? iFKM { get; set; }
        public int? iFM { get; set; }
        public int? iTKM { get; set; }
        public int? iTM { get; set; }


        int GetHeaderNoCode { get; set; }

        public FormDAddDetailsPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;

            _restApi = restApi;

            _localDatabase = localDatabase;

            _editViewModel = new EditViewModel();
            DDSiterefListItems = new ObservableCollection<DDListItems>();
            DDUnitListItems = new ObservableCollection<DDListItems>();
            DDSourceTypeListItems = new ObservableCollection<DDListItems>();
            DDERTWorkListItems = new ObservableCollection<DDListItems>();
            DDRoadCodeListItems = new ObservableCollection<DDListItems>();
            DDActivtyListItems = new ObservableCollection<DDListItems>();
            DDFormXListItems = new ObservableCollection<DDListItems>();

        }


        public override async void Init(object initData)
        {
            base.Init(initData);

            _editViewModel = initData as EditViewModel;

            iDTL = 0;

            iRet = _editViewModel.dtlserialNo;

            //App.HeaderCode = _editViewModel.HdrFahPkRefNo;

            siterefpicker = CurrentPage.FindByName<ExtendedPicker>("siterefpicker");

            unitpick = CurrentPage.FindByName<ExtendedPicker>("productionpicker");

            activitypicker = CurrentPage.FindByName<ExtendedPicker>("activitypicker");

            roadpicker = CurrentPage.FindByName<ExtendedPicker>("roadpicker");

            formxpicker = CurrentPage.FindByName<ExtendedPicker>("formxpicker");

            sourcepicker = CurrentPage.FindByName<ExtendedPicker>("sourcepicker");

            workpicker = CurrentPage.FindByName<ExtendedPicker>("workpicker");

            tArrival = CurrentPage.FindByName<TimePicker>("tArrivpick");

            tDeparture = CurrentPage.FindByName<TimePicker>("tDeppick");


            enctrlQty = CurrentPage.FindByName<EntryControl>("enctrlQty");

            enctrlRemarks = CurrentPage.FindByName<CustomEditor>("enctrlRemarks");

            entrlFKM = CurrentPage.FindByName<EntryControl>("entrlFKM");

            entrlFM = CurrentPage.FindByName<EntryControl>("entrlFM");

            entrlTKM = CurrentPage.FindByName<EntryControl>("entrlTKM");

            entrlTM = CurrentPage.FindByName<EntryControl>("entrlTM");

            entrlroadcode = CurrentPage.FindByName<EntryControl>("entrlroadcode");

            enctrlactcode = CurrentPage.FindByName<EntryControl>("enctrlactcode");

            entrlsourcecode = CurrentPage.FindByName<EntryControl>("entrlsourcecode");

            enctrllength = CurrentPage.FindByName<EntryControl>("enctrllength");

            enctrlwidth = CurrentPage.FindByName<EntryControl>("enctrlwidth");

            enctrlheight = CurrentPage.FindByName<EntryControl>("enctrlheight");


            btnSave = CurrentPage.FindByName<Button>("btnSave");

            btnSaveandExit = CurrentPage.FindByName<Button>("btnSaveandExit");

            btnCancel = CurrentPage.FindByName<Button>("btnCancel");

            btnuseeuaccident = CurrentPage.FindByName<Button>("btnuseeuaccident");

            btnWar = CurrentPage.FindByName<Button>("btnWar");

            GetHeaderNoCode = _editViewModel.HdrFahPkRefNo;

            strRefNo = _editViewModel.Rmu;

        }


        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (_editViewModel.RoadName == "Add")
            {
                // DropDownMasterSetup(_editViewModel.Type);

                _editViewModel.HdrFahPkRefNo = _editViewModel.HdrFahPkRefNo;

                GetHeaderNoCode = _editViewModel.HdrFahPkRefNo;

                iDTL = 0;

                await dropdown();

                isEnableControl(true);

                return;
            }
            else if (_editViewModel.Type == "Edit" || _editViewModel.Type == "View")
            {

                _editViewModel.HdrFahPkRefNo = _editViewModel.HdrFahPkRefNo;

                GetHeaderNoCode = _editViewModel.HdrFahRefNo;

                App.FormDDetailCode = GetHeaderNoCode;

                //MyBaseFormDList = await GetMyFormDLabourListReports("Grid");

                //show details

                await dropdown();

                SelectedNewHdrItem = await GetdetailList(GetHeaderNoCode);

                isEnableControl(false);

                return;
            }


        }

        private async Task<int> dropdown()
        {
            try
            {
                //await GetLabourList();

                await GetddListDetails("Site Ref");

                await GetddListDetails("Unit");

                await GetddListDetails("Source Type");

                await GetddListDetails("ERTWorkStatus");

                await GetRoadListDetails();

                await GetActivtyCodeDetails("ACT-BR");

                unitpick.ItemsSource = DDUnitListItems.Select((DDListItems arg) => arg.Text).ToList();

                unitpick.SelectedIndexChanged += (s, e) =>
                {
                    if (unitpick.SelectedIndex != -1)
                    {
                        SelectedUnit = DDUnitListItems[unitpick.SelectedIndex].Value.ToString();

                    }

                };

                siterefpicker.ItemsSource = DDSiterefListItems.Select((DDListItems arg) => arg.Text).ToList();

                siterefpicker.SelectedIndexChanged += (s, e) =>
                {
                    if (siterefpicker.SelectedIndex != -1)
                    {
                        SelectedSiteRef = DDSiterefListItems[siterefpicker.SelectedIndex].Value.ToString();

                    }

                };


                sourcepicker.ItemsSource = DDSourceTypeListItems.Select((DDListItems arg) => arg.Text).ToList();

                sourcepicker.SelectedIndexChanged += (s, e) =>
                {
                    if (sourcepicker.SelectedIndex != -1)
                    {
                        SelectedSource = DDSourceTypeListItems[sourcepicker.SelectedIndex].Value.ToString();
                        if (SelectedSource == "Form X")
                        {
                            formxpicker.IsVisible = true;
                            entrlsourcecode.IsVisible = false;
                        }
                        else
                        {
                            formxpicker.IsVisible = false;
                            entrlsourcecode.IsVisible = true;

                        }

                    }

                };


                workpicker.ItemsSource = DDERTWorkListItems.Select((DDListItems arg) => arg.Text).ToList();

                workpicker.SelectedIndexChanged += (s, e) =>
                {
                    if (workpicker.SelectedIndex != -1)
                    {
                        SelectedWork = DDERTWorkListItems[workpicker.SelectedIndex].Value.ToString();

                    }

                };


                roadpicker.ItemsSource = DDRoadCodeListItems.Select((DDListItems arg) => arg.Text).ToList();

                roadpicker.SelectedIndexChanged += async (s, e) =>
                {
                    if (roadpicker.SelectedIndex != -1)
                    {
                        SelectedRoadCode = DDRoadCodeListItems[roadpicker.SelectedIndex].Value.ToString();
                        entrlroadcode.Text = DDRoadCodeListItems[roadpicker.SelectedIndex].Text.ToString().Split('-')[1];

                        await GetFormXDetails(SelectedRoadCode.ToString());
                    }

                };

                activitypicker.ItemsSource = DDActivtyListItems.Select((DDListItems arg) => arg.Text).ToList();

                activitypicker.SelectedIndexChanged += (s, e) =>
                {
                    if (activitypicker.SelectedIndex != -1)
                    {
                        SelectedActivtiy = DDActivtyListItems[activitypicker.SelectedIndex].Value.ToString();

                        enctrlactcode.Text = DDActivtyListItems[activitypicker.SelectedIndex].Text.ToString().Split('-')[1];
                    }

                };



                formxpicker.SelectedIndexChanged += (s, e) =>
                {
                    if (formxpicker.SelectedIndex != -1)
                    {
                        SelectedFormx = DDFormXListItems[formxpicker.SelectedIndex].Value.ToString();

                    }

                };

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return 1;
        }



        public async Task<ObservableCollection<DDListItems>> GetFormXDetails(string roadCode)
        {
            try
            {
                _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    //var ddlist = new DDLookUpDTO()
                    //{
                    //    Type = roadCode,
                    //};
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(roadCode);

                    var response = await _restApi.GetFormXRefForFormD(roadCode);

                    if (response.success)
                    {

                        DDFormXListItems = new ObservableCollection<DDListItems>(response.data);
                        formxpicker.ItemsSource = DDFormXListItems.Select((DDListItems arg) => arg.Text).ToList();
                        return DDFormXListItems;
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
            return new ObservableCollection<DDListItems>();
        }


        public async Task<ObservableCollection<DDListItems>> GetActivtyCodeDetails(string ddtype)
        {
            try
            {
                _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    var ddlist = new DDLookUpDTO()
                    {
                        Type = ddtype,
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);
                    var response = await _restApi.GetErtActivityCode();
                    if (response.success)
                    {

                        DDActivtyListItems = new ObservableCollection<DDListItems>(response.data);
                        return DDActivtyListItems;

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
            return new ObservableCollection<DDListItems>();
        }



        public async Task<FormDDetailsRequestDTO> GetdetailList(int HeaderCode)
        {
            try
            {
                _userDialogs.ShowLoading("loading");

                if (CrossConnectivity.Current.IsConnected)
                {

                    //GetMaterCodeItem = new RoadMasterRequestDTO
                    //{
                    //    RoadCode = HeaderCode
                    //};

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(HeaderCode);

                    var response = await _restApi.FormDDtlGetById(HeaderCode);

                    if (response.success)
                    {
                        SelectedNewHdrItem = response.data;
                        //unitpick.Items.Clear();
                        unitpick.ItemsSource = DDUnitListItems.Select((DDListItems arg) => arg.Text).ToList();
                        int unitindex = DDUnitListItems.ToList().FindIndex(a => a.Value == SelectedNewHdrItem.ProdUnit);
                        unitpick.SelectedIndex = unitindex;

                        //siterefpicker.Items.Clear();
                        siterefpicker.ItemsSource = DDSiterefListItems.Select((DDListItems arg) => arg.Text).ToList();

                        int Siterefindex = DDSiterefListItems.ToList().FindIndex(a => a.Value == SelectedNewHdrItem.SiteRef);
                        siterefpicker.SelectedIndex = Siterefindex;

                        //sourcepicker.Items.Clear();
                        sourcepicker.ItemsSource = DDSourceTypeListItems.Select((DDListItems arg) => arg.Text).ToList();
                        int sourceindex = DDSourceTypeListItems.ToList().FindIndex(a => a.Value == SelectedNewHdrItem.SourceType);
                        sourcepicker.SelectedIndex = sourceindex;

                        //workpicker.Items.Clear();
                        workpicker.ItemsSource = DDERTWorkListItems.Select((DDListItems arg) => arg.Text).ToList();
                        int ERTWorkindex = DDERTWorkListItems.ToList().FindIndex(a => a.Value == SelectedNewHdrItem.WorkSts);
                        workpicker.SelectedIndex = ERTWorkindex;

                        //roadpicker.Items.Clear();
                        roadpicker.ItemsSource = DDRoadCodeListItems.Select((DDListItems arg) => arg.Text).ToList();
                        int RoadCodeIndex = DDRoadCodeListItems.ToList().FindIndex(a => a.Value == SelectedNewHdrItem.RoadCode);
                        roadpicker.SelectedIndex = RoadCodeIndex;
                        SelectedRoadCode = DDRoadCodeListItems[roadpicker.SelectedIndex].Value.ToString();
                        await GetFormXDetails(SelectedRoadCode.ToString());


                        //formxpicker.Items.Clear();
                        formxpicker.ItemsSource = DDFormXListItems.Select((DDListItems arg) => arg.Text).ToList();
                        if (SelectedNewHdrItem.FormXPKRefNo != null)
                        {
                            int FIndex = DDFormXListItems.ToList().FindIndex(a => a.Value == SelectedNewHdrItem.FormXPKRefNo.ToString());
                            formxpicker.SelectedIndex = FIndex;
                        }


                        //activitypicker.Items.Clear();
                        activitypicker.ItemsSource = DDActivtyListItems.Select((DDListItems arg) => arg.Text).ToList();
                        int ActIndex = DDActivtyListItems.ToList().FindIndex(a => a.Value == SelectedNewHdrItem.ActCode.ToString());
                        activitypicker.SelectedIndex = ActIndex;

                        strRefNo = SelectedNewHdrItem.ReferenceID;

                        strArrivalTime = SelectedNewHdrItem.TimeArr;

                        tArrival.Time = Convert.ToDateTime(SelectedNewHdrItem.TimeArr).TimeOfDay;

                        strDepartureTime = SelectedNewHdrItem.TimeDep;

                        tDeparture.Time = Convert.ToDateTime(SelectedNewHdrItem.TimeDep).TimeOfDay;

                        strHeight = SelectedNewHdrItem.Height;

                        strLength = SelectedNewHdrItem.Length;

                        strWidth = SelectedNewHdrItem.Width;

                        iProductionQty = SelectedNewHdrItem.ProdQty;

                        enctrlQty.Text = SelectedNewHdrItem.ProdQty.ToString();

                        SelectedUnit = SelectedNewHdrItem.ProdUnit;

                        strSourceCode = SelectedNewHdrItem.SourceRefID;

                        iFKM = SelectedNewHdrItem.FrmCh;

                        iFM = SelectedNewHdrItem.FrmChDeci;

                        iTKM = SelectedNewHdrItem.ToCh;

                        iTM = SelectedNewHdrItem.ToChDeci;

                        strRemarksValue = SelectedNewHdrItem.Remarks;

                        //int wsindex = DDLabourListItems.ToList().FindIndex(a => a.Value == SelectedNewHdrItem.EquipmentCode);
                        //if (wsindex == -1) { wsindex = 1; }
                        ////ws.SelectedIndex = wsindex;                                                
                        //labourpick.SelectedIndex = wsindex;

                        //SelectedLabour = SelectedNewHdrItem.EquipmentCode;

                        //strLabourValue = SelectedNewHdrItem.CodeDesc;

                        //strQty = SelectedNewHdrItem.Quantity;

                        //strRemarksValue = SelectedNewHdrItem.EquipmentDesc;

                        ////unitpick
                        //int unitindex = DDUnitListItems.ToList().FindIndex(a => a.Text == SelectedNewHdrItem.Unit);

                        //if (unitindex == -1) { unitindex = 1; }

                        ////ws.SelectedIndex = wsindex;
                        //unitpick.SelectedIndex = unitindex;
                    }
                    else
                    {
                        _userDialogs.Toast("Unable to connect please check your internet connection.");
                        return null;
                    }

                    //return SelectedNewHdrItem;
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
            return new FormDDetailsRequestDTO();
        }

        public void isEnableControl(bool bValue)
        {

            if (_editViewModel.RoadName == "Add")
            {
                IsControlEnabled = true;

                //siterefpicker.IsEnabled = true;
                //unitpick.IsEnabled = true;
                //activitypicker.IsEnabled = true;
                //roadpicker.IsEnabled = true;
                //sourcepicker.IsEnabled = true;
                //workpicker.IsEnabled = true;
                //enctrlQty.IsEnabled = true;
                //enctrlRemarks.IsEnabled = true;
                //entrlFKM.IsEnabled = true;
                //entrlFM.IsEnabled = true;
                //entrlTKM.IsEnabled = true;
                //entrlTM.IsEnabled = true;
                //entrlroadcode.IsEnabled = true;
                //entrlsourcecode.IsEnabled = true;
                //enctrllength.IsEnabled = true;
                //enctrlwidth.IsEnabled = true;
                //enctrlheight.IsEnabled = true;

                btnSave.IsEnabled = true;
                btnCancel.IsEnabled = true;
                btnSave.IsVisible = true;
                App.ViewState = false;
                formxpicker.IsEnabled = true;
                entrlsourcecode.IsEnabled = true;

                btnuseeuaccident.IsEnabled = false;
                btnWar.IsEnabled = false;

                btnSaveandExit.Text = "Save and Exit";
                btnSaveandExit.IsVisible = bValue;


            }
            else if (_editViewModel.Type == "Edit")
            {
                IsControlEnabled = true;
                //siterefpicker.IsEnabled = true;
                //unitpick.IsEnabled = true;
                //activitypicker.IsEnabled = true;
                //roadpicker.IsEnabled = true;
                //sourcepicker.IsEnabled = true;
                //workpicker.IsEnabled = true;
                //enctrlQty.IsEnabled = true;
                //enctrlRemarks.IsEnabled = true;
                //entrlFKM.IsEnabled = true;
                //entrlFM.IsEnabled = true;
                //entrlTKM.IsEnabled = true;
                //entrlTM.IsEnabled = true;
                //entrlroadcode.IsEnabled = true;
                //// entrlsourcecode.IsEnabled = true;
                //enctrllength.IsEnabled = true;
                //enctrlwidth.IsEnabled = true;
                //enctrlheight.IsEnabled = true;

                btnuseeuaccident.IsEnabled = true;
                btnWar.IsEnabled = true;
                btnCancel.IsEnabled = true;
                formxpicker.IsEnabled = true;
                entrlsourcecode.IsEnabled = true;

                btnSave.IsVisible = false;
                btnSaveandExit.IsEnabled = true;
                App.ViewState = false;
                btnSaveandExit.IsVisible = true;
                btnSaveandExit.Text = "Update and Exit";


            }
            else if (_editViewModel.Type == "View")
            {
                IsControlEnabled = false;
                //siterefpicker.IsEnabled = bValue;
                //unitpick.IsEnabled = bValue;
                //activitypicker.IsEnabled = bValue;
                //roadpicker.IsEnabled = bValue;
                //sourcepicker.IsEnabled = bValue;
                //workpicker.IsEnabled = bValue;
                //enctrlQty.IsEnabled = bValue;
                //enctrlRemarks.IsEnabled = bValue;
                //entrlFKM.IsEnabled = bValue;
                //entrlFM.IsEnabled = bValue;
                //entrlTKM.IsEnabled = bValue;
                //entrlTM.IsEnabled = bValue;
                //entrlroadcode.IsEnabled = bValue;
                ////entrlsourcecode.IsEnabled = bValue;
                //enctrllength.IsEnabled = bValue;
                //enctrlwidth.IsEnabled = bValue;
                //enctrlheight.IsEnabled = bValue;

                App.ViewState = true;
                btnCancel.IsEnabled = true;
                btnSave.IsEnabled = bValue;
                btnSaveandExit.IsEnabled = bValue;
                btnSaveandExit.IsVisible = false;
                btnSave.IsVisible = false;
                formxpicker.IsEnabled = false;
                entrlsourcecode.IsEnabled = false;

                btnuseeuaccident.IsEnabled = true;
                btnWar.IsEnabled = true;
            }
        }


        public int Clear()
        {

            if (siterefpicker.SelectedIndex != -1)
            {
                siterefpicker.SelectedIndex = -1;
            }
            //siterefpicker.IsEnabled = true;

            if (unitpick.SelectedIndex != -1)
            {
                unitpick.SelectedIndex = -1;
            }

            if (activitypicker.SelectedIndex != -1)
            {
                activitypicker.SelectedIndex = -1;
            }

            if (sourcepicker.SelectedIndex != -1)
            {
                sourcepicker.SelectedIndex = -1;
            }

            if (workpicker.SelectedIndex != -1)
            {
                workpicker.SelectedIndex = -1;
            }

            if (formxpicker.SelectedIndex != -1)
            {
                formxpicker.SelectedIndex = -1;
            }

            if (roadpicker.SelectedIndex != -1)
            {
                roadpicker.SelectedIndex = -1;
            }

            tArrival = null;
            tArrival = new TimePicker();

            tDeparture = null;
            tDeparture = new TimePicker();

            enctrlactcode.Text = string.Empty;

            enctrlQty.Text = string.Empty;

            enctrlRemarks.Text = string.Empty;

            entrlFKM.Text = string.Empty;

            entrlFM.Text = string.Empty;

            entrlTKM.Text = string.Empty;

            entrlTM.Text = string.Empty;

            entrlroadcode.Text = string.Empty;

            entrlsourcecode.Text = string.Empty;

            enctrllength.Text = string.Empty;

            enctrlwidth.Text = string.Empty;

            enctrlheight.Text = string.Empty;

            //unitpick.IsEnabled = true;
            //activitypicker.IsEnabled = true;
            //roadpicker.IsEnabled = true;
            //sourcepicker.IsEnabled = true;
            //workpicker.IsEnabled = true;
            //formxpicker.

            //enctrlQty.IsEnabled = true;
            //enctrlRemarks.IsEnabled = true;
            //entrlFKM.IsEnabled = true;
            //entrlFM.IsEnabled = true;
            //entrlTKM.IsEnabled = true;
            //entrlTM.IsEnabled = true;
            //entrlroadcode.IsEnabled = true;
            //// entrlsourcecode.IsEnabled = true;
            //enctrllength.IsEnabled = true;
            //enctrlwidth.IsEnabled = true;
            //enctrlheight.IsEnabled = true;

            //btnCancel.IsEnabled = true;
            //btnSave.IsVisible = false;
            //btnSaveandExit.IsEnabled = true;
            //btnSaveandExit.IsVisible = true;
            //btnSaveandExit.Text = "Update and Exit";


            return 1;

        }



        public async Task<ObservableCollection<DDListItems>> GetddListDetails(string ddtype)
        {
            try
            {
                //  _userDialogs.ShowLoading("Loading");
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

                        if (ddtype == "Unit")
                        {
                            DDUnitListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDUnitListItems;
                        }
                        else if (ddtype == "Site Ref")
                        {
                            DDSiterefListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDSiterefListItems;
                        }
                        else if (ddtype == "Source Type")
                        {
                            DDSourceTypeListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDSourceTypeListItems;
                        }
                        else if (ddtype == "ERTWorkStatus")
                        {
                            DDERTWorkListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDERTWorkListItems;
                        }



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
            return new ObservableCollection<DDListItems>();
        }

        public async Task<ObservableCollection<DDListItems>> GetRoadListDetails()
        {
            try
            {
                //  _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {

                    //var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);

                    var response = await _restApi.RodeCodeBySection(_editViewModel.Section);

                    if (response.success)
                    {
                        DDRoadCodeListItems = new ObservableCollection<DDListItems>(response.data);
                        return DDRoadCodeListItems;
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
            return new ObservableCollection<DDListItems>();
        }





        //public async Task<ObservableCollection<DDListItems>> GetEquimentList()
        //{
        //    try
        //    {
        //        //_userDialogs.ShowLoading("Loading");

        //        if (CrossConnectivity.Current.IsConnected)
        //        {
        //            var response = await _restApi.GetFormDEqpCode();

        //            if (response.success)
        //            {
        //                DDLabourListItems = new ObservableCollection<DDListItems>(response.data);
        //                return DDLabourListItems;
        //            }
        //            else
        //                _userDialogs.Toast(response.errorMessage);

        //            return DDLabourListItems;
        //        }
        //        else
        //            UserDialogs.Instance.Alert("Please check your Internet Connection !");
        //    }
        //    catch (Exception ex)
        //    {
        //        _userDialogs.Alert(ex.Message);
        //    }
        //    finally
        //    {
        //        //_userDialogs.HideLoading();
        //    }
        //    return new ObservableCollection<DDListItems>();
        //}



        public async Task<ObservableCollection<FormDDetailsRequestDTO>> UpdateDetailHeaderList()
        {
            _userDialogs.ShowLoading("Loading");

            try
            {
                FormDDetailsRequestDTO objRequest = new FormDDetailsRequestDTO();

                if (iDTL != 0)
                {
                    GetHeaderNoCode = iDTL;

                }
                if (SelectedFormx != null)
                    SelectedFormxId = Convert.ToInt32(SelectedFormx);

                if (CrossConnectivity.Current.IsConnected)
                {

                    objRequest = new FormDDetailsRequestDTO()
                    {
                        No = GetHeaderNoCode,

                        FormDHeaderNo = _editViewModel.HdrFahPkRefNo,

                        RoadCode = SelectedRoadCode,

                        SrNo = iRet,

                        Remarks = strRemarksValue,

                        FrmCh = iFKM,

                        FrmChDeci = iFM,

                        ToCh = iTKM,

                        ToChDeci = iTM,

                        FormXPKRefNo = SelectedFormxId,

                        SourceType = SelectedSource,

                        TimeArr = strArrivalTime,

                        TimeDep = strDepartureTime,

                        ProdUnit = SelectedUnit,

                        ProdQty = iProductionQty,

                        Height = strHeight,

                        Length = strLength,

                        Width = strWidth,

                        WorkSts = SelectedWork,

                        Unit = SelectedUnit,

                        SiteRef = SelectedSiteRef,

                        ReferenceID = strRefNo,

                        SourceRefID = strSourceCode,

                        ActCode = ConvertToNullableInt(SelectedActivtiy),

                        ActiveYn = true

                    };


                    //var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

                    var response = await _restApi.UpdateFormDDtl(objRequest);

                    if (response.success)
                    {
                        try
                        {
                            _editViewModel.Type = _editViewModel.Type;

                            _editViewModel.HdrFahPkRefNo = _editViewModel.HdrFahPkRefNo;

                            _editViewModel.HdrFahRefNo = response.data;

                            App.FormDDetailCode = response.data;

                            UserDialogs.Instance.Toast("Detail Updated Successfully.");


                        }
                        catch (Exception ex)
                        {
                            //_userDialogs.Alert(ex.Message);
                            _userDialogs.HideLoading();

                            //UserDialogs.Instance.Alert("Header Details Saved Successfully.");

                        }

                        //return DetailFromAHdrGridListItems;
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

            return new ObservableCollection<FormDDetailsRequestDTO>();
        }


        public async Task<ObservableCollection<FormDDetailsRequestDTO>> SaveFormDHeaderList()
        {
            _userDialogs.ShowLoading("Loading");

            try
            {
                FormDDetailsRequestDTO objRequest = new FormDDetailsRequestDTO();

                if (SelectedFormx != null)
                    SelectedFormxId = Convert.ToInt32(SelectedFormx);

                if (CrossConnectivity.Current.IsConnected)
                {

                    objRequest = new FormDDetailsRequestDTO()
                    {


                        No = iDTL,

                        FormDHeaderNo = _editViewModel.HdrFahPkRefNo,

                        RoadCode = SelectedRoadCode,

                        SrNo = iRet,

                        Remarks = strRemarksValue,

                        FrmCh = iFKM,

                        FrmChDeci = iFM,

                        ToCh = iTKM,

                        ToChDeci = iTM,

                        FormXPKRefNo = SelectedFormxId,

                        SourceType = SelectedSource,

                        TimeArr = strArrivalTime,

                        TimeDep = strDepartureTime,

                        ProdUnit = SelectedUnit,

                        ProdQty = iProductionQty,

                        Height = strHeight,

                        Length = strLength,

                        Width = strWidth,

                        Unit = SelectedUnit,

                        ActiveYn = true,

                        WorkSts = SelectedWork,

                        SiteRef = SelectedSiteRef,

                        SourceRefID = strSourceCode,

                        ReferenceID = strRefNo,

                        ActCode = ConvertToNullableInt(SelectedActivtiy)


                    };


                    //var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

                    var response = await _restApi.SaveFormDDtl(objRequest);

                    if (response.success)
                    {
                        try
                        {
                            _editViewModel.Type = _editViewModel.Type;

                            _editViewModel.HdrFahPkRefNo = _editViewModel.HdrFahPkRefNo;

                            _editViewModel.HdrFahRefNo = response.data;

                            App.FormDDetailCode = response.data;

                            iDTL = response.data;

                            UserDialogs.Instance.Toast("Detail Data Saved Successfully.");


                        }
                        catch (Exception ex)
                        {
                            //_userDialogs.Alert(ex.Message);
                            _userDialogs.HideLoading();

                            //UserDialogs.Instance.Alert("Header Details Saved Successfully.");

                        }

                        //return DetailFromAHdrGridListItems;
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

            return new ObservableCollection<FormDDetailsRequestDTO>();
        }

        ////private async void Clear()
        ////{
        ////    try
        ////    {

        ////        if (labourpick.SelectedIndex != -1)
        ////        {
        ////            labourpick.SelectedIndex = -1;
        ////        }

        ////        if (unitpick.SelectedIndex != -1)
        ////        {
        ////            unitpick.SelectedIndex = -1;
        ////        }


        ////        SelectedLabour = "";

        ////        strLabourValue = "";

        ////        strQty = null;

        ////        strRemarksValue = "";

        ////        entrllabour.Text = "";

        ////        enctrlQty.Text = "";

        ////        enctrlRemarks.Text = "";


        ////    }
        ////    catch
        ////    { }

        ////}


        public async Task<int?> GetSerialNo(int HeaderID)
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
            return iRet;
        }


        public ICommand FormDDetailSaveCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    try
                    {
                        _userDialogs.ShowLoading("Loading");


                        strArrivalTime = tArrival.Time.ToString();

                        strDepartureTime = tDeparture.Time.ToString();



                        iTKM = ConvertToNullableInt(entrlTKM.Text);

                        iTM = ConvertToNullableInt(entrlTM.Text);

                        iFKM = ConvertToNullableInt(entrlFKM.Text);

                        iFM = ConvertToNullableInt(entrlFM.Text);

                        iProductionQty = ConvertToNullableInt(enctrlQty.Text);


                        if (iFKM == null || iFKM == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter Chainage From KM.", "RAMS", "OK");
                            return;
                        }
                        if (iFM == null || iFM == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter Chainage From M.", "RAMS", "OK");
                            return;
                        }
                        if (iTKM == null || iTKM == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter Chainage To KM.", "RAMS", "OK");
                            return;
                        }
                        if (iTM == null || iTM == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter Chainage To M.", "RAMS", "OK");
                            return;
                        }


                        if (string.IsNullOrEmpty(SelectedRoadCode))
                        {
                            UserDialogs.Instance.Alert("Please select road code.", "RAMS", "OK");
                            return;
                        }


                        if (string.IsNullOrEmpty(strRemarksValue))
                        {
                            UserDialogs.Instance.Alert("Please enter Description.", "RAMS", "OK");
                            return;
                        }


                        if (string.IsNullOrEmpty(SelectedSource))
                        {
                            UserDialogs.Instance.Alert("Please select source.", "RAMS", "OK");
                            return;
                        }


                        if (string.IsNullOrEmpty(strArrivalTime))
                        {
                            UserDialogs.Instance.Alert("Please enter arrival time.", "RAMS", "OK");
                            return;
                        }


                        if (string.IsNullOrEmpty(strDepartureTime))
                        {
                            UserDialogs.Instance.Alert("Please enter departure time.", "RAMS", "OK");
                            return;
                        }


                        if (string.IsNullOrEmpty(SelectedUnit))
                        {
                            UserDialogs.Instance.Alert("Please select Production unit.", "RAMS", "OK");
                            return;
                        }


                        if (iProductionQty == null && iProductionQty == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter Production Quality.", "RAMS", "OK");
                            return;
                        }


                        if (strHeight == null && strHeight == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter height.", "RAMS", "OK");
                            return;
                        }

                        if (strLength == null && strLength == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter length.", "RAMS", "OK");
                            return;
                        }


                        if (strWidth == null && strWidth == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter width.", "RAMS", "OK");
                            return;
                        }

                        if (string.IsNullOrEmpty(SelectedWork))
                        {
                            UserDialogs.Instance.Alert("Please select Work Status.", "RAMS", "OK");
                            return;
                        }


                        if (string.IsNullOrEmpty(SelectedSiteRef))
                        {
                            UserDialogs.Instance.Alert("Please select site ref.", "RAMS", "OK");
                            return;
                        }


                        if (string.IsNullOrEmpty(SelectedActivtiy))
                        {
                            UserDialogs.Instance.Alert("Please select Activity Code.", "RAMS", "OK");
                            return;
                        }


                        if (iDTL == 0)
                        {
                            var strValue = SaveFormDHeaderList();
                        }
                        else
                        {

                            var strUpdateValue = UpdateDetailHeaderList();
                        }

                        //strRefNo = strRefNo;

                        btnuseeuaccident.IsEnabled = true;

                        btnWar.IsEnabled = true;
                        _editViewModel.RoadName = "";

                        //int i = await Clear();

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


        public ICommand FormDDetailSaveExitCommand
        {

            get
            {
                return new Command(async (obj) =>
                {
                    try
                    {
                        _userDialogs.ShowLoading("Loading");


                        strArrivalTime = tArrival.Time.ToString();

                        strDepartureTime = tDeparture.Time.ToString();

                        iTKM = ConvertToNullableInt(entrlTKM.Text);

                        iTM = ConvertToNullableInt(entrlTM.Text);

                        iFKM = ConvertToNullableInt(entrlFKM.Text);

                        iFM = ConvertToNullableInt(entrlFM.Text);

                        iProductionQty = ConvertToNullableInt(enctrlQty.Text);


                        if (iFKM == null || iFKM == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter Chainage From KM.", "RAMS", "OK");
                            return;
                        }
                        if (iFM == null || iFM == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter Chainage From M.", "RAMS", "OK");
                            return;
                        }
                        if (iTKM == null || iTKM == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter Chainage To KM.", "RAMS", "OK");
                            return;
                        }
                        if (iTM == null || iTM == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter Chainage To M.", "RAMS", "OK");
                            return;
                        }


                        if (string.IsNullOrEmpty(SelectedRoadCode))
                        {
                            UserDialogs.Instance.Alert("Please select road code.", "RAMS", "OK");
                            return;
                        }


                        if (string.IsNullOrEmpty(strRemarksValue))
                        {
                            UserDialogs.Instance.Alert("Please enter Description.", "RAMS", "OK");
                            return;
                        }


                        if (string.IsNullOrEmpty(SelectedSource))
                        {
                            UserDialogs.Instance.Alert("Please select source.", "RAMS", "OK");
                            return;
                        }


                        if (string.IsNullOrEmpty(strArrivalTime))
                        {
                            UserDialogs.Instance.Alert("Please enter arrival time.", "RAMS", "OK");
                            return;
                        }


                        if (string.IsNullOrEmpty(strDepartureTime))
                        {
                            UserDialogs.Instance.Alert("Please enter departure time.", "RAMS", "OK");
                            return;
                        }


                        if (string.IsNullOrEmpty(SelectedUnit))
                        {
                            UserDialogs.Instance.Alert("Please select Production unit.", "RAMS", "OK");
                            return;
                        }


                        if (iProductionQty == null && iProductionQty == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter Production Quality.", "RAMS", "OK");
                            return;
                        }


                        if (strHeight == null && strHeight == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter height.", "RAMS", "OK");
                            return;
                        }

                        if (strLength == null && strLength == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter length.", "RAMS", "OK");
                            return;
                        }


                        if (strWidth == null && strWidth == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter width.", "RAMS", "OK");
                            return;
                        }

                        if (string.IsNullOrEmpty(SelectedWork))
                        {
                            UserDialogs.Instance.Alert("Please select Work Status.", "RAMS", "OK");
                            return;
                        }


                        if (string.IsNullOrEmpty(SelectedSiteRef))
                        {
                            UserDialogs.Instance.Alert("Please select site ref.", "RAMS", "OK");
                            return;
                        }


                        if (string.IsNullOrEmpty(SelectedActivtiy))
                        {
                            UserDialogs.Instance.Alert("Please select Activity Code.", "RAMS", "OK");
                            return;
                        }

                        if (iDTL == 0)
                        {
                            if (_editViewModel.Type != "Edit")
                            {

                                var strSaveValue = SaveFormDHeaderList();
                            }
                            else if (_editViewModel.RoadName == "Add")
                            {
                                _editViewModel.RoadName = "";
                                var strSaveValue = SaveFormDHeaderList();

                            }
                            else
                            {

                                var strUpdateValue = UpdateDetailHeaderList();

                            }
                        }
                        else
                        {
                            var strUpdateValue = UpdateDetailHeaderList();
                        }

                        btnuseeuaccident.IsEnabled = true;

                        btnWar.IsEnabled = true;


                        await CurrentPage.Navigation.PopAsync();


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

        public ICommand CloseDetailCommand
        {

            get
            {
                return new Command(async (obj) =>
                {
                    if (_editViewModel.Type != "View")
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


                });
            }
        }

        private int? ConvertToNullableInt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return Convert.ToInt32(value);
        }


    }


}
