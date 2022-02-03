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
    public class FormFDAddPageModel : FreshBasePageModel
    {
        private IRestApi _restApi;
        private IUserDialogs _userDialogs;
        private ILocalDatabase _localDatabase;
        private bool isModify;
        private bool isDelete;
        private bool isView;
        private bool _fromAdd;
        private bool _fromAdd1;

        private AssetDDLResponseDTO.DropDown _selectedRMU;
        private AssetDDLResponseDTO.DropDown _selectedRoadCode;
        private AssetDDLResponseDTO.DropDown _selectedSectionCode;
        private int _selectedDivision = -1;
        private int _selectedYear = -1;
        private int _selectedCrewIndex = -1;
        private int _selectedInspIndex = -1;
        SignaturePadView InspPadView;
        public bool IsAdd { get; set; }
        public bool IsHeaderEnable { get; set; } = true;
        public bool CanSave { get; set; } = false;
        public bool IsView { get; set; } = false;
        public string SelectedRefNo { get; set; }
        public bool IsCrewNameEnabled { get; set; }
        public bool IsInspNameEnabled { get; set; }
        public string InspName { get; set; }
        public bool AverageEnable { get; set; }
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




        private double? avgWidth_Left_Ditch_1 = null;



        public double? AvgWidth_Left_Ditch
        {
            get { return avgWidth_Left_Ditch_1; }
            set
            {
                if (value == null || value == 0)
                {
                    avgWidth_Left_Ditch_1 = null;
                }
                else if (!avgWidth_Left_Ditch_1.HasValue || !avgWidth_Left_Ditch_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        avgWidth_Left_Ditch_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        avgWidth_Left_Ditch_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }




        private double? _avgWidth_Left_Drain_Earth_1 = null;



        public double? AvgWidth_Left_Drain_Earth
        {
            get { return _avgWidth_Left_Drain_Earth_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Left_Drain_Earth_1 = null;
                }
                else if (!_avgWidth_Left_Drain_Earth_1.HasValue || !_avgWidth_Left_Drain_Earth_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Left_Drain_Earth_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Left_Drain_Earth_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }


        private double? _avgWidth_Left_Drain_Concrete_1 = null;



        public double? AvgWidth_Left_Drain_Concrete
        {
            get { return _avgWidth_Left_Drain_Concrete_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Left_Drain_Concrete_1 = null;
                }
                else if (!_avgWidth_Left_Drain_Concrete_1.HasValue || !_avgWidth_Left_Drain_Concrete_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Left_Drain_Concrete_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Left_Drain_Concrete_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }


        private double? _avgWidth_Left_Drain_BlockStone_1 = null;



        public double? AvgWidth_Left_Drain_BlockStone
        {
            get { return _avgWidth_Left_Drain_BlockStone_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Left_Drain_BlockStone_1 = null;
                }
                else if (!_avgWidth_Left_Drain_BlockStone_1.HasValue || !_avgWidth_Left_Drain_BlockStone_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Left_Drain_BlockStone_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Left_Drain_BlockStone_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }


        private double? _avgWidth_Left_Shoulder_Asphalt_1 = null;



        public double? AvgWidth_Left_Shoulder_Asphalt
        {
            get { return _avgWidth_Left_Shoulder_Asphalt_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Left_Shoulder_Asphalt_1 = null;
                }
                else if (!_avgWidth_Left_Shoulder_Asphalt_1.HasValue || !_avgWidth_Left_Shoulder_Asphalt_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Left_Shoulder_Asphalt_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Left_Shoulder_Asphalt_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }


        private double? _avgWidth_Left_Shoulder_Gravel_1 = null;



        public double? AvgWidth_Left_Shoulder_Gravel
        {
            get { return _avgWidth_Left_Shoulder_Gravel_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Left_Shoulder_Gravel_1 = null;
                }
                else if (!_avgWidth_Left_Shoulder_Gravel_1.HasValue || !_avgWidth_Left_Shoulder_Gravel_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Left_Shoulder_Gravel_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Left_Shoulder_Gravel_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }


        private double? _avgWidth_Left_Shoulder_Earth_1 = null;



        public double? AvgWidth_Left_Shoulder_Earth
        {
            get { return _avgWidth_Left_Shoulder_Earth_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Left_Shoulder_Earth_1 = null;
                }
                else if (!_avgWidth_Left_Shoulder_Earth_1.HasValue || !_avgWidth_Left_Shoulder_Earth_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Left_Shoulder_Earth_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Left_Shoulder_Earth_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }


        private double? _avgWidth_Left_Shoulder_Concrete_1 = null;



        public double? AvgWidth_Left_Shoulder_Concrete
        {
            get { return _avgWidth_Left_Shoulder_Concrete_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Left_Shoulder_Concrete_1 = null;
                }
                else if (!_avgWidth_Left_Shoulder_Concrete_1.HasValue || !_avgWidth_Left_Shoulder_Concrete_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Left_Shoulder_Concrete_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Left_Shoulder_Concrete_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }

        private double? _avgWidth_Left_Shoulder_Kerb_1 = null;



        public double? AvgWidth_Left_Shoulder_Kerb
        {
            get { return _avgWidth_Left_Shoulder_Kerb_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Left_Shoulder_Kerb_1 = null;
                }
                else if (!_avgWidth_Left_Shoulder_Kerb_1.HasValue || !_avgWidth_Left_Shoulder_Kerb_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Left_Shoulder_Kerb_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Left_Shoulder_Kerb_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }


        private double? _avgWidth_Right_Ditch_1 = null;



        public double? AvgWidth_Right_Ditch
        {
            get { return _avgWidth_Right_Ditch_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Right_Ditch_1 = null;
                }
                else if (!_avgWidth_Right_Ditch_1.HasValue || !_avgWidth_Right_Ditch_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Right_Ditch_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Right_Ditch_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }

        private double? _avgWidth_Right_Drain_Earth_1 = null;



        public double? AvgWidth_Right_Drain_Earth
        {
            get { return _avgWidth_Right_Drain_Earth_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Right_Drain_Earth_1 = null;
                }
                else if (!_avgWidth_Right_Drain_Earth_1.HasValue || !_avgWidth_Right_Drain_Earth_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Right_Drain_Earth_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Right_Drain_Earth_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }


        private double? _avgWidth_Right_Drain_Concrete_1 = null;



        public double? AvgWidth_Right_Drain_Concrete
        {
            get { return _avgWidth_Right_Drain_Concrete_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Right_Drain_Concrete_1 = null;
                }
                else if (!_avgWidth_Right_Drain_Concrete_1.HasValue || !_avgWidth_Right_Drain_Concrete_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Right_Drain_Concrete_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Right_Drain_Concrete_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }


        private double? _avgWidth_Right_Drain_BlockStone_1 = null;



        public double? AvgWidth_Right_Drain_BlockStone
        {
            get { return _avgWidth_Right_Drain_BlockStone_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Right_Drain_BlockStone_1 = null;
                }
                else if (!_avgWidth_Right_Drain_BlockStone_1.HasValue || !_avgWidth_Right_Drain_BlockStone_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Right_Drain_BlockStone_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Right_Drain_BlockStone_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }


        private double? _avgWidth_Right_Shoulder_Asphalt_1 = null;



        public double? AvgWidth_Right_Shoulder_Asphalt
        {
            get { return _avgWidth_Right_Shoulder_Asphalt_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Right_Shoulder_Asphalt_1 = null;
                }
                else if (!_avgWidth_Right_Shoulder_Asphalt_1.HasValue || !_avgWidth_Right_Shoulder_Asphalt_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Right_Shoulder_Asphalt_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Right_Shoulder_Asphalt_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }


        private double? _avgWidth_Right_Shoulder_Gravel_1 = null;



        public double? AvgWidth_Right_Shoulder_Gravel
        {
            get { return _avgWidth_Right_Shoulder_Gravel_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Right_Shoulder_Gravel_1 = null;
                }
                else if (!_avgWidth_Right_Shoulder_Gravel_1.HasValue || !_avgWidth_Right_Shoulder_Gravel_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Right_Shoulder_Gravel_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Right_Shoulder_Gravel_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }


        private double? _avgWidth_Right_Shoulder_Earth_1 = null;



        public double? AvgWidth_Right_Shoulder_Earth
        {
            get { return _avgWidth_Right_Shoulder_Earth_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Right_Shoulder_Earth_1 = null;
                }
                else if (!_avgWidth_Right_Shoulder_Earth_1.HasValue || !_avgWidth_Right_Shoulder_Earth_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Right_Shoulder_Earth_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Right_Shoulder_Earth_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }



        private double? _avgWidth_Right_Shoulder_Concrete_1 = null;



        public double? AvgWidth_Right_Shoulder_Concrete
        {
            get { return _avgWidth_Right_Shoulder_Concrete_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Right_Shoulder_Concrete_1 = null;
                }
                else if (!_avgWidth_Right_Shoulder_Concrete_1.HasValue || !_avgWidth_Right_Shoulder_Concrete_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Right_Shoulder_Concrete_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Right_Shoulder_Concrete_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }

        private double? _avgWidth_Right_Shoulder_Kerb_1 = null;



        public double? AvgWidth_Right_Shoulder_Kerb
        {
            get { return _avgWidth_Right_Shoulder_Kerb_1; }
            set
            {
                if (value == null || value == 0)
                {
                    _avgWidth_Right_Shoulder_Kerb_1 = null;
                }
                else if (!_avgWidth_Right_Shoulder_Kerb_1.HasValue || !_avgWidth_Right_Shoulder_Kerb_1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _avgWidth_Right_Shoulder_Kerb_1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _avgWidth_Right_Shoulder_Kerb_1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
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
                SelectedRefNo = "CI/Form FD/" + SelectedRoadCode.Value + "/" + DDYearListItems[SelectedYear].Value;
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
        public IList<FormFDDetailsDTO> InsDtl { get; set; }
        public ObservableCollection<GridData> HeaderCollection { get; set; }
        public ObservableCollection<GridData> Left_Ditch { get; set; }
        public ObservableCollection<GridData> Left_Drain_Earth { get; set; }
        public ObservableCollection<GridData> Left_Drain_Concrete { get; set; }
        public ObservableCollection<GridData> Left_Drain_BlockStone { get; set; }
        public ObservableCollection<GridData> Left_Shoulder_Asphalt { get; set; }
        public ObservableCollection<GridData> Left_Shoulder_Gravel { get; set; }
        public ObservableCollection<GridData> Left_Shoulder_Earth { get; set; }
        public ObservableCollection<GridData> Left_Shoulder_Concrete { get; set; }
        public ObservableCollection<GridData> Left_Shoulder_Kerb { get; set; }
        public ObservableCollection<GridData> Right_Ditch { get; set; }
        public ObservableCollection<GridData> Right_Drain_Earth { get; set; }
        public ObservableCollection<GridData> Right_Drain_Concrete { get; set; }
        public ObservableCollection<GridData> Right_Drain_BlockStone { get; set; }
        public ObservableCollection<GridData> Right_Shoulder_Asphalt { get; set; }
        public ObservableCollection<GridData> Right_Shoulder_Gravel { get; set; }
        public ObservableCollection<GridData> Right_Shoulder_Earth { get; set; }
        public ObservableCollection<GridData> Right_Shoulder_Concrete { get; set; }
        public ObservableCollection<GridData> Right_Shoulder_Kerb { get; set; }

        public string Left_Ditch_1 { get; set; }
        public string Left_Ditch_2 { get; set; }
        public string Left_Ditch_3 { get; set; }
        public string Left_Ditch_Total { get; set; }

        public string Left_Drain_Earth_1 { get; set; }
        public string Left_Drain_Earth_2 { get; set; }
        public string Left_Drain_Earth_3 { get; set; }
        public string Left_Drain_Earth_Total { get; set; }

        public string Left_Drain_Concrete_1 { get; set; }
        public string Left_Drain_Concrete_2 { get; set; }
        public string Left_Drain_Concrete_3 { get; set; }
        public string Left_Drain_Concrete_Total { get; set; }

        public string Left_Drain_BlockStone_1 { get; set; }
        public string Left_Drain_BlockStone_2 { get; set; }
        public string Left_Drain_BlockStone_3 { get; set; }
        public string Left_Drain_BlockStone_Total { get; set; }

        public string Left_Shoulder_Asphalt_1 { get; set; }
        public string Left_Shoulder_Asphalt_2 { get; set; }
        public string Left_Shoulder_Asphalt_3 { get; set; }
        public string Left_Shoulder_Asphalt_Total { get; set; }

        public string Left_Shoulder_Gravel_1 { get; set; }
        public string Left_Shoulder_Gravel_2 { get; set; }
        public string Left_Shoulder_Gravel_3 { get; set; }
        public string Left_Shoulder_Gravel_Total { get; set; }

        public string Left_Shoulder_Earth_1 { get; set; }
        public string Left_Shoulder_Earth_2 { get; set; }
        public string Left_Shoulder_Earth_3 { get; set; }
        public string Left_Shoulder_Earth_Total { get; set; }

        public string Left_Shoulder_Concrete_1 { get; set; }
        public string Left_Shoulder_Concrete_2 { get; set; }
        public string Left_Shoulder_Concrete_3 { get; set; }
        public string Left_Shoulder_Concrete_Total { get; set; }

        public string Left_Shoulder_Kerb_1 { get; set; }
        public string Left_Shoulder_Kerb_2 { get; set; }
        public string Left_Shoulder_Kerb_3 { get; set; }
        public string Left_Shoulder_Kerb_Total { get; set; }

        public string Right_Ditch_1 { get; set; }
        public string Right_Ditch_2 { get; set; }
        public string Right_Ditch_3 { get; set; }
        public string Right_Ditch_Total { get; set; }

        public string Right_Drain_Earth_1 { get; set; }
        public string Right_Drain_Earth_2 { get; set; }
        public string Right_Drain_Earth_3 { get; set; }
        public string Right_Drain_Earth_Total { get; set; }

        public string Right_Drain_Concrete_1 { get; set; }
        public string Right_Drain_Concrete_2 { get; set; }
        public string Right_Drain_Concrete_3 { get; set; }
        public string Right_Drain_Concrete_Total { get; set; }

        public string Right_Drain_BlockStone_1 { get; set; }
        public string Right_Drain_BlockStone_2 { get; set; }
        public string Right_Drain_BlockStone_3 { get; set; }
        public string Right_Drain_BlockStone_Total { get; set; }

        public string Right_Shoulder_Asphalt_1 { get; set; }
        public string Right_Shoulder_Asphalt_2 { get; set; }
        public string Right_Shoulder_Asphalt_3 { get; set; }
        public string Right_Shoulder_Asphalt_Total { get; set; }

        public string Right_Shoulder_Gravel_1 { get; set; }
        public string Right_Shoulder_Gravel_2 { get; set; }
        public string Right_Shoulder_Gravel_3 { get; set; }
        public string Right_Shoulder_Gravel_Total { get; set; }
        
        public string Right_Shoulder_Earth_1 { get; set; }
        public string Right_Shoulder_Earth_2 { get; set; }
        public string Right_Shoulder_Earth_3 { get; set; }
        public string Right_Shoulder_Earth_Total { get; set; }

        public string Right_Shoulder_Concrete_1 { get; set; }
        public string Right_Shoulder_Concrete_2 { get; set; }
        public string Right_Shoulder_Concrete_3 { get; set; }
        public string Right_Shoulder_Concrete_Total { get; set; }

        public string Right_Shoulder_Kerb_1 { get; set; }
        public string Right_Shoulder_Kerb_2 { get; set; }
        public string Right_Shoulder_Kerb_3 { get; set; }
        public string Right_Shoulder_Kerb_Total { get; set; }

        public FormFDAddPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
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
                FromAdd1 = false;
                App.HeaderCode = 0;
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
                FromAdd1 = false;
                // App.HeaderCode = 0;
            }
            if (App.ReturnType == "Edit" || App.ReturnType == "View")
            {
                FromAdd = false;
                FromAdd1 = true;
                // App.HeaderCode = 0;
            }
            CanSave = App.ReturnType == "Edit" ? true : false;
            if (App.ReturnType == "Edit" || App.ReturnType == "View")
            {
                FDGridView = true;
                IsHeaderEnable = false;
                IsView = App.ReturnType == "View" ? true : false;
                await GetFDById(App.HeaderCode);


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
                              
                                if (userprp.ToLower() == "others" && App.ReturnType == "Edit")
                                {
                                    IsInspNameEnabled = true;
                                    InspName = (InspName != null ? response.data.UserName : null);
                                    InspDesignation = (InspDesignation != null ? response.data.Position : null);

                                    //InspName = InspName ?? response.data.UserName;
                                    //InspDesignation = InspDesignation ?? response.data.Position;
                                }
                                else
                                {
                                    IsInspNameEnabled = false;
                                    InspName = response.data.UserName;
                                    InspDesignation = response.data.Position;
                                   
                                }
                                if (App.ReturnType == "Add" && userprp.ToLower() == "others" )
                                {
                                    InspName = null;
                                    IsInspNameEnabled = true;
                                    InspDesignation = null;
                                    //InspName = (InspName != null ? response.data.UserName : null);
                                    //InspDesignation = (InspDesignation != null ? response.data.Position : null);


                                }


                            }
                            //else if (usertype == "crewuser")
                            //{
                            //    var userprp = DDCrewUserListListItems[SelectedCrewIndex].Text.Split('-')[1];
                            //    if (userprp.ToLower() == "others" && App.ReturnType == "Edit")
                            //    {
                            //        IsCrewNameEnabled = true;
                            //        CrewName = null;
                            //    }
                            //    else
                            //    {
                            //        IsCrewNameEnabled = false;
                            //        CrewName = response.data.UserName;

                            //    }
                            //    if (App.ReturnType == "Add" && userprp.ToLower() == "others")
                            //    {
                            //        CrewName = null;
                            //    }
                            //    // CrewDesignation = response.data.Position;
                            //}


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
                                    CrewName = response.data.UserName;
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

        private async Task<int> GetFDById(int headerCode)
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.GetFDById(headerCode);

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

       // public string AvgWidth_Left_Ditch { get; set; }
        //public string AvgWidth_Left_Drain_Earth { get; set; }
        //public string AvgWidth_Left_Drain_Concrete { get; set; }
        //public string AvgWidth_Left_Drain_BlockStone { get; set; }
        //public string AvgWidth_Left_Shoulder_Asphalt { get; set; }
        //public string AvgWidth_Left_Shoulder_Gravel { get; set; }
        //public string AvgWidth_Left_Shoulder_Earth { get; set; }
        //public string AvgWidth_Left_Shoulder_Concrete { get; set; }
//        public string AvgWidth_Left_Shoulder_Kerb { get; set; }

       // public string AvgWidth_Right_Ditch { get; set; }
        //public string AvgWidth_Right_Drain_Earth { get; set; }
        //public string AvgWidth_Right_Drain_Concrete { get; set; }
        //public string AvgWidth_Right_Drain_BlockStone { get; set; }
        //public string AvgWidth_Right_Shoulder_Asphalt { get; set; }
        //public string AvgWidth_Right_Shoulder_Gravel { get; set; }
        //public string AvgWidth_Right_Shoulder_Earth { get; set; }
        //public string AvgWidth_Right_Shoulder_Concrete { get; set; }
        //public string AvgWidth_Right_Shoulder_Kerb { get; set; }
        
        private void SetViewData(ResponseBaseObject<FormFDHeaderRequestDTO> response)
        {
            SelectedRefNo = response.data.FormRefId;

            FrmCh = response.data.FrmCh;
            InsDtl = response.data.InsDtl;
            PkRefNo = response.data.PkRefNo;
            Remarks = response.data.Remarks;
         
            SelectedRoadCode = DDRodeCodeListItems[DDRodeCodeListItems.ToList().FindIndex(x => x.Value == response.data.RoadCode)];


            SelectedRoadName = response.data.RoadName;
            ToCh = response.data.ToCh;
            YearOfInsp = response.data.YearOfInsp;
            SelectedYear = DDYearListItems.ToList().FindIndex(x => x.Value == response.data.YearOfInsp.ToString());

            int inspindex = DDInspUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.UserIdInspBy);
            SelectedInspIndex = inspindex;
            InspName = response.data.UserNameInspBy;
            InspDesignation = response.data.UserDesignationInspBy;
            InspDate = response.data.DtInsBy.HasValue ? response.data.DtInsBy.Value : (DateTime?)null;
            InspSign = ImageSource.FromStream(
                () => new MemoryStream(Convert.FromBase64String(response.data.SignpathInspBy)));

            int crewindex = DDCrewUserListListItems.ToList().FindIndex(a => Convert.ToInt32(a.Value) == response.data.CrewLeaderId);
            SelectedCrewIndex = crewindex;
            CrewName = response.data.CrewLeaderName;

            var Item = JsonConvert.DeserializeObject<Root1>(response.data.AssetTypes);

            if (Item.DI[0]?.LAvgWidth != "")
                AvgWidth_Left_Ditch = Convert.ToDouble(Item.DI[0]?.LAvgWidth);
            if (Item.DR[0]?.LAvgWidth != "")
                AvgWidth_Left_Drain_Earth = Convert.ToDouble(Item.DR[0]?.LAvgWidth);
            if (Item.DR[1]?.LAvgWidth != "")
                AvgWidth_Left_Drain_Concrete = Convert.ToDouble(Item.DR[1]?.LAvgWidth);
            if (Item.DR[2]?.LAvgWidth != "")
                AvgWidth_Left_Drain_BlockStone = Convert.ToDouble(Item.DR[2]?.LAvgWidth);
            if (Item.SH[0]?.LAvgWidth != "")
                AvgWidth_Left_Shoulder_Asphalt = Convert.ToDouble(Item.SH[0]?.LAvgWidth);
            if (Item.SH[1]?.LAvgWidth != "")
                AvgWidth_Left_Shoulder_Gravel = Convert.ToDouble(Item.SH[1]?.LAvgWidth);
            if (Item.SH[2]?.LAvgWidth != "")
                AvgWidth_Left_Shoulder_Earth = Convert.ToDouble(Item.SH[2]?.LAvgWidth);
            if (Item.SH[3]?.LAvgWidth != "")
                AvgWidth_Left_Shoulder_Concrete = Convert.ToDouble(Item.SH[3]?.LAvgWidth);
            if (Item.SH[4]?.LAvgWidth != "")
                AvgWidth_Left_Shoulder_Kerb = Convert.ToDouble(Item.SH[4]?.LAvgWidth);
            if (Item.DI[0]?.RAvgWidth != "")
                AvgWidth_Right_Ditch = Convert.ToDouble(Item.DI[0]?.RAvgWidth);
            if (Item.DR[0]?.RAvgWidth != "")
                AvgWidth_Right_Drain_Earth = Convert.ToDouble(Item.DR[0]?.RAvgWidth);
            if (Item.DR[1]?.RAvgWidth != "")
                AvgWidth_Right_Drain_Concrete = Convert.ToDouble(Item.DR[1]?.RAvgWidth);
            if (Item.DR[2]?.RAvgWidth != "")
                AvgWidth_Right_Drain_BlockStone = Convert.ToDouble(Item.DR[2]?.RAvgWidth);
            if (Item.SH[0]?.RAvgWidth != "")
                AvgWidth_Right_Shoulder_Asphalt = Convert.ToDouble(Item.SH[0]?.RAvgWidth);
            if (Item.SH[1]?.RAvgWidth != "")
                AvgWidth_Right_Shoulder_Gravel = Convert.ToDouble(Item.SH[1]?.RAvgWidth);
            if (Item.SH[2]?.RAvgWidth != "")
                AvgWidth_Right_Shoulder_Earth = Convert.ToDouble(Item.SH[2]?.RAvgWidth);
            if (Item.SH[3]?.RAvgWidth != "")
                AvgWidth_Right_Shoulder_Concrete = Convert.ToDouble(Item.SH[3]?.RAvgWidth);
            if (Item.SH[4]?.RAvgWidth != "")
                AvgWidth_Right_Shoulder_Kerb = Convert.ToDouble(Item.SH[4]?.RAvgWidth);

            GenerateTableData(response.data);
        }


        private void GenerateTableData(FormFDHeaderRequestDTO data)
        {
            string diff1 = string.Format("0.000", data.FrmCh);
           // decimal diff = (decimal)data.FrmCh;
            var count = 0;
            decimal diff =Convert.ToDecimal(diff1);

            HeaderCollection = new ObservableCollection<GridData>();

            Left_Ditch = new ObservableCollection<GridData>();
            Left_Drain_Earth = new ObservableCollection<GridData>();
            Left_Drain_Concrete = new ObservableCollection<GridData>();
            Left_Drain_BlockStone = new ObservableCollection<GridData>();
            Left_Shoulder_Asphalt = new ObservableCollection<GridData>();
            Left_Shoulder_Gravel = new ObservableCollection<GridData>();
            Left_Shoulder_Earth = new ObservableCollection<GridData>();
            Left_Shoulder_Concrete = new ObservableCollection<GridData>();
            Left_Shoulder_Kerb = new ObservableCollection<GridData>();

            Right_Ditch = new ObservableCollection<GridData>();
            Right_Drain_Earth = new ObservableCollection<GridData>();
            Right_Drain_Concrete = new ObservableCollection<GridData>();
            Right_Drain_BlockStone = new ObservableCollection<GridData>();
            Right_Shoulder_Asphalt = new ObservableCollection<GridData>();
            Right_Shoulder_Gravel = new ObservableCollection<GridData>();
            Right_Shoulder_Earth = new ObservableCollection<GridData>();
            Right_Shoulder_Concrete = new ObservableCollection<GridData>();
            Right_Shoulder_Kerb = new ObservableCollection<GridData>();


            var minValu = 499;
            var maxValu = 999;
            var toCHValue = data.ToCh.ToString().Split('.');

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
                var km1 = diff.ToString().Split('.');
                if (km1[1] == "099" | km1[1] == "199" | km1[1] == "299" | km1[1] == "399" | km1[1] == "499" | km1[1] == "599" | km1[1] == "699" | km1[1] == "799" | km1[1] == "899" | km1[1] == "999")
                {
                    diff = decimal.Add(diff, (decimal)0.001);
                }

                Left_Ditch.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "DI").Where(x => x.AiGrpType == "Gravel/Sand/Earth").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "DI").Where(x => x.AiGrpType == "Gravel/Sand/Earth").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "DI").Where(x => x.AiGrpType == "Gravel/Sand/Earth").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                Left_Drain_Earth.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Earth").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Earth").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Earth").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                Left_Drain_Concrete.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Concrete").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Concrete").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Concrete").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                Left_Drain_BlockStone.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Block Stone").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Block Stone").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Block Stone").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                Left_Shoulder_Asphalt.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Asphalt").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Asphalt").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Asphalt").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                Left_Shoulder_Gravel.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Gravel").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Gravel").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Gravel").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList()[0].Condition : null });
                Left_Shoulder_Earth.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Earth").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Earth").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Earth").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList()[0].Condition : null });
                Left_Shoulder_Concrete.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Concrete").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Concrete").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Concrete").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                Left_Shoulder_Kerb.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Footpath/Kerb").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Footpath/Kerb").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Footpath/Kerb").Where(x => x.AiBound == "L").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });

                Right_Ditch.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "DI").Where(x => x.AiGrpType == "Gravel/Sand/Earth").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "DI").Where(x => x.AiGrpType == "Gravel/Sand/Earth").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "DI").Where(x => x.AiGrpType == "Gravel/Sand/Earth").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                Right_Drain_Earth.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Earth").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Earth").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Earth").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                Right_Drain_Concrete.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Concrete").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Concrete").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Concrete").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                Right_Drain_BlockStone.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Block Stone").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Block Stone").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "DR").Where(x => x.AiGrpType == "Block Stone").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                Right_Shoulder_Asphalt.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Asphalt").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Asphalt").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Asphalt").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                Right_Shoulder_Gravel.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Gravel").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Gravel").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Gravel").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                Right_Shoulder_Earth.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Earth").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Earth").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Earth").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                Right_Shoulder_Concrete.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Concrete").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Concrete").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Concrete").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });
                Right_Shoulder_Kerb.Add(new GridData() { BoxNo = diff.ToString(), FrmCH = diff.ToString(), Detail = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Footpath/Kerb").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList(), Condition = data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Footpath/Kerb").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList().Count > 0 ? data.InsDtl.Where(x => x.AiAssetGrpCode == "SH").Where(x => x.AiGrpType == "Footpath/Kerb").Where(x => x.AiBound == "R").Where(x => x.FromCHKm == diff).ToList()[0]?.Condition : null });


                //if (Left_Ditch.Count() % 5 == 0)
                //{
                //    HeaderCollection.Add(new GridData() { FrmCH = Left_Ditch[Left_Ditch.Count() - 5].FrmCH, ToCH = diff.ToString() });
                //}

                count++;
                diff = decimal.Add(diff, (decimal)0.100);
                var km = diff.ToString().Split('.');
                if (km[1] == "000" | km[1] == "100" | km[1] == "200" | km[1] == "300" | km[1] == "400" | km[1] == "500" | km[1] == "600" | km[1] == "700" | km[1] == "800" | km[1] == "900")
                {
                    diff = decimal.Subtract(diff, (decimal)0.001);
                }
                else
                {
                    diff = decimal.Add(diff, (decimal)0.001);
                    diff = decimal.Subtract(diff, (decimal)0.001);

                }

                if (Left_Ditch.Count() % 5 == 0)
                {

                    var fromch = Left_Ditch[Left_Ditch.Count() - 5].FrmCH;
                    if (fromch != "0.000")
                    {
                        decimal ifromch = Convert.ToDecimal(fromch);

                        // decimal ifromch = decimal.Add(Convert.ToDecimal(fromch), (decimal)0.001);
                        HeaderCollection.Add(new GridData() { FrmCH = ifromch.ToString().Replace(".", "+"), ToCH = diff.ToString().Replace(".", "+") });
                    }
                    else
                    {
                        decimal ifromch = Convert.ToDecimal(fromch);
                        HeaderCollection.Add(new GridData() { FrmCH = ifromch.ToString().Replace(".", "+"), ToCH = diff.ToString().Replace(".", "+") });
                    }

                }

            }

           

            //if(Left_Ditch.Count() %5 != 0)
            //{
            //    HeaderCollection.Add(new GridData() { FrmCH = decimal.Add( Convert.ToDecimal(HeaderCollection[HeaderCollection.Count() - 1].ToCH), (decimal)0.100).ToString(), ToCH = data.ToCh.ToString() });
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
                            await UserDialogs.Instance.AlertAsync("Please select Year", "RAMS", "OK");
                            return;
                        }

                        var response = SaveFormFDHeader();

                       // FromAdd = false;
                    }
                    catch
                    {
                    }


                });
            }
        }

        private async Task<ObservableCollection<FormFDHeaderRequestDTO>> SaveFormFDHeader()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");
                if (CrossConnectivity.Current.IsConnected)
                {
                    FormFDHeaderRequestDTO GridItems = new FormFDHeaderRequestDTO()
                    {
                       // PkRefNo = App.HeaderCode,
                      //  DivCode = DDDivisionListItems[SelectedDivision]?.Value,
                       // RmuName = SelectedRMU?.Text,
                        //SectionCode = SelectedSectionCode?.Code,
                        //SectionName = SelectedSectionName,
                        RoadId = SelectedRoadCode?.PKId,
                        RoadCode = SelectedRoadCode?.Value,
                        RoadName = SelectedRoadName,
                        YearOfInsp = Convert.ToInt32(DDYearListItems[SelectedYear].Value),
                    };

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(GridItems);

                   
                       // var response = await _restApi.SaveFDHdr(GridItems);
                    var response1 = await _restApi.AssetCheckFd(SelectedRoadCode.Value);

                    if (response1.success)
                    {
                        if (response1.data)
                        {

                            var response = await _restApi.SaveFDHdr(GridItems);

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
                              
                                FromAdd1 = false;
                                // App.HeaderCode = 0;
                            }
                        }
                    }

                }

                return null;
            }
            catch (Exception ex) { return null; }
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
                        var HeaderItemResponse = await _restApi.GetFDById(App.HeaderCode);

                        if (HeaderItemResponse.success)
                        {
                            HeaderItemResponse.data.PkRefNo = App.HeaderCode;
                            HeaderItemResponse.data.UserIdInspBy = SelectedInspIndex != -1 ? Convert.ToInt32(DDInspUserListListItems[SelectedInspIndex].Value) : (int?)null;
                            HeaderItemResponse.data.UserNameInspBy = InspName;
                            HeaderItemResponse.data.UserDesignationInspBy = InspDesignation;
                            HeaderItemResponse.data.DtInsBy = InspDate;
                            HeaderItemResponse.data.SignpathInspBy = inspSign ?? HeaderItemResponse.data.SignpathInspBy ?? null;
                            HeaderItemResponse.data.CrewLeaderId = SelectedCrewIndex != -1 ? Convert.ToInt32(DDCrewUserListListItems[SelectedCrewIndex].Value) : (int?)null;
                            HeaderItemResponse.data.CrewLeaderName = CrewName;
                            HeaderItemResponse.data.Remarks = Remarks;
                            HeaderItemResponse.data.SubmitSts = type == "save" ? false : true;

                            var Item = JsonConvert.DeserializeObject<Root1>(HeaderItemResponse.data.AssetTypes);

                            Item.DI[0].LAvgWidth = AvgWidth_Left_Ditch.ToString();
                            Item.DR[0].LAvgWidth = AvgWidth_Left_Drain_Earth.ToString();
                            Item.DR[1].LAvgWidth = AvgWidth_Left_Drain_Concrete.ToString();
                            Item.DR[2].LAvgWidth = AvgWidth_Left_Drain_BlockStone.ToString();
                            Item.SH[0].LAvgWidth = AvgWidth_Left_Shoulder_Asphalt.ToString();
                            Item.SH[1].LAvgWidth = AvgWidth_Left_Shoulder_Gravel.ToString();
                            Item.SH[2].LAvgWidth = AvgWidth_Left_Shoulder_Earth.ToString();
                            Item.SH[3].LAvgWidth = AvgWidth_Left_Shoulder_Concrete.ToString();
                            Item.SH[4].LAvgWidth = AvgWidth_Left_Shoulder_Kerb.ToString();

                            Item.DI[0].RAvgWidth = AvgWidth_Right_Ditch.ToString();
                            Item.DR[0].RAvgWidth = AvgWidth_Right_Drain_Earth.ToString();
                            Item.DR[1].RAvgWidth = AvgWidth_Right_Drain_Concrete.ToString();
                            Item.DR[2].RAvgWidth = AvgWidth_Right_Drain_BlockStone.ToString();
                            Item.SH[0].RAvgWidth = AvgWidth_Right_Shoulder_Asphalt.ToString();
                            Item.SH[1].RAvgWidth = AvgWidth_Right_Shoulder_Gravel.ToString();
                            Item.SH[2].RAvgWidth = AvgWidth_Right_Shoulder_Earth.ToString();
                            Item.SH[3].RAvgWidth = AvgWidth_Right_Shoulder_Concrete.ToString();
                            Item.SH[4].RAvgWidth = AvgWidth_Right_Shoulder_Kerb.ToString();

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
                                    if (item.AiAssetGrpCode == "DI" && item.AiGrpType == "Gravel/Sand/Earth" && item.AiBound == "L" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Left_Ditch.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = Left_Ditch.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "DR" && item.AiGrpType == "Earth" && item.AiBound == "L" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Left_Drain_Earth.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = Left_Drain_Earth.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "DR" && item.AiGrpType == "Concrete" && item.AiBound == "L" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Left_Drain_Concrete.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = Left_Drain_Concrete.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "DR" && item.AiGrpType == "Block Stone" && item.AiBound == "L" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Left_Drain_BlockStone.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;
                                        Boolean isenable = Left_Drain_BlockStone.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "SH" && item.AiGrpType == "Asphalt" && item.AiBound == "L" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Left_Shoulder_Asphalt.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = Left_Shoulder_Asphalt.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "SH" && item.AiGrpType == "Gravel" && item.AiBound == "L" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Left_Shoulder_Gravel.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = Left_Shoulder_Gravel.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "SH" && item.AiGrpType == "Earth" && item.AiBound == "L" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Left_Shoulder_Earth.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = Left_Shoulder_Earth.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }


                                    }
                                    if (item.AiAssetGrpCode == "SH" && item.AiGrpType == "Concrete" && item.AiBound == "L" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Left_Shoulder_Concrete.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = Left_Shoulder_Concrete.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }
                                    }
                                    if (item.AiAssetGrpCode == "SH" && item.AiGrpType == "Footpath/Kerb" && item.AiBound == "L" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Left_Shoulder_Kerb.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = Left_Shoulder_Kerb.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }
                                    }


                                    if (item.AiAssetGrpCode == "DI" && item.AiGrpType == "Gravel/Sand/Earth" && item.AiBound == "R" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Right_Ditch.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;
                                        Boolean isenable = Right_Ditch.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }
                                    }
                                    if (item.AiAssetGrpCode == "DR" && item.AiGrpType == "Earth" && item.AiBound == "R" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Right_Drain_Earth.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;
                                        Boolean isenable = Right_Drain_Earth.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }
                                    }
                                    if (item.AiAssetGrpCode == "DR" && item.AiGrpType == "Concrete" && item.AiBound == "R" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Right_Drain_Concrete.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = Right_Drain_Concrete.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "DR" && item.AiGrpType == "Block Stone" && item.AiBound == "R" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Right_Drain_BlockStone.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = Right_Drain_BlockStone.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }
                                    }
                                    if (item.AiAssetGrpCode == "SH" && item.AiGrpType == "Asphalt" && item.AiBound == "R" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Right_Shoulder_Asphalt.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = Right_Shoulder_Asphalt.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "SH" && item.AiGrpType == "Gravel" && item.AiBound == "R" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Right_Shoulder_Gravel.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = Right_Shoulder_Gravel.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }
                                    }
                                    if (item.AiAssetGrpCode == "SH" && item.AiGrpType == "Earth" && item.AiBound == "R" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Right_Shoulder_Earth.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = Right_Shoulder_Earth.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }

                                    }
                                    if (item.AiAssetGrpCode == "SH" && item.AiGrpType == "Concrete" && item.AiBound == "R" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Right_Shoulder_Concrete.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = Right_Shoulder_Concrete.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
                                        if (isenable && (item.Condition == null) && type != "save")
                                        {
                                            await UserDialogs.Instance.AlertAsync("Condition is Required.", "RAMS", "OK");
                                            return;
                                        }
                                    }
                                    if (item.AiAssetGrpCode == "SH" && item.AiGrpType == "Footpath/Kerb" && item.AiBound == "R" && item.FromCHKm == diff)
                                    {
                                        item.Condition = Right_Shoulder_Kerb.Where(x => x.BoxNo == diff.ToString()).ToList()[0].Condition;

                                        Boolean isenable = Right_Shoulder_Kerb.Where(x => x.BoxNo == diff.ToString()).ToList()[0].IsEnable;
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


                            var response = await _restApi.UpdateFD(HeaderItemResponse.data);
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
                    switch (obj as string)
                    {
                        case "Left_Ditch":
                            Left_Ditch_Total = Left_Ditch_1 = Left_Ditch_2 = Left_Ditch_3 = 0.ToString();

                            foreach (var item in Left_Ditch)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Left_Ditch_1 = decimal.Add(Convert.ToDecimal(Left_Ditch_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Left_Ditch_2 = decimal.Add(Convert.ToDecimal(Left_Ditch_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Left_Ditch_3 = decimal.Add(Convert.ToDecimal(Left_Ditch_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Left_Ditch_SubTotal = decimal.Add(Convert.ToDecimal(Left_Ditch_1), Convert.ToDecimal(Left_Ditch_2));
                            Left_Ditch_Total = decimal.Add(Left_Ditch_SubTotal, Convert.ToDecimal(Left_Ditch_3)).ToString();

                            Left_Ditch_Total = Left_Ditch_Total == "0" ? "" : Left_Ditch_Total;
                            Left_Ditch_1 = Left_Ditch_1 == "0" ? "" : Left_Ditch_1;
                            Left_Ditch_2 = Left_Ditch_2 == "0" ? "" : Left_Ditch_2;
                            Left_Ditch_3 = Left_Ditch_3 == "0" ? "" : Left_Ditch_3;

                            break;

                        case "Left_Drain_Earth":
                            Left_Drain_Earth_Total = Left_Drain_Earth_1 = Left_Drain_Earth_2 = Left_Drain_Earth_3 = 0.ToString();

                            foreach (var item in Left_Drain_Earth)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Left_Drain_Earth_1 = decimal.Add(Convert.ToDecimal(Left_Drain_Earth_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Left_Drain_Earth_2 = decimal.Add(Convert.ToDecimal(Left_Drain_Earth_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Left_Drain_Earth_3 = decimal.Add(Convert.ToDecimal(Left_Drain_Earth_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Left_Drain_Earth_SubTotal = decimal.Add(Convert.ToDecimal(Left_Drain_Earth_1), Convert.ToDecimal(Left_Drain_Earth_2));
                            Left_Drain_Earth_Total = decimal.Add(Left_Drain_Earth_SubTotal, Convert.ToDecimal(Left_Drain_Earth_3)).ToString();

                            Left_Drain_Earth_Total = Left_Drain_Earth_Total == "0" ? "" : Left_Drain_Earth_Total;
                            Left_Drain_Earth_1 = Left_Drain_Earth_1 == "0" ? "" : Left_Drain_Earth_1;
                            Left_Drain_Earth_2 = Left_Drain_Earth_2 == "0" ? "" : Left_Drain_Earth_2;
                            Left_Drain_Earth_3 = Left_Drain_Earth_3 == "0" ? "" : Left_Drain_Earth_3;

                            break;

                        case "Left_Drain_Concrete":
                            Left_Drain_Concrete_Total = Left_Drain_Concrete_1 = Left_Drain_Concrete_2 = Left_Drain_Concrete_3 = 0.ToString();

                            foreach (var item in Left_Drain_Concrete)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Left_Drain_Concrete_1 = decimal.Add(Convert.ToDecimal(Left_Drain_Concrete_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Left_Drain_Concrete_2 = decimal.Add(Convert.ToDecimal(Left_Drain_Concrete_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Left_Drain_Concrete_3 = decimal.Add(Convert.ToDecimal(Left_Drain_Concrete_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Left_Drain_Concrete_SubTotal = decimal.Add(Convert.ToDecimal(Left_Drain_Concrete_1), Convert.ToDecimal(Left_Drain_Concrete_2));
                            Left_Drain_Concrete_Total = decimal.Add(Left_Drain_Concrete_SubTotal, Convert.ToDecimal(Left_Drain_Concrete_3)).ToString();

                            Left_Drain_Concrete_Total = Left_Drain_Concrete_Total == "0" ? "" : Left_Drain_Concrete_Total;
                            Left_Drain_Concrete_1 = Left_Drain_Concrete_1 == "0" ? "" : Left_Drain_Concrete_1;
                            Left_Drain_Concrete_2 = Left_Drain_Concrete_2 == "0" ? "" : Left_Drain_Concrete_2;
                            Left_Drain_Concrete_3 = Left_Drain_Concrete_3 == "0" ? "" : Left_Drain_Concrete_3;

                            break;

                        case "Left_Drain_BlockStone":
                            Left_Drain_BlockStone_Total = Left_Drain_BlockStone_1 = Left_Drain_BlockStone_2 = Left_Drain_BlockStone_3 = 0.ToString();

                            foreach (var item in Left_Drain_BlockStone)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Left_Drain_BlockStone_1 = decimal.Add(Convert.ToDecimal(Left_Drain_BlockStone_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Left_Drain_BlockStone_2 = decimal.Add(Convert.ToDecimal(Left_Drain_BlockStone_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Left_Drain_BlockStone_3 = decimal.Add(Convert.ToDecimal(Left_Drain_BlockStone_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Left_Drain_BlockStone_SubTotal = decimal.Add(Convert.ToDecimal(Left_Drain_BlockStone_1), Convert.ToDecimal(Left_Drain_BlockStone_2));
                            Left_Drain_BlockStone_Total = decimal.Add(Left_Drain_BlockStone_SubTotal, Convert.ToDecimal(Left_Drain_BlockStone_3)).ToString();

                            Left_Drain_BlockStone_Total = Left_Drain_BlockStone_Total == "0" ? "" : Left_Drain_BlockStone_Total;
                            Left_Drain_BlockStone_1 = Left_Drain_BlockStone_1 == "0" ? "" : Left_Drain_BlockStone_1;
                            Left_Drain_BlockStone_2 = Left_Drain_BlockStone_2 == "0" ? "" : Left_Drain_BlockStone_2;
                            Left_Drain_BlockStone_3 = Left_Drain_BlockStone_3 == "0" ? "" : Left_Drain_BlockStone_3;

                            break;

                        case "Left_Shoulder_Asphalt":
                            Left_Shoulder_Asphalt_Total = Left_Shoulder_Asphalt_1 = Left_Shoulder_Asphalt_2 = Left_Shoulder_Asphalt_3 = 0.ToString();

                            foreach (var item in Left_Shoulder_Asphalt)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Left_Shoulder_Asphalt_1 = decimal.Add(Convert.ToDecimal(Left_Shoulder_Asphalt_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Left_Shoulder_Asphalt_2 = decimal.Add(Convert.ToDecimal(Left_Shoulder_Asphalt_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Left_Shoulder_Asphalt_3 = decimal.Add(Convert.ToDecimal(Left_Shoulder_Asphalt_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Left_Shoulder_Asphalt_SubTotal = decimal.Add(Convert.ToDecimal(Left_Shoulder_Asphalt_1), Convert.ToDecimal(Left_Shoulder_Asphalt_2));
                            Left_Shoulder_Asphalt_Total = decimal.Add(Left_Shoulder_Asphalt_SubTotal, Convert.ToDecimal(Left_Shoulder_Asphalt_3)).ToString();

                            Left_Shoulder_Asphalt_Total = Left_Shoulder_Asphalt_Total == "0" ? "" : Left_Shoulder_Asphalt_Total;
                            Left_Shoulder_Asphalt_1 = Left_Shoulder_Asphalt_1 == "0" ? "" : Left_Shoulder_Asphalt_1;
                            Left_Shoulder_Asphalt_2 = Left_Shoulder_Asphalt_2 == "0" ? "" : Left_Shoulder_Asphalt_2;
                            Left_Shoulder_Asphalt_3 = Left_Shoulder_Asphalt_3 == "0" ? "" : Left_Shoulder_Asphalt_3;

                            break;

                        case "Left_Shoulder_Gravel":
                            Left_Shoulder_Gravel_Total = Left_Shoulder_Gravel_1 = Left_Shoulder_Gravel_2 = Left_Shoulder_Gravel_3 = 0.ToString();

                            foreach (var item in Left_Shoulder_Gravel)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Left_Shoulder_Gravel_1 = decimal.Add(Convert.ToDecimal(Left_Shoulder_Gravel_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Left_Shoulder_Gravel_2 = decimal.Add(Convert.ToDecimal(Left_Shoulder_Gravel_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Left_Shoulder_Gravel_3 = decimal.Add(Convert.ToDecimal(Left_Shoulder_Gravel_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Left_Shoulder_Gravel_SubTotal = decimal.Add(Convert.ToDecimal(Left_Shoulder_Gravel_1), Convert.ToDecimal(Left_Shoulder_Gravel_2));
                            Left_Shoulder_Gravel_Total = decimal.Add(Left_Shoulder_Gravel_SubTotal, Convert.ToDecimal(Left_Shoulder_Gravel_3)).ToString();

                            Left_Shoulder_Gravel_Total = Left_Shoulder_Gravel_Total == "0" ? "" : Left_Shoulder_Gravel_Total;
                            Left_Shoulder_Gravel_1 = Left_Shoulder_Gravel_1 == "0" ? "" : Left_Shoulder_Gravel_1;
                            Left_Shoulder_Gravel_2 = Left_Shoulder_Gravel_2 == "0" ? "" : Left_Shoulder_Gravel_2;
                            Left_Shoulder_Gravel_3 = Left_Shoulder_Gravel_3 == "0" ? "" : Left_Shoulder_Gravel_3;

                            break;

                        case "Left_Shoulder_Earth":
                            Left_Shoulder_Earth_Total = Left_Shoulder_Earth_1 = Left_Shoulder_Earth_2 = Left_Shoulder_Earth_3 = 0.ToString();

                            foreach (var item in Left_Shoulder_Earth)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Left_Shoulder_Earth_1 = decimal.Add(Convert.ToDecimal(Left_Shoulder_Earth_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Left_Shoulder_Earth_2 = decimal.Add(Convert.ToDecimal(Left_Shoulder_Earth_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Left_Shoulder_Earth_3 = decimal.Add(Convert.ToDecimal(Left_Shoulder_Earth_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Left_Shoulder_Earth_SubTotal = decimal.Add(Convert.ToDecimal(Left_Shoulder_Earth_1), Convert.ToDecimal(Left_Shoulder_Earth_2));
                            Left_Shoulder_Earth_Total = decimal.Add(Left_Shoulder_Earth_SubTotal, Convert.ToDecimal(Left_Shoulder_Earth_3)).ToString();

                            Left_Shoulder_Earth_Total = Left_Shoulder_Earth_Total == "0" ? "" : Left_Shoulder_Earth_Total;
                            Left_Shoulder_Earth_1 = Left_Shoulder_Earth_1 == "0" ? "" : Left_Shoulder_Earth_1;
                            Left_Shoulder_Earth_2 = Left_Shoulder_Earth_2 == "0" ? "" : Left_Shoulder_Earth_2;
                            Left_Shoulder_Earth_3 = Left_Shoulder_Earth_3 == "0" ? "" : Left_Shoulder_Earth_3;
                            break;

                        case "Left_Shoulder_Concrete":
                            Left_Shoulder_Concrete_Total = Left_Shoulder_Concrete_1 = Left_Shoulder_Concrete_2 = Left_Shoulder_Concrete_3 = 0.ToString();

                            foreach (var item in Left_Shoulder_Concrete)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Left_Shoulder_Concrete_1 = decimal.Add(Convert.ToDecimal(Left_Shoulder_Concrete_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Left_Shoulder_Concrete_2 = decimal.Add(Convert.ToDecimal(Left_Shoulder_Concrete_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Left_Shoulder_Concrete_3 = decimal.Add(Convert.ToDecimal(Left_Shoulder_Concrete_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Left_Shoulder_Concrete_SubTotal = decimal.Add(Convert.ToDecimal(Left_Shoulder_Concrete_1), Convert.ToDecimal(Left_Shoulder_Concrete_2));
                            Left_Shoulder_Concrete_Total = decimal.Add(Left_Shoulder_Concrete_SubTotal, Convert.ToDecimal(Left_Shoulder_Concrete_3)).ToString();

                            Left_Shoulder_Concrete_Total = Left_Shoulder_Concrete_Total == "0" ? "" : Left_Shoulder_Concrete_Total;
                            Left_Shoulder_Concrete_1 = Left_Shoulder_Concrete_1 == "0" ? "" : Left_Shoulder_Concrete_1;
                            Left_Shoulder_Concrete_2 = Left_Shoulder_Concrete_2 == "0" ? "" : Left_Shoulder_Concrete_2;
                            Left_Shoulder_Concrete_3 = Left_Shoulder_Concrete_3 == "0" ? "" : Left_Shoulder_Concrete_3;

                            break;

                        case "Left_Shoulder_Kerb":
                            Left_Shoulder_Kerb_Total = Left_Shoulder_Kerb_1 = Left_Shoulder_Kerb_2 = Left_Shoulder_Kerb_3 = 0.ToString();

                            foreach (var item in Left_Shoulder_Kerb)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Left_Shoulder_Kerb_1 = decimal.Add(Convert.ToDecimal(Left_Shoulder_Kerb_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Left_Shoulder_Kerb_2 = decimal.Add(Convert.ToDecimal(Left_Shoulder_Kerb_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Left_Shoulder_Kerb_3 = decimal.Add(Convert.ToDecimal(Left_Shoulder_Kerb_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Left_Shoulder_Kerb_SubTotal = decimal.Add(Convert.ToDecimal(Left_Shoulder_Kerb_1), Convert.ToDecimal(Left_Shoulder_Kerb_2));
                            Left_Shoulder_Kerb_Total = decimal.Add(Left_Shoulder_Kerb_SubTotal, Convert.ToDecimal(Left_Shoulder_Kerb_3)).ToString();

                            Left_Shoulder_Kerb_Total = Left_Shoulder_Kerb_Total == "0" ? "" : Left_Shoulder_Kerb_Total;
                            Left_Shoulder_Kerb_1 = Left_Shoulder_Kerb_1 == "0" ? "" : Left_Shoulder_Kerb_1;
                            Left_Shoulder_Kerb_2 = Left_Shoulder_Kerb_2 == "0" ? "" : Left_Shoulder_Kerb_2;
                            Left_Shoulder_Kerb_3 = Left_Shoulder_Kerb_3 == "0" ? "" : Left_Shoulder_Kerb_3;

                            break;

                        case "Right_Ditch":
                            Right_Ditch_Total = Right_Ditch_1 = Right_Ditch_2 = Right_Ditch_3 = 0.ToString();

                            foreach (var item in Right_Ditch)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Right_Ditch_1 = decimal.Add(Convert.ToDecimal(Right_Ditch_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Right_Ditch_2 = decimal.Add(Convert.ToDecimal(Right_Ditch_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Right_Ditch_3 = decimal.Add(Convert.ToDecimal(Right_Ditch_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Right_Ditch_SubTotal = decimal.Add(Convert.ToDecimal(Right_Ditch_1), Convert.ToDecimal(Right_Ditch_2));
                            Right_Ditch_Total = decimal.Add(Right_Ditch_SubTotal, Convert.ToDecimal(Right_Ditch_3)).ToString();

                            Right_Ditch_Total = Right_Ditch_Total == "0" ? "" : Right_Ditch_Total;
                            Right_Ditch_1 = Right_Ditch_1 == "0" ? "" : Right_Ditch_1;
                            Right_Ditch_2 = Right_Ditch_2 == "0" ? "" : Right_Ditch_2;
                            Right_Ditch_3 = Right_Ditch_3 == "0" ? "" : Right_Ditch_3;

                            break;

                        case "Right_Drain_Earth":
                            Right_Drain_Earth_Total = Right_Drain_Earth_1 = Right_Drain_Earth_2 = Right_Drain_Earth_3 = 0.ToString();

                            foreach (var item in Right_Drain_Earth)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Right_Drain_Earth_1 = decimal.Add(Convert.ToDecimal(Right_Drain_Earth_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Right_Drain_Earth_2 = decimal.Add(Convert.ToDecimal(Right_Drain_Earth_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Right_Drain_Earth_3 = decimal.Add(Convert.ToDecimal(Right_Drain_Earth_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Right_Drain_Earth_SubTotal = decimal.Add(Convert.ToDecimal(Right_Drain_Earth_1), Convert.ToDecimal(Right_Drain_Earth_2));
                            Right_Drain_Earth_Total = decimal.Add(Right_Drain_Earth_SubTotal, Convert.ToDecimal(Right_Drain_Earth_3)).ToString();

                            Right_Drain_Earth_Total = Right_Drain_Earth_Total == "0" ? "" : Right_Drain_Earth_Total;
                            Right_Drain_Earth_1 = Right_Drain_Earth_1 == "0" ? "" : Right_Drain_Earth_1;
                            Right_Drain_Earth_2 = Right_Drain_Earth_2 == "0" ? "" : Right_Drain_Earth_2;
                            Right_Drain_Earth_3 = Right_Drain_Earth_3 == "0" ? "" : Right_Drain_Earth_3;

                            break;

                        case "Right_Drain_Concrete":
                            Right_Drain_Concrete_Total = Right_Drain_Concrete_1 = Right_Drain_Concrete_2 = Right_Drain_Concrete_3 = 0.ToString();

                            foreach (var item in Right_Drain_Concrete)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Right_Drain_Concrete_1 = decimal.Add(Convert.ToDecimal(Right_Drain_Concrete_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Right_Drain_Concrete_2 = decimal.Add(Convert.ToDecimal(Right_Drain_Concrete_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Right_Drain_Concrete_3 = decimal.Add(Convert.ToDecimal(Right_Drain_Concrete_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Right_Drain_Concrete_SubTotal = decimal.Add(Convert.ToDecimal(Right_Drain_Concrete_1), Convert.ToDecimal(Right_Drain_Concrete_2));
                            Right_Drain_Concrete_Total = decimal.Add(Right_Drain_Concrete_SubTotal, Convert.ToDecimal(Right_Drain_Concrete_3)).ToString();

                            Right_Drain_Concrete_Total = Right_Drain_Concrete_Total == "0" ? "" : Right_Drain_Concrete_Total;
                            Right_Drain_Concrete_1 = Right_Drain_Concrete_1 == "0" ? "" : Right_Drain_Concrete_1;
                            Right_Drain_Concrete_2 = Right_Drain_Concrete_2 == "0" ? "" : Right_Drain_Concrete_2;
                            Right_Drain_Concrete_3 = Right_Drain_Concrete_3 == "0" ? "" : Right_Drain_Concrete_3;

                            break;

                        case "Right_Drain_BlockStone":
                            Right_Drain_BlockStone_Total = Right_Drain_BlockStone_1 = Right_Drain_BlockStone_2 = Right_Drain_BlockStone_3 = 0.ToString();

                            foreach (var item in Right_Drain_BlockStone)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Right_Drain_BlockStone_1 = decimal.Add(Convert.ToDecimal(Right_Drain_BlockStone_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Right_Drain_BlockStone_2 = decimal.Add(Convert.ToDecimal(Right_Drain_BlockStone_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Right_Drain_BlockStone_3 = decimal.Add(Convert.ToDecimal(Right_Drain_BlockStone_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Right_Drain_BlockStone_SubTotal = decimal.Add(Convert.ToDecimal(Right_Drain_BlockStone_1), Convert.ToDecimal(Right_Drain_BlockStone_2));
                            Right_Drain_BlockStone_Total = decimal.Add(Right_Drain_BlockStone_SubTotal, Convert.ToDecimal(Right_Drain_BlockStone_3)).ToString();

                            Right_Drain_BlockStone_Total = Right_Drain_BlockStone_Total == "0" ? "" : Right_Drain_BlockStone_Total;
                            Right_Drain_BlockStone_1 = Right_Drain_BlockStone_1 == "0" ? "" : Right_Drain_BlockStone_1;
                            Right_Drain_BlockStone_2 = Right_Drain_BlockStone_2 == "0" ? "" : Right_Drain_BlockStone_2;
                            Right_Drain_BlockStone_3 = Right_Drain_BlockStone_3 == "0" ? "" : Right_Drain_BlockStone_3;

                            break;

                        case "Right_Shoulder_Asphalt":
                            Right_Shoulder_Asphalt_Total = Right_Shoulder_Asphalt_1 = Right_Shoulder_Asphalt_2 = Right_Shoulder_Asphalt_3 = 0.ToString();

                            foreach (var item in Right_Shoulder_Asphalt)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Right_Shoulder_Asphalt_1 = decimal.Add(Convert.ToDecimal(Right_Shoulder_Asphalt_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Right_Shoulder_Asphalt_2 = decimal.Add(Convert.ToDecimal(Right_Shoulder_Asphalt_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Right_Shoulder_Asphalt_3 = decimal.Add(Convert.ToDecimal(Right_Shoulder_Asphalt_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Right_Shoulder_Asphalt_SubTotal = decimal.Add(Convert.ToDecimal(Right_Shoulder_Asphalt_1), Convert.ToDecimal(Right_Shoulder_Asphalt_2));
                            Right_Shoulder_Asphalt_Total = decimal.Add(Right_Shoulder_Asphalt_SubTotal, Convert.ToDecimal(Right_Shoulder_Asphalt_3)).ToString();

                            Right_Shoulder_Asphalt_Total = Right_Shoulder_Asphalt_Total == "0" ? "" : Right_Shoulder_Asphalt_Total;
                            Right_Shoulder_Asphalt_1 = Right_Shoulder_Asphalt_1 == "0" ? "" : Right_Shoulder_Asphalt_1;
                            Right_Shoulder_Asphalt_2 = Right_Shoulder_Asphalt_2 == "0" ? "" : Right_Shoulder_Asphalt_2;
                            Right_Shoulder_Asphalt_3 = Right_Shoulder_Asphalt_3 == "0" ? "" : Right_Shoulder_Asphalt_3;

                            break;

                        case "Right_Shoulder_Gravel":
                            Right_Shoulder_Gravel_Total = Right_Shoulder_Gravel_1 = Right_Shoulder_Gravel_2 = Right_Shoulder_Gravel_3 = 0.ToString();

                            foreach (var item in Right_Shoulder_Gravel)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Right_Shoulder_Gravel_1 = decimal.Add(Convert.ToDecimal(Right_Shoulder_Gravel_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Right_Shoulder_Gravel_2 = decimal.Add(Convert.ToDecimal(Right_Shoulder_Gravel_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Right_Shoulder_Gravel_3 = decimal.Add(Convert.ToDecimal(Right_Shoulder_Gravel_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Right_Shoulder_Gravel_SubTotal = decimal.Add(Convert.ToDecimal(Right_Shoulder_Gravel_1), Convert.ToDecimal(Right_Shoulder_Gravel_2));
                            Right_Shoulder_Gravel_Total = decimal.Add(Right_Shoulder_Gravel_SubTotal, Convert.ToDecimal(Right_Shoulder_Gravel_3)).ToString();

                            Right_Shoulder_Gravel_Total = Right_Shoulder_Gravel_Total == "0" ? "" : Right_Shoulder_Gravel_Total;
                            Right_Shoulder_Gravel_1 = Right_Shoulder_Gravel_1 == "0" ? "" : Right_Shoulder_Gravel_1;
                            Right_Shoulder_Gravel_2 = Right_Shoulder_Gravel_2 == "0" ? "" : Right_Shoulder_Gravel_2;
                            Right_Shoulder_Gravel_3 = Right_Shoulder_Gravel_3 == "0" ? "" : Right_Shoulder_Gravel_3;

                            break;

                        case "Right_Shoulder_Earth":
                            Right_Shoulder_Earth_Total = Right_Shoulder_Earth_1 = Right_Shoulder_Earth_2 = Right_Shoulder_Earth_3 = 0.ToString();

                            foreach (var item in Right_Shoulder_Earth)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Right_Shoulder_Earth_1 = decimal.Add(Convert.ToDecimal(Right_Shoulder_Earth_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Right_Shoulder_Earth_2 = decimal.Add(Convert.ToDecimal(Right_Shoulder_Earth_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Right_Shoulder_Earth_3 = decimal.Add(Convert.ToDecimal(Right_Shoulder_Earth_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Right_Shoulder_Earth_SubTotal = decimal.Add(Convert.ToDecimal(Right_Shoulder_Earth_1), Convert.ToDecimal(Right_Shoulder_Earth_2));
                            Right_Shoulder_Earth_Total = decimal.Add(Right_Shoulder_Earth_SubTotal, Convert.ToDecimal(Right_Shoulder_Earth_3)).ToString();

                            Right_Shoulder_Earth_Total = Right_Shoulder_Earth_Total == "0" ? "" : Right_Shoulder_Earth_Total;
                            Right_Shoulder_Earth_1 = Right_Shoulder_Earth_1 == "0" ? "" : Right_Shoulder_Earth_1;
                            Right_Shoulder_Earth_2 = Right_Shoulder_Earth_2 == "0" ? "" : Right_Shoulder_Earth_2;
                            Right_Shoulder_Earth_3 = Right_Shoulder_Earth_3 == "0" ? "" : Right_Shoulder_Earth_3;
                            break;

                        case "Right_Shoulder_Concrete":
                            Right_Shoulder_Concrete_Total = Right_Shoulder_Concrete_1 = Right_Shoulder_Concrete_2 = Right_Shoulder_Concrete_3 = 0.ToString();

                            foreach (var item in Right_Shoulder_Concrete)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Right_Shoulder_Concrete_1 = decimal.Add(Convert.ToDecimal(Right_Shoulder_Concrete_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Right_Shoulder_Concrete_2 = decimal.Add(Convert.ToDecimal(Right_Shoulder_Concrete_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Right_Shoulder_Concrete_3 = decimal.Add(Convert.ToDecimal(Right_Shoulder_Concrete_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Right_Shoulder_Concrete_SubTotal = decimal.Add(Convert.ToDecimal(Right_Shoulder_Concrete_1), Convert.ToDecimal(Right_Shoulder_Concrete_2));
                            Right_Shoulder_Concrete_Total = decimal.Add(Right_Shoulder_Concrete_SubTotal, Convert.ToDecimal(Right_Shoulder_Concrete_3)).ToString();

                            Right_Shoulder_Concrete_Total = Right_Shoulder_Concrete_Total == "0" ? "" : Right_Shoulder_Concrete_Total;
                            Right_Shoulder_Concrete_1 = Right_Shoulder_Concrete_1 == "0" ? "" : Right_Shoulder_Concrete_1;
                            Right_Shoulder_Concrete_2 = Right_Shoulder_Concrete_2 == "0" ? "" : Right_Shoulder_Concrete_2;
                            Right_Shoulder_Concrete_3 = Right_Shoulder_Concrete_3 == "0" ? "" : Right_Shoulder_Concrete_3;

                            break;

                        case "Right_Shoulder_Kerb":
                            Right_Shoulder_Kerb_Total = Right_Shoulder_Kerb_1 = Right_Shoulder_Kerb_2 = Right_Shoulder_Kerb_3 = 0.ToString();

                            foreach (var item in Right_Shoulder_Kerb)
                            {
                                switch (item.Condition)
                                {
                                    case 1:
                                        Right_Shoulder_Kerb_1 = decimal.Add(Convert.ToDecimal(Right_Shoulder_Kerb_1), (decimal)0.1).ToString();
                                        break;
                                    case 2:
                                        Right_Shoulder_Kerb_2 = decimal.Add(Convert.ToDecimal(Right_Shoulder_Kerb_2), (decimal)0.1).ToString();
                                        break;
                                    case 3:
                                        Right_Shoulder_Kerb_3 = decimal.Add(Convert.ToDecimal(Right_Shoulder_Kerb_3), (decimal)0.1).ToString();
                                        break;
                                }
                            }
                            var Right_Shoulder_Kerb_SubTotal = decimal.Add(Convert.ToDecimal(Right_Shoulder_Kerb_1), Convert.ToDecimal(Right_Shoulder_Kerb_2));
                            Right_Shoulder_Kerb_Total = decimal.Add(Right_Shoulder_Kerb_SubTotal, Convert.ToDecimal(Right_Shoulder_Kerb_3)).ToString();

                            Right_Shoulder_Kerb_Total = Right_Shoulder_Kerb_Total == "0" ? "" : Right_Shoulder_Kerb_Total;
                            Right_Shoulder_Kerb_1 = Right_Shoulder_Kerb_1 == "0" ? "" : Right_Shoulder_Kerb_1;
                            Right_Shoulder_Kerb_2 = Right_Shoulder_Kerb_2 == "0" ? "" : Right_Shoulder_Kerb_2;
                            Right_Shoulder_Kerb_3 = Right_Shoulder_Kerb_3 == "0" ? "" : Right_Shoulder_Kerb_3;

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


                    if (SelectedCrewIndex == -1)
                    {
                        _userDialogs.Alert("Crew Leader ID field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }
                    if (CrewName == null)
                    {
                        _userDialogs.Alert("Crew Leader Name field is required. ", "RAMS", "Ok");
                        return;
                    }

                    if (SelectedInspIndex == -1)
                    {
                        _userDialogs.Alert("Inspected By User ID field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (InspName == null)
                    {
                        _userDialogs.Alert("Inspected By User Name field is required. Please choose from the dropdown list.", "RAMS", "Ok");
                        return;
                    }

                    if (InspDesignation == null)
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
    public class GridData : FreshBasePageModel
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
                else if (value > 3)
                {
                    value = _condition;
                    UserDialogs.Instance.Alert("Please enter the value ranges between 1 to 3");
                }
                RaisePropertyChanged();
            }
        }

        public bool IsEnable { get {

                if (App.ReturnType != "View")
                    return Detail.Count == 1 ? true : false;
                else
                    return false;
            } 
        }
        public IList<FormFDDetailsDTO> Detail { get; set; }
        public Color Color { get { return IsEnable == true ? Color.White : Color.LightGray; } }
    }

    public class DR
    {
        public string Id { get; set; }
        public string Desc { get; set; }
        public string Value { get; set; }
        public string LAvgWidth { get; set; }
        public string RAvgWidth { get; set; }
    }

    public class SH
    {
        public string Id { get; set; }
        public string Desc { get; set; }
        public string Value { get; set; }
        public string LAvgWidth { get; set; }
        public string RAvgWidth { get; set; }
    }

    public class DI
    {
        public string Id { get; set; }
        public string Desc { get; set; }
        public string Value { get; set; }
        public string LAvgWidth { get; set; }
        public string RAvgWidth { get; set; }
    }

    public class Root1
    {
        public List<DR> DR { get; set; }
        public List<SH> SH { get; set; }
        public List<DI> DI { get; set; }
    }



}
