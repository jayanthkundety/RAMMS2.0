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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps.PageModel
{
    public class FormDPageModel : FreshBasePageModel
    {

        private IRestApi _restApi;



        public IUserDialogs _userDialogs;

        public ILocalDatabase _localDatabase;

        int iResultValue { get; set; }

        public bool IsEmpty { get; set; }
        public float LstViewHeightRequest { get; set; }

        public float SearchbarHeightRequest { get; set; }

        public ListView FormDGridListview { get; set; }

        public FormDHeaderResponseDTO SelectedFormDRowItem { get; set; }

        public int SelectedSection { get; set; }

        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDSectionListItems { get; set; }

        //public ObservableCollection<DDListItems> DDMonthListItems { get; set; }

        //public ObservableCollection<DDListItems> DDYearListItems { get; set; }

        public List<DropDown> RMUList { get; set; }

        public string SelectedDate { get; set; }

        private bool isModify;

        private bool isDelete;

        private bool isView;

        public bool IsAdd { get; set; }

        public string totalsize { get; set; }
        public string pagesize { get; set; }
        public string SelectedRoadCode { get; set; }
        public string SelectedRMU { get; set; }
        public string SelectedWeekNo { get; set; }

        public string SelectWeekday { get; set; }
        public int? SelectedMonth { get; set; }
        public int? SelectedYear { get; set; }
        public string SmartSearch { get; set; }

        public string Detailsearch { get; set; }

        public EditViewModel editViewModel { get; set; }

        public FilteredPagingDefinition<FormDSearchGridDTO> GridItems { get; set; }

        public ObservableCollection<FormDHeaderResponseDTO> MyBaseFormDList { get; set; }

        public ObservableCollection<DDListItems> WeekListItems { get; set; }

        private SearchBar searchBar;

        public int pageno { get; set; }

        public int startPage { get; set; }

        private CustomMyPicker cuspicker;

        private ExtendedPicker sectioncode, rmucode;

        CustomDatePicker dtDate;

        private ObservableCollection<DDListItems> DDRodeCodeListItems { get; set; }

        private ObservableCollection<DDListItems> DDRMUListItems { get; set; }


        private ObservableCollection<DDListItems> DDAssetTypeListItems { get; set; }


        private ObservableCollection<DDListItems> MonthListItems { get; set; }


        Button btnSave, btnCancel, btnSubmit;


        public FormDPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;

            _restApi = restApi;

            _localDatabase = localDatabase;


        }

        public override async void Init(object initData)
        {
            base.Init(initData);

            DDRodeCodeListItems = new ObservableCollection<DDListItems>();

            DDRMUListItems = new ObservableCollection<DDListItems>();

            DDAssetTypeListItems = new ObservableCollection<DDListItems>();

            MonthListItems = new ObservableCollection<DDListItems>();

            FormDGridListview = CurrentPage.FindByName<ListView>("Gridlist");

            dtDate = CurrentPage.FindByName<CustomDatePicker>("datepick");

            searchBar = CurrentPage.FindByName<SearchBar>("formDsearchBar");

            cuspicker = CurrentPage.FindByName<CustomMyPicker>("scustompicker");

            rmucode = CurrentPage.FindByName<ExtendedPicker>("RMUCodePicker");

            sectioncode = CurrentPage.FindByName<ExtendedPicker>("sectionCodePicker");

            searchBar.TextChanged += SearchBar_TextChanged;

            editViewModel = new EditViewModel();

            //Grid List

            isView = Model.Security.IsView(ModuleNameList.Emergency_Response_Team);
            IsAdd = isModify = Model.Security.IsModify(ModuleNameList.Emergency_Response_Team);
            isDelete = Model.Security.IsDelete(ModuleNameList.Emergency_Response_Team);

            MyBaseFormDList = new ObservableCollection<FormDHeaderResponseDTO>();

            //DropDown

            //await GetddListDetails("Section Code");

            //await GetddListDetails("RMU");

            await GetTypeCode("RMU");

            await GetSectionByRmu(null);

            //await GetSectionByRmu("MRI");

            //await GetddListDetails("Month");

            //await GetddListDetails("Year");

            //await GetWeekddListDetails();

            startPage = 1;

            //Dropdown();

        }



        private void Dropdown()
        {

            try
            {
                rmucode.ItemsSource = DDRMUListItems.Select((DDListItems arg) => arg.Value + " - " + arg.Text).ToList();

                rmucode.SelectedIndexChanged += async (s, e) =>
                {
                    if (rmucode.SelectedIndex != -1)
                    {
                        SelectedRMU = DDRMUListItems[rmucode.SelectedIndex].Value.ToString();

                        await GetSectionByRmu(SelectedRMU);
                    }

                };


                sectioncode.SelectedIndexChanged += (s, e) =>
                {
                    if (sectioncode.SelectedIndex != -1)
                    {
                        SelectedRoadCode = DDRodeCodeListItems[sectioncode.SelectedIndex].Value.ToString();
                    }

                };

                cuspicker.SelectedIndexChanged += async (s, e) =>
                {
                    try
                    {

                        if (cuspicker.SelectedIndex != -1)
                        {

                            if (cuspicker.SelectedItem.ToString() == "10 rows")
                            {
                                iValueRet = 1;
                                pageno = 10;
                                await GetMyFormDListReports("Grid");
                                FormDGridListview.ItemsSource = MyBaseFormDList;

                                return;
                            }

                            else if (cuspicker.SelectedItem.ToString() == "25 rows")
                            {
                                iValueRet = 1;
                                pageno = 25;
                                await GetMyFormDListReports("Grid");
                                FormDGridListview.ItemsSource = MyBaseFormDList;

                                return;

                            }

                            else if (cuspicker.SelectedItem.ToString() == "50 rows")
                            {
                                iValueRet = 1;
                                pageno = 50;
                                await GetMyFormDListReports("Grid");
                                FormDGridListview.ItemsSource = MyBaseFormDList;

                                return;
                            }

                            else if (cuspicker.SelectedItem.ToString() == "100 rows")
                            {
                                iValueRet = 1;
                                pageno = 100;
                                await GetMyFormDListReports("Grid");
                                FormDGridListview.ItemsSource = MyBaseFormDList;
                                return;
                            }
                            else if (cuspicker.SelectedItem.ToString() == "500 rows")
                            {
                                iValueRet = 1;
                                pageno = 500;
                                await GetMyFormDListReports("Grid");
                                FormDGridListview.ItemsSource = MyBaseFormDList;
                                return;
                            }
                            else
                            {
                                iValueRet = 1;
                                pageno = 1000;
                                await GetMyFormDListReports("Grid");
                                FormDGridListview.ItemsSource = MyBaseFormDList;
                                return;
                            }
                        }
                    }
                    catch { }

                };






                //monthpick = CurrentPage.FindByName<ExtendedPicker>("monthPicker");

                //monthpick.ItemsSource = DDMonthListItems.Select((DDListItems arg) => arg.Text).ToList();

                //monthpick.SelectedIndexChanged += (s, e) =>
                //{
                //    if (monthpick.SelectedIndex != -1)
                //    {
                //        SelectedMonth = Convert.ToInt32(DDMonthListItems[monthpick.SelectedIndex].Value);
                //    }
                //    //    //SelectedMonth = 
                //};


                //yearpick = CurrentPage.FindByName<ExtendedPicker>("yearPicker");

                //yearpick.ItemsSource = DDYearListItems.Select((DDListItems arg) => arg.Text).ToList();

                //yearpick.SelectedIndexChanged += (s, e) =>
                //{
                //    if (yearpick.SelectedIndex != -1)
                //    {
                //        SelectedYear = Convert.ToInt32(DDYearListItems[yearpick.SelectedIndex].Value);
                //    }


                //};
            }
            catch { }

        }


        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                FormDGridListview.ItemsSource = MyBaseFormDList;
            }
            else
            {
                // FormAGridListview.ItemsSource = MyBaseFormAList.Where(x => x.RoadCode.ToLower().Contains(e.NewTextValue.ToLower())).ToList();
            }
        }


        public async Task<ObservableCollection<DDListItems>> GetWeekddListDetails()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.GetDDWeekList();
                    if (response.success)
                    {
                        WeekListItems = new ObservableCollection<DDListItems>(response.data);
                        return WeekListItems;
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return WeekListItems;
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
            return new ObservableCollection<DDListItems>();
        }


        public async Task<ObservableCollection<DDListItems>> GetSectionByRmu(string rmu)
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

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
                        _userDialogs.Toast(response.errorMessage);

                    return DDRodeCodeListItems;
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
            return new ObservableCollection<DDListItems>();
        }


        public async Task<ObservableCollection<DDListItems>> GetTypeCode(string ddlType)
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

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
                        _userDialogs.Toast(response.errorMessage);

                    return DDRMUListItems;
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
            return new ObservableCollection<DDListItems>();
        }




        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            if (pageno == 0 || pageno == null)
                pageno = 10;

            MyBaseFormDList = await GetMyFormDListReports("Grid");

            Dropdown();
        }




        protected override void ViewIsDisappearing(object sender, EventArgs e)
        {
            base.ViewIsDisappearing(sender, e);
            ClearCommand.Execute(null);
        }


        public async Task<ObservableCollection<FormDHeaderResponseDTO>> GetMyFormDListReports(string Type)
        {
            _userDialogs.ShowLoading("Loading");
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    if (Type == "Smartsearch")
                    {
                        if (SmartSearch != null && SmartSearch != "")
                            SmartSearch = SmartSearch.Trim();
                        GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
                        {
                            StartPageNo = 0,

                            RecordsPerPage = pageno,

                            sortOrder = "0",

                            Filters = new FormDSearchGridDTO() { SmartInputValue = SmartSearch },
                        };
                    }
                    else if (Type == "Detailsearch")
                    {
                        GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
                        {
                            StartPageNo = 0,

                            RecordsPerPage = pageno,

                            sortOrder = "0",


                            Filters = new FormDSearchGridDTO() { Road_Code = SelectedRoadCode, RMU = SelectedRMU, WeekNo = SelectedWeekNo, Year = SelectedYear, WeekDay = SelectWeekday }
                        };
                    }
                    else
                    {
                        GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
                        {
                            StartPageNo = 0,

                            RecordsPerPage = pageno,

                            sortOrder = "0",

                            Filters = new FormDSearchGridDTO(),
                        };
                    }

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

                    var response = await _restApi.GetFormDGridList(GridItems);

                    if (response.success)
                    {

                        startPage = response.data.TotalRecords == 0 ? 0 : (response.data.PageNo + 1);

                        totalsize = response.data.TotalRecords.ToString();

                        pagesize = response.data.FilteredRecords.ToString();

                        MyBaseFormDList = new ObservableCollection<FormDHeaderResponseDTO>(response.data.PageResult);

                        FormDGridListview.ItemsSource = MyBaseFormDList;

                        foreach (var ivalue in MyBaseFormDList)
                        {
                            if (ivalue.SubmitSts == true)
                            {
                                ivalue.SubAuthStatus = "Submitted";
                            }
                            else
                            {
                                ivalue.SubAuthStatus = "Saved";

                            }
                        }




                        //SelectedRoadCode = string.Empty;

                        //SelectedRMU = string.Empty;

                        //SelectedWeekNo = string.Empty;

                        //SelectedYear = null;

                        //SelectWeekday = string.Empty;

                        return MyBaseFormDList;
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return MyBaseFormDList;
                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");


                IsEmpty = MyBaseFormDList.Count == 0;

                LstViewHeightRequest = MyBaseFormDList.Count * 40;

                return MyBaseFormDList;
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }

            return new ObservableCollection<FormDHeaderResponseDTO>();
        }




        private void splitdate(DateTime searchByDate)
        {
            //searchByDate = searchByDate.Substring(0, 10);


            if (searchByDate != null)// && searchByDate.IndexOf("/") >= 0)
            {
                //string years = searchByDate.Split('/')[2].Replace(" ","");
                //string month = searchByDate.Split('/')[1];
                //string day = searchByDate.Split('/')[0];
                //DateTime dt = new DateTime(Convert.ToInt32(years), Convert.ToInt32(month), Convert.ToInt32(day));
                int WeekNo = GetWeek(searchByDate);
                SelectedWeekNo = Convert.ToString(WeekNo);
                SelectedYear = searchByDate.Year;
                SelectWeekday = searchByDate.ToString("dddd");
            }

        }


        private static int GetWeek(DateTime date)
        {
            int weeknumber = 0;
            if ((date.DayOfYear % 7) == 0)
            {
                weeknumber = date.DayOfYear / 7;
            }
            else
            {
                double num = date.DayOfYear;
                double number = num / 7.0;
                weeknumber = (int)Math.Round(number);
            }
            return weeknumber;

            //date = date.Date;
            //DateTime firstMonthDay = new DateTime(date.Year, date.Month, 1);
            //DateTime firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            //if (firstMonthMonday > date)
            //{
            //    firstMonthDay = firstMonthDay.AddMonths(-1);
            //    firstMonthMonday = firstMonthDay.AddDays((DayOfWeek.Monday + 7 - firstMonthDay.DayOfWeek) % 7);
            //}
            //return (date - firstMonthMonday).Days / 7 + 1;
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
                        if (ddtype == "Section Code")
                        {
                            DDRodeCodeListItems = new ObservableCollection<DDListItems>(response.data);

                            return DDRodeCodeListItems;

                        }
                        else if (ddtype == "RMU")
                        {
                            DDRMUListItems = new ObservableCollection<DDListItems>(response.data);

                            return DDRMUListItems;
                        }
                        //else if (ddtype == "Month")
                        //{
                        //    DDMonthListItems = new ObservableCollection<DDListItems>(response.data);
                        //    return DDMonthListItems;
                        //}
                        //else if (ddtype == "Year")
                        //{
                        //    DDYearListItems = new ObservableCollection<DDListItems>(response.data);
                        //    return DDYearListItems;
                        //}
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


        public ICommand FormAListItemTappedCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    SelectedFormDRowItem = (FormDHeaderResponseDTO)obj;
                });
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    try
                    {

                        _userDialogs.ShowLoading("Loading");
                        if (!string.IsNullOrEmpty(SmartSearch))
                        {
                            await GetMyFormDListReports("Smartsearch");

                            FormDGridListview.ItemsSource = MyBaseFormDList;
                        }
                        else
                        {
                            if (dtDate.NullableDate != null && dtDate.NullableDate.Value != null)
                            {
                                splitdate(dtDate.Date);

                                if (SelectedWeekNo == "1" && SelectedYear == 1)
                                {
                                    SelectedWeekNo = "";
                                    SelectedYear = null;
                                    SelectWeekday = "";
                                }
                            }

                            await GetMyFormDListReports("Detailsearch");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        _userDialogs.HideLoading();
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
                    editViewModel.HdrFahPkRefNo = 0;

                    await CoreMethods.PushPageModel<FormDAddPageModel>(editViewModel);
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
                    string StrType = string.Empty;

                    if (!string.IsNullOrEmpty(SmartSearch))
                    {
                        StrType = "Smartsearch";

                    }
                    else if ((!string.IsNullOrEmpty(SelectedRoadCode)) || (!string.IsNullOrEmpty(SelectedRMU)) || (!string.IsNullOrEmpty(SelectedWeekNo)) || (!string.IsNullOrEmpty(SelectWeekday)) ||
                    (SelectedYear == 0 && SelectedYear == null))
                    {
                        StrType = "Detailsearch";
                    }
                    else
                    {
                        StrType = "Grid";
                    }


                    if (iValueRet > 1)
                        iValueRet = iValueRet - 1;

                    await ButtonPagination(StrType, iValueRet);
                });
            }
        }


        private async Task<int> ButtonPagination(string Type, int Value)
        {

            if (Type == "Smartsearch")
            {
                if (SmartSearch != null && SmartSearch != "")
                    SmartSearch = SmartSearch.Trim();
                GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
                {
                    StartPageNo = (iValueRet - 1) * pageno,

                    RecordsPerPage = pageno,

                    sortOrder = "0",

                    Filters = new FormDSearchGridDTO() { SmartInputValue = SmartSearch },
                };
            }
            else if (Type == "Detailsearch")
            {
                GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
                {
                    StartPageNo = (iValueRet - 1) * pageno,

                    RecordsPerPage = pageno,

                    sortOrder = "0",

                    Filters = new FormDSearchGridDTO() { Road_Code = SelectedRoadCode, RMU = SelectedRMU, WeekNo = SelectedWeekNo, Year = SelectedYear, WeekDay = SelectWeekday }
                };
            }
            else
            {

                GridItems = new FilteredPagingDefinition<FormDSearchGridDTO>()
                {
                    StartPageNo = (iValueRet - 1) * pageno,

                    RecordsPerPage = pageno,

                    sortOrder = "0",

                    Filters = new FormDSearchGridDTO(),
                };
            }

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

            var response = await _restApi.GetFormDGridList(GridItems);

            if (response.success)
            {
                startPage = ((iValueRet - 1) * pageno) + 1;

                totalsize = response.data.TotalRecords.ToString();
                if ((iValueRet * pageno) > Convert.ToInt32(totalsize))
                    pagesize = totalsize;
                else
                    pagesize = (iValueRet * pageno).ToString(); //response.data.FilteredRecords.ToString();

                MyBaseFormDList = new ObservableCollection<FormDHeaderResponseDTO>(response.data.PageResult);

                FormDGridListview.ItemsSource = MyBaseFormDList;

                foreach (var ivalue in MyBaseFormDList)
                {
                    if (ivalue.SubmitSts == true)
                    {
                        ivalue.SubAuthStatus = "Submitted";
                    }
                    else
                    {
                        ivalue.SubAuthStatus = "Saved";

                    }
                }
            }


            //SelectedRoadCode = string.Empty;

            //SelectedRMU = string.Empty;

            //SelectedWeekNo = string.Empty;

            //SelectedYear = null;

            //SelectWeekday = string.Empty;


            return 1;
        }

        public ICommand btnNextCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    string StrType = string.Empty;

                    if (!string.IsNullOrEmpty(SmartSearch))
                    {
                        StrType = "Smartsearch";

                    }
                    else if ((!string.IsNullOrEmpty(SelectedRoadCode)) || (!string.IsNullOrEmpty(SelectedRMU)) || (!string.IsNullOrEmpty(SelectedWeekNo)) || (!string.IsNullOrEmpty(SelectWeekday)) ||
                    (SelectedYear == 0 && SelectedYear == null))
                    {
                        StrType = "Detailsearch";
                    }
                    else
                    {
                        StrType = "Grid";
                    }


                    int iRet = (Convert.ToInt32(totalsize) / pageno);

                    if (iValueRet <= iRet)

                        iValueRet = iValueRet + 1;

                    await ButtonPagination(StrType, iValueRet);

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
                        rmucode.SelectedIndex = -1;

                        sectioncode.SelectedIndex = -1;

                        if (rmucode.SelectedIndex == -1)
                            await GetSectionByRmu(null);


                        dtDate.NullableDate = null;

                        dtDate.NullText = "";

                        searchBar.Text = "";

                        SelectedRoadCode = string.Empty;

                        SelectedRMU = string.Empty;

                        SelectedWeekNo = string.Empty;

                        SelectedYear = null;

                        SelectWeekday = string.Empty;
                        await GetMyFormDListReports("Grid");

                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                });
            }
        }


        public ICommand FormDListItemTappedCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    SelectedFormDRowItem = (FormDHeaderResponseDTO)obj;
                });
            }
        }


        public ICommand ClickMeActionCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    var SelectedFormARowItem = (FormDHeaderResponseDTO)obj;
                    var actionResult = "";

                    string view = isView ? "View" : "";
                    string delete = isDelete ? "Delete" : "";
                    string edit = isModify && SelectedFormARowItem.SubAuthStatus.ToLower() != "submitted" ? "Edit" : "";

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
                            MyBaseFormDList = await GetMyFormDListReports("Grid");
                            FormDGridListview.ItemsSource = MyBaseFormDList;
                            return;
                        }
                    }
                    if (actionResult == "Edit")
                    {
                        editViewModel.Type = "Edit";
                        editViewModel.HdrFahPkRefNo = SelectedFormARowItem.No;
                        App.SubmitViewState = false;
                        //editViewModel.HdrFahPkRefNo = SelectedFormARowItem.FahRmu;

                        await CoreMethods.PushPageModel<FormDAddPageModel>(editViewModel);
                    }
                    else if (actionResult == "View")
                    {
                        editViewModel.Type = "View";
                        App.SubmitViewState = true;
                        editViewModel.HdrFahPkRefNo = SelectedFormARowItem.No;
                        //editViewModel.HdrFahPkRefNo = SelectedFormARowItem.FahRmu;

                        await CoreMethods.PushPageModel<FormDAddPageModel>(editViewModel);
                    }
                });
            }
        }

        public async Task<int> DeleteHeaderdetails(int HeaderCode)

        {
            try

            {

                _userDialogs.ShowLoading("loading");

                if (CrossConnectivity.Current.IsConnected)
                {


                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(HeaderCode);

                    var response = await _restApi.DeleteFormDHdr(HeaderCode);


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
                        _userDialogs.Toast("Unable to connect please check your internet connection.");

                    return iResultValue;
                }
                else
                    UserDialogs.Instance.Alert("“Unable to connect please check your internet connection.”");
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }
            return iResultValue;
        }




    }
}
