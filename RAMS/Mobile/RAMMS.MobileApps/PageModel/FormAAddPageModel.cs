using Acr.UserDialogs;
using DLToolkit.Forms.Controls;
using FreshMvvm;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Plugin.Media;
using Plugin.Media.Abstractions;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.MobileApps.Page;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
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
    public class FormAAddPageModel : FreshBasePageModel
    {
        private IUserDialogs _userDialogs;
        private IRestApi _restApi;
        private bool _isPhotoTabVisible;
        public ILocalDatabase _localDatabase;
        private int? _selectedWI = null;
        private int? _selectedWS = null;
        private int? _selectedWTC = null;
        private int? _selectedWC = null;
        private int? _selectedShiftPs = null;
        private int? _selectedShiftWI = null;
        private int? _selectedRT = null;
        public EditViewModel editViewModel { get; set; }
        public string UploadFileName { get; set; }
        public FileData fileDataList { get; set; }
        public string FormAPPText { get; set; }
        public bool ViewType { get; set; }

        public bool NotToEdit { get; set; }
        public int? intSrNo { get; set; }
        public ObservableCollection<DDListItems> MonthListItems { get; set; }

        public ObservableCollection<DDListItems> WeekListItems { get; set; }

        public ObservableCollection<DDListItems> DDDistressCodeListItems { get; set; }

        public ObservableCollection<DDListItems> DDLocationListItems { get; set; }

        public ObservableCollection<DDListItems> DDActivityCodeListItems { get; set; }

        public ObservableCollection<DDListItems> DDUnitListItems { get; set; }

        public ObservableCollection<DDListItems> DDPriorityListItems { get; set; }
        public ObservableCollection<DDListItems> DDShiftPSListItems { get; set; }

        public ObservableCollection<RmFormaImageDtl> SaveItems { get; set; }

        public FormADetailsRequestDTO SelecteddtlEditItem { get; set; }

        public ObservableCollection<FormAImageListRequestDTO> DetailImageList { get; set; }

        public ObservableCollection<ImageUploadFormATABDTO> ImageList { get; set; }

        FlowListView imageListView;

        Uri ImageUrl { get; set; }

        public string strentry { get; set; }

        public int? FromMText { get; set; }

        public int? FromKmText { get; set; }

        public int? ToKmText { get; set; }

        public int? ToMText { get; set; }

        public string DescriptionText { get; set; }

        public string ADPText { get; set; }

        public string CDRText { get; set; }

        private double? cdrlText = null;
        private double? cdrwText = null;
        private double? cdrhText = null;
        public double? CDRLText
        {
            get { return cdrlText; }
            set
            {
                if (value == null || value == 0)
                {
                    cdrlText = null;
                }
                else if (!cdrlText.HasValue || !cdrlText.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count()>1 && textString[1].Length <= 5)
                        cdrlText = Convert.ToDouble(value.Value.ToString("0.#####"));
                    else if (textString[0].Length > 0 && textString.Count() == 1)
                        cdrlText = value;
                }
                RaisePropertyChanged();
            }
        }

        public double? CDRWText 
        {
            get { return cdrwText; }
            set
            {
                if (value == null || value == 0)
                {
                    cdrwText = null;
                }
                else if (!cdrwText.HasValue || !cdrwText.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        cdrwText = Convert.ToDouble(value.Value.ToString("0.#####"));
                    else if (textString[0].Length > 0 && textString.Count() == 1)
                        cdrwText = value;
                }
                RaisePropertyChanged();
            }
        }

        public double? CDRHText 
        {
            get { return cdrhText; }
            set
            {
                if (value == null || value == 0)
                {
                    cdrhText = null;
                }
                else if (!cdrhText.HasValue || !cdrhText.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        cdrhText = Convert.ToDouble(value.Value.ToString("0.#####"));
                    else if (textString[0].Length > 0 && textString.Count() == 1)
                        cdrhText = value;
                }
                RaisePropertyChanged();
            }
        }

        public string RemarksText { get; set; }

        public string SelectedDistressCode { get; set; }

        ObservableCollection<List<string>> selectName { get; set; }

        public DateTime SelectedDateTime { get; set; }

        public DateTime? SelectedDate { get; set; } = null;
        public DateTime? MinimumDate { get; set; } = null;
        public DateTime? MaximumDate { get; set; } = null;

        public string SelectedLocation { get; set; }

        public List<string> SelectedLocationList { get; set; }

        public int GetHeaderNo { get; set; }

        public int? iGetHeaderNo { get; set; }

        public int GetLastReferenceNo { get; set; }

        public string SelectedActivityCode { get; set; }
        public string FormH { get; set; }

        public List<String> SiteRef_multiSelect { get; set; }

        public string SelectedRefNo { get; set; }

        public string selectHeaderNo { get; set; }

        public string SelectedUnit { get; set; }

        public string SelectedPriority { get; set; }
        public string SelectedShiftPS { get; set; }

        public int? SelectedWI
        {
            get { return _selectedWI; }
            set
            {
                if (value == null || value == 0)
                {
                    _selectedWI = null;
                }
                else if (!_selectedWI.HasValue || (!_selectedWI.Equals(value) && value.Value <= 52))
                {
                    _selectedWI = value;

                }
                else
                {
                    UserDialogs.Instance.Alert("Please enter the value ranges between 0-52");
                }

                RaisePropertyChanged();
            }
        }

        public int? SelectedWS
        {
            get { return _selectedWS; }
            set
            {
                if (value == null || value == 0)
                {
                    _selectedWS = null;
                }
                else if (!_selectedWS.HasValue || (!_selectedWS.Equals(value) && value.Value <= 52))
                {
                    _selectedWS = value;

                }
                else
                {
                    UserDialogs.Instance.Alert("Please enter the value ranges between 0-52");
                }

                RaisePropertyChanged();
            }
        }
        
        public int? SelectedWTC
        {
            get { return _selectedWTC; }
            set
            {
                if (value == null || value == 0)
                {
                    _selectedWTC = null;
                }
                else if (!_selectedWTC.HasValue || (!_selectedWTC.Equals(value) && value.Value <= 52))
                {
                    _selectedWTC = value;

                }
                else
                {
                    UserDialogs.Instance.Alert("Please enter the value ranges between 0-52");
                }

                RaisePropertyChanged();
            }
        }
        
        public int? SelectedWC
        {
            get { return _selectedWC; }
            set
            {
                if (value == null || value == 0)
                {
                    _selectedWC = null;
                }
                else if (!_selectedWC.HasValue || (!_selectedWC.Equals(value) && value.Value <= 52))
                {
                    _selectedWC = value;

                }
                else
                {
                    UserDialogs.Instance.Alert("Please enter the value ranges between 0-52");
                }

                RaisePropertyChanged();
            }
        }
        
        public int? SelectedShiftPs
        {
            get { return _selectedShiftPs; }
            set
            {
                if (value == null || value ==0)
                {
                    _selectedShiftPs = null;
                }
                else if (!_selectedShiftPs.HasValue || (!_selectedShiftPs.Equals(value) && value.Value <= 52))
                {
                    _selectedShiftPs = value;

                }
                else
                {
                    UserDialogs.Instance.Alert("Please enter the value ranges between 0-52");
                }

                RaisePropertyChanged();
            }
        }
        
        public int? SelectedShiftWI
        {
            get { return _selectedShiftWI; }
            set
            {
                if (value == null || value == 0)
                {
                    _selectedShiftWI = null;
                }
                else if (!_selectedShiftWI.HasValue || (!_selectedShiftWI.Equals(value) && value.Value <= 52))
                {
                    _selectedShiftWI = value;

                }
                else
                {
                    UserDialogs.Instance.Alert("Please enter the value ranges between 0-52");
                }

                RaisePropertyChanged();
            }
        }
        
        public int? SelectedRT
        {
            get { return _selectedRT; }
            set
            {
                if (value == null || value == 0)
                {
                    _selectedRT = null;
                }
                else if (!_selectedRT.HasValue || (!_selectedRT.Equals(value) && value.Value <= 52))
                {
                    _selectedRT = value;

                }
                else
                {
                    UserDialogs.Instance.Alert("Please enter the value ranges between 0-52");
                }

                RaisePropertyChanged();
            }
        }

        public FormAHeaderResponseDTO SelectedHeaderItems { get; set; }

        public FormADetailResponseDTO SelectedDetailItems { get; set; }

        Image imagesrc;

        public string AppType { get; set; }

        public bool IsPhotoTabVisible
        {
            get
            {
                return _isPhotoTabVisible;
            }
            set
            {
                _isPhotoTabVisible = value;
                RaisePropertyChanged("IsPhotoTabVisible");
            }
        }


        ExtendedPicker distresscode, activity, unit, priority, inspdate, shiftpspicker;

        EntryControl enctrlRefNo, entry, enctrlFKM, enctrlFM, enctrlTM, enctrlTKM, enctrlADPText, entrlCDRText;

        DecimalEntryControl enctrlCDRLText, enctrlCDRWText, enctrlCDRHText;

        EntryControl wipicker, wspicker, wtcpicker, wcpicker, shiftwispicker, rtpicker;

        CustomEditor enctrlDesc, enctrlRemarks;

        ImageCell img;

        Grid MainGrid;

        Label lblName;

        Button btnSaveAndContinue, btnSaveAndExit, btnClear, btnCancel, btnAddImage, btnSaveAndContinue1, btnSaveAndExit1, btnClear1, btnCancel1, btnlocation;

        public FormAAddPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;
            _localDatabase = localDatabase;
            SelectedHeaderItems = new FormAHeaderResponseDTO();
            editViewModel = new EditViewModel();

        }

        public override async void Init(object initData)
        {

            try
            {
                base.Init(initData);

                // editViewModel = initData as EditViewModel;

                IsPhotoTabVisible = false;

                SelectedHeaderItems = initData as FormAHeaderResponseDTO;

                SelectedDetailItems = initData as FormADetailResponseDTO;

                MonthListItems = new ObservableCollection<DDListItems>();

                WeekListItems = new ObservableCollection<DDListItems>();

                DDDistressCodeListItems = new ObservableCollection<DDListItems>();

                DDLocationListItems = new ObservableCollection<DDListItems>();

                DDActivityCodeListItems = new ObservableCollection<DDListItems>();

                DDUnitListItems = new ObservableCollection<DDListItems>();

                DDPriorityListItems = new ObservableCollection<DDListItems>();

                DDShiftPSListItems = new ObservableCollection<DDListItems>();

                SaveItems = new ObservableCollection<RmFormaImageDtl>();

                DetailImageList = new ObservableCollection<FormAImageListRequestDTO>();

                fileDataList = new FileData();


                ImageList = new ObservableCollection<ImageUploadFormATABDTO>();

                MainGrid = CurrentPage.FindByName<Grid>("MainGrid");

                lblName = CurrentPage.FindByName<Label>("txtName");

                SiteRef_multiSelect = new List<String>(); // { "Left Hand Side", "Right Hand Side", "Center Line", "Critical Intervention Lev", "High Priority", "Normal Priority" };

                UploadFileName = "Nil";

                imagesrc = CurrentPage.FindByName<Image>("imagesrc");

                enctrlRefNo = CurrentPage.FindByName<EntryControl>("enctrlRefNo");

                enctrlFKM = CurrentPage.FindByName<EntryControl>("enctrlFKM");

                enctrlFM = CurrentPage.FindByName<EntryControl>("enctrlFM");

                enctrlTM = CurrentPage.FindByName<EntryControl>("enctrlTM");

                enctrlTKM = CurrentPage.FindByName<EntryControl>("enctrlTKM");

                enctrlDesc = CurrentPage.FindByName<CustomEditor>("enctrlDesc");

                enctrlCDRLText = CurrentPage.FindByName<DecimalEntryControl>("enctrlCDRLText");

                enctrlCDRWText = CurrentPage.FindByName<DecimalEntryControl>("enctrlCDRWText");

                enctrlCDRHText = CurrentPage.FindByName<DecimalEntryControl>("enctrlCDRHText");

                enctrlADPText = CurrentPage.FindByName<EntryControl>("enctrlADPText");

                entrlCDRText = CurrentPage.FindByName<EntryControl>("entrlCDRText");

                enctrlRemarks = CurrentPage.FindByName<CustomEditor>("enctrlRemarks");

                btnSaveAndContinue = CurrentPage.FindByName<Button>("btnSaveAndContinue");

                btnSaveAndExit = CurrentPage.FindByName<Button>("btnSaveAndExit");

                btnClear = CurrentPage.FindByName<Button>("btnClear");

                btnCancel = CurrentPage.FindByName<Button>("btnCancel");

                btnSaveAndContinue1 = CurrentPage.FindByName<Button>("btnSaveAndContinue1");

                btnSaveAndExit1 = CurrentPage.FindByName<Button>("btnSaveAndExit1");

                btnClear1 = CurrentPage.FindByName<Button>("btnClear1");

                btnCancel1 = CurrentPage.FindByName<Button>("btnCancel1");

                btnlocation = CurrentPage.FindByName<Button>("locationpicker");


                imageListView = CurrentPage.FindByName<FlowListView>("listItems");

                //pickerlist
                distresscode = CurrentPage.FindByName<ExtendedPicker>("distresscodepicker");

                //location = CurrentPage.FindByName<ExtendedPicker>("locationpicker");

                activity = CurrentPage.FindByName<ExtendedPicker>("activitycodepicker");

                unit = CurrentPage.FindByName<ExtendedPicker>("unitpicker");

                priority = CurrentPage.FindByName<ExtendedPicker>("prioritypicker");

                wipicker = CurrentPage.FindByName<EntryControl>("wipicker");

                wspicker = CurrentPage.FindByName<EntryControl>("wspicker");

                wtcpicker = CurrentPage.FindByName<EntryControl>("wtcpicker");

                wcpicker = CurrentPage.FindByName<EntryControl>("wcpicker");

                shiftpspicker = CurrentPage.FindByName<ExtendedPicker>("shiftpspicker");

                shiftwispicker = CurrentPage.FindByName<EntryControl>("shiftwispicker");

                rtpicker = CurrentPage.FindByName<EntryControl>("rtpicker");

                btnAddImage = CurrentPage.FindByName<Button>("btnAddImage");

                //GetHeaderNo = editViewModel.HdrFahPkRefNo;
                AppType = App.DetailType;

                //img = new ImageCell();

                if (AppType == "Add")
                {
                    App.DetailType = "AddDetail";
                    App.HeaderCode = 0;
                    DropdownControl(AppType);

                    isControl("Add", true);

                    return;
                }
                else if (AppType == "Edit")
                {
                    App.DetailType = "AddDetail";

                    int GetDetailID = App.HeaderCode;

                    await GetMyFormADetailEditAndViewReports(GetDetailID);

                    DropdownControl("Edit");

                    isControl("Edit", false);

                    string strDetailCode = Convert.ToInt32(App.HeaderCode).ToString();

                    GetImageList(strDetailCode);
                    //App.ReturnType="";

                    return;
                }
                else if (AppType == "View")
                {
                    App.DetailType = "AddDetail";
                    //App.ReturnType = "View";

                    int GetDetailID = App.HeaderCode;

                    await GetMyFormADetailEditAndViewReports(GetDetailID);

                    DropdownControl("View");

                    isControl("View", false);

                    string strDetailCode = Convert.ToInt32(App.HeaderCode).ToString();

                    GetImageList(strDetailCode);

                    return;

                }
                

            }
            catch
            { }




        }

        Image image { get; set; }

        Label label { get; set; }



        private async void GetImageList(string AssetID)
        {
            try
            {



                if (CrossConnectivity.Current.IsConnected)
                {

                    try
                    {

                        //ImageCell img = new ImageC();

                        using (var client = new HttpClient())

                        using (var formData = new MultipartFormDataContent())
                        {
                            formData.Add(new StringContent(App.HeaderCode.ToString()), "AssetID");
                            try
                            {

                                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", App.AuthToken);

                                var res1 = await client.PostAsync(AppConst.ImageApiGetBaseAddress, formData);

                                if (res1.IsSuccessStatusCode)
                                {

                                    var content = res1.Content.ReadAsStringAsync().Result;

                                    List<FormAImageListRequestDTO> userresult = JsonConvert.DeserializeObject<List<FormAImageListRequestDTO>>(content);

                                    // var count = userresult.Count;

                                    DetailImageList = new ObservableCollection<FormAImageListRequestDTO>(userresult);


                                    int i = 0;

                                    try
                                    {
                                        MainGrid.Children.Clear();
                                        foreach (var listdata in DetailImageList)
                                        {
                                            listdata.SNO = i + 1;
                                            string Path = listdata.ImageFilename;

                                            try
                                            {
                                                ActivityIndicator indicator = new ActivityIndicator() { BindingContext = image };
                                                indicator.SetBinding(ActivityIndicator.IsRunningProperty, new Binding("IsLoading", source: image));
                                                indicator.SetBinding(ActivityIndicator.IsVisibleProperty, new Binding("IsLoading", source: image));
                                                image = new Image
                                                {
                                                    Source = ImageSource.FromUri(new Uri(AppConst.ImageApiGetDownloadAddress + Path))

                                                };

                                                label = new Label
                                                {
                                                    Text = listdata.ImageFilenameSys,
                                                    HorizontalTextAlignment = TextAlignment.Center,
                                                    Margin = -10
                                                };

                                                MainGrid.RowDefinitions.Add(new RowDefinition { Height = 100 });

                                                Grid.SetColumn(indicator, i);
                                                Grid.SetColumn(image, i);
                                                Grid.SetRow(label, 1);
                                                Grid.SetColumn(label, i);

                                                MainGrid.Children.Add(indicator);
                                                MainGrid.Children.Add(image);
                                                MainGrid.Children.Add(label);

                                                i = i + 1;
                                            }
                                            catch (Exception ex)
                                            {

                                            }




                                            //}

                                            //for (var i = 0; i < DetailImageList.Count; i++)
                                            //{
                                            //    MainGrid.RowDefinitions.Add(new RowDefinition { Height = 100 });

                                            //    for (var j = 0; j < DetailImageList.Count; j++)
                                            //    {
                                            //        image = new Image
                                            //        {
                                            //            Source = ImageSource.FromUri(new Uri(AppConst.ImageApiGetDownloadAddress + Path))

                                            //        };

                                            //        label = new Label
                                            //        {
                                            //            Text = listdata.ImageFilenameSys,
                                            //            HorizontalTextAlignment = TextAlignment.Center,
                                            //            Margin = -10

                                            //            //BackgroundColor=Color.Red

                                            //        };


                                            //        Grid.SetRow(image, i);
                                            //        Grid.SetColumn(image, j);


                                            //        Grid.SetRow(label, i + 1);

                                            //        Grid.SetColumn(label, j);


                                            //        MainGrid.Children.Add(image);

                                            //        MainGrid.Children.Add(label);

                                            //    }

                                            //    //lblName.Text += "Test";


                                            //}




                                            //img.ImageSource = Xamarin.Forms.ImageSource.FromUri(new Uri(AppConst.ImageApiGetDownloadAddress + Path));


                                            // () => new MemoryStream(imagearray.FileContent)); 
                                        }
                                    }
                                    catch (Exception ex)
                                    { }
                                                                                                       
                                        //foreach (FormAImageListRequestDTO imagearray in DetailImageList)
                                        //{





                                        //    //string base64String = imagearray.FileContent;

                                        //    //Stream stream = new MemoryStream(imagearray.FileContent);

                                        //    //MemoryStream ms = new MemoryStream(byteArrayIn);

                                        //    //Image returnImage = Image.FromStream(ms);

                                        //    //ImageSource dtlImage = Xamarin.Forms.ImageSource.FromStream(
                                        //    //() => new MemoryStream(imagearray.FileContent));

                                        //    //  img.ImageSource = Xamarin.Forms.ImageSource.FromStream(
                                        //    //() => new MemoryStream(imagearray.FileContent));

                                        //    //image.Source = ImageSource.FromStream(stream);

                                        //    // img.Source= ImageSource.FromStream(base64String);



                                        //}



                                    }
                            }
                            catch { }

                        }
                    
                    }
                    catch (Exception ex) { }



                }
            }
            catch { }

        }


        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            ViewType = editViewModel.Type == "View" ? false : true;

            if (editViewModel.Type == "Add")
            {
                DropdownControl("Add");
                isControl("Add", true);

            }
            if (editViewModel.Type == "Edit")
            {
                int GetDetailID = editViewModel.HdrFahPkRefNo;
                await GetMyFormADetailEditAndViewReports(GetDetailID);
                DropdownControl("Edit");

                isControl("Edit", true);

                return;
            }

            else if (editViewModel.Type == "View")
            {
                DropdownControl("View");

                isControl("Edit", false);

                return;
            }


            string strDetailCode = Convert.ToInt32(App.HeaderCode).ToString();

            GetImageList(strDetailCode);


            MessagingCenter.Unsubscribe<object,string>(this, "Uploaded");

            MessagingCenter.Subscribe<object, string>(this, "Uploaded", (obj, s) =>
            {
                GetImageList(strDetailCode);
            });

        }







        public async void ControlInit()
        {
            try
            {
                //btnSaveAndContinue = CurrentPage.FindByName<Button>("btnSaveAndContinue");

                //btnSaveAndExit = CurrentPage.FindByName<Button>("btnSaveAndExit");

                //btnClear = CurrentPage.FindByName<Button>("btnClear");

                //btnCancel = CurrentPage.FindByName<Button>("btnCancel");

                //enctrlRefNo = CurrentPage.FindByName<EntryControl>("enctrlRefNo");

                ////entry = currentpage.findbyname<entrycontrol>("entry");



                //enctrlFKM = CurrentPage.FindByName<EntryControl>("enctrlFKM");

                //enctrlFM = CurrentPage.FindByName<EntryControl>("enctrlFM");

                //enctrlTM = CurrentPage.FindByName<EntryControl>("enctrlTM");

                //enctrlTKM = CurrentPage.FindByName<EntryControl>("enctrlTKM");

                //enctrlDesc = CurrentPage.FindByName<CustomEditor>("enctrlDesc");

                //enctrlCDRLText = CurrentPage.FindByName<DecimalEntryControl>("enctrlCDRLText");

                //enctrlCDRWText = CurrentPage.FindByName<DecimalEntryControl>("enctrlCDRWText");

                //enctrlCDRHText = CurrentPage.FindByName<DecimalEntryControl>("enctrlCDRHText");

                //enctrlADPText = CurrentPage.FindByName<EntryControl>("enctrlADPText");

                //entrlCDRText = CurrentPage.FindByName<EntryControl>("entrlCDRText");

                //enctrlRemarks = CurrentPage.FindByName<CustomEditor>("enctrlRemarks");
            }
            catch
            {

            }

        }


        public void isControl(string Type, bool ibVal)
        {
            try
            {
                enctrlRefNo = CurrentPage.FindByName<EntryControl>("enctrlRefNo");
                enctrlFKM = CurrentPage.FindByName<EntryControl>("enctrlFKM");
                enctrlFM = CurrentPage.FindByName<EntryControl>("enctrlFM");
                enctrlTM = CurrentPage.FindByName<EntryControl>("enctrlTM");
                enctrlTKM = CurrentPage.FindByName<EntryControl>("enctrlTKM");
                enctrlDesc = CurrentPage.FindByName<CustomEditor>("enctrlDesc");
                enctrlCDRLText = CurrentPage.FindByName<DecimalEntryControl>("enctrlCDRLText");
                enctrlCDRWText = CurrentPage.FindByName<DecimalEntryControl>("enctrlCDRWText");
                enctrlCDRHText = CurrentPage.FindByName<DecimalEntryControl>("enctrlCDRHText");
                enctrlADPText = CurrentPage.FindByName<EntryControl>("enctrlADPText");
                entrlCDRText = CurrentPage.FindByName<EntryControl>("entrlCDRText");
                enctrlRemarks = CurrentPage.FindByName<CustomEditor>("enctrlRemarks");
                btnSaveAndContinue = CurrentPage.FindByName<Button>("btnSaveAndContinue");
                btnSaveAndExit = CurrentPage.FindByName<Button>("btnSaveAndExit");
                btnClear = CurrentPage.FindByName<Button>("btnClear");
                btnCancel = CurrentPage.FindByName<Button>("btnCancel");
                btnSaveAndContinue1 = CurrentPage.FindByName<Button>("btnSaveAndContinue1");
                btnSaveAndExit1 = CurrentPage.FindByName<Button>("btnSaveAndExit1");
                btnClear1 = CurrentPage.FindByName<Button>("btnClear1");
                btnCancel1 = CurrentPage.FindByName<Button>("btnCancel1");
                //pickerlist
                distresscode = CurrentPage.FindByName<ExtendedPicker>("distresscodepicker");
                //location = CurrentPage.FindByName<ExtendedPicker>("locationpicker");
                activity = CurrentPage.FindByName<ExtendedPicker>("activitycodepicker");
                unit = CurrentPage.FindByName<ExtendedPicker>("unitpicker");
                priority = CurrentPage.FindByName<ExtendedPicker>("prioritypicker");
                wipicker = CurrentPage.FindByName<EntryControl>("wipicker");
                wspicker = CurrentPage.FindByName<EntryControl>("wspicker");
                wtcpicker = CurrentPage.FindByName<EntryControl>("wtcpicker");
                wcpicker = CurrentPage.FindByName<EntryControl>("wcpicker");
                shiftpspicker = CurrentPage.FindByName<ExtendedPicker>("shiftpspicker");
                shiftwispicker = CurrentPage.FindByName<EntryControl>("shiftwispicker");
                rtpicker = CurrentPage.FindByName<EntryControl>("rtpicker");
                btnAddImage = CurrentPage.FindByName<Button>("btnAddImage");

                if (Type == "Add")
                {


                    //enctrlRefNo.IsEnabled = ibVal;
                    // entry.IsEnabled = ibVal;

                    //enctrlFKM.IsEnabled = ibVal;


                    //enctrlFM.IsEnabled = ibVal;


                    //enctrlTM.IsEnabled = ibVal;


                    //enctrlTKM.IsEnabled = ibVal;


                    //enctrlDesc.IsEnabled = ibVal;


                    //enctrlCDRLText.IsEnabled = ibVal;


                    //enctrlCDRWText.IsEnabled = ibVal;


                    //enctrlCDRHText.IsEnabled = ibVal;


                    //enctrlADPText.IsEnabled = ibVal;


                    //entrlCDRText.IsEnabled = ibVal;


                    //enctrlRemarks.IsEnabled = ibVal;

                    NotToEdit = false;
                    btnSaveAndContinue.IsVisible = ibVal;


                    btnSaveAndExit.IsEnabled = ibVal;

                    btnSaveAndExit.Text = "Save and Exit";


                    btnClear.IsVisible = ibVal;
                    //nClear.IsEnabled = ibVal;


                    btnCancel.IsEnabled = ibVal;

                    btnSaveAndContinue1.IsVisible = ibVal;
                    btnSaveAndExit1.IsEnabled = ibVal;
                    btnSaveAndExit1.Text = "Save and Exit";
                    btnClear1.IsVisible = ibVal;
                    btnCancel1.IsEnabled = ibVal;


                    //distresscode.IsEnabled = ibVal;

                    //activity.IsEnabled = ibVal;

                    //unit.IsEnabled = ibVal;

                    //priority.IsEnabled = ibVal;

                    //wipicker.IsEnabled = ibVal;

                    //wspicker.IsEnabled = ibVal;

                    //wcpicker.IsEnabled = ibVal;

                    //shiftpspicker.IsEnabled = ibVal;

                    //shiftwispicker.IsEnabled = ibVal;

                    //rtpicker.IsEnabled = ibVal;

                    btnAddImage.IsEnabled = ibVal;

                }
                else if (Type == "Edit")
                {
                    //enctrlRefNo.IsEnabled = ibVal;
                    //entry.IsEnabled = ibVal;
                    //enctrlFKM.IsEnabled = true;
                    //enctrlFM.IsEnabled = true;
                    //enctrlTM.IsEnabled = true;
                    //enctrlDesc.IsEnabled = true;
                    //enctrlCDRLText.IsEnabled = true;
                    //enctrlCDRWText.IsEnabled = true;
                    //enctrlCDRHText.IsEnabled = true;
                    //enctrlADPText.IsEnabled = true;
                    //entrlCDRText.IsEnabled = true;
                    //enctrlRemarks.IsEnabled = true;

                    NotToEdit = false;
                    btnSaveAndContinue.IsVisible = ibVal;
                    btnSaveAndExit.Text = "Update and Exit";
                    btnSaveAndExit.IsEnabled = true;
                    btnClear.IsVisible = ibVal;
                    btnCancel.IsEnabled = true;

                    btnSaveAndContinue1.IsVisible = ibVal;
                    btnSaveAndExit1.Text = "Update and Exit";
                    btnSaveAndExit1.IsEnabled = true;
                    btnClear1.IsVisible = ibVal;
                    btnCancel1.IsEnabled = true;
                    //distresscode.IsEnabled = true;
                    //activity.IsEnabled = true;
                    //unit.IsEnabled = true;
                    //priority.IsEnabled = true;

                    //wipicker.IsEnabled = true;

                    //wspicker.IsEnabled = true;

                    //wcpicker.IsEnabled = true;

                    //shiftpspicker.IsEnabled = true;

                    //shiftwispicker.IsEnabled = true;

                    //rtpicker.IsEnabled = true;

                    btnAddImage.IsEnabled = true;



                }
                else if (Type == "View")
                {
                    //enctrlRefNo.IsEnabled = ibVal;
                    // entry.IsEnabled = ibVal;
                    //enctrlFKM.IsEnabled = ibVal;
                    //enctrlFM.IsEnabled = ibVal;
                    //enctrlTM.IsEnabled = ibVal;
                    //enctrlDesc.IsEnabled = ibVal;
                    //enctrlCDRLText.IsEnabled = ibVal;
                    //enctrlCDRWText.IsEnabled = ibVal;
                    //enctrlCDRHText.IsEnabled = ibVal;
                    //enctrlADPText.IsEnabled = ibVal;
                    //entrlCDRText.IsEnabled = ibVal;
                    //enctrlRemarks.IsEnabled = ibVal;

                    btnSaveAndContinue.IsVisible = ibVal;
                    btnSaveAndExit.IsVisible = ibVal;
                    btnClear.IsVisible = ibVal;
                    btnCancel.IsEnabled = true;

                    btnSaveAndContinue1.IsVisible = ibVal;
                    btnSaveAndExit1.IsVisible = ibVal;
                    btnClear1.IsVisible = ibVal;
                    btnCancel1.IsEnabled = true;

                    NotToEdit = true;

                    //distresscode.IsEnabled = ibVal;
                    //activity.IsEnabled = ibVal;
                    //unit.IsEnabled = ibVal;
                    //priority.IsEnabled = ibVal;
                    //wipicker.IsEnabled = ibVal;

                    //wspicker.IsEnabled = ibVal;

                    //wcpicker.IsEnabled = ibVal;

                    //shiftpspicker.IsEnabled = ibVal;

                    //shiftwispicker.IsEnabled = ibVal;

                    //rtpicker.IsEnabled = ibVal;

                    btnAddImage.IsEnabled = ibVal;

                }
            }
            catch { }

        }


        public async void DropdownControl(string Type)
        {
            try
            {



                await GetWeekddListDetails();


                await GetDistressCodeDetails(App.AssetGroupSelection);

                await GetddListDetails("Site Ref");

                await GetActivtyCodeDetails("ACT-" + App.AssetGroupSelection);

                await GetddListDetails("Unit");

                await GetddListDetails("Priority");

                if (AppType == "Add")
                {
                    App.IReferenceNo = SelectedHeaderItems.Id;

                    GetHeaderNo = SelectedHeaderItems.No;

                    int? iobjData = await GetLastRecordReferenceNumber(GetHeaderNo);

                    GetLastReferenceNo = (int)iobjData;

                    SelectedRefNo = SelectedHeaderItems.Id.ToString() + "/" + GetLastReferenceNo;
                }

                int selectedMonth = Convert.ToInt32(SelectedHeaderItems.Month);
                int selectedYear = Convert.ToInt32(SelectedHeaderItems.Year);

                var minDate = new DateTime(selectedYear, selectedMonth, 1);
                var maxDate = minDate.AddMonths(1).AddDays(-1);
                MinimumDate = minDate;
                MaximumDate = maxDate;
                SelectedDate = null;

                distresscode = CurrentPage.FindByName<ExtendedPicker>("distresscodepicker");

                distresscode.ItemsSource = DDDistressCodeListItems.Select((DDListItems arg) => arg.Text).ToList();

                distresscode.SelectedIndexChanged += (s, e) =>
                {
                    if (distresscode.SelectedIndex != -1)
                    {
                        SelectedDistressCode = DDDistressCodeListItems[distresscode.SelectedIndex].Value.ToString();
                        DescriptionText = DDDistressCodeListItems[distresscode.SelectedIndex].Text.ToString().Split('-')[1].ToString();
                    }
                };

                //location = CurrentPage.FindByName<ExtendedPicker>("locationpicker");

                //location.ItemsSource = DDLocationListItems.Select((DDListItems arg) => arg.Text).ToList();

                //location.SelectedIndexChanged += (s, e) =>
                //{
                //    if (location.SelectedIndex != -1)
                //        SelectedLocation = DDLocationListItems[location.SelectedIndex].Value.ToString();
                //};


                //SelectedLocationList = DDLocationListItems.Where(x => x.Selected == true).ToList();


                MessagingCenter.Subscribe<object, ObservableCollection<DDListItems>>(this, "IsChecked", (obj, multiSelectList) =>
                {
                    var result = multiSelectList.Where(w => w.IsChecked == true).ToList();

                    string s = "";

                    int index = 0;
                    foreach (var model in result)
                    {
                        s = s + model.Value;
                        if (index < result.Count - 1)
                        {
                            s = s + ",";
                        }
                        index++;
                    }

                    SelectedLocationList = result.Select(x => x.Value).ToList();

                    if (SelectedLocationList.Count > 0)
                        btnlocation.Text = s;
                    else
                        btnlocation.Text = "Select Location";
                });


                activity = CurrentPage.FindByName<ExtendedPicker>("activitycodepicker");

                //activity.ItemsSource = DDActivityCodeListItems.Select((DDListItems arg) => arg.Text).ToList();

                activity.ItemsSource = DDActivityCodeListItems.Select((DDListItems arg) => arg.Value + "-" + arg.Text).ToList();

                activity.SelectedIndexChanged += (s, e) =>
                {
                    if (activity.SelectedIndex != -1)
                    {
                        SelectedActivityCode = DDActivityCodeListItems[activity.SelectedIndex].Value.ToString();
                    }
                };

                unit = CurrentPage.FindByName<ExtendedPicker>("unitpicker");

                unit.ItemsSource = DDUnitListItems.Select((DDListItems arg) => arg.Text).ToList();

                unit.SelectedIndexChanged += (s, e) =>
                {
                    if (unit.SelectedIndex != -1)
                        SelectedUnit = DDUnitListItems[unit.SelectedIndex].Value.ToString();
                };

                priority = CurrentPage.FindByName<ExtendedPicker>("prioritypicker");

                priority.ItemsSource = DDPriorityListItems.Select((DDListItems arg) => arg.Value).ToList();

                priority.SelectedIndexChanged += (s, e) =>
                {
                    if (priority.SelectedIndex != -1)
                        SelectedPriority = DDPriorityListItems[priority.SelectedIndex].Value.ToString();
                };

                //Old based on Senthil 24122020
                //wi = CurrentPage.FindByName<ExtendedPicker>("wipicker");

                //wi.ItemsSource = WeekListItems.Select((DDListItems arg) => arg.Text).ToList();

                //wi.SelectedIndexChanged += (s, e) =>
                //{
                //    if (wi.SelectedIndex != -1)
                //        SelectedWI = Convert.ToInt32(WeekListItems[wi.SelectedIndex].Value.ToString());
                //};

                //ws = CurrentPage.FindByName<ExtendedPicker>("wspicker");

                //ws.ItemsSource = WeekListItems.Select((DDListItems arg) => arg.Text).ToList();

                //ws.SelectedIndexChanged += (s, e) =>
                //{
                //    if (ws.SelectedIndex != -1)
                //        SelectedWS = Convert.ToInt32(WeekListItems[ws.SelectedIndex].Value.ToString());
                //};

                //wtc = CurrentPage.FindByName<ExtendedPicker>("wtcpicker");

                //wtc.ItemsSource = WeekListItems.Select((DDListItems arg) => arg.Text).ToList();

                //wtc.SelectedIndexChanged += (s, e) =>
                //{
                //    if (wtc.SelectedIndex != -1)
                //        SelectedWTC = Convert.ToInt32(WeekListItems[wtc.SelectedIndex].Value.ToString());
                //};

                //wc = CurrentPage.FindByName<ExtendedPicker>("wcpicker");

                //wc.ItemsSource = WeekListItems.Select((DDListItems arg) => arg.Text).ToList();

                //wc.SelectedIndexChanged += (s, e) =>
                //{
                //    if (wc.SelectedIndex != -1)
                //        SelectedWC = Convert.ToInt32(WeekListItems[wc.SelectedIndex].Value.ToString());
                //};

                shiftpspicker = CurrentPage.FindByName<ExtendedPicker>("shiftpspicker");

                shiftpspicker.ItemsSource = DDShiftPSListItems.Select((DDListItems arg) => arg.Value).ToList();

                shiftpspicker.SelectedIndexChanged += (s, e) =>
                {
                    if (shiftpspicker.SelectedIndex != -1)
                        SelectedShiftPS = DDShiftPSListItems[shiftpspicker.SelectedIndex].Value.ToString();
                };

                //shiftwis = CurrentPage.FindByName<ExtendedPicker>("shiftwispicker");

                //shiftwis.ItemsSource = WeekListItems.Select((DDListItems arg) => arg.Text).ToList();

                //shiftwis.SelectedIndexChanged += (s, e) =>
                //{
                //    if (shiftwis.SelectedIndex != -1)
                //        SelectedShiftWI = Convert.ToInt32(WeekListItems[shiftwis.SelectedIndex].Value.ToString());
                //};

                //rt = CurrentPage.FindByName<ExtendedPicker>("rtpicker");

                //rt.ItemsSource = WeekListItems.Select((DDListItems arg) => arg.Text).ToList();

                //rt.SelectedIndexChanged += (s, e) =>
                //{
                //    if (rt.SelectedIndex != -1)
                //        SelectedRT = Convert.ToInt32(WeekListItems[rt.SelectedIndex].Value.ToString());
                //};


                if (Type == "Edit" || Type == "View")
                {
                    //
                    iGetHeaderNo = SelecteddtlEditItem.HeaderNo;

                    SelectedRefNo = App.IReferenceNo + "/" + SelecteddtlEditItem.Srno;

                    int DDDistressIndex = DDDistressCodeListItems.ToList().FindIndex(a => (a.Text == SelecteddtlEditItem.DefCode)||(a.Value == SelecteddtlEditItem.DefCode));

                    //if (DDDistressIndex == -1) { DDDistressIndex = 1; }

                    distresscode.SelectedIndex = DDDistressIndex;

                    SelectedDistressCode = DDDistressCodeListItems[distresscode.SelectedIndex].Text;

                    //SelecteddtlEditItem.SiteRef_multiSelect.Add(strentry);

                    //////LocationIndex
                    //int LocationIndex = DDLocationListItems.ToList().FindIndex(a => a.Value == SelecteddtlEditItem.SiteRef);
                    ////if (LocationIndex == -1) { LocationIndex = 1; }

                    //location.SelectedIndex = LocationIndex;

                    //SelectedLocation = DDLocationListItems[location.SelectedIndex].Text;


                    if (SelecteddtlEditItem.SiteRef_multiSelect.Count > 0)
                    {
                        string s = "";

                        int index = 0;
                        SelectedLocationList = SelecteddtlEditItem.SiteRef_multiSelect;
                        foreach (var model in SelectedLocationList)
                        {
                            s = s + model;
                            if (index < SelectedLocationList.Count - 1)
                            {
                                s = s + ",";
                            }
                            index++;
                            DDLocationListItems.FirstOrDefault(x => x.Value == model).IsChecked = true;
                        }
                        btnlocation.Text = s;

                    }
                    else
                    {
                        btnlocation.Text = "Select Location";
                    }
                    
                    int ActivityIndex = DDActivityCodeListItems.ToList().FindIndex(a => a.Value == SelecteddtlEditItem.ActCode?.ToString());
                    //if (ActivityIndex == -1) { ActivityIndex = 1; }
                    activity.SelectedIndex = ActivityIndex;

                    //SelectedActivityCode = DDLocationListItems[activity.SelectedIndex].Text;

                    //unit Type
                    int unitindex = DDUnitListItems.ToList().FindIndex(a => a.Value == SelecteddtlEditItem.Unit);
                    //if (unitindex == -1) { unitindex = 1; }
                    unit.SelectedIndex = unitindex;

                    //SelectedUnit = DDUnitListItems[unit.SelectedIndex].Value.ToString();

                    int Priindex = DDPriorityListItems.ToList().FindIndex(a => a.Value == SelecteddtlEditItem.Priority);
                    //if (Priindex == -1) { Priindex = 1; }
                    priority.SelectedIndex = Priindex;

                    //SelectedPriority = DDPriorityListItems[priority.SelectedIndex].Value.ToString();

                    //int wiindex = WeekListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == SelecteddtlEditItem.Wi);
                    //if (wiindex == -1) { wiindex = 1; }
                    SelectedWI = Convert.ToInt32(SelecteddtlEditItem.Wi);

                    //SelectedWI = Convert.ToInt32(WeekListItems[wi.SelectedIndex].Value.ToString());

                    //int wsindex = WeekListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == SelecteddtlEditItem.Ws);
                    //if (wsindex == -1) { wsindex = 1; }
                    //ws.SelectedIndex = wsindex;

                    SelectedWS = Convert.ToInt32(SelecteddtlEditItem.Ws);

                    //SelectedWS = Convert.ToInt32(WeekListItems[ws.SelectedIndex].Value.ToString());

                    //int wtcindex = WeekListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == SelecteddtlEditItem.Wtc);
                    //if (wtcindex == -1) { wtcindex = 1; }
                    //wtc.SelectedIndex = wtcindex;
                    SelectedWTC = Convert.ToInt32(SelecteddtlEditItem.Wtc);

                    //SelectedWTC = Convert.ToInt32(WeekListItems[wtc.SelectedIndex].Value.ToString());

                    //int wcindex = WeekListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == SelecteddtlEditItem.Wc);
                    //if (wcindex == -1) { wcindex = 1; }
                    //wc.SelectedIndex = wcindex;

                    SelectedWC = Convert.ToInt32(SelecteddtlEditItem.Wc);

                    //SelectedWC = Convert.ToInt32(WeekListItems[wc.SelectedIndex].Value.ToString());

                    int shiftpsindex = DDShiftPSListItems.ToList().FindIndex(a =>a.Value == SelecteddtlEditItem.SftPs);
                    //if (shiftpsindex == -1) { shiftpsindex = 1; }
                    shiftpspicker.SelectedIndex = shiftpsindex;

                    //SelectedShiftPs = Convert.ToInt32(SelecteddtlEditItem.SftPs);

                    //SelectedShiftPs = Convert.ToInt32(WeekListItems[shiftps.SelectedIndex].Value.ToString());

                    //int shiftwisindex = WeekListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == SelecteddtlEditItem.SftWis);
                    //if (shiftwisindex == -1) { shiftwisindex = 1; }
                    //shiftwis.SelectedIndex = shiftwisindex;

                    SelectedShiftWI = Convert.ToInt32(SelecteddtlEditItem.SftWis);
                    //SelectedShiftWI = Convert.ToInt32(WeekListItems[shiftwis.SelectedIndex].Value.ToString());

                    //int rtindex = WeekListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == SelecteddtlEditItem.Rt);
                    //if (rtindex == -1) { rtindex = 1; }
                    //rt.SelectedIndex = rtindex;

                    SelectedRT = Convert.ToInt32(SelecteddtlEditItem.Rt);
                    //SelectedRT = Convert.ToInt32(WeekListItems[rt.SelectedIndex].Value.ToString());

                    intSrNo = SelecteddtlEditItem.Srno;

                    FromKmText = SelecteddtlEditItem.FromCh;

                    FromMText = SelecteddtlEditItem.FromChDeci;

                    ToKmText = SelecteddtlEditItem.ToCh;

                    ToMText = SelecteddtlEditItem.ToChDeci;

                    SelectedActivityCode = SelecteddtlEditItem.ActCode;

                    CDRLText = SelecteddtlEditItem.Length;

                    CDRWText = SelecteddtlEditItem.Width;

                    CDRHText = SelecteddtlEditItem.Height;

                    ADPText = SelecteddtlEditItem.Adp;

                    CDRText = SelecteddtlEditItem.Cdr;

                    DescriptionText = SelecteddtlEditItem.Desc;

                    RemarksText = SelecteddtlEditItem.Remarks;

                    FormAPPText = SelecteddtlEditItem.FormhApp;

                    SelectedDate = Convert.ToDateTime(SelecteddtlEditItem.Dt);
                    FormH = SelecteddtlEditItem.FormhApp;

                }

                ControlInit();

            }
            catch (Exception ex) { }


        }


        public async Task<FormADetailsRequestDTO> GetMyFormADetailEditAndViewReports(int iDetailNo)
        {

            _userDialogs.ShowLoading("Loading");

            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    //var gridList = new FormAHeaderRequestDTO()
                    //{
                    //   RefNo = hdrRefNo
                    //};



                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(iDetailNo);

                    var response = await _restApi.GetFormADetail(iDetailNo);

                    if (response.success)
                    {
                        SelecteddtlEditItem = response.data;

                        return SelecteddtlEditItem;
                    }
                    else
                        _userDialogs.Toast(response.errorMessage);

                    return SelecteddtlEditItem;
                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");
                //IsEmpty = DetailGridListViewItems.Count == 0;
                //LstViewHeightRequest = DetailGridListViewItems.Count * 40;
                return SelecteddtlEditItem;

            }
            catch (Exception ex)
            {
                _userDialogs.Alert(ex.Message);
            }
            finally
            {
                _userDialogs.HideLoading();
            }

            return new FormADetailsRequestDTO();
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
                        if (ddtype == "Distress Code")
                        {
                            DDDistressCodeListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDDistressCodeListItems;
                        }
                        else if (ddtype == "Site Ref")
                        {
                            ObservableCollection<DDListItems> locationList = new ObservableCollection<DDListItems>();
                            foreach (var item in response.data)
                            {
                                DDListItems listItems = new DDListItems();
                                listItems = item;
                                if (item.Text == "Left Hand Side")
                                {
                                    listItems.Value = "LHS";
                                    locationList.Add(listItems);
                                }
                                if (item.Text == "Right Hand Side")
                                {
                                    listItems.Value = "RHS";
                                    locationList.Add(listItems);
                                }
                                if (item.Text == "Center Line")
                                {
                                    listItems.Value = "CL";
                                    locationList.Add(listItems);
                                }
                            }

                            DDLocationListItems = new ObservableCollection<DDListItems>(locationList);
                            return DDLocationListItems;
                        }
                        else if (ddtype == "Asset Type")
                        {
                            DDActivityCodeListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDActivityCodeListItems;
                        }
                        else if (ddtype == "Unit")
                        {
                            DDUnitListItems = new ObservableCollection<DDListItems>(response.data);
                            return DDUnitListItems;
                        }
                        else if (ddtype == "Priority")
                        {
                            ObservableCollection<DDListItems> priorityList = new ObservableCollection<DDListItems>();
                            foreach(var item in response.data)
                            {
                                DDListItems listItems = new DDListItems();
                                listItems = item;
                                if (item.Text == "Critical Intervention Lev.")
                                {
                                    listItems.Value = "CIL";
                                    priorityList.Add(listItems);
                                }
                                if (item.Text == "High Priority")
                                {
                                    listItems.Value = "1";
                                    priorityList.Add(listItems);
                                }
                                if (item.Text == "Normal Priority")
                                {
                                    listItems.Value = "2";
                                    priorityList.Add(listItems);
                                }
                            }
                            DDPriorityListItems = new ObservableCollection<DDListItems>(priorityList);
                            DDShiftPSListItems = DDPriorityListItems;
                            return DDPriorityListItems;
                        }
                        return DDLocationListItems;
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


        public async Task<ObservableCollection<DDListItems>> GetDistressCodeDetails(string ddtype)
        {
            try
            {
                //  _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    var ddlist = new RmAssetDefectCode()
                    {
                        AdcAssetGrpCode = ddtype
                    };
                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddtype);
                    var response = await _restApi.GetDistressCode(ddtype);
                    if (response.success)
                    {

                        DDDistressCodeListItems = new ObservableCollection<DDListItems>(response.data);
                        return DDDistressCodeListItems;



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


        public async Task<ObservableCollection<DDListItems>> GetActivtyCodeDetails(string ddtype)
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
                    var response = await _restApi.GetTypeCodeAndValue(ddlist);
                    if (response.success)
                    {

                        DDActivityCodeListItems = new ObservableCollection<DDListItems>(response.data);
                        return DDActivityCodeListItems;


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
                        return WeekListItems;
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

        public async Task<ObservableCollection<DDListItems>> GetWeekddListDetails()
        {
            try
            {
                //_userDialogs.ShowLoading("Loading");

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
                //_userDialogs.HideLoading();
            }
            return new ObservableCollection<DDListItems>();
        }

        public async Task<ObservableCollection<RmFormaImageDtl>> FormASaveDetails()
        {
            return new ObservableCollection<RmFormaImageDtl>();
        }

        public ICommand AddButtonCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    IsPhotoTabVisible = false;
                });
            }
        }

        public ICommand PhotoButtonCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    IsPhotoTabVisible = true;
                });
            }
        }

        public ICommand LocationSelectionCommand
        {
            get
            {
                return new Command((obj) =>
                {
                    
                    CurrentPage.Navigation.PushPopupAsync(new LocationSiteRef_PopUp(DDLocationListItems));
                });
            }
        }

        public ICommand AddImageCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    try
                    {
                        if (!Validate())
                        {
                            return;
                        }
                        //await CurrentPage.Navigation.PushAsync(new CameraPopUpPage());
                        await PopupNavigation.Instance.PushAsync(new CameraPopUpPage());
                    }
                    catch { }
                });
            }
        }


        public ICommand DeleteImageCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    try
                    {
                        var actionResult = await UserDialogs.Instance.ConfirmAsync("Are you sure want to delete?", "RAMS", "Yes", "No");
                        if (actionResult)
                        {
                            _userDialogs.ShowLoading("Loading");
                            if (CrossConnectivity.Current.IsConnected)
                            {
                                var imageID = (obj as FormAImageListRequestDTO).ImageId;
                                var response = await _restApi.DeleteImage(imageID);

                                if (response.success)
                                {

                                    await UserDialogs.Instance.AlertAsync("Image deleted successfully.", "RAMS", "0K");

                                    string strDetailCode = Convert.ToInt32(App.HeaderCode).ToString();
                                    editViewModel.Type = "Edit";


                                    GetImageList(strDetailCode);

                                }
                                else
                                    _userDialogs.Toast(response.errorMessage);
                            }
                            else
                                UserDialogs.Instance.Alert("Please check your Internet Connection !");
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
                });
            }

        }
        public FreshAwaitCommand FormASaveCommand
        {
            get
            {
                return new FreshAwaitCommand(async (obj) =>
                {
                    try
                    {

                        if(!Validate())
                        {
                            return;
                        }
                        _userDialogs.ShowLoading("Loading");



                        if (CrossConnectivity.Current.IsConnected)
                        {
                            var savedtl = new FormADetailsRequestDTO()
                            {


                                Dt = SelectedDate?.Date.ToString("yyyy/MM/dd"), //DateTime.Now.Date.ToString("yyyy/MM/dd"),//null,//DateTime.Now.DateString(),

                                No = App.HeaderCode,

                                Srno = 1,

                                HeaderNo = GetHeaderNo,

                                //ReferenceId = GetLastReferenceNo,

                                FadRefNO = SelectedRefNo,

                                AssetId = null,

                                SiteRef = null,  // SelectedLocation,


                                FromCh = FromKmText,

                                FromChDeci = FromMText,

                                ToCh = ToKmText,

                                ToChDeci = ToMText,

                                DefCode = SelectedDistressCode,

                                ActCode = SelectedActivityCode,

                                Unit = SelectedUnit,

                                Length = CDRLText,

                                Width = CDRWText,

                                Height = CDRHText,

                                Adp = ADPText,

                                Cdr = CDRText,

                                Priority = SelectedPriority,

                                Wi = SelectedWI,

                                Ws = SelectedWS,

                                Wtc = SelectedWTC,

                                Wc = SelectedWC,

                                SftPs = SelectedShiftPS,

                                SftWis = SelectedShiftWI,

                                Rt = SelectedRT,

                                Remarks = RemarksText,

                                FormhApp = FormH,

                                ModBy = "Avows",

                                CallFrom = "Save Continue",

                                Desc = DescriptionText,

                                SubmitSts = false,

                                ActiveYn = null,

                                // SiteRef_multiSelect = new List<String>() { SelectedLocation },
                                SiteRef_multiSelect = SelectedLocationList,  //  new List<String>() { SelectedLocation },

                                CreatedDt = null //DateTime.Now.Date.ToString("MM/dd/yyyy")
                            };

                            var json = Newtonsoft.Json.JsonConvert.SerializeObject(savedtl);

                            var hdrresponse = await _restApi.SaveFormADtl(savedtl);

                            if (hdrresponse.success)
                            {

                                if (hdrresponse.data > 0)
                                {
                                    App.HeaderCode = hdrresponse.data;
                                    await UserDialogs.Instance.AlertAsync("Data saved successfully.", "RAMS", "0K");
                                    var res = hdrresponse.data;
                                    //int? iobjData = await GetLastRecordReferenceNumber(GetHeaderNo);
                                    //GetLastReferenceNo = (int)iobjData;
                                    SelectedRefNo = SelectedHeaderItems.Id.ToString() + "/" + GetLastReferenceNo;

                                    App.DetailHeaderCode = GetHeaderNo;

                                    //ClearData();  While save and continue data will not be cleared so commented the code.

                                    IsPhotoTabVisible = true;



                                    //await CoreMethods.PushPageModel<FormAAddPageModel>(hdrresponse.data);
                                }

                                //Get Last Record Number
                                //Clear all record
                                //Set reference no to reference id field



                                //await CurrentPage.Navigation.PopAsync();
                            }
                            else
                                _userDialogs.Toast(hdrresponse.errorMessage);

                        }



                        //if (savedtl.FadFahPkRefNo != null)
                        //    savedtl.FadFahPkRefNo = hdrresponse.data;

                        //var jsondtl = Newtonsoft.Json.JsonConvert.SerializeObject(savedtl);
                        //var response = await _restApi.GetFormASaveItem(savedtl);
                        //if (response.success)
                        //{
                        //    var res = response.data;
                        //    await CurrentPage.Navigation.PopAsync();
                        //}
                        //else
                        //    _userDialogs.Toast(response.errorMessage);

                        else
                            UserDialogs.Instance.Alert("Please check your Internet Connection !");


                    }
                    catch (Exception ex)
                    {
                        _userDialogs.Alert(ex.Message);
                    }
                    finally
                    {
                        obj.SetResult(true);
                        _userDialogs.HideLoading();
                    }
                });
            }
        }

        bool Validate()
        {
            if (SelectedDate == null)
            {
                _userDialogs.Alert("Please select the Date of Inspection", "Error");
                return false;

            }
            //else if (string.IsNullOrEmpty(SelectedLocation))
            //{
            //    _userDialogs.Alert("Please select the Location Site Ref", "Error");
            //    return false;
            //}
            else if (SelectedLocationList == null || SelectedLocationList.Count==0)
            {
                _userDialogs.Alert("Please select the Location Site Ref", "Error");
                return false;
            }
            else if (string.IsNullOrWhiteSpace(SelectedDistressCode))
            {
                _userDialogs.Alert("Please select the Distress Code", "Error");
                return false;
            }
            else if (!FromKmText.HasValue)
            {
                _userDialogs.Alert("Please enter the Location Chainage From Km", "Error");
                return false;
            }
            else if (!FromMText.HasValue)
            {
                _userDialogs.Alert("Please enter the Location Chainage From m", "Error");
                return false;
            }
            else if (!ToKmText.HasValue)
            {
                _userDialogs.Alert("Please enter the Location Chainage To Km", "Error");
                return false;
            }
            else if (!ToMText.HasValue)
            {
                _userDialogs.Alert("Please enter the Location Chainage To m", "Error");
                return false;
            }
            return true;
        }

        public bool isvalidateint(int? ivalue, string pValue)
        {
            bool isbvalue = false;

            if (ivalue != null && ivalue != 0)
            {
                isbvalue = true;
                UserDialogs.Instance.Alert("Please Enter " + pValue, "RAMS");

            }
            return isbvalue;
        }

        public bool isvalidate(string Value, string pValue)
        {
            bool isbvalue = false;

            if (Value != null && Value != string.Empty)
            {
                isbvalue = true;
                UserDialogs.Instance.Alert("Please Enter " + pValue, "RAMS");

            }

            return isbvalue;

            //try
            //{
            //    if (distresscode.SelectedIndex != -1)
            //    {
            //        distresscode.SelectedIndex = -1;
            //    }
            //}
            //catch (Exception ex) { }
            //if (location.SelectedIndex != -1)
            //    location.SelectedIndex = -1;
            //try
            //{
            //    if (activity.SelectedIndex != -1)
            //        activity.SelectedIndex = -1;
            //}
            //catch { }

            //if (unit.SelectedIndex != -1)
            //    unit.SelectedIndex = -1;

            //if (priority.SelectedIndex != -1)
            //    priority.SelectedIndex = -1;

            //if (wi.SelectedIndex != -1)
            //    wi.SelectedIndex = -1;

            //if (ws.SelectedIndex != -1)
            //    ws.SelectedIndex = -1;

            //if (wtc.SelectedIndex != -1)
            //    wtc.SelectedIndex = -1;

            //if (wc.SelectedIndex != -1)
            //    wc.SelectedIndex = -1;

            //if (shiftps.SelectedIndex != -1)
            //    shiftps.SelectedIndex = -1;

            //if (shiftwis.SelectedIndex != -1)
            //    shiftwis.SelectedIndex = -1;

            //if (rt.SelectedIndex != -1)
            //    rt.SelectedIndex = -1;

            ////if (inspdate.SelectedIndex != -1)
            ////    inspdate.SelectedIndex = 1;

            //FormKmText = null;

            //FormMText = null;


            //ToKmText = null;

            //ToMText = null;

            //CDRLText = null;

            //CDRHText = null;

            //CDRWText = null;

            //DescriptionText = "";

            //RemarksText = "";

            //SelectedActivityCode = "";

            //ADPText = "";

            //CDRText = "";
        }

        public FreshAwaitCommand FormASaveExitCommand
        {

            get
            {
                return new FreshAwaitCommand(async (obj) =>
                {
                    try
                    {
                    //    if (SelectedDate == null || string.IsNullOrWhiteSpace(SelectedLocation) || string.IsNullOrWhiteSpace(SelectedDistressCode) ||
                    //!FromKmText.HasValue || !FromMText.HasValue || !ToKmText.HasValue || !ToMText.HasValue)
                    //    {
                    //        await UserDialogs.Instance.AlertAsync("Please enter all data", "RAMS", "OK");
                    //        return;
                    //    }

                        if (!Validate())
                        {
                            return;
                        }
                        _userDialogs.ShowLoading("Loading");

                        if (CrossConnectivity.Current.IsConnected)
                        {
                            //if (!isvalidateint(FormKmText, "Form KM") && !isvalidateint(FormMText, "Form M") && isvalidateint(ToMText, "To M") && isvalidateint(ToKmText, "To KM") &&
                            //isvalidate(SelectedLocation, "or Select Site Location"))
                            //{



                            if (AppType == "Add")
                            {

                                var savedtl = new FormADetailsRequestDTO()
                                {
                                    No = App.HeaderCode,

                                    Dt = SelectedDate?.Date.ToString("yyyy/MM/dd"), //DateTime.Now.Date.ToString("yyyy/MM/dd"),

                                    Srno = GetLastReferenceNo,

                                    HeaderNo = GetHeaderNo,

                                    FadRefNO = SelectedRefNo,

                                    HeaderRefNo = SelectedRefNo,

                                    AssetId = null,

                                    SiteRef = null, // SelectedLocation,

                                    FromCh = FromKmText,

                                    FromChDeci = FromMText,

                                    ToCh = ToKmText,

                                    ToChDeci = ToMText,

                                    DefCode = SelectedDistressCode,

                                    ActCode = SelectedActivityCode,

                                    Unit = SelectedUnit,

                                    Length = CDRLText,

                                    Width = CDRWText,

                                    Height = CDRHText,

                                    Adp = ADPText,

                                    Cdr = CDRText,

                                    Priority = SelectedPriority,

                                    Wi = SelectedWI,

                                    Ws = SelectedWS,

                                    Wtc = SelectedWTC,

                                    Wc = SelectedWC,

                                    SftPs = SelectedShiftPS,

                                    SftWis = SelectedShiftWI,

                                    Rt = SelectedRT,

                                    Remarks = RemarksText,

                                    FormhApp = FormH,

                                    ModBy = null,

                                    Desc = DescriptionText,

                                    CallFrom = "Save Exit",

                                    SubmitSts = false,

                                    ActiveYn = null,

                                    SiteRef_multiSelect = SelectedLocationList,  // new List<String>() { SelectedLocation },

                                    CreatedDt = null//DateTime.Now.Date.ToString("MM/dd/yyyy")
                                };


                                var json = Newtonsoft.Json.JsonConvert.SerializeObject(savedtl);

                                var hdrresponse = await _restApi.SaveFormADtl(savedtl);

                                if (hdrresponse.success)
                                {


                                    if (hdrresponse.data > 0)
                                    {
                                        await UserDialogs.Instance.AlertAsync("Data Saved Successfully.", "RAMS", "0K");

                                        var res = hdrresponse.data;


                                        editViewModel.HdrFahPkRefNo = GetHeaderNo; //hdrresponse.data;

                                        App.DetailType = "AddDetail";

                                        App.ReturnType = "Add";

                                        App.DetailHeaderCode = GetHeaderNo; //hdrresponse.data;

                                        App.HeaderCode = GetHeaderNo;

                                        await CurrentPage.Navigation.PopAsync();

                                    }


                                }
                                else
                                    _userDialogs.Toast(hdrresponse.errorMessage);


                            }
                            else if (AppType == "Edit")
                            {

                                var updatedtl = new FormADetailsRequestDTO()
                                {
                                    No = App.HeaderCode,

                                    HeaderNo = iGetHeaderNo,

                                    Dt = SelectedDate?.Date.ToString("yyyy/MM/dd"), // DateTime.Now.Date.ToString("yyyy/MM/dd"),

                                    Srno = intSrNo,

                                    FadRefNO = SelectedRefNo,

                                    HeaderRefNo = SelectedRefNo,

                                    AssetId = null,

                                    SiteRef = null, // SelectedLocation,

                                    FromCh = FromKmText,

                                    FromChDeci = FromMText,

                                    ToCh = ToKmText,

                                    ToChDeci = ToMText,

                                    DefCode = SelectedDistressCode,

                                    ActCode = SelectedActivityCode,

                                    Unit = SelectedUnit,

                                    Length = CDRLText,

                                    Width = CDRWText,

                                    Height = CDRHText,

                                    Adp = ADPText,

                                    Cdr = CDRText,

                                    Priority = SelectedPriority,

                                    Wi = SelectedWI,

                                    Ws = SelectedWS,

                                    Wtc = SelectedWTC,

                                    Wc = SelectedWC,

                                    SftPs = SelectedShiftPS,

                                    SftWis = SelectedShiftWI,

                                    Rt = SelectedRT,

                                    Remarks = RemarksText,

                                    FormhApp = "FormA",

                                    ModBy = "Avows",

                                    Desc = DescriptionText,

                                    SubmitSts = false,

                                    ActiveYn = true,

                                    SiteRef_multiSelect = SelectedLocationList, // new List<String>() { SiteRef_multiSelect[1].ToString(), SiteRef_multiSelect[2].ToString() },

                                    CreatedDt = null
                                };
                                var json = Newtonsoft.Json.JsonConvert.SerializeObject(updatedtl);

                                var hdrresponse = await _restApi.SaveFormADtl(updatedtl);

                                if (hdrresponse.success)
                                {

                                    if (hdrresponse.data > 0)
                                    {
                                        await UserDialogs.Instance.AlertAsync("Data Updated Successfully.", "RAMS", "0K");

                                        var res = hdrresponse.data;

                                        //editViewModel.HdrFahPkRefNo = hdrresponse.data;

                                        App.DetailType = "AddDetail";

                                        App.ReturnType = "Edit";

                                        //App.HeaderCode = hdrresponse.data;

                                        await CurrentPage.Navigation.PopAsync();


                                    }


                                }
                                else
                                    _userDialogs.Toast(hdrresponse.errorMessage);

                            }


                        }



                    }

                    catch (Exception ex)
                    {
                        _userDialogs.Alert(ex.Message);
                    }
                    finally
                    {
                        obj.SetResult(true);
                        _userDialogs.HideLoading();
                    }
                });
            }
        }

        public void ClearData()
        {

            try
            {
                try
                {
                    if (distresscode.SelectedIndex != -1)
                    {
                        distresscode.SelectedIndex = -1;
                    }
                }
                catch (Exception ex) { }
                //if (location.SelectedIndex != -1)
                //    location.SelectedIndex = -1;
                try
                {
                    if (activity.SelectedIndex != -1)
                        activity.SelectedIndex = -1;
                }
                catch { }

                if (unit.SelectedIndex != -1)
                    unit.SelectedIndex = -1;

                if (priority.SelectedIndex != -1)
                    priority.SelectedIndex = -1;

                if (shiftpspicker.SelectedIndex != -1)
                    shiftpspicker.SelectedIndex = -1;

                foreach (var model in SelectedLocationList)
                {
                    DDLocationListItems.FirstOrDefault(x => x.Value == model).IsChecked = false;
                }
                btnlocation.Text = "Select Location";
                SelectedLocationList = null;

                SelectedWI = null;
                SelectedWS = null;
                SelectedWTC = null;
                SelectedWC = null;
                SelectedShiftPs = null;
                SelectedShiftWI = null;
                SelectedRT = null;

                //if (wi.SelectedIndex != -1)
                //    wi.SelectedIndex = -1;

                //if (ws.SelectedIndex != -1)
                //    ws.SelectedIndex = -1;

                //if (wtc.SelectedIndex != -1)
                //    wtc.SelectedIndex = -1;

                //if (wc.SelectedIndex != -1)
                //    wc.SelectedIndex = -1;

                //if (shiftps.SelectedIndex != -1)
                //    shiftps.SelectedIndex = -1;

                //if (shiftwis.SelectedIndex != -1)
                //    shiftwis.SelectedIndex = -1;

                //if (rt.SelectedIndex != -1)
                //    rt.SelectedIndex = -1;

                //if (inspdate.SelectedIndex != -1)
                //    inspdate.SelectedIndex = 1;

                FromKmText = null;

                FromMText = null;


                ToKmText = null;

                ToMText = null;

                CDRLText = null;

                CDRHText = null;

                CDRWText = null;

                DescriptionText = "";

                RemarksText = "";

                SelectedActivityCode = "";

                ADPText = "";

                CDRText = "";

                SelectedDate = null;

                SelectedDistressCode = null;
            }

            catch (Exception ex) { }
        }



        public ICommand ClearFormADetailsCommand
        {

            get
            {
                return new FreshCommand(async (obj) =>
                {
                    //if (obj.ToString() == "Clear")
                    //{
                    //    SelectedPriority.SelectedIndex = -1;
                    //    //SelectedRoadCode = string.Empty;
                    //    rmucode.SelectedIndex = -1;
                    //    //SelectedRMU = string.Empty;
                    //    assetype.SelectedIndex = -1;
                    //    //SelectedAssetType = string.Empty;
                    //    monthpick.SelectedIndex = -1;
                    //    //SelectedMonth = string.Empty;
                    //}
                    //if (advanceFrame.IsVisible)
                    //{
                    //    advanceFrame.IsVisible = false;
                    //}
                    //else
                    //{
                    //    advanceFrame.IsVisible = true;
                    //}


                    // SelectedRefNo

                    await UserDialogs.Instance.AlertAsync("Do you want to clear Data?.", "RAMS", "OK");

                    try
                    {
                        ClearData();
                    }

                    catch { }








                });
            }


        }


        private void Picker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = inspdate.SelectedItem as ObservableCollection<object>;

            string month = selectedItem[0].ToString();
            string day = selectedItem[1].ToString();
            string year = selectedItem[2].ToString();

            SelectedDateTime.ToString("Month : " + month + "Day : " + day + "Year : " + year);
        }



        public ICommand CancelFormADetailsCommand
        {

            get
            {
                return new Command(async (obj) =>
                {

                    if (AppType == "View")
                    {
                        App.DetailType = "AddDetail";

                        //App.ReturnType = "View";

                        await CurrentPage.Navigation.PopAsync();

                        return;
                    }
                    else
                    {




                        editViewModel.Type = "Add";

                        editViewModel.HdrFahPkRefNo = GetHeaderNo;

                        App.DetailType = "AddDetail";

                        //App.DetailHeaderCode = GetHeaderNo;


                        await CurrentPage.Navigation.PopAsync();
                    }


                });
            }
        }


        public async Task<int> GetLastRecordReferenceNumber(int iRefCode)
        {
            int iStrValue = 0;

            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {

                    try
                    {
                        var response = await _restApi.DetailSrNo(iRefCode);

                        iStrValue = response.data + 1;

                        return iStrValue;

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
            return iStrValue;
        }


        public ICommand FileBrowsCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    bool uploadedSuccess = true;

                    var actionResult = await UserDialogs.Instance.ActionSheetAsync("Upload Files", "", null, null, "Take a photo", "Gallery");
                    if (actionResult == "Cancel")
                    {
                        return;
                    }
                    if (actionResult == "Take a photo")
                    {
                        var file = await TakePhoto();

                        if (file == null)
                            return;
                        else
                        {
                            if (fileDataList.Files == null)
                            {
                                fileDataList.Files = new List<MediaFile>();
                                fileDataList.FileNames = new ObservableCollection<string>();
                            }
                            fileDataList.Files.Add(file);
                            fileDataList.FileNames.Add(Path.GetFileName(file.Path));
                            UploadFileName = Path.GetFileName(file.Path);



                        }
                    }
                    else if (actionResult == "Gallery")
                    {
                        var file = await PickPhoto();

                        if (file == null)
                            return;
                        else
                        {
                            if (fileDataList.Files == null)
                            {
                                fileDataList.Files = new List<MediaFile>();
                                fileDataList.FileNames = new ObservableCollection<string>();
                            }
                            fileDataList.Files.Add(file);
                            fileDataList.FileNames.Add(Path.GetFileName(file.Path));
                            UploadFileName = Path.GetFileName(file.Path);
                            //UploadFileName = Path.Combine

                        }
                    }
                });
            }
        }

        public async Task<MediaFile> PickPhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                _userDialogs.Alert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return null;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return null;
            else
            {
                double filesize = (file.GetStream().Length / 1048576);
                if (filesize > 5)
                {
                    _userDialogs.Alert("Maximum 5Mb files allowed");
                    return null;
                }
            }
            return file;
        }

        public async Task<MediaFile> TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                _userDialogs.Alert("No camera available", ":Permission not granted to photos.", "OK");
                return null;
            }

            var options = new StoreCameraMediaOptions()
            {
                Name = DateTime.Now.ToShortDateString() + "Form_A",
                SaveToAlbum = false,
            };
            var file = await CrossMedia.Current.TakePhotoAsync(options);
            if (file == null)
                return null;
            else
            {
                double filesize = (file.GetStream().Length / 1048576);
                if (filesize > 5)
                {
                    _userDialogs.Alert("Maximum 5Mb files allowed");
                    return null;
                }
            }
            return file;
        }


    }
}