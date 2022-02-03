using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;
using RAMMS.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace RAMMS.MobileApps.PageModel
{
    public class FormF2DetailsPageModel : FreshBasePageModel
    {
        private IRestApi _restApi;
        private IUserDialogs _userDialogs;
        private ILocalDatabase _localDatabase;
        private double? _grCondition1 = null;
        private double? _grCondition2 = null;
        private double? _grCondition3 = null;

        FormF2DetailRequestDTO SelectedRowItem;
        public int? StartingChKm { get; set; }
        public string StartingChM { get; set; }
        public string GrCode { get; set; }
        public string Bound { get; set; }
        public double? PostSpac { get; set; }
        public double? Length { get; set; }
        public double? GrCondition1
        {
            get { return _grCondition1; }
            set
            {
                if (value == null || value == 0)
                {
                    _grCondition1 = null;
                }
                else if (!_grCondition1.HasValue || !_grCondition1.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _grCondition1 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _grCondition1 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }

        public double? GrCondition2
        {
            get { return _grCondition2; }
            set
            {
                if (value == null || value == 0)
                {
                    _grCondition2 = null;
                }
                else if (!_grCondition2.HasValue || !_grCondition2.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _grCondition2 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _grCondition2 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }

        public double? GrCondition3
        {
            get { return _grCondition3; }
            set
            {
                if (value == null || value == 0)
                {
                    _grCondition3 = null;
                }
                else if (!_grCondition3.HasValue || !_grCondition3.Equals(value))
                {
                    var textString = value.Value.ToString().Split('.');
                    if (textString.Count() > 1 && textString[1].Length <= 5)
                        _grCondition3 = Convert.ToDouble(value.Value.ToString("#####.#####"));
                    else if (textString[0].Length <= 5 && textString.Count() == 1)
                        _grCondition3 = textString[0].Length > 0 ? value : null;
                }
                RaisePropertyChanged();
            }
        }
        
        public string Remarks { get; set; }
        public bool CanSave { get; set; }

        public FormF2DetailsPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;
            _restApi = restApi;
            _localDatabase = localDatabase;
        }
        public override void Init(object initData)
        {
            base.Init(initData);

            SelectedRowItem = (FormF2DetailRequestDTO)initData;
            if (SelectedRowItem != null)
            {
                StartingChKm = SelectedRowItem.StartingChKm;
                StartingChM = SelectedRowItem.StartingChM;
                GrCode = SelectedRowItem.GrCode;
                Bound = SelectedRowItem.Bound;
                PostSpac = SelectedRowItem.PostSpac;
                Length = SelectedRowItem.Length;
                GrCondition1 = SelectedRowItem.GrCondition1;
                GrCondition2 = SelectedRowItem.GrCondition2;
                GrCondition3 = SelectedRowItem.GrCondition3;
                Remarks = SelectedRowItem.Remarks;
            }
        }
        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            CanSave = App.DetailType == "Edit" ? true : false;
        }

        public async void SaveDetail()
        {
            _userDialogs.ShowLoading("Loading");
            try
            {                
                if(Length != ((GrCondition1 ?? 0) + (GrCondition2 ?? 0) + (GrCondition3 ?? 0)))
                {
                    await UserDialogs.Instance.AlertAsync("Length shoule be equal to (Condition 1 + Condition 2 + Condition 3)", "RAMS", "OK");
                    return;                
                }

                if (CrossConnectivity.Current.IsConnected)
                {
                    FormF2DetailRequestDTO detail = new FormF2DetailRequestDTO()
                    {
                        GrCondition1 = GrCondition1,
                        GrCondition2 = GrCondition2,
                        GrCondition3 = GrCondition3,
                        Remarks = Remarks,
                        PostSpac = PostSpac,
                        PkRefNo = SelectedRowItem.PkRefNo
                    };

                    var response = await _restApi.SaveF2Detail(detail);
                    if (response.success)
                    {
                        await UserDialogs.Instance.AlertAsync("Data Saved Successfully.", "RAMS", "OK");
                        await CoreMethods.PopPageModel();
                    }
                }
                else
                    UserDialogs.Instance.Alert("Please check your Internet Connection !");
            }
            catch(Exception ex)
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
                return new FreshCommand(async (obj) =>
                {
                    SaveDetail();
                });
            }
        }

        public ICommand CancelAction
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    if (App.DetailType == "View")
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
    }
}
