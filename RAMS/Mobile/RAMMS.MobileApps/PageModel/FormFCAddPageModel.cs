using Acr.UserDialogs;
using FreshMvvm;
using Newtonsoft.Json;
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
    public class FormFCAddPageModel : FreshBasePageModel
    {
        private IRestApi _restApi;
        private IUserDialogs _userDialogs;
        private ILocalDatabase _localDatabase;
        private bool isModify;
        private bool isDelete;
        private bool isView;
        private bool _fromAdd;
        private bool _fromEdit;
        private AssetDDLResponseDTO.DropDown _selectedRMU;
        private AssetDDLResponseDTO.DropDown _selectedRoadCode;
        private AssetDDLResponseDTO.DropDown _selectedSectionCode;
        private int _selectedDivision = -1;
        private int _selectedYear = -1;
        private int _selectedCrewIndex = -1;
        private int _selectedInspIndex = -1;

        SignaturePadView InspPadView;
        public bool AverageEnable { get; set; } = true;
        public bool IsAdd { get; set; }
        public bool IsHeaderEnable { get; set; } = true;
        public bool CanSave { get; set; } = false;
        public bool IsView { get; set; } = false;
        public string SelectedRefNo { get; set; }
        public bool IsCrewNameEnabled { get; set; }
        public bool IsInspNameEnabled { get; set; }
        public string InspName { get; set; }
        public string CrewName { get; set; }
        public string InspDesignation { get; set; }
        public string CrewDesignation { get; set; }
        public ImageSource InspSign { get; set; }
        public ImageSource CrewSign { get; set; }
        public DateTime? InspDate { get; set; } = null;
        public DateTime? CrewDate { get; set; } = null;
        public string SelectedRoadName { get; set; }
        public string SelectedSectionName { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRodeCodeListItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRMUListItems { get; set; }
        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDSectionListItems { get; set; }
        public ObservableCollection<DDListItems> DDYearListItems { get; set; }
        public ObservableCollection<DDListItems> DDDivisionListItems { get; set; }
        public ObservableCollection<DDListItems> DDInspUserListListItems { get; set; }
        public ObservableCollection<DDListItems> DDCrewUserListListItems { get; set; }

        private double? _avgWidth_ELM_Left_Paint_1 = null;


        public double? AvgWidth_ELM_Left_Paint
        {
            get { return _avgWidth_ELM_Left_Paint_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_ELM_Left_Paint_1 = null;
                }
                else if (!_avgWidth_ELM_Left_Paint_1.HasValue || !_avgWidth_ELM_Left_Paint_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_ELM_Left_Paint_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_ELM_Left_Paint_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }

        private double? _avgWidth_ELM_Left_Thermoplastic_1 = null;

        public double? AvgWidth_ELM_Left_Thermoplastic
        {
            get { return _avgWidth_ELM_Left_Thermoplastic_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_ELM_Left_Thermoplastic_1 = null;
                }
                else if (!_avgWidth_ELM_Left_Thermoplastic_1.HasValue || !_avgWidth_ELM_Left_Thermoplastic_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_ELM_Left_Thermoplastic_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_ELM_Left_Thermoplastic_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }

        private double? _avgWidth_ELM_Left_RoadStuds_1 = null;

        public double? AvgWidth_ELM_Left_RoadStuds
        {
            get { return _avgWidth_ELM_Left_RoadStuds_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_ELM_Left_RoadStuds_1 = null;
                }
                else if (!_avgWidth_ELM_Left_RoadStuds_1.HasValue || !_avgWidth_ELM_Left_RoadStuds_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_ELM_Left_RoadStuds_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_ELM_Left_RoadStuds_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }
        private double? _avgWidth_CW_Asphalt_1 = null;

        public double? AvgWidth_CW_Asphalt
        {
            get { return _avgWidth_CW_Asphalt_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_CW_Asphalt_1 = null;
                }
                else if (!_avgWidth_CW_Asphalt_1.HasValue || !_avgWidth_CW_Asphalt_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_CW_Asphalt_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_CW_Asphalt_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }
        private double? _avgWidth_CW_Surface_Dressed_1 = null;

        public double? AvgWidth_CW_Surface_Dressed
        {
            get { return _avgWidth_CW_Surface_Dressed_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_CW_Surface_Dressed_1 = null;
                }
                else if (!_avgWidth_CW_Surface_Dressed_1.HasValue || !_avgWidth_CW_Surface_Dressed_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_CW_Surface_Dressed_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_CW_Surface_Dressed_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }

        private double? _avgWidth_CW_Gravel_1 = null;

        public double? AvgWidth_CW_Gravel
        {
            get { return _avgWidth_CW_Gravel_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_CW_Gravel_1 = null;
                }
                else if (!_avgWidth_CW_Gravel_1.HasValue || !_avgWidth_CW_Gravel_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_CW_Gravel_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_CW_Gravel_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }
        private double? _avgWidth_CW_Earth_1 = null;

        public double? AvgWidth_CW_Earth
        {
            get { return _avgWidth_CW_Earth_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_CW_Earth_1 = null;
                }
                else if (!_avgWidth_CW_Earth_1.HasValue || !_avgWidth_CW_Earth_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_CW_Earth_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_CW_Earth_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }
        private double? _avgWidth_CW_Concrete_1 = null;

        public double? AvgWidth_CW_Concrete
        {
            get { return _avgWidth_CW_Concrete_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_CW_Concrete_1 = null;
                }
                else if (!_avgWidth_CW_Concrete_1.HasValue || !_avgWidth_CW_Concrete_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_CW_Concrete_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_CW_Concrete_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }

        private double? _avgWidth_CW_Sand_1 = null;

        public double? AvgWidth_CW_Sand
        {
            get { return _avgWidth_CW_Sand_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_CW_Sand_1 = null;
                }
                else if (!_avgWidth_CW_Sand_1.HasValue || !_avgWidth_CW_Sand_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_CW_Sand_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_CW_Sand_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }

        private double? _avgWidth_Center_RoadStuds_1 = null;

        public double? AvgWidth_Center_RoadStuds
        {
            get { return _avgWidth_Center_RoadStuds_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Center_RoadStuds_1 = null;
                }
                else if (!_avgWidth_Center_RoadStuds_1.HasValue || !_avgWidth_Center_RoadStuds_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Center_RoadStuds_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Center_RoadStuds_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }

        private double? _avgWidth_CLM_Left_Paint_1 = null;

        public double? AvgWidth_CLM_Left_Paint
        {
            get { return _avgWidth_CLM_Left_Paint_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_CLM_Left_Paint_1 = null;
                }
                else if (!_avgWidth_CLM_Left_Paint_1.HasValue || !_avgWidth_CLM_Left_Paint_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_CLM_Left_Paint_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_CLM_Left_Paint_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }
        private double? _avgWidth_CLM_Left_Thermoplastic_1 = null;

        public double? AvgWidth_CLM_Left_Thermoplastic
        {
            get { return _avgWidth_CLM_Left_Thermoplastic_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_CLM_Left_Thermoplastic_1 = null;
                }
                else if (!_avgWidth_CLM_Left_Thermoplastic_1.HasValue || !_avgWidth_CLM_Left_Thermoplastic_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_CLM_Left_Thermoplastic_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_CLM_Left_Thermoplastic_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }
        private double? _avgWidth_ELM_Right_RoadStuds_1 = null;

        public double? AvgWidth_ELM_Right_RoadStuds
        {
            get { return _avgWidth_ELM_Right_RoadStuds_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_ELM_Right_RoadStuds_1 = null;
                }
                else if (!_avgWidth_ELM_Right_RoadStuds_1.HasValue || !_avgWidth_ELM_Right_RoadStuds_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_ELM_Right_RoadStuds_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_ELM_Right_RoadStuds_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }
        private double? _avgWidth_ELM_Right_Paint_1 = null;

        public double? AvgWidth_ELM_Right_Paint
        {
            get { return _avgWidth_ELM_Right_Paint_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_ELM_Right_Paint_1 = null;
                }
                else if (!_avgWidth_ELM_Right_Paint_1.HasValue || !_avgWidth_ELM_Right_Paint_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_ELM_Right_Paint_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_ELM_Right_Paint_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }
        private double? _avgWidth_ELM_Right_Thermoplastic_1 = null;

        public double? AvgWidth_ELM_Right_Thermoplastic
        {
            get { return _avgWidth_ELM_Right_Thermoplastic_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_ELM_Right_Thermoplastic_1 = null;
                }
                else if (!_avgWidth_ELM_Right_Thermoplastic_1.HasValue || !_avgWidth_ELM_Right_Thermoplastic_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_ELM_Right_Thermoplastic_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_ELM_Right_Thermoplastic_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }

        public bool FromEdit
        {
            get
            {
                return _fromEdit;
            }
            set
            {
                _fromEdit = value;
                RaisePropertyChanged("FromEdit");
            }
        }

        public bool _fromAdd1;
        public bool FromAdd1
        {
            get
            {
                return _fromAdd1;
            }
            set
            {
                _fromAdd1 = value;
                RaisePropertyChanged("FromAdd1");
            }
        }

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

        public AssetDDLResponseDTO.DropDown SelectedRoadCode
        {
            get => _selectedRoadCode;
            set
            {
                _selectedRoadCode = value;
                if (_selectedRoadCode != null)
                {
                    SelectedRoadName = _selectedRoadCode.Item1;
                    SetRefNumber();
                }
                RaisePropertyChanged();
            }
        }

        private void SetRefNumber()
        {
            if (SelectedRoadCode != null && SelectedYear > 0)
                SelectedRefNo = "CI/Form FC/" + SelectedRoadCode.Value + "/" + DDYearListItems[SelectedYear].Value;
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
                    var year = DDYearListItems[SelectedYear]?.Text;
                    MinimumDate = Convert.ToDateTime("1-1-" + year);
                    MaximumDate = Convert.ToDateTime("12-31-" + year);

                    InspDate = null;
                }
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
                if (_selectedCrewIndex != -1)
                {
                    var selectedItem = Convert.ToInt32(DDCrewUserListListItems?[_selectedCrewIndex].Value.ToString());

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

                    GetUserDetilsList("inspuser", selectedItem);
                }
            }
        }

        public int PkRefNo { get; set; }
        public string DivCode { get; set; }
        public string Dist { get; set; }
        public string RmuName { get; set; }
        public int? RoadId { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public decimal? RoadLength { get; set; }
        public int? YearOfInsp { get; set; }
        public int? UserIdInspBy { get; set; }
        public string UserNameInspBy { get; set; }
        public string UserDesignationInspBy { get; set; }
        public DateTime? DtInspBy { get; set; }
        public string SignpathInspBy { get; set; }
        public string FormRefId { get; set; }
        public int? CrewLeaderId { get; set; }
        public string CrewLeaderName { get; set; }
        public string Remarks { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }
        public decimal? FrmCh { get; set; }
        public decimal? ToCh { get; set; }
        public string AssetTypes { get; set; }
        public IList<FormFCDetailsDTO> InsDtl { get; set; }
        public ObservableCollection<GridBoxData> HeaderCollection { get; set; }
        public ObservableCollection<GridBoxData> ELM_Left_Paint { get; set; }
        public ObservableCollection<GridBoxData> ELM_Left_Thermoplastic { get; set; }
        public ObservableCollection<GridBoxData> ELM_Left_RoadStuds { get; set; }
        public ObservableCollection<GridBoxData> CW_Asphalt { get; set; }
        public ObservableCollection<GridBoxData> CW_Surface { get; set; }
        public ObservableCollection<GridBoxData> CW_Gravel { get; set; }
        public ObservableCollection<GridBoxData> CW_Earth { get; set; }
        public ObservableCollection<GridBoxData> CW_Concrete { get; set; }
        public ObservableCollection<GridBoxData> CW_Sand { get; set; }
        public ObservableCollection<GridBoxData> CW_Center_RoadStuds { get; set; }
        public ObservableCollection<GridBoxData> CLM_Paint { get; set; }
        public ObservableCollection<GridBoxData> CLM_Thermoplastic { get; set; }
        public ObservableCollection<GridBoxData> ELM_Right_RoadStuds { get; set; }
        public ObservableCollection<GridBoxData> ELM_Right_Paint { get; set; }
        public ObservableCollection<GridBoxData> ELM_Right_ThermoplasticList { get; set; }

        //public decimal? ELM_Left_Paint_1
        //{
        //    get; set;
        //}

        public string ELM_Left_Paint_1 { get; set; } 
        public string ELM_Left_Paint_2 { get; set; } 
        public string ELM_Left_Paint_3 { get; set; } 
        public string ELM_Left_Paint_Total { get; set; }

        public string ELM_Left_Thermoplastic_1 { get; set; } 
        public string ELM_Left_Thermoplastic_2 { get; set; } 
        public string ELM_Left_Thermoplastic_3 { get; set; } 
        public string ELM_Left_Thermoplastic_Total { get; set; }

        public string ELM_Left_RoadStuds_1 { get; set; }
        public string ELM_Left_RoadStuds_2 { get; set; }
        public string ELM_Left_RoadStuds_3 { get; set; }
        public string ELM_Left_RoadStuds_Total { get; set; }

        public string CW_Asphalt_1 { get; set; }
        public string CW_Asphalt_2 { get; set; }
        public string CW_Asphalt_3 { get; set; }
        public string CW_Asphalt_Total { get; set; }

        public string CW_Surface_1 { get; set; }
        public string CW_Surface_2 { get; set; }
        public string CW_Surface_3 { get; set; }
        public string CW_Surface_Total { get; set; }

        public string CW_Gravel_1 { get; set; }
        public string CW_Gravel_2 { get; set; }
        public string CW_Gravel_3 { get; set; }
        public string CW_Gravel_Total { get; set; }

        public string CW_Earth_1 { get; set; }
        public string CW_Earth_2 { get; set; }
        public string CW_Earth_3 { get; set; }
        public string CW_Earth_Total { get; set; }
        
        public string CW_Concrete_1 { get; set; }
        public string CW_Concrete_2 { get; set; }
        public string CW_Concrete_3 { get; set; }
        public string CW_Concrete_Total { get; set; }
        
        public string CW_Sand_1 { get; set; }
        public string CW_Sand_2 { get; set; }
        public string CW_Sand_3 { get; set; }
        public string CW_Sand_Total { get; set; }
        
        public string CW_Center_RoadStuds_1 { get; set; }
        public string CW_Center_RoadStuds_2 { get; set; }
        public string CW_Center_RoadStuds_3 { get; set; }
        public string CW_Center_RoadStuds_Total { get; set; }
        
        public string CLM_Paint_1 { get; set; }
        public string CLM_Paint_2 { get; set; }
        public string CLM_Paint_3 { get; set; }
        public string CLM_Paint_Total { get; set; }
        
        public string CLM_Thermoplastic_1 { get; set; }
        public string CLM_Thermoplastic_2 { get; set; }
        public string CLM_Thermoplastic_3 { get; set; }
        public string CLM_Thermoplastic_Total { get; set; }
        
        public string ELM_Right_RoadStuds_1 { get; set; }
        public string ELM_Right_RoadStuds_2 { get; set; }
        public string ELM_Right_RoadStuds_3 { get; set; }
        public string ELM_Right_RoadStuds_Total { get; set; }
        
        public string ELM_Right_Paint_1 { get; set; }
        public string ELM_Right_Paint_2 { get; set; }
        public string ELM_Right_Paint_3 { get; set; }
        public string ELM_Right_Paint_Total { get; set; }
        
        public string ELM_Right_ThermoplasticList_1 { get; set; }
        public string ELM_Right_ThermoplasticList_2 { get; set; }
        public string ELM_Right_ThermoplasticList_3 { get; set; }
        public string ELM_Right_ThermoplasticList_Total { get; set; }


        public FormFCAddPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;
            _localDatabase = localDatabase;
        }
        public override async void Init(object initData)
        {
            base.Init(initData);

            isView = Model.Security.IsView(ModuleNameList.Condition_Inspection);
            IsAdd = isModify = Model.Security.IsModify(ModuleNameList.Condition_Inspection);
            isDelete = Model.Security.IsDelete(ModuleNameList.Condition_Inspection);

            if (App.ReturnType == "Add")
            {
                FromAdd = true;
                FromEdit = false;
                FromAdd1 = false;
               // App.HeaderCode = 0;
            }
            if (App.ReturnType == "Edit")
            {
                FromEdit = true;
                FromAdd = false;
                FromAdd1 = false;
                // App.HeaderCode = 0;
            }
        }
        protected override async void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);

            await GetLandingDropDownList();
            await GetDDListDetails("division");
            await GetDDListDetails("Year");
            await GetDDListDetails("crew");

            await GetUserList();
            if (App.ReturnType == "Add")
            {
                FromAdd = true;
                FromEdit = false;
                FromAdd1 = false;
                // App.HeaderCode = 0;
            }
            if (App.ReturnType == "Edit")
            {
                FromEdit = true;
                FromAdd = false;
                FromAdd1 = false;
                // App.HeaderCode = 0;
            }
            if (App.ReturnType == "View")
            {
                FromEdit = true;
                FromAdd = false;
                FromAdd1 = false;
                // App.HeaderCode = 0;
            }

            CanSave = App.ReturnType == "Edit" ? true : false;
            if (App.ReturnType == "Edit" || App.ReturnType == "View")
            {
                FDGridView = true;
                IsHeaderEnable = false;
                IsView = App.ReturnType == "View" ? true : false;
                await GetFCById(App.HeaderCode);




            }
            if (App.ReturnType == "View")
                AverageEnable = false;           
            else
                AverageEnable = true;
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
                    var response = new ResponseBaseListObject<DDListItems>();
                    if (ddtype == "crew")
                         response = await _restApi.GetSupervisor();
                    else
                     response = await _restApi.GetDDList(ddlist);

                    if (response.success)
                    {
                        if (ddtype == "Year")
                            DDYearListItems = new ObservableCollection<DDListItems>(response.data);
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
                      //  DDCrewUserListListItems = new ObservableCollection<DDListItems>(response.data);
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
                                    if (App.ReturnType == "Add")
                                    {
                                        InspName = null;
                                        InspName = InspName ?? response.data.UserName;
                                        InspDesignation = InspDesignation ?? response.data.Position;
                                    }
                                    
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
                                    CrewName = (CrewName != null ? CrewName : null);

                                    //if (response.data.UserName.ToLower() == "others")
                                    //{
                                    //    CrewName = null;
                                    //    CrewName = (CrewName == null ? response.data.UserName : CrewName);
                                    //}
                                }
                                else
                                {
                                    IsCrewNameEnabled = false;
                                    CrewName = response.data.UserName;

                                    
                                }

                                // CrewDesignation = response.data.Position;
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

        private async Task<int> GetFCById(int headerCode)
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.GetFCById(headerCode);

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
                _userDialogs.HideLoading();
            }
            return 1;
        }
        //public string AvgWidth_ELM_Left_Paint { get; set; }
        //public string AvgWidth_ELM_Left_Thermoplastic { get; set; }
        //public string AvgWidth_ELM_Left_RoadStuds { get; set; }
        //public string AvgWidth_CW_Asphalt { get; set; }
        //public string AvgWidth_CW_Surface_Dressed { get; set; }
        //public string AvgWidth_CW_Gravel { get; set; }
        //public string AvgWidth_CW_Earth { get; set; }
        //public string AvgWidth_CW_Concrete { get; set; }
        //public string AvgWidth_CW_Sand { get; set; }
        //public string AvgWidth_Center_RoadStuds { get; set; }
        //public string AvgWidth_CLM_Left_Paint { get; set; }
        //public string AvgWidth_CLM_Left_Thermoplastic { get; set; }
        //public string AvgWidth_ELM_Right_RoadStuds { get; set; }
        //public string AvgWidth_ELM_Right_Paint { get; set; }
        //public string AvgWidth_ELM_Right_Thermoplastic { get; set; }

        private void SetViewData(ResponseBaseObject<FormFCHeaderRequestDTO> response)
        {
            SelectedRefNo = response.data.FormRefId;

            FrmCh = response.data.FrmCh;
            InsDtl = response.data.InsDtl;
            PkRefNo = response.data.PkRefNo;
            Remarks = response.data.Remarks;
            RoadName = response.data.RoadName;
            ToCh = response.data.ToCh;
            YearOfInsp = response.data.YearOfInsp;
            SelectedYear = DDYearListItems.ToList().FindIndex(x => x.Value == response.data.YearOfInsp.ToString());





            // InspDate = response.data.DtInspBy.HasValue ? response.data.DtInspBy.Value : (DateTime?)null;




            int inspindex = DDInspUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.UserIdInspBy);
            SelectedInspIndex = inspindex;
            InspName = response.data.UserNameInspBy;
            InspDesignation = response.data.UserDesignationInspBy;
            InspDate = response.data.DtInspBy.HasValue ? response.data.DtInspBy.Value : (DateTime?)null;
            InspSign = ImageSource.FromStream(
                () => new MemoryStream(Convert.FromBase64String(response.data.SignpathInspBy)));

            int crewindex = DDCrewUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.CrewLeaderId);
            SelectedCrewIndex = crewindex;
            CrewName = response.data.CrewLeaderName;

            var Item = JsonConvert.DeserializeObject<Root>(response.data.AssetTypes);

            if(Item.ELM[0].LAvgWidth != "")
               AvgWidth_ELM_Left_Paint =  Convert.ToDouble(Item.ELM[0].LAvgWidth);
            if(Item.ELM[1].LAvgWidth != "")
               AvgWidth_ELM_Left_Thermoplastic = Convert.ToDouble(Item.ELM[1].LAvgWidth);
            if (Item.RS[0].LAvgWidth !="")
                AvgWidth_ELM_Left_RoadStuds = Convert.ToDouble(Item.RS[0].LAvgWidth);
          if (Item.CW[0].AvgWidth !="")
                AvgWidth_CW_Asphalt = Convert.ToDouble(Item.CW[0].AvgWidth);
          if(Item.CW[1].AvgWidth !="")
            AvgWidth_CW_Surface_Dressed = Convert.ToDouble(Item.CW[1].AvgWidth);
          if(Item.CW[2].AvgWidth !="")
            AvgWidth_CW_Gravel = Convert.ToDouble (Item.CW[2].AvgWidth);
          if (Item.CW[3].AvgWidth !="")
                AvgWidth_CW_Earth = Convert.ToDouble(Item.CW[3].AvgWidth);
          if (Item.CW[4].AvgWidth != "")
                AvgWidth_CW_Concrete = Convert.ToDouble(Item.CW[4].AvgWidth);
          if (Item.CW[5].AvgWidth !="")
                AvgWidth_CW_Sand = Convert.ToDouble(Item.CW[5].AvgWidth);
          if (Item.RS[1].AvgWidth !="")
                AvgWidth_Center_RoadStuds = Convert.ToDouble (Item.RS[1].AvgWidth);
          if (Item.CLM[0].AvgWidth !="")
                AvgWidth_CLM_Left_Paint = Convert.ToDouble(Item.CLM[0].AvgWidth);
          if (Item.CLM[1].AvgWidth !="")
                AvgWidth_CLM_Left_Thermoplastic = Convert.ToDouble (Item.CLM[1].AvgWidth);
          if (Item.RS[2].RAvgWidth != "")
                AvgWidth_ELM_Right_RoadStuds = Convert.ToDouble(Item.RS[2].RAvgWidth);
          if (Item.ELM[0].RAvgWidth !="")
                AvgWidth_ELM_Right_Paint = Convert.ToDouble(Item.ELM[0].RAvgWidth);
          if (Item.ELM[1].RAvgWidth !="")
                AvgWidth_ELM_Right_Thermoplastic = Convert.ToDouble(Item.ELM[1].RAvgWidth);

            GenerateTableData(response.data);
        }

       
        private void GenerateTableData(FormFCHeaderRequestDTO data)
        {
            decimal diff = (decimal)data.FrmCh;
            var count = 0;

            HeaderCollection = new ObservableCollection<GridBoxData>();
            ELM_Left_Paint = new ObservableCollection<GridBoxData>();
            ELM_Left_Thermoplastic = new ObservableCollection<GridBoxData>();
            ELM_Left_RoadStuds = new ObservableCollection<GridBoxData>();

            CW_Asphalt = new ObservableCollection<GridBoxData>();
            CW_Surface = new ObservableCollection<GridBoxData>();
            CW_Gravel = new ObservableCollection<GridBoxData>();
            CW_Earth = new ObservableCollection<GridBoxData>();
            CW_Concrete = new ObservableCollection<GridBoxData>();
            CW_Sand = new ObservableCollection<GridBoxData>();
            CW_Center_RoadStuds = new ObservableCollection<GridBoxData>();

            CLM_Paint = new ObservableCollection<GridBoxData>();
            CLM_Thermoplastic = new ObservableCollection<GridBoxData>();

            ELM_Right_RoadStuds = new ObservableCollection<GridBoxData>();
            ELM_Right_Paint = new ObservableCollection<GridBoxData>();
            ELM_Right_ThermoplasticList = new ObservableCollection<GridBoxData>();


            var minValu = 499;
            var maxValu = 999;
            var toCHValue = data.ToCh.ToString().Split('.');
            
            if(Convert.ToInt32(toCHValue[1])< minValu)
            {
                toCHValue[1] = minValu.ToString(); 
            }
            else if (Convert.ToInt32(toCHValue[1]) < maxValu)
            {
                toCHValue[1] = maxValu.ToString();
            }

            decimal ? UpdatedToCH = Convert.ToDecimal(toCHValue[0] + "." + toCHValue[1]);

            while (diff<= UpdatedToCH)
            {
                var km1 = diff.ToString().Split('.');
                if (km1[1] == "099" | km1[1] == "199" | km1[1] == "299" | km1[1] == "399" | km1[1] == "499" | km1[1] == "599" | km1[1] == "699" | km1[1] == "799" | km1[1] == "899" | km1[1] == "999")
                {
                    diff = decimal.Add(diff, (decimal)0.001);
                }
                

                // diff = decimal.Add(diff, (decimal)0.100);


                ELM_Left_Paint.Add(new GridBoxData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "ELM").Where(x => x.AiGrpType == "Paint").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "ELM").Where(x => x.AiGrpType == "Paint").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "ELM").Where(x => x.AiGrpType == "Paint").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                ELM_Left_Thermoplastic.Add(new GridBoxData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "ELM").Where(x => x.AiGrpType == "Thermoplastic").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "ELM").Where(x => x.AiGrpType == "Thermoplastic").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "ELM").Where(x => x.AiGrpType == "Thermoplastic").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                ELM_Left_RoadStuds.Add(new GridBoxData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "RS").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "RS").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "RS").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });

                CW_Asphalt.Add(new GridBoxData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Asphalt").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Asphalt").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Asphalt").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                CW_Surface.Add(new GridBoxData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Surface Dressed").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Surface Dressed").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Surface Dressed").Where(x => x.FromCHKm == diff).ToList()[0].Condition : null });
                CW_Gravel.Add(new GridBoxData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Gravel").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Gravel").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Gravel").Where(x => x.FromCHKm == diff).ToList()[0].Condition : null });
                CW_Earth.Add(new GridBoxData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Earth").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Earth").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Earth").Where(x => x.FromCHKm == diff).ToList()[0].Condition : null });
                CW_Concrete.Add(new GridBoxData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Concrete").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Concrete").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Concrete").Where(x => x.FromCHKm == diff).ToList()[0].Condition : null });
                CW_Sand.Add(new GridBoxData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Sand").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Sand").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "CW").Where(x => x.AiGrpType == "Sand").Where(x => x.FromCHKm == diff).ToList()[0].Condition : null });
                CW_Center_RoadStuds.Add(new GridBoxData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "RS").Where(x => x.AiBound == "M").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "RS").Where(x => x.AiBound == "M").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "RS").Where(x => x.AiBound == "M").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });

                CLM_Paint.Add(new GridBoxData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "CLM").Where(x => x.AiGrpType == "Paint").Where(x => x.AiBound == "M").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "CLM").Where(x => x.AiGrpType == "Paint").Where(x => x.AiBound == "M").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "CLM").Where(x => x.AiGrpType == "Paint").Where(x => x.AiBound == "M").Where(x => x.FromCHKm == diff).ToList()[0].Condition : null });
                CLM_Thermoplastic.Add(new GridBoxData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "CLM").Where(x => x.AiGrpType == "Thermoplastic").Where(x => x.AiBound == "M").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "CLM").Where(x => x.AiGrpType == "Thermoplastic").Where(x => x.AiBound == "M").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "CLM").Where(x => x.AiGrpType == "Thermoplastic").Where(x => x.AiBound == "M").Where(x => x.FromCHKm == diff).ToList()[0].Condition : null });

                ELM_Right_RoadStuds.Add(new GridBoxData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "RS").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "RS").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "RS").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList()[0].Condition : null });
                ELM_Right_Paint.Add(new GridBoxData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "ELM").Where(x => x.AiGrpType == "Paint").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "ELM").Where(x => x.AiGrpType == "Paint").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "ELM").Where(x => x.AiGrpType == "Paint").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList()[0].Condition : null });
                ELM_Right_ThermoplasticList.Add(new GridBoxData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "ELM").Where(x => x.AiGrpType == "Thermoplastic").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "ELM").Where(x => x.AiGrpType == "Thermoplastic").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "ELM").Where(x => x.AiGrpType == "Thermoplastic").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList()[0].Condition : null });

                //if (ELM_Left_Paint.Count() % 5 == 0)
                //{
                //    HeaderCollection.Add(new GridBoxData() { FrmCH = ELM_Left_Paint[ELM_Left_Paint.Count() - 5].FrmCH, ToCH = diff.ToString() });
                //}

                count++;
               diff = decimal.Subtract(diff, (decimal)0.001);

                diff = decimal.Add(diff, (decimal)0.100);
                var km = diff.ToString().Split('.');
                if(km[1] == "000" | km[1] == "100" | km[1] == "200" | km[1] == "300" | km[1] == "400" | km[1] == "500" | km[1] == "600" | km[1] == "700" | km[1] == "800" | km[1] == "900"  )
                {
                    diff = decimal.Subtract(diff, (decimal)0.001);
                }
                else
                {
                    diff = decimal.Add(diff, (decimal)0.001);
                    diff = decimal.Subtract(diff, (decimal)0.001);

                }

                if (ELM_Left_Paint.Count() % 5 == 0)
                {
                    var fromch = ELM_Left_Paint[ELM_Left_Paint.Count() - 5].FrmCH;
                    if (fromch != "0.000")
                    {
                        decimal ifromch = Convert.ToDecimal(fromch);

                        // decimal ifromch = decimal.Add(Convert.ToDecimal(fromch), (decimal)0.001);
                        HeaderCollection.Add(new GridBoxData() { FrmCH = ifromch.ToString().Replace(".", "+"), ToCH = diff.ToString().Replace(".", "+") });
                    }
                    else
                    {
                        decimal ifromch = Convert.ToDecimal(fromch);
                        HeaderCollection.Add(new GridBoxData() { FrmCH = ifromch.ToString().Replace(".", "+"), ToCH = diff.ToString().Replace(".", "+") });
                    }

                }
            }

            //if(ELM_Left_Paint.Count() %5 != 0)
            //{
            //    HeaderCollection.Add(new GridBoxData() { FrmCH = decimal.Add( Convert.ToDecimal(HeaderCollection[HeaderCollection.Count() - 1].ToCH), (decimal)0.100).ToString(), ToCH = data.ToCh.ToString() });
            //}
        }
        public bool FDGridView { get; set; } = false;
        public ICommand OKCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    try
                    {
                        FDGridView = true;
                        if (string.IsNullOrWhiteSpace(SelectedRoadCode?.Value))
                        {
                            await UserDialogs.Instance.AlertAsync("Road Code field is required.Please choose from the dropdown list.", "RAMS", "OK");
                            return;
                        }

                        if (SelectedYear == -1)
                        {
                            await UserDialogs.Instance.AlertAsync("Year field is required. Please choose from the dropdown list.", "RAMS", "OK");
                            return;
                        }

                        var response = SaveFormFCHeader();


                        // FromAdd = false;

                        // FromEdit = false;
                       
                        //if (App.ReturnType == "Edit")
                        //{
                        //    FromEdit = true;
                        //    FromAdd = false;
                        //    FromAdd1 = false;
                        //    // App.HeaderCode = 0;
                        //}
                    }
                    catch
                    {
                    }


                });
            }
        }
        public DateTime? DtOfInsp { get; set; }

        private async Task<ObservableCollection<FormFCHeaderRequestDTO>> SaveFormFCHeader()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    FormFCHeaderRequestDTO GridItems = new FormFCHeaderRequestDTO()
                    {
                      //  PkRefNo = App.HeaderCode,
                        //DivCode = SelectedDivision == -1 ? null : DDDivisionListItems[SelectedDivision].Value,
                        //RmuName = SelectedRMU?.Value,
                        RoadId = SelectedRoadCode.PKId,
                        RoadCode = SelectedRoadCode.Value,
                        RoadName = SelectedRoadName,
                        YearOfInsp = Convert.ToInt32(DDYearListItems[SelectedYear]?.Value),
                        DtInspBy = InspDate.HasValue ? InspDate.Value : (DateTime?)null


                    };
                    // GridItems.PkRefNo = App.HeaderCode;
                    //// GridItems.DivCode = DDDivisionListItems[SelectedDivision]?.Value;
                    // GridItems.DivCode = SelectedDivision == -1 ? null : DDDivisionListItems[SelectedDivision]?.Value;
                    //    GridItems.RmuName = SelectedRMU?.Value;
                    // //GridItems.SectionCode = SelectedSectionCode?.Code;
                    // //GridItems.SectionName = SelectedSectionName;
                    // GridItems.RoadId = SelectedRoadCode.PKId;
                    // GridItems.RoadCode = SelectedRoadCode.Value;
                    //GridItems.RoadName = SelectedRoadName;
                    // GridItems.YearOfInsp = Convert.ToInt32(DDYearListItems[SelectedYear].Value);


                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);
                    var response1 = await _restApi.AssetCheck(SelectedRoadCode.Value);
                    if(response1.success)
                    {
                        if(response1.data)
                        {

                            var response = await _restApi.SaveFCHdr(GridItems);

                            if (response.success)
                            {
                                IsHeaderEnable = false;
                                if (response.data.SubmitSts)
                                    CanSave = false;
                                else
                                    CanSave = true;
                                App.HeaderCode = response.data.PkRefNo;
                                SetViewData(response);
                                if (App.ReturnType == "Add")
                                {
                                    FromAdd = false;
                                    FromEdit = false;
                                    FromAdd1 = true;
                                    // App.HeaderCode = 0;
                                }
                            }
                            return null;
                        }
                        else
                        {
                            await UserDialogs.Instance.AlertAsync("There is no assets for the selected road.", "RAMS", "OK");
                            if (App.ReturnType == "Add")
                            {
                                FromAdd = true;
                                FromEdit = false;
                                FromAdd1 = false;
                                // App.HeaderCode = 0;
                            }
                        }
                    }



                }
                return null;
            }
            catch (Exception ex) 
            {
                _userDialogs.Alert(ex.Message);
                return null;
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

                try
                {
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        var HeaderItemResponse = await _restApi.GetFCById(App.HeaderCode);

                        if (HeaderItemResponse.success)
                        {
                            HeaderItemResponse.data.PkRefNo = App.HeaderCode;
                            HeaderItemResponse.data.UserIdInspBy = SelectedInspIndex != -1 ? Convert.ToInt32(DDInspUserListListItems[SelectedInspIndex].Value) : (int?)null;
                            HeaderItemResponse.data.UserNameInspBy = InspName;
                            HeaderItemResponse.data.UserDesignationInspBy = InspDesignation;
                            HeaderItemResponse.data.DtInspBy = InspDate;
                            HeaderItemResponse.data.SignpathInspBy = inspSign ?? HeaderItemResponse.data.SignpathInspBy ?? null;
                            HeaderItemResponse.data.CrewLeaderId = SelectedCrewIndex != -1 ? Convert.ToInt32(DDCrewUserListListItems[SelectedCrewIndex].Value) : (int?)null;
                            HeaderItemResponse.data.CrewLeaderName = CrewName;
                            HeaderItemResponse.data.Remarks = Remarks;
                            HeaderItemResponse.data.SubmitSts = type == "save" ? false : true;

                            var Item = JsonConvert.DeserializeObject<Root>(HeaderItemResponse.data.AssetTypes);
                            Item.ELM[0].LAvgWidth = AvgWidth_ELM_Left_Paint.ToString();
                            Item.ELM[1].LAvgWidth = AvgWidth_ELM_Left_Thermoplastic.ToString();
                            Item.RS[0].LAvgWidth = AvgWidth_ELM_Left_RoadStuds.ToString();
                            Item.CW[0].AvgWidth = AvgWidth_CW_Asphalt.ToString();
                            Item.CW[1].AvgWidth = AvgWidth_CW_Surface_Dressed.ToString();
                            Item.CW[2].AvgWidth = AvgWidth_CW_Gravel.ToString();
                            Item.CW[3].AvgWidth = AvgWidth_CW_Earth.ToString();
                            Item.CW[4].AvgWidth = AvgWidth_CW_Concrete.ToString();
                            Item.CW[5].AvgWidth = AvgWidth_CW_Sand.ToString();
                            Item.RS[1].AvgWidth = AvgWidth_Center_RoadStuds.ToString();
                            Item.CLM[0].AvgWidth = AvgWidth_CLM_Left_Paint.ToString();
                            Item.CLM[1].AvgWidth = AvgWidth_CLM_Left_Thermoplastic.ToString();
                            Item.RS[2].RAvgWidth = AvgWidth_ELM_Right_RoadStuds.ToString();
                            Item.ELM[0].RAvgWidth = AvgWidth_ELM_Right_Paint.ToString();
                            Item.ELM[1].RAvgWidth = AvgWidth_ELM_Right_Thermoplastic.ToString();

                            HeaderItemResponse.data.AssetTypes = JsonConvert.SerializeObject(Item);

                            decimal diff = (decimal)HeaderItemResponse.data.FrmCh;


                            var minValu = 499;
                            var maxValu = 999;
                            var toCHValue = HeaderItemResponse.data.ToCh.ToString().Split('.');

                            if (Convert.ToInt32(toCHValue[1]) < minValu)
                            {
                                toCHValue[1] = minValu.ToString();
                            }
                            else if (Convert.ToInt32(toCHValue[1]) < maxValu)
                            {
                                toCHValue[1] = maxValu.ToString();
                            }

                            decimal? UpdatedToCH = Convert.ToDecimal(toCHValue[0] + "." + toCHValue[1]);



                            while (diff <= UpdatedToCH)
                            {
                                foreach (var item in HeaderItemResponse.data.InsDtl)
                                {
                                    if (item.AiAssetGrpCode == "ELM" && item.AiGrpType == "Paint" && item.AiBound == "L" && item.FromCHKm == diff)
                                    {
                                        item.Condition = ELM_Left_Paint.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;
                                        Boolean isenable = ELM_Left_Paint.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }
                                    }
                                    if (item.AiAssetGrpCode == "ELM" && item.AiGrpType == "Thermoplastic" && item.AiBound == "L" && item.FromCHKm == diff)
                                    {
                                        item.Condition = ELM_Left_Thermoplastic.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = ELM_Left_Thermoplastic.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "RS" && item.AiBound == "L" && item.FromCHKm == diff)
                                    {
                                        item.Condition = ELM_Left_RoadStuds.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = ELM_Left_RoadStuds.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }
                                    }
                                    if (item.AiAssetGrpCode == "CW" && item.AiGrpType == "Asphalt" && item.FromCHKm == diff)
                                    {
                                        item.Condition = CW_Asphalt.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = CW_Asphalt.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }
                                    }
                                    if (item.AiAssetGrpCode == "CW" && item.AiGrpType == "Surface Dressed" && item.FromCHKm == diff)
                                    {
                                        item.Condition = CW_Surface.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = CW_Surface.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "CW" && item.AiGrpType == "Gravel" && item.FromCHKm == diff)
                                    {
                                        item.Condition = CW_Gravel.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;
                                        Boolean isenable = CW_Gravel.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "CW" && item.AiGrpType == "Earth" && item.FromCHKm == diff)
                                    {
                                        item.Condition = CW_Earth.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = CW_Earth.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }
                                    }
                                    if (item.AiAssetGrpCode == "CW" && item.AiGrpType == "Concrete" && item.FromCHKm == diff)
                                    {
                                        item.Condition = CW_Concrete.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;
                                        Boolean isenable = CW_Concrete.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "CW" && item.AiGrpType == "Sand" && item.FromCHKm == diff)
                                    {
                                        item.Condition = CW_Sand.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;
                                        Boolean isenable = CW_Sand.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "RS" && item.AiBound == "M" && item.FromCHKm == diff)
                                    {
                                        item.Condition = CW_Center_RoadStuds.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = CW_Center_RoadStuds.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "CLM" && item.AiGrpType == "Paint" && item.AiBound == "M" && item.FromCHKm == diff)
                                    {
                                        item.Condition = CLM_Paint.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = CLM_Paint.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }
                                    }
                                    if (item.AiAssetGrpCode == "CLM" && item.AiGrpType == "Thermoplastic" && item.AiBound == "M" && item.FromCHKm == diff)
                                    {
                                        item.Condition = CLM_Thermoplastic.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = CLM_Thermoplastic.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "RS" && item.AiBound == "R" && item.FromCHKm == diff)
                                    {
                                        item.Condition = ELM_Right_RoadStuds.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = ELM_Right_RoadStuds.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }
                                    }
                                    if (item.AiAssetGrpCode == "ELM" && item.AiGrpType == "Paint" && item.AiBound == "R" && item.FromCHKm == diff)
                                    {
                                        item.Condition = ELM_Right_Paint.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = ELM_Right_Paint.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "ELM" && item.AiGrpType == "Thermoplastic" && item.AiBound == "R" && item.FromCHKm == diff)
                                    {
                                        item.Condition = ELM_Right_ThermoplasticList.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;
                                        Boolean isenable = ELM_Right_ThermoplasticList.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }
                                    }
                                }

                                diff = decimal.Add(diff, (decimal)0.100);
                            }


                            //if (type != "save")
                            //{
                            //    foreach (var item in HeaderItemResponse.data.InsDtl)
                            //    {
                            //        if (item.Condition == null)
                            //        {
                            //            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                            //            return;
                            //        }
                            //    }

                            //}

                            var response = await _restApi.UpdateFC(HeaderItemResponse.data);
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

        public ICommand EntryTextChangedCommand
        {
            get
            {
                return new Command(async (obj) =>
                {
                    switch(obj as string)
                    {
                        case "ELM_Left_Paint":
                            ELM_Left_Paint_Total = ELM_Left_Paint_1 = ELM_Left_Paint_2 = ELM_Left_Paint_3 = 0.ToString();

                            foreach (var item in ELM_Left_Paint)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        ELM_Left_Paint_1 = decimal.Add(Convert.ToDecimal(ELM_Left_Paint_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        ELM_Left_Paint_2 = decimal.Add(Convert.ToDecimal(ELM_Left_Paint_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        ELM_Left_Paint_3 = decimal.Add(Convert.ToDecimal(ELM_Left_Paint_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var ELM_Left_Paint_SubTotal = decimal.Add(Convert.ToDecimal(ELM_Left_Paint_1), Convert.ToDecimal(ELM_Left_Paint_2));
                            ELM_Left_Paint_Total = decimal.Add(ELM_Left_Paint_SubTotal, Convert.ToDecimal(ELM_Left_Paint_3)).ToString();

                            ELM_Left_Paint_Total = ELM_Left_Paint_Total == "0" ? "" : ELM_Left_Paint_Total;
                            ELM_Left_Paint_1 = ELM_Left_Paint_1 == "0" ? "" : ELM_Left_Paint_1;
                            ELM_Left_Paint_2 = ELM_Left_Paint_2 == "0" ? "" : ELM_Left_Paint_2;
                            ELM_Left_Paint_3 = ELM_Left_Paint_3 == "0" ? "" : ELM_Left_Paint_3;

                            break;

                        case "ELM_Left_Thermoplastic":
                            ELM_Left_Thermoplastic_Total = ELM_Left_Thermoplastic_1 = ELM_Left_Thermoplastic_2 = ELM_Left_Thermoplastic_3 = 0.ToString();

                            foreach (var item in ELM_Left_Thermoplastic)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        ELM_Left_Thermoplastic_1 = decimal.Add(Convert.ToDecimal(ELM_Left_Thermoplastic_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        ELM_Left_Thermoplastic_2 = decimal.Add(Convert.ToDecimal(ELM_Left_Thermoplastic_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        ELM_Left_Thermoplastic_3 = decimal.Add(Convert.ToDecimal(ELM_Left_Thermoplastic_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var ELM_Left_Thermoplastic_SubTotal = decimal.Add(Convert.ToDecimal(ELM_Left_Thermoplastic_1) , Convert.ToDecimal(ELM_Left_Thermoplastic_2));
                            ELM_Left_Thermoplastic_Total = decimal.Add(ELM_Left_Thermoplastic_SubTotal , Convert.ToDecimal(ELM_Left_Thermoplastic_3)).ToString();

                            ELM_Left_Thermoplastic_Total = ELM_Left_Thermoplastic_Total == "0" ? "" : ELM_Left_Thermoplastic_Total;
                            ELM_Left_Thermoplastic_1 = ELM_Left_Thermoplastic_1 == "0" ? "" : ELM_Left_Thermoplastic_1;
                            ELM_Left_Thermoplastic_2 = ELM_Left_Thermoplastic_2 == "0" ? "" : ELM_Left_Thermoplastic_2;
                            ELM_Left_Thermoplastic_3 = ELM_Left_Thermoplastic_3 == "0" ? "" : ELM_Left_Thermoplastic_3;

                            break;

                        case "ELM_Left_RoadStuds":
                            ELM_Left_RoadStuds_Total = ELM_Left_RoadStuds_1 = ELM_Left_RoadStuds_2 = ELM_Left_RoadStuds_3 = 0.ToString();

                            foreach (var item in ELM_Left_RoadStuds)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        ELM_Left_RoadStuds_1 = decimal.Add(Convert.ToDecimal(ELM_Left_RoadStuds_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        ELM_Left_RoadStuds_2 = decimal.Add(Convert.ToDecimal(ELM_Left_RoadStuds_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        ELM_Left_RoadStuds_3 = decimal.Add(Convert.ToDecimal(ELM_Left_RoadStuds_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var ELM_Left_RoadStuds_SubTotal = decimal.Add(Convert.ToDecimal(ELM_Left_RoadStuds_1), Convert.ToDecimal(ELM_Left_RoadStuds_2));
                            ELM_Left_RoadStuds_Total = decimal.Add(ELM_Left_RoadStuds_SubTotal, Convert.ToDecimal(ELM_Left_RoadStuds_3)).ToString();

                            ELM_Left_RoadStuds_Total = ELM_Left_RoadStuds_Total == "0" ? "" : ELM_Left_RoadStuds_Total;
                            ELM_Left_RoadStuds_1 = ELM_Left_RoadStuds_1 == "0" ? "" : ELM_Left_RoadStuds_1;
                            ELM_Left_RoadStuds_2 = ELM_Left_RoadStuds_2 == "0" ? "" : ELM_Left_RoadStuds_2;
                            ELM_Left_RoadStuds_3 = ELM_Left_RoadStuds_3 == "0" ? "" : ELM_Left_RoadStuds_3;

                            break;

                        case "CW_Asphalt":
                            CW_Asphalt_Total = CW_Asphalt_1 = CW_Asphalt_2 = CW_Asphalt_3 = 0.ToString();

                            foreach (var item in CW_Asphalt)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        CW_Asphalt_1 = decimal.Add(Convert.ToDecimal(CW_Asphalt_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        CW_Asphalt_2 = decimal.Add(Convert.ToDecimal(CW_Asphalt_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        CW_Asphalt_3 = decimal.Add(Convert.ToDecimal(CW_Asphalt_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var CW_Asphalt_SubTotal = decimal.Add(Convert.ToDecimal(CW_Asphalt_1), Convert.ToDecimal(CW_Asphalt_2));
                            CW_Asphalt_Total = decimal.Add(CW_Asphalt_SubTotal, Convert.ToDecimal(CW_Asphalt_3)).ToString();

                            CW_Asphalt_Total = CW_Asphalt_Total == "0" ? "" : CW_Asphalt_Total;
                            CW_Asphalt_1 = CW_Asphalt_1 == "0" ? "" : CW_Asphalt_1;
                            CW_Asphalt_2 = CW_Asphalt_2 == "0" ? "" : CW_Asphalt_2;
                            CW_Asphalt_3 = CW_Asphalt_3 == "0" ? "" : CW_Asphalt_3;

                            break;

                        case "CW_Surface":
                            CW_Surface_Total = CW_Surface_1 = CW_Surface_2 = CW_Surface_3 = 0.ToString();

                            foreach (var item in CW_Surface)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        CW_Surface_1 = decimal.Add(Convert.ToDecimal(CW_Surface_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        CW_Surface_2 = decimal.Add(Convert.ToDecimal(CW_Surface_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        CW_Surface_3 = decimal.Add(Convert.ToDecimal(CW_Surface_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var CW_Surface_SubTotal = decimal.Add(Convert.ToDecimal(CW_Surface_1), Convert.ToDecimal(CW_Surface_2));
                            CW_Surface_Total = decimal.Add(CW_Surface_SubTotal, Convert.ToDecimal(CW_Surface_3)).ToString();

                            CW_Surface_Total = CW_Surface_Total == "0" ? "" : CW_Surface_Total;
                            CW_Surface_1 = CW_Surface_1 == "0" ? "" : CW_Surface_1;
                            CW_Surface_2 = CW_Surface_2 == "0" ? "" : CW_Surface_2;
                            CW_Surface_3 = CW_Surface_3 == "0" ? "" : CW_Surface_3;

                            break;

                        case "CW_Gravel":
                            CW_Gravel_Total = CW_Gravel_1 = CW_Gravel_2 = CW_Gravel_3 = 0.ToString();

                            foreach (var item in CW_Gravel)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        CW_Gravel_1 = decimal.Add(Convert.ToDecimal(CW_Gravel_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        CW_Gravel_2 = decimal.Add(Convert.ToDecimal(CW_Gravel_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        CW_Gravel_3 = decimal.Add(Convert.ToDecimal(CW_Gravel_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var CW_Gravel_SubTotal = decimal.Add(Convert.ToDecimal(CW_Gravel_1), Convert.ToDecimal(CW_Gravel_2));
                            CW_Gravel_Total = decimal.Add(CW_Gravel_SubTotal, Convert.ToDecimal(CW_Gravel_3)).ToString();

                            CW_Gravel_Total = CW_Gravel_Total == "0" ? "" : CW_Gravel_Total;
                            CW_Gravel_1 = CW_Gravel_1 == "0" ? "" : CW_Gravel_1;
                            CW_Gravel_2 = CW_Gravel_2 == "0" ? "" : CW_Gravel_2;
                            CW_Gravel_3 = CW_Gravel_3 == "0" ? "" : CW_Gravel_3;

                            break;

                        case "CW_Earth":
                            CW_Earth_Total = CW_Earth_1 = CW_Earth_2 = CW_Earth_3 = 0.ToString();

                            foreach (var item in CW_Earth)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        CW_Earth_1 = decimal.Add(Convert.ToDecimal(CW_Earth_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        CW_Earth_2 = decimal.Add(Convert.ToDecimal(CW_Earth_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        CW_Earth_3 = decimal.Add(Convert.ToDecimal(CW_Earth_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var CW_Earth_SubTotal = decimal.Add(Convert.ToDecimal(CW_Earth_1), Convert.ToDecimal(CW_Earth_2));
                            CW_Earth_Total = decimal.Add(CW_Earth_SubTotal, Convert.ToDecimal(CW_Earth_3)).ToString();

                            CW_Earth_Total = CW_Earth_Total == "0" ? "" : CW_Earth_Total;
                            CW_Earth_1 = CW_Earth_1 == "0" ? "" : CW_Earth_1;
                            CW_Earth_2 = CW_Earth_2 == "0" ? "" : CW_Earth_2;
                            CW_Earth_3 = CW_Earth_3 == "0" ? "" : CW_Earth_3;
                            break;

                        case "CW_Concrete":
                            CW_Concrete_Total = CW_Concrete_1 = CW_Concrete_2 = CW_Concrete_3 = 0.ToString();

                            foreach (var item in CW_Concrete)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        CW_Concrete_1 = decimal.Add(Convert.ToDecimal(CW_Concrete_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        CW_Concrete_2 = decimal.Add(Convert.ToDecimal(CW_Concrete_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        CW_Concrete_3 = decimal.Add(Convert.ToDecimal(CW_Concrete_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var CW_Concrete_SubTotal = decimal.Add(Convert.ToDecimal(CW_Concrete_1), Convert.ToDecimal(CW_Concrete_2));
                            CW_Concrete_Total = decimal.Add(CW_Concrete_SubTotal, Convert.ToDecimal(CW_Concrete_3)).ToString();

                            CW_Concrete_Total = CW_Concrete_Total == "0" ? "" : CW_Concrete_Total;
                            CW_Concrete_1 = CW_Concrete_1 == "0" ? "" : CW_Concrete_1;
                            CW_Concrete_2 = CW_Concrete_2 == "0" ? "" : CW_Concrete_2;
                            CW_Concrete_3 = CW_Concrete_3 == "0" ? "" : CW_Concrete_3;

                            break;

                        case "CW_Sand":
                            CW_Sand_Total = CW_Sand_1 = CW_Sand_2 = CW_Sand_3 = 0.ToString();

                            foreach (var item in CW_Sand)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        CW_Sand_1 = decimal.Add(Convert.ToDecimal(CW_Sand_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        CW_Sand_2 = decimal.Add(Convert.ToDecimal(CW_Sand_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        CW_Sand_3 = decimal.Add(Convert.ToDecimal(CW_Sand_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var CW_Sand_SubTotal = decimal.Add(Convert.ToDecimal(CW_Sand_1), Convert.ToDecimal(CW_Sand_2));
                            CW_Sand_Total = decimal.Add(CW_Sand_SubTotal, Convert.ToDecimal(CW_Sand_3)).ToString();

                            CW_Sand_Total = CW_Sand_Total == "0" ? "" : CW_Sand_Total;
                            CW_Sand_1 = CW_Sand_1 == "0" ? "" : CW_Sand_1;
                            CW_Sand_2 = CW_Sand_2 == "0" ? "" : CW_Sand_2;
                            CW_Sand_3 = CW_Sand_3 == "0" ? "" : CW_Sand_3;

                            break;

                        case "CW_Center_RoadStuds":
                            CW_Center_RoadStuds_Total = CW_Center_RoadStuds_1 = CW_Center_RoadStuds_2 = CW_Center_RoadStuds_3 = 0.ToString();

                            foreach (var item in CW_Center_RoadStuds)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        CW_Center_RoadStuds_1 = decimal.Add(Convert.ToDecimal(CW_Center_RoadStuds_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        CW_Center_RoadStuds_2 = decimal.Add(Convert.ToDecimal(CW_Center_RoadStuds_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        CW_Center_RoadStuds_3 = decimal.Add(Convert.ToDecimal(CW_Center_RoadStuds_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var CW_Center_RoadStuds_SubTotal = decimal.Add(Convert.ToDecimal(CW_Center_RoadStuds_1), Convert.ToDecimal(CW_Center_RoadStuds_2));
                            CW_Center_RoadStuds_Total = decimal.Add(CW_Center_RoadStuds_SubTotal, Convert.ToDecimal(CW_Center_RoadStuds_3)).ToString();

                            CW_Center_RoadStuds_Total = CW_Center_RoadStuds_Total == "0" ? "" : CW_Center_RoadStuds_Total;
                            CW_Center_RoadStuds_1 = CW_Center_RoadStuds_1 == "0" ? "" : CW_Center_RoadStuds_1;
                            CW_Center_RoadStuds_2 = CW_Center_RoadStuds_2 == "0" ? "" : CW_Center_RoadStuds_2;
                            CW_Center_RoadStuds_3 = CW_Center_RoadStuds_3 == "0" ? "" : CW_Center_RoadStuds_3;

                            break;

                        case "CLM_Paint":
                            CLM_Paint_Total = CLM_Paint_1 = CLM_Paint_2 = CLM_Paint_3 = 0.ToString();

                            foreach (var item in CLM_Paint)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        CLM_Paint_1 = decimal.Add(Convert.ToDecimal(CLM_Paint_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        CLM_Paint_2 = decimal.Add(Convert.ToDecimal(CLM_Paint_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        CLM_Paint_3 = decimal.Add(Convert.ToDecimal(CLM_Paint_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var CLM_Paint_SubTotal = decimal.Add(Convert.ToDecimal(CLM_Paint_1), Convert.ToDecimal(CLM_Paint_2));
                            CLM_Paint_Total = decimal.Add(CLM_Paint_SubTotal, Convert.ToDecimal(CLM_Paint_3)).ToString();

                            CLM_Paint_Total = CLM_Paint_Total == "0" ? "" : CLM_Paint_Total;
                            CLM_Paint_1 = CLM_Paint_1 == "0" ? "" : CLM_Paint_1;
                            CLM_Paint_2 = CLM_Paint_2 == "0" ? "" : CLM_Paint_2;
                            CLM_Paint_3 = CLM_Paint_3 == "0" ? "" : CLM_Paint_3;

                            break;

                        case "CLM_Thermoplastic":
                            CLM_Thermoplastic_Total = CLM_Thermoplastic_1 = CLM_Thermoplastic_2 = CLM_Thermoplastic_3 = 0.ToString();

                            foreach (var item in CLM_Thermoplastic)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        CLM_Thermoplastic_1 = decimal.Add(Convert.ToDecimal(CLM_Thermoplastic_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        CLM_Thermoplastic_2 = decimal.Add(Convert.ToDecimal(CLM_Thermoplastic_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        CLM_Thermoplastic_3 = decimal.Add(Convert.ToDecimal(CLM_Thermoplastic_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var CLM_Thermoplastic_SubTotal = decimal.Add(Convert.ToDecimal(CLM_Thermoplastic_1), Convert.ToDecimal(CLM_Thermoplastic_2));
                            CLM_Thermoplastic_Total = decimal.Add(CLM_Thermoplastic_SubTotal, Convert.ToDecimal(CLM_Thermoplastic_3)).ToString();

                            CLM_Thermoplastic_Total = CLM_Thermoplastic_Total == "0" ? "" : CLM_Thermoplastic_Total;
                            CLM_Thermoplastic_1 = CLM_Thermoplastic_1 == "0" ? "" : CLM_Thermoplastic_1;
                            CLM_Thermoplastic_2 = CLM_Thermoplastic_2 == "0" ? "" : CLM_Thermoplastic_2;
                            CLM_Thermoplastic_3 = CLM_Thermoplastic_3 == "0" ? "" : CLM_Thermoplastic_3;

                            break;

                            case "ELM_Right_RoadStuds":
                            ELM_Right_RoadStuds_Total = ELM_Right_RoadStuds_1 = ELM_Right_RoadStuds_2 = ELM_Right_RoadStuds_3 = 0.ToString();

                            foreach (var item in ELM_Right_RoadStuds)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        ELM_Right_RoadStuds_1 = decimal.Add(Convert.ToDecimal(ELM_Right_RoadStuds_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        ELM_Right_RoadStuds_2 = decimal.Add(Convert.ToDecimal(ELM_Right_RoadStuds_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        ELM_Right_RoadStuds_3 = decimal.Add(Convert.ToDecimal(ELM_Right_RoadStuds_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var ELM_Right_RoadStuds_SubTotal = decimal.Add(Convert.ToDecimal(ELM_Right_RoadStuds_1), Convert.ToDecimal(ELM_Right_RoadStuds_2));
                            ELM_Right_RoadStuds_Total = decimal.Add(ELM_Right_RoadStuds_SubTotal, Convert.ToDecimal(ELM_Right_RoadStuds_3)).ToString();

                            ELM_Right_RoadStuds_Total = ELM_Right_RoadStuds_Total == "0" ? "" : ELM_Right_RoadStuds_Total;
                            ELM_Right_RoadStuds_1 = ELM_Right_RoadStuds_1 == "0" ? "" : ELM_Right_RoadStuds_1;
                            ELM_Right_RoadStuds_2 = ELM_Right_RoadStuds_2 == "0" ? "" : ELM_Right_RoadStuds_2;
                            ELM_Right_RoadStuds_3 = ELM_Right_RoadStuds_3 == "0" ? "" : ELM_Right_RoadStuds_3;
                            break;

                        case "ELM_Right_Paint":
                            ELM_Right_Paint_Total = ELM_Right_Paint_1 = ELM_Right_Paint_2 = ELM_Right_Paint_3 = 0.ToString();

                            foreach (var item in ELM_Right_Paint)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        ELM_Right_Paint_1 = decimal.Add(Convert.ToDecimal(ELM_Right_Paint_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        ELM_Right_Paint_2 = decimal.Add(Convert.ToDecimal(ELM_Right_Paint_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        ELM_Right_Paint_3 = decimal.Add(Convert.ToDecimal(ELM_Right_Paint_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var ELM_Right_Paint_SubTotal = decimal.Add(Convert.ToDecimal(ELM_Right_Paint_1), Convert.ToDecimal(ELM_Right_Paint_2));
                            ELM_Right_Paint_Total = decimal.Add(ELM_Right_Paint_SubTotal, Convert.ToDecimal(ELM_Right_Paint_3)).ToString();

                            ELM_Right_Paint_Total = ELM_Right_Paint_Total == "0" ? "" : ELM_Right_Paint_Total;
                            ELM_Right_Paint_1 = ELM_Right_Paint_1 == "0" ? "" : ELM_Right_Paint_1;
                            ELM_Right_Paint_2 = ELM_Right_Paint_2 == "0" ? "" : ELM_Right_Paint_2;
                            ELM_Right_Paint_3 = ELM_Right_Paint_3 == "0" ? "" : ELM_Right_Paint_3;

                            break;

                        case "ELM_Right_ThermoplasticList":
                            ELM_Right_ThermoplasticList_Total = ELM_Right_ThermoplasticList_1 = ELM_Right_ThermoplasticList_2 = ELM_Right_ThermoplasticList_3 = 0.ToString();

                            foreach (var item in ELM_Right_ThermoplasticList)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        ELM_Right_ThermoplasticList_1 = decimal.Add(Convert.ToDecimal(ELM_Right_ThermoplasticList_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        ELM_Right_ThermoplasticList_2 = decimal.Add(Convert.ToDecimal(ELM_Right_ThermoplasticList_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        ELM_Right_ThermoplasticList_3 = decimal.Add(Convert.ToDecimal(ELM_Right_ThermoplasticList_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var ELM_Right_ThermoplasticList_SubTotal = decimal.Add(Convert.ToDecimal(ELM_Right_ThermoplasticList_1), Convert.ToDecimal(ELM_Right_ThermoplasticList_2));
                            ELM_Right_ThermoplasticList_Total = decimal.Add(ELM_Right_ThermoplasticList_SubTotal, Convert.ToDecimal(ELM_Right_ThermoplasticList_3)).ToString();

                            ELM_Right_ThermoplasticList_Total = ELM_Right_ThermoplasticList_Total == "0" ? "" : ELM_Right_ThermoplasticList_Total;
                            ELM_Right_ThermoplasticList_1 = ELM_Right_ThermoplasticList_1 == "0" ? "" : ELM_Right_ThermoplasticList_1;
                            ELM_Right_ThermoplasticList_2 = ELM_Right_ThermoplasticList_2 == "0" ? "" : ELM_Right_ThermoplasticList_2;
                            ELM_Right_ThermoplasticList_3 = ELM_Right_ThermoplasticList_3 == "0" ? "" : ELM_Right_ThermoplasticList_3;

                            break;
                    }
                });
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



                    //if (SelectedInspIndex == "Others" == SelectedCrewIndex = -1)
                    //{


                    //    _userDialogs.Alert("Crew Leader Name field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                    //    return;
                    //}



                    if (SelectedCrewIndex == -1)
                    {
                        _userDialogs.Alert("Crew Leader ID field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                   if (CrewName==null)
                    {
                        _userDialogs.Alert("Crew Leader Name field is required. ", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedInspIndex == -1)
                    {
                        _userDialogs.Alert("Inspected By User ID field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (InspName==null)
                    {
                        _userDialogs.Alert("Inspected By User Name field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if(InspDesignation == null)
                    {
                        _userDialogs.Alert("Inspected By User Designation field is required. ", "RAMS", "Ok");
                        return;
                    }

                    if (InspDate == null)
                    {
                        _userDialogs.Alert("Inspected By date is required.", "RAMS", "Ok");
                        return;
                    }



                    SaveSignature("submit");
                });
            }
        }

    }
    public class GridBoxData : FreshBasePageModel
    {
        public string FrmCH { get; set; }
        public string ToCH { get; set; }
        public string Text { get; set; }
        public string BoxNo { get; set; }
        private int? _condition = null;
        public int? Condition 
        { 
            get 
            {
                return _condition;
            }
            set
            {
                if (value == null || value == 0)
                {
                    _condition = null;
                }
                else if ((!_condition.Equals(value) && value.Value <= 3))
                {
                    _condition = value;
                }
                else if(value >3)
                {
                    _condition = null;
                    //value = _condition;
                    UserDialogs.Instance.Alert("Please enter the value ranges between 1 to 3");
                  
                }
                RaisePropertyChanged();
            }
        }

        public bool IsEnable
        {
            get
            {

                if (App.ReturnType != "View")
                    return Detail.Count == 1 ? true : false;
                else
                    return false;
            }
        }
        //public bool IsEnable { get { return Detail.Count == 1 ? true : false; } }
        public IList<FormFCDetailsDTO> Detail { get; set; }
        public Color Color { get { return IsEnable == true ? Color.White : Color.LightGray; } }
    }


    public class R
    {
        public string Id { get; set; }
        public string Desc { get; set; }
        public string Value { get; set; }
        public string LAvgWidth { get; set; }
        public string AvgWidth { get; set; }
        public string RAvgWidth { get; set; }
    }

    public class ELM
    {
        public string Id { get; set; }
        public string Desc { get; set; }
        public string Value { get; set; }
        public string LAvgWidth { get; set; }
        public string RAvgWidth { get; set; }
    }

    public class CLM
    {
        public string Id { get; set; }
        public string Desc { get; set; }
        public string Value { get; set; }
        public string AvgWidth { get; set; }
    }

    public class CW
    {
        public string Id { get; set; }
        public string Desc { get; set; }
        public string Value { get; set; }
        public string AvgWidth { get; set; }
    }

    public class Root
    {
        public List<R> RS { get; set; }
        public List<ELM> ELM { get; set; }
        public List<CLM> CLM { get; set; }
        public List<CW> CW { get; set; }
    }
}
