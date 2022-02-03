using Acr.UserDialogs;
using FreshMvvm;
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
    class FormB1B2PageModel : FreshBasePageModel
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
        private DDListItems _selectedAssetType;
        private int iValueRet = 1;
        private int _selectedPageSize = 0;
        private bool isModify;
        private bool isDelete;
        private bool isView;
        #endregion

        #region Public Properties
        public bool IsAdd { get; set; }
        public bool DetailSearchVisible { get; set; } = false;
        public string SelectedRoadName { get; set; }
        public int? ToChKM { get; set; }
        public string ToChM { get; set; }
        public int? FromChKM { get; set; }
        public string FromChM { get; set; }
        public string SelectedSectionName { get; set; }
        public string SmartSearch { get; set; }
        public string TotalRecords { get; set; }
        public string PageStart { get; set; }
        public string PageEnd { get; set; }
        public int PageSize { get; set; } = 10;
        public string SortOrder { get; set; } = "0";
        public bool IsRefNoAssending { get; set; } = false;
        public bool IsDateOfInspectAssending { get; set; } = false;
        public bool IsYearOfInspectAssending { get; set; } = false;
        public bool IsBridgeIDAssending { get; set; } = false;
        public bool IsDivisionAssending { get; set; } = false;
        public bool IsRMUAssending { get; set; } = false;
        public bool IsRMUNameAssending { get; set; } = false;
        public bool IsSecCodeAssending { get; set; } = false;
        public bool IsSecNameAssending { get; set; } = false;
        public bool IsRoadCodeAssending { get; set; } = false;
        public bool IsRoadNameAssending { get; set; } = false;
        public bool IsStatusAssending { get; set; } = false;
        public bool IsInspectByAssending { get; set; } = false;
        public bool IsAuditedByAssending { get; set; } = false;
        public int ColumnIndex { get; set; }
        public FilteredPagingDefinition<FormB1B2SearchGridDTO> SearchCriteriaItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRodeCodeListItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRMUListItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDSectionListItems { get; set; }
        public ObservableCollection<FormB1B2HeaderRequestDTO> MyBaseFormB1B2ListViewItems { get; set; }
        public ObservableCollection<DDListItems> DDAssetTypeListItems { get; set; }
        public ObservableCollection<DDListItems> DDFromYearListItems { get; set; }
        public ObservableCollection<DDListItems> DDToYearListItems { get; set; }
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
        public DDListItems SelectedAssetType
        {
            get => _selectedAssetType;
            set
            {
                _selectedAssetType = value;
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

        #endregion

        #region Constructor
        public FormB1B2PageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
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
            MyBaseFormB1B2ListViewItems = new ObservableCollection<FormB1B2HeaderRequestDTO>();
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            GetLandingDropDownList();
            GetDDListDetails("Asset type");
            GetDDListDetails("Year");
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
                    //string rmuSearchcode = string.Empty;
                    //if (SelectedRMU != null && SelectedRMU.Value == "MIRI")
                    //    rmuSearchcode = "MRI";
                    //if (SelectedRMU != null && SelectedRMU.Value == "Batu Niah")
                    //    rmuSearchcode = "BTN";

                    var values = (iValueRet - 1) * PageSize > 0 ? (iValueRet - 1) * PageSize : 0;
                    SearchCriteriaItems = new FilteredPagingDefinition<FormB1B2SearchGridDTO>()
                    {
                        StartPageNo = values,
                        RecordsPerPage = PageSize,
                        sortOrder = SortOrder,
                        ColumnIndex = ColumnIndex,
                        Filters = new FormB1B2SearchGridDTO()
                        {
                           // RmuCode = rmuSearchcode,
                           RmuCode= SelectedRMU?.Value,
                            RoadCode = SelectedRoadCode != null ? SelectedRoadCode?.Value : null,
                            SecCode = SelectedSectionCode != null ? Convert.ToInt32(SelectedSectionCode?.Code) : (int?)null,
                            SmartSearch = SmartSearch,
                            FromYear = SelectedFromYear != null ? Convert.ToInt32(SelectedFromYear?.Value) : (int?)null,
                            ToYear = SelectedToYear != null ? Convert.ToInt32(SelectedToYear?.Value) : (int?)null,
                            Division = "",
                            FromInspectionDate = null,
                            ToInspectionDate = null,
                            AssetType = SelectedAssetType != null ? SelectedAssetType?.Value : null,
                            locchFromKm = FromChKM,
                            locchFromM = FromChM,
                            locchToKm = ToChKM,
                            locchToM = ToChM,
                            Year = null
                        },
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(SearchCriteriaItems);
                    var response = await _restApi.GetFormB1B2LandingGridData(SearchCriteriaItems);

                    if (response.success)
                    {
                        TotalRecords = response.data.TotalRecords.ToString();
                        PageStart = response.data.TotalRecords == 0 ? "0" : (response.data.PageNo + 1).ToString();
                        PageEnd = (response.data.PageNo + response.data.FilteredRecords).ToString();

                        var listItems = new ObservableCollection<FormB1B2HeaderRequestDTO>(response.data.PageResult);

                        int i = 0;
                        foreach (var ivalue in listItems)
                        {
                            ivalue.Status = ivalue.SubmitSts == true ? "Submitted" : "Saved";
                            ivalue.SNo = i++;
                        }

                        MyBaseFormB1B2ListViewItems = listItems;
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
                    ResponseBaseListObject<DDListItems> response;
                    var ddlist = new DDLookUpDTO()
                    {
                        Type = ddtype,
                        TypeCode = ddtype == "Asset type" ? "BR" : ""
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);

                    if (ddtype == "Asset type")
                        response = await _restApi.GetDDLDescValue(ddlist);
                    else
                        response = await _restApi.GetDDList(ddlist);

                    if (response.success)
                    {
                        if (ddtype == "Asset type")
                            DDAssetTypeListItems = new ObservableCollection<DDListItems>(response.data);
                        else if (ddtype == "Year")
                        {
                            DDFromYearListItems = new ObservableCollection<DDListItems>(response.data);
                            DDToYearListItems = new ObservableCollection<DDListItems>(response.data);
                        }
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
            if (columnIndex == 2)
                SortOrder = IsRefNoAssending ? "1" : "0";
            if (columnIndex == 3)
                SortOrder = IsYearOfInspectAssending ? "1" : "0";
            if (columnIndex == 4)
                SortOrder = IsDateOfInspectAssending ? "1" : "0";
            if (columnIndex == 5)
                SortOrder = IsBridgeIDAssending ? "1" : "0";
            if (columnIndex == 6)
                SortOrder = IsDivisionAssending ? "1" : "0";
            if (columnIndex == 7)
                SortOrder = IsRMUAssending ? "1" : "0";
            if (columnIndex == 8)
                SortOrder = IsRMUNameAssending ? "1" : "0";
            if (columnIndex == 9)
                SortOrder = IsSecCodeAssending ? "1" : "0";
            if (columnIndex == 10)
                SortOrder = IsSecNameAssending ? "1" : "0";
            if (columnIndex == 11)
                SortOrder = IsRoadCodeAssending ? "1" : "0";
            if (columnIndex == 12)
                SortOrder = IsRoadNameAssending ? "1" : "0";
            if (columnIndex == 13)
                SortOrder = IsStatusAssending ? "1" : "0";
            if (columnIndex == 14)
                SortOrder = IsInspectByAssending ? "1" : "0";
            if (columnIndex == 15)
                SortOrder = IsAuditedByAssending ? "1" : "0";
        }
        private void SetSortOrder(int columnIndex)
        {
            if (columnIndex == 2)
            {
                IsRefNoAssending = !IsRefNoAssending;
                IsDateOfInspectAssending = IsYearOfInspectAssending = IsBridgeIDAssending = IsDivisionAssending = IsRMUAssending = IsRMUNameAssending = IsSecCodeAssending = IsSecNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsStatusAssending = IsInspectByAssending = IsAuditedByAssending = false;
            }
            else if (columnIndex == 3)
            {
                IsYearOfInspectAssending = !IsYearOfInspectAssending;
                IsRefNoAssending = IsDateOfInspectAssending = IsBridgeIDAssending = IsDivisionAssending = IsRMUAssending = IsRMUNameAssending = IsSecCodeAssending = IsSecNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsStatusAssending = IsInspectByAssending = IsAuditedByAssending = false;
            }

            else if (columnIndex == 4)
            {
                IsDateOfInspectAssending = !IsDateOfInspectAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsBridgeIDAssending = IsDivisionAssending = IsRMUAssending = IsRMUNameAssending = IsSecCodeAssending = IsSecNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsStatusAssending = IsInspectByAssending = IsAuditedByAssending = false;
            }
            else if (columnIndex == 5)
            {
                IsBridgeIDAssending = !IsBridgeIDAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsDivisionAssending = IsRMUAssending = IsRMUNameAssending = IsSecCodeAssending = IsSecNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsStatusAssending = IsInspectByAssending = IsAuditedByAssending = false;
            }
            else if (columnIndex == 6)
            {
                IsDivisionAssending = !IsDivisionAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsBridgeIDAssending = IsRMUAssending = IsRMUNameAssending = IsSecCodeAssending = IsSecNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsStatusAssending = IsInspectByAssending = IsAuditedByAssending = false;
            }
            else if (columnIndex == 7)
            {
                IsRMUAssending = !IsRMUAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsBridgeIDAssending = IsDivisionAssending = IsRMUNameAssending = IsSecCodeAssending = IsSecNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsStatusAssending = IsInspectByAssending = IsAuditedByAssending = false;
            }
            else if (columnIndex == 8)
            {
                IsRMUNameAssending = !IsRMUNameAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsBridgeIDAssending = IsDivisionAssending = IsRMUAssending = IsSecCodeAssending = IsSecNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsStatusAssending = IsInspectByAssending = IsAuditedByAssending = false;
            }
            else if (columnIndex == 9)
            {
                IsSecCodeAssending = !IsSecCodeAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsBridgeIDAssending = IsDivisionAssending = IsRMUAssending = IsRMUNameAssending = IsSecNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsStatusAssending = IsInspectByAssending = IsAuditedByAssending = false;
            }
            else if (columnIndex == 10)
            {
                IsSecNameAssending = !IsSecNameAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsBridgeIDAssending = IsDivisionAssending = IsRMUAssending = IsRMUNameAssending = IsSecCodeAssending = IsRoadCodeAssending = IsRoadNameAssending = IsStatusAssending = IsInspectByAssending = IsAuditedByAssending = false;
            }
            else if (columnIndex == 11)
            {
                IsRoadCodeAssending = !IsRoadCodeAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsBridgeIDAssending = IsDivisionAssending = IsRMUAssending = IsRMUNameAssending = IsSecCodeAssending = IsSecNameAssending = IsRoadNameAssending = IsStatusAssending = IsInspectByAssending = IsAuditedByAssending = false;
            }
            else if (columnIndex == 12)
            {
                IsRoadNameAssending = !IsRoadNameAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsBridgeIDAssending = IsDivisionAssending = IsRMUAssending = IsRMUNameAssending = IsSecCodeAssending = IsSecNameAssending = IsRoadCodeAssending = IsStatusAssending = IsInspectByAssending = IsAuditedByAssending = false;
            }
            else if (columnIndex == 13)
            {
                IsStatusAssending = !IsStatusAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsBridgeIDAssending = IsDivisionAssending = IsRMUAssending = IsRMUNameAssending = IsSecCodeAssending = IsSecNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsInspectByAssending = IsAuditedByAssending = false;
            }
            else if (columnIndex == 14)
            {
                IsInspectByAssending = !IsInspectByAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsBridgeIDAssending = IsDivisionAssending = IsRMUAssending = IsRMUNameAssending = IsSecCodeAssending = IsSecNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsStatusAssending = IsAuditedByAssending = false;
            }
            else if (columnIndex == 15)
            {
                IsAuditedByAssending = !IsAuditedByAssending;
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsBridgeIDAssending = IsDivisionAssending = IsRMUAssending = IsRMUNameAssending = IsSecCodeAssending = IsSecNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsStatusAssending = IsInspectByAssending = false;
            }
            else
            {
                IsRefNoAssending = IsYearOfInspectAssending = IsDateOfInspectAssending = IsBridgeIDAssending = IsDivisionAssending = IsRMUAssending = IsRMUNameAssending = IsSecCodeAssending = IsSecNameAssending = IsRoadCodeAssending = IsRoadNameAssending = IsStatusAssending = IsInspectByAssending = IsAuditedByAssending = false;
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
                       // GrpCode = "BR"
                    };

                    var response = await _restApi.GetB1B2LandingDropDown(strQuery);

                    if (response.success)
                    {
                        if (response.data.RMU != null)
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

        public async Task<int> DeleteRecord(int pkRefNo)
        {
            try
            {
                _userDialogs.ShowLoading("loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.DeleteB1B2Hdr(pkRefNo);
                    if (response.success)
                    {
                        if (response.data)
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
                    await CoreMethods.PushPageModel<FormB1B2AddPageModel>();
                });
            }
        }
        public ICommand ClickMeAction
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    var SelectedRowItem = (FormB1B2HeaderRequestDTO)obj;

                    App.HeaderCode = SelectedRowItem.PkRefNo;
                    var actionResult = "";

                    string view = isView ? "View" : "";
                    string delete = isDelete ? "Delete" : "";
                    string edit = isModify && SelectedRowItem.Status.ToLower() != "submitted" ? "Edit" : "";

                    string[] permissions = new string[] { edit, view, delete };
                    permissions = permissions.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                    actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, permissions);

                    if (actionResult == "Delete")
                    {
                        var actionResult1 = await UserDialogs.Instance.ConfirmAsync("Are you sure want to delete this record?", "RAMS", "Yes", "No");
                        if (actionResult1)
                        {
                            await DeleteRecord(SelectedRowItem.PkRefNo);
                            GetGridData();
                            return;
                        }
                    }
                    if (actionResult == "Edit")
                    {
                        App.ReturnType = "Edit";
                        await CoreMethods.PushPageModel<FormB1B2AddPageModel>();
                    }
                    else if (actionResult == "View")
                    {
                        App.ReturnType = "View";
                        await CoreMethods.PushPageModel<FormB1B2AddPageModel>();
                    }
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
                    SelectedSectionName = SelectedRoadName = SmartSearch = "";
                    // DDAssetTypeListItems = null;
                    SelectedAssetType = null;
                     FromChKM = ToChKM = null;
                    FromChM = ToChM = null;


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
