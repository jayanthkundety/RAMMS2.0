using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RAMMS.Domain.Models;
using RAMMS.Business.ServiceProvider;
using RAMMS.DTO.RequestBO;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.DTO.ResponseBO;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using RAMMS.WebAPI.Model;
using RAMMS.Common;
using System.IO;

namespace RAMMS.WebAPI
{
    public class LoginController : Controller
    {
        private readonly IUserBO _userBO;
        private readonly IUserService _userService;
        private readonly ISecurity security;
        private readonly IConfiguration configuration;
        public LoginController(IUserBO userBO, IUserService userService, ISecurity iSecurity, IConfiguration config)
        {
            _userBO = userBO;
            _userService = userService;
            security = iSecurity;
            configuration = config;
        }

        [Route("api/login")]
        [HttpPost]
        public IActionResult UserLoginAccount([FromBody] object UserObj)
        {
            try
            {

                RmUserCredential request = JsonConvert.DeserializeObject<RmUserCredential>(UserObj.ToString());
                RmUsers user = _userBO.UserLogin(request);
                return RAMMSApiSuccessResponse(user);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }

        }
        [Route("api/signin")]
        [HttpPost]
        public IActionResult SignIn([FromBody] object user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    RmUserCredential rmUser = JsonConvert.DeserializeObject<RmUserCredential>(user.ToString());
                    RmUsers _user = _userBO.UserLogin(rmUser);
                    if (_user != null)
                    {
                        var Token = security.DeviceSignInRegister(configuration, _user);
                        return RAMMSApiSuccessResponse(Token);
                    }
                    else
                    {
                        return Unauthorized("Invalid User Credentials");
                    }
                }
                else
                {
                    return new CBadRequest("Invalid Request user object");
                }

            }
            catch (Exception ex)
            {
                return new CBadRequest(ex.Message);
            }

        }

        [Route("api/user")]
        [HttpPost]
        public async Task<IActionResult> UserDtl([FromBody] object UserObj)
        {
            try
            {

                UserRequestDTO request = JsonConvert.DeserializeObject<UserRequestDTO>(UserObj.ToString());
                UserResponseDTO user = await _userService.GetUserNameByCode(request);
                return RAMMSApiSuccessResponse(user);
            }
            catch (Exception ex)
            {
                return this.RAMMSApiErrorResponse(ex.Message);
            }

        }


        [Authorize]
        [Route("api/resetPassword")]
        [HttpPost]
        public IActionResult SaveResetPassword(ResetPassword reset)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return UpdateResetPassword(security.UserID, reset.OldPassword, reset.NewPassword);
                }
                else
                {
                    return RAMMSApiSuccessResponse(new Common.Result<object>() { IsSuccess = false, Message = "Invalid user object to reset the password" });
                }
            }
            catch (Exception ex)
            {
                return RAMMSApiErrorResponse( ex.Message );
            }
        }

        [Route("api/updatePassword")]
        [HttpPost]
        private IActionResult UpdateResetPassword(int userId, string oldPassword, string newPassword)
        {
            var obj = _userBO.UpdatePassword(userId, oldPassword, newPassword);
            if (obj != null)
            {
                var response = new Common.Result<object>() { IsSuccess = true, Message = "Password update sucessfully! Now you can login with your new password" };
                return RAMMSApiSuccessResponse(response);
            }
            else
            {
                var response = new Common.Result<object>() { IsSuccess = false, Message = "Invalid user details to reset the password" };
                return RAMMSApiSuccessResponse(response);
            }
        }

        [Route("api/forgetPassword")]
        [HttpPost]
        public async Task<IActionResult> GetPasswordReset(string path, string email)
        {
            string hostedURL = Path.Combine(path + @"/SignIn/ResetPassword");
            int rowsAffected = await _userService.GetPasswordReset(email, hostedURL);
            return RAMMSApiSuccessResponse(rowsAffected);
        }

        [Route("api/resetTempPassword")]
        [HttpPost]
        public async Task<IActionResult> ResetTempPassword(ResetPassword reset)
        {
            if (ModelState.IsValid)
            {
                UserRequestDTO userRequestDTO = new UserRequestDTO();
                userRequestDTO.UsrUserId = Cryptography.DecryptData(reset.USRUserId);
                var userResponse = await _userService.GetUserByUsrUserId(userRequestDTO);
                return UpdateResetPassword(userResponse.UserId, reset.OldPassword, reset.NewPassword);
            }
            else
            {
               return RAMMSApiErrorResponse("Invalid user object to reset the password");
            }
        }

    }
}