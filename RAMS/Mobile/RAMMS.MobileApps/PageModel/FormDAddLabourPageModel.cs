using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;
using RAMMS.DTO;
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
    public class FormDAddLabourPageModel : FreshBasePageModel
    {

        private IRestApi _restApi;

        public IUserDialogs _userDialogs;

        public ILocalDatabase _localDatabase;


        public int? iRet { get; set; }

        public int GetHeaderNoCode { get; set; }

        public FormDLabourDetailsRequestDTO SelectedNewHdrItem { get; set; }

        public FormDLabourDetailsResponseDTO SelectedFormARowItem { get; set; }

        public int iResultValue { get; set; }

        public EditViewModel _editViewModel { get; set; }

        public bool IsEmpty { get; set; }

        public float LstViewHeightRequest { get; set; }

        public ListView FormAGridListview { get; set; }

        public string strLabourValue { get; set; }
        public string laborlabel { get; set; }

        public ObservableCollection<DDListItems> DDLabourListItems { get; set; }

        public ObservableCollection<DDListItems> DDUnitListItems { get; set; }


        public FilteredPagingDefinition<FormDSearchGridDTO> GridItems { get; set; }

        public ObservableCollection<FormDLabourDetailsResponseDTO> MyBaseFormDList { get; set; }

        public string totalsize { get; set; }

        public string pagesize { get; set; }

        public string SelectedLabour { get; set; }

        public string SelectedUnit { get; set; }

        public int? strQty { get; set; }

        public string strRemarksValue { get; set; }

        public int pageno { get; set; }

        public ExtendedPicker labourpick, unitpick;
        public EntryControl enctrlQty, entrllabour;
        public CustomEditor enctrlRemarks;
        public Button btnSave, btnSaveandExit, btnCancel;

        public FormDLabourDetailsRequestDTO SelectedEditView { get; set; }

        public FormDAddLabourPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;

            _restApi = restApi;

            _localDatabase = localDatabase;

            _editViewModel = new EditViewModel();

            DDLabourListItems = new ObservableCollection<DDListItems>();

            DDUnitListItems = new ObservableCollection<DDListItems>();




        }

        public override async void Init(object initData)
        {
            base.Init(initData);

            _editViewModel = initData as EditViewModel;
            if (_editViewModel.Type == "Add")
            {
                laborlabel = "Add Labour";
            }
            if (_editViewModel.Type == "Edit")
            {
                laborlabel = "Edit Labour";
            }
            if (_editViewModel.Type == "View")
            {
                laborlabel = "View Labour";
            }

            labourpick = CurrentPage.FindByName<ExtendedPicker>("labourpicker");

            unitpick = CurrentPage.FindByName<ExtendedPicker>("unitpicker");

            enctrlQty = CurrentPage.FindByName<EntryControl>("enctrlQty");

            enctrlRemarks = CurrentPage.FindByName<CustomEditor>("enctrlRemarks");

            entrllabour = CurrentPage.FindByName<EntryControl>("entrllabour");

            btnSave = CurrentPage.FindByName<Button>("btnSave");

            btnSaveandExit = CurrentPage.FindByName<Button>("btnSaveandExit");

            btnCancel = CurrentPage.FindByName<Button>("btnCancel");

            GetHeaderNoCode = _editViewModel.HdrFahPkRefNo;


        }


        private async Task<int> dropdown()
        {
            try
            {
                await GetLabourList();

                await GetddListDetails("LabourUnit");

                labourpick.ItemsSource = DDLabourListItems.Select((DDListItems arg) => arg.Text).ToList();

                labourpick.SelectedIndexChanged += (s, e) =>
                {
                    if (labourpick.SelectedIndex != -1)
                    {
                        SelectedLabour = DDLabourListItems[labourpick.SelectedIndex].Value.ToString();

                        strLabourValue = DDLabourListItems[labourpick.SelectedIndex].Text.ToString().Split('-')[1];

                        strRemarksValue = strLabourValue;
                    }

                };

                unitpick.ItemsSource = DDUnitListItems.Select((DDListItems arg) => arg.Text).ToList();

                unitpick.SelectedIndexChanged += (s, e) =>
                {
                    if (labourpick.SelectedIndex != -1)
                    {
                        SelectedUnit = DDUnitListItems[unitpick.SelectedIndex].Value.ToString();

                    }

                };



                strQty = Convert.ToInt32(enctrlQty.Text);

                if (strQty == 0)
                {
                    strQty = null;
                }

                strRemarksValue = enctrlRemarks.Text;
            }
            catch
            { }

            return 1;

        }


        public async Task<FormDLabourDetailsRequestDTO> GetLabourHeaderdetails(int HeaderCode)
        {
            try
            {
                //_userDialogs.ShowLoading("loading");

                if (CrossConnectivity.Current.IsConnected)
                {

                    //GetMaterCodeItem = new RoadMasterRequestDTO
                    //{
                    //    RoadCode = HeaderCode
                    //};

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(HeaderCode);

                    var response = await _restApi.FormDLabourGetById(HeaderCode);

                    if (response.success)
                    {
                        SelectedNewHdrItem = response.data;

                        //labourpick.Items.Clear();
                        labourpick.ItemsSource = DDLabourListItems.Select((DDListItems arg) => arg.Text).ToList();
                        int wsindex = DDLabourListItems.ToList().FindIndex(a => a.Value == SelectedNewHdrItem.LabourCode);
                        if (wsindex == -1) { wsindex = 1; }
                        //ws.SelectedIndex = wsindex;                                                
                        labourpick.SelectedIndex = wsindex;

                        SelectedLabour = SelectedNewHdrItem.LabourCode;

                        strLabourValue = SelectedNewHdrItem.CodeDesc;

                        iRet = SelectedNewHdrItem.SerialNo;

                        strQty = SelectedNewHdrItem.Quantity;

                        strRemarksValue = SelectedNewHdrItem.LabourDesc;

                        //unitpick
                        //unitpick.Items.Clear();

                        unitpick.ItemsSource = DDUnitListItems.Select((DDListItems arg) => arg.Text).ToList();

                        int unitindex = DDUnitListItems.ToList().FindIndex(a => a.Text == SelectedNewHdrItem.Unit);

                        if (unitindex == -1) { unitindex = 1; }

                        //ws.SelectedIndex = wsindex;
                        unitpick.SelectedIndex = unitindex;


                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return SelectedNewHdrItem;
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
            return new FormDLabourDetailsRequestDTO();
        }

        public void isEnableControl(bool bValue)
        {

            if (_editViewModel.RoadName == "Add")
            {

                labourpick.IsEnabled = bValue;

                unitpick.IsEnabled = bValue;

                enctrlQty.IsEnabled = bValue;

                enctrlRemarks.IsEnabled = bValue;

                btnCancel.IsEnabled = bValue;

                btnSave.IsEnabled = bValue;

                btnSave.IsVisible = bValue;

                btnSaveandExit.IsEnabled = bValue;

                btnSaveandExit.Text = "Save and Exit";

                btnSaveandExit.IsVisible = bValue;
                _editViewModel.Type = "Edit";

                App.ViewState = false;


            }
            else if (_editViewModel.Type == "Edit")
            {

                labourpick.IsEnabled = true;

                unitpick.IsEnabled = true;

                enctrlQty.IsEnabled = true;

                enctrlRemarks.IsEnabled = true;

                btnCancel.IsEnabled = true;

                btnSave.IsVisible = false;

                btnSaveandExit.IsEnabled = true;

                btnSaveandExit.IsVisible = true;

                btnSaveandExit.Text = "Update and Exit";

                App.ViewState = false;
            }
            else if (_editViewModel.Type == "View")
            {
                labourpick.IsEnabled = bValue;

                unitpick.IsEnabled = bValue;

                enctrlQty.IsEnabled = bValue;

                enctrlRemarks.IsEnabled = bValue;

                btnCancel.IsEnabled = true;

                btnSave.IsEnabled = bValue;

                btnSaveandExit.IsEnabled = bValue;

                btnSaveandExit.IsVisible = false;

                btnSave.IsVisible = false;

                App.ViewState = true;
            }
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

                        if (ddtype == "LabourUnit")
                        {
                            DDUnitListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDUnitListItems;
                        }

                    }
                    else
                        _userDialogs.Toast("Unable to connect please check your internet connection.");

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
                _userDialogs.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }


        public async Task<ObservableCollection<DDListItems>> GetLabourList()
        {
            try
            {
                //_userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.GetFormDLabourCode();

                    if (response.success)
                    {
                        DDLabourListItems = new ObservableCollection<DDListItems>(response.data);
                        return DDLabourListItems;
                    }
                    else
                        _userDialogs.Toast("Unable to connect please check your internet connection.");

                    return DDLabourListItems;
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


        public async Task<int?> GetSerialNo(int HeaderID)
        {

            try
            {
                //_userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.FormDLabourSrNo(HeaderID);

                    if (response.success)
                    {
                        iRet = response.data;

                    }
                    else
                        _userDialogs.Toast("Unable to connect please check your internet connection.");


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
            return iRet;
        }


        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (_editViewModel.RoadName == "Add")
            {
                // DropDownMasterSetup(_editViewModel.Type);

                _editViewModel.HdrFahPkRefNo = _editViewModel.HdrFahPkRefNo;

                GetHeaderNoCode = _editViewModel.HdrFahPkRefNo;

                //await GetSerialNo(GetHeaderNoCode);

                await dropdown();

                isEnableControl(true);


                return;
            }
            else if (_editViewModel.Type == "Edit" || _editViewModel.Type == "View")
            {

                _editViewModel.HdrFahPkRefNo = _editViewModel.HdrFahPkRefNo;

                GetHeaderNoCode = _editViewModel.HdrFahRefNo;

                //MyBaseFormDList = await GetMyFormDLabourListReports("Grid");

                //show details
                

                await dropdown();

                SelectedNewHdrItem = await GetLabourHeaderdetails(GetHeaderNoCode);

                isEnableControl(false);

                return;
            }


        }


        public async Task<ObservableCollection<FormDLabourDetailsResponseDTO>> UpdateLabourHeaderList()
        {
            _userDialogs.ShowLoading("Loading");

            try
            {
                FormDLabourDetailsRequestDTO objRequest = new FormDLabourDetailsRequestDTO();


                if (CrossConnectivity.Current.IsConnected)
                {

                    objRequest = new FormDLabourDetailsRequestDTO()
                    {
                        No = GetHeaderNoCode,
                        FdmdFdhPkRefNo = _editViewModel.HdrFahPkRefNo,
                        SerialNo = iRet,
                        LabourCode = SelectedLabour,
                        LabourDesc = strRemarksValue,
                        Quantity = strQty,
                        Unit = SelectedUnit,
                        CodeDesc = strLabourValue,
                        ActiveYn = true

                    };


                    //var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

                    var response = await _restApi.UpdateFormDLabourHdr(objRequest);

                    if (response.success)
                    {
                        try
                        {
                            _editViewModel.Type = _editViewModel.Type;

                            _editViewModel.HdrFahPkRefNo = _editViewModel.HdrFahPkRefNo;

                            _editViewModel.HdrFahRefNo = response.data;

                            App.FormDDetailCode = response.data;

                            UserDialogs.Instance.Toast("Labour Header Details Updated Successfully.");


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
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");

            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }

            return new ObservableCollection<FormDLabourDetailsResponseDTO>();
        }


        public async Task<ObservableCollection<FormDLabourDetailsResponseDTO>> SaveFormDHeaderList()
        {
            _userDialogs.ShowLoading("Loading");

            try
            {
                FormDLabourDetailsRequestDTO objRequest = new FormDLabourDetailsRequestDTO();




                if (CrossConnectivity.Current.IsConnected)
                {

                    await GetSerialNo(GetHeaderNoCode);

                   objRequest = new FormDLabourDetailsRequestDTO()
                   {
                        FdmdFdhPkRefNo = GetHeaderNoCode,
                        SerialNo = iRet,
                        LabourCode = SelectedLabour,
                        LabourDesc = strRemarksValue,
                        Quantity = strQty,
                        Unit = SelectedUnit,
                        CodeDesc = strLabourValue

                    };


                    //var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

                    var response = await _restApi.SaveFormDLabourHdr(objRequest);

                    if (response.success)
                    {
                        try
                        {
                            _editViewModel.Type = _editViewModel.Type;

                            _editViewModel.HdrFahPkRefNo = _editViewModel.HdrFahPkRefNo;

                            _editViewModel.HdrFahRefNo = response.data;

                            App.FormDDetailCode = response.data;

                            UserDialogs.Instance.Toast("Labour Header Details Saved Successfully.");


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
                    UserDialogs.Instance.Alert("Unable to connect please check your internet connection.");

            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }

            return new ObservableCollection<FormDLabourDetailsResponseDTO>();
        }

        private async void Clear()
        {
            try
            {

                if (labourpick.SelectedIndex != -1)
                {
                    labourpick.SelectedIndex = -1;
                }

                if (unitpick.SelectedIndex != -1)
                {
                    unitpick.SelectedIndex = -1;
                }

                SelectedUnit = null;

                SelectedLabour = "";

                strLabourValue = "";

                strQty = null;

                strRemarksValue = "";

                entrllabour.Text = "";

                enctrlQty.Text = "";

                enctrlRemarks.Text = "";


            }
            catch
            { }

        }

        public ICommand FormASaveCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    try
                    {
                        _userDialogs.ShowLoading("Loading");

                        if (string.IsNullOrEmpty(SelectedLabour))
                        {
                            UserDialogs.Instance.Alert("Please Select Code", "RAMS", "OK");

                            return;

                        }
                        if (string.IsNullOrEmpty(SelectedUnit))
                        {
                            UserDialogs.Instance.Alert("Please Select Unit", "RAMS", "OK");
                            return;
                        }
                        if (strQty == null || strQty == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter Quantity", "RAMS", "OK");
                            return;
                        }
                        if (string.IsNullOrEmpty(strRemarksValue))
                        {
                            UserDialogs.Instance.Alert("Please enter Description.", "RAMS", "OK");
                            return;
                        }



                        var strValue = await SaveFormDHeaderList();

                      
                        Clear();

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


        public ICommand FormASaveExitCommand
        {

            get
            {
                return new Command(async (obj) =>
                {
                    try
                    {
                        _userDialogs.ShowLoading("Loading");


                        if (string.IsNullOrEmpty(SelectedLabour))
                        {
                            UserDialogs.Instance.Alert("Please Select Code", "RAMS", "OK");

                            return;

                        }
                        if (string.IsNullOrEmpty(SelectedUnit))
                        {
                            UserDialogs.Instance.Alert("Please Select Unit", "RAMS", "OK");
                            return;
                        }
                        if (strQty == null || strQty == 0)
                        {
                            UserDialogs.Instance.Alert("Please enter Quantity", "RAMS", "OK");
                            return;
                        }
                        if (string.IsNullOrEmpty(strRemarksValue))
                        {
                            UserDialogs.Instance.Alert("Please enter Description.", "RAMS", "OK");
                            return;
                        }


                        


                        if (_editViewModel.Type != "Edit")
                        {
                            //await GetSerialNo(GetHeaderNoCode);
                            var strSaveValue = SaveFormDHeaderList();
                        }
                        else if (_editViewModel.RoadName == "Add")
                        {
                            _editViewModel.RoadName = "";

                            var strSaveValue = SaveFormDHeaderList();
                            

                        }
                        else
                        {

                            var strUpdateValue = UpdateLabourHeaderList();

                        }

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

        public ICommand CancelFormDDetailsCommand
        {

            get
            {
                return new Command(async (obj) =>
                {
                    if (App.ViewState)
                    {
                        await CurrentPage.Navigation.PopAsync();
                    }
                    else
                    {
                        var actionResult1 = await UserDialogs.Instance.ConfirmAsync("Unsaved changes might be lost. Are you sure you want to cancel?", "RAMS", "Yes", "No");
                        if (actionResult1)
                        {
                            await CurrentPage.Navigation.PopAsync();
                        }
                    }
                });
            }
        }
    }
}
