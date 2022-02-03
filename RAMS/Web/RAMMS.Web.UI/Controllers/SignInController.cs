using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RAMMS.Domain.Models;
using RAMMS.Business.ServiceProvider;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using RAMMS.Web.UI.Models;
using RAMMS.Business.ServiceProvider.Interfaces;
using Microsoft.AspNetCore.Hosting;
using RAMMS.DTO.RequestBO;
using RAMMS.Common;

namespace RAMMS.Web.UI.Controllers
{
    public class SignInController : Controller
    {
        private readonly IUserBO _userBO;
        private readonly ISecurity security;
        private readonly IUserService _userService;
        public SignInController(IUserBO userBO, ISecurity iSecurity, IUserService userService)
        {
            _userBO = userBO;
            security = iSecurity;
            _userService = userService;
        }
        public async Task<IActionResult> Index(string id)
        {
            if (id == "logout")
            {
                security.WebSignOut();
            }
            else if (security.IsLogin)
            {

                UserRequestDTO userRequestDTO = new UserRequestDTO();
                userRequestDTO.UserId = security.UserID;
                var user = await _userService.GetUserNameByCode(userRequestDTO);
                if (user.ForceResetPwd == true) return RedirectToAction("ResetPassword", "SignIn", new { id = Cryptography.EncryptData(user.UserId.ToString()) });
                else return RedirectToAction("Index", "Home");
            }
            return View(new RmUserCredential());
        }

        [HttpPost]
        public ActionResult Index(RmUserCredential _user, [FromQuery(Name = "ReturnUrl")] string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                RmUsers user = _userBO.UserLogin(_user);
                //user.

                if (user != null)
                {
                    if (!user.UsrIsDisabled)
                    {
                        security.WebSignInRegister(user);
                        if (!string.IsNullOrEmpty(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        if (user.UsrForceRstPwd == true)
                        {
                            if (user.UsrPasswordExpiry == null || (user.UsrPasswordExpiry != null && DateTime.UtcNow <= user.UsrPasswordExpiry))
                            {
                                return RedirectToAction("ResetPassword", "SignIn", new { id = Cryptography.EncryptData(user.UsrPkId.ToString()) });
                            }
                            else if (DateTime.UtcNow > user.UsrPasswordExpiry)
                            {

                                return RedirectToAction("ForgotPassword", "SignIn", new { id = "1" });
                            }
                        }
                        else return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Your account has been locked due to multiple failed login attempts";
                    }

                }
                else
                    ViewBag.ErrorMessage = "Invalid User Credentials";
            }
            else
            {

            }
            //DA da = new DA();
            //User m = da.User.Authenticate(Username, Password);
            //if (ValidateData(m, Username))
            //{
            //    int res = 0;
            //    if (int.TryParse(Password, out res) && Password.Length <= 6)
            //        return RedirectToAction("Index", "ChangePasswordGeneral", new { area = "Admin", username = Username, change = "true" });
            //    if (Password == Username)
            //        return RedirectToAction("Index", "ChangePasswordGeneral", new { area = "Admin", username = Username, change = "true" });
            //    if (m.PasswordExpiry.ToLocalTime() <= DateTime.Now)
            //        return RedirectToAction("Index", "ChangePasswordGeneral", new { area = "Admin", username = Username, expire = "true" });

            //    List<int> AccessRights = Helper.GetUserPermissionList(m.PK);
            //    List<int> SectionRights = Helper.GetUserSectionList(m.PK);

            //    Session_Create(m, Enumeration.UserType.Administrator, AccessRights, SectionRights);
            //    LoggedInUser l = Session_Get(Enumeration.UserType.Administrator);
            //    ActivityLog aLog = CreateLog("Signed In.", l);
            //    m.LockedUntil = null;
            //    m.RetryCount = 0;
            //    m.LastLoginDate = DateTime.Now;
            //    da.User.Save(m, null, aLog);
            //    aLog.Save();

            //    if (Request["returnurl"] != null && Request["returnurl"].Trim() != "")
            //    {
            //        return Redirect(Request["returnurl"]);
            //    }
            //    else
            //    {
            //        if (Password == Username)
            //            return RedirectToAction("Index", "ChangePassword", new { area = "Admin", doreset = "true" });
            //        else
            //            return RedirectToAction("Index", "Default", new { area = "Admin" });
            //    }
            //}
            //else
            //{
            //    ViewBag.Username = Username;
            return View(_user);
            //}
        }

        #region Reset Password page Redirection & Method to reset
        public IActionResult ResetPassword(string id)
        {
            ViewBag.ResetForm = true;
            ViewBag.UserDetail = id;
            return View("~/Views/SignIn/ForgotPassword.cshtml");
        }

        public async Task<JsonResult> ResetTempPassword(ResetPassword reset)
        {
            if (ModelState.IsValid)
            {
                UserRequestDTO userRequestDTO = new UserRequestDTO();
                userRequestDTO.UserId = Utility.ToInt(Cryptography.DecryptData(reset.USRUserId));
                //var userResponse = await _userService.GetUserByUsrUserId(userRequestDTO);
                var userResponse = await _userService.GetUserNameByCode(userRequestDTO);
                return UpdateResetPassword(userResponse.UserId, reset.OldPassword, reset.NewPassword);
            }
            else
            {
                return Json(new Common.Result<object>() { IsSuccess = false, Message = "Invalid user object to reset the password" });
            }
        }
        #endregion

        #region Forgot Password Page redirection & Temp Password sharing to registered mail method

        public IActionResult ForgotPassword(string id)
        {
            if (id == "1")
            {
                ViewBag.ErrorMessage = "Password shared through email is expired. Please regenerate a new password by entering your registered email.";
            }
            ViewBag.ResetForm = false;
            return View();
        }

        // Temp Password sharing to registered mail method
        [HttpPost]
        public async Task<IActionResult> GetPasswordReset(string email)
        {
            string hostedURL = Request.Scheme.ToString() + "://" + Request.Host.ToString() + @"/SignIn/ResetPassword";
            int rowsAffected = await _userService.GetPasswordReset(email, hostedURL);
            return Json(rowsAffected);
        }

        #endregion

        [CAuthorize]
        [HttpPost]
        public JsonResult SaveResetPassword(ResetPassword reset)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return UpdateResetPassword(security.UserID, reset.OldPassword, reset.NewPassword);
                }
                else
                {
                    return Json(new Common.Result<object>() { IsSuccess = false, Message = "Invalid user object to reset the password" });
                }
            }
            catch (Exception ex)
            {
                return Json(new Common.Result<object>() { IsSuccess = false, Message = ex.Message });
            }
        }

        private JsonResult UpdateResetPassword(int userId, string oldPassword, string newPassword)
        {
            var obj = _userBO.UpdatePassword(userId, oldPassword, newPassword);
            if (obj != null)
            {
                return Json(new Common.Result<object>() { IsSuccess = true, Message = "Password update sucessfully! Now you can login with your new password" }); ; ;
            }
            else
            {
                return Json(new Common.Result<object>() { IsSuccess = false, Message = "Invalid user details to reset the password" });
            }
        }


        //[HttpPost]
        //public async Task<int> GetPasswordResetTab(string email)
        //{
        //    string hostedURL = Request.Scheme.ToString() + "://" + Request.Host.ToString() + @"/SignIn/ResetPassword";
        //    int rowsAffected = await _userService.GetPasswordReset(email, hostedURL);
        //    return rowsAffected;
        //}

        //private bool ValidateData(User m, string Username)
        //{
        //    bool hasError = false;

        //    SystemMessage sm = new SystemMessage();
        //    sm.SetMessageType(Enumeration.MessageType.Error);

        //    if (m == null)
        //    {
        //        DA da = new DA();
        //        m = da.User.Get(Username);
        //        if (m != null)
        //        {
        //            if (m.LockedUntil.HasValue && DateTime.Now < m.LockedUntil.Value.ToLocalTime())
        //            {
        //                sm.AddSubMessage("Your account has been deactivated until " + m.LockedUntil.Value.ToLocalTime().ToString(Variable.Format_DateTime) + ". You may contact your administrator for further assistance.");
        //            }
        //            else
        //            {
        //                m.RetryCount += 1;

        //                if (m.RetryCount == Variable.Setting_SignInMaxRetryCount)
        //                {
        //                    m.RetryCount = 0;
        //                    m.LockedUntil = DateTime.Now.AddMinutes(Variable.Setting_SignInLockMinutes);
        //                    sm.AddSubMessage("Incorrect Password. Your account has been deactivated until " + m.LockedUntil.Value.ToLocalTime().ToString(Variable.Format_DateTime) + ". You may contact your administrator for further assistance.");
        //                }
        //                else
        //                {
        //                    sm.AddSubMessage("Incorrect Password. Retries left: " + (Variable.Setting_SignInMaxRetryCount - m.RetryCount).ToString());
        //                }
        //                ActivityLog aLog = CreateLog("Incorrect Sign in", Username);
        //                da.User.Save(m, null, aLog);
        //                aLog.Save();
        //            }
        //        }
        //        else
        //        {
        //            sm.AddSubMessage("Invalid Sign in credentials.");
        //        }
        //        hasError = true;
        //    }
        //    else
        //    {
        //        if (m.IsDisabled)
        //        {
        //            sm.AddSubMessage("Your account has been locked. Please contact your administrator.");
        //            hasError = true;
        //        }
        //        else if (m.LockedUntil.HasValue && DateTime.Now < m.LockedUntil.Value.ToLocalTime())
        //        {
        //            sm.AddSubMessage("Your account has been deactivated for too many retries. Please wait for another " + TimeSpan.FromTicks(m.LockedUntil.Value.ToLocalTime().Ticks - DateTime.Now.Ticks).Minutes.ToString() + " minutes or contact your administrator.");
        //            hasError = true;
        //        }
        //        else if (m.ContractorPK.HasValue && m.ContractorPK.Value > 0)
        //        {
        //            sm.AddSubMessage("Sign in credentials is not a valid admin account. Please contact your administrator.");
        //            hasError = true;
        //        }
        //    }

        //    if (hasError)
        //    {
        //        sm.SetPrimaryMessage("Opps!");
        //        ViewBag.ErrorMessage = sm.Message;
        //        return false;
        //    }
        //    return true;
        //}       
    }
}
