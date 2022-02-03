using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;
using RAMMS.DTO;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.MobileApps.Page;
using Rg.Plugins.Popup.Extensions;
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
    public class FormC1C2AddPageModel : FreshBasePageModel
    {
        //private bool _fromAdd;

        //private bool _isPhotoTabVisible;

        //public bool FromAdd
        //{
        //    get
        //    {
        //        return _fromAdd;
        //    }
        //    set
        //    {
        //        _fromAdd = value;
        //        RaisePropertyChanged("FromAdd");
        //    }
        //}
        //public override async void Init(object initData)
        //{
        //    base.Init(initData);
        //    IsPhotoTabVisible = false;



        //    if (App.ReturnType == "Add")
        //    {
        //        FromAdd = true;
        //        App.HeaderCode = 0;
        //    }

        //}
        //public bool IsPhotoTabVisible
        //{
        //    get
        //    {
        //        return _isPhotoTabVisible;
        //    }
        //    set
        //    {
        //        _isPhotoTabVisible = value;
        //        RaisePropertyChanged("IsPhotoTabVisible");
        //    }
        //}

        //public ICommand AddButtonCommand
        //{
        //    get
        //    {
        //        return new Command((obj) =>
        //        {
        //            IsPhotoTabVisible = false;
        //        });
        //    }
        //}

        //public ICommand PhotoButtonCommand
        //{
        //    get
        //    {
        //        return new Command((obj) =>
        //        {
        //            IsPhotoTabVisible = true;
        //        });
        //    }
        //}
        //public ICommand ToggleCommand
        //{
        //    get
        //    {
        //        return new Command(ToggleBarTapped);
        //    }
        //}

        //private void ToggleBarTapped(object obj)
        //{
        //    Frame layout = obj as Frame;

        //    if (layout != null)
        //    {
        //        if (layout.IsVisible)
        //        {
        //            layout.IsVisible = false;
        //        }
        //        else
        //        {
        //            layout.IsVisible = true;
        //        }
        //    }
        //    else
        //    {
        //        Image image = obj as Image;
        //        string imgsrc = (image?.Source as FileImageSource).File;
        //        if (String.Equals(imgsrc, "RoundedAddIcon.png"))
        //        {
        //            image.Source = "minusicon.png";
        //        }
        //        else
        //        {
        //            image.Source = "RoundedAddIcon.png";
        //        }
        //    }
        //}

        //public ICommand AddImage
        //{
        //    get
        //    {
        //        return new FreshCommand(async (obj) =>
        //        {

        //            await CoreMethods.PushPageModel<FormC1C2CameraPopupPageModel>();
        //        });
        //    }
        //}



        private bool _isPhotoTabVisible;
        private bool _fromAdd;
        private IRestApi _restApi;
        private IUserDialogs _userDialogs;
        private ILocalDatabase _localDatabase;
        private AssetDDLResponseDTO.DropDown _selectedRMU;
        private AssetDDLResponseDTO.DropDown _selectedRoadCode;
        private AssetDDLResponseDTO.DropDown _selectedSectionCode;
        private int _selectedDivision = -1;
        private int _selectedYear = -1;
        private int _selectedAsset = -1;
       // private int _selectedSectionCode = -1;
        private int? _selectedConditionRating = -1;
        private int? _selectedFutureInvestigation = -1;
        private int? _selectedParkingPosition = -1;
        private int? _selectedAccesibility = -1;
        private int? _selectedPotentialHazard = -1;

        private int _selectedVerIndex = -1;
        private int _selectedInspIndex = -1;

        private bool isModify;
        private bool isDelete;
        private bool isView;
        SignaturePadView InspPadView;
        SignaturePadView VerPadView;

        Grid MainGrid;
        public ObservableCollection<FormC1C2ImgRequestDTO> DetailImageList { get; set; }
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
        public bool IsVerNameEnabled { get; set; }=false;
        public bool IsInspNameEnabled { get; set; } = false;
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
        public ObservableCollection<DDListItems> DDAssetListItems { get; set; }
        public ObservableCollection<DDListItems> DDRatingListItems { get; set; }
        public ObservableCollection<DDListItems> DDInspUserListListItems { get; set; }
        public ObservableCollection<DDListItems> DDVerUserListListItems { get; set; }

        public ObservableCollection<DDListItems> DDCulvertMarkerSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDWaterwaySeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDEmbankmentSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDHeadwallInletSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDWingwallInletSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDApronInletSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDRiprapInletSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDHeadwallOutletSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDWingwallOutletSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDApronOutletSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDRiprapOutletSeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDCulvertApproachSeverityListItems { get; set; }

        public ObservableCollection<DDListItems> DDCulvertMarkerDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDWaterwayDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDEmbankmentDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDHeadwallInletDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDWingwallInletDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDApronInletDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDRiprapInletDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDHeadwallOutletDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDWingwallOutletDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDApronOutletDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDRiprapOutletDistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDCulvertApproachDistressListItems { get; set; }




        public ObservableCollection<DDListItems> DDBarrel1SeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel2SeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel3SeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel4SeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel5SeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel6SeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel7SeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel8SeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel9SeverityListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel10SeverityListItems { get; set; }



        public ObservableCollection<DDListItems> DDCulvertUnitSeverityListItems { get; set; }

        public ObservableCollection<DDListItems> DDCulvertUnitDistressListItems { get; set; }


        public ObservableCollection<DDListItems> DDBarrel1DistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel2DistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel3DistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel4DistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel5DistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel6DistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel7DistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel8DistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel9DistressListItems { get; set; }
        public ObservableCollection<DDListItems> DDBarrel10DistressListItems { get; set; }


        public ObservableCollection<DDListItems> DDCulvertUnitsDistressListIems { get; set; }
        public FormB1B2DetailRequestDTO SelecteddtlEditItem { get; set; }
        public List<String> SiteRef_multiSelect { get; set; }
       // SiteRef_multiSelect = new List<String>();


        public bool IsCulvertMarkerOthersVisible { get; set; } = false;
        public bool IsWaterwayOthersVisible { get; set; } = false;
        public bool IsEmbankmentOthersVisible { get; set; } = false;
        public bool IsHeadwallInletOthersVisible { get; set; } = false;
        public bool IsWingwallInletOthersVisible { get; set; } = false;
        public bool IsApronInletOthersVisible { get; set; } = false;
        public bool IsRiprapInletOthersVisible { get; set; } = false;
        public bool IsHeadwallOutletOthersVisible { get; set; } = false;
        public bool IsWingwallOutletOthersVisible { get; set; } = false;
        public bool IsApronOutletOthersVisible { get; set; } = false;
        public bool IsRiprapOutletOthersVisible { get; set; } = false;
        public bool IsCulvertApproachOthersVisible { get; set; } = false;
       
        
        
        public bool IsBarrel1DistressOthersVisible { get; set; } = false;
        public bool IsBarrel2DistressOthersVisible { get; set; } = false;
        public bool IsBarrel3DistressOthersVisible { get; set; } = false;
        public bool IsBarrel4DistressOthersVisible { get; set; } = false;
        public bool IsBarrel5DistressOthersVisible { get; set; } = false;
        public bool IsBarrel6DistressOthersVisible { get; set; } = false;
        public bool IsBarrel7DistressOthersVisible { get; set; } = false;
        public bool IsBarrel8DistressOthersVisible { get; set; } = false;
        public bool IsBarrel9DistressOthersVisible { get; set; } = false;
        public bool IsBarrel10DistressOthersVisible { get; set; } = false;















        private int _selectedCulvertMarkerDistress = -1;
        private int _selectedWaterwayDistress = -1;
        private int _selectedEmbankmentDistress = -1;
        private int _selectedHeadwallInletDistress = -1;
        private int _selectedWingwallInletDistress = -1;
        private int _selectedApronInletDistress = -1;
        private int _selectedRiprapInletDistress = -1;
        private int _selectedHeadwallOutletDistress = -1;
        private int _selectedWingwallOutletDistress = -1;
        private int _selectedApronOutletDistress = -1;
        private int _selectedRiprapOutletDistress = -1;
        private int _selectedCulvertApproachDistress = -1;
      
        private int _selectedBarrel1DistressDistress = -1;
        private int _selectedBarrel2DistressDistress = -1;
        private int _selectedBarrel3DistressDistress = -1;
        private int _selectedBarrel4DistressDistress = -1;
        private int _selectedBarrel5DistressDistress = -1;
        private int _selectedBarrel6DistressDistress = -1;
        private int _selectedBarrel7DistressDistress = -1;
        private int _selectedBarrel8DistressDistress = -1;
        private int _selectedBarrel9DistressDistress = -1;
        private int _selectedBarrel10DistressDistress = -1;






        public int SelectedCulvertMarkerDistress
        {
            get { return _selectedCulvertMarkerDistress; }
            set
            {
                if(value !=-1)
                {
                    _selectedCulvertMarkerDistress = value;

                    var userprp = DDCulvertMarkerDistressListItems[_selectedCulvertMarkerDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsCulvertMarkerOthersVisible = true;
                    }
                    else
                    {
                        IsCulvertMarkerOthersVisible = false;
                    }

                }
            }
        }
        public int SelectedWaterwayDistress
        {
            get { return _selectedWaterwayDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedWaterwayDistress = value;

                    var userprp = DDWaterwayDistressListItems[_selectedWaterwayDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsWaterwayOthersVisible = true;
                    }
                    else
                    {
                        IsWaterwayOthersVisible = false;
                    }

                }
            }
        }
        public int SelectedEmbankmentDistress
        {
            get { return _selectedEmbankmentDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedEmbankmentDistress = value;

                    var userprp = DDEmbankmentDistressListItems[_selectedEmbankmentDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsEmbankmentOthersVisible = true;
                    }
                    else
                    {
                        IsEmbankmentOthersVisible = false;
                    }

                }
            }
        }
        public int SelectedHeadwallInletDistress
        {
            get { return _selectedHeadwallInletDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedHeadwallInletDistress = value;

                    var userprp = DDHeadwallInletDistressListItems[_selectedHeadwallInletDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsHeadwallInletOthersVisible = true;
                    }
                    else
                    {
                        IsHeadwallInletOthersVisible = false;
                    }

                }
            }
        }
        public int SelectedWingwallInletDistress
        {
            get { return _selectedWingwallInletDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedWingwallInletDistress = value;

                    var userprp = DDWingwallInletDistressListItems[_selectedWingwallInletDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsWingwallInletOthersVisible = true;
                    }
                    else
                    {
                        IsWingwallInletOthersVisible = false;
                    }

                }
            }
        }
        public int SelectedApronInletDistress
        {
            get { return _selectedApronInletDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedApronInletDistress = value;

                    var userprp = DDApronInletDistressListItems[_selectedApronInletDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsApronInletOthersVisible = true;
                    }
                    else
                    {
                        IsApronInletOthersVisible = false;
                    }

                }
            }
        }
        public int SelectedRiprapInletDistress
        {
            get { return _selectedRiprapInletDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedRiprapInletDistress = value;

                    var userprp = DDRiprapInletDistressListItems[_selectedRiprapInletDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsRiprapInletOthersVisible = true;
                    }
                    else
                    {
                        IsRiprapInletOthersVisible = false;
                    }

                }
            }
        }
        public int SelectedHeadwallOutletDistress
        {
            get { return _selectedHeadwallOutletDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedHeadwallOutletDistress = value;

                    var userprp = DDHeadwallOutletDistressListItems[_selectedHeadwallOutletDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsHeadwallOutletOthersVisible = true;
                    }
                    else
                    {
                        IsHeadwallOutletOthersVisible = false;
                    }

                }
            }
        }
        public int SelectedWingwallOutletDistress
        {
            get { return _selectedWingwallOutletDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedWingwallOutletDistress = value;

                    var userprp = DDWingwallOutletDistressListItems[_selectedWingwallOutletDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsWingwallOutletOthersVisible = true;
                    }
                    else
                    {
                        IsWingwallOutletOthersVisible = false;
                    }

                }
            }
        }
        public int SelectedApronOutletDistress
        {
            get { return _selectedApronOutletDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedApronOutletDistress = value;

                    var userprp = DDApronOutletDistressListItems[_selectedApronOutletDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsApronOutletOthersVisible = true;
                    }
                    else
                    {
                        IsApronOutletOthersVisible = false;
                    }

                }
            }
        }
        public int SelectedRiprapOutletDistress
        {
            get { return _selectedRiprapOutletDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedRiprapOutletDistress = value;

                    var userprp = DDRiprapOutletDistressListItems[_selectedRiprapOutletDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsRiprapOutletOthersVisible = true;
                    }
                    else
                    {
                        IsRiprapOutletOthersVisible = false;
                    }

                }
            }
        }
        public int SelectedCulvertApproachDistress
        {
            get { return _selectedCulvertApproachDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedCulvertApproachDistress = value;

                    var userprp = DDCulvertApproachDistressListItems[_selectedCulvertApproachDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsCulvertApproachOthersVisible = true;
                    }
                    else
                    {
                        IsCulvertApproachOthersVisible = false;
                    }

                }
            }
        }




        public int SelectedBarrel1DistressDistress
        {
            get { return _selectedBarrel1DistressDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedBarrel1DistressDistress = value;

                    var userprp = DDBarrel1DistressListItems[_selectedBarrel1DistressDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsBarrel1DistressOthersVisible = true;
                    }
                    else
                    {
                        IsBarrel1DistressOthersVisible = false;
                    }

                }
            }
        }





        public int SelectedBarrel2DistressDistress
        {
            get { return _selectedBarrel2DistressDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedBarrel2DistressDistress = value;

                    var userprp = DDBarrel2DistressListItems[_selectedBarrel2DistressDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsBarrel2DistressOthersVisible = true;
                    }
                    else
                    {
                        IsBarrel2DistressOthersVisible = false;
                    }

                }
            }
        }




        public int SelectedBarrel3DistressDistress
        {
            get { return _selectedBarrel3DistressDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedBarrel3DistressDistress = value;

                    var userprp = DDBarrel3DistressListItems[_selectedBarrel3DistressDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsBarrel3DistressOthersVisible = true;
                    }
                    else
                    {
                        IsBarrel3DistressOthersVisible = false;
                    }

                }
            }
        }



        public int SelectedBarrel4DistressDistress
        {
            get { return _selectedBarrel4DistressDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedBarrel4DistressDistress = value;

                    var userprp = DDBarrel4DistressListItems[_selectedBarrel4DistressDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsBarrel4DistressOthersVisible = true;
                    }
                    else
                    {
                        IsBarrel4DistressOthersVisible = false;
                    }

                }
            }
        }






        public int SelectedBarrel5DistressDistress
        {
            get { return _selectedBarrel5DistressDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedBarrel5DistressDistress = value;

                    var userprp = DDBarrel5DistressListItems[_selectedBarrel5DistressDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsBarrel5DistressOthersVisible = true;
                    }
                    else
                    {
                        IsBarrel5DistressOthersVisible = false;
                    }

                }
            }
        }




        public int SelectedBarrel6DistressDistress
        {
            get { return _selectedBarrel6DistressDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedBarrel6DistressDistress = value;

                    var userprp = DDBarrel6DistressListItems[_selectedBarrel6DistressDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsBarrel6DistressOthersVisible = true;
                    }
                    else
                    {
                        IsBarrel6DistressOthersVisible = false;
                    }

                }
            }
        }


        public int SelectedBarrel7DistressDistress
        {
            get { return _selectedBarrel7DistressDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedBarrel7DistressDistress = value;

                    var userprp = DDBarrel7DistressListItems[_selectedBarrel7DistressDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsBarrel7DistressOthersVisible = true;
                    }
                    else
                    {
                        IsBarrel7DistressOthersVisible = false;
                    }

                }
            }
        }


        public int SelectedBarrel8DistressDistress
        {
            get { return _selectedBarrel8DistressDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedBarrel8DistressDistress = value;

                    var userprp = DDBarrel8DistressListItems[_selectedBarrel8DistressDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsBarrel8DistressOthersVisible = true;
                    }
                    else
                    {
                        IsBarrel8DistressOthersVisible = false;
                    }

                }
            }
        }





        public int SelectedBarrel9DistressDistress
        {
            get { return _selectedBarrel9DistressDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedBarrel9DistressDistress = value;

                    var userprp = DDBarrel9DistressListItems[_selectedBarrel9DistressDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsBarrel9DistressOthersVisible = true;
                    }
                    else
                    {
                        IsBarrel9DistressOthersVisible = false;
                    }

                }
            }
        }



        public int SelectedBarrel10DistressDistress
        {
            get { return _selectedBarrel10DistressDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedBarrel10DistressDistress = value;

                    var userprp = DDBarrel10DistressListItems[_selectedBarrel10DistressDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsBarrel10DistressOthersVisible = true;
                    }
                    else
                    {
                        IsBarrel10DistressOthersVisible = false;
                    }

                }
            }
        }


        public int SelectedBarrel1SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel2SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel3SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel4SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel5SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel6SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel7SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel8SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel9SeveritySeverity { get; set; } = -1;
        public int SelectedBarrel10SeveritySeverity { get; set; } = -1;






        public int SelectedCulvertMarkerSeverity { get; set; } = -1;
        public int SelectedWaterwaySeverity { get; set; } = -1;
        public int SelectedEmbankmentSeverity { get; set; } = -1;
        public int SelectedHeadwallInletSeverity { get; set; } = -1;
        public int SelectedWingwallInletSeverity { get; set; } = -1;
        public int SelectedApronInletSeverity { get; set; } = -1;
        public int SelectedRiprapInletSeverity { get; set; } = -1;
        public int SelectedHeadwallOutletSeverity { get; set; } = -1;
        public int SelectedWingwallOutletSeverity { get; set; } = -1;
        public int SelectedApronOutletSeverity { get; set; } = -1;
        public int SelectedRiprapOutletSeverity { get; set; } = -1;
        public int SelectedCulvertApproachSeverity { get; set; } = -1;
        public ObservableCollection<CulvertListData> CulvertUnitItemsSource { get; set; }

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
                    var selectedItem = Convert.ToInt32(DDVerUserListListItems?[_selectedVerIndex]?.Value.ToString());

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
            await GetAssetList();
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

                    GetAssetList();
                    SetRefNumber();
                }
                RaisePropertyChanged();
            }
        }

        private void SetRefNumber()
        {
            if (SelectedRoadCode != null && SelectedYear > 0)
                SelectedRefNo = "CI/Form C1/C2/" + DDAssetListItems[SelectedAsset].Text+ "/" + DDYearListItems[SelectedYear].Value;
        }

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
                    GetAssetList();
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
                SetRefNumber();
                RaisePropertyChanged();

                if (SelectedYear != -1)
                {
                    var year = DDYearListItems[SelectedYear].Text;
                    MinimumDate = Convert.ToDateTime("1-1-" + year);
                    MaximumDate = Convert.ToDateTime("12-31-" + year);
                    SelectedDate = null;
                }
            }
        }
        public int SelectedAsset
        {
            get => _selectedAsset;
            set
            {
                _selectedAsset = value;
                RaisePropertyChanged();
            }
        }

        public int? SelectedConditionRating
        {
            get => _selectedConditionRating;
            set
            {
                _selectedConditionRating = value;
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

        public int? SelectedParkingPosition
        {
            get => _selectedParkingPosition;
            set
            {
                _selectedParkingPosition = value;
                RaisePropertyChanged();
            }
        }
        public int? SelectedAccesibility
        {
            get => _selectedAccesibility;
            set
            {
                _selectedAccesibility = value;
                RaisePropertyChanged();
            }
        }
        public int? SelectedPotentialHazard
        {
            get => _selectedPotentialHazard;
            set
            {
                _selectedPotentialHazard = value;
                RaisePropertyChanged();
            }
        }

        #region


        public int PkRefNo { get; set; }
        public int? AiPkRefNo { get; set; }
        public string AiAssetId { get; set; }
        public string AiDivCode { get; set; }
        public string AiRmuName { get; set; }
        public string AiRdCode { get; set; }
        public string AiRdName { get; set; }
        public int? AiLocChKm { get; set; }
        public string AiLocChM { get; set; }
        public double? AiFinRdLevel { get; set; }
        public string AiStrucCode { get; set; }
        public double? AiCatchArea { get; set; }
        public double? AiSkew { get; set; }
        public double? AiDesignFlow { get; set; }
        public double? AiLength { get; set; }
        public string AiPrecastSitu { get; set; }
        public string AiGrpType { get; set; }
        public int? AiBarrelNo { get; set; }
        public double? AiGpsEasting { get; set; }
        public double? AiGpsNorthing { get; set; }
        public string AiMaterial { get; set; }
        public double? AiIntelLevel { get; set; }
        public double? AiOutletLevel { get; set; }
        public string AiIntelStruc { get; set; }
        public string AiOutletStruc { get; set; }
        public string CInspRefNo { get; set; }
        public int? YearOfInsp { get; set; }
        public DateTime? DtOfInsp { get; set; }
        public int? RecordNo { get; set; }
        public bool? PrkPosition { get; set; }
        public bool? Accessibility { get; set; }
        public bool? PotentialHazards { get; set; }
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
        public int? CulvertConditionRat { get; set; }
        public bool? ReqFurtherInv { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }
        public FormB1B2DetailRequestDTO Detail { get; set; }
        public bool IsView { get; set; } = false;
        public string RmuCode { get; set; }
        public string SectionCode { get; set; }
        public string SectionName { get; set; }
        public string Status { get; set; }
        public string DisplayAssetId { get; set; }

        #endregion

        public FormC1C2AddPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;
            _localDatabase = localDatabase;
        }
        public override void Init(object initData)
        {
            base.Init(initData);

            DDCulvertUnitsDistressListIems = new ObservableCollection<DDListItems>();

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
            await GetDDLDescValueContact("C1C2_Severity");
            await GetDDLDescValueContact("C1C2_Culvert Marker");
            await GetDDLDescValueContact("C1C2_Waterways");
            await GetDDLDescValueContact("C1C2_Walls and Aprons");
            await GetDDLDescValueContact("C1C2_Culvert Approach Condition");
            await GetDDLDescValueContact("C1C2_Culvert Units");


            await GetAssetList();
            await GetUserList();
            CanSave = App.ReturnType == "Edit" ? true : false;
           
            if (App.ReturnType == "Edit" || App.ReturnType == "View")
            {


                //if (SelecteddtlEditItem.SiteRef_multiSelect.Count > 0)
                //{
                //    string s = "";

                //    int index = 0;
                //    SelectedLocationList = SelecteddtlEditItem.SiteRef_multiSelect;
                //    foreach (var model in SelectedLocationList)
                //    {
                //        s = s + model;
                //        if (index < SelectedLocationList.Count - 1)
                //        {
                //            s = s + ",";
                //        }
                //        index++;
                //        DDLocationListItems.FirstOrDefault(x => x.Value == model).IsChecked = true;
                //    }
                //    btnlocation.Text = s;

                //}
                //else
                //{
                //    btnlocation.Text = "Select Location";
                //}








                IsHeaderEnable = false;
                IsView = App.ReturnType == "View" ? true : false;
                await GetC1C2ById(App.HeaderCode);
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
                        var hdrresponse = await _restApi.GetC1C2Images(App.HeaderCode);

                        if (hdrresponse.success == true)
                        {
                            var list = hdrresponse.data;
                            DetailImageList = new ObservableCollection<FormC1C2ImgRequestDTO>(hdrresponse.data);

                            int i = 0;
                            foreach (var listdata in DetailImageList)
                            {
                                listdata.ImageSrno = i + 1;
                                i++;
                            }
                        }
                    }
                    catch (Exception ex) 
                    {
                        _userDialogs.Alert(ex.Message);
                    }
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
                                var imageID = (obj as FormC1C2ImgRequestDTO).PkRefNo;
                                var response = await _restApi.DeleteC1C2Image( App.HeaderCode ,imageID);

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
                    await CoreMethods.PushPageModel<FormC1C2CameraPopupPageModel>();
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



                        if (SelectedRoadCode == null)
                        {
                            await UserDialogs.Instance.AlertAsync("Please select Road Code", "RAMS", "OK");
                            return;
                        }

                        if (SelectedAsset == -1)
                        {
                            await UserDialogs.Instance.AlertAsync("Please select Asset ID", "RAMS", "OK");
                            return;
                        }

                        if (SelectedYear == -1)
                        {
                            await UserDialogs.Instance.AlertAsync("Please select Year Of Inspection", "RAMS", "OK");
                            return;
                        }

                        

                        var response = SaveFormC1C2Header();

                        FromAdd = false;
                    }
                    catch
                    {
                    }


                });
            }
        }

        private async Task<ObservableCollection<FormC1C2HeaderRequestDTO>> SaveFormC1C2Header()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    FormC1C2HeaderRequestDTO GridItems = new FormC1C2HeaderRequestDTO()
                    {
                        PkRefNo = App.HeaderCode,
                        AiPkRefNo = Convert.ToInt32(DDAssetListItems[SelectedAsset]?.Value),
                        AiAssetId = DDAssetListItems[SelectedAsset]?.Text,
                        //AiDivCode = DDDivisionListItems[SelectedDivision]?.Value ?? null,
                        AiDivCode = SelectedDivision == -1 ? null : DDDivisionListItems[SelectedDivision]?.Value,
                        AiRmuName = SelectedRMU?.Text,
                        //SectionCode = SelectedSectionCode?.Code,
                        //SectionName = SelectedSectionName,
                        AiRdCode = SelectedRoadCode?.Value,
                        AiRdName = SelectedRoadName,
                        YearOfInsp = Convert.ToInt32(DDYearListItems[SelectedYear]?.Value),
                        DtOfInsp = SelectedDate.HasValue ? SelectedDate.Value : (DateTime?)null
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

                    var response = await _restApi.SaveC1C2Hdr(GridItems);

                    if (response.success)
                    {
                        IsHeaderEnable = false;
                        //CanSave = true;
                        if (response.data.SubmitSts)
                            CanSave = false;
                        else
                            CanSave = true;
                        App.HeaderCode = response.data.PkRefNo;
                        SetViewData(response);
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

        private async Task<int> GetC1C2ById(int headerCode)
        {
            try
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.GetC1C2ById(headerCode);

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

        private void SetViewData(ResponseBaseObject<FormC1C2HeaderRequestDTO> response)
        {
            SelectedRefNo = response.data.CInspRefNo;
            App.InspReferenceNo = response.data.CInspRefNo;
            AiAssetId = response.data.AiAssetId;
            YearOfInsp = response.data.YearOfInsp;
            SelectedYear = DDYearListItems.ToList().FindIndex(x => x.Value == response.data.YearOfInsp.ToString());
            SelectedDate = response.data.DtOfInsp.HasValue ? response.data.DtOfInsp.Value : (DateTime?)null;
            AiDivCode = response.data.AiDivCode;
            AiRmuName = response.data.AiRmuName;
            AiRdCode = response.data.AiRdCode;
            AiRdName = response.data.AiRdName;
            AiStrucCode = response.data.AiStrucCode;
            AiLocChKm = response.data.AiLocChKm;
            AiLocChM = response.data.AiLocChM;
            AiLength = response.data.AiLength;
            AiMaterial = response.data.AiMaterial;
            AiFinRdLevel = response.data.AiFinRdLevel;
            AiCatchArea = response.data.AiCatchArea;
            AiSkew = response.data.AiSkew;
            AiGrpType = response.data.AiGrpType;
            AiDesignFlow = response.data.AiDesignFlow;
            AiPrecastSitu = response.data.AiPrecastSitu;
            AiBarrelNo = response.data.AiBarrelNo;
            AiIntelLevel = response.data.AiIntelLevel;
            AiOutletLevel = response.data.AiOutletLevel;
            AiGpsEasting = response.data.AiGpsEasting;
            AiGpsNorthing = response.data.AiGpsNorthing;
            AiIntelStruc = response.data.AiIntelStruc;
            AiOutletStruc = response.data.AiOutletStruc;

            SelectedCulvertMarkerDistress = DDCulvertMarkerDistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[0]?.Distress);
            SelectedCulvertMarkerSeverity = DDCulvertMarkerSeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[0]?.Severity?.ToString());

            SelectedWaterwayDistress = DDWaterwayDistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[1]?.Distress);
            SelectedWaterwaySeverity = DDWaterwaySeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[1]?.Severity?.ToString());

            SelectedEmbankmentDistress = DDEmbankmentDistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[2]?.Distress);
            SelectedEmbankmentSeverity = DDEmbankmentSeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[2]?.Severity?.ToString());

            SelectedHeadwallInletDistress = DDHeadwallInletDistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[3]?.Distress);
            SelectedHeadwallInletSeverity = DDHeadwallInletSeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[3]?.Severity?.ToString());

            SelectedWingwallInletDistress = DDWingwallInletDistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[4]?.Distress);
            SelectedWingwallInletSeverity = DDWingwallInletSeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[4]?.Severity?.ToString());

            SelectedApronInletDistress = DDApronInletDistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[5]?.Distress);
            SelectedApronInletSeverity = DDApronInletSeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[5]?.Severity?.ToString());

            SelectedRiprapInletDistress = DDRiprapInletDistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[6]?.Distress);
            SelectedRiprapInletSeverity = DDRiprapInletSeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[6]?.Severity?.ToString());

            SelectedHeadwallOutletDistress = DDHeadwallOutletDistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[7]?.Distress);
            SelectedHeadwallOutletSeverity = DDHeadwallOutletSeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[7]?.Severity?.ToString());

            SelectedWingwallOutletDistress = DDWingwallOutletDistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[8]?.Distress);
            SelectedWingwallOutletSeverity = DDWingwallOutletSeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[8]?.Severity?.ToString());

            SelectedApronOutletDistress = DDApronOutletDistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[9]?.Distress);
            SelectedApronOutletSeverity = DDApronOutletSeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[9]?.Severity?.ToString());

            SelectedRiprapOutletDistress = DDRiprapOutletDistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[10]?.Distress);
            SelectedRiprapOutletSeverity = DDRiprapOutletSeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[10]?.Severity?.ToString());

            SelectedCulvertApproachDistress = DDCulvertApproachDistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[response.data.InsDtl.Count -1]?.Distress);
            SelectedCulvertApproachSeverity = DDCulvertApproachSeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[response.data.InsDtl.Count -1]?.Severity?.ToString());



            CulvertUnitItemsSource = new ObservableCollection<CulvertListData>();

            var totalCount = response.data.InsDtl.Count - 1;

            for(int i = 11; i<totalCount; i++)
            {
                CulvertUnitItemsSource.Add(new CulvertListData() { CulvertName = response.data.InsDtl[i].InspCodeDesc, CulvertUnitDistressList = DDCulvertUnitDistressListItems, SelectedCulvertUnitDistress = DDCulvertUnitDistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[i]?.Distress), CulvertUnitSeverityList = DDCulvertUnitSeverityListItems, SelectedCulvertUnitSeverity = response.data.InsDtl[i]?.Severity == null ? -1 : (int)response.data.InsDtl[i]?.Severity });
            }

            //SelectedBarrel1DistressDistress = DDBarrel1DistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[0]?.Distress);
            //SelectedBarrel1SeveritySeverity = DDBarrel1SeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[0]?.Severity?.ToString());


            //SelectedBarrel2DistressDistress = DDBarrel2DistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[1]?.Distress);
            //SelectedBarrel2SeveritySeverity = DDBarrel2SeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[1]?.Severity?.ToString());

            //SelectedBarrel3DistressDistress = DDBarrel3DistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[2]?.Distress);
            //SelectedBarrel3SeveritySeverity = DDBarrel3SeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[2]?.Severity?.ToString());

            //SelectedBarrel4DistressDistress = DDBarrel4DistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[3]?.Distress);
            //SelectedBarrel4SeveritySeverity = DDBarrel4SeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[3]?.Severity?.ToString());

            //SelectedBarrel5DistressDistress = DDBarrel5DistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[4]?.Distress);
            //SelectedBarrel5SeveritySeverity = DDBarrel5SeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[4]?.Severity?.ToString());

            //SelectedBarrel6DistressDistress = DDBarrel6DistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[5]?.Distress);
            //SelectedBarrel6SeveritySeverity = DDBarrel6SeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[5]?.Severity?.ToString());

            //SelectedBarrel7DistressDistress = DDBarrel7DistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[6]?.Distress);
            //SelectedBarrel7SeveritySeverity = DDBarrel7SeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[6]?.Severity?.ToString());

            //SelectedBarrel8DistressDistress = DDBarrel8DistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[7]?.Distress);
            //SelectedBarrel8SeveritySeverity = DDBarrel8SeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[7]?.Severity?.ToString());

            //SelectedBarrel9DistressDistress = DDBarrel9DistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[8]?.Distress);
            //SelectedBarrel9SeveritySeverity = DDBarrel9SeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[8]?.Severity?.ToString());

            //SelectedBarrel10DistressDistress = DDBarrel10DistressListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[9]?.Distress);
            //SelectedBarrel10SeveritySeverity = DDBarrel10SeverityListItems.ToList().FindIndex(x => x.Value == response.data.InsDtl[9]?.Severity?.ToString());














            SelectedConditionRating = response.data.CulvertConditionRat - 1;
            SelectedFutureInvestigation = response.data.ReqFurtherInv != null ? (response.data.ReqFurtherInv == true ? 0 : 1) : (int?)null;
            SelectedParkingPosition = response.data.PrkPosition != null ? (response.data.PrkPosition == true ? 0 : 1) : (int?)null;
            SelectedAccesibility = response.data.Accessibility != null ? (response.data.Accessibility == true ? 0 : 1) : (int?)null;
            SelectedPotentialHazard = response.data.PotentialHazards != null ? (response.data.PotentialHazards == true ? 0 : 1) : (int?)null;

            SerProviderDefObs = response.data.SerProviderDefObs;
            AuthDefObs = response.data.AuthDefObs;
            SerProviderDefGenCom = response.data.SerProviderDefGenCom;
            AuthDefGenCom = response.data.AuthDefGenCom;
            SerProviderDefFeedback = response.data.SerProviderDefFeedback;
            AuthDefFeedback = response.data.AuthDefFeedback;

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
                        GrpCode = "CV"
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
            return 1;
        }

        private async Task<int> GetAssetList()
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
                    var response = await _restApi.GetDDAssetList(ddlist);

                    if (response.success)
                    {
                        DDAssetListItems = new ObservableCollection<DDListItems>(response.data);
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
                            DDYearListItems = new ObservableCollection<DDListItems>(response.data);
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
                        if (ddtype == "C1C2_Severity")
                        {
                            response.data = response.data.OrderBy(x => Convert.ToInt32(x.Value)).ToList();

                            DDCulvertUnitSeverityListItems = new ObservableCollection<DDListItems>(response.data);

                            DDCulvertMarkerSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDWaterwaySeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDEmbankmentSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDHeadwallInletSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDWingwallInletSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDApronInletSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDRiprapInletSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDHeadwallOutletSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDWingwallOutletSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDApronOutletSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDRiprapOutletSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                            DDCulvertApproachSeverityListItems = new ObservableCollection<DDListItems>(response.data);
                       
                            }
                        else if (ddtype == "C1C2_Culvert Marker")
                        {

                            DDCulvertMarkerDistressListItems = new ObservableCollection<DDListItems>(response.data);
                        }
                        else if(ddtype == "C1C2_Waterways")
                        {
                            DDWaterwayDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDEmbankmentDistressListItems = new ObservableCollection<DDListItems>(response.data);
                        }
                        else if (ddtype == "C1C2_Walls and Aprons")
                        {
                            DDHeadwallInletDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDWingwallInletDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDApronInletDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDRiprapInletDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDHeadwallOutletDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDWingwallOutletDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDApronOutletDistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDRiprapOutletDistressListItems = new ObservableCollection<DDListItems>(response.data);
                        }
                        else if (ddtype == "C1C2_Culvert Approach Condition")
                        {
                            DDCulvertApproachDistressListItems = new ObservableCollection<DDListItems>(response.data);
                        }


                        else if (ddtype == "C1C2_Culvert Units")
                        {
                            DDCulvertUnitDistressListItems = new ObservableCollection<DDListItems>(response.data);

                            DDBarrel1DistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDBarrel2DistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDBarrel3DistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDBarrel4DistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDBarrel5DistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDBarrel6DistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDBarrel7DistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDBarrel8DistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDBarrel9DistressListItems = new ObservableCollection<DDListItems>(response.data);
                            DDBarrel10DistressListItems = new ObservableCollection<DDListItems>(response.data);

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
                                if (userprp.ToLower() == "others")
                                {
                                    IsInspNameEnabled = true;
                                }
                                else
                                {
                                    IsInspNameEnabled = false;
                                }

                                InspName = response.data.UserName;
                                InspDesignation = response.data.Position;
                            }
                            else if (usertype == "veruser")
                            {
                                var userprp = DDVerUserListListItems[SelectedVerIndex].Text.Split('-')[1];
                                if (userprp.ToLower() == "others")
                                {
                                    IsVerNameEnabled = true;
                                }
                                else
                                {
                                    IsVerNameEnabled = false;
                                }

                                VerName = response.data.UserName;
                                VerDesignation = response.data.Position;
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
                        var HeaderItemResponse = await _restApi.GetC1C2ById(App.HeaderCode);

                        if (HeaderItemResponse.success)
                        {
                            HeaderItemResponse.data.PkRefNo = App.HeaderCode;
                            HeaderItemResponse.data.DtOfInsp = SelectedDate;

                            HeaderItemResponse.data.InsDtl[0].Distress = SelectedCulvertMarkerDistress.ToString() == "-1" ? null : DDCulvertMarkerDistressListItems[SelectedCulvertMarkerDistress]?.Value;
                            HeaderItemResponse.data.InsDtl[1].Distress = SelectedWaterwayDistress.ToString() == "-1" ? null : DDWaterwayDistressListItems[SelectedWaterwayDistress]?.Value;
                            HeaderItemResponse.data.InsDtl[2].Distress = SelectedEmbankmentDistress.ToString() == "-1" ? null : DDEmbankmentDistressListItems[SelectedEmbankmentDistress]?.Value;
                            HeaderItemResponse.data.InsDtl[3].Distress = SelectedHeadwallInletDistress.ToString() == "-1" ? null : DDHeadwallInletDistressListItems[SelectedHeadwallInletDistress]?.Value;
                            HeaderItemResponse.data.InsDtl[4].Distress = SelectedWingwallInletDistress.ToString() == "-1" ? null : DDWingwallInletDistressListItems[SelectedWingwallInletDistress]?.Value;
                            HeaderItemResponse.data.InsDtl[5].Distress = SelectedApronInletDistress.ToString() == "-1" ? null : DDApronInletDistressListItems[SelectedApronInletDistress]?.Value;
                            HeaderItemResponse.data.InsDtl[6].Distress = SelectedRiprapInletDistress.ToString() == "-1" ? null : DDRiprapInletDistressListItems[SelectedRiprapInletDistress]?.Value;
                            HeaderItemResponse.data.InsDtl[7].Distress = SelectedHeadwallOutletDistress.ToString() == "-1" ? null : DDHeadwallOutletDistressListItems[SelectedHeadwallOutletDistress]?.Value;
                            HeaderItemResponse.data.InsDtl[8].Distress = SelectedWingwallOutletDistress.ToString() == "-1" ? null : DDWingwallOutletDistressListItems[SelectedWingwallOutletDistress]?.Value;
                            HeaderItemResponse.data.InsDtl[9].Distress = SelectedApronOutletDistress.ToString() == "-1" ? null : DDApronOutletDistressListItems[SelectedApronOutletDistress]?.Value;
                            HeaderItemResponse.data.InsDtl[10].Distress = SelectedRiprapOutletDistress.ToString() == "-1" ? null : DDRiprapOutletDistressListItems[SelectedRiprapOutletDistress]?.Value;
                          if(SelectedCulvertApproachDistress != -1)
                            HeaderItemResponse.data.InsDtl[HeaderItemResponse.data.InsDtl.Count].Distress = SelectedCulvertApproachDistress.ToString() == "-1" ? null : DDCulvertApproachDistressListItems[SelectedCulvertApproachDistress]?.Value;


                            for (int i = 11; i < HeaderItemResponse.data.InsDtl.Count; i++)
                            {
                                int j = 0;

                                HeaderItemResponse.data.InsDtl[i].Distress = CulvertUnitItemsSource[j].SelectedCulvertUnitDistress.ToString() == "-1" ? null : CulvertUnitItemsSource[j].CulvertUnitDistressList[CulvertUnitItemsSource[j].SelectedCulvertUnitDistress]?.Value;
                                HeaderItemResponse.data.InsDtl[i].Severity = CulvertUnitItemsSource[j].SelectedCulvertUnitSeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(CulvertUnitItemsSource[j].CulvertUnitSeverityList[CulvertUnitItemsSource[j].SelectedCulvertUnitSeverity]?.Value);
                                j++;
                            }


                            //HeaderItemResponse.data.InsDtl[0].Distress = SelectedBarrel1DistressDistress.ToString() == "-1" ? null : DDBarrel1DistressListItems[SelectedBarrel1DistressDistress]?.Value;
                            //HeaderItemResponse.data.InsDtl[1].Distress = SelectedBarrel2DistressDistress.ToString() == "-1" ? null : DDBarrel2DistressListItems[SelectedBarrel2DistressDistress]?.Value;
                            //HeaderItemResponse.data.InsDtl[2].Distress = SelectedBarrel3DistressDistress.ToString() == "-1" ? null : DDBarrel3DistressListItems[SelectedBarrel3DistressDistress]?.Value;
                            //HeaderItemResponse.data.InsDtl[3].Distress = SelectedBarrel4DistressDistress.ToString() == "-1" ? null : DDBarrel4DistressListItems[SelectedBarrel4DistressDistress]?.Value;
                            //HeaderItemResponse.data.InsDtl[4].Distress = SelectedBarrel5DistressDistress.ToString() == "-1" ? null : DDBarrel5DistressListItems[SelectedBarrel5DistressDistress]?.Value;
                            //HeaderItemResponse.data.InsDtl[5].Distress = SelectedBarrel6DistressDistress.ToString() == "-1" ? null : DDBarrel6DistressListItems[SelectedBarrel6DistressDistress]?.Value;
                            //HeaderItemResponse.data.InsDtl[6].Distress = SelectedBarrel7DistressDistress.ToString() == "-1" ? null : DDBarrel7DistressListItems[SelectedBarrel7DistressDistress]?.Value;
                            //HeaderItemResponse.data.InsDtl[7].Distress = SelectedBarrel8DistressDistress.ToString() == "-1" ? null : DDBarrel8DistressListItems[SelectedBarrel8DistressDistress]?.Value;
                            //HeaderItemResponse.data.InsDtl[8].Distress = SelectedBarrel9DistressDistress.ToString() == "-1" ? null : DDBarrel9DistressListItems[SelectedBarrel9DistressDistress]?.Value;
                            //HeaderItemResponse.data.InsDtl[9].Distress = SelectedBarrel10DistressDistress.ToString() == "-1" ? null : DDBarrel10DistressListItems[SelectedBarrel10DistressDistress]?.Value;






                            //HeaderItemResponse.data.InsDtl[0].Severity = SelectedBarrel1SeveritySeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDBarrel1SeverityListItems[SelectedBarrel1SeveritySeverity]?.Value);
                            //HeaderItemResponse.data.InsDtl[1].Severity = SelectedBarrel2SeveritySeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDBarrel2SeverityListItems[SelectedBarrel2SeveritySeverity]?.Value);
                            //HeaderItemResponse.data.InsDtl[2].Severity = SelectedBarrel3SeveritySeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDBarrel3SeverityListItems[SelectedBarrel3SeveritySeverity]?.Value);
                            //HeaderItemResponse.data.InsDtl[3].Severity = SelectedBarrel4SeveritySeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDBarrel4SeverityListItems[SelectedBarrel4SeveritySeverity]?.Value);
                            //HeaderItemResponse.data.InsDtl[4].Severity = SelectedBarrel5SeveritySeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDBarrel5SeverityListItems[SelectedBarrel5SeveritySeverity]?.Value);
                            //HeaderItemResponse.data.InsDtl[5].Severity = SelectedBarrel6SeveritySeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDBarrel6SeverityListItems[SelectedBarrel6SeveritySeverity]?.Value);
                            //HeaderItemResponse.data.InsDtl[6].Severity = SelectedBarrel7SeveritySeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDBarrel7SeverityListItems[SelectedBarrel7SeveritySeverity]?.Value);
                            //HeaderItemResponse.data.InsDtl[7].Severity = SelectedBarrel8SeveritySeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDBarrel8SeverityListItems[SelectedBarrel8SeveritySeverity]?.Value);
                            //HeaderItemResponse.data.InsDtl[8].Severity = SelectedBarrel9SeveritySeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDBarrel9SeverityListItems[SelectedBarrel9SeveritySeverity]?.Value);
                            //HeaderItemResponse.data.InsDtl[9].Severity = SelectedBarrel10SeveritySeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDBarrel10SeverityListItems[SelectedBarrel10SeveritySeverity]?.Value);






                            HeaderItemResponse.data.InsDtl[0].Severity = SelectedCulvertMarkerSeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDCulvertMarkerSeverityListItems[SelectedCulvertMarkerSeverity]?.Value);
                            HeaderItemResponse.data.InsDtl[1].Severity = SelectedWaterwaySeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDWaterwaySeverityListItems[SelectedWaterwaySeverity]?.Value);
                            HeaderItemResponse.data.InsDtl[2].Severity = SelectedEmbankmentSeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDEmbankmentSeverityListItems[SelectedEmbankmentSeverity]?.Value);
                            HeaderItemResponse.data.InsDtl[3].Severity = SelectedHeadwallInletSeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDHeadwallInletSeverityListItems[SelectedHeadwallInletSeverity]?.Value);
                            HeaderItemResponse.data.InsDtl[4].Severity = SelectedWingwallInletSeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDWingwallInletSeverityListItems[SelectedWingwallInletSeverity]?.Value);
                            HeaderItemResponse.data.InsDtl[5].Severity = SelectedApronInletSeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDApronInletSeverityListItems[SelectedApronInletSeverity]?.Value);
                            HeaderItemResponse.data.InsDtl[6].Severity = SelectedRiprapInletSeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDRiprapInletSeverityListItems[SelectedRiprapInletSeverity]?.Value);
                            HeaderItemResponse.data.InsDtl[7].Severity = SelectedHeadwallOutletSeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDHeadwallOutletSeverityListItems[SelectedHeadwallOutletSeverity]?.Value);
                            HeaderItemResponse.data.InsDtl[8].Severity = SelectedWingwallOutletSeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDWingwallOutletSeverityListItems[SelectedWingwallOutletSeverity]?.Value);
                            HeaderItemResponse.data.InsDtl[9].Severity = SelectedApronOutletSeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDApronOutletSeverityListItems[SelectedApronOutletSeverity]?.Value);
                            HeaderItemResponse.data.InsDtl[10].Severity = SelectedRiprapOutletSeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDRiprapOutletSeverityListItems[SelectedRiprapOutletSeverity]?.Value);
                            HeaderItemResponse.data.InsDtl[11].Severity = SelectedCulvertApproachSeverity.ToString() == "-1" ? null : (int?)Convert.ToInt32(DDCulvertApproachSeverityListItems[SelectedCulvertApproachSeverity]?.Value);

                            HeaderItemResponse.data.SerProviderUserId = SelectedInspIndex != -1 ? Convert.ToInt32(DDInspUserListListItems[SelectedInspIndex]?.Value) : (int?)null;
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

                            if (SelectedConditionRating != null && SelectedConditionRating != -1)
                                HeaderItemResponse.data.CulvertConditionRat = SelectedConditionRating + 1;
                            if (SelectedFutureInvestigation != null && SelectedFutureInvestigation != -1)
                                HeaderItemResponse.data.ReqFurtherInv = SelectedFutureInvestigation == 0 ? true : false;

                            if (SelectedParkingPosition != null && SelectedParkingPosition != -1)
                                HeaderItemResponse.data.PrkPosition = SelectedParkingPosition == 0 ? true : false;
                            if (SelectedAccesibility != null && SelectedAccesibility != -1)
                                HeaderItemResponse.data.Accessibility = SelectedAccesibility == 0 ? true : false;
                            if (SelectedPotentialHazard != null && SelectedPotentialHazard != -1)
                                HeaderItemResponse.data.PotentialHazards = SelectedPotentialHazard == 0 ? true : false;

                            var response = await _restApi.UpdateC1C2(HeaderItemResponse.data);
                            if (response.success)
                            {
                                if (type == "save")
                                    await UserDialogs.Instance.AlertAsync("Data Saved Successfully.", "RAMS", "OK");
                                else
                                    await UserDialogs.Instance.AlertAsync("Data Submitted Successfully.", "RAMS", "OK");

                                await CoreMethods.PopPageModel();
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

                    if (SelectedConditionRating == -1 || SelectedConditionRating == null)
                    {
                        _userDialogs.Alert("Overall Condition Rating field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedFutureInvestigation == -1 || SelectedFutureInvestigation == null)
                    {
                        _userDialogs.Alert("Further Investigation field is required. Please choose from the dropdown list.", "RAMS", "Ok");
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

                    if (SelectedParkingPosition == -1 || SelectedParkingPosition == null)
                    {
                        _userDialogs.Alert("Parking Position field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedAccesibility == -1 || SelectedAccesibility == null)
                    {
                        _userDialogs.Alert("Accesibility field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedPotentialHazard == -1 || SelectedPotentialHazard == null)
                    {
                        _userDialogs.Alert("PotentialHazard field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedDate == null)
                    {
                        _userDialogs.Alert("Date of Inspection is required.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedCulvertMarkerDistress == -1 || SelectedCulvertMarkerDistress == null)
                    {
                        _userDialogs.Alert("Culver Marker (1A) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedCulvertMarkerSeverity == -1 || SelectedCulvertMarkerSeverity == null)
                    {
                        _userDialogs.Alert("Culver Marker (1A) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }



                    if (SelectedWaterwayDistress == -1 || SelectedWaterwayDistress == null)
                    {
                        _userDialogs.Alert("Waterway (2A) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedWaterwaySeverity == -1 || SelectedWaterwaySeverity == null)
                    {
                        _userDialogs.Alert("Waterway (2A) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }


                    if (SelectedEmbankmentDistress == -1 || SelectedEmbankmentDistress == null)
                    {
                        _userDialogs.Alert("Embankment (Revetment) (2B) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedEmbankmentSeverity == -1 || SelectedEmbankmentSeverity == null)
                    {
                        _userDialogs.Alert("Embankment (Revetment) (2B) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }


                    if (SelectedHeadwallInletDistress == -1 || SelectedHeadwallInletDistress == null)
                    {
                        _userDialogs.Alert("Headwall (Inlet) (3A) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedHeadwallInletSeverity == -1 || SelectedHeadwallInletSeverity == null)
                    {
                        _userDialogs.Alert("Headwall (Inlet) (3A) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedWingwallInletDistress == -1 || SelectedWingwallInletDistress == null)
                    {
                        _userDialogs.Alert("Wingwall (Inlet) (3B) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedWingwallInletSeverity == -1 || SelectedWingwallInletSeverity == null)
                    {
                        _userDialogs.Alert("Wingwall (Inlet) (3B) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }



                    if (SelectedApronInletDistress == -1 || SelectedApronInletDistress == null)
                    {
                        _userDialogs.Alert("Apron (Inlet) (3C) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedApronInletSeverity == -1 || SelectedApronInletSeverity == null)
                    {
                        _userDialogs.Alert("Apron (Inlet) (3C) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }



                    if (SelectedRiprapInletDistress == -1 || SelectedRiprapInletDistress == null)
                    {
                        _userDialogs.Alert("Riprap (Inlet) (3D) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedRiprapInletSeverity == -1 || SelectedRiprapInletSeverity == null)
                    {
                        _userDialogs.Alert("Riprap (Inlet) (3D) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedHeadwallOutletDistress == -1 || SelectedHeadwallOutletDistress == null)
                    {
                        _userDialogs.Alert("Headwall (Outlet) (3E) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedHeadwallOutletSeverity == -1 || SelectedHeadwallOutletSeverity == null)
                    {
                        _userDialogs.Alert("Headwall (Outlet) (3E) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }


                    if (SelectedWingwallOutletDistress == -1 || SelectedWingwallOutletDistress == null)
                    {
                        _userDialogs.Alert("Wingwall(Outlet) (3F) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedWingwallOutletSeverity == -1 || SelectedWingwallOutletSeverity == null)
                    {
                        _userDialogs.Alert("Wingwall(Outlet) (3F) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }






                    if (SelectedApronOutletDistress == -1 || SelectedApronOutletDistress == null)
                    {
                        _userDialogs.Alert("Apron (Outlet) (3G) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedApronOutletSeverity == -1 || SelectedApronOutletSeverity == null)
                    {
                        _userDialogs.Alert("Apron (Outlet) (3G) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedRiprapOutletDistress == -1 || SelectedRiprapOutletDistress == null)
                    {
                        _userDialogs.Alert("Riprap (Outlet) (3H) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedRiprapOutletSeverity == -1 || SelectedRiprapOutletSeverity == null)
                    {
                        _userDialogs.Alert("Riprap (Outlet) (3H) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }









                    //if (SelectedBarrel1DistressDistress == -1 || SelectedBarrel1DistressDistress == null)
                    //{
                    //    _userDialogs.Alert("Barrel #1 (4A) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}

                    //if (SelectedBarrel1SeveritySeverity == -1 || SelectedBarrel1SeveritySeverity == null)
                    //{
                    //    _userDialogs.Alert("Barrel #1 (4A) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}

                    //if (SelectedBarrel2DistressDistress == -1 || SelectedBarrel2DistressDistress == null)
                    //{
                    //    _userDialogs.Alert("Barrel #2 (4B) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}

                    //if (SelectedBarrel2SeveritySeverity == -1 || SelectedBarrel2SeveritySeverity == null)
                    //{
                    //    _userDialogs.Alert("Barrel #2 (4B) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}

                    //if (SelectedBarrel3DistressDistress == -1 || SelectedBarrel3DistressDistress == null)
                    //{
                    //    _userDialogs.Alert("Barrel #3 (4C) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}

                    //if (SelectedBarrel3SeveritySeverity == -1 || SelectedBarrel3SeveritySeverity == null)
                    //{
                    //    _userDialogs.Alert("Barrel #3 (4C) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}


                    //if (SelectedBarrel4DistressDistress == -1 || SelectedBarrel4DistressDistress == null)
                    //{
                    //    _userDialogs.Alert("Barrel #4 (4D) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}

                    //if (SelectedBarrel4SeveritySeverity == -1 || SelectedBarrel4SeveritySeverity == null)
                    //{
                    //    _userDialogs.Alert("Barrel #4 (4D) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}


                    //if (SelectedBarrel5DistressDistress == -1 || SelectedBarrel5DistressDistress == null)
                    //{
                    //    _userDialogs.Alert("Barrel #5 (5D) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}

                    //if (SelectedBarrel5SeveritySeverity == -1 || SelectedBarrel5SeveritySeverity == null)
                    //{
                    //    _userDialogs.Alert("Barrel #5 (5D) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}



                    //if (SelectedBarrel6DistressDistress == -1 || SelectedBarrel6DistressDistress == null)
                    //{
                    //    _userDialogs.Alert("Barrel #6 (6D) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}

                    //if (SelectedBarrel6SeveritySeverity == -1 || SelectedBarrel6SeveritySeverity == null)
                    //{
                    //    _userDialogs.Alert("Barrel #6 (6D) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}


                    //if (SelectedBarrel7DistressDistress == -1 || SelectedBarrel7DistressDistress == null)
                    //{
                    //    _userDialogs.Alert("Barrel #7 (7D) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}

                    //if (SelectedBarrel7SeveritySeverity == -1 || SelectedBarrel7SeveritySeverity == null)
                    //{
                    //    _userDialogs.Alert("Barrel #7 (7D) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}


                    //if (SelectedBarrel8DistressDistress == -1 || SelectedBarrel8DistressDistress == null)
                    //{
                    //    _userDialogs.Alert("Barrel #8 (8D) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}

                    //if (SelectedBarrel8SeveritySeverity == -1 || SelectedBarrel8SeveritySeverity == null)
                    //{
                    //    _userDialogs.Alert("Barrel #8 (8D) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}


                    //if (SelectedBarrel9DistressDistress == -1 || SelectedBarrel9DistressDistress == null)
                    //{
                    //    _userDialogs.Alert("Barrel #9 (9D) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}

                    //if (SelectedBarrel9SeveritySeverity == -1 || SelectedBarrel9SeveritySeverity == null)
                    //{
                    //    _userDialogs.Alert("Barrel #9 (9D) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}



                    //if (SelectedBarrel10DistressDistress == -1 || SelectedBarrel10DistressDistress == null)
                    //{
                    //    _userDialogs.Alert("Barrel #10 (10D) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}

                    //if (SelectedBarrel10SeveritySeverity == -1 || SelectedBarrel10SeveritySeverity == null)
                    //{
                    //    _userDialogs.Alert("Barrel #10 (10D) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}





                    if (SelectedCulvertApproachDistress == -1 || SelectedCulvertApproachDistress == null)
                    {
                        _userDialogs.Alert("Culvert Approach Condition (5A) Distress field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedCulvertApproachSeverity == -1 || SelectedCulvertApproachSeverity == null)
                    {
                        _userDialogs.Alert("Culvert Approach Condition (5A) Severity field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedInspIndex == -1 || SelectedInspIndex == null)
                    {
                        _userDialogs.Alert("Inspected By User Id field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (InspDate == null)
                    {
                        _userDialogs.Alert("Inspected by date is required.", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedConditionRating == -1 || SelectedConditionRating == null)
                    {
                        _userDialogs.Alert("Overall Condition Rating field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (SelectedFutureInvestigation == -1 || SelectedFutureInvestigation == null)
                    {
                        _userDialogs.Alert("Further Investigation field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    SaveSignature("submit");
                });
            }
        }



}

    public class CulvertListData : FreshBasePageModel
    {
        public int SelectedCulvertUnitSeverity { get; set; } = -1;
        private int _selectedCulvertUnitDistress = -1;
        public bool IsCulvertUnitOthersVisible { get; set; } = false;
        public int SelectedCulvertUnitDistress
        {
            get { return _selectedCulvertUnitDistress; }
            set
            {
                if (value != -1)
                {
                    _selectedCulvertUnitDistress = value;

                    var userprp = CulvertUnitDistressList[_selectedCulvertUnitDistress].Text.Split('-')[1];
                    if (userprp.ToLower() == "others")
                    {
                        IsCulvertUnitOthersVisible = true;
                    }
                    else
                    {
                        IsCulvertUnitOthersVisible = false;
                    }

                }
            }
        }

        public string CulvertName { get; set; }
        public ObservableCollection<DDListItems> CulvertUnitDistressList { get; set; }
        public ObservableCollection<DDListItems> CulvertUnitSeverityList { get; set; }


        public ICommand LocationSelectionCommand
        {
            get
            {
                return new Command((obj) =>
                {

                   // CurrentPage.Navigation.PushPopupAsync(new FormB1B2CulvertUnit(CulvertUnitDistressList));
                });
            }
        }
    }
}
