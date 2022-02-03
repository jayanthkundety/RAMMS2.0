using Acr.UserDialogs;
using FreshMvvm;
using Newtonsoft.Json;
using PCLStorage;
using Plugin.Connectivity;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.MobileApps.Controls;
using RAMMS.MobileApps.Helpers;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RAMMS.MobileApps
{
    public class FormADetailsPageModel : FreshBasePageModel
    {
        private IUserDialogs _userDialogs;
        private IRestApi _restApi;
        public ILocalDatabase _localDatabase;
        public FormADetailResponseDTO SelectedFormARowItem { get; set; }
        public ObservableCollection<RmFormADtl> DetailGridListItems { get; set; }

        SignaturePadView padView, vpadview;

        int iResultValue { get; set; }

        public ObservableCollection<FormADetailResponseDTO> DetailGridListViewItems { get; set; }

        //public ObservableCollection<FormADetailResponseDTO> DetailFromADtlGridListItems { get; set; }

        public ObservableCollection<FormAHeaderResponseDTO> DetailFromAHdrGridListItems { get; set; }

        public ObservableCollection<FormADetailsRequestDTO> DetailFromReqADtlGridListItems { get; set; }

        public FilteredPagingDefinition<FormADetailsRequestDTO> GridDetailItems { get; set; }

        FormAHeaderRequestDTO objValue { get; set; }

        public float LstViewHeightRequest { get; set; }
        public bool IsEmpty { get; set; }

        private Xamarin.Forms.ImageSource inspimage { get; set; }
        private Xamarin.Forms.ImageSource verimage { get; set; }



        public int startPage { get; set; }
        public string totalsize { get; set; }
        public string pagesize { get; set; }
        public int pageno { get; set; }
        private CustomMyPicker cuspicker;


        public Dictionary<string, string> dictionaryAction { get; set; }

        public ObservableCollection<DDListItems> DDRodeCodeListItems { get; set; }

        public ObservableCollection<DDListItems> DDRMUListItems { get; set; }

        public ObservableCollection<DDListItems> DDRoadNameListItems { get; set; }

        public ObservableCollection<DDListItems> DDSectionListItems { get; set; }

        public ObservableCollection<DDListItems> DDYearListItems { get; set; }

        public ObservableCollection<DDListItems> DDMonthtListItems { get; set; }

        public ObservableCollection<DDListItems> DDAssertListItems { get; set; }

        public ObservableCollection<DDListItems> MonthListItems { get; set; }

        public ObservableCollection<DDListItems> DDAssetTypeListItems { get; set; }

        public ObservableCollection<DDListItems> DDInspUserListListItems { get; set; }

        public ObservableCollection<DDListItems> DDVerUserListListItems { get; set; }

        public RoadMasterResponseDTO GetAllMasterRoadCode { get; set; }

        public DateTime? dtDateinsp { get; set; } = null;

        public DateTime? dtdateever { get; set; } = null;

        public int GetFormDetailsCount { get; set; }


        ExtendedPicker yearcode, MonthCode, AssetCode, roadcode, userinspcode, uservercode;

        Button btnAdd, btnOk;

        EntryControl inspUser, inspdesg, enctrlverName, enctrlverdesignation;

        string strRefNo { get; set; }

        public FormAHeaderRequestDTO GridItems { get; set; }

        public int GetHeaderNoCode { get; set; }

        public string SelectedRoadCode { get; set; }

        public string SelectedRMU { get; set; }

        public string SelectedSection { get; set; }


        public string AssertBRSection { get; set; }


        public string Selectedrefno { get; set; }

        public string SelectedRoadName { get; set; }

        public int? SelectedMonth { get; set; }

        public int? SelectedYear { get; set; }

        public string SelectedAssetType { get; set; }

        public FormAHeaderResponseDTO SelectedHdrItem { get; set; }

        public UserResponseDTO SelectedInspUserItem { get; set; }

        public UserResponseDTO SelectedVerUserItem { get; set; }

        public string Status { get; set; }
        public FormAHeaderResponseDTO SelectedNewHdrItem { get; set; }

        public FormAHeaderResponseDTO SelectedHdrEditItem { get; set; }

        public FormADetailsRequestDTO SelecteddtlEditItem { get; set; }

        public bool ViewType { get; set; }

        public bool CanSave { get; set; }

        public bool IsHeaderEnable { get; set; } = true;

        public bool NotToEdit { get; set; }

        public EditViewModel _editViewModel { get; set; }

        public RoadMasterRequestDTO GetMaterCodeItem { get; set; }

        public string strGetRoadCode { get; set; }

        public string StrGetRMUCode { get; set; }

        public string StrGetRMUName { get; set; }

        public string StrGetSectioncode { get; set; }

        public string userprp { get; set; }
        public string userver { get; set; }

        public string StrRefcode { get; set; }

        public string StrAssertValue { get; set; }

        public string Selectedinspuser { get; set; }

        public string strinsName { get; set; }

        public string strinspDesignation { get; set; }

        public string Selectedverpuser { get; set; }

        public string strverName { get; set; }

        public string strverDesignation { get; set; }

        public string strVerSign { get; set; }

        public string strInspSign { get; set; }

        public string AppType { get; set; }

        DatePicker inspcode, vercode;

        private void SetProperty(ref SignaturePadView sign, SignaturePadView value)
        {
            throw new NotImplementedException();
        }

        public FormADetailsPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;
            _localDatabase = localDatabase;



            DetailGridListItems = new ObservableCollection<RmFormADtl>();

            //DetailFromADtlGridListItems = new ObservableCollection<FormADetailResponseDTO>();

            DetailFromAHdrGridListItems = new ObservableCollection<FormAHeaderResponseDTO>();

            DetailFromReqADtlGridListItems = new ObservableCollection<FormADetailsRequestDTO>();

            SelectedNewHdrItem = new FormAHeaderResponseDTO();

            SelectedHdrItem = new FormAHeaderResponseDTO();

            SelectedHdrEditItem = new FormAHeaderResponseDTO();

            dictionaryAction = new Dictionary<string, string>();

            DDRodeCodeListItems = new ObservableCollection<DDListItems>();

            GridItems = new FormAHeaderRequestDTO();

            GridDetailItems = new FilteredPagingDefinition<FormADetailsRequestDTO>();

            DetailGridListViewItems = new ObservableCollection<FormADetailResponseDTO>();


            DDRMUListItems = new ObservableCollection<DDListItems>();

            DDRoadNameListItems = new ObservableCollection<DDListItems>();

            DDAssetTypeListItems = new ObservableCollection<DDListItems>();

            DDInspUserListListItems = new ObservableCollection<DDListItems>();

            DDVerUserListListItems = new ObservableCollection<DDListItems>();

            MonthListItems = new ObservableCollection<DDListItems>();

            DDSectionListItems = new ObservableCollection<DDListItems>();

            DDYearListItems = new ObservableCollection<DDListItems>();

            DDMonthtListItems = new ObservableCollection<DDListItems>();

            GetMaterCodeItem = new RoadMasterRequestDTO();

            //public ObservableCollection<DDListItems> DDAssertListItems { get; set; }

            _editViewModel = new EditViewModel();


            DetailGridListItems = new ObservableCollection<RmFormADtl>();

            GetAllMasterRoadCode = new RoadMasterResponseDTO();



        }

        public override async void Init(object initData)
        {
            base.Init(initData);

            startPage = 0;
            pagesize = "0";
            totalsize = "0";

            pageno = 10;

            userinspcode = CurrentPage.FindByName<ExtendedPicker>("insppicker");

            uservercode = CurrentPage.FindByName<ExtendedPicker>("verpicker");
            AssetCode = CurrentPage.FindByName<ExtendedPicker>("assetpicker");

            cuspicker = CurrentPage.FindByName<CustomMyPicker>("scustompicker");


            MonthCode = CurrentPage.FindByName<ExtendedPicker>("monthpicker");

            roadcode = CurrentPage.FindByName<ExtendedPicker>("rodeCodePicker");

            roadcode.ItemsSource = DDRodeCodeListItems.Select((DDListItems arg) => arg.Text).ToList();


            btnAdd = CurrentPage.FindByName<Button>("btnAddCtl");

            btnOk = CurrentPage.FindByName<Button>("btnOKCtl");

            yearcode = CurrentPage.FindByName<ExtendedPicker>("yearpicker");

            inspdesg = CurrentPage.FindByName<EntryControl>("inspdesg");
            inspUser = CurrentPage.FindByName<EntryControl>("inspUser");
            enctrlverdesignation = CurrentPage.FindByName<EntryControl>("enctrlverdesignation");
            enctrlverName = CurrentPage.FindByName<EntryControl>("enctrlverName");

            try
            {

                _editViewModel = initData as EditViewModel;
                AppType = _editViewModel.Type;


                if (App.DetailType == "Add")
                {
                    _editViewModel.Type = App.DetailType;
                    _editViewModel.HdrFahPkRefNo = App.DetailHeaderCode;

                }
                else
                {
                    App.DetailType = "";

                    App.DetailHeaderCode = 0;

                    _editViewModel.Type = _editViewModel.Type;

                    _editViewModel.HdrFahPkRefNo = _editViewModel.HdrFahPkRefNo;
                }
            }
            catch { }


        }


        //public async void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var picker = (Picker)sender;

        //    int selectedIndex = picker.SelectedIndex;

        //    var roadcode = CurrentPage.FindByName<ExtendedPicker>("rodeCodePicker");

        //    roadcode.ItemsSource = DDRodeCodeListItems.Select((DDListItems arg) => arg.Text).ToList();


        //}

        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            if (_editViewModel.Type == "" || _editViewModel.Type == "Edit")
            {
                _editViewModel.Type = "Edit";
                App.ReturnType = "";
            }
            if (_editViewModel.Type == "View")
            {
                App.ReturnType = "View";
            }

            //if (GetHeaderNoCode != 0)
            //{
            //    _editViewModel.Type = "Edit";
            //}


            //if (App.ReturnType == "View")
            //{
            //    //_editViewModel.Type = "View";
            //    //App.ReturnType = "";
            //    //ViewType = false;
            //}



            ViewType = _editViewModel.Type == "View" ? false : true;

            if (_editViewModel.Type == "Add")
            {
                IsControlVisible(true, _editViewModel.Type);

                await DropDownMasterSetup(_editViewModel.Type);

                SelectedHdrItem.No = _editViewModel.HdrFahPkRefNo;

                //GetHeaderNoCode = _editViewModel.HdrFahPkRefNo;

                if (SelectedNewHdrItem.No > 2 && SelectedNewHdrItem.FormADetails.Count > 0)
                {
                    DetailGridListViewItems = await GetMyFormAHeaderEditAndViewListReports(GetHeaderNoCode);
                    return;
                }
                else if (SelectedHdrItem.No > 2)
                {
                    DetailGridListViewItems = await GetMyFormAHeaderEditAndViewListReports(GetHeaderNoCode);
                    return;
                }
                _editViewModel.HdrFahPkRefNo = GetHeaderNoCode;

                //else if (GetHeaderNoCode > 2)
                //{
                //    DetailFromReqADtlGridListItems = await GetDetailNewGridListDetails(GetHeaderNoCode);
                //    return;
                //}


            }
            if (_editViewModel.Type == "Edit" || _editViewModel.Type == "View")
            {
                if (GetHeaderNoCode != 0)
                {
                    _editViewModel.HdrFahPkRefNo = GetHeaderNoCode;
                }
                //await GetMyFormAHeaderEditAndViewListReports(_editViewModel.HdrFahPkRefNo);

                SelectedHdrItem.No = _editViewModel.HdrFahPkRefNo;
                GetHeaderNoCode = _editViewModel.HdrFahPkRefNo;
                IsControlVisible(false, _editViewModel.Type);
                //GetHeader Details for set
                //Add button call function
                SelectedHdrEditItem = await GetEditViewHeaderdetails(GetHeaderNoCode);

                await DropDownMasterSetup(_editViewModel.Type);


                if (SelectedHdrEditItem.No > 2 && SelectedHdrEditItem.FormADetails.Count > 0)
                    DetailGridListViewItems = await GetMyFormAHeaderEditAndViewListReports(SelectedNewHdrItem.No);
                else if (SelectedHdrEditItem.No > 2)
                {
                    DetailGridListViewItems = await GetMyFormAHeaderEditAndViewListReports(GetHeaderNoCode);
                    //DetailFromADtlGridListItems = await GetMyFormAHeaderEditAndViewListReports(SelectedHdrItem.No);
                }
                // App.DetailHeaderCode = _editViewModel.HdrFahPkRefNo;
                //Find Details button need to work it out 
                //Retrieve Header data disable finish button
                //Retrieve details and allow user to add, edit and delete

                _editViewModel.Type = "";
                return;
            }

            if (App.ReturnType == "Edit" || App.ReturnType == "View")
            {
                //await GetMyFormAHeaderEditAndViewListReports(_editViewModel.HdrFahPkRefNo);


                GetHeaderNoCode = App.DetailHeaderCode;

                SelectedHdrEditItem = await GetEditViewHeaderdetails(GetHeaderNoCode);

                await DropDownMasterSetup(App.ReturnType);


                if (SelectedHdrEditItem.No > 2 && SelectedHdrEditItem.FormADetails.Count > 0)
                    DetailGridListViewItems = await GetMyFormAHeaderEditAndViewListReports(SelectedNewHdrItem.No);
                else if (SelectedHdrEditItem.No > 2)
                {
                    DetailGridListViewItems = await GetMyFormAHeaderEditAndViewListReports(GetHeaderNoCode);
                    //DetailFromADtlGridListItems = await GetMyFormAHeaderEditAndViewListReports(SelectedHdrItem.No);
                }
                // App.DetailHeaderCode = _editViewModel.HdrFahPkRefNo;

                //Find Details button need to work it out 
                //Retrieve Header data disable finish button
                //Retrieve details and allow user to add, edit and delete
                App.ReturnType = "";
                return;
            }


            //if ()
            //{
            //    //await GetMyFormAHeaderEditAndViewListReports(_editViewModel.HdrFahPkRefNo);
            //DropDownMasterSetup(_editViewModel.Type);

            //SelectedHdrItem.No = _editViewModel.HdrFahPkRefNo;

            //if (SelectedNewHdrItem.No > 2 && SelectedNewHdrItem.FormADetails.Count > 0)

            //    DetailFromReqADtlGridListItems = await GetDetailNewGridListDetails(SelectedNewHdrItem.No);

            //else if (SelectedHdrItem.No > 2)
            //{
            //    DetailFromReqADtlGridListItems = await GetDetailNewGridListDetails(SelectedHdrItem.No);
            //}

            //    return;
            //}
            else
            {

                SelectedHdrItem.No = _editViewModel.HdrFahPkRefNo;

                if (SelectedNewHdrItem.No > 2 /*&& SelectedNewHdrItem.FormADetails.Count > 0*/)

                    DetailGridListViewItems = await GetMyFormAHeaderEditAndViewListReports(SelectedNewHdrItem.No);

                else if (SelectedHdrItem.No > 2)
                {
                    GetHeaderNoCode = SelectedHdrItem.No;

                    DetailGridListViewItems = await GetMyFormAHeaderEditAndViewListReports(SelectedHdrItem.No);
                }

                return;
            }



            //ObjFormADetail.OnGetInspSign(null, null);

            //ObjFormADetail.OnGetVerifySign(null, null);


        }




        //private async Task<bool> OnSaveSignature(Stream bitmap, string filename)
        //{
        //    var storageFolder = await KnownFolders.GetFolderForUserAsync(null, KnownFolderId.PicturesLibrary);
        //    var file = await storageFolder.CreateFileAsync(filename, CreationCollisionOption.ReplaceExisting);

        //    using (var stream = await file.OpenAsync(FileAccessMode.ReadWrite))
        //    using (var dest = stream.AsStreamForWrite())
        //    {
        //        await bitmap.CopyToAsync(dest);
        //    }

        //    return true;
        //}

        //                  public async void InspectorSignature()
        //       {
        //           try {

        //               var signature = SignaturePad.GetImage(Acr.XamForms.SignaturePad.ImageFormatType.Png);

        //               //using (BinaryReader br = new BinaryReader(signature))
        //               //{
        //               //    signbytes = br.ReadBytes((int)signature.Length);

        //               //}

        //               var signatureMemoryStream = signature as MemoryStream;

        //               if (signatureMemoryStream == null)
        //               {
        //                   signatureMemoryStream = new MemoryStream();

        //                   signature.CopyTo(signatureMemoryStream);
        //               }


        //               signbytes = signatureMemoryStream.ToArray();

        //               DataAccess.offsignbytes = signbytes;

        //               await SaveShiftInfo(signbytes);

        //               //var image = new Image();

        //               //image.HeightRequest = 200;

        //               //image.WidthRequest = 300;

        //               DependencyService.Get<ISignature>().SavePictureToDisk("Signature", signbytes);
        //           }





        //           } catch 
        //{


        //}


        //}








        //public async Task<ObservableCollection<RmFormADtl>> GetDetailGridListDetails()
        //{
        //    try
        //    {
        //        _userDialogs.ShowLoading("Loading");

        //        if (CrossConnectivity.Current.IsConnected)
        //        {
        //            var gridList = new RmFormADtl()
        //            {
        //                FadPkRefNo = 1,
        //            };
        //            var json = Newtonsoft.Json.JsonConvert.SerializeObject(gridList);
        //            var response = await _restApi.GetFormADetailGridFormList(gridList);
        //            if (response.success)
        //            {
        //                DetailGridListItems = new ObservableCollection<RmFormADtl>(response.data);
        //                return DetailGridListItems;
        //            }
        //            else
        //                _userDialogs.Toast(response.errorMessage);

        //            return DetailGridListItems;
        //        }
        //        else
        //            UserDialogs.Instance.Alert("Please check your Internet Connection !");
        //        IsEmpty = DetailGridListItems.Count == 0;
        //        LstViewHeightRequest = DetailGridListItems.Count * 40;
        //        return DetailGridListItems;
        //    }
        //    catch (Exception ex)
        //    {
        //        _userDialogs.Alert(ex.Message);
        //    }
        //    finally
        //    {
        //        _userDialogs.HideLoading();
        //    }
        //    return new ObservableCollection<RmFormADtl>();
        //}


        public async Task<ObservableCollection<FormADetailsRequestDTO>> GetDetailNewGridListDetails(int hdrRefNo)
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    //var gridList = new FormAHeaderRequestDTO()
                    //{
                    //    No = 1033,
                    //};
                    //var json = Newtonsoft.Json.JsonConvert.SerializeObject(hdrRefNo);

                    var response = await _restApi.GetFormAUpdateAndView(hdrRefNo);

                    if (response.success)
                    {


                        DetailFromReqADtlGridListItems = new ObservableCollection<FormADetailsRequestDTO>(response.data.FormADetails);

                        //return DetailFromReqADtlGridListItems;
                    }

                    else

                        _userDialogs.Toast(response.errorMessage);

                    return DetailFromReqADtlGridListItems;
                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");

                IsEmpty = DetailFromReqADtlGridListItems.Count == 0;

                LstViewHeightRequest = DetailFromReqADtlGridListItems.Count * 40;

                return DetailFromReqADtlGridListItems;

            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }
            return new ObservableCollection<FormADetailsRequestDTO>();
        }



        public async Task<ObservableCollection<DDListItems>> GetRoadListDetails()
        {
            _userDialogs.ShowLoading("Loading");
            try
            {
                _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {

                    //var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);

                    var response = await _restApi.GetConcatCodeValue();

                    if (response.success)
                    {
                        DDRodeCodeListItems = new ObservableCollection<DDListItems>(response.data);
                        return DDRodeCodeListItems;
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
            return new ObservableCollection<DDListItems>();
        }




        public async Task<ObservableCollection<DDListItems>> GetddListDetails(string ddtype)
        {
            _userDialogs.ShowLoading("Loading");
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

                    var response = await _restApi.GetDDList(ddlist);

                    if (response.success)
                    {
                        if (ddtype == "RD_Code")
                        {
                            DDRodeCodeListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDRodeCodeListItems;
                        }
                        else if (ddtype == "RMU")
                        {
                            DDRMUListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDRMUListItems;
                        }
                        else if (ddtype == "RD_Name")
                        {
                            DDRoadNameListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDRoadNameListItems;
                        }
                        else if (ddtype == "FormA_Assets")
                        {
                            DDAssetTypeListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDAssetTypeListItems;
                        }
                        else if (ddtype == "Year")
                        {
                            DDYearListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDAssetTypeListItems;
                        }
                        else if (ddtype == "Month")
                        {
                            DDMonthtListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDAssetTypeListItems;
                        }
                        else if (ddtype == "Section Code")
                        {
                            DDSectionListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDAssetTypeListItems;
                        }

                    }
                    //else
                    //_userDialogs.Toast(response.errorMessage);

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



        public async Task<RoadMasterResponseDTO> Getmasterrmcodedetails(string rmcodevalue)
        {

            try
            {
                _userDialogs.ShowLoading("loading");

                if (CrossConnectivity.Current.IsConnected)
                {

                    GetMaterCodeItem = new RoadMasterRequestDTO
                    {
                        RoadCode = rmcodevalue
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GetMaterCodeItem);

                    var response = await _restApi.GetRM_RoadCode_Service(GetMaterCodeItem);

                    if (response.success)
                    {
                        GetAllMasterRoadCode = response.data;

                        strGetRoadCode = GetAllMasterRoadCode.RoadName;

                        StrGetRMUCode = GetAllMasterRoadCode.RmuCode;

                        StrGetSectioncode = GetAllMasterRoadCode.SecName;

                        return GetAllMasterRoadCode;
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return GetAllMasterRoadCode;
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
            return new RoadMasterResponseDTO();
        }


        public async Task<ObservableCollection<DDListItems>> GetInspUserList()
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
                        return DDInspUserListListItems;

                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return DDInspUserListListItems;
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


        public async Task<ObservableCollection<DDListItems>> GetVerUserList()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.userList();
                    if (response.success)
                    {
                        DDVerUserListListItems = new ObservableCollection<DDListItems>(response.data);
                        return DDVerUserListListItems;

                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return DDVerUserListListItems;
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



        public async Task<ObservableCollection<DDListItems>> GetMonthddListDetails()
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
                _userDialogs.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }




        public async Task<ObservableCollection<FormADetailResponseDTO>> GetMyFormAHeaderEditAndViewListReports(int iHeaderNo)
        {


            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    //var gridList = new FormAHeaderRequestDTO()
                    //{
                    //   RefNo = hdrRefNo
                    //};

                    GridDetailItems = new FilteredPagingDefinition<FormADetailsRequestDTO>()
                    {
                        StartPageNo = 0,

                        RecordsPerPage = 10,
                        sortOrder = "0",

                        Filters = new FormADetailsRequestDTO() { HeaderNo = iHeaderNo },
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridDetailItems);

                    var response = await _restApi.GetDetails(GridDetailItems);

                    if (response.success)
                    {
                        startPage = (response.data.TotalRecords == 0) ? 0 : response.data.PageNo + 1;

                        totalsize = response.data.TotalRecords.ToString();

                        pagesize = response.data.FilteredRecords.ToString();

                        DetailGridListViewItems = new ObservableCollection<FormADetailResponseDTO>(response.data.PageResult);


                        return DetailGridListViewItems;
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return DetailGridListViewItems;
                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");
                IsEmpty = DetailGridListViewItems.Count == 0;
                LstViewHeightRequest = DetailGridListViewItems.Count * 40;
                return DetailGridListViewItems;

            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }

            return new ObservableCollection<FormADetailResponseDTO>();
        }


        public async Task<int> DropDownMasterSetup(string type)
        {
            //DropDown
            try
            {

                //await GetddListDetails("RD_Code");

                await GetddListDetails("RMU");

                await GetddListDetails("RD_Name");

                await GetddListDetails("FormA_Assets");

                await GetddListDetails("Section Code");

                await GetddListDetails("Month");

                await GetddListDetails("Year");

                await GetRoadListDetails();

                await GetInspUserList();

                await GetVerUserList();


                if (App.DetailType != "AddDetail")
                {


                    yearcode.ItemsSource = DDYearListItems.Select((DDListItems arg) => arg.Text).ToList();

                    MonthCode.ItemsSource = DDMonthtListItems.Select((DDListItems arg) => arg.Text).ToList();

                    AssetCode.ItemsSource = DDAssetTypeListItems.Select((DDListItems arg) => arg.Text).ToList();

                    userinspcode.ItemsSource = DDInspUserListListItems.Select((DDListItems arg) => arg.Text).ToList();

                    uservercode.ItemsSource = DDVerUserListListItems.Select((DDListItems arg) => arg.Text).ToList();

                    userinspcode.SelectedIndexChanged += (s, e) =>
                    {
                        try
                        {
                            // SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                            if (userinspcode.SelectedIndex != -1)
                            {


                                Selectedinspuser = DDInspUserListListItems[userinspcode.SelectedIndex].Value.ToString();
                                userprp = DDInspUserListListItems[userinspcode.SelectedIndex].Text.Split('-')[1];
                                if (userprp.ToLower() == "others")
                                {
                                    inspUser.IsEnabled = true;
                                    inspdesg.IsEnabled = true;
                                }
                                else
                                {
                                    inspUser.IsEnabled = false;
                                    inspdesg.IsEnabled = false;
                                }

                                    int iCode = Convert.ToInt32(Selectedinspuser);

                                //service call for getting user list

                                var objUser = GetUserDetilsList("inspuser", iCode);

                                //inspcode = CurrentPage.FindByName<DatePicker>("insppicker");
                                //dtDateinsp = DateTime.Now.Date;  //inspcode.Date;


                            }

                        }
                        catch
                        {

                        }

                    };


                    uservercode.SelectedIndexChanged += (s, e) =>
                    {
                        try
                        {
                            // SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                            if (uservercode.SelectedIndex != -1)
                            {


                                Selectedverpuser = DDVerUserListListItems[uservercode.SelectedIndex].Value.ToString();
                                userver = DDInspUserListListItems[uservercode.SelectedIndex].Text.Split('-')[1];

                                if (userver.ToLower() == "others")
                                {
                                    enctrlverName.IsEnabled = true;
                                    enctrlverdesignation.IsEnabled = true;
                                }
                                else
                                {
                                    enctrlverName.IsEnabled = false;
                                    enctrlverdesignation.IsEnabled = false;
                                }


                                int iCode = Convert.ToInt32(Selectedverpuser);
                                //service call for getting user list
                                var objUser = GetUserDetilsList("veruser", iCode);

                                //vercode = CurrentPage.FindByName<DatePicker>("verdatepicker");
                                //dtdateever = DateTime.Now.Date; 

                            }

                        }
                        catch
                        {

                        }

                    };



                    if (type == "Edit" || type == "View")
                    {
                        _userDialogs.ShowLoading("Loading");
                        if (!string.IsNullOrEmpty(SelectedHdrEditItem.RoadCode))
                        {

                            if (roadcode.SelectedIndex == -1)
                            {

                                //roadcode.Items.Clear();

                                roadcode.ItemsSource = DDRodeCodeListItems.Select((DDListItems arg) => arg.Text).ToList();

                                int roadIndex = DDRodeCodeListItems.ToList().FindIndex(a => a.Value == SelectedHdrEditItem.RoadCode);

                                if (roadIndex == -1) { roadIndex = 1; }

                                roadcode.SelectedIndex = roadIndex;

                                SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                                await Getmasterrmcodedetails(SelectedRoadCode);
                            }


                            //MonthCode.Items.Clear();

                            MonthCode.ItemsSource = DDMonthtListItems.Select((DDListItems arg) => arg.Text).ToList();


                            string strStartValue = ""; int MonthIndex;
                            //Month
                            if (SelectedHdrEditItem.Month.ToString().Length == 1)
                            {
                                strStartValue = "0";

                                MonthIndex = DDMonthtListItems.ToList().FindIndex(a => a.Value == strStartValue + SelectedHdrEditItem.Month.ToString());
                            }
                            else
                            {
                                MonthIndex = DDMonthtListItems.ToList().FindIndex(a => a.Value == SelectedHdrEditItem.Month.ToString());
                            }


                            MonthCode.SelectedIndex = MonthIndex;

                            SelectedMonth = int.Parse(DDMonthtListItems[MonthCode.SelectedIndex].Value.ToString());



                            //yearcode.Items.Clear();

                            yearcode.ItemsSource = DDYearListItems.Select((DDListItems arg) => arg.Text).ToList();


                            //Year
                            int YearIndex = DDYearListItems.ToList().FindIndex(a => a.Value == SelectedHdrEditItem.Year.ToString());

                            yearcode.SelectedIndex = YearIndex;

                            SelectedYear = int.Parse(DDYearListItems[yearcode.SelectedIndex].Value.ToString());


                            //AssetCode.Items.Clear();

                            AssetCode.ItemsSource = DDAssetTypeListItems.Select((DDListItems arg) => arg.Text).ToList();




                            int Assertindex = DDAssetTypeListItems.ToList().FindIndex(a => a.Value == SelectedHdrEditItem.AssetGroupCode);

                            AssetCode.SelectedIndex = Assertindex;

                            SelectedSection = DDAssetTypeListItems[AssetCode.SelectedIndex].Value.ToString();

                            var StrValue = GetAssetCodeByNameFormA(SelectedSection);


                            //userinspcode.Items.Clear();

                            userinspcode.ItemsSource = DDInspUserListListItems.Select((DDListItems arg) => arg.Text).ToList();


                            int inspindex = DDInspUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == SelectedHdrEditItem.UseridPrp);

                            //if (inspindex == -1) { inspindex = 1; }

                            userinspcode.SelectedIndex = inspindex;


                            //uservercode.Items.Clear();

                            uservercode.ItemsSource = DDVerUserListListItems.Select((DDListItems arg) => arg.Text).ToList();

                            int iverindex = DDVerUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == SelectedHdrEditItem.UseridVer);

                            //if (iverindex == -1) { iverindex = 1; }

                            uservercode.SelectedIndex = iverindex;

                            //Control Disabled



                        }
                    }
                    else
                    {

                        if (type == "Add")
                        {

                            roadcode.ItemsSource = DDRodeCodeListItems.Select((DDListItems arg) => arg.Text).ToList();

                            roadcode.SelectedIndexChanged += (s, e) =>
                            {
                                if (roadcode.SelectedIndex != -1)
                                {
                                    SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                                    SelectedHdrItem.RoadCode = SelectedRoadCode;

                                    int roadIndex = DDRodeCodeListItems.ToList().FindIndex(a => a.Value == SelectedHdrEditItem.RoadCode);

                                    SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                                    Getmasterrmcodedetails(SelectedRoadCode);
                                }


                            };

                            yearcode.SelectedIndexChanged += (s, e) =>
                            {
                                try
                                {
                                    // SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                                    if (yearcode.SelectedIndex != -1)
                                    {

                                        SelectedYear = int.Parse(DDYearListItems[yearcode.SelectedIndex].Value.ToString());

                                        if (AssetCode.SelectedIndex != -1)
                                            SelectedSection = DDAssetTypeListItems[AssetCode.SelectedIndex].Text.ToString();

                                        if (MonthCode.SelectedIndex != -1)
                                            SelectedMonth = int.Parse(DDMonthtListItems[MonthCode.SelectedIndex].Value.ToString());

                                        //btnOk.IsEnabled = false;
                                        var AutoRefNo = GetReferenceNumber(SelectedRoadCode, Convert.ToInt32(MonthCode.SelectedItem), yearcode.SelectedItem.ToString(), AssetCode.SelectedItem.ToString());               // "NOD/Form A" + "/" + roadcode.SelectedItem + "/" + MonthCode.SelectedItem + "/" + AssetCode.SelectedItem + "-" + yearcode.SelectedItem;

                                        StrRefcode = AutoRefNo.ToString();

                                    }

                                }
                                catch
                                {

                                }

                            };


                            MonthCode.SelectedIndexChanged += (s, e) =>
                            {
                                try
                                {
                                    // SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                                    if (MonthCode.SelectedIndex != -1)
                                    {

                                        SelectedMonth = int.Parse(DDMonthtListItems[MonthCode.SelectedIndex].Value.ToString());


                                        if (AssetCode.SelectedIndex != -1)
                                            SelectedSection = DDAssetTypeListItems[AssetCode.SelectedIndex].Text.ToString();


                                        //AssertBRSection = DDAssetTypeListItems[AssetCode.SelectedIndex].Value.ToString();




                                        if (yearcode.SelectedIndex != -1)
                                            SelectedYear = int.Parse(DDYearListItems[yearcode.SelectedIndex].Value.ToString());

                                        // btnOk.IsEnabled = false;
                                        var AutoRefNo = GetReferenceNumber(SelectedRoadCode, Convert.ToInt32(MonthCode.SelectedItem), yearcode.SelectedItem.ToString(), AssetCode.SelectedItem.ToString());               // "NOD/Form A" + "/" + roadcode.SelectedItem + "/" + MonthCode.SelectedItem + "/" + AssetCode.SelectedItem + "-" + yearcode.SelectedItem;


                                        StrRefcode = AutoRefNo.ToString();

                                    }

                                }
                                catch
                                {

                                }

                            };



                            AssetCode.SelectedIndexChanged += (s, e) =>
                            {
                                try
                                {

                                    // SelectedRoadCode = DDRodeCodeListItems[roadcode.SelectedIndex].Value.ToString();

                                    if (AssetCode.SelectedIndex != -1)
                                    {

                                        //Assert Type
                                        App.AssetGroupSelection = "";

                                        SelectedSection = DDAssetTypeListItems[AssetCode.SelectedIndex].Text.ToString();


                                        var StrValue = GetAssetCodeByNameFormA(SelectedSection);


                                        // App.AssetGroupSelection = StrValue.ToString();


                                        AssertBRSection = DDAssetTypeListItems[AssetCode.SelectedIndex].Value.ToString();


                                        SelectedMonth = int.Parse(DDMonthtListItems[MonthCode.SelectedIndex].Value.ToString());


                                        SelectedYear = int.Parse(DDYearListItems[yearcode.SelectedIndex].Value.ToString());


                                        // btnOk.IsEnabled = false;


                                        var AutoRefNo = GetReferenceNumber(SelectedRoadCode, Convert.ToInt32(MonthCode.SelectedItem), yearcode.SelectedItem.ToString(), AssetCode.SelectedItem.ToString());               // "NOD/Form A" + "/" + roadcode.SelectedItem + "/" + MonthCode.SelectedItem + "/" + AssetCode.SelectedItem + "-" + yearcode.SelectedItem;


                                        StrRefcode = AutoRefNo.ToString();

                                    }

                                }
                                catch
                                {

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
                                            pageno = 10;
                                            await GetMyFormAHeaderEditAndViewListReports(GetHeaderNoCode);
                                            return;

                                        }

                                        else if (cuspicker.SelectedItem.ToString() == "25 rows")
                                        {
                                            pageno = 25;
                                            await GetMyFormAHeaderEditAndViewListReports(GetHeaderNoCode);
                                            return;

                                        }

                                        else if (cuspicker.SelectedItem.ToString() == "50 rows")
                                        {
                                            pageno = 50;
                                            await GetMyFormAHeaderEditAndViewListReports(GetHeaderNoCode);
                                            return;
                                        }

                                        else if (cuspicker.SelectedItem.ToString() == "100 rows")
                                        {
                                            pageno = 100;
                                            await GetMyFormAHeaderEditAndViewListReports(GetHeaderNoCode);
                                            return;
                                        }
                                        else if (cuspicker.SelectedItem.ToString() == "500 rows")
                                        {
                                            pageno = 500;
                                            await GetMyFormAHeaderEditAndViewListReports(GetHeaderNoCode);
                                            return;
                                        }
                                        else
                                        {
                                            pageno = 1000;
                                            await GetMyFormAHeaderEditAndViewListReports(GetHeaderNoCode);
                                            return;
                                        }
                                    }
                                }
                                catch { }


                            };
                        }
                    }

                }
                else
                {
                    // DetailFromReqADtlGridListItems = await GetDetailNewGridListDetails(App.DetailHeaderCode);
                    if (App.DetailHeaderCode.ToString().Length > 2)
                    {
                        DetailGridListViewItems = await GetMyFormAHeaderEditAndViewListReports(App.DetailHeaderCode);
                        GetHeaderNoCode = App.DetailHeaderCode;
                    }

                    App.DetailType = "";

                    //App.DetailHeaderCode = 0;

                    //App.HeaderCode = 0;
                }
            }
            catch (Exception ex)
            {

            }

            return 1;
        }

        public void IsControlVisible(bool ibValue, string Type)
        {
            //roadcode = CurrentPage.FindByName<ExtendedPicker>("rodeCodePicker");
            //yearcode = CurrentPage.FindByName<ExtendedPicker>("yearpicker");
            //MonthCode = CurrentPage.FindByName<ExtendedPicker>("monthpicker");
            //AssetCode = CurrentPage.FindByName<ExtendedPicker>("assetpicker");
            //btnAdd = CurrentPage.FindByName<Button>("btnAddCtl");
            //btnOk = CurrentPage.FindByName<Button>("btnOKCtl");
            //ectrlRoadName = CurrentPage.FindByName<EntryControl>("ectrlRoadName");
            //ectrlRMU = CurrentPage.FindByName<EntryControl>("ectrlRMU");
            //ectrlSection = CurrentPage.FindByName<EntryControl>("ectrlSection");
            //ectrlRefNo = CurrentPage.FindByName<EntryControl>("ectrlRefNo");           
            //userinspcode = CurrentPage.FindByName<ExtendedPicker>("insppicker");
            //uservercode = CurrentPage.FindByName<ExtendedPicker>("verpicker");

            if (Type == "Edit")
            {
                NotToEdit = false;
                btnOk.IsVisible = ibValue;
                if (!btnOk.IsVisible)
                {
                    btnAdd.IsVisible = true;
                    CanSave = true;
                    IsHeaderEnable = false;
                }
                //ectrlRefNo.IsEnabled = ibValue;
                //ectrlRMU.IsEnabled = ibValue;
                //ectrlRoadName.IsEnabled = ibValue;
                //ectrlSection.IsEnabled = ibValue;

            }
            else if (Type == "View")
            {
                NotToEdit = true;
                btnAdd.IsVisible = ibValue;
                btnOk.IsVisible = ibValue;
                CanSave = false;
                IsHeaderEnable = false;
                //ectrlRefNo.IsEnabled = ibValue;
                //ectrlRMU.IsEnabled = ibValue;
                //ectrlRoadName.IsEnabled = ibValue;
                //ectrlSection.IsEnabled = ibValue;
            }
            else if (Type == "Add")
            {
                NotToEdit = false;
                //btnAdd.IsEnabled = ibValue;
                if (CanSave)
                    btnOk.IsVisible = false;
                else
                    btnOk.IsVisible = ibValue;

                if (!btnOk.IsVisible)
                {
                    btnAdd.IsVisible = ibValue;
                    CanSave = true;
                    IsHeaderEnable = false;
                }
                else
                    IsHeaderEnable = true;
                //ectrlRefNo.IsEnabled = ibValue;
                //ectrlRMU.IsEnabled = ibValue;
                //ectrlRoadName.IsEnabled = ibValue;
                //ectrlSection.IsEnabled = ibValue;
            }
        }


        public async Task<FormAHeaderResponseDTO> GetEditViewHeaderdetails(int HeaderCode)
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

                    var response = await _restApi.GetFormAHdrById(HeaderCode);

                    if (response.success)
                    {
                        SelectedHdrEditItem = response.data;

                        SelectedRoadCode = SelectedHdrEditItem.RoadCode;

                        strGetRoadCode = SelectedHdrEditItem.RoadName;

                        StrGetRMUCode = SelectedHdrEditItem.Rmu;

                        StrGetSectioncode = SelectedHdrEditItem.section;

                        SelectedMonth = SelectedHdrEditItem.Month;

                        SelectedYear = SelectedHdrEditItem.Year;

                        SelectedSection = SelectedHdrEditItem.section;

                        StrRefcode = SelectedHdrEditItem.Id;

                        strInspSign = SelectedHdrEditItem.SignPrp;

                        strVerSign = SelectedHdrEditItem.SignVer;

                        Selectedinspuser = SelectedHdrEditItem.UseridPrp.ToString();


                        //await GetInspUserList();

                        //userinspcode = CurrentPage.FindByName<ExtendedPicker>("insppicker");

                        //userinspcode.ItemsSource = DDInspUserListListItems.Select((DDListItems arg) => arg.Text).ToList();

                        //int inspindex = DDInspUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == SelectedHdrEditItem.UseridPrp);

                        //if (inspindex == -1) { inspindex = 1; }

                        //userinspcode.SelectedIndex = inspindex;



                        strinsName = SelectedHdrEditItem.UsernamePrp;

                        strinspDesignation = SelectedHdrEditItem.DesignationPrp;


                        dtDateinsp = SelectedHdrEditItem.DtPrp.HasValue ? SelectedHdrEditItem.DtPrp.Value : (DateTime?)null; //DateTime.Now.Date; 


                        Selectedverpuser = SelectedHdrEditItem.UseridVer.ToString();


                        strverName = SelectedHdrEditItem.UsernameVer;

                        strverDesignation = SelectedHdrEditItem.DesignationVer;

                        //vercode = CurrentPage.FindByName<DatePicker>("verdatepicker");

                        dtdateever = SelectedHdrEditItem.VerifiedDt.HasValue ? SelectedHdrEditItem.VerifiedDt.Value : (DateTime?)null; //DateTime.Now.Date; 


                        inspimage = Xamarin.Forms.ImageSource.FromStream(
                        () => new MemoryStream(Convert.FromBase64String(strInspSign)));

                        padView = CurrentPage.FindByName<SignaturePadView>("SignatureView");

                        //if(inspimage != null || inspimage.IsEmpty)
                        //byte[] bytes = Convert.FromBase64String(strInspSign);

                        // byte[] resizedImage = await CrossImageResizer.Current.ResizeImageWithAspectRatioAsync(bytes, 500, 1000);
                        //float[] dataArray = Enumerable.Range(0, bytes.Length / 4).Select(i => BitConverter.ToSingle(bytes, i * 4)).ToArray();

                        //Point[] points;
                        //using (var ms = new MemoryStream(bytes))
                        //{
                        //    using (var r = new BinaryReader(ms))
                        //    {
                        //        int len = r.ReadInt32();
                        //        points = new Point[len];
                        //        for (int i = 0; i != len; i++)
                        //        {
                        //            points[i] = new Point(r.ReadInt32(), r.ReadInt32());
                        //        }
                        //    }
                        //}

                        //padView.Points = ;

                        padView.BackgroundImage = inspimage;

                        verimage = Xamarin.Forms.ImageSource.FromStream(
                        () => new MemoryStream(Convert.FromBase64String(strVerSign)));

                        vpadview = CurrentPage.FindByName<SignaturePadView>("VPadView");
                        vpadview.BackgroundImage = verimage;

                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return SelectedHdrEditItem;
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
            return new FormAHeaderResponseDTO();
        }




        public async Task<FormAHeaderResponseDTO> GetHeaderdetails(int HeaderCode)
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

                    var response = await _restApi.GetFormAHdrById(HeaderCode);

                    if (response.success)
                    {
                        SelectedNewHdrItem = response.data;


                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return SelectedNewHdrItem;
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
            return new FormAHeaderResponseDTO();
        }

        public ICommand FormAListItemTappedCommand
        {
            get
            {

                return new Command(async (obj) =>
                {
                    SelectedFormARowItem = (FormADetailResponseDTO)obj;
                });
            }
        }


        public async Task<string> GetAssetCodeByNameFormA(string assetGroup)
        {

            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {

                    try
                    {


                        var response = await _restApi.GetAssetCodeByNameFormA(assetGroup);

                        if (response.success)
                        {
                            App.AssetGroupSelection = "";

                            StrAssertValue = response.data.ToString();

                            App.AssetGroupSelection = StrAssertValue;

                            //btnOk.IsEnabled = true;
                        }
                        else
                            _userDialogs.Toast(response.errorMessage);
                        //iStrValue = response.ToString();
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
            return StrAssertValue;
        }






        public async Task<string> GetReferenceNumber(string strGroadCode, int strGmonth, string StrGyear, string assetGroup)
        {

            try
            {


                if (CrossConnectivity.Current.IsConnected)
                {

                    try
                    {


                        var response = await _restApi.GetReferenceNOData(strGroadCode, strGmonth, StrGyear, assetGroup);

                        if (response.success)
                        {

                            StrRefcode = response.data.ToString();

                            //btnOk.IsEnabled = true;
                        }
                        else
                            _userDialogs.Toast(response.errorMessage);
                        //iStrValue = response.ToString();
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
            return StrRefcode;
        }



        public async Task<UserResponseDTO> GetUserDetilsList(string usertype, int iUser)
        {
            // _userDialogs.ShowLoading("Loading");

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
                                SelectedInspUserItem = response.data;
                                if (SelectedHdrEditItem != null && SelectedHdrEditItem.UsernamePrp != null && SelectedHdrEditItem.UsernamePrp == SelectedInspUserItem.UserName || SelectedInspUserItem.UserName.ToLower() == "others")
                                {
                                    strinsName = SelectedHdrEditItem.UsernamePrp;
                                    strinspDesignation = SelectedHdrEditItem.DesignationPrp;
                                }
                                else
                                {
                                    strinsName = SelectedInspUserItem.UserName;
                                    strinspDesignation = SelectedInspUserItem.Position;
                                }

                                    
                            }
                            else if (usertype == "veruser")
                            {
                                SelectedVerUserItem = response.data;
                                if (SelectedHdrEditItem != null && SelectedHdrEditItem.UsernameVer != null && SelectedHdrEditItem.UsernameVer == SelectedVerUserItem.UserName || SelectedVerUserItem.UserName.ToLower() == "others")
                                {
                                    strverName = SelectedHdrEditItem.UsernameVer;
                                    strverDesignation = SelectedHdrEditItem.DesignationVer;
                                }
                                else
                                {
                                    strverName = SelectedVerUserItem.UserName;
                                    strverDesignation = SelectedVerUserItem.Position;
                                }

                                    
                            }

                        }
                        catch (Exception ex)
                        {
                            //_userDialogs.Alert(ex.Message);
                            _userDialogs.HideLoading();



                        }

                        return SelectedInspUserItem;
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return SelectedInspUserItem;

                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");

                return SelectedInspUserItem;
            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }

            return new UserResponseDTO();
        }





        public async Task<ObservableCollection<FormAHeaderResponseDTO>> SaveFormAHeaderList(string Type)
        {
            // _userDialogs.ShowLoading("Loading");

            try
            {
                _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    if (Type == "Grid")
                    {
                        GridItems = new FormAHeaderRequestDTO()
                        {
                            RoadCode = SelectedRoadCode,

                            RoadName = strGetRoadCode,

                            Rmu = StrGetRMUCode,

                            section = StrGetSectioncode,

                            Month = SelectedMonth,

                            Year = SelectedYear,

                            AssetGroupCode = SelectedSection,

                            Id = StrRefcode
                        };
                    }


                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

                    var response = await _restApi.SaveFormAHdr(GridItems);



                    if (response.success)
                    {
                        try
                        {

                            GetHeaderNoCode = response.data.No;
                            strinsName = response.data.UsernamePrp;
                            strinspDesignation = response.data.DesignationPrp;
                            dtDateinsp = response.data.DtPrp.HasValue ? response.data.DtPrp.Value : (DateTime?)null;
                            strverName = response.data.UsernameVer;
                            strverDesignation = response.data.DesignationVer;
                            dtdateever = response.data.VerifiedDt.HasValue ? response.data.VerifiedDt.Value : (DateTime?)null;

                            int inspindex = DDInspUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.UseridPrp);
                            userinspcode.SelectedIndex = inspindex;

                            int iverindex = DDVerUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.UseridVer);
                            uservercode.SelectedIndex = iverindex;

                            var inspimage = Xamarin.Forms.ImageSource.FromStream(
                            () => new MemoryStream(Convert.FromBase64String(response.data.SignPrp)));
                            padView = CurrentPage.FindByName<SignaturePadView>("SignatureView");
                            padView.BackgroundImage = inspimage;

                            var verimage = Xamarin.Forms.ImageSource.FromStream(
                            () => new MemoryStream(Convert.FromBase64String(response.data.SignVer)));
                            vpadview = CurrentPage.FindByName<SignaturePadView>("VPadView");
                            vpadview.BackgroundImage = verimage;

                            GetMyFormAHeaderEditAndViewListReports(GetHeaderNoCode);
                            if (GetHeaderNoCode > 0)
                            {
                                btnOk.IsEnabled = false;
                                btnOk.IsVisible = false;
                                btnAdd.IsVisible = true;
                                CanSave = true;
                                IsHeaderEnable = false;
                            }
                            else
                            {
                                btnOk.IsVisible = true;
                                btnOk.IsEnabled = true;
                                btnAdd.IsVisible = false;
                                CanSave = false;
                                IsHeaderEnable = true;
                            }

                            GetFormDetailsCount = response.data.FormADetails.Count;

                            if (response.data.Id.Length > 0)
                            {
                                _userDialogs.HideLoading();

                                //await UserDialogs.Instance.PromptAsync("Already Header Detail Available. Do you want to Continue.", "RAMMS", "OK");
                                //DetailFromADtlGridListItems = new ObservableCollection<FormADetailResponseDTO>(response.data.FormADetails);

                                if (GetHeaderNoCode.ToString().Length > 2)
                                {
                                    DetailFromReqADtlGridListItems = await GetDetailNewGridListDetails(GetHeaderNoCode);
                                }

                            }
                            else
                            {
                                _userDialogs.HideLoading();

                                //UserDialogs.Instance.Alert("Header Details Saved Successfully.");

                            }


                        }
                        catch (Exception ex)
                        {
                            //_userDialogs.Alert(ex.Message);
                            _userDialogs.HideLoading();

                            //UserDialogs.Instance.Alert("Header Details Saved Successfully.");

                        }

                        return DetailFromAHdrGridListItems;
                    }
                    else
                        //_userDialogs.Toast(response.errorMessage);

                        return DetailFromAHdrGridListItems;

                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");

                IsEmpty = DetailFromAHdrGridListItems.Count == 0;
                LstViewHeightRequest = DetailFromAHdrGridListItems.Count * 40;
                return DetailFromAHdrGridListItems;
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




        public async Task<ObservableCollection<FormAHeaderResponseDTO>> UpddateFormAHeaderList(int HeaderNo, string InspSign, string versign, string Type,
            int? inspid, string strinsname, string strinsdesign, DateTime? dtinspdate, int? verid, string vername, string verdesign, DateTime? dtverdate)
        {

            _userDialogs.ShowLoading("Loading");

            try
            {

                if (CrossConnectivity.Current.IsConnected)
                {

                    if (inspid == 0) { inspid = null; }
                    if (verid == 0) { verid = null; }

                    if (Type == "Save")
                    {
                        objValue = new FormAHeaderRequestDTO()
                        {
                            No = HeaderNo,
                            SignPrp = InspSign,
                            SignVer = versign,
                            UseridPrp = inspid,
                            UsernamePrp = strinsname,
                            DesignationPrp = strinsdesign,
                            DtPrp = dtinspdate,
                            UseridVer = verid,
                            UsernameVer = vername,
                            DesignationVer = verdesign,
                            VerifiedDt = dtverdate

                        };
                    }
                    else if (Type == "Submit")
                    {
                        objValue = new FormAHeaderRequestDTO()
                        {
                            No = HeaderNo,
                            SubmitSts = true,
                            SignPrp = InspSign,
                            SignVer = versign,
                            UseridPrp = inspid,
                            UsernamePrp = strinsname,
                            DesignationPrp = strinsdesign,
                            DtPrp = dtinspdate,
                            UseridVer = verid,
                            UsernameVer = vername,
                            DesignationVer = verdesign,
                            VerifiedDt = dtverdate
                        };

                    }

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(objValue);

                    var response = await _restApi.UpdateSignature(objValue);


                    if (response.success)
                    {
                        _userDialogs.ShowLoading("Loading");
                        try

                        {
                            if (response.data > 0)
                            {
                                _userDialogs.HideLoading();

                                if (Type == "Save")
                                    await UserDialogs.Instance.AlertAsync("Data Saved Successfully.", "RAMS", "OK");
                                else
                                    await UserDialogs.Instance.AlertAsync("Data Submitted Successfully.", "RAMS", "OK");

                            }
                            else
                            {
                                _userDialogs.HideLoading();

                            }


                        }
                        catch (Exception ex)
                        {
                            //_userDialogs.Alert(ex.Message);
                            _userDialogs.HideLoading();

                            UserDialogs.Instance.Alert("Header Details Saved Successfully.");

                        }

                        return DetailFromAHdrGridListItems;
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return DetailFromAHdrGridListItems;

                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");

                IsEmpty = DetailFromAHdrGridListItems.Count == 0;
                LstViewHeightRequest = DetailFromAHdrGridListItems.Count * 40;
                return DetailFromAHdrGridListItems;
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





        public async Task<ObservableCollection<FormAHeaderResponseDTO>> GetMyFormADetailListReports(string Type, int Formid)
        {
            _userDialogs.ShowLoading("Loading");

            try
            {

                if (CrossConnectivity.Current.IsConnected)
                {
                    if (Type == "Grid")
                    {
                        GridItems = new FormAHeaderRequestDTO()
                        {
                            Id = Formid.ToString(),

                        };
                    }


                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

                    //I need to discuss with pravin for get detail
                    var response = await _restApi.SaveFormAHdr(GridItems);

                    if (response.success)
                    {
                        _userDialogs.ShowLoading("Loading");
                        DetailGridListViewItems = new ObservableCollection<FormADetailResponseDTO>(response.data.FormADetails);

                        //if (response.data.Count > 0)
                        //{
                        //    _userDialogs.HideLoading();

                        //    UserDialogs.Instance.Alert("Already Data Exist. Loading Reference Detail Data.");
                        //}

                        return DetailFromAHdrGridListItems;
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return DetailFromAHdrGridListItems;

                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");

                IsEmpty = DetailFromAHdrGridListItems.Count == 0;

                LstViewHeightRequest = DetailFromAHdrGridListItems.Count * 40;

                return DetailFromAHdrGridListItems;
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


        public async Task<int> GetLastReferenceNumber(int RefCode)
        {
            int iStrValue = 0;

            try
            {
                //_userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {

                    try
                    {


                        var response = await _restApi.DetailSrNo(RefCode);

                        //var json = Newtonsoft.Json.JsonConvert.DeserializeObject(response.ToString());
                        if (response.success)
                        {
                            iStrValue = response.data + 1;

                            return iStrValue;

                        }
                        else
                            _userDialogs.Toast(response.errorMessage);


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
            //finally
            //{

            //    //_userDialogs.HideLoading();
            //}
            return iStrValue;
        }


        public ICommand ClickMeAction
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    try
                    {
                        
                        var actionResult = "";

                        var objValue = (FormADetailResponseDTO)obj;

                        SelectedFormARowItem = objValue;

                        if (App.ReturnType == "View")
                        {
                            _editViewModel.Type = "View";
                            ViewType = false;
                        }


                        if (ViewType == true)
                        {
                            if (Model.Security.IsDelete(ModuleNameList.NOD))
                                actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "Edit", "View", "Delete");
                            else
                                actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "Edit", "View");

                        }
                        else

                            actionResult = await UserDialogs.Instance.ActionSheetAsync("", "", null, null, "View");


                        if (actionResult == "Delete")
                        {
                            var actionResult1 = await UserDialogs.Instance.ConfirmAsync("Are you sure want to delete this record?", "RAMS", "Yes", "No");
                            if (actionResult1)
                            {
                                //Delete details
                                _editViewModel.HdrFahPkRefNo = SelectedFormARowItem.No;

                                _editViewModel.HdrFahRefNo = SelectedFormARowItem.HeaderNo;

                                await DeleteHeaderdetails(_editViewModel.HdrFahPkRefNo);

                                //GetHeaderNoCode = _editViewModel.HdrFahPkRefNo;

                                if (GetHeaderNoCode.ToString().Length > 2)
                                    DetailGridListViewItems = await GetMyFormAHeaderEditAndViewListReports(_editViewModel.HdrFahRefNo);

                                return;
                            }
                        }
                        if (actionResult == "Edit")
                        {
                            App.DetailHeaderCode = _editViewModel.HdrFahPkRefNo;
                            App.DetailType = "Edit";

                            if (GetHeaderNoCode == 0)
                                GetHeaderNoCode = App.DetailHeaderCode;

                            SelectedNewHdrItem = await GetHeaderdetails(GetHeaderNoCode);

                            App.DetailHeaderCode = GetHeaderNoCode;

                            SelectedNewHdrItem.No = GetHeaderNoCode;

                            SelectedNewHdrItem.Id = SelectedNewHdrItem.Id;

                            App.HeaderCode = SelectedFormARowItem.No;

                            App.IReferenceNo = SelectedNewHdrItem.Id;

                            await CoreMethods.PushPageModel<FormAAddPageModel>(SelectedNewHdrItem);
                        }
                        else if (actionResult == "View")
                        {

                            App.DetailType = "View";

                            if (GetHeaderNoCode == 0)

                                GetHeaderNoCode = App.DetailHeaderCode;

                            SelectedNewHdrItem = await GetHeaderdetails(GetHeaderNoCode);

                            App.DetailHeaderCode = GetHeaderNoCode;

                            SelectedNewHdrItem.No = GetHeaderNoCode;

                            SelectedNewHdrItem.Id = SelectedNewHdrItem.Id;

                            App.HeaderCode = SelectedFormARowItem.No;

                            App.IReferenceNo = SelectedNewHdrItem.Id;

                            await CoreMethods.PushPageModel<FormAAddPageModel>(SelectedNewHdrItem);
                        }
                    }
                    catch (Exception ex) { _userDialogs.Alert(ex.Message); }
                });
            }
        }



        public async Task<int> DeleteHeaderdetails(int DetailCode)
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

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(DetailCode);

                    var response = await _restApi.DeleteDetail(DetailCode);


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





        public ICommand OKCommand
        {

            //inserted header information 
            get
            {
                return new FreshCommand(async (obj) =>
                {

                    try
                    {

                        if (string.IsNullOrWhiteSpace(strGetRoadCode) && string.IsNullOrWhiteSpace(StrGetRMUCode) && string.IsNullOrWhiteSpace(StrGetSectioncode))
                        {
                            await UserDialogs.Instance.AlertAsync("Please Enter all data", "RAMS", "OK");
                            return;
                        }

                        if (SelectedYear == null || SelectedMonth == null)
                        {
                            await UserDialogs.Instance.AlertAsync("Please Enter Month and Year", "RAMS", "OK");
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(SelectedSection))
                        {
                            await UserDialogs.Instance.AlertAsync("Please Select Asset Group", "RAMS", "OK");
                            return;
                        }


                        var response = SaveFormAHeaderList("Grid");

                    }
                    catch
                    {
                    }


                });
            }
        }


        public ICommand inspredosign
        {
            get
            {

                return new FreshCommand(async (obj) =>
                {


                    padView = CurrentPage.FindByName<SignaturePadView>("SignatureView");

                    padView.BackgroundImage = null;


                });

            }
        }


        public ICommand verredosign
        {

            get
            {

                return new FreshCommand(async (obj) =>
                {
                    vpadview = CurrentPage.FindByName<SignaturePadView>("VPadView");

                    vpadview.BackgroundImage = null;
                });

            }
        }


        public ICommand AddCommand
        {

            //inserted header information 



            get
            {


                return new FreshCommand(async (obj) =>
                {


                    try
                    {



                        if (string.IsNullOrWhiteSpace(strGetRoadCode) && string.IsNullOrWhiteSpace(StrGetRMUCode) && string.IsNullOrWhiteSpace(StrGetSectioncode))
                        {
                            await UserDialogs.Instance.AlertAsync("Please Enter all data", "RAMS", "OK");
                            return;
                        }

                        if (SelectedYear == null || SelectedMonth == null)
                        {
                            await UserDialogs.Instance.AlertAsync("Please Enter Month and Year", "RAMS", "OK");
                            return;
                        }

                        if (string.IsNullOrWhiteSpace(SelectedSection))
                        {
                            await UserDialogs.Instance.AlertAsync("Please Select Asset Group", "RAMS", "OK");
                            return;
                        }


                        App.DetailType = "Add";

                        if (GetHeaderNoCode != 0)
                        {
                            SelectedNewHdrItem = await GetHeaderdetails(GetHeaderNoCode);

                            SelectedNewHdrItem.No = GetHeaderNoCode;
                        }

                        SelectedNewHdrItem.Id = SelectedNewHdrItem.Id;

                        await CoreMethods.PushPageModel<FormAAddPageModel>(SelectedNewHdrItem);
                    }
                    catch (Exception ex)
                    {
                        await UserDialogs.Instance.AlertAsync("Please check with Administrator.", "RAMS", "OK");
                        return;
                    }
                });
            }
        }


        public async void GetSignature()
        {
            try
            {
                IFolder rootFolder = FileSystem.Current.LocalStorage;
                //IFolder myCoolFolder = await rootFolder.CreateFolderAsync("FormA", CreationCollisionOption.ReplaceExisting);

                try
                {

                    padView = CurrentPage.FindByName<SignaturePadView>("SignatureView");

                    Stream image = await padView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Jpeg, strokeColor: Color.Black, Color.Transparent, false, true);


                    if (!padView.IsBlank)
                    {

                        using (BinaryReader binaryReader = new BinaryReader(image))
                        {
                            binaryReader.BaseStream.Position = 0;
                            byte[] Signature = binaryReader.ReadBytes((int)image.Length);
                            //await PCLHelper.SaveImage(Signature, "Inspsign", myCoolFolder);
                        }

                        var signatureMemoryStream = image as System.IO.MemoryStream;

                        var byteArray = signatureMemoryStream.ToArray();

                        string base64String = Convert.ToBase64String(byteArray);

                        App.inspSign = base64String;
                    }
                }
                catch { }


                vpadview = CurrentPage.FindByName<SignaturePadView>("VPadView");

                Stream vimage = await vpadview.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Jpeg, strokeColor: Color.Black, Color.Transparent, false, true);


                if (!vpadview.IsBlank)
                {


                    using (BinaryReader binaryReader = new BinaryReader(vimage))
                    {
                        binaryReader.BaseStream.Position = 0;
                        byte[] Signature = binaryReader.ReadBytes((int)vimage.Length);
                        //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                    }

                    var vsignatureMemoryStream = vimage as System.IO.MemoryStream;

                    var byteArray = vsignatureMemoryStream.ToArray();

                    string base64String = Convert.ToBase64String(byteArray);

                    //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                    App.versign = base64String;
                }
            }
            catch
            {

            }
        }

        public ICommand FormASaveCommand
        {

            get
            {
                return new FreshCommand(async (obj) =>
                {
                    try
                    {
                        App.inspSign = "";
                        App.versign = "";


                        try
                        {
                            IFolder rootFolder = FileSystem.Current.LocalStorage;
                            //IFolder myCoolFolder = await rootFolder.CreateFolderAsync("FormA", CreationCollisionOption.ReplaceExisting);

                            try
                            {

                                padView = CurrentPage.FindByName<SignaturePadView>("SignatureView");

                                Stream image = await padView.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);


                                if (!padView.IsBlank)
                                {

                                    using (BinaryReader binaryReader = new BinaryReader(image))
                                    {
                                        binaryReader.BaseStream.Position = 0;
                                        byte[] Signature = binaryReader.ReadBytes((int)image.Length);
                                        //await PCLHelper.SaveImage(Signature, "Inspsign", myCoolFolder);
                                    }

                                    var signatureMemoryStream = image as System.IO.MemoryStream;

                                    var byteArray = signatureMemoryStream.ToArray();

                                    string base64String = Convert.ToBase64String(byteArray);

                                    App.inspSign = base64String;
                                }
                                else
                                    App.inspSign = null;
                            }
                            catch { }


                            vpadview = CurrentPage.FindByName<SignaturePadView>("VPadView");

                            Stream vimage = await vpadview.GetImageStreamAsync(SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);



                            if (!vpadview.IsBlank)
                            {


                                using (BinaryReader binaryReader = new BinaryReader(vimage))
                                {
                                    binaryReader.BaseStream.Position = 0;

                                    byte[] Signature = binaryReader.ReadBytes((int)vimage.Length);
                                    //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                                }

                                var vsignatureMemoryStream = vimage as System.IO.MemoryStream;

                                var byteArray = vsignatureMemoryStream.ToArray();

                                string base64String = Convert.ToBase64String(byteArray);

                                //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                                App.versign = base64String;

                            }
                            else
                                App.versign = null;
                        }
                        catch
                        {

                        }

                        //User list to update on header including designation


                        if (GetHeaderNoCode == 0 || GetHeaderNoCode == 1)
                            GetHeaderNoCode = App.DetailHeaderCode;

                        if (App.inspSign == null) { App.inspSign = null; }
                        if (App.versign == null) { App.versign = null; }

                        if (Selectedinspuser == "" || Selectedinspuser == null) { Selectedinspuser = "0"; }
                        if (Selectedverpuser == "" || Selectedverpuser == null) { Selectedverpuser = "0"; }

                        //if (dtdateever == null) 
                        //{
                        //    dtdateever = DateTime.Now.Date;
                        //}
                        //if (dtDateinsp == null)
                        //{
                        //    dtDateinsp = DateTime.Now.Date;
                        //}

                        var inspDate = dtDateinsp.HasValue ? dtDateinsp.Value : (DateTime?)null;
                        var verDate = dtdateever.HasValue ? dtdateever.Value : (DateTime?)null;

                        await UpddateFormAHeaderList(GetHeaderNoCode, App.inspSign, App.versign, "Save",
                            Convert.ToInt32(Selectedinspuser), strinsName, strinspDesignation, inspDate, Convert.ToInt32(Selectedverpuser), strverName, strverDesignation, verDate);

                        await CurrentPage.Navigation.PopAsync();

                    }

                    catch (Exception ex)
                    { }
                });
            }


        }

        public ICommand FormASubmitCommand
        {

            get
            {
                return new FreshCommand(async (obj) =>
                {
                    //Submited command 
                    //issubmit is true or false

                    try
                    {
                        IFolder rootFolder = FileSystem.Current.LocalStorage;
                        //IFolder myCoolFolder = await rootFolder.CreateFolderAsync("FormA", CreationCollisionOption.ReplaceExisting);

                        try
                        {

                            padView = CurrentPage.FindByName<SignaturePadView>("SignatureView");

                            Stream image = await padView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Jpeg, strokeColor: Color.Black, Color.Transparent, false, true);


                            if (!padView.IsBlank)
                            {

                                using (BinaryReader binaryReader = new BinaryReader(image))
                                {
                                    binaryReader.BaseStream.Position = 0;
                                    byte[] Signature = binaryReader.ReadBytes((int)image.Length);
                                    //await PCLHelper.SaveImage(Signature, "Inspsign", myCoolFolder);
                                }

                                var signatureMemoryStream = image as System.IO.MemoryStream;

                                var byteArray = signatureMemoryStream.ToArray();

                                string base64String = Convert.ToBase64String(byteArray);

                                App.inspSign = base64String;
                            }
                            else
                                App.inspSign = null;
                        }
                        catch { }


                        vpadview = CurrentPage.FindByName<SignaturePadView>("VPadView");

                        Stream vimage = await vpadview.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Jpeg, strokeColor: Color.Black, Color.Transparent, false, true);


                        if (!vpadview.IsBlank)
                        {


                            using (BinaryReader binaryReader = new BinaryReader(vimage))
                            {
                                binaryReader.BaseStream.Position = 0;
                                byte[] Signature = binaryReader.ReadBytes((int)vimage.Length);
                                //await PCLHelper.SaveImage(Signature, "Versign", myCoolFolder);
                            }

                            var vsignatureMemoryStream = vimage as System.IO.MemoryStream;

                            var byteArray = vsignatureMemoryStream.ToArray();

                            string base64String = Convert.ToBase64String(byteArray);

                            //PadView..Source = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                            App.versign = base64String;
                        }
                        else
                            App.versign = null;
                    }
                    catch
                    {

                    }

                    if (GetHeaderNoCode == 0 || GetHeaderNoCode == 1)

                        GetHeaderNoCode = App.DetailHeaderCode;

                    //if (App.inspSign == null) { App.inspSign = ""; } if(App.versign==null) { App.versign = ""; }
                    if (Selectedinspuser == "" || Selectedinspuser == null) { Selectedinspuser = "0"; }
                    if (Selectedverpuser == "" || Selectedverpuser == null) { Selectedverpuser = "0"; }

                    DateTime? inspDate = dtDateinsp.HasValue ? dtDateinsp.Value : (DateTime?)null;
                    DateTime? verDate = dtdateever.HasValue ? dtdateever.Value : (DateTime?)null;

                    //await UpddateFormAHeaderList(GetHeaderNoCode, App.inspSign, App.versign, "Submit");

                    await UpddateFormAHeaderList(GetHeaderNoCode, App.inspSign, App.versign, "Submit",
                             Convert.ToInt32(Selectedinspuser), strinsName, strinspDesignation, inspDate, Convert.ToInt32(Selectedverpuser), strverName, strverDesignation, verDate);


                    await CurrentPage.Navigation.PopAsync();
                });
            }



        }


        public ICommand FormACancelCommand
        {

            get
            {
                return new FreshCommand(async (obj) =>
                {
                    if (AppType != "View")
                    {
                        var actionResult = await UserDialogs.Instance.ConfirmAsync(" Unsaved changes will be lost. Are you sure want to cancel?", "RAMS", "Yes", "No");
                        if (actionResult)
                            await CurrentPage.Navigation.PopAsync();
                    }
                    else
                        await CurrentPage.Navigation.PopAsync();
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
                    if (iValueRet > 1)
                        iValueRet = iValueRet - 1;

                    await ButtonPagination(GetHeaderNoCode, iValueRet);
                });
            }
        }


        private async Task<int> ButtonPagination(int iHeaderNo, int Value)
        {


            GridDetailItems = new FilteredPagingDefinition<FormADetailsRequestDTO>()
            {
                StartPageNo = (iValueRet - 1) * pageno,

                RecordsPerPage = pageno,
                sortOrder = "0",
                Filters = new FormADetailsRequestDTO() { HeaderNo = iHeaderNo },
            };

            var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridDetailItems);

            var response = await _restApi.GetDetails(GridDetailItems);

            if (response.success)
            {
                startPage = ((iValueRet - 1) * pageno) + 1;

                totalsize = response.data.TotalRecords.ToString();

                if ((iValueRet * pageno) > Convert.ToInt32(totalsize))
                    pagesize = totalsize;
                else
                    pagesize = (iValueRet * pageno).ToString(); //response.data.FilteredRecords.ToString();

                DetailGridListViewItems = new ObservableCollection<FormADetailResponseDTO>(response.data.PageResult);

            }
            else
                _userDialogs.Toast(response.errorMessage);




            return 1;
        }

        public ICommand btnNextCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {

                    int iRet = (Convert.ToInt32(totalsize) / pageno);

                    if (iValueRet <= iRet)
                        iValueRet = iValueRet + 1;

                    await ButtonPagination(GetHeaderNoCode, iValueRet);

                });
            }
        }




    }
}