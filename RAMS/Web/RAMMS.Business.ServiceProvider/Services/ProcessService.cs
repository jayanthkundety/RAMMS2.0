using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using RAMMS.Common;
using RAMMS.Domain.Models;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class ProcessService : IProcessService
    {
        private readonly Repository.RAMMSContext context;
        private readonly ISecurity security;
        public ProcessService(IRepositoryUnit repoUnit, ISecurity Security)
        {
            context = repoUnit._context;
            security = Security;
        }
        public async Task<int> Save(DTO.RequestBO.ProcessDTO process)
        {
            int iResult = 0;

            switch (process.Form)
            {
                case "FormA":
                    iResult = await SaveFormA(process);
                    break;
                case "FormB1B2":
                    iResult = await SaveFormB1B2(process);
                    break;
                case "FormC1C2":
                    iResult = await SaveFormC1C2(process);
                    break;
                case "FormD":
                    iResult = await SaveFormD(process);
                    break;
                case "FormF2":
                    iResult = await SaveFormF2(process);
                    break;
                case "FormF4":
                    iResult = await SaveFormF4(process);
                    break;
                case "FormF5":
                    iResult = await SaveFormF5(process);
                    break;
                case "FormFC":
                    iResult = await SaveFormFC(process);
                    break;
                case "FormFD":
                    iResult = await SaveFormFD(process);
                    break;
                case "FormFS":
                    iResult = await SaveFormFS(process);
                    break;
                case "FormH":
                    iResult = await SaveFormH(process);
                    break;
                case "FormJ":
                    iResult = await SaveFormJ(process);
                    break;
                case "FormN1":
                    iResult = await SaveFormN1(process);
                    break;
                case "FormN2":
                    iResult = await SaveFormN2(process);
                    break;
                case "FormS1":
                    iResult = await SaveFormS1(process);
                    break;
                case "FormS2":
                    iResult = await SaveFormS2(process);
                    break;
                case "FormX":
                    iResult = await SaveFormX(process);
                    break;
            }
            return iResult;
        }
        public void SaveNotification(RmUserNotification notification, bool isContextSave)
        {
            context.RmUserNotification.Add(notification);
            if (isContextSave)
                context.SaveChanges();
        }
        public List<Dictionary<string, object>> AuditHistory(string formName, int RefId)
        {
            string logs = string.Empty;
            switch (formName)
            {
                case "FormA":
                    logs = this.context.RmFormAHdr.Where(x => x.FahPkRefNo == RefId).Select(x => x.FahAuditLog).FirstOrDefault();
                    break;
                case "FormB1B2":
                    logs = this.context.RmFormB1b2BrInsHdr.Where(x => x.FbrihPkRefNo == RefId).Select(x => x.FbrihAuditLog).FirstOrDefault();
                    break;
                case "FormC1C2":
                    logs = this.context.RmFormCvInsHdr.Where(x => x.FcvihPkRefNo == RefId).Select(x => x.FcvihAuditLog).FirstOrDefault();
                    break;
                case "FormD":
                    logs = this.context.RmFormDHdr.Where(x => x.FdhPkRefNo == RefId).Select(x => x.FdhAuditLog).FirstOrDefault();
                    break;
                case "FormF2":
                    logs = this.context.RmFormF2GrInsHdr.Where(x => x.FgrihPkRefNo == RefId).Select(x => x.FgrihAuditLog).FirstOrDefault();
                    break;
                case "FormF4":
                    logs = this.context.RmFormF4InsHdr.Where(x => x.FivahPkRefNo == RefId).Select(x => x.FivahAuditLog).FirstOrDefault();
                    break;
                case "FormF5":
                    logs = this.context.RmFormF5InsHdr.Where(x => x.FvahPkRefNo == RefId).Select(x => x.FvahAuditLog).FirstOrDefault();
                    break;
                case "FormFC":
                    logs = this.context.RmFormFcInsHdr.Where(x => x.FcihPkRefNo == RefId).Select(x => x.FcihAuditLog).FirstOrDefault();
                    break;
                case "FormFD":
                    logs = this.context.RmFormFdInsHdr.Where(x => x.FdihPkRefNo == RefId).Select(x => x.FdihAuditLog).FirstOrDefault();
                    break;
                case "FormFS":
                    logs = this.context.RmFormFsInsHdr.Where(x => x.FshPkRefNo == RefId).Select(x => x.FshAuditLog).FirstOrDefault();
                    break;
                case "FormH":
                    logs = this.context.RmFormHHdr.Where(x => x.FhhPkRefNo == RefId).Select(x => x.FhhAuditLog).FirstOrDefault();
                    break;
                case "FormJ":
                    logs = this.context.RmFormJHdr.Where(x => x.FjhPkRefNo == RefId).Select(x => x.FjhAuditLog).FirstOrDefault();
                    break;
                case "FormN1":
                    logs = this.context.RmFormN1Hdr.Where(x => x.FnihPkRefNo == RefId).Select(x => x.FnihAuditLog).FirstOrDefault();
                    break;
                case "FormN2":
                    logs = this.context.RmFormN2Hdr.Where(x => x.FnthPkRefNo == RefId).Select(x => x.FnthAuditLog).FirstOrDefault();
                    break;
                case "FormS1":
                    logs = this.context.RmFormS1Hdr.Where(x => x.FsihPkRefNo == RefId).Select(x => x.FsihAuditLog).FirstOrDefault();
                    break;
                case "FormS2":
                    logs = this.context.RmFormS2Hdr.Where(x => x.FsiihPkRefNo == RefId).Select(x => x.FsiihAuditLog).FirstOrDefault();
                    break;
            }
            return Utility.ProcessLog(logs);
        }
        private async Task<int> SaveFormN1(DTO.RequestBO.ProcessDTO process)
        {
            var form = context.RmFormN1Hdr.Where(x => x.FnihPkRefNo == process.RefId).FirstOrDefault();
            if (form != null)
            {
                string strTitle = "";
                string strNotURL = "";
                string strNotMsg = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (form.FnihSubmitSts && (form.FnihStatus == Common.StatusList.N1Init))
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperRegionManager : GroupNames.JKRSSuperiorOfficerSO;
                    form.FnihStatus = process.IsApprove ? Common.StatusList.N1Issued : StatusList.N1Init;
                    strTitle = "Issued";
                }
                else if (form.FnihStatus == Common.StatusList.N1Issued)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperRegionManager : GroupNames.JKRSSuperiorOfficerSO;
                    form.FnihStatus = process.IsApprove ? Common.StatusList.N1Received : StatusList.N1Init;
                    if (!process.IsApprove) { form.FnihSubmitSts = false; }
                    form.FnihUseridRcvd = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FnihUsernameRcvd = !process.IsApprove ? null : process.UserName;
                    form.FnihDesignationRcvd = !process.IsApprove ? null : process.UserDesignation;
                    strTitle = "Received";
                }
                else if (form.FnihStatus == Common.StatusList.N1Received)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OperRegionManager;
                    strTitle = "Corrective Action Completed";
                    form.FnihStatus = process.IsApprove ? Common.StatusList.N1CorrectiveCompleted : StatusList.N1Issued;
                    form.FnihUseridCorrective = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FnihUsernameCorrective = !process.IsApprove ? null : process.UserName;
                    form.FnihDesignationCorrective = !process.IsApprove ? null : process.UserDesignation;
                    form.FnihDtCorrective = !process.IsApprove ? null : process.ApproveDate;
                }
                else if (form.FnihStatus == Common.StatusList.N1CorrectiveCompleted)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OperRegionManager;
                    strTitle = "Corrective Action Accepted";
                    form.FnihStatus = process.IsApprove ? Common.StatusList.N1CorrectiveAccepted : StatusList.N1Received;
                    form.FnihUseridAccptd = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FnihUsernameAccptd = !process.IsApprove ? null : process.UserName;
                    form.FnihDesignationAccptd = !process.IsApprove ? null : process.UserDesignation;
                    form.FnihDtAccptd = !process.IsApprove ? null : process.ApproveDate;
                }
                else if (form.FnihStatus == Common.StatusList.N1CorrectiveAccepted)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.JKRSSuperiorOfficerSO;
                    strTitle = "Verified";
                    form.FnihStatus = process.IsApprove ? Common.StatusList.Completed : StatusList.N1CorrectiveCompleted;
                    form.FnihUseridVer = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FnihUsernameVer = !process.IsApprove ? null : process.UserName;
                    form.FnihDesignationVer = !process.IsApprove ? null : process.UserDesignation;
                    if (process.IsApprove)
                    {
                        List<int> lstNotUserId = new List<int>();
                        if (form.FnihUseridIssued.HasValue)
                            lstNotUserId.Add(form.FnihUseridIssued.Value);
                        if (form.FnihUseridRcvd.HasValue)
                            lstNotUserId.Add(form.FnihUseridRcvd.Value);
                        if (form.FnihUseridCorrective.HasValue)
                            lstNotUserId.Add(form.FnihUseridCorrective.Value);
                        if (form.FnihUseridAccptd.HasValue)
                            lstNotUserId.Add(form.FnihUseridAccptd.Value);
                        if (form.FnihUseridVer.HasValue)
                            lstNotUserId.Add(form.FnihUseridVer.Value);
                        strNotUserID = string.Join(",", lstNotUserId.Distinct());
                    }
                }
                form.FnihAuditLog = Utility.ProcessLog(form.FnihAuditLog, strTitle, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                strNotMsg = (process.IsApprove ? "" : "Rejected - ") + strTitle + ":" + process.UserName + " - Form N1 (" + form.FnihNcnNo + ")";
                strNotURL = "/MAM/EditFormN1?headerId=" + form.FnihPkRefNo.ToString() + "&view=1";
                SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
        private async Task<int> SaveFormN2(DTO.RequestBO.ProcessDTO process)
        {
            var form = context.RmFormN2Hdr.Where(x => x.FnthPkRefNo == process.RefId).FirstOrDefault();
            if (form != null)
            {
                string strTitle = "";
                string strNotURL = "";
                string strNotMsg = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (form.FnihSubmitSts && (form.FnthStatus == Common.StatusList.N2Init))
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperRegionManager : GroupNames.JKRSSuperiorOfficerSO;
                    form.FnthStatus = process.IsApprove ? Common.StatusList.N2Issued : StatusList.N2Init;
                    strTitle = "Issued";
                }
                else if (form.FnthStatus == Common.StatusList.N2Issued)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperRegionManager : GroupNames.JKRSSuperiorOfficerSO;
                    form.FnthStatus = process.IsApprove ? Common.StatusList.N2Received : StatusList.N2Init;
                    if (!process.IsApprove) { form.FnihSubmitSts = false; }
                    form.FnthUseridRcvd = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FnthUsernameRcvd = !process.IsApprove ? null : process.UserName;
                    form.FnthDesignationRcvd = !process.IsApprove ? null : process.UserDesignation;
                    strTitle = "Received";
                }
                else if (form.FnthStatus == Common.StatusList.N2Received)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OperRegionManager;
                    strTitle = "Corrective Action Completed";
                    form.FnthStatus = process.IsApprove ? Common.StatusList.N2CorrectiveCompleted : StatusList.N2Issued;
                    form.FnthUseridCorrective = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FnthUsernameCorrective = !process.IsApprove ? null : process.UserName;
                    form.FnthDesignationCorrective = !process.IsApprove ? null : process.UserDesignation;
                    form.FnthDtCorrective = !process.IsApprove ? null : process.ApproveDate;
                }
                else if (form.FnthStatus == Common.StatusList.N2CorrectiveCompleted)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OperRegionManager;
                    strTitle = "Corrective Action Accepted";
                    form.FnthStatus = process.IsApprove ? Common.StatusList.N2CorrectiveAccepted : StatusList.N2Received;
                    form.FnthUseridAccptd = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FnthUsernameAccptd = !process.IsApprove ? null : process.UserName;
                    form.FnthDesignationAccptd = !process.IsApprove ? null : process.UserDesignation;
                    form.FnthDtAccptd = !process.IsApprove ? null : process.ApproveDate;
                }
                else if (form.FnthStatus == Common.StatusList.N2CorrectiveAccepted)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.JKRSSuperiorOfficerSO;
                    strTitle = "Corrective Action Accepted";
                    form.FnthStatus = process.IsApprove ? Common.StatusList.N2PreventRecurrenceAccepted : StatusList.N2CorrectiveCompleted;
                    form.FnthUseridPreventive = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FnthUsernamePreventive = !process.IsApprove ? null : process.UserName;
                    form.FnthDesignationPreventive = !process.IsApprove ? null : process.UserDesignation;
                    form.FnthDtPreventive = !process.IsApprove ? null : process.ApproveDate;
                }
                else if (form.FnthStatus == Common.StatusList.N2PreventRecurrenceAccepted)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.JKRSSuperiorOfficerSO;
                    strTitle = "Verified";
                    form.FnthStatus = process.IsApprove ? Common.StatusList.Completed : StatusList.N2CorrectiveAccepted;
                    form.FnthUseridVer = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FnthUsernameVer = !process.IsApprove ? null : process.UserName;
                    form.FnthDesignationVer = !process.IsApprove ? null : process.UserDesignation;
                    if (process.IsApprove)
                    {
                        List<int> lstNotUserId = new List<int>();
                        if (form.FnthUseridIssued.HasValue)
                            lstNotUserId.Add(form.FnthUseridIssued.Value);
                        if (form.FnthUseridRcvd.HasValue)
                            lstNotUserId.Add(form.FnthUseridRcvd.Value);
                        if (form.FnthUseridCorrective.HasValue)
                            lstNotUserId.Add(form.FnthUseridCorrective.Value);
                        if (form.FnthUseridAccptd.HasValue)
                            lstNotUserId.Add(form.FnthUseridAccptd.Value);
                        if (form.FnthUseridVer.HasValue)
                            lstNotUserId.Add(form.FnthUseridVer.Value);
                        strNotUserID = string.Join(",", lstNotUserId.Distinct());
                    }
                }
                form.FnthAuditLog = Utility.ProcessLog(form.FnthAuditLog, strTitle, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                strNotMsg = (process.IsApprove ? "" : "Rejected - ") + strTitle + ":" + process.UserName + " - Form N2 (" + form.FnthNcrNo + ")";
                strNotURL = "/MAM/EditFormN2?headerId=" + form.FnthPkRefNo.ToString() + "&view=1";
                SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
        private async Task<int> SaveFormS1(DTO.RequestBO.ProcessDTO process)
        {
            var form = context.RmFormS1Hdr.Where(x => x.FsihPkRefNo == process.RefId).FirstOrDefault();
            if (form != null)
            {
                string strTitle = "";
                string strNotURL = "";
                string strNotMsg = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (form.FsihSubmitSts == true && (form.FsihStatus == Common.StatusList.S1Init))
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperationsExecutive : GroupNames.Supervisor;
                    form.FsihStatus = process.IsApprove ? Common.StatusList.S1Planned : StatusList.S1Init;
                    strTitle = "Planned";
                }
                else if (form.FsihStatus == Common.StatusList.S1Planned)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OperationsExecutive;
                    form.FsihStatus = process.IsApprove ? Common.StatusList.S1Vetted : StatusList.S1Init;
                    if (!process.IsApprove) { form.FsihSubmitSts = false; }
                    form.FsiihUseridVet = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FsiihUserNameVet = !process.IsApprove ? null : process.UserName;
                    form.FsiihUserDesignationVet = !process.IsApprove ? null : process.UserDesignation;
                    form.FsiihDtVet = !process.IsApprove ? null : process.ApproveDate;
                    strTitle = "Vetted";
                }
                else if (form.FsihStatus == Common.StatusList.S1Vetted)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.OperationsExecutive;
                    strTitle = "Agreed";
                    form.FsihStatus = process.IsApprove ? Common.StatusList.Completed : StatusList.S1Planned;
                    form.FsiihUseridAgrd = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FsiihUserNameAgrd = !process.IsApprove ? null : process.UserName;
                    form.FsiihUserDesignationAgrd = !process.IsApprove ? null : process.UserDesignation;
                    form.FsiihDtAgrd = !process.IsApprove ? null : process.ApproveDate;

                    if (process.IsApprove)
                    {
                        List<int> lstNotUserId = new List<int>();
                        if (form.FsiihUseridPlan.HasValue)
                            lstNotUserId.Add(form.FsiihUseridPlan.Value);
                        if (form.FsiihUseridVet.HasValue)
                            lstNotUserId.Add(form.FsiihUseridVet.Value);
                        if (form.FsiihUseridAgrd.HasValue)
                            lstNotUserId.Add(form.FsiihUseridAgrd.Value);
                        strNotUserID = string.Join(",", lstNotUserId.Distinct());
                    }
                }
                form.FsihAuditLog = Utility.ProcessLog(form.FsihAuditLog, strTitle, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                strNotMsg = (process.IsApprove ? "" : "Rejected - ") + strTitle + ":" + process.UserName + " - Form S1 (" + form.FsihRefId + ")";
                strNotURL = "/FormS1/View/" + form.FsihPkRefNo.ToString();
                SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
        private async Task<int> SaveFormS2(DTO.RequestBO.ProcessDTO process)
        {
            var form = context.RmFormS2Hdr.Where(x => x.FsiihPkRefNo == process.RefId).FirstOrDefault();
            if (form != null)
            {
                string strTitle = "";
                string strNotURL = "";
                string strNotMsg = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (form.FsiihSubmitSts == true && (form.FsiihStatus == Common.StatusList.S2Init))
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperationsExecutive : GroupNames.Supervisor;
                    form.FsiihStatus = process.IsApprove ? Common.StatusList.S2Submitted : StatusList.S2Init;
                    strTitle = "Submitted";
                }
                else if (form.FsiihStatus == Common.StatusList.S2Submitted)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OperationsExecutive;
                    form.FsiihStatus = process.IsApprove ? Common.StatusList.S2Vetted : StatusList.S2Init;
                    if (!process.IsApprove) { form.FsiihSubmitSts = false; }
                    form.FsiihUseridVet = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FsiihUserNameVet = !process.IsApprove ? null : process.UserName;
                    form.FsiihUserDesignationVet = !process.IsApprove ? null : process.UserDesignation;
                    form.FsiihDtVet = !process.IsApprove ? null : process.ApproveDate;
                    strTitle = "Vetted";
                }
                else if (form.FsiihStatus == Common.StatusList.S2Vetted)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.OperationsExecutive;
                    strTitle = "Agreed";
                    form.FsiihStatus = process.IsApprove ? Common.StatusList.Completed : StatusList.S2Submitted;
                    form.FsiihUseridAgrd = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FsiihUserNameAgrd = !process.IsApprove ? null : process.UserName;
                    form.FsiihUserDesignationAgrd = !process.IsApprove ? null : process.UserDesignation;
                    form.FsiihDtAgrd = !process.IsApprove ? null : process.ApproveDate;

                    if (process.IsApprove)
                    {
                        List<int> lstNotUserId = new List<int>();
                        if (form.FsiihUseridSub.HasValue)
                            lstNotUserId.Add(form.FsiihUseridSub.Value);
                        if (form.FsiihUseridSchld.HasValue)
                            lstNotUserId.Add(form.FsiihUseridSchld.Value);
                        if (form.FsiihUseridPrioritised.HasValue)
                            lstNotUserId.Add(form.FsiihUseridPrioritised.Value);
                        if (form.FsiihUseridVet.HasValue)
                            lstNotUserId.Add(form.FsiihUseridVet.Value);
                        if (form.FsiihUseridAgrd.HasValue)
                            lstNotUserId.Add(form.FsiihUseridAgrd.Value);
                        strNotUserID = string.Join(",", lstNotUserId.Distinct());
                    }
                }
                form.FsiihAuditLog = Utility.ProcessLog(form.FsiihAuditLog, strTitle, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                strNotMsg = (process.IsApprove ? "" : "Rejected - ") + strTitle + ":" + process.UserName + " - Form S2 (" + form.FsiihRefId + ")";
                strNotURL = "/FormS2/AddS2?id=" + form.FsiihPkRefNo.ToString() + "&isview=false";
                SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
        private async Task<int> SaveFormX(DTO.RequestBO.ProcessDTO process)
        {
            var form = context.RmFormXHdr.Where(x => x.FxhPkRefNo == process.RefId).FirstOrDefault();
            if (form != null)
            {
                string strTitle = "";
                string strNotURL = "";
                string strNotMsg = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (process.Stage == Common.StatusList.FormXInit)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.Supervisor : GroupNames.Supervisor;
                    strTitle = "Assigned";
                }
                else if (process.Stage == Common.StatusList.FormXWorkCompleted)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperationsExecutive : GroupNames.Supervisor;
                    strTitle = "Work Completed";
                }
                else if (process.Stage == Common.StatusList.FormXVetted)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OperationsExecutive;
                    strTitle = "Vetted";
                }
                else if (process.Stage == Common.StatusList.FormXVerified)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.OperationsExecutive;
                    strTitle = "Agreed";

                    if (process.IsApprove)
                    {
                        List<int> lstNotUserId = new List<int>();
                        if (form.FxhUseridAssgn.HasValue)
                            lstNotUserId.Add(form.FxhUseridAssgn.Value);
                        if (form.FxhUseridVer.HasValue)
                            lstNotUserId.Add(form.FxhUseridVer.Value);
                        if (form.FxhUseridVet.HasValue)
                            lstNotUserId.Add(form.FxhUseridVet.Value);
                        if (form.FxhUseridSchdVer.HasValue)
                            lstNotUserId.Add(form.FxhUseridSchdVer.Value);
                        if (form.FxhUseridPrp.HasValue)
                            lstNotUserId.Add(form.FxhUseridPrp.Value);
                        strNotUserID = string.Join(",", lstNotUserId.Distinct());
                    }
                }
                //form.FsiihAuditLog = Utility.ProcessLog(form.FsiihAuditLog, strTitle, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                strNotMsg = (process.IsApprove ? "" : "Rejected - ") + strTitle + ":" + process.UserName + " - Form X (" + form.FxhRefId + ")";
                strNotURL = "/ERT/FormX?vid=" + form.FxhPkRefNo.ToString();
                SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
        private async Task<int> SaveFormD(DTO.RequestBO.ProcessDTO process)
        {

            var form = context.RmFormDHdr.Where(x => x.FdhPkRefNo == process.RefId).FirstOrDefault();
            if (form != null)
            {
                string strTitle = "";
                string strNotURL = "";
                string strNotMsg = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (form.FdhStatus == Common.StatusList.Executive)
                {
                    if (!process.IsApprove) { form.FdhSubmitSts = false; }
                    strNotGroupName = process.IsApprove ? GroupNames.OpeHeadMaintenance : GroupNames.Supervisor;
                    form.FdhStatus = process.IsApprove ? Common.StatusList.HeadMaintenance : Common.StatusList.Supervisor;
                    form.FdhUseridVer = !process.IsApprove ? null : process.UserID;
                    form.FdhUsernameVer = !process.IsApprove ? null : process.UserName;
                    form.FdhDesignationVer = !process.IsApprove ? null : process.UserDesignation;
                    form.FdhDtVer = !process.IsApprove ? null : process.ApproveDate;
                    strTitle = "Verified By";
                }
                else if (form.FdhStatus == Common.StatusList.HeadMaintenance)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OperationsExecutive;
                    form.FdhStatus = process.IsApprove ? Common.StatusList.VerifiedJKRSSuperior : StatusList.Executive;
                    form.FdhUseridVet = !process.IsApprove ? null : process.UserID;
                    form.FdhUsernameVet = !process.IsApprove ? null : process.UserName;
                    form.FdhDesignationVet = !process.IsApprove ? null : process.UserDesignation;
                    form.FdhDtVet = !process.IsApprove ? null : process.ApproveDate;
                    strTitle = "Vetted By";
                }
                else if (form.FdhStatus == Common.StatusList.VerifiedJKRSSuperior)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OpeHeadMaintenance;
                    form.FdhStatus = process.IsApprove ? Common.StatusList.ProcessedJKRSSuperior : StatusList.HeadMaintenance;
                    form.FdhUseridVerSo = !process.IsApprove ? null : process.UserID;
                    form.FdhUsernameVerSo = !process.IsApprove ? null : process.UserName;
                    form.FdhDesignationVerSo = !process.IsApprove ? null : process.UserDesignation;
                    form.FdhDtVerSo = !process.IsApprove ? null : process.ApproveDate;
                    strTitle = "S.O Verified By";
                }
                else if (form.FdhStatus == Common.StatusList.ProcessedJKRSSuperior)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.JKRSSuperiorOfficerSO;
                    form.FdhStatus = process.IsApprove ? Common.StatusList.AgreedJKRSSuperior : StatusList.VerifiedJKRSSuperior;
                    form.FdhUseridPrcdSo = !process.IsApprove ? null : process.UserID;
                    form.FdhUsernamePrcdSo = !process.IsApprove ? null : process.UserName;
                    form.FdhDesignationPrcdSo = !process.IsApprove ? null : process.UserDesignation;
                    form.FdhDtPrcdSo = !process.IsApprove ? null : process.ApproveDate;
                    strTitle = "S.O Processed By";
                }
                else if (form.FdhStatus == Common.StatusList.AgreedJKRSSuperior)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.JKRSSuperiorOfficerSO;
                    form.FdhStatus = process.IsApprove ? Common.StatusList.Completed : StatusList.ProcessedJKRSSuperior;
                    form.FdhUseridAgrdSo = !process.IsApprove ? null : process.UserID;
                    form.FdhUsernameAgrdSo = !process.IsApprove ? null : process.UserName;
                    form.FdhDesignationAgrdSo = !process.IsApprove ? null : process.UserDesignation;
                    form.FdhDtAgrdSo = !process.IsApprove ? null : process.ApproveDate;
                    strTitle = "S.O Agreed By";
                    if (process.IsApprove)
                    {
                        List<string> lstNotUserId = new List<string>();
                        lstNotUserId.Add(form.FdhUseridAgrdSo);
                        lstNotUserId.Add(form.FdhUseridPrcdSo);
                        lstNotUserId.Add(form.FdhUseridVerSo);
                        lstNotUserId.Add(form.FdhUseridVet);
                        lstNotUserId.Add(form.FdhUseridVer);
                        lstNotUserId.Add(form.FdhUseridPrp);
                        strNotUserID = string.Join(",", lstNotUserId.Distinct());
                    }
                }
                form.FdhAuditLog = Utility.ProcessLog(form.FdhAuditLog, strTitle, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                strNotMsg = (process.IsApprove ? "" : "Rejected - ") + strTitle + ":" + process.UserName + " - Form D (" + form.FdhRefId + ")";
                strNotURL = "/ERT/EditFormD?id=" + form.FdhPkRefNo.ToString() + "&view=1";
                SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
        private async Task<int> SaveFormA(DTO.RequestBO.ProcessDTO process)
        {
            var form = context.RmFormAHdr.Where(x => x.FahPkRefNo == process.RefId).FirstOrDefault();
            if (form != null)
            {
                string strTitle = "";
                string strNotURL = "";
                string strNotMsg = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (form.FahSubmitSts == true && (form.FahStatus == Common.StatusList.FormAInit))
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperationsExecutive : GroupNames.Supervisor;
                    form.FahStatus = process.IsApprove ? Common.StatusList.FormAInspected : StatusList.FormAInit;
                    strTitle = StatusList.FormFCInspected;
                }
                else if (form.FahStatus == Common.StatusList.FormAInspected)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OpeHeadMaintenance : GroupNames.OperationsExecutive;
                    form.FahStatus = process.IsApprove ? Common.StatusList.FormAExecutiveApproved : StatusList.FormAInit;
                    if (!process.IsApprove) { form.FahSubmitSts = false; }
                    strTitle = Common.StatusList.FormAExecutiveApproved;
                }
                else if (form.FahStatus == Common.StatusList.FormAExecutiveApproved)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperRegionManager : GroupNames.OperationsExecutive;
                    form.FahStatus = process.IsApprove ? Common.StatusList.FormAHeadMaintenanceApproved : StatusList.FormAExecutiveApproved;
                    strTitle = Common.StatusList.FormAHeadMaintenanceApproved;
                }
                else if (form.FahStatus == Common.StatusList.FormAHeadMaintenanceApproved)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OperRegionManager;
                    form.FahStatus = process.IsApprove ? Common.StatusList.FormARegionManagerApproved : StatusList.FormAExecutiveApproved;
                    strTitle = Common.StatusList.FormARegionManagerApproved;
                }
                else if (form.FahStatus == Common.StatusList.FormARegionManagerApproved)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.JKRSSuperiorOfficerSO;
                    form.FahStatus = process.IsApprove ? Common.StatusList.Completed : StatusList.FormAHeadMaintenanceApproved;
                    strTitle = "Verified";
                    form.FahUseridVer = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FahUsernameVer = !process.IsApprove ? null : process.UserName;
                    form.FahDesignationVer = !process.IsApprove ? null : process.UserDesignation;
                    form.FahDtVer = !process.IsApprove ? null : process.ApproveDate;

                    if (process.IsApprove)
                    {
                        List<int> lstNotUserId = new List<int>();
                        if (form.FahUseridVer.HasValue)
                            lstNotUserId.Add(form.FahUseridVer.Value);
                        if (form.FahUseridPrp.HasValue)
                            lstNotUserId.Add(form.FahUseridPrp.Value);
                        strNotUserID = string.Join(",", lstNotUserId.Distinct());
                    }
                }
                form.FahAuditLog = Utility.ProcessLog(form.FahAuditLog, strTitle, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                strNotMsg = (process.IsApprove ? "" : "Rejected - ") + strTitle + ":" + process.UserName + " - Form A (" + form.FahRefId + ")";
                strNotURL = "/NOD?vid=" + form.FahPkRefNo.ToString();
                SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
        private async Task<int> SaveFormJ(DTO.RequestBO.ProcessDTO process)
        {
            var form = context.RmFormJHdr.Where(x => x.FjhPkRefNo == process.RefId).FirstOrDefault();
            if (form != null)
            {
                string strTitle = "";
                string strNotURL = "";
                string strNotMsg = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (form.FjhSubmitSts == true && (form.FjhStatus == Common.StatusList.FormJInit))
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperationsExecutive : GroupNames.Supervisor;
                    form.FjhStatus = process.IsApprove ? Common.StatusList.FormJInspected : StatusList.FormJInit;
                    strTitle = StatusList.FormFCInspected;
                }
                else if (form.FjhStatus == Common.StatusList.FormJInspected)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OpeHeadMaintenance : GroupNames.OperationsExecutive;
                    form.FjhStatus = process.IsApprove ? Common.StatusList.FormJChecked : StatusList.FormJInit;
                    if (!process.IsApprove) { form.FjhSubmitSts = false; }
                    form.FjhUseridVer = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FjhUsernameVer = !process.IsApprove ? null : process.UserName;
                    form.FjhDesignationVer = !process.IsApprove ? null : process.UserDesignation;
                    form.FjhDtVer = !process.IsApprove ? null : process.ApproveDate;
                    strTitle = Common.StatusList.FormJChecked;
                }
                else if (form.FjhStatus == Common.StatusList.FormJChecked)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperRegionManager : GroupNames.OperationsExecutive;
                    form.FjhStatus = process.IsApprove ? Common.StatusList.FormJHeadMaintenanceApproved : StatusList.FormJInspected;
                    strTitle = Common.StatusList.FormJHeadMaintenanceApproved;
                }
                else if (form.FjhStatus == Common.StatusList.FormJHeadMaintenanceApproved)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OperRegionManager;
                    form.FjhStatus = process.IsApprove ? Common.StatusList.FormJRegionManagerApproved : StatusList.FormJChecked;
                    strTitle = Common.StatusList.FormJRegionManagerApproved;
                }
                else if (form.FjhStatus == Common.StatusList.FormJRegionManagerApproved)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.JKRSSuperiorOfficerSO;
                    form.FjhStatus = process.IsApprove ? Common.StatusList.Completed : StatusList.FormJHeadMaintenanceApproved;
                    strTitle = "Audited";
                    form.FjhUseridVet = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FjhUsernameVet = !process.IsApprove ? null : process.UserName;
                    form.FjhDesignationVet = !process.IsApprove ? null : process.UserDesignation;
                    form.FjhDtVet = !process.IsApprove ? null : process.ApproveDate;

                    if (process.IsApprove)
                    {
                        List<int> lstNotUserId = new List<int>();
                        if (form.FjhUseridVer.HasValue)
                            lstNotUserId.Add(form.FjhUseridVer.Value);
                        if (form.FjhUseridPrp.HasValue)
                            lstNotUserId.Add(form.FjhUseridPrp.Value);
                        if (form.FjhUseridVet.HasValue)
                            lstNotUserId.Add(form.FjhUseridVet.Value);
                        strNotUserID = string.Join(",", lstNotUserId.Distinct());
                    }
                }
                form.FjhAuditLog = Utility.ProcessLog(form.FjhAuditLog, strTitle, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                strNotMsg = (process.IsApprove ? "" : "Rejected - ") + strTitle + ":" + process.UserName + " - Form J (" + form.FjhRefId + ")";
                strNotURL = "/NOD/FormJ?vid=" + form.FjhPkRefNo.ToString();
                SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
        private async Task<int> SaveFormH(DTO.RequestBO.ProcessDTO process)
        {
            var form = context.RmFormHHdr.Where(x => x.FhhPkRefNo == process.RefId).FirstOrDefault();
            if (form != null)
            {
                string strTitle = "";
                string strNotURL = "";
                string strNotMsg = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (form.FhhSubmitSts == true && (form.FhhStatus == Common.StatusList.FormHInit))
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OpeHeadMaintenance : GroupNames.OperationsExecutive;
                    form.FhhStatus = process.IsApprove ? Common.StatusList.FormHReported : StatusList.FormHInit;
                    strTitle = Common.StatusList.FormJChecked;
                }
                else if (form.FhhStatus == Common.StatusList.FormHReported)
                {
                    if (!process.IsApprove) { form.FhhSubmitSts = false; }
                    strNotGroupName = process.IsApprove ? GroupNames.OperRegionManager : GroupNames.OperationsExecutive;
                    form.FhhStatus = process.IsApprove ? Common.StatusList.FormHVerified : StatusList.FormHInit;
                    form.FhhUseridVer = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FhhUsernameVer = !process.IsApprove ? null : process.UserName;
                    form.FhhDesignationVer = !process.IsApprove ? null : process.UserDesignation;
                    form.FhhDtVer = !process.IsApprove ? null : process.ApproveDate;
                    strTitle = Common.StatusList.FormHVerified;
                }
                else if (form.FhhStatus == Common.StatusList.FormHVerified)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OperRegionManager;
                    form.FhhStatus = process.IsApprove ? Common.StatusList.FormHRegionManagerApproved : StatusList.FormHReported;
                    strTitle = Common.StatusList.FormHRegionManagerApproved;
                }
                else if (form.FhhStatus == Common.StatusList.FormHRegionManagerApproved)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.JKRSSuperiorOfficerSO;
                    form.FhhStatus = process.IsApprove ? Common.StatusList.FormHReceived : StatusList.FormHVerified;
                    form.FhhUseridRcvdAuth = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FhhUsernameRcvdAuth = !process.IsApprove ? null : process.UserName;
                    form.FhhDesignationRcvdAuth = !process.IsApprove ? null : process.UserDesignation;
                    form.FhhDtRcvdAuth = !process.IsApprove ? null : process.ApproveDate;
                    strTitle = Common.StatusList.FormHReceived;
                }
                else if (form.FhhStatus == Common.StatusList.FormHReceived)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.JKRSSuperiorOfficerSO;
                    form.FhhStatus = process.IsApprove ? Common.StatusList.Completed : StatusList.FormHRegionManagerApproved;
                    strTitle = "Vetted";
                    form.FhhUseridVetAuth = !process.IsApprove ? null : Utility.ToNullInt(process.UserID);
                    form.FhhUsernameVetAuth = !process.IsApprove ? null : process.UserName;
                    form.FhhDesignationVetAuth = !process.IsApprove ? null : process.UserDesignation;
                    form.FhhDtVetAuth = !process.IsApprove ? null : process.ApproveDate;

                    if (process.IsApprove)
                    {
                        List<int> lstNotUserId = new List<int>();
                        if (form.FhhUseridVer.HasValue)
                            lstNotUserId.Add(form.FhhUseridVer.Value);
                        if (form.FhhUseridVetAuth.HasValue)
                            lstNotUserId.Add(form.FhhUseridVetAuth.Value);
                        if (form.FhhUseridRcvdAuth.HasValue)
                            lstNotUserId.Add(form.FhhUseridRcvdAuth.Value);
                        if (form.FhhUseridPrp.HasValue)
                            lstNotUserId.Add(form.FhhUseridPrp.Value);
                        strNotUserID = string.Join(",", lstNotUserId.Distinct());
                    }
                }
                form.FhhAuditLog = Utility.ProcessLog(form.FhhAuditLog, strTitle, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                strNotMsg = (process.IsApprove ? "" : "Rejected - ") + strTitle + ":" + process.UserName + " - Form H (" + form.FhhRefId + ")";
                strNotURL = "/NOD/FormH?vid=" + form.FhhPkRefNo.ToString();
                SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
        private async Task<int> SaveFormC1C2(DTO.RequestBO.ProcessDTO process)
        {

            var form = context.RmFormCvInsHdr.Where<RmFormCvInsHdr>(x => x.FcvihPkRefNo == process.RefId).FirstOrDefault();
            if (form != null)
            {
                string strTitle = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (form.FcvihSubmitSts && form.FcvihStatus == StatusList.FormC1C2Init)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperationsExecutive : GroupNames.Supervisor;
                    form.FcvihStatus = process.IsApprove ? StatusList.FormC1C2Inspected : StatusList.FormC1C2Init;
                    strTitle = StatusList.FormC1C2Inspected;
                }
                else if (form.FcvihStatus == StatusList.FormC1C2Inspected)
                {
                    if (!process.IsApprove)
                        form.FcvihSubmitSts = false;
                    strNotGroupName = process.IsApprove ? GroupNames.OpeHeadMaintenance : GroupNames.Supervisor;
                    form.FcvihStatus = process.IsApprove ? StatusList.FormC1C2ExecutiveApproved : StatusList.FormC1C2Init;
                    strTitle = StatusList.FormC1C2ExecutiveApproved;
                }
                else if (form.FcvihStatus == StatusList.FormC1C2ExecutiveApproved)
                {
                    if (!process.IsApprove)
                        form.FcvihSubmitSts = false;
                    strNotGroupName = process.IsApprove ? GroupNames.OperRegionManager : GroupNames.OperationsExecutive;
                    form.FcvihStatus = process.IsApprove ? StatusList.FormC1C2HeadMaintenanceApproved : StatusList.FormC1C2Inspected;
                    strTitle = StatusList.FormC1C2HeadMaintenanceApproved;
                }
                else if (form.FcvihStatus == StatusList.FormC1C2HeadMaintenanceApproved)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OpeHeadMaintenance;
                    form.FcvihStatus = process.IsApprove ? StatusList.RegionManager : StatusList.FormC1C2ExecutiveApproved;
                    strTitle = StatusList.RegionManager;
                }
                else if (form.FcvihStatus == StatusList.RegionManager)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.OperRegionManager;
                    form.FcvihStatus = process.IsApprove ? StatusList.Completed : StatusList.FormC1C2HeadMaintenanceApproved;
                    strTitle = "Completed / Audited";
                    form.FcvihUserIdAud = !process.IsApprove ? new int?() : Utility.ToNullInt((object)process.UserID);
                    form.FcvihUserNameAud = !process.IsApprove ? (string)null : process.UserName;
                    form.FcvihUserDesignationAud = !process.IsApprove ? (string)null : process.UserDesignation;
                    form.FcvihDtAud = !process.IsApprove ? new DateTime?() : process.ApproveDate;
                    if (process.IsApprove)
                    {
                        List<int> lstNotUserId = new List<int>();
                      
                        if (form.FcvihSerProviderUserId.HasValue)
                            lstNotUserId.Add(form.FcvihSerProviderUserId.Value);
                        
                        if (form.FcvihUserIdAud.HasValue)
                            lstNotUserId.Add(form.FcvihUserIdAud.Value);
                        
                        strNotUserID = string.Join(",", lstNotUserId.Distinct());
                    }
                }
                form.FcvihAuditLog = Utility.ProcessLog(form.FcvihAuditLog, strTitle, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                string strNotMsg = (process.IsApprove ? "Approved - " : "Rejected - ") + strTitle + ":" + process.UserName + " - Form C1C2 (" + form.FcvihCInspRefNo + ")";
                string strNotURL = "/FormC1C2/View/" + form.FcvihPkRefNo.ToString();
                
                SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
        private async Task<int> SaveFormB1B2(DTO.RequestBO.ProcessDTO process)
        {
            var form = context.RmFormB1b2BrInsHdr.Where(x => x.FbrihPkRefNo == process.RefId).FirstOrDefault();

            if (form != null)
            {
                string strTitle = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (form.FbrihSubmitSts && form.FbrihStatus == Common.StatusList.FormB1B2Init)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperationsExecutive : GroupNames.Supervisor;
                    form.FbrihStatus = process.IsApprove ? Common.StatusList.FormB1B2Inspected : StatusList.FormB1B2Init;
                    strTitle = Common.StatusList.FormB1B2Inspected;
                }
                else if (form.FbrihStatus == StatusList.FormB1B2Inspected)
                {
                    if (!process.IsApprove)
                        form.FbrihSubmitSts = false;
                    strNotGroupName = process.IsApprove ? GroupNames.OpeHeadMaintenance : GroupNames.Supervisor;
                    form.FbrihStatus = process.IsApprove ? StatusList.FormB1B2ExecutiveApproved : StatusList.FormB1B2Init;
                    strTitle = Common.StatusList.FormB1B2ExecutiveApproved;
                }
                else if (form.FbrihStatus == StatusList.FormB1B2ExecutiveApproved)
                {
                    if (!process.IsApprove)
                        form.FbrihSubmitSts = false;
                    strNotGroupName = process.IsApprove ? GroupNames.OperRegionManager : GroupNames.OperationsExecutive;
                    form.FbrihStatus = process.IsApprove ? Common.StatusList.FormB1B2HeadMaintenanceApproved : StatusList.FormB1B2Inspected;
                    strTitle = Common.StatusList.FormB1B2HeadMaintenanceApproved;
                }
                else if (form.FbrihStatus == StatusList.FormB1B2HeadMaintenanceApproved)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OpeHeadMaintenance;
                    form.FbrihStatus = process.IsApprove ? StatusList.RegionManager : StatusList.FormB1B2ExecutiveApproved;
                    strTitle = StatusList.RegionManager;
                }
                else if (form.FbrihStatus == StatusList.RegionManager)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.OperRegionManager;
                    form.FbrihStatus = process.IsApprove ? StatusList.Completed : StatusList.FormB1B2HeadMaintenanceApproved;
                    strTitle = "Completed / Audited";
                    form.FbrihUserIdAud = !process.IsApprove ? new int?() : Utility.ToNullInt((object)process.UserID);
                    form.FbrihUserNameAud = !process.IsApprove ? (string)null : process.UserName;
                    form.FbrihUserDesignationAud = !process.IsApprove ? (string)null : process.UserDesignation;
                    form.FbrihDtAud = !process.IsApprove ? new DateTime?() : process.ApproveDate;

                    if (process.IsApprove)
                    {
                        List<int> lstNotUserId = new List<int>();

                        if (form.FbrihSerProviderUserId.HasValue)
                            lstNotUserId.Add(form.FbrihSerProviderUserId.Value);

                        if (form.FbrihUserIdAud.HasValue)
                            lstNotUserId.Add(form.FbrihUserIdAud.Value);


                        strNotUserID = string.Join(",", lstNotUserId.Distinct());
                    }
                }
                form.FbrihAuditLog = Utility.ProcessLog(form.FbrihAuditLog, strTitle, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                string strNotMsg = (process.IsApprove ? "Approved - " : "Rejected - ") + strTitle + ":" + process.UserName + " - Form B1B2 (" + form.FbrihCInspRefNo + ")";
                string strNotURL = "/FormB1B2/Add?id=" + form.FbrihPkRefNo.ToString() + "&isview=true";

                SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
        private async Task<int> SaveFormF2(DTO.RequestBO.ProcessDTO process)
        {

            var form = context.RmFormF2GrInsHdr.Where<RmFormF2GrInsHdr>(x => x.FgrihPkRefNo == process.RefId).FirstOrDefault();
            if (form != null)
            {
                string strTitle = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (form.FgrihSubmitSts && form.FgrihStatus == StatusList.FormF2Init)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperationsExecutive : GroupNames.Supervisor;
                    form.FgrihStatus = process.IsApprove ? StatusList.FormF2Inspected : StatusList.FormF2Init;
                    strTitle = StatusList.FormF2Inspected;
                }
                else if (form.FgrihStatus == StatusList.FormF2Inspected)
                {
                    if (!process.IsApprove)
                        form.FgrihSubmitSts = false;
                    strNotGroupName = process.IsApprove ? GroupNames.OpeHeadMaintenance : GroupNames.Supervisor;
                    form.FgrihStatus = process.IsApprove ? StatusList.FormF2ExecutiveApproved : StatusList.FormF2Init;
                    strTitle = StatusList.FormF2ExecutiveApproved;
                }
                else if (form.FgrihStatus == StatusList.FormF2ExecutiveApproved)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperRegionManager : GroupNames.OperationsExecutive;
                    form.FgrihStatus = process.IsApprove ? StatusList.FormB1B2HeadMaintenanceApproved : StatusList.FormF2Inspected;
                    strTitle = StatusList.FormB1B2HeadMaintenanceApproved;
                }
                else if (form.FgrihStatus == StatusList.FormB1B2HeadMaintenanceApproved)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OpeHeadMaintenance;
                    form.FgrihStatus = process.IsApprove ? StatusList.RegionManager : StatusList.FormF2ExecutiveApproved;
                    strTitle = StatusList.RegionManager;
                }
                else if (form.FgrihStatus == StatusList.RegionManager)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.OperRegionManager;
                    form.FgrihStatus = process.IsApprove ? StatusList.Completed : StatusList.FormB1B2HeadMaintenanceApproved;
                    strTitle = "Completed";
                    if (process.IsApprove)
                    {
                        List<int> lstNotUserId = new List<int>();

                        if (form.FgrihUserIdInspBy.HasValue)
                            lstNotUserId.Add(form.FgrihUserIdInspBy.Value);
                        strNotUserID = string.Join(",", lstNotUserId.Distinct());
                    }
                }
                form.FgrihAuditLog = Utility.ProcessLog(form.FgrihAuditLog, strTitle, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                string strNotMsg = (process.IsApprove ? "Approved - " : "Rejected - ") + strTitle + ":" + process.UserName + " - Form F2 (" + form.FgrihFormRefId + ")";
                string strNotURL = "/FormF2/Add?id=" + form.FgrihPkRefNo.ToString() + "&isview=true";

                SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
        private async Task<int> SaveFormF4(DTO.RequestBO.ProcessDTO process)
        {
            var form = context.RmFormF4InsHdr.Where<RmFormF4InsHdr>(x => x.FivahPkRefNo == process.RefId).FirstOrDefault();
            if (form != null)
            {
                string title = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (form.FivahSubmitSts && form.FivahStatus == StatusList.FormF4Init)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperationsExecutive : GroupNames.Supervisor;
                    form.FivahStatus = process.IsApprove ? StatusList.FormF4Inspected : StatusList.FormF4Init;
                    title = StatusList.FormF4Inspected;
                }
                else if (form.FivahStatus == StatusList.FormF4Inspected)
                {
                    if (!process.IsApprove)
                        form.FivahSubmitSts = false;
                    strNotGroupName = process.IsApprove ? GroupNames.OpeHeadMaintenance : GroupNames.Supervisor;
                    form.FivahStatus = process.IsApprove ? StatusList.FormF4ExecutiveApproved : StatusList.FormF4Init;
                    title = StatusList.FormF4ExecutiveApproved;
                }
                else if (form.FivahStatus == StatusList.FormF4ExecutiveApproved)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperRegionManager : GroupNames.OperationsExecutive;
                    form.FivahStatus = process.IsApprove ? StatusList.FormF4HeadMaintenanceApproved : StatusList.FormF4Inspected;
                    title = StatusList.FormF4HeadMaintenanceApproved;
                }
                else if (form.FivahStatus == StatusList.FormF4HeadMaintenanceApproved)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OpeHeadMaintenance;
                    form.FivahStatus = process.IsApprove ? StatusList.RegionManager : StatusList.FormF4ExecutiveApproved;
                    title = StatusList.RegionManager;
                }
                else if (form.FivahStatus == StatusList.RegionManager)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.OperRegionManager;
                    form.FivahStatus = process.IsApprove ? StatusList.Completed : StatusList.FormF4HeadMaintenanceApproved;
                    title = "Completed";
                    if (process.IsApprove)
                    {
                        List<int> lstNotUserId = new List<int>();

                        if (form.FivahUserIdInspBy.HasValue)
                            lstNotUserId.Add(form.FivahUserIdInspBy.Value);

                        strNotUserID = string.Join(",", lstNotUserId.Distinct());
                    }
                }
                form.FivahAuditLog = Utility.ProcessLog(form.FivahAuditLog, title, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                string strNotMsg = (process.IsApprove ? "Approved - " : "Rejected - ") + title + ":" + process.UserName + " - Form F4 (" + form.FivahFormRefId + ")";
                string strNotURL = "/FormF4/View/" + form.FivahPkRefNo.ToString();

                SaveNotification(new RmUserNotification
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
        private async Task<int> SaveFormF5(DTO.RequestBO.ProcessDTO process)
        {

            var form = context.RmFormF5InsHdr.Where<RmFormF5InsHdr>(x => x.FvahPkRefNo == process.RefId).FirstOrDefault();
            if (form != null)
            {
                string strTitle = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (form.FvahSubmitSts && form.FvahStatus == StatusList.FormF5Init)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperationsExecutive : GroupNames.Supervisor;
                    form.FvahStatus = process.IsApprove ? StatusList.FormF5Inspected : StatusList.FormF5Init;
                    strTitle = StatusList.FormF5Inspected;
                }
                else if (form.FvahStatus == StatusList.FormF5Inspected)
                {
                    if (!process.IsApprove)
                        form.FvahSubmitSts = false;
                    strNotGroupName = process.IsApprove ? GroupNames.OpeHeadMaintenance : GroupNames.Supervisor;
                    form.FvahStatus = process.IsApprove ? StatusList.FormB1B2ExecutiveApproved : StatusList.FormF5Init;
                    strTitle = StatusList.FormB1B2ExecutiveApproved;
                }
                else if (form.FvahStatus == StatusList.FormB1B2ExecutiveApproved)
                {
                    if (!process.IsApprove)
                        form.FvahSubmitSts = false;
                    strNotGroupName = process.IsApprove ? GroupNames.OperRegionManager : GroupNames.OperationsExecutive;
                    form.FvahStatus = process.IsApprove ? StatusList.FormF5HeadMaintenanceApproved : StatusList.FormF5Inspected;
                    strTitle = StatusList.FormF5HeadMaintenanceApproved;
                }
                else if (form.FvahStatus == StatusList.FormF5HeadMaintenanceApproved)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OpeHeadMaintenance;
                    form.FvahStatus = process.IsApprove ? StatusList.RegionManager : StatusList.FormB1B2ExecutiveApproved;
                    strTitle = StatusList.RegionManager;
                }
                else if (form.FvahStatus == StatusList.RegionManager)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.OperRegionManager;
                    form.FvahStatus = process.IsApprove ? StatusList.Completed : StatusList.FormF5HeadMaintenanceApproved;
                    strTitle = "Completed";
                    if (process.IsApprove)
                    {
                        List<int> lstNotUserId = new List<int>();
                        if (form.FvahUserIdInspBy.HasValue)
                            lstNotUserId.Add(form.FvahUserIdInspBy.Value);

                        strNotUserID = string.Join(",", lstNotUserId.Distinct());
                    }
                }
                form.FvahAuditLog = Utility.ProcessLog(form.FvahAuditLog, strTitle, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                string strNotMsg = (process.IsApprove ? "Approved - " : "Rejected - ") + strTitle + ":" + process.UserName + " - Form F5 (" + form.FvahFormRefId + ")";
                string strNotURL = "/FormF5/View/" + form.FvahPkRefNo.ToString();

                SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
        private async Task<int> SaveFormFC(DTO.RequestBO.ProcessDTO process)
        {

            var form = context.RmFormFcInsHdr.Where<RmFormFcInsHdr>(x => x.FcihPkRefNo == process.RefId).FirstOrDefault();
            if (form != null)
            {
                string strTitle = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (form.FcihSubmitSts && form.FcihStatus == StatusList.FormFCInit)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperationsExecutive : GroupNames.Supervisor;
                    form.FcihStatus = process.IsApprove ? StatusList.FormFCInspected : StatusList.FormFCInit;
                    strTitle = StatusList.FormFCInspected;
                }
                else if (form.FcihStatus == StatusList.FormFCInspected)
                {
                    if (!process.IsApprove)
                        form.FcihSubmitSts = false;
                    strNotGroupName = process.IsApprove ? GroupNames.OpeHeadMaintenance : GroupNames.Supervisor;
                    form.FcihStatus = process.IsApprove ? StatusList.FormB1B2ExecutiveApproved : StatusList.FormFCInit;
                    strTitle = StatusList.FormB1B2ExecutiveApproved;
                }
                else if (form.FcihStatus == StatusList.FormB1B2ExecutiveApproved)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperRegionManager : GroupNames.OperationsExecutive;
                    form.FcihStatus = process.IsApprove ? StatusList.FormB1B2HeadMaintenanceApproved : StatusList.FormFCInspected;
                    strTitle = StatusList.FormB1B2HeadMaintenanceApproved;
                }
                else if (form.FcihStatus == StatusList.FormB1B2HeadMaintenanceApproved)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OpeHeadMaintenance;
                    form.FcihStatus = process.IsApprove ? StatusList.RegionManager : StatusList.FormB1B2ExecutiveApproved;
                    strTitle = StatusList.RegionManager;
                }
                else if (form.FcihStatus == StatusList.RegionManager)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.OperRegionManager;
                    form.FcihStatus = process.IsApprove ? StatusList.Completed : StatusList.FormB1B2HeadMaintenanceApproved;
                    strTitle = "Completed";
                    if (process.IsApprove)
                    {
                        List<int> lstNotUserId = new List<int>();

                        if (form.FcihUserIdInspBy.HasValue)
                            lstNotUserId.Add(form.FcihUserIdInspBy.Value);

                        strNotUserID = string.Join(",", lstNotUserId.Distinct());
                    }
                }
                form.FcihAuditLog = Utility.ProcessLog(form.FcihAuditLog, strTitle, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                string strNotMsg = (process.IsApprove ? "Approved - " : "Rejected - ") + strTitle + ":" + process.UserName + " - Form FC (" + form.FcihFormRefId + ")";
                string strNotURL = "/FormFC/View/" + form.FcihPkRefNo.ToString();

                SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
        private async Task<int> SaveFormFD(DTO.RequestBO.ProcessDTO process)
        {

            var form = context.RmFormFdInsHdr.Where<RmFormFdInsHdr>(x => x.FdihPkRefNo == process.RefId).FirstOrDefault();
            if (form != null)
            {
                string title = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (form.FdihSubmitSts && form.FdihStatus == StatusList.FormFDInit)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperationsExecutive : GroupNames.Supervisor;
                    form.FdihStatus = process.IsApprove ? StatusList.FormFDInspected : StatusList.FormFDInit;
                    title = StatusList.FormFDInspected;
                }
                else if (form.FdihStatus == StatusList.FormFDInspected)
                {
                    if (!process.IsApprove)
                        form.FdihSubmitSts = false;
                    strNotGroupName = process.IsApprove ? GroupNames.OpeHeadMaintenance : GroupNames.Supervisor;
                    form.FdihStatus = process.IsApprove ? StatusList.FormFDExecutiveApproved : StatusList.FormFDInit;
                    title = StatusList.FormFDExecutiveApproved;
                }
                else if (form.FdihStatus == StatusList.FormFDExecutiveApproved)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OperRegionManager : GroupNames.OperationsExecutive;
                    form.FdihStatus = process.IsApprove ? StatusList.FormFDHeadMaintenanceApproved : StatusList.FormFDInspected;
                    title = StatusList.FormFDHeadMaintenanceApproved;
                }
                else if (form.FdihStatus == StatusList.FormFDHeadMaintenanceApproved)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OpeHeadMaintenance;
                    form.FdihStatus = process.IsApprove ? StatusList.RegionManager : StatusList.FormFDExecutiveApproved;
                    title = StatusList.RegionManager;
                }
                else if (form.FdihStatus == StatusList.RegionManager)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.OperRegionManager;
                    form.FdihStatus = process.IsApprove ? StatusList.Completed : StatusList.FormFDHeadMaintenanceApproved;
                    title = "Completed";
                    if (process.IsApprove)
                    {
                        List<int> lstNotUserId = new List<int>();

                        if (form.FdihUserIdInspBy.HasValue)
                            lstNotUserId.Add(form.FdihUserIdInspBy.Value);

                        strNotUserID = string.Join(",", lstNotUserId.Distinct());
                    }
                }
                form.FdihAuditLog = Utility.ProcessLog(form.FdihAuditLog, title, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                string strNotMsg = (process.IsApprove ? "Approved - " : "Rejected - ") + title + ":" + process.UserName + " - Form FD (" + form.FdihFormRefId + ")";
                string strNotURL = "/FormFD/View/" + form.FdihPkRefNo.ToString();

                SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
        private async Task<int> SaveFormFS(DTO.RequestBO.ProcessDTO process)
        {

            var form = context.RmFormFsInsHdr.Where<RmFormFsInsHdr>(x => x.FshPkRefNo == process.RefId).FirstOrDefault();
            if (form != null)
            {
                string strTitle = "";
                string strNotGroupName = "";
                string strNotUserID = "";
                if (form.FshSubmitSts && form.FshStatus == StatusList.FormFSInit)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.OpeHeadMaintenance : "Supervisor";
                    form.FshStatus = process.IsApprove ? StatusList.FormFSSummarized : StatusList.FormFSInit;
                    strTitle = StatusList.FormFSSummarized;
                }
                else if (form.FshStatus == StatusList.FormFSSummarized)
                {
                    if (!process.IsApprove)
                        form.FshSubmitSts = false;
                    strNotGroupName = process.IsApprove ? GroupNames.OperRegionManager : GroupNames.OperationsExecutive;
                    form.FshStatus = process.IsApprove ? StatusList.FormFSHeadMaintenanceApproved : StatusList.FormFSInit;
                    strTitle = StatusList.FormFSHeadMaintenanceApproved;
                }
                else if (form.FshStatus == StatusList.FormFSHeadMaintenanceApproved)
                {
                    strNotGroupName = process.IsApprove ? GroupNames.JKRSSuperiorOfficerSO : GroupNames.OpeHeadMaintenance;
                    form.FshStatus = process.IsApprove ? StatusList.FormFSRegionManagerApproved : StatusList.FormFSSummarized;
                    strTitle = StatusList.FormFSRegionManagerApproved;
                }
                else if (form.FshStatus == StatusList.FormFSRegionManagerApproved)
                {
                    strNotGroupName = process.IsApprove ? "" : GroupNames.OperRegionManager;
                    form.FshStatus = process.IsApprove ? StatusList.Completed : StatusList.FormFSHeadMaintenanceApproved;
                    strTitle = "Completed";
                    form.FshUserIdChckdBy = !process.IsApprove ? new int?() : Utility.ToNullInt((object)process.UserID);
                    form.FshUserNameChckdBy = !process.IsApprove ? (string)null : process.UserName;
                    form.FshUserDesignationChckdBy = !process.IsApprove ? (string)null : process.UserDesignation;
                    form.FshDtChckdBy = !process.IsApprove ? new DateTime?() : process.ApproveDate;
                    if (process.IsApprove)
                    {
                        List<int> lstNotUserId = new List<int>();

                        if (form.FshUserIdInspBy.HasValue)
                            lstNotUserId.Add(form.FshUserIdInspBy.Value);

                        if (form.FshUserIdChckdBy.HasValue)
                            lstNotUserId.Add(form.FshUserIdChckdBy.Value);

                        if (form.FshUserIdSmzdBy.HasValue)
                            lstNotUserId.Add(form.FshUserIdSmzdBy.Value);

                        strNotUserID = string.Join<int>(",", lstNotUserId.Distinct<int>());
                    }
                }
                form.FshAuditLog = Utility.ProcessLog(form.FshAuditLog, strTitle, process.IsApprove ? "Approved" : "Rejected", process.UserName, process.Remarks, process.ApproveDate, security.UserName);
                string strNotMsg = (process.IsApprove ? "Approved - " : "Rejected - ") + strTitle + ":" + process.UserName + " - Form FS (" + form.FshRoadName + ")";
                string strNotURL = "/FormFS/Add?id=" + form.FshPkRefNo.ToString() + "&isview=true";

                SaveNotification(new RmUserNotification()
                {
                    RmNotCrBy = security.UserName,
                    RmNotGroup = strNotGroupName,
                    RmNotMessage = strNotMsg,
                    RmNotOn = DateTime.Now,
                    RmNotUrl = strNotURL,
                    RmNotUserId = strNotUserID,
                    RmNotViewed = ""
                }, false);
            }
            return await context.SaveChangesAsync();
        }
    }
}
