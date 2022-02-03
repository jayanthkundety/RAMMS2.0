using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Input;
using Acr.UserDialogs;
using FreshMvvm;
using Plugin.Connectivity;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;

namespace RAMMS.MobileApps.PageModel
{
    public class FormXAddPageModel : FreshBasePageModel
    {
        private IRestApi _restApi;

        public IUserDialogs _userDialogs;

        public ILocalDatabase _localDatabase;

        public bool IsView { get; set; }

        private EditViewModel editViewModel;
        public string ChoosenFileName { get; set; }
        public FormXHeaderRequestDTO CurrentItem { get; set; }
        public AssetDDLResponseDTO.DropDown SelectedRMU { get; set; }
        public AssetDDLResponseDTO.DropDown SelectedRoadCode { get; set; }
        public string SelectedRoadName { get; set; }
        public string SelectedSection { get; set; }
        public DDListItems SelectedMainTask { get; set; }
        public DDListItems SelectedSubTask { get; set; }
        public DDListItems SelectedComMode { get; set; }
        public DDListItems SelectedReportedBy { get; set; }
        public DDListItems SelectedAssignedTo { get; set; }
        public DDListItems SelectedAttentionTo { get; set; }
        public DDListItems SelectedVerfiedBy { get; set; }
        public DDListItems SelectedSchVerfiedBy { get; set; }
        public DDListItems SelectedVettedBy { get; set; }
        public string ReportedByName { get; set; }
        public string LocRepDesc { get; set; }

        public string Comment2 { get; set; }
        public string ReferenceId { get; set; }
        public DateTime? ReportedDate { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public DateTime? SchVerifiedDate { get; set; }
        public DateTime? JKRSSentDate { get; set; }
        public DateTime? JKRSRecDate { get; set; }
        public DateTime? EstimatedDate { get; set; }
        public DateTime? AssignedDate { get; set; }
        public DateTime? WorkScheduledDate { get; set; }
        public DateTime? WorkCompletedDate { get; set; }
        public DateTime? CaseClosedDate { get; set; }
        public string WorkPhone { get; set; }
        public string HandPhone { get; set; }
        public string EMail { get; set; }
        public string Description { get; set; }
        public string AttentionToName { get; set; }
        public string AssignedToName { get; set; }
        public string VerifiedByName { get; set; }
        public string SchVerifiedByName { get; set; }
        public bool EnableLoc { get; set; }=false;

        public string VettedByName { get; set; }
        public string AssWrkRef { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public string MainTaskName { get; set; }
        public string SubTaskName { get; set; }
        public string EstCrewDays { get; set; }

        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRodeCodeListItems { get; set; }

        public ObservableCollection<AssetDDLResponseDTO.DropDown> DDRMUListItems { get; set; }

        public ObservableCollection<DDListItems> MainTaskListItems { get; set; }
        public ObservableCollection<DDListItems> SubTaskListItems { get; set; }
        public ObservableCollection<DDListItems> UserListItems { get; set; }
        public ObservableCollection<DDListItems> ComModeListItems { get; set; }
        public ObservableCollection<DDListItems> ReportedByListItems { get; set; }

        public ICommand UseeuCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    await CoreMethods.PushPageModel<FormXUSeeUPageModel>(editViewModel);
                });
            }
        }

        public ICommand WarCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    await CoreMethods.PushPageModel<FormXWarPageModel>(editViewModel);
                });
            }
        }

        public ICommand CancelCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    await CurrentPage.Navigation.PopAsync();
                });
            }
        }

        public FormXAddPageModel(IRestApi restApi, IUserDialogs userDialogs, ILocalDatabase localDatabase)
        {
            _userDialogs = userDialogs;

            _restApi = restApi;

            _localDatabase = localDatabase;
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            editViewModel = initData as EditViewModel;
        }

        protected override void ViewIsAppearing(object sender, EventArgs e)
        {
            base.ViewIsAppearing(sender, e);
            GetDropDownList();
            GetTaskDropdownlist("ACT-Main_Task");
            GetTaskDropdownlist("ACT-Sub_Task");
            GetTaskDropdownlist("ComMode");
            GetTaskDropdownlist("ReportedBy");
            GetUserList();
            if (editViewModel.Type == "View")
                IsView = true;
            GetFormXDetails(editViewModel.HdrFahPkRefNo);
        }

        private async void GetTaskDropdownlist(string type)
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = new ResponseBaseListObject<DDListItems>();

                    var strQuery = new DDLookUpDTO
                    {
                        Type = type
                    };

                    if (type == "ComMode" || type == "ReportedBy")
                    {
                        response = await _restApi.GetDDList(strQuery);
                    }

                    else
                    {
                        response = await _restApi.GetTaskList(strQuery);
                    }


                    if (response.success)
                    {
                        switch (type)
                        {
                            case "ACT-Main_Task":
                                MainTaskListItems = new ObservableCollection<DDListItems>(response.data);
                                break;

                            case "ACT-Sub_Task":
                                SubTaskListItems = new ObservableCollection<DDListItems>(response.data);
                                SubTaskListItems.Add(new DDListItems {Text="099-Others", Value="099" });
                                break;


                            case "ComMode":
                                ComModeListItems = new ObservableCollection<DDListItems>(response.data);
                                break;

                            case "ReportedBy":
                                ReportedByListItems = new ObservableCollection<DDListItems>(response.data);
                                break;
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

        public async void GetUserList()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {
                    var response = await _restApi.userList();
                    if (response.success)
                    {
                        UserListItems = new ObservableCollection<DDListItems>(response.data);
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

        private async void GetFormXDetails(int hdrFahPkRefNo)
        {
            _userDialogs.ShowLoading("Loading");
            try
            {
                var response = await _restApi.FormXDetailsById(hdrFahPkRefNo);
                if (response.success)
                {
                    var selectedItem = CurrentItem = response.data;
                    SelectedRMU = DDRMUListItems?.Where(a => a.Value.ToLower() == selectedItem.Rmu.ToLower()).FirstOrDefault();
                    ReferenceId = selectedItem.ReferenceId;
                    SelectedRoadCode = DDRodeCodeListItems?.Where(a => a.Value.ToLower() == selectedItem.RoadCode.ToLower()).FirstOrDefault();
                    SelectedRoadName = SelectedRoadCode?.Text.Split('-')[1].ToString();
                    SelectedSection = selectedItem.Section;
                    SelectedComMode = ComModeListItems?.Where(a => a.Text.Contains(selectedItem.ModComType ?? "xxx")).FirstOrDefault();
                    SelectedReportedBy = ReportedByListItems?.Where(a => a.Text.ToLower().Contains(selectedItem.Location?.ToLower() ?? "xxx")).FirstOrDefault();
                    ReportedByName = selectedItem.Name;// SelectedReportedBy?.Text;
                    if (selectedItem.Location == "OTHERS")
                    { LocRepDesc = selectedItem.LocRepDesc;
                        EnableLoc = true;
                    }    
                    else
                    { EnableLoc = false; }
                    ReportedDate = selectedItem.DateReported;
                    WorkPhone = selectedItem.WorkPhone;
                    HandPhone = selectedItem.HandPhone;
                    EMail = selectedItem.EmailId;
                    Description = selectedItem.Description;
                    SelectedAttentionTo = selectedItem.UseridAttnTo == null ? null : UserListItems?[UserListItems.ToList().FindIndex(x => x.Value == selectedItem.UseridAttnTo.ToString())];
                    AttentionToName = selectedItem.AttentionTo;// SelectedAttentionTo?.Text.Split('-')[1].ToString();
                    SelectedVerfiedBy = selectedItem.UseridVer == null ? null : UserListItems?[UserListItems.ToList().FindIndex(x => x.Value == selectedItem.UseridVer.ToString())];
                    VerifiedByName = selectedItem.UsernameVer;// SelectedVerfiedBy?.Text.Split('-')[1].ToString();
                    VerifiedDate = selectedItem.DtVer;
                    AssWrkRef = selectedItem.AssignWork;
                    Comment = selectedItem.Comments;
                    JKRSSentDate = selectedItem.DtJkrSent;
                    JKRSRecDate = selectedItem.DtJkrRcvdFrm;
                    Status = selectedItem.StsJkr;// ? "Received" : "Reviewed and Closed";
                    Remarks = selectedItem.JkrRemarks;
                    Comment2 = selectedItem.Remarks;

                    SelectedMainTask = MainTaskListItems?.Where(a => a.Text.Contains(selectedItem.ActMainCode?.ToString() ?? "xxx")).FirstOrDefault();
                    MainTaskName = SelectedMainTask == null ? null : SelectedMainTask.Text.Split('-')[1].ToString();
                    //if (selectedItem.ActSubName != null)
                    //  SelectedSubTask = SubTaskListItems?.Where(a => a.Text.Contains(selectedItem.ActMainCode?.ToString() ?? "xxx")).FirstOrDefault();
                    SelectedSubTask = selectedItem.ActSubCode == null ? null : selectedItem.ActSubCode == "Select Sub Task" ? null : SubTaskListItems?[SubTaskListItems.ToList().FindIndex(x => x.Value == selectedItem.ActSubCode.ToString())];
                    SubTaskName = selectedItem.ActSubName ?? "";
                    EstCrewDays = selectedItem.EstDays;
                    EstimatedDate = selectedItem.EstDate;
                    SelectedAssignedTo = selectedItem.UseridAssgn == null ? null : UserListItems?[UserListItems.ToList().FindIndex(x => x.Value == selectedItem.UseridAssgn.ToString())];
                    AssignedToName = selectedItem.UsernameAssgn;
                    AssignedDate = selectedItem.DtAssgn;
                    WorkScheduledDate = selectedItem.WorkSc;
                    WorkCompletedDate = selectedItem.WorkCompleted;
                    SelectedSchVerfiedBy = selectedItem.UseridSchdVer == null ? null : UserListItems?[UserListItems.ToList().FindIndex(x => x.Value == selectedItem.UseridSchdVer.ToString())];
                    SchVerifiedByName = selectedItem.UsernameSchdVer;// SelectedVerfiedBy?.Text.Split('-')[1].ToString();
                    SchVerifiedDate = selectedItem.DtSchdVer;
                    CaseClosedDate = selectedItem.CaseClosedOn;
                    SelectedVettedBy = selectedItem.UseridVet == null ? null : UserListItems?[UserListItems.ToList().FindIndex(x => x.Value == selectedItem.UseridVet.ToString())];
                    VettedByName = selectedItem.UsernameVet;
                    if (CurrentItem.ModComUpload != null && CurrentItem.ModComUpload != "")
                    {
                        var fileName = CurrentItem.ModComUpload.Split('\\');
                        ChoosenFileName = fileName[fileName.Count() - 1];
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


        private async void GetDropDownList()
        {
            try
            {
                _userDialogs.ShowLoading("Loading");

                if (CrossConnectivity.Current.IsConnected)
                {

                    var strQuery = new AssetDDLRequestDTO
                    {
                        RMU = null
                    };


                    var response = await _restApi.GetLandingDropDown(strQuery);

                    if (response.success)
                    {
                        if (response.data.RMU != null)
                        {
                            DDRMUListItems = new ObservableCollection<AssetDDLResponseDTO.DropDown>(response.data.RMU);
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
        }

        public ICommand AddCommand
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    await CoreMethods.PushPageModel<FormFCAddPageModel>();
                });
            }
        }



        public ICommand Warpage
        {
            get
            {
                return new FreshCommand(async (obj) =>
                {
                    await CoreMethods.PushPageModel<FormXWarPageModel>();
                });
            }
        }

        public ICommand ChoosenFileDownloadCommand
        {
            get
            {
                return new FreshAwaitCommand(async (obj) =>
                {
                    WebClient client = new WebClient();
                    var url = new Uri($"{AppConst.WebBaseURL}/" + CurrentItem.ModComUpload);

                    client.DownloadDataAsync(url);

                    client.DownloadDataCompleted += async (s, e) =>
                    {
                        var data = e.Result; // get the downloaded text
                        string documentsPath = "/storage/emulated/0/Android/data/ramms.mobileapps/";
                        string localPath = Path.Combine(documentsPath, ChoosenFileName);

                        if (File.Exists(localPath))
                        {
                            File.Delete(localPath);
                        }
                        using (FileStream fs = File.Create(localPath))
                        {
                            fs.Write(data, 0, data.Length);
                        }
                        _userDialogs.Alert("File downloaded and saved in Android/data/ramms.mobileapps/", "Done", "OK");
                    };
                });
            }
        }

    }
}
