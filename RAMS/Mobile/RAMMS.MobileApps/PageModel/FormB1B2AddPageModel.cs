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
    public class FormB1B2AddPageModel : FreshBasePageModel
    {
        private bool _isPhotoTabVisible;
        private bool _isInspNameEnabled;
        private bool _fromAdd;
        private IRestApi _restApi;
        private IUserDialogs _userDialogs;
        private ILocalDatabase _localDatabase;
        private AssetDDLResponseDTO.DropDown _selectedRMU;
        private AssetDDLResponseDTO.DropDown _selectedRoadCode;
        private AssetDDLResponseDTO.DropDown _selectedSectionCode;
        private int _selectedDivision = -1;
        private int _selectedYear = -1;
        private int _selectedBridge = -1;
        private int? _selectedBridgeConditionRating = -1;
        private int? _selectedFutureInvestigation = -1;

        private int _selectedVerIndex = -1;
        private int _selectedInspIndex = -1;

        private bool isModify;
        private bool isDelete;
        private bool isView;
        SignaturePadView InspPadView;
        SignaturePadView VerPadView;

        Grid MainGrid;
        public ObservableCollection<FormB1B2ImgRequestDTO> DetailImageList { get; set; }
        Image image { get; set; }
        Label label { get; set; }
        public bool IsAdd { get; set; }
        public bool IsHeaderEnable { get; set; } = true;
        public bool CanSave { get; set; } = false;
        public string SelectedRoadName { get; set; }
        public string SelectedSectionName { get; set; }
        public string SmartSearch { get; set; }
        public DateTime? SelectedDate { get; set; } = null;
        public decimal? RoadLength { get; set; }
        public string SelectedRefNo { get; set; }
        //public string SelectedRefNo { get; set; }
        public bool IsVerNameEnabled { get; set; } = false;
        // public bool IsInspNameEnabled { get; set; } = false;

        public bool AbutmentOthersVisible { get; set; } = false;
        public bool BeamOthersVisible { get; set; } = false;
        public bool BearingOthersVisible { get; set; } = false;
        public bool DeckOthersVisible { get; set; } = false;
        public bool ExpansionOthersVisible { get; set; } = false;
        public bool ParapetOthersVisible { get; set; } = false;
        public bool PiersOthersVisible { get; set; } = false;
        public bool KerbOthersVisible { get; set; } = false;
        public bool SlopeOthersVisible { get; set; } = false;
        public bool SignboardOthersVisible { get; set; } = false;
        public bool DrainOthersVisible { get; set; } = false;
        public bool WaterwayOthersVisible { get; set; } = false;


        public string AbutmentOthers { get; set; }
        public string BeamOthers { get; set; }
        public string BearingOthers { get; set; }
        public string DeckOthers { get; set; }
        public string ExpansionOthers { get; set; }
        public string ParapetOthers { get; set; }
        public string PiersOthers { get; set; }
        public string KerbOthers { get; set; }
        public string SlopeOthers { get; set; }
        public string SignboardOthers { get; set; }
        public string DrainOthers { get; set; }
        public string WaterwayOthers { get; set; }


        private int _selectedAbutmentDistress = -1;
        private int _selectedBeamsDistress = -1;
        private int _selectedBearingDistress = -1;
        public int _selectedDeckDistress = -1;
        private int _selectedExpansionDistress = -1;
        private int _selectedParapetDistress = -1;
        private int _selectedPiersDistress = -1;
        private int _selectedSidewalksDistress = -1;
        private int _selectedSlopeDistress = -1;
        private int _selectedSignboardDistress = -1;
        private int _selectedDrainDistress = -1;
        private int _selectedWaterDistress = -1;

        public int SelectedAbutmentDistress
        {
            get { return _selectedAbutmentDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedAbutmentDistress = value;

                    var userprp = DDAbutmentDistressListItems[_selectedAbutmentDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        AbutmentOthersVisible = true;
                    }
                    else
                    {
                        AbutmentOthersVisible = false;
                    }

                }
            }
        }

        public int SelectedBeamsDistress
        {
            get { return _selectedBeamsDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedBeamsDistress = value;

                    var userprp = DDBeamsDistressListItems[_selectedBeamsDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        BeamOthersVisible = true;
                    }
                    else
                    {
                        BeamOthersVisible = false;
                    }

                }
            }
        }


        public int SelectedBearingDistress
        {
            get { return _selectedBearingDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedBearingDistress = value;

                    var userprp = DDBearingDistressListItems[_selectedBearingDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        BearingOthersVisible = true;
                    }
                    else
                    {
                        BearingOthersVisible = false;
                    }

                }
            }
        }


        public int SelectedDeckDistress
        {
            get { return _selectedDeckDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedDeckDistress = value;

                    var userprp = DDDeckDistressListItems[_selectedDeckDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        DeckOthersVisible = true;
                    }
                    else
                    {
                        DeckOthersVisible = false;
                    }

                }
            }
        }

        public int SelectedExpansionDistress
        {
            get { return _selectedExpansionDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedExpansionDistress = value;

                    var userprp = DDExpansionDistressListItems[_selectedExpansionDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        ExpansionOthersVisible = true;
                    }
                    else
                    {
                        ExpansionOthersVisible = false;
                    }

                }
            }
        }

        public int SelectedParapetDistress
        {
            get { return _selectedParapetDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedParapetDistress = value;

                    var userprp = DDParapetDistressListItems[_selectedParapetDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        ParapetOthersVisible = true;
                    }
                    else
                    {
                        ParapetOthersVisible = false;
                    }

                }
            }
        }

        public int SelectedPiersDistress
        {
            get { return _selectedPiersDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedPiersDistress = value;

                    var userprp = DDPiersDistressListItems[_selectedPiersDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        PiersOthersVisible = true;
                    }
                    else
                    {
                        PiersOthersVisible = false;
                    }

                }
            }
        }

        public int SelectedSidewalksDistress
        {
            get { return _selectedSidewalksDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedSidewalksDistress = value;

                    var userprp = DDSidewalksDistressListItems[_selectedSidewalksDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        KerbOthersVisible = true;
                    }
                    else
                    {
                        KerbOthersVisible = false;
                    }

                }
            }
        }


        public int SelectedSlopeDistress
        {
            get { return _selectedSlopeDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedSlopeDistress = value;

                    var userprp = DDSlopeDistressListItems[_selectedSlopeDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        SlopeOthersVisible = true;
                    }
                    else
                    {
                        SlopeOthersVisible = false;
                    }

                }
            }
        }


        public int SelectedSignboardDistress
        {
            get { return _selectedSignboardDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedSignboardDistress = value;

                    var userprp = DDSignboardDistressListItems[_selectedSignboardDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        SignboardOthersVisible = true;
                    }
                    else
                    {
                        SignboardOthersVisible = false;
                    }

                }
            }
        }


        public int SelectedDrainDistress
        {
            get { return _selectedDrainDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedDrainDistress = value;

                    var userprp = DDDrainDistressListItems[_selectedDrainDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        DrainOthersVisible = true;
                    }
                    else
                    {
                        DrainOthersVisible = false;
                    }

                }
            }
        }

        public int SelectedWaterDistress
        {
            get { return _selectedWaterDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedWaterDistress = value;

                    var userprp = DDWaterDistressListItems[_selectedWaterDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        WaterwayOthersVisible = true;
                    }
                    else
                    {
                        WaterwayOthersVisible = false;
                    }

                }
            }
        }

        public bool IsInspNameEnabled
        {
            get
            {
                return _isInspNameEnabled;
            }
            set
            {
                _isInspNameEnabled = value;
                RaisePropertyChanged("IsInspNameEnabled");
            }
        }
        public string InspName { get; set; }
        public string VerName { get; set; }
        public string InspDesignation { get; set; }
        public string VerDesignation { get; set; }
        public ImageSource InspSign { get; set; }
        public ImageSource VerSign { get; set; }
        public DateTime? InspDate { get; set; } = null;
        public DateTime? VerDate { get; set; } = null;
       

        public int RoadID { get; set; }
        public string PartBServiceProvider { get; set; }
        public string PartCServiceProvider { get; set; }
        public string PartDServiceProvider { get; set; }
        public string PartBConsultant { get; set; }
        public string PartCConsultant { get; set; }
        public string PartDConsultant { get; set; }
        public FilteredPagingDefinition<FormB1B2DetailRequestDTO> SearchCriteriaItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRodeCodeListItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRMUListItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDSectionListItems { get; set; }
        public ObservableCollection<DDListItems> DDYearListItems { get; set; }
        public ObservableCollection<DDListItems> DDDivisionListItems { get; set; }
        public ObservableCollection<DDListItems> DDBridgeListItems { get; set; }
        public ObservableCollection<DDListItems> DDRatingListItems { get; set; }
        public ObservableCollection<DDListItems> DDInspUserListListItems { get; set; }
        public ObservableCollection<DDListItems> DDVerUserListListItems { get; set; }

        public ObservableCollection<DDListItems> DDAbutmentSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDBeamsSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDBearingSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDDeckSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDExpansionSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDParapetSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDPiersSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDSidewalksSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDSlopeSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDSignboardSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDDrainSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDWaterSeverityListItems { get; set; }

        public ObservableCollection<DDListItems> DDAbutmentDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDBeamsDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDBearingDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDDeckDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDExpansionDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDParapetDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDPiersDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDSidewalksDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDSlopeDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDSignboardDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDDrainDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDWaterDistressListItems { get; set; }

       // public int SelectedAbutmentDistress { get; set; } = -1;
      //  public int SelectedBeamsDistress { get; set; } = -1;
      //  public int SelectedBearingDistress { get; set; } = -1;
       // public int SelectedDeckDistress { get; set; } = -1;
       // public int SelectedExpansionDistress { get; set; } = -1;
       // public int SelectedParapetDistress { get; set; } = -1;
       // public int SelectedPiersDistress { get; set; } = -1;
       // public int SelectedSidewalksDistress { get; set; } = -1;
        //public int SelectedSlopeDistress { get; set; } = -1;
        //public int SelectedSignboardDistress { get; set; } = -1;
        //public int SelectedDrainDistress { get; set; } = -1;
        //public int SelectedWaterDistress { get; set; } = -1;


        public int SelectedAbutmentSeverity { get; set; } = -1;
        public int SelectedBeamsSeverity { get; set; } = -1;
        public int SelectedBearingSeverity { get; set; } = -1;
        public int SelectedDeckSeverity { get; set; } = -1;
        public int SelectedExpansionSeverity { get; set; } = -1;
        public int SelectedParapetSeverity { get; set; } = -1;
        public int SelectedPiersSeverity { get; set; } = -1;
        public int SelectedSidewalksSeverity { get; set; } = -1;
        public int SelectedSlopeSeverity { get; set; } = -1;
        public int SelectedSignboardSeverity { get; set; } = -1;
        public int SelectedDrainSeverity { get; set; } = -1;
        public int SelectedWaterSeverity { get; set; } = -1;
        public FormB1B2HeaderRequestDTO SelectedHeaderItems { get; set; }

        public bool FromAdd
        {
            get
            {
                return _fromAdd;
            }
            set
            {
                _fromAdd = value;
                RaisePropertyChanged("FromAdd");
            }
        }

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
        public int SelectedVerIndex
        {

            get
            {
                return _selectedVerIndex;
            }
            set
            {
                _selectedVerIndex = value;
                if (_selectedVerIndex != -1)
                {
                    var selectedItem = Convert.ToInt32(DDVerUserListListItems?[_selectedVerIndex].Value.ToString());

                    GetUserDetilsList("veruser", selectedItem);

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
            await GetBridgeList();
        }

        public AssetDDLResponseDTO.DropDown SelectedRoadCode
        {
            get => _selectedRoadCode;
            set
            {
                _selectedRoadCode = value;
                if (_selectedRoadCode != null)
                {
                    SelectedRoadName = _selectedRoadCode.Item1;
                    GetBridgeList();
                    //SetRefNumber();
                }
                RaisePropertyChanged();
            }
        }

        //private void SetRefNumber()
        //{
        //    if (SelectedRoadCode != null && SelectedYear > 0)
        //        SelectedRefNo = "CI/Form F2/" + SelectedRoadCode.Value + "/" + DDYearListItems[SelectedYear].Value;
        //}

        public AssetDDLResponseDTO.DropDown SelectedSectionCode
        {
            get => _selectedSectionCode;
            set
            {
                _selectedSectionCode = value;
                if (_selectedSectionCode != null)
                {
                    SelectedRoadCode = null;
                    SelectedRoadName = "";
                    GetLandingDropDownList();
                    GetBridgeList();
                    SelectedSectionName = _selectedSectionCode.Text.Split('-')[1].ToString();
                }
                RaisePropertyChanged();
            }
        }

        public DateTime? MinimumDate { get; set; } = null;
        public DateTime? MaximumDate { get; set; } = null;
        public int SelectedYear
        {
            get => _selectedYear;
            set
            {
                _selectedYear = value;
                RaisePropertyChanged();


                if (SelectedYear != -1)
                {
                    var year = DDYearListItems[SelectedYear]?.Text;
                    MinimumDate = Convert.ToDateTime("1-1-" + year);
                    MaximumDate = Convert.ToDateTime("12-31-" + year);

                    SelectedDate = null;
                }
            }
        }
        public int SelectedBridge
        {
            get => _selectedBridge;
            set
            {
                _selectedBridge = value;
                RaisePropertyChanged();
            }
        }

        public int? SelectedBridgeConditionRating
        {
            get => _selectedBridgeConditionRating;
            set
            {
                _selectedBridgeConditionRating = value;
                RaisePropertyChanged();
            }
        }

        public int? SelectedFutureInvestigation
        {
            get => _selectedFutureInvestigation;
            set
            {
                _selectedFutureInvestigation = value;
                RaisePropertyChanged();
            }
        }

        #region


        public int PkRefNo { get; set; }
        public int? AiPkRefNo { get; set; }
        public string AiAssetId { get; set; }
        public int? AiLocChKm { get; set; }
        public string AiLocChM { get; set; }
        public string AiStrucCode { get; set; }
        public double? AiGpsEasting { get; set; }
        public double? AiGpsNorthing { get; set; }
        public string AiRdCode { get; set; }
        public string AiRdName { get; set; }
        public string AiRiverName { get; set; }
        public string AiDivCode { get; set; }
        public string AiRmuName { get; set; }
        public string AiStrucSuper { get; set; }
        public string AiParapetType { get; set; }
        public string AiBearingType { get; set; }
        public string AiExpanType { get; set; }
        public string AiDeckType { get; set; }
        public string AiAbutType { get; set; }
        public string AiPierType { get; set; }
        public int? AiExpanJointCount { get; set; }
        public double? AiWidthLane { get; set; }
        public double? AiLengthSpan { get; set; }
        public double? AiLength { get; set; }
        public double? AiWidth { get; set; }
        public int? AiLaneCnt { get; set; }
        public int? AiSpanCnt { get; set; }
        public double? AiMedian { get; set; }
        public double? AiWalkway { get; set; }
        public string CInspRefNo { get; set; }
        public int? YearOfInsp { get; set; }
        public DateTime? DtOfInsp { get; set; }
        public int? RecordNo { get; set; }
        public string SerProviderDefObs { get; set; }
        public string AuthDefObs { get; set; }
        public string SerProviderDefGenCom { get; set; }
        public string AuthDefGenCom { get; set; }
        public string SerProviderDefFeedback { get; set; }
        public string AuthDefFeedback { get; set; }
        public int? SerProviderUserId { get; set; }
        public string SerProviderUserName { get; set; }
        public string SerProviderUserDesignation { get; set; }
        public DateTime? SerProviderInsDt { get; set; }
        public string SignpathSerProvider { get; set; }
        public int? UserIdAud { get; set; }
        public string UserNameAud { get; set; }
        public string UserDesignationAud { get; set; }
        public DateTime? DtAud { get; set; }
        public string SignpathAud { get; set; }
        public int? BridgeConditionRat { get; set; }
        public bool? ReqFurtherInv { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public FormB1B2DetailRequestDTO Detail { get; set; }
        public bool IsView { get; set; } = false;
        public string RmuCode { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string Status { get; set; }
        public string DisplayAssetId { get; set; }

        #endregion

        public FormB1B2AddPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;
            _localDatabase = localDatabase;
        }
        public override void Init(object initData)
        {
            base.Init(initData);
            IsPhotoTabVisible = false;

            isView = Model.Security.IsView(ModuleNameList.Condition_Inspection);
            IsAdd = isModify = Model.Security.IsModify(ModuleNameList.Condition_Inspection);
            isDelete = Model.Security.IsDelete(ModuleNameList.Condition_Inspection);

            if (App.ReturnType == "Add")
            {
                FromAdd = true;
                App.HeaderCode = 0;
            }
        }

        protected async override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            await GetLandingDropDownList();
            await GetDDListDetails("division");
            await GetDDListDetails("Year");
            await GetDDLDescValueContact("B1B2_Severity");
            await GetDDLDescValueContact("B1B2_Distress");
            await GetBridgeList();
            await GetUserList();
            CanSave = App.ReturnType == "Edit" ? true : false;
            if (App.ReturnType == "Edit" || App.ReturnType == "View")
            {
                IsHeaderEnable = false;
                IsView = App.ReturnType == "View" ? true : false;
                await GetB1B2ById(App.HeaderCode);
            }

            string strDetailCode = Convert.ToInt32(App.HeaderCode).ToString();

            GetImageList(strDetailCode);


            MessagingCenter.Unsubscribe<object, string>(this, "Uploaded");

            MessagingCenter.Subscribe<object, string>(this, "Uploaded", (obj, s) =>
            {
                GetImageList(strDetailCode);
            });
        }

        private async void GetImageList(string AssetID)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    try
                    {
                        var hdrresponse = await _restApi.GetB1B2Images(App.HeaderCode);

                        if (hdrresponse.success == true)
                        {
                            DetailImageList = new ObservableCollection<FormB1B2ImgRequestDTO>(hdrresponse.data);

                            int i = 0;
                            foreach (var listdata in DetailImageList)
                            {
                                listdata.ImageSrno = i + 1;
                                i++;
                            }


                            //int i = 0;

                            //try
                            //{
                            //    MainGrid.Children.Clear();
                            //    foreach (var listdata in DetailImageList)
                            //    {
                            //        listdata.ImageSrno = i + 1;
                            //        string Path = listdata.ImageUserFilepath;

                            //        try
                            //        {
                            //            ActivityIndicator indicator = new ActivityIndicator() { BindingContext = image };
                            //            indicator.SetBinding(ActivityIndicator.IsRunningProperty, new Binding("IsLoading", source: image));
                            //            indicator.SetBinding(ActivityIndicator.IsVisibleProperty, new Binding("IsLoading", source: image));
                            //            image = new Image
                            //            {
                            //                Source = ImageSource.FromUri(new Uri(AppConst.WebBaseURL + Path))
                            //            };

                            //            label = new Label
                            //            {
                            //                Text = listdata.ImageFilenameSys,
                            //                HorizontalTextAlignment = TextAlignment.Center,
                            //                Margin = -10
                            //            };
                            //            MainGrid.RowDefinitions.Add(new RowDefinition { Height = 100 });
                            //            Grid.SetColumn(indicator, i);
                            //            Grid.SetColumn(image, i);
                            //            Grid.SetRow(label, 1);
                            //            Grid.SetColumn(label, i);

                            //            MainGrid.Children.Add(indicator);
                            //            MainGrid.Children.Add(image);
                            //            MainGrid.Children.Add(label);

                            //            i = i + 1;
                            //        }
                            //        catch (Exception ex)
                            //        {

                            //        }

                            //    }
                            //}
                            //catch (Exception ex)
                            //{ }

                        }
                    }
                    catch (Exception ex) { }
                }
            }
            catch { }
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
                                var imageID = (obj as FormB1B2ImgRequestDTO).PkRefNo;
                                var response = await _restApi.DeleteB1B2Image(imageID);

                                if (response.success)
                                {
                                    await UserDialogs.Instance.AlertAsync("Image deleted successfully.", "RAMS", "0K");

                                    string strDetailCode = Convert.ToInt32(App.HeaderCode).ToString();

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
        public ICommand AddImage
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    await CoreMethods.PushPageModel<FormB1B2CameraPopupPageModel>();
               
               // CurrentPage.Navigation.PushAsync(new FormB1B2CameraPopupPageModel());
                   


                });
            }
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
        public ICommand ToggleCommand
        {
            get
            {
                return new Command(ToggleBarTapped);
            }
        }

        public ICommand OKCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    try
                    {



                        //if (SelectedDivision == -1)
                        //{
                        //    await UserDialogs.Instance.AlertAsync("Please select Division", "RAMS", "OK");
                        //    return;
                        //}

                        if (string.IsNullOrWhiteSpace(SelectedRoadCode?.Value))
                        {
                            await UserDialogs.Instance.AlertAsync("Road Code field is required. Please choose from the dropdown list.", "RAMS", "OK");
                            return;
                        }
                        if (SelectedBridge == -1)
                        {
                            await UserDialogs.Instance.AlertAsync("Bridge Id field is required. Please choose from the dropdown list.", "RAMS", "OK");
                            return;
                        }

                        if (SelectedYear == -1)
                        {
                            await UserDialogs.Instance.AlertAsync("Year field is required. Please choose from the dropdown list.", "RAMS", "OK");
                            return;
                        }

                        //if (SelectedDate == null)
                        //{
                        //    _userDialogs.Alert("Please select the Date of Inspection", "Error");
                        //    return;
                        //}

                        var response = SaveFormB1B2Header();

                        FromAdd = false;

                        App.ReturnType = "Edit";

                    }
                    catch
                    {
                    }


                });
            }
        }

        private async Task<ObservableCollection<FormB1B2HeaderRequestDTO>> SaveFormB1B2Header()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    FormB1B2HeaderRequestDTO GridItems = new FormB1B2HeaderRequestDTO()
                    {
                        PkRefNo = App.HeaderCode,
                        AiPkRefNo = Convert.ToInt32(DDBridgeListItems[SelectedBridge]?.Value),
                        DisplayAssetId = DDBridgeListItems[SelectedBridge]?.Text,
                     //   AiDivCode = DDDivisionListItems[SelectedDivision].Value,
                        AiDivCode = SelectedDivision == -1 ? null : DDDivisionListItems[SelectedDivision]?.Value,

                        RmuCode = SelectedRMU?.Value,
                        AiRmuName = SelectedRMU?.Text,
                        SectionCode = SelectedSectionCode?.Code,
                        SectionName = SelectedSectionName,
                        AiRdCode = SelectedRoadCode?.Value,
                        AiRdName = SelectedRoadName,
                        YearOfInsp = Convert.ToInt32(DDYearListItems[SelectedYear]?.Value),
                        DtOfInsp = SelectedDate.HasValue ? SelectedDate.Value : (DateTime?)null
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

                    var response = await _restApi.SaveB1B2Hdr(GridItems);

                    if (response.success)
                    {
                        IsHeaderEnable = false;
                        if (response.data.SubmitSts)
                            CanSave = false;
                        else
                            CanSave = true;
                        App.HeaderCode = response.data.PkRefNo;
                        SetViewData(response);
                       // App.ReturnType = "";
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

        private async Task<int> GetB1B2ById(int headerCode)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.GetB1B2ById(headerCode);

                    if (response.success)
                    {

                        SetViewData(response);
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

        public string AbutFoundMat { get; set; }
        public string AbutFoundMatCode { get; set; }
        public string AbutFoundDistress { get; set; }
        public int? AbutFoundSeverity { get; set; }
        public string PiersPrimCompMat { get; set; }
        public string PiersPrimCompMatCode { get; set; }
        public string PiersPrimCompDistress { get; set; }
        public int? PiersPrimCompSeverity { get; set; }
        public string BearingStDiaphgMat { get; set; }
        public string BearingStDiaphgMatCode { get; set; }
        public string BearingStDiaphgDistress { get; set; }
        public int? BearingStDiaphgSeverity { get; set; }
        public string BeamsGridTrusArch { get; set; }
        public string BeamsGridTrusArchCode { get; set; }
        public string BeamsGridTrusArchDistress { get; set; }
        public int? BeamsGridTrusArchSeverity { get; set; }
        public string DeckPavement { get; set; }
        public string DeckPavementCode { get; set; }
        public string DeckPavementDistress { get; set; }
        public int? DeckPavementSeverity { get; set; }
        public string Utilities { get; set; }
        public string UtilitiesCode { get; set; }
        public string UtilitiesDistress { get; set; }
        public int? UtilitiesSeverity { get; set; }
        public string Waterway { get; set; }
        public string WaterwayCode { get; set; }
        public string WaterwayDistress { get; set; }
        public int? WaterwaySeverity { get; set; }
        public string WaterDownpipe { get; set; }
        public string WaterDownpipeCode { get; set; }
        public string WaterDownpipeDistress { get; set; }
        public int? WaterDownpipeSeverity { get; set; }
        public string ParapetRailing { get; set; }
        public string ParapetRailingCode { get; set; }
        public string ParapetRailingDistress { get; set; }
        public int? ParapetRailingSeverity { get; set; }
        public string SidewalksAppSlab { get; set; }
        public string SidewalksAppSlabCode { get; set; }
        public string SidewalksAppSlabDistress { get; set; }
        public int? SidewalksAppSlabSeverity { get; set; }
        public string ExpanJoint { get; set; }
        public string ExpanJointCode { get; set; }
        public string ExpanJointDistress { get; set; }
        public int? ExpanJointSeverity { get; set; }
        public string SlopeRetainWall { get; set; }
        public string SlopeRetainWallCode { get; set; }
        public string SlopeRetainWallDistress { get; set; }
        public int? SlopeRetainWallSeverity { get; set; }


        private void SetViewData(ResponseBaseObject<FormB1B2HeaderRequestDTO> response)
        {
            SelectedRefNo = response.data.CInspRefNo;
            SelectedBridge = DDBridgeListItems.ToList().FindIndex(x => x.Text == response.data.AiAssetId);
            SelectedDivision = DDDivisionListItems.ToList().FindIndex(x => x.Value == response.data.AiDivCode);
            SelectedYear = DDYearListItems.ToList().FindIndex(x => x.Value == response.data.YearOfInsp.ToString());
            SelectedDate = response.data.DtOfInsp.HasValue ? response.data.DtOfInsp.Value : (DateTime?)null;

            AiAssetId = response.data.AiAssetId;
            AiDivCode = response.data.AiDivCode;
            AiRmuName = response.data.AiRmuName;
            AiRdCode = response.data.AiRdCode;
            AiRdName = response.data.AiRdName;
            AiStrucCode = response.data.AiStrucCode;
            AiLocChKm = response.data.AiLocChKm;
            AiLocChM = response.data.AiLocChM;
            AiRiverName = response.data.AiRiverName;
            AiWidthLane = response.data.AiWidthLane;
            AiLengthSpan = response.data.AiLengthSpan;
            AiLength = response.data.AiLength;
            AiWidth = response.data.AiWidth;
            AiLaneCnt = response.data.AiLaneCnt;
            AiSpanCnt = response.data.AiSpanCnt;
            AiMedian = response.data.AiMedian;
            AiWalkway = response.data.AiWalkway;
            AiStrucSuper = response.data.AiStrucSuper;
            AiParapetType = response.data.AiParapetType;
            AiBearingType = response.data.AiBearingType;
            AiExpanType = response.data.AiExpanType;
            AiDeckType = response.data.AiDeckType;
            AiAbutType = response.data.AiAbutType;
            AiPierType = response.data.AiPierType;
            AiExpanJointCount = response.data.AiExpanJointCount;
            AiGpsEasting = response.data.AiGpsEasting;
            AiGpsNorthing = response.data.AiGpsNorthing;

            AbutFoundMat = response.data.Detail.AbutFoundMat;
            AbutFoundMatCode = response.data.Detail.AbutFoundMatCode;
            //AbutFoundDistress = response.data.Detail.AbutFoundDistress;
            //AbutFoundSeverity = response.data.Detail.AbutFoundSeverity;
            SelectedAbutmentDistress = DDAbutmentDistressListItems.ToList().FindIndex(x => x.Value == response.data.Detail.AbutFoundDistress);
            SelectedAbutmentSeverity = DDAbutmentSeverityListItems.ToList().FindIndex(x => x.Value == response.data.Detail.AbutFoundSeverity.ToString());


            BeamsGridTrusArch = response.data.Detail.BeamsGridTrusArch;
            BeamsGridTrusArchCode = response.data.Detail.BeamsGridTrusArchCode;
            //BeamsGridTrusArchDistress = response.data.Detail.BeamsGridTrusArchDistress;
            //BeamsGridTrusArchSeverity = response.data.Detail.BeamsGridTrusArchSeverity;
            SelectedBeamsDistress = DDBeamsDistressListItems.ToList().FindIndex(x => x.Value == response.data.Detail.BeamsGridTrusArchDistress);
            SelectedBeamsSeverity = DDBeamsSeverityListItems.ToList().FindIndex(x => x.Value == response.data.Detail.BeamsGridTrusArchSeverity.ToString());

            BearingStDiaphgMat = response.data.Detail.BearingStDiaphgMat;
            BearingStDiaphgMatCode = response.data.Detail.BearingStDiaphgMatCode;
            //BearingStDiaphgDistress = response.data.Detail.BearingStDiaphgDistress;
            //BearingStDiaphgSeverity = response.data.Detail.BearingStDiaphgSeverity;
            SelectedBearingDistress = DDBearingDistressListItems.ToList().FindIndex(x => x.Value == response.data.Detail.BearingStDiaphgDistress);
            SelectedBearingSeverity = DDBearingSeverityListItems.ToList().FindIndex(x => x.Value == response.data.Detail.BearingStDiaphgSeverity.ToString());


            DeckPavement = response.data.Detail.DeckPavement;
            DeckPavementCode = response.data.Detail.DeckPavementCode;
            //DeckPavementDistress = response.data.Detail.DeckPavementDistress;
            //DeckPavementSeverity = response.data.Detail.DeckPavementSeverity;
            SelectedDeckDistress = DDDeckDistressListItems.ToList().FindIndex(x => x.Value == response.data.Detail.DeckPavementDistress);
            SelectedDeckSeverity = DDDeckSeverityListItems.ToList().FindIndex(x => x.Value == response.data.Detail.DeckPavementSeverity.ToString());

            ExpanJoint = response.data.Detail.ExpanJoint;
            ExpanJointCode = response.data.Detail.ExpanJointCode;
            //ExpanJointDistress = response.data.Detail.ExpanJointDistress;
            //ExpanJointSeverity = response.data.Detail.ExpanJointSeverity;
            SelectedExpansionDistress = DDExpansionDistressListItems.ToList().FindIndex(x => x.Value == response.data.Detail.ExpanJointDistress);
            SelectedExpansionSeverity = DDExpansionSeverityListItems.ToList().FindIndex(x => x.Value == response.data.Detail.ExpanJointSeverity.ToString());

            ParapetRailing = response.data.Detail.ParapetRailing;
            ParapetRailingCode = response.data.Detail.ParapetRailingCode;
            //ParapetRailingDistress = response.data.Detail.ParapetRailingDistress;
            //ParapetRailingSeverity = response.data.Detail.ParapetRailingSeverity;
            SelectedParapetDistress = DDParapetDistressListItems.ToList().FindIndex(x => x.Value == response.data.Detail.ParapetRailingDistress);
            SelectedParapetSeverity = DDParapetSeverityListItems.ToList().FindIndex(x => x.Value == response.data.Detail.ParapetRailingSeverity.ToString());

            PiersPrimCompMat = response.data.Detail.PiersPrimCompMat;
            PiersPrimCompMatCode = response.data.Detail.PiersPrimCompMatCode;
            //PiersPrimCompDistress = response.data.Detail.PiersPrimCompDistress;
            //PiersPrimCompSeverity = response.data.Detail.PiersPrimCompSeverity;
            SelectedPiersDistress = DDPiersDistressListItems.ToList().FindIndex(x => x.Value == response.data.Detail.PiersPrimCompDistress);
            SelectedPiersSeverity = DDPiersSeverityListItems.ToList().FindIndex(x => x.Value == response.data.Detail.PiersPrimCompSeverity.ToString());

            SidewalksAppSlab = response.data.Detail.SidewalksAppSlab;
            SidewalksAppSlabCode = response.data.Detail.SidewalksAppSlabCode;
            //SidewalksAppSlabDistress = response.data.Detail.SidewalksAppSlabDistress;
            //SidewalksAppSlabSeverity = response.data.Detail.SidewalksAppSlabSeverity;
            SelectedSidewalksDistress = DDSidewalksDistressListItems.ToList().FindIndex(x => x.Value == response.data.Detail.SidewalksAppSlabDistress);
            SelectedSidewalksSeverity = DDSidewalksSeverityListItems.ToList().FindIndex(x => x.Value == response.data.Detail.SidewalksAppSlabSeverity.ToString());


            SlopeRetainWall = response.data.Detail.SlopeRetainWall;
            SlopeRetainWallCode = response.data.Detail.SlopeRetainWallCode;
            //SlopeRetainWallDistress = response.data.Detail.SlopeRetainWallDistress;
            //SlopeRetainWallSeverity = response.data.Detail.SlopeRetainWallSeverity;
            SelectedSlopeDistress = DDSlopeDistressListItems.ToList().FindIndex(x => x.Value == response.data.Detail.SlopeRetainWallDistress);
            SelectedSlopeSeverity = DDSlopeSeverityListItems.ToList().FindIndex(x => x.Value == response.data.Detail.SlopeRetainWallSeverity.ToString());

            Utilities = response.data.Detail.Utilities;
            UtilitiesCode = response.data.Detail.UtilitiesCode;
            //UtilitiesDistress = response.data.Detail.UtilitiesDistress;
            //UtilitiesSeverity = response.data.Detail.UtilitiesSeverity;
            SelectedSignboardDistress = DDSignboardDistressListItems.ToList().FindIndex(x => x.Value == response.data.Detail.UtilitiesDistress);
            SelectedSignboardSeverity = DDSignboardSeverityListItems.ToList().FindIndex(x => x.Value == response.data.Detail.UtilitiesSeverity.ToString());


            WaterDownpipe = response.data.Detail.WaterDownpipe;
            WaterDownpipeCode = response.data.Detail.WaterDownpipeCode;
            //WaterDownpipeDistress = response.data.Detail.WaterDownpipeDistress;
            //WaterDownpipeSeverity = response.data.Detail.WaterDownpipeSeverity;
            SelectedDrainDistress = DDDrainDistressListItems.ToList().FindIndex(x => x.Value == response.data.Detail.WaterDownpipeDistress);
            SelectedDrainSeverity = DDDrainSeverityListItems.ToList().FindIndex(x => x.Value == response.data.Detail.WaterDownpipeSeverity.ToString());

            Waterway = response.data.Detail.Waterway;
            WaterwayCode = response.data.Detail.WaterwayCode;
            //WaterwayDistress = response.data.Detail.WaterwayDistress;
            //WaterwaySeverity = response.data.Detail.WaterwaySeverity;
            SelectedWaterDistress = DDWaterDistressListItems.ToList().FindIndex(x => x.Value == response.data.Detail.WaterwayDistress);
            SelectedWaterSeverity = DDWaterSeverityListItems.ToList().FindIndex(x => x.Value == response.data.Detail.WaterwaySeverity.ToString());


            SelectedBridgeConditionRating = response.data.BridgeConditionRat - 1;
            SelectedFutureInvestigation = response.data.ReqFurtherInv != null ? (response.data.ReqFurtherInv == true ? 0 : 1) : (int?)null;

            SerProviderDefObs = response.data.SerProviderDefObs;
            AuthDefObs = response.data.AuthDefObs;
            SerProviderDefGenCom = response.data.SerProviderDefGenCom;
            AuthDefGenCom = response.data.AuthDefGenCom;
            SerProviderDefFeedback = response.data.SerProviderDefFeedback;
            AuthDefFeedback = response.data.AuthDefFeedback;

            //others
            AbutmentOthers = response.data.Detail.AbutFoundDistressOthers;

            BeamOthers = response.data.Detail.BeamsGridTrusArchDistressOthers;
            BearingOthers = response.data.Detail.BearingStDiaphgDistressOthers;
            DeckOthers = response.data.Detail.DeckPavementDistressOthers;
            ExpansionOthers = response.data.Detail.ExpanJointDistressOthers;
            ParapetOthers = response.data.Detail.ParapetRailingDistressOthers;
            PiersOthers = response.data.Detail.PiersPrimCompDistressOthers;
            KerbOthers = response.data.Detail.SidewalksAppSlabDistressOthers;
            SlopeOthers = response.data.Detail.SlopeRetainWallDistressOthers;
            SignboardOthers = response.data.Detail.UtilitiesDistressOthers;
            DrainOthers = response.data.Detail.WaterDownpipeDistressOthers;
            WaterwayOthers = response.data.Detail.WaterwayDistressOthers;

            int inspindex = DDInspUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.SerProviderUserId);
            SelectedInspIndex = inspindex;
            InspName = response.data.SerProviderUserName;
            InspDesignation = response.data.SerProviderUserDesignation;
            InspDate = response.data.SerProviderInsDt.HasValue ? response.data.SerProviderInsDt.Value : (DateTime?)null;
            InspSign = ImageSource.FromStream(
                () => new MemoryStream(Convert.FromBase64String(response.data.SignpathSerProvider)));

            int verindex = DDVerUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.UserIdAud);
            SelectedVerIndex = verindex;
            VerName = response.data.UserNameAud;
            VerDesignation = response.data.UserDesignationAud;
            VerDate = response.data.DtAud.HasValue ? response.data.DtAud.Value : (DateTime?)null;
            VerSign = ImageSource.FromStream(
                () => new MemoryStream(Convert.FromBase64String(response.data.SignpathAud)));

        }

        private void ToggleBarTapped(object obj)
        {
            Frame layout = obj as Frame;

            if (layout != null)
            {
                if (layout.IsVisible)
                {
                    layout.IsVisible = false;
                }
                else
                {
                    layout.IsVisible = true;
                }
            }
            else
            {
                Image image = obj as Image;
                string imgsrc = (image?.Source as FileImageSource).File;
                if (String.Equals(imgsrc, "RoundedAddIcon.png"))
                {
                    image.Source = "minusicon.png";
                }
                else
                {
                    image.Source = "RoundedAddIcon.png";
                }
            }
        }

        public async Task<int> GetLandingDropDownList(string propName = null)
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
                        //GrpCode = "BR"
                    };

                    var response = await _restApi.GetB1B2LandingDropDown(strQuery);

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
            return 1;
        }

        private async Task<int> GetBridgeList()
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var ddlist = new AssetDDLRequestDTO()
                    {
                        RMU = SelectedRMU?.Value,
                        SectionCode = Convert.ToInt32(SelectedSectionCode?.Code),
                        RdCode = SelectedRoadCode?.Value
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(ddlist);
                    var response = await _restApi.GetDDBridgeList(ddlist);

                    if (response.success)
                    {
                        DDBridgeListItems = new ObservableCollection<DDListItems>(response.data);
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
                    var response = await _restApi.GetDDList(ddlist);

                    if (response.success)
                    {
                      if (ddtype == "Year")
                        {
                            SelectedYear = -1;
                            DDYearListItems = new ObservableCollection<DDListItems>(response.data);
                        }
                        else if (ddtype == "division")
                            DDDivisionListItems = new ObservableCollection<DDListItems>(response.data);
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

        private async Task<int> GetDDLDescValueContact(string ddtype)
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
                    var response = await _restApi.GetDDLDescValueConcat(ddlist);
                    response.data = response.data.OrderBy(x => x.Value).ToList();

                    if (response.success)
                    {
                        if (ddtype == "B1B2_Severity")
                        {
                            response.data = response.data.OrderBy(x => Convert.ToInt32(x.Value)).ToList();

                            DDAbutmentSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDBeamsSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDBearingSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDDeckSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDExpansionSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDParapetSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDPiersSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDSidewalksSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDSlopeSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDSignboardSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDDrainSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDWaterSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                        }
                        else if (ddtype == "B1B2_Distress")
                        {
                            DDAbutmentDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDBeamsDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDBearingDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDDeckDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDExpansionDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDParapetDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDPiersDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDSidewalksDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDSlopeDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDSignboardDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDDrainDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDWaterDistressListItems = new ObservableCollection<DDListItems>(response.data);
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
                        DDVerUserListListItems = new ObservableCollection<DDListItems>(response.data);
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
                                if (userprp.ToLower() == "others" && App.ReturnType == "Edit")
                                {
                                    IsInspNameEnabled = true;
                                    InspName = InspName ?? response.data.UserName;
                                    InspDesignation = InspDesignation ?? response.data.Position;
                                }
                                else
                                {
                                    IsInspNameEnabled = false;
                                    InspName = response.data.UserName;
                                    InspDesignation = response.data.Position;
                                }


                            }
                            else if (usertype == "veruser" && App.ReturnType == "Edit")
                            {
                                var userprp = DDVerUserListListItems[SelectedVerIndex].Text.Split('-')[1];
                                if (userprp.ToLower() == "others" && App.ReturnType == "Edit")
                                {
                                    IsVerNameEnabled = true;
                                    VerName = VerName ?? response.data.UserName;
                                    VerDesignation = VerDesignation ?? response.data.Position;
                                }
                                else
                                {
                                    IsVerNameEnabled = false;
                                    VerName = response.data.UserName;
                                    VerDesignation = response.data.Position;
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

        private async void SaveSignature(string type)
        {
            _userDialogs.ShowLoading("Loading");
            try
            {
                string inspSign = "";
                string verSign = "";
                InspPadView = CurrentPage.FindByName<SignaturePadView>("InspPadView");
                Stream image = await InspPadView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                if (!InspPadView.IsBlank)
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
                else
                    inspSign = null;

                VerPadView = CurrentPage.FindByName<SignaturePadView>("VerPadView");
                Stream image1 = await VerPadView.GetImageStreamAsync(SignaturePad.Forms.SignatureImageFormat.Png, strokeColor: Color.Black, Color.Transparent, false, true);

                if (!VerPadView.IsBlank)
                {
                    using (BinaryReader binaryReader = new BinaryReader(image1))
                    {
                        binaryReader.BaseStream.Position = 0;
                        byte[] Signature = binaryReader.ReadBytes((int)image1.Length);
                    }
                    var signatureMemoryStream = image1 as System.IO.MemoryStream;
                    var byteArray = signatureMemoryStream.ToArray();
                    string base64String = Convert.ToBase64String(byteArray);
                    verSign = base64String;
                }
                else
                    verSign = null;

                try
                {
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        var HeaderItemResponse = await _restApi.GetB1B2ById(App.HeaderCode);

                        if (HeaderItemResponse.success)
                        {
                            HeaderItemResponse.data.PkRefNo = App.HeaderCode;
                            HeaderItemResponse.data.DtOfInsp = SelectedDate;
                            HeaderItemResponse.data.Detail.AbutFoundDistress = SelectedAbutmentDistress.ToString() == "-1" ? null : DDAbutmentDistressListItems[SelectedAbutmentDistress]?.Value;
                            HeaderItemResponse.data.Detail.BeamsGridTrusArchDistress = SelectedBeamsDistress.ToString() == "-1" ? null : DDBeamsDistressListItems[SelectedBeamsDistress]?.Value;
                            HeaderItemResponse.data.Detail.BearingStDiaphgDistress = SelectedBearingDistress.ToString() == "-1" ? null : DDBearingDistressListItems[SelectedBearingDistress]?.Value;
                            HeaderItemResponse.data.Detail.DeckPavementDistress = SelectedDeckDistress.ToString() == "-1" ? null : DDDeckDistressListItems[SelectedDeckDistress]?.Value;
                            HeaderItemResponse.data.Detail.ExpanJointDistress = SelectedExpansionDistress.ToString() == "-1" ? null : DDExpansionDistressListItems[SelectedExpansionDistress]?.Value;
                            HeaderItemResponse.data.Detail.ParapetRailingDistress = SelectedParapetDistress.ToString() == "-1" ? null : DDParapetDistressListItems[SelectedParapetDistress]?.Value;
                            HeaderItemResponse.data.Detail.PiersPrimCompDistress = SelectedPiersDistress.ToString() == "-1" ? null : DDPiersDistressListItems[SelectedPiersDistress]?.Value;
                            HeaderItemResponse.data.Detail.SidewalksAppSlabDistress = SelectedSidewalksDistress.ToString() == "-1" ? null : DDSidewalksDistressListItems[SelectedSidewalksDistress]?.Value;
                            HeaderItemResponse.data.Detail.SlopeRetainWallDistress = SelectedSlopeDistress.ToString() == "-1" ? null : DDSlopeDistressListItems[SelectedSlopeDistress]?.Value;
                            HeaderItemResponse.data.Detail.UtilitiesDistress = SelectedSignboardDistress.ToString() == "-1" ? null : DDSignboardDistressListItems[SelectedSignboardDistress]?.Value;
                            HeaderItemResponse.data.Detail.WaterDownpipeDistress = SelectedDrainDistress.ToString() == "-1" ? null : DDDrainDistressListItems[SelectedDrainDistress]?.Value;
                            HeaderItemResponse.data.Detail.WaterwayDistress = SelectedWaterDistress.ToString() == "-1" ? null : DDWaterDistressListItems[SelectedWaterDistress]?.Value;

                            HeaderItemResponse.data.Detail.AbutFoundSeverity = SelectedAbutmentSeverity == -1 ? null : (int?)Convert.ToInt32(DDAbutmentSeverityListItems[SelectedAbutmentSeverity]?.Value);
                            HeaderItemResponse.data.Detail.BeamsGridTrusArchSeverity = SelectedBeamsSeverity == -1 ? null : (int?)Convert.ToInt32(DDBeamsSeverityListItems[SelectedBeamsSeverity]?.Value);
                            HeaderItemResponse.data.Detail.BearingStDiaphgSeverity = SelectedBearingSeverity == -1 ? null : (int?)Convert.ToInt32(DDBearingSeverityListItems[SelectedBearingSeverity]?.Value);
                            HeaderItemResponse.data.Detail.DeckPavementSeverity = SelectedDeckSeverity == -1 ? null : (int?)Convert.ToInt32(DDDeckSeverityListItems[SelectedDeckSeverity]?.Value);
                            HeaderItemResponse.data.Detail.ExpanJointSeverity = SelectedExpansionSeverity == -1 ? null : (int?)Convert.ToInt32(DDExpansionSeverityListItems[SelectedExpansionSeverity]?.Value);
                            HeaderItemResponse.data.Detail.ParapetRailingSeverity = SelectedParapetSeverity == -1 ? null : (int?)Convert.ToInt32(DDParapetSeverityListItems[SelectedParapetSeverity]?.Value);
                            HeaderItemResponse.data.Detail.PiersPrimCompSeverity = SelectedPiersSeverity == -1 ? null : (int?)Convert.ToInt32(DDPiersSeverityListItems[SelectedPiersSeverity]?.Value);
                            HeaderItemResponse.data.Detail.SidewalksAppSlabSeverity = SelectedSidewalksSeverity == -1 ? null : (int?)Convert.ToInt32(DDSidewalksSeverityListItems[SelectedSidewalksSeverity]?.Value);
                            HeaderItemResponse.data.Detail.SlopeRetainWallSeverity = SelectedSlopeSeverity == -1 ? null : (int?)Convert.ToInt32(DDSlopeSeverityListItems[SelectedSlopeSeverity]?.Value);
                            HeaderItemResponse.data.Detail.UtilitiesSeverity = SelectedSignboardSeverity == -1 ? null : (int?)Convert.ToInt32(DDSignboardSeverityListItems[SelectedSignboardSeverity]?.Value);
                            HeaderItemResponse.data.Detail.WaterDownpipeSeverity = SelectedDrainSeverity == -1 ? null : (int?)Convert.ToInt32(DDDrainSeverityListItems[SelectedDrainSeverity]?.Value);
                            HeaderItemResponse.data.Detail.WaterwaySeverity = SelectedWaterSeverity == -1 ? null : (int?)Convert.ToInt32(DDWaterSeverityListItems[SelectedWaterSeverity]?.Value);

                            HeaderItemResponse.data.SerProviderUserId = SelectedInspIndex != -1 ? Convert.ToInt32(DDInspUserListListItems[SelectedInspIndex].Value) : (int?)null;
                            HeaderItemResponse.data.SerProviderUserName = InspName;
                            HeaderItemResponse.data.SerProviderUserDesignation = InspDesignation;
                            HeaderItemResponse.data.SerProviderInsDt = InspDate;
                            HeaderItemResponse.data.SignpathSerProvider = inspSign ?? HeaderItemResponse.data.SignpathSerProvider ?? null;
                            HeaderItemResponse.data.UserIdAud = SelectedVerIndex != -1 ? Convert.ToInt32(DDVerUserListListItems[SelectedVerIndex].Value) : (int?)null;
                            HeaderItemResponse.data.UserNameAud = VerName;
                            HeaderItemResponse.data.UserDesignationAud = VerDesignation;
                            HeaderItemResponse.data.DtAud = VerDate;
                            HeaderItemResponse.data.SignpathAud = verSign ?? HeaderItemResponse.data.SignpathAud ?? null;
                            HeaderItemResponse.data.SerProviderDefObs = SerProviderDefObs;
                            HeaderItemResponse.data.AuthDefObs = AuthDefObs;
                            HeaderItemResponse.data.SerProviderDefGenCom = SerProviderDefGenCom;
                            HeaderItemResponse.data.AuthDefGenCom = AuthDefGenCom;
                            HeaderItemResponse.data.SerProviderDefFeedback = SerProviderDefFeedback;
                            HeaderItemResponse.data.AuthDefFeedback = AuthDefFeedback;
                            HeaderItemResponse.data.SubmitSts = type == "save" ? false : true;
                            //others
                            HeaderItemResponse.data.Detail.AbutFoundDistressOthers = AbutmentOthers;
                            HeaderItemResponse.data.Detail.BeamsGridTrusArchDistressOthers = BeamOthers;
                            HeaderItemResponse.data.Detail.BearingStDiaphgDistressOthers = BearingOthers;
                            HeaderItemResponse.data.Detail.DeckPavementDistressOthers = DeckOthers;
                            HeaderItemResponse.data.Detail.ExpanJointDistressOthers = ExpansionOthers;
                            HeaderItemResponse.data.Detail.ParapetRailingDistressOthers = ParapetOthers;
                            HeaderItemResponse.data.Detail.PiersPrimCompDistressOthers = PiersOthers;
                            HeaderItemResponse.data.Detail.SidewalksAppSlabDistressOthers = KerbOthers;
                            HeaderItemResponse.data.Detail.SlopeRetainWallDistressOthers = SlopeOthers;
                            HeaderItemResponse.data.Detail.UtilitiesDistressOthers = SignboardOthers;
                            HeaderItemResponse.data.Detail.WaterDownpipeDistressOthers = DrainOthers;
                            HeaderItemResponse.data.Detail.WaterwayDistressOthers = WaterwayOthers;


                            if (SelectedBridgeConditionRating != null && SelectedBridgeConditionRating != -1)
                                HeaderItemResponse.data.BridgeConditionRat = SelectedBridgeConditionRating + 1;
                            if (SelectedFutureInvestigation != null && SelectedFutureInvestigation != -1)
                                HeaderItemResponse.data.ReqFurtherInv = SelectedFutureInvestigation == 0 ? true : false;

                            var response = await _restApi.UpdateB1B2(HeaderItemResponse.data);
                            if (response.success)
                            {
                              //  if (response.data == -1)
                             //   {
                                    //_userDialogs.Alert("Atleast one photo is require for each photo type", "RAMS", "OK");
                                    //return;
                               // }
                                if (type == "save")
                                    await UserDialogs.Instance.AlertAsync("Data Saved Successfully.", "RAMS", "OK");
                                else
                                    await UserDialogs.Instance.AlertAsync("Data Submitted Successfully.", "RAMS", "OK");

                                CurrentPage.Navigation.PopAsync();
                            }
                            else
                            {
                                _userDialogs.Alert(response.errorMessage, "RAMS", "OK");
                            }
                        }
                        else
                        {
                            _userDialogs.Toast(HeaderItemResponse.errorMessage);
                        }
                        //await CoreMethods.PopPageModel();
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
                    if (SelectedDate == null)
                    {
                        _userDialogs.Alert("Date of Inspection is required.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedInspIndex == -1)
                    {
                        _userDialogs.Alert("Inspected by User Id field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (InspName == null || InspName == "")
                    {
                        _userDialogs.Alert("Inspected by User Name is required.", "RAMS", "Ok");
                        return;
                    }
                    if (InspDesignation == null || InspDesignation == "")
                    {
                        _userDialogs.Alert("Inspected by User Designation is required.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedBridgeConditionRating == -1 || SelectedBridgeConditionRating == null)
                    {
                        _userDialogs.Alert("Bridge Condition Rating field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedFutureInvestigation == -1 || SelectedFutureInvestigation == null)
                    {
                        _userDialogs.Alert("Further Investigation field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedAbutmentDistress != -1 && DDAbutmentDistressListItems[SelectedAbutmentDistress]?.Value == "B22" && (AbutmentOthers == null || AbutmentOthers == "")  )
                    {

                        _userDialogs.Alert("Foundation Distress others is required", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedBeamsDistress != -1 && DDBeamsDistressListItems[SelectedBeamsDistress]?.Value == "B22" && (BeamOthers == null || BeamOthers == ""))
                    {
                        _userDialogs.Alert("Arches - Distress Others is required", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedBearingDistress != -1 && DDBearingDistressListItems[SelectedBearingDistress]?.Value == "B22" && (BearingOthers == null || BearingOthers == ""))
                    {
                        _userDialogs.Alert("Bearing Diaphragm - Distress Others is required", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedDeckDistress != -1 && DDDeckDistressListItems[SelectedDeckDistress]?.Value == "B22" && (DeckOthers == null || DeckOthers == ""))
                    {
                        _userDialogs.Alert("Pavement - Distress Others is required", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedExpansionDistress != -1 && DDExpansionDistressListItems[SelectedExpansionDistress]?.Value == "B22" && (ExpansionOthers == null || ExpansionOthers == ""))
                    {
                        _userDialogs.Alert("Expansion Joint - Distress Others is required", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedParapetDistress != -1 && DDParapetDistressListItems[SelectedParapetDistress]?.Value == "B22" && (ParapetOthers == null || ParapetOthers == ""))
                    {
                        _userDialogs.Alert("Railing - Distress Others is required", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedPiersDistress != -1 && DDPiersDistressListItems[SelectedPiersDistress]?.Value == "B22" && (PiersOthers == null || PiersOthers == ""))
                    {
                        _userDialogs.Alert("Connection Of Primary Components - Distress Others is required", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedSidewalksDistress != -1 && DDSidewalksDistressListItems[SelectedSidewalksDistress]?.Value == "B22" && (KerbOthers  == null || KerbOthers == ""))
                    {
                        _userDialogs.Alert("Approaches Slab - Distress Others is required", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedSlopeDistress != -1 && DDSlopeDistressListItems[SelectedSlopeDistress]?.Value == "B22" && (SlopeOthers == null || SlopeOthers == ""))
                    {
                        _userDialogs.Alert("Retaining Wall - Distress Others is required", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedSignboardDistress != -1 && DDSignboardDistressListItems[SelectedSignboardDistress]?.Value == "B22" && (SignboardOthers == null || SignboardOthers == ""))
                    {
                        _userDialogs.Alert("Utilities - Distress Others is required", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedDrainDistress != -1 && DDDrainDistressListItems[SelectedDrainDistress]?.Value == "B22" && (DrainOthers == null || DrainOthers == ""))
                    {
                        _userDialogs.Alert("Drain Water Down Pipe - Distress Others is required", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedWaterDistress != -1 &&  DDWaterDistressListItems[SelectedWaterDistress]?.Value == "B22" && (WaterwayOthers == null || WaterwayOthers == ""))
                    {
                        _userDialogs.Alert("Waterway - Distress Others is required", "RAMS", "Ok");
                        return;
                    }




                    SaveSignature("save");
                });
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
                       // await CoreMethods.PopPageModel();
                        CurrentPage.Navigation.PopAsync();
                    }
                    else
                    {
                        var actionResult = await UserDialogs.Instance.ConfirmAsync(" Unsaved changes will be lost. Are you sure want to cancel?", "RAMS", "Yes", "No");
                        if (actionResult)
                      //  await CoreMethods.PopPageModel();

                        CurrentPage.Navigation.PopAsync();
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
                    if (SelectedDate == null)
                    {
                        _userDialogs.Alert("Date of Inspection is required.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedAbutmentDistress == -1)
                    {
                        _userDialogs.Alert("Foundation Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedAbutmentSeverity == -1)
                    {
                        _userDialogs.Alert("Foundation Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedBeamsDistress == -1)
                    {
                        _userDialogs.Alert("Arches - Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedBeamsSeverity == -1)
                    {
                        _userDialogs.Alert("Arches - Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedBearingDistress == -1)
                    {
                        _userDialogs.Alert("Bearing Diaphragm - Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedBearingSeverity == -1)
                    {
                        _userDialogs.Alert("Bearing Diaphragm - Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedDeckDistress == -1)
                    {
                        _userDialogs.Alert("Pavement - Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedDeckSeverity == -1)
                    {
                        _userDialogs.Alert("Pavement - Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedExpansionDistress == -1)
                    {
                        _userDialogs.Alert("Expansion Joint - Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedExpansionSeverity == -1)
                    {
                        _userDialogs.Alert("Expansion Joint - Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedParapetDistress == -1)
                    {
                        _userDialogs.Alert("Railing - Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedParapetSeverity == -1)
                    {
                        _userDialogs.Alert("Railing - Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedPiersDistress == -1)
                    {
                        _userDialogs.Alert("Connection Of Primary Components - Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedPiersSeverity == -1)
                    {
                        _userDialogs.Alert("Connection Of Primary Components - Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedSidewalksDistress == -1)
                    {
                        _userDialogs.Alert("Approaches Slab - Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedSidewalksSeverity == -1)
                    {
                        _userDialogs.Alert("Approaches Slab - Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedSlopeDistress == -1)
                    {
                        _userDialogs.Alert("Retaining Wall - Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedSlopeSeverity == -1)
                    {
                        _userDialogs.Alert("Retaining Wall - Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedSignboardDistress == -1)
                    {
                        _userDialogs.Alert("Utilities - Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedSignboardSeverity == -1)
                    {
                        _userDialogs.Alert("Utilities - Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedDrainDistress == -1)
                    {
                        _userDialogs.Alert("Drain Water Down Pipe - Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedDrainSeverity == -1)
                    {
                        _userDialogs.Alert("Drain Water Down Pipe - Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedWaterDistress == -1)
                    {
                        _userDialogs.Alert("Waterway - Distress field is required.Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedWaterSeverity == -1)
                    {
                        _userDialogs.Alert("Waterway - Severity field is required.Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedInspIndex == -1)
                    {
                        _userDialogs.Alert("Inspected by User Id field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (InspName == null || InspName == "")
                    {
                        _userDialogs.Alert("Inspected by User Name is required.", "RAMS", "Ok");
                        return;
                    }
                    if (InspDesignation == null || InspDesignation == "")
                    {
                        _userDialogs.Alert("Inspected by User Designation is required.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedBridgeConditionRating == -1 || SelectedBridgeConditionRating == null)
                    {
                        _userDialogs.Alert("Bridge Condition Rating field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedFutureInvestigation == -1 || SelectedFutureInvestigation == null)
                    {
                        _userDialogs.Alert("Further Investigation field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }



                    if (DDAbutmentDistressListItems[SelectedAbutmentDistress]?.Value == "B22" && (AbutmentOthers == null || AbutmentOthers == ""))
                    {
                        _userDialogs.Alert("Foundation Distress others is required", "RAMS", "Ok");
                        return;
                    }

                    if (DDBeamsDistressListItems[SelectedBeamsDistress]?.Value == "B22" && (BeamOthers == null || BeamOthers == ""))
                    {
                        _userDialogs.Alert("Arches - Distress Others is required", "RAMS", "Ok");
                        return;
                    }
                    if (DDBearingDistressListItems[SelectedBearingDistress]?.Value == "B22" && (BearingOthers == null || BearingOthers == ""))
                    {
                        _userDialogs.Alert("Bearing Diaphragm - Distress Others is required", "RAMS", "Ok");
                        return;
                    }

                    if (DDDeckDistressListItems[SelectedDeckDistress]?.Value == "B22" && (DeckOthers == null || DeckOthers == ""))
                    {
                        _userDialogs.Alert("Pavement - Distress Others is required", "RAMS", "Ok");
                        return;
                    }

                    if (DDExpansionDistressListItems[SelectedExpansionDistress]?.Value == "B22" && (ExpansionOthers == null || ExpansionOthers == ""))
                    {
                        _userDialogs.Alert("Expansion Joint - Distress Others is required", "RAMS", "Ok");
                        return;
                    }

                    if (DDParapetDistressListItems[SelectedParapetDistress]?.Value == "B22" && (ParapetOthers == null || ParapetOthers == ""))
                    {
                        _userDialogs.Alert("Railing - Distress Others is required", "RAMS", "Ok");
                        return;
                    }

                    if (DDPiersDistressListItems[SelectedPiersDistress]?.Value == "B22" && (PiersOthers == null || PiersOthers == ""))
                    {
                        _userDialogs.Alert("Connection Of Primary Components - Distress Others is required", "RAMS", "Ok");
                        return;
                    }

                    if (DDSidewalksDistressListItems[SelectedSidewalksDistress]?.Value == "B22" && (KerbOthers == null || KerbOthers == ""))
                    {
                        _userDialogs.Alert("Approaches Slab - Distress Others is required", "RAMS", "Ok");
                        return;
                    }

                    if (DDSlopeDistressListItems[SelectedSlopeDistress]?.Value == "B22" && (SlopeOthers == null || SlopeOthers == ""))
                    {
                        _userDialogs.Alert("Retaining Wall - Distress Others is required", "RAMS", "Ok");
                        return;
                    }

                    if (DDSignboardDistressListItems[SelectedSignboardDistress]?.Value == "B22" && (SignboardOthers == null || SignboardOthers == ""))
                    {
                        _userDialogs.Alert("Utilities - Distress Others is required", "RAMS", "Ok");
                        return;
                    }
                    if (DDDrainDistressListItems[SelectedDrainDistress]?.Value == "B22" && (DrainOthers == null || DrainOthers == ""))
                    {
                        _userDialogs.Alert("Drain Water Down Pipe - Distress Others is required", "RAMS", "Ok");
                        return;
                    }

                    if (DDWaterDistressListItems[SelectedWaterDistress]?.Value == "B22" && (WaterwayOthers == null || WaterwayOthers == ""))
                    {
                        _userDialogs.Alert("Waterway - Distress Others is required", "RAMS", "Ok");
                        return;
                    }





                    SaveSignature("submit");
                });
            }
        }
    }
}
