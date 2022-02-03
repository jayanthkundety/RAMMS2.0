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
    public class FormDAddMaterialPageModel : FreshBasePageModel
    {
        private IRestApi _restApi;

        public IUserDialogs _userDialogs;

        public ILocalDatabase _localDatabase;

        public int? iRet { get; set; }
        public int GetHeaderNoCode { get; set; }

        public FormDMaterialDetailsRequestDTO SelectedNewHdrItem { get; set; }

        public FormDMaterialDetailsResponseDTO SelectedFormARowItem { get; set; }

        public int iResultValue { get; set; }

        public EditViewModel _editViewModel { get; set; }

        public bool IsEmpty { get; set; }

        public float LstViewHeightRequest { get; set; }

        public ListView FormAGridListview { get; set; }

        public string strLabourValue { get; set; }
        public string materiallabel { get; set; }

        public ObservableCollection<DDListItems> DDLabourListItems { get; set; }

        public ObservableCollection<DDListItems> DDUnitListItems { get; set; }


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



        public FormDAddMaterialPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;

            _restApi = restApi;

            _localDatabase = localDatabase;

            _editViewModel = new EditViewModel();

            DDLabourListItems = new ObservableCollection<DDListItems>();

            DDUnitListItems = new ObservableCollection<DDListItems>();

        }


        public async Task<int?> GetSerialNo(int HeaderID)
        {

            try
            {
                //_userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.FormDMaterialSrNo(HeaderID);

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


        public override async void Init(object initData)
        {
            base.Init(initData);
            _editViewModel = initData as EditViewModel;

            if (_editViewModel.Type == "Add")
            {
                materiallabel = "Add Material";
            }
            if (_editViewModel.Type == "Edit")
            {
                materiallabel = "Edit Material";
            }
            if (_editViewModel.Type == "View")
            {
                materiallabel = "View Material";
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
                await GetMaterialList();

                await GetddListDetails("MaterialUnit");

                labourpick.ItemsSource = DDLabourListItems.Select((DDListItems arg) => arg.Text).ToList();

                labourpick.SelectedIndexChanged += (s, e) =>
                {
                    if (labourpick.SelectedIndex != -1)
                    {
                        SelectedLabour = DDLabourListItems[labourpick.SelectedIndex].Value.ToString();

                        strLabourValue = DDLabourListItems[labourpick.SelectedIndex].Text.ToString().Split(new char[] { '-' }, 2, StringSplitOptions.None)[1];

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


        public async Task<FormDMaterialDetailsRequestDTO> GetMaterialHeaderdetails(int HeaderCode)
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

                    var response = await _restApi.FormDMaterialGetById(HeaderCode);

                    if (response.success)
                    {
                        SelectedNewHdrItem = response.data;

                        //labourpick.Items.Clear();

                        labourpick.ItemsSource = DDLabourListItems.Select((DDListItems arg) => arg.Text).ToList();

                        int wsindex = DDLabourListItems.ToList().FindIndex(a => a.Value == SelectedNewHdrItem.MaterialCode);

                        if (wsindex == -1) { wsindex = 1; }
                        //ws.SelectedIndex = wsindex;                                                
                        labourpick.SelectedIndex = wsindex;

                        SelectedLabour = SelectedNewHdrItem.MaterialCode;

                        iRet = SelectedNewHdrItem.SerialNo;

                        strLabourValue = SelectedNewHdrItem.CodeDesc;

                        strQty = SelectedNewHdrItem.Quantity;

                        strRemarksValue = SelectedNewHdrItem.MaterialDesc;

                        //unitpick
                        //unitpick.Items.Clear();

                        unitpick.ItemsSource = DDUnitListItems.Select((DDListItems arg) => arg.Text).ToList();

                        int unitindex = DDUnitListItems.ToList().FindIndex(a => a.Text == SelectedNewHdrItem.Unit);

                        if (unitindex == -1) { unitindex = 1; }

                        //ws.SelectedIndex = wsindex;
                        unitpick.SelectedIndex = unitindex;


                    }
                    else
                        _userDialogs.Toast("Unable to connect please check your internet connection.");

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
            return new FormDMaterialDetailsRequestDTO();
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

                _editViewModel.Type = "Edit";

                btnSaveandExit.IsVisible = bValue;


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

                        if (ddtype == "MaterialUnit")
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


        public async Task<ObservableCollection<DDListItems>> GetMaterialList()
        {
            try
            {
                //_userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.GetFormDMaterialCode();

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

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (_editViewModel.RoadName == "Add")
            {
                // DropDownMasterSetup(_editViewModel.Type);

                _editViewModel.HdrFahPkRefNo = _editViewModel.HdrFahPkRefNo;

                GetHeaderNoCode = _editViewModel.HdrFahPkRefNo;

                ///await GetSerialNo(GetHeaderNoCode);

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

                SelectedNewHdrItem = await GetMaterialHeaderdetails(GetHeaderNoCode);

                isEnableControl(false);

                return;
            }


        }


        public async Task<ObservableCollection<FormDLabourDetailsResponseDTO>> UpdateMaterialHeaderList()
        {
            _userDialogs.ShowLoading("Loading");

            try
            {
                FormDMaterialDetailsRequestDTO objRequest = new FormDMaterialDetailsRequestDTO();


                if (CrossConnectivity.Current.IsConnected)
                {

                    objRequest = new FormDMaterialDetailsRequestDTO()
                    {
                        No = GetHeaderNoCode,
                        FdmdFdhPkRefNo = _editViewModel.HdrFahPkRefNo,
                        SerialNo = iRet,
                        MaterialCode = SelectedLabour,
                        MaterialDesc = strRemarksValue,
                        Quantity = strQty,
                        Unit = SelectedUnit,
                        CodeDesc = strLabourValue,
                        ActiveYn = true

                    };


                    //var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

                    var response = await _restApi.UpdateFormDMaterialHdr(objRequest);

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
                FormDMaterialDetailsRequestDTO objRequest = new FormDMaterialDetailsRequestDTO();


                if (CrossConnectivity.Current.IsConnected)
                {
                    await GetSerialNo(GetHeaderNoCode);

                    objRequest = new FormDMaterialDetailsRequestDTO()
                    {
                        FdmdFdhPkRefNo = GetHeaderNoCode,
                        SerialNo = iRet,
                        MaterialCode = SelectedLabour,
                        MaterialDesc = strRemarksValue,
                        Quantity = strQty,
                        Unit = SelectedUnit,
                        CodeDesc = strLabourValue

                    };


                    //var json = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

                    var response = await _restApi.SaveFormDMaterialHdr(objRequest);

                    if (response.success)
                    {
                        try
                        {
                            _editViewModel.Type = _editViewModel.Type;

                            _editViewModel.HdrFahPkRefNo = _editViewModel.HdrFahPkRefNo;

                            _editViewModel.HdrFahRefNo = response.data;

                            App.FormDDetailCode = response.data;

                            UserDialogs.Instance.Toast("Material Header Details Saved Successfully.");


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

                        //await GetSerialNo(GetHeaderNoCode);

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
                            var strSaveValue = SaveFormDHeaderList();
                        }
                        else if (_editViewModel.RoadName == "Add")
                        {
                            _editViewModel.RoadName = "";
                            var strSaveValue = SaveFormDHeaderList();

                        }
                        else
                        {

                            var strUpdateValue = UpdateMaterialHeaderList();

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





    }
}