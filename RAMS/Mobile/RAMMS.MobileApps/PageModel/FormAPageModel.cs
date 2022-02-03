using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.MobileApps.Controls;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps
{

    public class FormAPageModel : FreshBasePageModel
    {

        private IRestApi _restApi;

        private string _pagesize;

        private string _totalsize;

        private string _startsize = "1";

        public IUserDialogs _userDialogs;

        public ILocalDatabase _localDatabase;

        public FormAHeaderResponseDTO SelectedFormARowItem { get; set; }

        public FormAHeaderRequestDTO SelectedDeleteFormARowItem { get; set; }

        public ObservableCollection<FormAHeaderResponseDTO> MyBaseFormAList { get; set; }

        private bool isModify;

        private bool isDelete;

        private bool isView;

        public bool IsAdd { get; set; }

        int iResultValue { get; set; }

        public bool IsEmpty { get; set; }

        public float LstViewHeightRequest { get; set; }

        public float SearchbarHeightRequest { get; set; }

        public ListView FormAGridListview { get; set; }

        public string SelectedSection { get; set; }

        public int SelectedSectionName { get; set; }

        public string SelectSectionName { get; set; }

        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRodeCodeListItems { get; set; }

        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRMUListItems { get; set; }

        public ObservableCollection<DDListItems> DDAssetTypeListItems { get; set; }

        public ObservableCollection<DDListItems> MonthListItems { get; set; }

        public ObservableCollection<AssetDDLResponseDTO> SearchListItems { get; set; }

        public ObservableCollection<DDListItems> YearListItems { get; set; }

        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDSectionListItems { get; set; }

        public ObservableCollection<DDListItems> DDMonthListItems { get; set; }

        public ObservableCollection<DDListItems> DDYearListItems { get; set; }

        public List<DropDown> RMUList { get; set; }

        public string Totalsize
        {
            get
            {
                return _totalsize;
            }
            set
            {
                _totalsize = value;
                RaisePropertyChanged("Totalsize");
            }
        }

        public string Pagesize
        {
            get
            {
                return _pagesize;
            }
            set
            {
                _pagesize = value;
                RaisePropertyChanged("Pagesize");
            }
        }

        public string Startsize
        {
            get
            {
                return _startsize;
            }
            set
            {
                _startsize = value;
                RaisePropertyChanged("Startsize");
            }
        }

        private string selectedRMUCode;

        public string SelectedRoadCode { get; set; }

        public string SelectedRoadName { get; set; }

        public string SelectedRMU { get; set; }

        public string SelectedAssetType { get; set; }

        public int? SelectedMonth { get; set; }

        public int? SelectedYear { get; set; }

        private ExtendedPicker roadcode, rmucode, assetype, monthpick, sectionpick, yearpick;

        private EntryControl enctrlFKM, enctrlFM, enctrlTKM, enctrlTM, enctrlSection, enctrlRoadName;

        public int pageno { get; set; }

        private CustomMyPicker cuspicker;

        private Frame advanceFrame;

        public string SmartSearch { get; set; }

        public EditViewModel editViewModel { get; set; }

        public FilteredPagingDefinition<FormASearchGridDTO> GridItems { get; set; }

        public int? ChinageFrom { get; set; }

        public int? ChinageTo { get; set; }


        public FormAPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;

            _restApi = restApi;

            _localDatabase = localDatabase;


        }

        public override async void Init(object initData)
        {
            base.Init(initData);

            editViewModel = new EditViewModel();

            DDRodeCodeListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>();

            DDRMUListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>();

            DDAssetTypeListItems = new ObservableCollection<DDListItems>();

            MonthListItems = new ObservableCollection<DDListItems>();

            DDMonthListItems = new ObservableCollection<DDListItems>();

            DDYearListItems = new ObservableCollection<DDListItems>();

            advanceFrame = CurrentPage.FindByName<Frame>("Toggle");

            var page_searchbar = CurrentPage.FindByName<SearchBar>("formAsearchBar");

            FormAGridListview = CurrentPage.FindByName<ListView>("Gridlist");

            page_searchbar.TextChanged += SearchBar_TextChanged;

            isView = Model.Security.IsView(ModuleNameList.NOD);
            IsAdd = isModify = Model.Security.IsModify(ModuleNameList.NOD);
            isDelete = Model.Security.IsDelete(ModuleNameList.NOD);
            
            //Grid List

            MyBaseFormAList = new ObservableCollection<FormAHeaderResponseDTO>();

            rmucode = CurrentPage.FindByName<ExtendedPicker>("RMUCodePicker");

            assetype = CurrentPage.FindByName<ExtendedPicker>("assetTypePicker");

            roadcode = CurrentPage.FindByName<ExtendedPicker>("rodeCodePicker");

            sectionpick = CurrentPage.FindByName<ExtendedPicker>("SectionPicker");

            enctrlSection = CurrentPage.FindByName<EntryControl>("enctrlSection");

            enctrlRoadName = CurrentPage.FindByName<EntryControl>("enctrlRoadName");

            enctrlFKM = CurrentPage.FindByName<EntryControl>("enctrlFKM");

            enctrlFM = CurrentPage.FindByName<EntryControl>("enctrlFM");

            enctrlTKM = CurrentPage.FindByName<EntryControl>("enctrlTKM");

            enctrlTM = CurrentPage.FindByName<EntryControl>("enctrlTM");

            cuspicker = CurrentPage.FindByName<CustomMyPicker>("scustompicker");


            //DropDown

            //await GetddListDetails("RD_Code");

            //await GetddListDetails("RMU");

            GetLandingListDetails();

            await GetddListDetails("FormA_Assets");

            // await GetddListDetails("Section Code");

            await GetddListDetails("Month");

            await GetddListDetails("Year");

            //rmucode.ItemsSource = DDRMUListItems.Select((AssetDDLResponseDTO.DropDown arg) => arg.Text).ToList();


            //rmucode.ItemsSource = DDRMUListItems.Select((AssetDDLResponseDTO.DropDown arg) => arg.Text).ToList();



            //await GetRMUddListDetails();

            //
            //enctrlmonth
            //enctrlmonth = CurrentPage.FindByName<EntryControl>("enctrlmonth");

            //enctrlyear = CurrentPage.FindByName<EntryControl>("enctrlyear");


        }


        private void Dropdown()
        {
            rmucode.ItemsSource = DDRMUListItems.Select((AssetDDLResponseDTO.DropDown arg) => arg.Text).ToList();

            rmucode.SelectedIndexChanged += (s, e) =>
            {
                if (rmucode.SelectedIndex != -1)
                {
                    if (roadcode.SelectedIndex != -1)
                    {
                        roadcode.SelectedIndex = -1;
                        SelectedRoadCode = SelectedRoadName = string.Empty;
                    }

                    if (sectionpick.SelectedIndex != -1)
                    {
                        sectionpick.SelectedIndex = -1;
                        SelectedSection = "";
                        SelectSectionName = "";
                        SelectedSectionName = 0;
                    }

                    SelectedRMU = DDRMUListItems[rmucode.SelectedIndex].Text.ToString();
                    selectedRMUCode = DDRMUListItems[rmucode.SelectedIndex].Value.ToString();

                    GetLandingListDetails("RMU");

                }


            };




            assetype.ItemsSource = DDAssetTypeListItems.Select((DDListItems arg) => arg.Text).ToList();

            assetype.SelectedIndexChanged += (s, e) =>
            {
                if (assetype.SelectedIndex != -1)
                {
                    SelectedAssetType = DDAssetTypeListItems[assetype.SelectedIndex].Value.ToString();
                }
            };




            //sectionpick.ItemsSource = DDSectionListItems.Select((AssetDDLResponseDTO.DropDown arg) => arg.Text).ToList();

            sectionpick.SelectedIndexChanged += (s, e) =>
            {
                if (sectionpick.SelectedIndex != -1)
                {
                    if (roadcode.SelectedIndex != -1)
                    {
                        roadcode.SelectedIndex = -1;
                        SelectedRoadCode = SelectedRoadName = string.Empty;
                    }

                    SelectedSectionName = Convert.ToInt32(DDSectionListItems[sectionpick.SelectedIndex].Value);

                    //SelectedSectionName = DDSectionListItems[sectionpick.SelectedIndex].Text;

                    string[] str = DDSectionListItems[sectionpick.SelectedIndex].Text.Split('-');

                    SelectSectionName = str[1].ToString();

                    GetLandingListDetails("Section");
                }

            };


            cuspicker.SelectedIndexChanged += (s, e) =>
            {
                try
                {
                    if (cuspicker.SelectedIndex != -1)
                    {
                        iValueRet = 1;
                        if (cuspicker.SelectedItem.ToString() == "10 rows")
                        {
                            pageno = 10;
                            GetMyFormAListReports("Grid");

                        }

                        else if (cuspicker.SelectedItem.ToString() == "25 rows")
                        {
                            pageno = 25;
                            GetMyFormAListReports("Grid");
                            return;

                        }

                        else if (cuspicker.SelectedItem.ToString() == "50 rows")
                        {
                            pageno = 50;
                            GetMyFormAListReports("Grid");
                            return;
                        }

                        else if (cuspicker.SelectedItem.ToString() == "100 rows")
                        {
                            pageno = 100;
                            GetMyFormAListReports("Grid");
                            return;
                        }
                        else if (cuspicker.SelectedItem.ToString() == "500 rows")
                        {
                            pageno = 500;
                            GetMyFormAListReports("Grid");
                            return;
                        }
                        else
                        {
                            pageno = 1000;
                            GetMyFormAListReports("Grid");
                            return;
                        }
                    }
                }
                catch { }


            };





            ///roadcode.ItemsSource = DDRodeCodeListItems.Select((AssetDDLResponseDTO.DropDown arg) => arg.Text).ToList();

            roadcode.SelectedIndexChanged += (s, e) =>
            {
                if (roadcode.SelectedIndex != -1)
                {
                    SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                    string[] str = DDRodeCodeListItems[roadcode.SelectedIndex].Text.Split('-');

                    SelectedRoadName = str[1].ToString();
                }

                //GetLandingListDetails();
            };


            monthpick = CurrentPage.FindByName<ExtendedPicker>("monthPicker");

            monthpick.ItemsSource = DDMonthListItems.Select((DDListItems arg) => arg.Text).ToList();

            monthpick.SelectedIndexChanged += (s, e) =>
            {
                if (monthpick.SelectedIndex != -1)
                {
                    SelectedMonth = Convert.ToInt32(DDMonthListItems[monthpick.SelectedIndex].Value);
                }
                //    //SelectedMonth = 
            };


            yearpick = CurrentPage.FindByName<ExtendedPicker>("yearPicker");

            yearpick.ItemsSource = DDYearListItems.Select((DDListItems arg) => arg.Text).ToList();

            yearpick.SelectedIndexChanged += (s, e) =>
            {
                if (yearpick.SelectedIndex != -1)
                {
                    SelectedYear = Convert.ToInt32(DDYearListItems[yearpick.SelectedIndex].Value);
                }


            };


        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                MyBaseFormAList = await GetMyFormAListReports("Grid");
                //foreach (var ivalue in MyBaseFormAList)
                //{
                //    if (ivalue.SubmitSts == true)
                //    {
                //        Status = "Submitted";
                //    }
                //    else
                //    {
                //        Status = "Saved";

                //    }
                //}
                FormAGridListview.ItemsSource = MyBaseFormAList;
            }
            else
            {
                // FormAGridListview.ItemsSource = MyBaseFormAList.Where(x => x.RoadCode.ToLower().Contains(e.NewTextValue.ToLower())).ToList();
            }
        }


        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            if (ChinageFrom != 0 && ChinageFrom != null)
                ChinageFrom = Convert.ToInt32(enctrlFKM.Text + enctrlFM.Text);

            if (ChinageTo != 0 && ChinageTo != null)
                ChinageTo = Convert.ToInt32(enctrlTKM.Text + enctrlTM.Text);


            if (pageno == 0 || pageno == null)
                pageno = 10;

            Dropdown();

            MyBaseFormAList = await GetMyFormAListReports("Grid");
        }


        public async Task<ObservableCollection<FormAHeaderResponseDTO>> GetMyFormAListReports(string Type)
        {
            _userDialogs.ShowLoading("Loading");
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    //if (Type == "Smartsearch")
                    //{
                    //    GridItems = new FilteredPagingDefinition<FormASearchGridDTO>()
                    //    {
                    //        StartPageNo = 1,

                    //        RecordsPerPage = pageno,

                    //        sortOrder = "Ascending",

                    //        Filters = new FormASearchGridDTO() { SmartInputValue = SmartSearch },
                    //    };
                    //}
                    //else if (Type == "Detailsearch")
                    {

                        string scc = string.Empty;
                        if (SelectedSectionName > 0)
                            scc = SelectSectionName;
                        string rmuSearchcode = string.Empty;
                        if (selectedRMUCode == "MIRI")
                            rmuSearchcode = "MRI";
                        if (selectedRMUCode == "Batu Niah")
                            rmuSearchcode = "BTN";
                        if (SmartSearch != null && SmartSearch != "")
                            SmartSearch = SmartSearch.Trim();

                        //GetSortOrder(ColumnIndex);

                        GridItems = new FilteredPagingDefinition<FormASearchGridDTO>()
                        {
                            StartPageNo = 0,

                            RecordsPerPage = pageno,

                            sortOrder = SortOrder,

                            ColumnIndex = ColumnIndex,

                            Filters = new FormASearchGridDTO()
                            {
                                Road_Code = SelectedRoadCode,
                                RMU = rmuSearchcode,
                                Asset_GroupCode = SelectedAssetType,
                                section = scc,
                                Month = SelectedMonth,
                                Year = SelectedYear,

                                ChinageFromKm = ConvertToNullableInt(enctrlFKM.Text),
                                ChinageFromM = ConvertToNullableInt(enctrlFM.Text),
                                ChinageToKm = ConvertToNullableInt(enctrlTKM.Text),
                                ChinageToM = ConvertToNullableInt(enctrlTM.Text),
                                SmartInputValue = SmartSearch
                            }
                        };
                    }
                    //else if (Type == "PageNation")
                    //{
                    //    GridItems = new FilteredPagingDefinition<FormASearchGridDTO>()
                    //    {
                    //        StartPageNo = 1,

                    //        RecordsPerPage = pageno,

                    //        Filters = new FormASearchGridDTO() { Road_Code = SelectedRoadCode.ToString(), RMU = SelectedRMU, Asset_Type = SelectedAssetType, Month = SelectedMonth, ChinageFromKm = ChinageFrom, ChinageToKm = ChinageTo },
                    //    };
                    //}
                    //else
                    //{
                    //    GridItems = new FilteredPagingDefinition<FormASearchGridDTO>()
                    //    {
                    //        StartPageNo = 1,

                    //        RecordsPerPage = pageno,

                    //        sortOrder = "Ascending",

                    //        Filters = new FormASearchGridDTO(),
                    //    };
                    //}

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

                    var response = await _restApi.GetGridFormALandingGridview(GridItems);

                    if (response.success)
                    {

                        Totalsize = response.data.TotalRecords.ToString();

                        Startsize = response.data.TotalRecords == 0 ? "0" : (response.data.PageNo + 1).ToString();

                        Pagesize = response.data.FilteredRecords.ToString();

                        MyBaseFormAList = new ObservableCollection<FormAHeaderResponseDTO>(response.data.PageResult);

                        foreach (var ivalue in MyBaseFormAList)
                        {
                            if (ivalue.SubmitSts == true)
                            {
                                ivalue.Status = "Submitted";
                            }
                            else
                            {
                                ivalue.Status = "Saved";

                            }
                        }

                        //SelectedRoadCode = string.Empty;
                        //SelectedRMU = string.Empty;
                        //SelectedAssetType = string.Empty;
                        //SelectedSectionName = string.Empty;
                        //SelectedMonth = null;
                        //SelectedYear = null;
                        //ChinageFrom = null;
                        //ChinageTo = null;
                        //SelectedSection = 0;

                        return MyBaseFormAList;
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return MyBaseFormAList;
                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");

                IsEmpty = MyBaseFormAList.Count == 0;
                LstViewHeightRequest = MyBaseFormAList.Count * 40;
                return MyBaseFormAList;
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }

            return new ObservableCollection<FormAHeaderResponseDTO>();
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
                        if (ddtype == "RD_Code")
                        {
                            //DDRodeCodeListItems = new ObservableCollection<DDListItems>(response.data);

                            //return DDRodeCodeListItems;

                        }
                        else if (ddtype == "RMU")
                        {
                            // DDRMUListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>(response.data);

                            // return DDRMUListItems;
                        }
                        else if (ddtype == "FormA_Assets")
                        {
                            DDAssetTypeListItems = new ObservableCollection<DDListItems>(response.data);

                            return DDAssetTypeListItems;
                        }
                        else if (ddtype == "Section Code")
                        {
                            //DDSectionListItems = new ObservableCollection<DDListItems>(response.data);
                            //return DDAssetTypeListItems;
                        }
                        else if (ddtype == "Month")
                        {
                            DDMonthListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDMonthListItems;
                        }
                        else if (ddtype == "Year")
                        {
                            DDYearListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDYearListItems;
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
                //_userDialogs.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }



        public async Task<ObservableCollection<DDListItems>> GetRMUddListDetails()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.GetDDMonthList();
                    if (response.success)
                    {
                        MonthListItems = new ObservableCollection<DDListItems>(response.data);
                        return MonthListItems;
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return MonthListItems;
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
                //_userDialogs.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }




        public async void GetLandingListDetails(string propName = null)
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {

                    var strQuery = new AssetDDLRequestDTO
                    {
                        RMU = SelectedRMU,

                        RdCode = SelectedRoadCode,

                        SectionCode = SelectedSectionName

                    };


                    var response = await _restApi.GetLandingDropDown(strQuery);

                    if (response.success)
                    {
                        if (response.data.RMU != null)
                        {
                            DDRMUListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>(response.data.RMU);
                            rmucode.ItemsSource = DDRMUListItems.Select((AssetDDLResponseDTO.DropDown arg) => arg.Text).ToList();

                        }

                        if (propName != "Section" && response.data.Section?.Count > 0)
                        {
                            DDSectionListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>(response.data.Section);
                            sectionpick.ItemsSource = DDSectionListItems.Select((AssetDDLResponseDTO.DropDown arg) => arg.Text).ToList();
                            //enctrlSection.Text = string.Empty;
                            //SelectedSectionName = null;
                            //if (sectionpick == null)
                            //{

                            //    sectionpick.ItemsSource = DDSectionListItems.Select((AssetDDLResponseDTO.DropDown arg) => arg.Text).ToList();
                            //}

                        }
                        if (response.data.RdCode != null)
                        {
                            DDRodeCodeListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>(response.data.RdCode);
                            roadcode.ItemsSource = DDRodeCodeListItems.Select((AssetDDLResponseDTO.DropDown arg) => arg.Text).ToList();
                            SelectedRoadName = string.Empty;
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

            //Dropdown();


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
                    _userDialogs.ShowLoading("Loading");
                    try
                    {
                        if (CrossConnectivity.Current.IsConnected)
                        {
                            ColumnIndex = Convert.ToInt32(obj);
                            {
                                string scc = string.Empty;
                                if (SelectedSectionName > 0)
                                    scc = SelectSectionName;
                                string rmuSearchcode = string.Empty;
                                if (selectedRMUCode == "MIRI")
                                    rmuSearchcode = "MRI";
                                if (selectedRMUCode == "Batu Niah")
                                    rmuSearchcode = "BTN";
                                if (SmartSearch != null && SmartSearch != "")
                                    SmartSearch = SmartSearch.Trim();

                                GetSortOrder(ColumnIndex);

                                GridItems = new FilteredPagingDefinition<FormASearchGridDTO>()
                                {
                                    StartPageNo = 0,

                                    RecordsPerPage = pageno,

                                    sortOrder = SortOrder,

                                    ColumnIndex = ColumnIndex,

                                    Filters = new FormASearchGridDTO()
                                    {
                                        Road_Code = SelectedRoadCode,
                                        RMU = rmuSearchcode,
                                        Asset_GroupCode = SelectedAssetType,
                                        section = scc,
                                        Month = SelectedMonth,
                                        Year = SelectedYear,

                                        ChinageFromKm = ConvertToNullableInt(enctrlFKM.Text),
                                        ChinageFromM = ConvertToNullableInt(enctrlFM.Text),
                                        ChinageToKm = ConvertToNullableInt(enctrlTKM.Text),
                                        ChinageToM = ConvertToNullableInt(enctrlTM.Text),
                                        SmartInputValue = SmartSearch
                                    }
                                };
                            }

                            var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

                            var response = await _restApi.GetGridFormALandingGridview(GridItems);

                            if (response.success)
                            {
                                SetSortOrder(ColumnIndex);

                                iValueRet = 1;
                                Totalsize = response.data.TotalRecords.ToString();

                                Startsize = response.data.TotalRecords == 0 ? "0" : (response.data.PageNo + 1).ToString();

                                Pagesize = response.data.FilteredRecords.ToString();

                                MyBaseFormAList = new ObservableCollection<FormAHeaderResponseDTO>(response.data.PageResult);

                                foreach (var ivalue in MyBaseFormAList)
                                {
                                    if (ivalue.SubmitSts == true)
                                    {
                                        ivalue.Status = "Submitted";
                                    }
                                    else
                                    {
                                        ivalue.Status = "Saved";

                                    }
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
                });
            }
        }

        
        public ICommand FormAListItemTappedCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    SelectedFormARowItem = (FormAHeaderResponseDTO)obj;
                });
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    _userDialogs.ShowLoading("Loading");
                    iValueRet = 1;
                    if (!string.IsNullOrEmpty(SmartSearch))
                    {
                        await GetMyFormAListReports("Smartsearch");
                        //MyBaseFormAList = await GetMyFormAListReports("Smartsearch");
                        FormAGridListview.ItemsSource = MyBaseFormAList;
                    }
                    else
                    {

                        //SelectedMonth = Convert.ToInt32(enctrlmonth.Text);
                        //SelectedYear = Convert.ToInt32(enctrlyear.Text);

                        if (SelectedYear != null && SelectedYear != 0)
                            SelectedYear = Convert.ToInt32(yearpick.SelectedItem);

                        if (SelectedMonth != null && SelectedMonth != 0)
                            SelectedMonth = Convert.ToInt32(monthpick.SelectedItem);

                        //if (ChinageFrom != 0 && ChinageFrom != null)
                        //    ChinageFrom = Convert.ToInt32(enctrlFKM.Text + enctrlFM.Text);

                        //if (ChinageTo != 0 && ChinageTo != null)
                        //    ChinageTo = Convert.ToInt32(enctrlTKM.Text + enctrlTM.Text);

                        await GetMyFormAListReports("Detailsearch");
                        //MyBaseFormAList = await GetMyFormAListReports("Detailsearch");
                        FormAGridListview.ItemsSource = MyBaseFormAList;
                    }
                });
            }
        }

        public ICommand PageNationCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    _userDialogs.ShowLoading("Loading");

                    await GetMyFormAListReports("PageNation");
                });
            }
        }

        public ICommand ClickMeActionCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    var SelectedFormARowItem = (FormAHeaderResponseDTO)obj;
                    
                    var actionResult = "";

                    string view = isView ? "View" : "";
                    string delete = isDelete ? "Delete" : "";
                    string edit = isModify && SelectedFormARowItem.Status.ToLower() != "submitted" ? "Edit" : "";

                    string[] permissions = new string[] { edit, view, delete };
                    permissions = permissions.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                    actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, permissions);

                    if (actionResult == "Delete")
                    {
                        var actionResult1 = await UserDialogs.Instance.ConfirmAsync("Are you sure want to delete this record?", "RAMS", "Yes", "No");
                        if (actionResult1)
                        {
                            editViewModel.HdrFahPkRefNo = SelectedFormARowItem.No;
                            await DeleteHeaderdetails(editViewModel.HdrFahPkRefNo);
                            MyBaseFormAList = await GetMyFormAListReports("Grid");
                            return;
                        }
                    }
                    if (actionResult == "Edit")
                    {
                        editViewModel.Type = "Edit";
                        editViewModel.HdrFahPkRefNo = SelectedFormARowItem.No;
                        //editViewModel.HdrFahPkRefNo = SelectedFormARowItem.FahRmu;

                        await CoreMethods.PushPageModel<FormADetailsPageModel>(editViewModel);
                    }
                    else if (actionResult == "View")
                    {
                        editViewModel.Type = "View";
                        editViewModel.HdrFahPkRefNo = SelectedFormARowItem.No;
                        //editViewModel.HdrFahPkRefNo = SelectedFormARowItem.FahRmu;

                        await CoreMethods.PushPageModel<FormADetailsPageModel>(editViewModel);
                    }
                });
            }
        }


        public ICommand ClickClearActionCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    if (obj.ToString() == "Clear")
                    {
                        roadcode.SelectedIndex = -1;
                        SelectedRoadCode = string.Empty;
                        rmucode.SelectedIndex = -1;
                        //SelectedRMU = string.Empty;
                        assetype.SelectedIndex = -1;
                        SelectedAssetType = string.Empty;
                        monthpick.SelectedIndex = -1;
                        //SelectedMonth = string.Empty;
                    }
                    if (advanceFrame.IsVisible)
                    {
                        advanceFrame.IsVisible = false;
                    }
                    else
                    {
                        advanceFrame.IsVisible = true;
                    }
                });
            }
        }

        public ICommand AddCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    editViewModel.Type = "Add";
                    App.ReturnType = "Add";
                    editViewModel.HdrFahPkRefNo = 0;
                    await CoreMethods.PushPageModel<FormADetailsPageModel>(editViewModel);
                });
            }
        }


        public ICommand ClearCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    try
                    {

                        if (rmucode.SelectedIndex != -1)
                        {
                            rmucode.SelectedIndex = -1;
                            selectedRMUCode = SelectedRMU = string.Empty;
                        }

                        if (roadcode.SelectedIndex != -1)
                        {
                            roadcode.SelectedIndex = -1;
                            SelectedRoadCode = SelectedRoadName = string.Empty;

                        }

                        if (sectionpick.SelectedIndex != -1)
                        {
                            sectionpick.SelectedIndex = -1;
                            SelectedSection = "";
                            SelectSectionName = "";
                            SelectedSectionName = 0;
                        }

                        if (monthpick.SelectedIndex != -1)
                        {
                            monthpick.SelectedIndex = -1;
                            SelectedMonth = null;
                        }

                        if (yearpick.SelectedIndex != -1)
                        {
                            yearpick.SelectedIndex = -1;
                            SelectedYear = null;
                        }


                        if (assetype.SelectedIndex != -1)
                        {
                            assetype.SelectedIndex = -1;
                            SelectedAssetType = string.Empty;

                        }

                        enctrlFKM.Text = "";

                        enctrlFM.Text = "";

                        enctrlTKM.Text = "";

                        enctrlTM.Text = "";

                        SmartSearch = "";

                        await GetMyFormAListReports("Detailsearch");
                        //MyBaseFormAList = await GetMyFormAListReports("Detailsearch");
                        FormAGridListview.ItemsSource = MyBaseFormAList;

                        GetLandingListDetails();
                    }
                    catch
                    { }

                });
            }
        }



        int iValueRet = 1;

        public ICommand btnPreviousCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    try
                    {
                        _userDialogs.ShowLoading("loading");

                        if (iValueRet > 1)
                            iValueRet = iValueRet - 1;

                        await ButtonPagination("Smartsearch", iValueRet);
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


        private async Task<int> ButtonPagination(string Type, int Value)
        {

            //if (Type == "Smartsearch")
            //{
            //    GridItems = new FilteredPagingDefinition<FormASearchGridDTO>()
            //    {
            //        StartPageNo = iValueRet,

            //        RecordsPerPage = pageno,

            //        Filters = new FormASearchGridDTO() { SmartInputValue = SmartSearch },
            //    };
            //}
            //else if (Type == "Detailsearch")
            {

                string scc = string.Empty;
                if (SelectedSectionName > 0)
                    scc = SelectSectionName;
                string rmuSearchcode = string.Empty;
                if (selectedRMUCode == "MIRI")
                    rmuSearchcode = "MRI";
                if (selectedRMUCode == "Batu Niah")
                    rmuSearchcode = "BTN";
                if (SmartSearch != null && SmartSearch != "")
                    SmartSearch = SmartSearch.Trim();

                //GetSortOrder(ColumnIndex);

                GridItems = new FilteredPagingDefinition<FormASearchGridDTO>()
                {
                    StartPageNo = (iValueRet - 1) * pageno,

                    RecordsPerPage = pageno,

                    sortOrder = SortOrder,

                    ColumnIndex = ColumnIndex,

                    Filters = new FormASearchGridDTO()
                    {
                        Road_Code = SelectedRoadCode,
                        RMU = rmuSearchcode,
                        Asset_GroupCode = SelectedAssetType,
                        section = scc,
                        Month = SelectedMonth,
                        Year = SelectedYear,
                        ChinageFromKm = ConvertToNullableInt(enctrlFKM.Text),
                        ChinageFromM = ConvertToNullableInt(enctrlFM.Text),
                        ChinageToKm = ConvertToNullableInt(enctrlTKM.Text),
                        ChinageToM = ConvertToNullableInt(enctrlTM.Text),
                        SmartInputValue = SmartSearch
                    }
                };
            }

            //else
            //{
            //    GridItems = new FilteredPagingDefinition<FormASearchGridDTO>()
            //    {
            //        StartPageNo = iValueRet,

            //        RecordsPerPage = pageno,

            //        Filters = new FormASearchGridDTO(),
            //    };
            //}

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

            var response = await _restApi.GetGridFormALandingGridview(GridItems);

            if (response.success)
            {


                Totalsize = response.data.TotalRecords.ToString();

                if (response.data.TotalRecords == 0)
                    Startsize = (response.data.PageNo).ToString();
                else
                    Startsize = (response.data.PageNo + 1).ToString();

                Pagesize = (response.data.PageNo + response.data.FilteredRecords).ToString();

                MyBaseFormAList = new ObservableCollection<FormAHeaderResponseDTO>(response.data.PageResult);

                foreach (var ivalue in MyBaseFormAList)
                {
                    if (ivalue.SubmitSts == true)
                    {
                        ivalue.Status = "Submitted";
                    }
                    else
                    {
                        ivalue.Status = "Saved";

                    }
                }

                //SelectedRoadCode = string.Empty;

                //SelectedRMU = string.Empty;

                //SelectedAssetType = string.Empty;

                //SelectedSectionName = string.Empty;

                //SelectedMonth = null;

                //SelectedYear = null;

                //ChinageFrom = null;

                //ChinageTo = null;

                //SelectedSection = 0;

                //SelectedRoadCode = string.Empty;

                //SelectedRMU = string.Empty;

                //SelectedYear = null;

            }

            return 1;
        }

        public ICommand btnNextCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    try
                    {
                        _userDialogs.ShowLoading("loading");

                        if (iValueRet <= (Convert.ToInt32(_totalsize) / pageno))

                            iValueRet = iValueRet + 1;

                        await ButtonPagination("Smartsearch", iValueRet);
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



        public async Task<int> DeleteHeaderdetails(int HeaderCode)

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

                    var response = await _restApi.DeleteHeader(HeaderCode);


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
                        _userDialogs.Toast(response.errorMessage);

                    return iResultValue;
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
                // _userdialogs.hideloading();
            }
            return iResultValue;
        }



        private int? ConvertToNullableInt(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return Convert.ToInt32(value);
        }

        protected override async void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            ClearCommand.Execute(null);
        }

    }


}