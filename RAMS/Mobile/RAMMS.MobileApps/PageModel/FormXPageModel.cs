using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;

namespace RAMMS.MobileApps.PageModel
{
    public class FormXPageModel : FreshBasePageModel
    {
        private IRestApi _restApi;

        public IUserDialogs _userDialogs;

        public ILocalDatabase _localDatabase;
        private int iValueRet = 1;
        private int rmuIndex = -1;
        public int RMUIndex
        {
            get
            {
                return rmuIndex;
            }
            set
            {
                if (rmuIndex != value)
                {
                    rmuIndex = value;
                    RaisePropertyChanged();
                    RMUSelectedIndexChanged(rmuIndex);
                }
            }
        }

        private int roadCodeIndex = -1;
        public int RoadCodeIndex
        {
            get
            {
                return roadCodeIndex;
            }
            set
            {
                if (roadCodeIndex != value)
                {
                    roadCodeIndex = value;
                    RaisePropertyChanged();
                    RoadCodeSelectedIndexChanged(roadCodeIndex);
                }
            }
        }

        private int mainTaskIndex = -1;
        public int MainTaskIndex
        {
            get
            {
                return mainTaskIndex;
            }
            set
            {
                if (mainTaskIndex != value)
                {
                    mainTaskIndex = value;
                    RaisePropertyChanged();
                    MainTaskSelectedIndexChanged(mainTaskIndex);
                }
            }
        }

        private int subTaskIndex = -1;
        public int SubTaskIndex
        {
            get
            {
                return subTaskIndex;
            }
            set
            {
                if (subTaskIndex != value)
                {
                    subTaskIndex = value;
                    RaisePropertyChanged();
                    SubTaskSelectedIndexChanged(subTaskIndex);
                }
            }
        }

        private int pageSizeIndex = 0;
        public int PageSizeIndex
        {
            get
            {
                return pageSizeIndex;
            }
            set
            {
                if (pageSizeIndex != value)
                {
                    pageSizeIndex = value;
                    RaisePropertyChanged();
                    PageSizeSelectedIndexChanged(pageSizeIndex);
                }
            }
        }

        public string SelectedRMU { get; set; }

        public string SelectedRoadCode { get; set; }

        public int? SelectedMainTaskCode { get; set; }

        public string SelectedSubTaskCode { get; set; }

        public string SmartSearch { get; set; }

        public DateTime? WorkScheduledDate { get; set; }

        public DateTime? WorkCompletedDate { get; set; }

        public DateTime? CaseClosedDate { get; set; }

        public string TotalRecords { get; set; }
        public string SortOrder { get; set; } = "0";
        public string PageStart { get; set; }
        public int ColumnIndex { get; set; }
        public string PageEnd { get; set; }
        public bool IsRefNoAssending { get; set; } = false;
        public bool IsRmuNameAssending { get; set; } = false;
        public bool IsRoadCodeAssending { get; set; } = false;
        public bool IsReportedByAssending { get; set; } = false;
        public bool IsReportedNameAssending { get; set; } = false;
        public bool IsAttentionToAssending { get; set; } = false;
        public bool IsModeCommunicationAssending { get; set; } = false;
        public bool IsVerifiedByAssending { get; set; } = false;


        public int PageSize { get; set; } = 10;

        public int PageNo { get; set; } = 0;

        private EditViewModel editViewModel;

        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRodeCodeListItems { get; set; }

        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRMUListItems { get; set; }

        public ObservableCollection<DDListItems> MainTaskListItems { get; set; }
        public ObservableCollection<DDListItems> SubTaskListItems { get; set; }

        public ObservableCollection<FormXHeaderResponseDTO> MyBaseFormXList { get; set; }
        public bool IsEmpty { get; private set; }
        public FilteredPagingDefinition<FormXSearchGridDTO> GridItems { get; private set; }

        public ICommand ClearCommand
        {
            get
            {
                return new FreshCommand((obj) =>
                {
                    RMUIndex = -1;
                    RoadCodeIndex = -1;
                    MainTaskIndex = -1;
                    SubTaskIndex = -1;
                    WorkCompletedDate = null;
                    WorkScheduledDate = null;
                    CaseClosedDate = null;
                    SmartSearch = null;
                    GetMyFormXListReports();
                });
            }
        }

        public ICommand ClickMeActionCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    var SelectedFormXRowItem = (FormXHeaderResponseDTO)obj;
                    var actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "View");
                    editViewModel.Type = "View";
                    App.SubmitViewState = true;
                    editViewModel.HdrFahPkRefNo = (int)SelectedFormXRowItem.No;
                    await CoreMethods.PushPageModel<FormXAddPageModel>(editViewModel);
                });
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    iValueRet = 1;
                    GetMyFormXListReports();
                });
            }
        }

        public ICommand NextCommand
        {
            get
            {
                return new FreshCommand((obj) =>
                {
                    int iRet = (Convert.ToInt32(TotalRecords) - 1) / PageSize;
                    if (PageNo < iRet)
                    {
                        PageNo = PageNo + 1;
                        GetMyFormXListReports(PageNo);
                    }

                });
            }
        }

        public ICommand PrevCommand
        {
            get
            {
                return new FreshCommand((obj) =>
                {
                    if (PageNo >= 1)
                    {
                        PageNo = PageNo - 1;
                        GetMyFormXListReports(PageNo);
                    }
                });
            }
        }

        public FormXPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;

            _restApi = restApi;

            _localDatabase = localDatabase;
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            editViewModel = new EditViewModel();
            DDRodeCodeListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>();
            DDRMUListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>();
            MyBaseFormXList = new ObservableCollection<FormXHeaderResponseDTO>();

        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            GetLandingDropDownList();
            GetTaskDropdownlist("ACT-Main_Task");
            GetTaskDropdownlist("ACT-Sub_Task");
            GetMyFormXListReports();
           
        }

        private void RMUSelectedIndexChanged(int rmuindex)
        {
            SelectedRMU = null;
            if (rmuindex != -1)
            {
                SelectedRMU = DDRMUListItems[rmuindex].Value;
                GetLandingDropDownList();
            }
        }


        private void RoadCodeSelectedIndexChanged(int roadcodeindex)
        {
            SelectedRoadCode = null;
            if (roadcodeindex != -1)
            {
                SelectedRoadCode = DDRodeCodeListItems[roadcodeindex].Value;
            }
        }
        private void MainTaskSelectedIndexChanged(int maintaskindex)
        {
            SelectedMainTaskCode = null;
            if (maintaskindex != -1)
            {
                SelectedMainTaskCode = Convert.ToInt32(MainTaskListItems[maintaskindex].Value);
            }
        }

        private void SubTaskSelectedIndexChanged(int subtaskindex)
        {
            SelectedSubTaskCode = null;
            if (subtaskindex != -1)
            {
                SelectedSubTaskCode = SubTaskListItems[subtaskindex].Value;
            }
        }

        private void PageSizeSelectedIndexChanged(int pagesizeindex)
        {
            iValueRet = 1;
            switch (pagesizeindex)
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
                default:
                    PageSize = 1000;
                    break;
            }

            GetMyFormXListReports();
        }

        public async void GetMyFormXListReports(int currentpageno = 0)
        {
            _userDialogs.ShowLoading("Loading");
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var values = (iValueRet - 1) * PageSize > 0 ? (iValueRet - 1) * PageSize : 0;


                    GridItems = new FilteredPagingDefinition<FormXSearchGridDTO>()
                    {
                        StartPageNo = currentpageno * PageSize,

                        RecordsPerPage = PageSize,

                        sortOrder = SortOrder,
                        ColumnIndex = ColumnIndex,
                        Filters = new FormXSearchGridDTO()
                        {
                            RoadCode = SelectedRoadCode,
                            Rmu = SelectedRMU,
                            SmartInputValue = SmartSearch,
                            ActMainCode = SelectedMainTaskCode,
                            ActSubCode = SelectedSubTaskCode,
                            WorkScheduleDt = WorkScheduledDate,
                            WorkCompltDt = WorkCompletedDate,
                            CaseClosedDt = CaseClosedDate
                        }
                    };


                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

                    var response = await _restApi.GetFormXGridList(GridItems);

                    if (response.success)
                    {

                        TotalRecords = response.data.TotalRecords.ToString();

                        PageStart = response.data.TotalRecords == 0 ? "0" : (response.data.PageNo + 1).ToString();

                        if (((currentpageno + 1) * PageSize) > Convert.ToInt32(TotalRecords))
                            PageEnd = TotalRecords;
                        else
                            PageEnd = ((currentpageno + 1) * PageSize).ToString();
                        //PageEnd = response.data.FilteredRecords.ToString();

                     var listItems = new ObservableCollection<FormXHeaderResponseDTO>(response.data.PageResult);

                        var sNo = 1;
                        foreach (var ivalue in listItems)
                        {
                          //  ivalue.Status = ivalue.SubmitSts == true ? "Submitted" : "Saved";
                            ivalue.SNo = sNo;
                            sNo++;
                        }
                        MyBaseFormXList = listItems;

                    }
                    else
                        _userDialogs.Toast(response.errorMessage);
                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");

                IsEmpty = MyBaseFormXList.Count == 0;
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


        private async void GetLandingDropDownList()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {

                    var strQuery = new AssetDDLRequestDTO
                    {
                        RMU = SelectedRMU
                    };


                    var response = await _restApi.GetLandingDropDown(strQuery);

                    if (response.success)
                    {
                        if (response.data.RMU != null)
                        {
                            DDRMUListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>(response.data.RMU);
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

        private async void GetTaskDropdownlist(string type)
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {

                    var strQuery = new DDLookUpDTO
                    {
                        Type = type
                    };


                    var response = await _restApi.GetTaskList(strQuery);

                    if (response.success)
                    {
                        if (type == "ACT-Main_Task")
                        {
                            MainTaskListItems = new ObservableCollection<DDListItems>(response.data);
                        }
                        else if(type == "ACT-Sub_Task")
                        {
                            SubTaskListItems = new ObservableCollection<DDListItems>(response.data);
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



        private void GetSortOrder(int columnIndex)
        {
            if (columnIndex == 2)
                SortOrder = IsRefNoAssending ? "1" : "0";
            if (columnIndex == 3)
                SortOrder = IsRmuNameAssending ? "1" : "0";
            if (columnIndex == 4)
                SortOrder = IsRoadCodeAssending ? "1" : "0";
            if (columnIndex == 5)
                SortOrder = IsReportedByAssending ? "1" : "0";
            if (columnIndex == 6)
                SortOrder = IsReportedNameAssending ? "1" : "0";
            if (columnIndex == 7)
                SortOrder = IsAttentionToAssending ? "1" : "0";
            if (columnIndex == 8)
                SortOrder = IsModeCommunicationAssending ? "1" : "0";
            if (columnIndex == 9)
                SortOrder = IsVerifiedByAssending ? "1" : "0";
            
        }





        private void SetSortOrder(int columnIndex)
        {
            if (columnIndex == 2)
            {
                IsRefNoAssending = !IsRefNoAssending;
                IsRmuNameAssending = IsRoadCodeAssending = IsReportedByAssending = IsReportedNameAssending = IsAttentionToAssending = IsModeCommunicationAssending = IsVerifiedByAssending = false;
            }
            else if (columnIndex == 3)
            {
                IsRmuNameAssending = !IsRmuNameAssending;
                IsRefNoAssending = IsRoadCodeAssending = IsReportedByAssending = IsReportedNameAssending = IsAttentionToAssending = IsModeCommunicationAssending = IsVerifiedByAssending = false;
            }
            else if (columnIndex == 4)
            {
                IsRoadCodeAssending = !IsRoadCodeAssending;
                IsRefNoAssending = IsRmuNameAssending = IsReportedByAssending = IsReportedNameAssending = IsAttentionToAssending = IsModeCommunicationAssending = IsVerifiedByAssending = false;
            }
            else if (columnIndex == 5)
            {
                IsReportedByAssending = !IsReportedByAssending;
                IsRefNoAssending = IsRmuNameAssending = IsRoadCodeAssending = IsReportedNameAssending = IsAttentionToAssending = IsModeCommunicationAssending = IsVerifiedByAssending = false;
            }
            else if (columnIndex == 6)
            {
                IsReportedNameAssending = !IsReportedNameAssending;
                IsRefNoAssending = IsRmuNameAssending = IsRoadCodeAssending = IsReportedByAssending = IsAttentionToAssending = IsModeCommunicationAssending = IsVerifiedByAssending = false;
            }
            else if (columnIndex == 7)
            {
                IsAttentionToAssending = !IsAttentionToAssending;
                IsRefNoAssending = IsRmuNameAssending = IsRoadCodeAssending = IsReportedByAssending = IsReportedNameAssending = IsModeCommunicationAssending = IsVerifiedByAssending = false;
            }
            else if (columnIndex == 8)
            {
                IsModeCommunicationAssending = !IsModeCommunicationAssending;
                IsRefNoAssending = IsRmuNameAssending = IsRoadCodeAssending = IsReportedByAssending = IsReportedNameAssending = IsAttentionToAssending = IsVerifiedByAssending = false;
            }
            else if (columnIndex == 9)
            {
                IsVerifiedByAssending = !IsVerifiedByAssending;
                IsRefNoAssending = IsRmuNameAssending = IsRoadCodeAssending = IsReportedByAssending = IsReportedNameAssending = IsAttentionToAssending = IsModeCommunicationAssending = false;
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
                    GetMyFormXListReports();
                    SetSortOrder(ColumnIndex);
                    iValueRet = 1;
                });
            }
        }


    }
}
