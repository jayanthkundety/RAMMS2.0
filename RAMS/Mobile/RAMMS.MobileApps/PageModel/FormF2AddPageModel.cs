using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps.PageModel
{
    public class FormF2AddPageModel : FreshBasePageModel
    {
        private IRestApi _restApi;
        private IUserDialogs _userDialogs;
        private ILocalDatabase _localDatabase;
        private AssetDDLResponseDTO.DropDown _selectedRMU;
        private AssetDDLResponseDTO.DropDown _selectedRoadCode;
        private AssetDDLResponseDTO.DropDown _selectedSectionCode;
        private int _selectedDivision = -1;
        private int _selectedYear = -1;
        private int iValueRet = 1;
        private int _selectedPageSize = 0;

        private bool isModify;
        private bool isDelete;
        private bool isView;
        SignaturePadView padView;
        public bool IsAdd { get; set; }
        public bool IsHeaderEnable { get; set; } = true;
        public bool CanSave { get; set; } = false;
        public string SelectedRoadName { get; set; }
        public string SelectedSectionName { get; set; }
        public string SmartSearch { get; set; }
        public string District { get; set; }
        public decimal? RoadLength { get; set; }
        public bool IsCrewNameEnabled { get; set; }
        public bool IsInspNameEnabled { get; set; }
        public DateTime? InspDate { get; set; } = null;
        public string TotalRecords { get; set; } = "0";
        public string PageStart { get; set; } = "0";
        public string PageEnd { get; set; } = "0";
        public int PageSize { get; set; } = 10;






        public string SortOrder { get; set; } = "0";
        public bool IsAssetIdAssending { get; set; } = false;
        public bool IsLocationChAssending { get; set; } = false;
        public bool IsLengthAssending { get; set; } = false;
        public bool IsStructureCodeAssending { get; set; } = false;
        public bool IsCondition1Assending { get; set; } = false;
        public bool IsCondition2Assending { get; set; } = false;
        public bool IsCondition3Assending { get; set; } = false;
        public bool IsBoundAssending { get; set; } = false;
        public bool IsPostSpacingAssending { get; set; } = false;
        public bool IsRemarksAssending { get; set; } = false;
        public int ColumnIndex { get; set; }







        public FilteredPagingDefinition<FormF2DetailRequestDTO> SearchCriteriaItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRodeCodeListItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRMUListItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDSectionListItems { get; set; }
        public ObservableCollection<FormF2DetailRequestDTO> MyBaseFormF2ListViewItems { get; set; }
        public ObservableCollection<DDListItems> DDYearListItems { get; set; }
        public ObservableCollection<DDListItems> DDDivisionListItems { get; set; }
        public ObservableCollection<DDListItems> DDInspUserListListItems { get; set; }
        public ObservableCollection<DDListItems> DDCrewUserListListItems { get; set; }

        public string CrewLeaerName { get; set; }
        public string SelectedRefNo { get; set; }
        public string InspName { get; set; }
        public string InspDesignation { get; set; }
        public ImageSource InspSign { get; set; }
        public int RoadID { get; set; }

        private int _selectedCrewIndex = -1;
        private int _selectedInspIndex = -1;
        public int SelectedCrewIndex
        {
            get
            {
                return _selectedCrewIndex;
            }
            set
            {
                _selectedCrewIndex = value;
                if (_selectedCrewIndex != -1)
                {
                    var selectedItem = Convert.ToInt32(DDCrewUserListListItems?[_selectedCrewIndex].Value.ToString());                    
                    CrewLeaerName = null;
                    GetUserDetilsList("crewuser", selectedItem);
                }
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

                if (_selectedInspIndex != -1)
                {
                    var selectedItem = Convert.ToInt32(DDInspUserListListItems?[_selectedInspIndex].Value.ToString());
                    InspName = null;
                    InspDesignation = null;                    
                    GetUserDetilsList("inspuser", selectedItem);
                }
            }
        }
        public int SelectedDivision
        {
            get => _selectedDivision;
            set
            {
                _selectedDivision = value;
                RaisePropertyChanged();
            }
        }
        public AssetDDLResponseDTO.DropDown SelectedRMU
        {
            get => _selectedRMU;
            set
            {
                _selectedRMU = value;

                SelectedSectionCode = SelectedRoadCode = null;
                SelectedSectionName = SelectedRoadName = "";
                RoadLength = null;
                RMUSelectionChanged();
                RaisePropertyChanged();
            }
        }

        private async void RMUSelectionChanged()
        {
            await GetLandingDropDownList();
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
                RoadLength = null;
                RMUSelectionChanged();
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
                {
                   // SelectedRoadName = _selectedRoadCode.Text.Split('-')[1].ToString();
                    GetRoadCode(SelectedRoadCode?.Value);
                    SetRefNumber();
                    SetRoadID(SelectedRoadCode?.Value);
                    GetLandingDropDownList();
                }
                RaisePropertyChanged();
            }
        }

        private async void SetRoadID(string value)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    RoadMasterRequestDTO roadMasterRequestDTO = new RoadMasterRequestDTO()
                    {
                        RoadCode = value
                    };
                    var response = await _restApi.GetRM_RoadCode_Service(roadMasterRequestDTO);
                    if (response.success)
                    {
                        RoadID = response.data.No;
                        SelectedRoadName = response.data.RoadName;
                    }
                }
            }
            catch (Exception ex) 
            {
                _userDialogs.Alert(ex.Message);
            }
        }

        private void SetRefNumber()
        {
            if (SelectedRoadCode != null && SelectedYear > 0)
                SelectedRefNo = "CI/Form F2/" + SelectedRoadCode.Value + "/" + DDYearListItems[SelectedYear].Value;
        }

        public async void GetRoadCode(string value)
        {
            var response = await _restApi.GetRoadLength(SelectedRoadCode?.Value);
            if (response.success)
            {
                RoadLength = response.data;
            }
        }

        

        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                SetRefNumber();
                RaisePropertyChanged();
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

        public FormF2AddPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;
            _localDatabase = localDatabase;
        }

        public override void Init(object initData)
        {
            base.Init(initData);

            isView = Model.Security.IsView(ModuleNameList.Condition_Inspection);
            IsAdd = isModify = Model.Security.IsModify(ModuleNameList.Condition_Inspection);
            isDelete = Model.Security.IsDelete(ModuleNameList.Condition_Inspection);
        }

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await GetLandingDropDownList();
            await GetDDListDetails("division");
            await GetDDListDetails("Year");
            await GetDDListDetails("crew");
            await GetUserList();
            //GetDetailGridData(App.HeaderCode);

            CanSave = App.ReturnType == "Edit" ? true : false;
            if (App.ReturnType == "Edit" || App.ReturnType == "View")
            {
                IsHeaderEnable = false;
                await GetF2HeaderById(App.HeaderCode);
                await GetDetailGridData(App.HeaderCode);
            }
        }

        public async Task<int> GetF2HeaderById(int headerCode)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.GetF2HeaderById(headerCode);

                    if (response.success)
                    {
                        SelectedRefNo = response.data.FormRefId;
                        SelectedDivision = DDDivisionListItems.ToList().FindIndex(x => x.Value == response.data.DivCode);
                        SelectedYear = DDYearListItems.ToList().FindIndex(x => x.Value == response.data.YearOfInsp.ToString());
                        District = response.data.Dist;
                        RoadLength = response.data.RoadLength;
                        SelectedCrewIndex = DDCrewUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.CrewLeaderId);
                        CrewLeaerName = response.data.CrewLeaderName;
                        SelectedInspIndex = DDInspUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.UserIdInspBy);
                        InspName = response.data.UserNameInspBy;
                        InspDesignation = response.data.UserDesignationInspBy;
                        InspDate = response.data.DtInspBy.HasValue ? response.data.DtInspBy.Value : (DateTime?)null;
                        InspSign = ImageSource.FromStream(
                            () => new MemoryStream(Convert.FromBase64String(response.data.SignpathInspBy)));

                        RoadMasterRequestDTO roadMasterRequestDTO = new RoadMasterRequestDTO()
                        {
                            RoadCode = response.data.RoadCode
                        };
                        var response1 = await _restApi.GetRM_RoadCode_Service(roadMasterRequestDTO);
                        if (response1.success)
                        {
                            SelectedRMU = DDRMUListItems[DDRMUListItems.ToList().FindIndex(x => x.Value == response1.data.RmuCode)];
                            await Task.Delay(2000);
                            SelectedSectionCode = DDSectionListItems[DDSectionListItems.ToList().FindIndex(x => x.Code == response1.data.SecCode.ToString())];
                            await Task.Delay(2000);
                            SelectedRoadCode = DDRodeCodeListItems[DDRodeCodeListItems.ToList().FindIndex(x => x.Value == response1.data.RoadCode)];
                            //GetSelectedRoadAndSection(response.data.RoadCode, response1.data.SecCode.ToString());
                        }
                    }
                    else
                    {
                        _userDialogs.Toast(response.errorMessage);
                    }
                }
                else
                    _userDialogs.Alert("Please check your Internet Connection !");
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
            }
            return 1;
        }

        public async void GetSelectedRoadAndSection(string roadCode, string secCode)
        {
            //await Task.Delay(2000);
            SelectedSectionCode = DDSectionListItems[DDSectionListItems.ToList().FindIndex(x => x.Code == secCode)];
            //await Task.Delay(2000);
            SelectedRoadCode = DDRodeCodeListItems[DDRodeCodeListItems.ToList().FindIndex(x => x.Value == roadCode)];
            
        }

        public async Task<int> GetLandingDropDownList()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var strQuery = new AssetDDLRequestDTO
                    {
                        RMU = SelectedRMU?.Value,
                        SectionCode = Convert.ToInt32(SelectedSectionCode?.Code),
                        RdCode = SelectedRoadCode?.Value,
                        
                        GrpCode = "GR"
                    };

                    //var response = await _restApi.GetF2LandingDropDown(strQuery);
                    var response = await _restApi.GetF2LandingDropDown(strQuery);

                    if (response.success)
                    {
                        if (response.data.RMU != null && SelectedRMU == null)
                        {
                            DDRMUListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>(response.data.RMU);
                        }
                        if (response.data.Section?.Count > 0)
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
            return 1;
        }

        private async Task<int> GetDDListDetails(string ddtype)
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
                    var response = new ResponseBaseListObject<DDListItems> ();
                    if (ddtype != "crew")
                        response = await _restApi.GetDDList(ddlist);
                    else
                        response = await _restApi.GetSupervisor();

                    if (response.success)
                    {
                        if (ddtype == "Year")
                        {
                            DDYearListItems = new ObservableCollection<DDListItems>(response.data);
                        }
                        else if (ddtype == "division")
                            DDDivisionListItems = new ObservableCollection<DDListItems>(response.data);
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
            return 1;
        }

        public ICommand OKCommand
        {

            //inserted header information 
            get
            {
                return new FreshCommand(async (obj) =>
                {

                    try
                    {

                        if (string.IsNullOrWhiteSpace(SelectedRoadCode?.Value) || string.IsNullOrWhiteSpace(SelectedRMU?.Value) || string.IsNullOrWhiteSpace(SelectedSectionCode?.Value))
                        {
                            await UserDialogs.Instance.AlertAsync("Please Enter all data", "RAMS", "OK");
                            return;
                        }

                        if (SelectedYear == -1)
                        {
                            await UserDialogs.Instance.AlertAsync("Please select Year", "RAMS", "OK");
                            return;
                        }

                        if (SelectedDivision ==-1)
                        {
                            await UserDialogs.Instance.AlertAsync("Please select Division", "RAMS", "OK");
                            return;
                        }
                        if (string.IsNullOrWhiteSpace(District))
                        {
                            await UserDialogs.Instance.AlertAsync("Please enter District", "RAMS", "OK");
                            return;
                        }

                        var response = SaveFormF2HeaderList();

                    }
                    catch
                    {
                    }


                });
            }
        }

        public async Task<ObservableCollection<FormF2HeaderRequestDTO>> SaveFormF2HeaderList()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    FormF2HeaderRequestDTO GridItems = new FormF2HeaderRequestDTO()
                    {
                        DivCode = DDDivisionListItems[SelectedDivision].Value,

                        Dist = District,
                        RmuCode = SelectedRMU?.Value,
                        //SectionCode = Convert.ToInt32(SelectedSectionCode?.Value),
                        //SectionName = SelectedSectionName,
                        RoadCode = SelectedRoadCode?.Value,
                        RoadName = SelectedRoadName,
                        RoadLength = RoadLength,
                        YearOfInsp = Convert.ToInt32(DDYearListItems[SelectedYear].Value),
                        FormRefId = SelectedRefNo,
                        RoadId = RoadID
                    };


                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

                    var response = await _restApi.SaveF2Hdr(GridItems);

                    if (response.success)
                    {
                        IsHeaderEnable = false;
                        CanSave = true;
                        App.ReturnType = "Edit";
                        App.HeaderCode = response.data.PkRefNo;
                        int inspindex = DDInspUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.UserIdInspBy);
                        SelectedInspIndex = inspindex;
                        InspName = response.data.UserNameInspBy;
                        InspDesignation = response.data.UserDesignationInspBy;
                        InspDate = response.data.DtInspBy.HasValue ? response.data.DtInspBy.Value : (DateTime?)null;

                        InspSign = ImageSource.FromStream(
                            () => new MemoryStream(Convert.FromBase64String(response.data.SignpathInspBy)));

                        int crewindex = DDCrewUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.CrewLeaderId);
                        SelectedCrewIndex = crewindex;
                        CrewLeaerName = response.data.CrewLeaderName;

                        await GetDetailGridData(response.data.PkRefNo);

                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex) { return null; }
            finally
            {
                _userDialogs.HideLoading();
            }
        }

        private async Task<int> GetDetailGridData(int pkRefNo)
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var values = (iValueRet - 1) * PageSize > 0 ? (iValueRet - 1) * PageSize : 0;

                    SearchCriteriaItems = new FilteredPagingDefinition<FormF2DetailRequestDTO>()
                    {
                        StartPageNo = values,
                        RecordsPerPage = PageSize,
                        sortOrder = SortOrder,
                        ColumnIndex = ColumnIndex,
                        Filters = new FormF2DetailRequestDTO()
                        {
                             FgrihPkRefNo = pkRefNo
                        },
                    };

                    var response = await _restApi.GetF2DetailList(SearchCriteriaItems);
                    if (response.success)
                    {
                        TotalRecords = response.data.TotalRecords.ToString();
                        PageStart = response.data.TotalRecords == 0 ? "0" : (response.data.PageNo + 1).ToString();
                        PageEnd = (response.data.PageNo + response.data.FilteredRecords).ToString();

                        var listItems = new ObservableCollection<FormF2DetailRequestDTO>(response.data.PageResult);

                        var sNo = 1;
                        foreach (var ivalue in listItems)
                        {
                            ivalue.SNo = sNo;
                            sNo++;
                        }
                        MyBaseFormF2ListViewItems = listItems;
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

        public async void GetUserDetilsList(string usertype, int iUser)
        {
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
                                var userprp = DDInspUserListListItems[SelectedInspIndex].Text.Split('-')[1];
                                if (userprp.ToLower() == "others" && App.ReturnType == "Edit" || App.ReturnType=="View") 
                                {
                                    IsInspNameEnabled = true;
                                  //  InspName = InspName ?? response.data.UserName;
                                    //InspDesignation = InspDesignation ?? response.data.Position;
                                }
                                else
                                {
                                    IsInspNameEnabled = false;
                                    InspName = response.data.UserName;
                                    InspDesignation = response.data.Position;
                                }
                            }
                            else if (usertype == "crewuser")
                            {
                                var userprp = DDCrewUserListListItems[SelectedCrewIndex].Text.Split('-')[1];
                                if (userprp.ToLower() == "others" && App.ReturnType == "Edit" || App.ReturnType == "View")
                                {
                                    IsCrewNameEnabled = true;
                                  //  CrewLeaerName = CrewLeaerName ?? response.data.UserName;
                                }
                                else
                                {
                                    IsCrewNameEnabled = false;
                                    CrewLeaerName = response.data.UserName;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _userDialogs.HideLoading();
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


        public ICommand SaveAction
        {
            get
            {
                return new Command(async (obj) =>
                {
                    SaveSignature("save");
                });
            }
        }
        private byte[] ImageSourceToByteArray(ImageSource source)
        {
            StreamImageSource streamImageSource = (StreamImageSource)source;
            System.Threading.CancellationToken cancellationToken = System.Threading.CancellationToken.None;
            Task<Stream> task = streamImageSource.Stream(cancellationToken);
            Stream stream = task.Result;

            byte[] b;
            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                b = ms.ToArray();
            }

            return b;
        }
        private async void SaveSignature(string type)
        {
            _userDialogs.ShowLoading("Loading");
            try
            {
                string inspSign = "";
                try
                {
                    padView = CurrentPage.FindByName<SignaturePadView>("SignatureView");
                    Stream image = await padView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                    if (!padView.IsBlank)
                    {
                        using (BinaryReader binaryReader = new BinaryReader(image))
                        {
                            binaryReader.BaseStream.Position = 0;
                            byte[] Signature = binaryReader.ReadBytes((int)image.Length);
                        }
                        var signatureMemoryStream = image as System.IO.MemoryStream;
                        var byteArray = signatureMemoryStream.ToArray();
                        string base64String = Convert.ToBase64String(byteArray);
                        inspSign = base64String;
                    }
                }
                catch (Exception ex) { }

                FormF2HeaderRequestDTO saveDetails = new FormF2HeaderRequestDTO()
                {
                    PkRefNo = App.HeaderCode,
                    CrewLeaderId = SelectedCrewIndex != -1 ? Convert.ToInt32(DDCrewUserListListItems[SelectedCrewIndex].Value) : (int?)null,
                    CrewLeaderName = CrewLeaerName,
                    UserIdInspBy = SelectedInspIndex != -1 ? Convert.ToInt32(DDInspUserListListItems[SelectedInspIndex].Value) : (int?)null,
                    UserNameInspBy = InspName,
                    UserDesignationInspBy = InspDesignation,
                    DtInspBy = InspDate,
                    DtOfInsp = InspDate,
                    SignpathInspBy = inspSign,
                    SubmitSts = type == "save" ? false : true
                };

                var response = await _restApi.SaveF2Signature(saveDetails);
                if (response.success)
                {
                    if (response.data > 0)
                    {
                        if (type == "save")
                            await UserDialogs.Instance.AlertAsync("Data Saved Successfully.", "RAMS", "OK");
                        else
                            await UserDialogs.Instance.AlertAsync("Data Submitted Successfully.", "RAMS", "OK");

                        await CoreMethods.PopPageModel();
                    }
                    else if(response.data == -1)
                    {
                        _userDialogs.Alert("You cannot submit until verify the conditional inspection, please verify all.", "RAMS", "OK");
                    }
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

        public ICommand CancelAction
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    if (App.ReturnType == "View")
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
                    if(CrewLeaerName == null || CrewLeaerName == "")
                    {
                        _userDialogs.Alert("Crew Leader Name is Required.", "RAMS", "Ok");
                        return;
                    }
                    if(SelectedCrewIndex == -1)
                    {
                        _userDialogs.Alert("Crew Leader Id field is Required. Please Choose from the dropdown list.","RAMS","Ok");
                        return;
                    }
                    if (SelectedInspIndex == -1)
                    {
                        _userDialogs.Alert("Inspected By Id field is Required. Please Choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    } 
                    if (InspName == null || InspName == "")
                    {
                        _userDialogs.Alert("Inspected By User Name is Required.", "RAMS", "Ok");
                        return;
                    }
                    if (InspDesignation == null || InspDesignation == "")
                    {
                        _userDialogs.Alert("Inspected By User Designation is Required.", "RAMS", "Ok");
                        return;
                    }
                    if (InspDate == null)
                    {
                        _userDialogs.Alert("Date Of Inspection is Required.", "RAMS", "Ok");
                        return;
                    }
                    SaveSignature("submit");
                });
            }
        }
        public ICommand ClickMeAction
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    var SelectedRowItem = (FormF2DetailRequestDTO)obj;

                    //App.DetailHeaderCode = SelectedRowItem.PkRefNo;
                    var actionResult = "";

                    string view = isView ? "View" : "";
                    //string delete = isDelete && App.ReturnType != "View" ? "Delete" : "";
                    string edit = isModify && App.ReturnType != "View" ? "Edit" : "";

                    string[] permissions = new string[] { edit, view };
                    permissions = permissions.Where(x => !string.IsNullOrEmpty(x)).ToArray();

                    actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, permissions);

                    //if (actionResult == "Delete")
                    //{
                    //    var actionResult1 = await UserDialogs.Instance.ConfirmAsync("Are you sure want to delete this record?", "RAMS", "Yes", "No");
                    //    if (actionResult1)
                    //    {
                    //        //await DeleteRecord(SelectedRowItem.PkRefNo);
                    //        await GetDetailGridData(App.HeaderCode);
                    //        return;
                    //    }
                    //}
                    if (actionResult == "Edit")
                    {
                        App.DetailType = "Edit";
                        await CoreMethods.PushPageModel<FormF2DetailsPageModel>(SelectedRowItem);
                    }
                    else if (actionResult == "View")
                    {
                        App.DetailType = "View";
                        await CoreMethods.PushPageModel<FormF2DetailsPageModel>(SelectedRowItem);
                    }
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

                    await GetDetailGridData(App.HeaderCode);
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

                    await GetDetailGridData(App.HeaderCode);
                });
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
            GetDetailGridData(App.HeaderCode);
        }









        private void GetSortOrder(int columnIndex)
        {
            if (columnIndex == 2)
                SortOrder = IsAssetIdAssending ? "1" : "0";
            if (columnIndex == 3)
                SortOrder = IsLocationChAssending ? "1" : "0";
            if (columnIndex == 4)
                SortOrder = IsLengthAssending ? "1" : "0";
            if (columnIndex == 5)
                SortOrder = IsStructureCodeAssending ? "1" : "0";
            if (columnIndex == 6)
                SortOrder = IsCondition1Assending ? "1" : "0";
            if (columnIndex == 7)
                SortOrder = IsCondition2Assending ? "1" : "0";
            if (columnIndex == 8)
                SortOrder = IsCondition3Assending ? "1" : "0";
            if (columnIndex == 9)
                SortOrder = IsBoundAssending ? "1" : "0";
            if (columnIndex == 10)
                SortOrder = IsPostSpacingAssending ? "1" : "0";
            if (columnIndex == 11)
                SortOrder = IsRemarksAssending ? "1" : "0";
           
        }






        private void SetSortOrder(int columnIndex)
        {
            if (columnIndex == 2)
            {
                IsAssetIdAssending = !IsAssetIdAssending;
                IsLocationChAssending = IsLengthAssending = IsStructureCodeAssending = IsCondition1Assending = IsCondition2Assending = IsCondition3Assending = IsBoundAssending = IsPostSpacingAssending = IsRemarksAssending = false;
            }
            else if (columnIndex == 3)
            {
                IsLocationChAssending = !IsLocationChAssending;
                IsAssetIdAssending = IsLengthAssending = IsStructureCodeAssending = IsCondition1Assending = IsCondition2Assending = IsCondition3Assending = IsBoundAssending = IsPostSpacingAssending = IsRemarksAssending = false;
            }
            else if (columnIndex == 4)
            {
                IsLengthAssending = !IsLengthAssending;
                IsAssetIdAssending = IsLocationChAssending = IsStructureCodeAssending = IsCondition1Assending = IsCondition2Assending = IsCondition3Assending = IsBoundAssending = IsPostSpacingAssending = IsRemarksAssending = false;
            }
            else if (columnIndex == 5)
            {
                IsStructureCodeAssending = !IsStructureCodeAssending;
                IsAssetIdAssending = IsLocationChAssending = IsLengthAssending = IsCondition1Assending = IsCondition2Assending = IsCondition3Assending = IsBoundAssending = IsPostSpacingAssending = IsRemarksAssending = false;
            }
            else if (columnIndex == 6)
            {
                IsCondition1Assending = !IsCondition1Assending;
                IsAssetIdAssending = IsLocationChAssending = IsLengthAssending = IsStructureCodeAssending = IsCondition2Assending = IsCondition3Assending = IsBoundAssending = IsPostSpacingAssending = IsRemarksAssending = false;
            }
            else if (columnIndex == 7)
            {
                IsCondition2Assending = !IsCondition2Assending;
                IsAssetIdAssending = IsLocationChAssending = IsLengthAssending = IsStructureCodeAssending = IsCondition1Assending = IsCondition3Assending = IsBoundAssending = IsPostSpacingAssending = IsRemarksAssending = false;
            }
            else if (columnIndex == 8)
            {
                IsCondition3Assending = !IsCondition3Assending;
                IsAssetIdAssending = IsLocationChAssending = IsLengthAssending = IsStructureCodeAssending = IsCondition1Assending = IsCondition2Assending = IsBoundAssending = IsPostSpacingAssending = IsRemarksAssending = false;
            }
            else if (columnIndex == 9)
            {
                IsBoundAssending = !IsBoundAssending;
                IsAssetIdAssending = IsLocationChAssending = IsLengthAssending = IsStructureCodeAssending = IsCondition1Assending = IsCondition2Assending = IsCondition3Assending = IsPostSpacingAssending = IsRemarksAssending = false;
            }
            else if (columnIndex == 10)
            {
                IsPostSpacingAssending = !IsPostSpacingAssending;
                IsAssetIdAssending = IsLocationChAssending = IsLengthAssending = IsStructureCodeAssending = IsCondition1Assending = IsCondition2Assending = IsCondition3Assending = IsBoundAssending = IsRemarksAssending = false;
            }
            else if (columnIndex == 11)
            {
                IsRemarksAssending = !IsRemarksAssending;
                IsAssetIdAssending = IsLocationChAssending = IsLengthAssending = IsStructureCodeAssending = IsCondition1Assending = IsCondition2Assending = IsCondition3Assending = IsBoundAssending = IsPostSpacingAssending = false;
            }
            else
            {
                IsAssetIdAssending = IsLocationChAssending = IsLengthAssending = IsStructureCodeAssending = IsCondition1Assending = IsCondition2Assending = IsCondition3Assending = IsBoundAssending = IsPostSpacingAssending = IsRemarksAssending = false;
            }
        }





        public async Task<int> DeleteRecord(int pkRefNo)
        {
            try
            {
                _userDialogs.ShowLoading("loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.DeleteF2Detail(pkRefNo);
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



        public ICommand SortingCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    ColumnIndex = Convert.ToInt32(obj);
                    GetSortOrder(ColumnIndex);
                    GetDetailGridData(App.HeaderCode);
                    SetSortOrder(ColumnIndex);
                    iValueRet = 1;
                });
            }
        }
    }
}
