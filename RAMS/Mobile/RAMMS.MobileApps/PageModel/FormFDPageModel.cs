using Acr.UserDialogs;
using FreshMvvm;
using Newtonsoft.Json;
using Plugin.Connectivity;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
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
    public class FormFDPageModel : FreshBasePageModel
    {
        #region Private fields

        private IRestApi _restApi;
        private IUserDialogs _userDialogs;
        private ILocalDatabase _localDatabase;
        private AssetDDLResponseDTO.DropDown _selectedRMU;
        private AssetDDLResponseDTO.DropDown _selectedRoadCode;
        private AssetDDLResponseDTO.DropDown _selectedSectionCode;
        private DDListItems _selectedFromYear;
        private DDListItems _selectedToYear;
        private int iValueRet = 1;
        private int _selectedPageSize = 0;
        private int _selectedCrewIndex = -1;
        private int _selectedInspIndex = -1;

        private bool isModify;
        private bool isDelete;
        private bool isView;
        #endregion

        #region Public Properties
        public string[] ColumnName = { null, "RefID", "Year", "InsDate", "InspectedBy", "RMUCode", "RMUDesc", "SecCode", "SecName", "RoadCode", "RoadName", "CrewLeader" };
        public List<Column> ColumnList { get; set; }
        public bool IsAdd { get; set; }
        public bool DetailSearchVisible { get; set; } = false;
        public string SelectedRoadName { get; set; }
        public string SelectedSectionName { get; set; }
        public DateTime? SelectedFromDate { get; set; } = null;
        public DateTime? SelectedToDate { get; set; } = null;
        public string SmartSearch { get; set; }
        public string TotalRecords { get; set; } = "0";
        public string PageStart { get; set; } = "0";
        public string PageEnd { get; set; } = "0";
        public int PageSize { get; set; } = 10;
        public string SortOrder { get; set; } = "0";

        public bool IsRefNoAssending { get; set; } = false;
        public bool IsyearOfInspectAssending { get; set; } = false;
        public bool IsDateOfInspectAssending { get; set; } = false;
        public bool IsInspectedByAssending { get; set; } = false;
        public bool IsRMUAssending { get; set; } = false;
        public bool IsRMUNameAssending { get; set; } = false;
        public bool IsSectionCodeAssending { get; set; } = false;
        public bool IsSectionNameAssending { get; set; } = false;
        public bool IsRoadCodeAssending { get; set; } = false;
        public bool IsRoadNameAssending { get; set; } = false;
        public bool IsCrewLeaderAssending { get; set; } = false;
        public int ColumnIndex { get; set; }
        public SearchRequestDTO SearchCriteriaItems { get; set; }

        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRodeCodeListItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRMUListItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDSectionListItems { get; set; }
        public ObservableCollection<FormFDSearchGridDTO> MyBaseFormFDListViewItems { get; set; }
        public ObservableCollection<DDListItems> DDAssetTypeListItems { get; set; }
        public ObservableCollection<DDListItems> DDFromYearListItems { get; set; }
        public ObservableCollection<DDListItems> DDToYearListItems { get; set; }
        public ObservableCollection<DDListItems> DDBoundListItems { get; set; }
        public ObservableCollection<DDListItems> DDInspUserListListItems { get; set; }
        public ObservableCollection<DDListItems> DDCrewUserListListItems { get; set; }

        public AssetDDLResponseDTO.DropDown SelectedRMU
        {
            get => _selectedRMU;
            set
            {
                _selectedRMU = value;

                SelectedSectionCode = SelectedRoadCode = null;
                SelectedSectionName = SelectedRoadName = "";
                GetLandingDropDownList();
                RaisePropertyChanged();
            }
        }
        public AssetDDLResponseDTO.DropDown SelectedRoadCode
        {
            get => _selectedRoadCode;
            set
            {
                _selectedRoadCode = value;
                if (_selectedRoadCode != null)
                    SelectedRoadName = _selectedRoadCode.Text.Split('-')[1].ToString();
                RaisePropertyChanged();
            }
        }
        public AssetDDLResponseDTO.DropDown SelectedSectionCode
        {
            get => _selectedSectionCode;
            set
            {
                _selectedSectionCode = value;
                if (_selectedSectionCode != null)
                    SelectedSectionName = _selectedSectionCode.Text.Split('-')[1].ToString();

                SelectedRoadCode = null;
                SelectedRoadName = "";
                GetLandingDropDownList();
                RaisePropertyChanged();
            }
        }

        public DDListItems SelectedFromYear
        {
            get => _selectedFromYear;
            set
            {
                _selectedFromYear = value;
            }
        }

        public DDListItems SelectedToYear
        {
            get => _selectedToYear;
            set
            {
                _selectedToYear = value;
            }
        }

        public int SelectedPageSize
        {
            get => _selectedPageSize;
            set
            {
                _selectedPageSize = value;
                SetPageSize(value);
            }
        }

        public int SelectedCrewIndex
        {
            get
            {
                return _selectedCrewIndex;
            }
            set
            {
                _selectedCrewIndex = value;
            }
        }
        public int SelectedInspIndex
        {
            get
            {
                return _selectedInspIndex;
            }
            set
            {
                _selectedInspIndex = value;
            }
        }

        #endregion

        #region Constructor
        public FormFDPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;
            _localDatabase = localDatabase;
        }

        #endregion

        #region Init and Appearing
        public override void Init(object initData)
        {
            base.Init(initData);

            isView = Model.Security.IsView(ModuleNameList.Condition_Inspection);
            IsAdd = isModify = Model.Security.IsModify(ModuleNameList.Condition_Inspection);
            isDelete = Model.Security.IsDelete(ModuleNameList.Condition_Inspection);

            DDRodeCodeListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>();
            DDRMUListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>();
            DDSectionListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>();
            MyBaseFormFDListViewItems = new ObservableCollection<FormFDSearchGridDTO>();
            ColumnList = new List<Column>();
            foreach (var item in ColumnName)
            {
                Column column = new Column() { data = item };
                ColumnList.Add(column);
            }
        }

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            GetLandingDropDownList();
            GetDDListDetails("Year");
            GetDDListDetails("crew");

            await GetUserList();
            GetGridData();
        }

        #endregion

        #region Methods
        private async void GetGridData()
        {
            _userDialogs.ShowLoading("Loading");
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var values = (iValueRet - 1) * PageSize > 0 ? (iValueRet - 1) * PageSize : 0;
                    SearchCriteriaItems = new SearchRequestDTO()
                    {
                        start = values,
                        length = PageSize,
                        columns = ColumnList,
                        order = ColumnIndex >= 1 ? new List<Order>() { new Order() { column = ColumnIndex, dir = SortOrder == "1" ? "asc" : "" } } : null,
                        filter = new Dictionary<string, string>()
                        {
                            { "KeySearch",SmartSearch},
                            { "RMUCode", SelectedRMU?.Value },
                            { "SecName", SelectedSectionCode?.Value },
                            { "RoadCode", SelectedRoadCode?.Value },
                            { "fromInsDate", SelectedFromDate?.Date.ToString("yyyy-MM-dd") },
                            { "toInsDate", SelectedToDate?.Date.ToString("yyyy-MM-dd") },
                            { "FromYear", SelectedFromYear?.Value },
                            { "ToYear",SelectedToYear?.Value },
                            {"InspectedByID", SelectedInspIndex == -1? null :  DDInspUserListListItems[SelectedInspIndex]?.Value.ToString() },
                            {"CrewLeaderID", SelectedCrewIndex == -1? null :   DDCrewUserListListItems[SelectedCrewIndex]?.Value.ToString() }
                        }
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(SearchCriteriaItems);
                     var response = await _restApi.GetFormFDLandingGridData(SearchCriteriaItems);

                    var response1 = JsonConvert.DeserializeObject<List<FormFDSearchGridDTO>>(response.data.data.ToString());
                    if (response.success)
                    {
                        TotalRecords = response.data.recordsTotal.ToString();
                        PageStart = response.data.recordsTotal == 0 ? "0" : (values + 1).ToString();
                        PageEnd = (values + PageSize) > response.data.recordsTotal ? TotalRecords : (values + PageSize).ToString();

                        var listItems = new ObservableCollection<FormFDSearchGridDTO>(response1);

                        var sNo = 1;
                        foreach (var ivalue in listItems)
                        {
                            ivalue.SNo = sNo;
                            sNo++;
                        }

                        MyBaseFormFDListViewItems = listItems;
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);
                }
                else
                {
                    _userDialogs.Alert("Please check your Internet Connection !");
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
        }

        private async void GetDDListDetails(string ddtype)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var ddlist = new DDLookUpDTO()
                    {
                        Type = ddtype,
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);
                    var response = new ResponseBaseListObject<DDListItems>();
                    if (ddtype == "crew")
                        response = await _restApi.GetSupervisor();
                    else
                        response = await _restApi.GetDDList(ddlist);


                    if (response.success)
                    {
                        if (ddtype == "Year")
                        {
                            DDFromYearListItems = new ObservableCollection<DDListItems>(response.data);
                            DDToYearListItems = new ObservableCollection<DDListItems>(response.data);
                        }
                        else if (ddtype == "crew")
                            DDCrewUserListListItems = new ObservableCollection<DDListItems>(response.data);
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);
                }
                else
                {
                    _userDialogs.Alert("Please check your Internet Connection !");
                }
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
            }
        }

        public async Task<int> GetUserList()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.userList();
                    if (response.success)
                    {
                        DDInspUserListListItems = new ObservableCollection<DDListItems>(response.data);
                       // DDCrewUserListListItems = new ObservableCollection<DDListItems>(response.data);
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);
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
            return 1;
        }

        private void SetPageSize(int value)
        {
            iValueRet = 1;
            switch (value)
            {
                case 0:
                    PageSize = 10;
                    break;
                case 1:
                    PageSize = 25;
                    break;
                case 2:
                    PageSize = 50;
                    break;
                case 3:
                    PageSize = 100;
                    break;
                case 4:
                    PageSize = 500;
                    break;
                case 5:
                    PageSize = 1000;
                    break;
            }
            GetGridData();
        }

        private void GetSortOrder(int columnIndex)
        {
            if (columnIndex == 1)
                SortOrder = IsRefNoAssending ? "1" : "0";
            if (columnIndex == 2)
                SortOrder = IsyearOfInspectAssending ? "1" : "0";
            if (columnIndex == 3)
                SortOrder = IsDateOfInspectAssending ? "1" : "0";
            if (columnIndex == 4)
                SortOrder = IsInspectedByAssending ? "1" : "0";
            if (columnIndex == 5)
                SortOrder = IsRMUAssending ? "1" : "0";
            if (columnIndex == 6)
                SortOrder = IsRMUNameAssending ? "1" : "0";
            if (columnIndex == 7)
                SortOrder = IsSectionCodeAssending ? "1" : "0";
            if (columnIndex == 8)
                SortOrder = IsSectionNameAssending ? "1" : "0";
            if (columnIndex == 9)
                SortOrder = IsRoadCodeAssending ? "1" : "0";
            if (columnIndex == 10)
                SortOrder = IsRoadNameAssending ? "1" : "0";
            if (columnIndex == 11)
                SortOrder = IsCrewLeaderAssending ? "1" : "0";
        }
        private void SetSortOrder(int columnIndex)
        {
            if (columnIndex == 1)
            {
                IsRefNoAssending = !IsRefNoAssending;
                IsyearOfInspectAssending = IsDateOfInspectAssending = IsInspectedByAssending = IsRMUAssending = IsRMUNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsCrewLeaderAssending = false;
            }
            else if (columnIndex == 2)
            {
                IsyearOfInspectAssending = !IsyearOfInspectAssending;
                IsRefNoAssending = IsDateOfInspectAssending = IsInspectedByAssending = IsRMUAssending = IsRMUNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsCrewLeaderAssending = false;

            }
            else if (columnIndex == 3)
            {
                IsDateOfInspectAssending = !IsDateOfInspectAssending;
                IsRefNoAssending = IsyearOfInspectAssending = IsInspectedByAssending = IsRMUAssending = IsRMUNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsCrewLeaderAssending = false;
            }
            else if (columnIndex == 4)
            {
                IsInspectedByAssending = !IsInspectedByAssending;
                IsRefNoAssending = IsyearOfInspectAssending = IsDateOfInspectAssending = IsRMUAssending = IsRMUNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsCrewLeaderAssending = false;
            }
            else if (columnIndex == 5)
            {
                IsRMUAssending = !IsRMUAssending;
                IsRefNoAssending = IsyearOfInspectAssending = IsDateOfInspectAssending = IsInspectedByAssending = IsRMUNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsCrewLeaderAssending = false;
            }
            else if (columnIndex == 6)
            {
                IsRMUNameAssending = !IsRMUNameAssending;
                IsRefNoAssending = IsyearOfInspectAssending = IsDateOfInspectAssending = IsInspectedByAssending = IsRMUAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsCrewLeaderAssending = false;
            }
            else if (columnIndex == 7)
            {
                IsSectionCodeAssending = !IsSectionCodeAssending;
                IsRefNoAssending = IsyearOfInspectAssending = IsDateOfInspectAssending = IsInspectedByAssending = IsRMUAssending = IsRMUNameAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsCrewLeaderAssending = false;
            }
            else if (columnIndex == 8)
            {
                IsSectionNameAssending = !IsSectionNameAssending;
                IsRefNoAssending = IsyearOfInspectAssending = IsDateOfInspectAssending = IsInspectedByAssending = IsRMUAssending = IsRMUNameAssending = IsSectionCodeAssending = IsRoadCodeAssending = IsRoadNameAssending = IsCrewLeaderAssending = false;
            }
            else if (columnIndex == 9)
            {
                IsRoadCodeAssending = !IsRoadCodeAssending;
                IsRefNoAssending = IsyearOfInspectAssending = IsDateOfInspectAssending = IsInspectedByAssending = IsRMUAssending = IsRMUNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadNameAssending = IsCrewLeaderAssending = false;
            }
            else if (columnIndex == 10)
            {
                IsRoadNameAssending = !IsRoadNameAssending;
                IsRefNoAssending = IsyearOfInspectAssending = IsDateOfInspectAssending = IsInspectedByAssending = IsRMUAssending = IsRMUNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending  = IsCrewLeaderAssending = false;
            }
            else if (columnIndex == 11)
            {
                IsCrewLeaderAssending = !IsCrewLeaderAssending;
                IsRefNoAssending = IsyearOfInspectAssending = IsDateOfInspectAssending = IsInspectedByAssending = IsRMUAssending = IsRMUNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = false;
            }
            else
            {
                IsRefNoAssending = IsyearOfInspectAssending = IsDateOfInspectAssending = IsInspectedByAssending = IsRMUAssending = IsRMUNameAssending = IsSectionCodeAssending = IsSectionNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsCrewLeaderAssending = false;
            }
        }

        public async void GetLandingDropDownList(string propName = null)
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var strQuery = new AssetDDLRequestDTO
                    {
                        RMU = SelectedRMU?.Value,
                        RdCode = SelectedRoadCode?.Value,
                        SectionCode = Convert.ToInt32(SelectedSectionCode?.Code),
                        GrpCode = "CW"
                    };

                    var response = await _restApi.GetF2LandingDropDown(strQuery);

                    if (response.success)
                    {
                        if (response.data.RMU != null && SelectedRMU == null)
                        {
                            DDRMUListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>(response.data.RMU);
                        }
                        if (propName != "Section" && response.data.Section?.Count > 0)
                        {
                            DDSectionListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>(response.data.Section);
                        }
                        if (response.data.RdCode != null)
                        {
                            DDRodeCodeListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>(response.data.RdCode);
                        }
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);
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
        }

        private int? ConvertToNullableInt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return Convert.ToInt32(value);
        }

        public async Task<int> DeleteRecord(int pkRefNo)
        {
            try
            {
                _userDialogs.ShowLoading("loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.DeleteFDHeader(pkRefNo);
                    if (response.success)
                    {
                        if (response.success)
                        {
                            await UserDialogs.Instance.AlertAsync("Data Deleted Successfully.", "RAMS", "0K");
                        }
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);
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
            return 1;
        }

        #endregion

        #region Commands

        public ICommand ClickMeAction
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    var SelectedRowItem = (FormFDSearchGridDTO)obj;

                    App.HeaderCode = SelectedRowItem.RefNo;
                    var actionResult = "";

                    string view = isView ? "View" : "";
                    string delete = isDelete ? "Delete" : "";
                    string edit = isModify && SelectedRowItem.Status == "Saved" ? "Edit" : "";

                    string[] permissions = new string[] { edit, view, delete };
                    permissions = permissions.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                    actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, permissions);

                    if (actionResult == "Delete")
                    {
                        var actionResult1 = await UserDialogs.Instance.ConfirmAsync("Are you sure want to delete this record?", "RAMS", "Yes", "No");
                        if (actionResult1)
                        {
                            await DeleteRecord(SelectedRowItem.RefNo);
                            GetGridData();
                            return;
                        }
                    }
                    if (actionResult == "Edit")
                    {
                        App.ReturnType = "Edit";
                        await CoreMethods.PushPageModel<FormFDAddPageModel>();
                    }
                    else if (actionResult == "View")
                    {
                        App.ReturnType = "View";
                        await CoreMethods.PushPageModel<FormFDAddPageModel>();
                    }
                });
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    iValueRet = 1;
                    GetGridData();
                });
            }
        }

        public ICommand SearchExpandCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    DetailSearchVisible = !DetailSearchVisible;
                });
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    App.ReturnType = "Add";
                    await CoreMethods.PushPageModel<FormFDAddPageModel>();
                });
            }
        }

        public ICommand ClearCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    SelectedRMU = SelectedSectionCode = SelectedRoadCode = null;
                    SelectedFromYear = SelectedToYear = null;
                    SelectedFromDate = SelectedToDate = null;
                    SelectedSectionName = SelectedRoadName = SmartSearch = "";
                    SelectedCrewIndex = SelectedInspIndex = -1;

                    GetGridData();
                    GetLandingDropDownList();
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

                    GetGridData();
                });
            }
        }

        public ICommand btnNextCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    if (iValueRet <= (Convert.ToInt32(TotalRecords) / PageSize))
                        iValueRet = iValueRet + 1;

                    GetGridData();
                });
            }
        }

        public ICommand SortingCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    ColumnIndex = Convert.ToInt32(obj);
                    GetSortOrder(ColumnIndex);
                    GetGridData();
                    SetSortOrder(ColumnIndex);
                    iValueRet = 1;
                });
            }
        }

        #endregion


    }
}
