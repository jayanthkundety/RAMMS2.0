using System;
using System.Collections.Generic;
using System.Text;
using RAMMS.Common;
//using RAMMS.Common.ServiceProvider;
using RAMMS.Domain.Models;
using System.Threading.Tasks;
using RAMS.Repository;
using Microsoft.Extensions.Logging;
using RAMMS.Repository.Interfaces;
using RAMMS.Business.ServiceProvider.Interfaces;

namespace RAMMS.Business.ServiceProvider
{
    public interface IUserBO
    {
        RmUsers UserLogin(RmUsers _user);
        RmUsers UserLogin(RmUserCredential _user);
        RmUsers UpdatePassword(int userId, string oldPassword, string newPassword);
        //User GetUser();
    }
    public class UserBO : IUserBO
    {
       //private readonly IUserProvider _userProv;
        private readonly IUserRepository userRepo;
        private readonly IUserService _userService;
        //private readonly ILogger _log;

        public UserBO(IUserRepository userrepo, IUserService userService)
        {
            //_userProv = userProv;
            userRepo = userrepo;
            _userService = userService;
        }
        public RmUsers UpdatePassword(int userId, string oldPassword, string newPassword)
        {
            RmUsers user = userRepo.GetByIdAsync(userId).Result;
            if (user != null)
            {
                if (user.UsrPassword == Cryptography.PasswordHashed(oldPassword))
                {
                    user.UsrForceRstPwd = false;
                    user.UsrPasswordExpiry = new DateTime(2099,12,31);
                    user.UsrPassword = Cryptography.PasswordHashed(newPassword);
                    userRepo.SaveUser(user);
                }
                else
                    user = null;
            }
            return user;
        }
        public RmUsers UserLogin(RmUsers _user)
        {
            //string DecryptedPwd = string.Empty;

            //RmUsers user = new RmUsers();
            //try
            //{
            //    user = _userProv.GetUser(_user);

            //    if (user != null)
            //    {
            //        DecryptedPwd = Cryptography.Decrypt(user.UsrPassword);

            //        if (_user.UsrPassword == DecryptedPwd)
            //            return user;
            //        else
            //            user = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //return user;
            RmUserCredential user = new RmUserCredential() { UsrUserName = _user.UsrUserName, UsrPassword = _user.UsrPassword };
            return UserLogin(user);
        }

        public RmUsers UserLogin(RmUserCredential _user)
        {
            string DecryptedPwd = string.Empty;
              RmUsers user = null; new RmUsers();
            try
            {
                user = _userService.GetUser(_user);

                /*if (user != null)
                {
                    DecryptedPwd = Cryptography.Decrypt(user.UsrPassword);
                    if (_user.UsrPassword == DecryptedPwd)
                        return user;
                    else
                        user = null;
                }*/

                if (user != null)
                {
                    if (user.UsrPassword == Cryptography.PasswordHashed(_user.UsrPassword))
                    {
                         if(!user.UsrIsDisabled==true)
                         {
                            if(user.UsrRetryCount>0)
                            {
                                user.UsrRetryCount = 0;
                                user.UsrIsDisabled = false;
                                userRepo.SaveUser(user);
                            }
                            
                            return user;
                         }
                        else
                        {
                            user = null;
                        }
                        
                    }
                    
                    else
                    {
                        if (user.UsrRetryCount<3)
                        {
                            user.UsrRetryCount = (short)(user.UsrRetryCount + 1);
                            userRepo.SaveUser(user);
                            user = null;
                        }
                        else
                        {
                            user.UsrIsDisabled = true;
                            userRepo.SaveUser(user);
                        }
                    }
                       // user = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        //public Task<User> GetUser()
        //{
        //     Task<User> usr = _userProv.GetUser();
        //    return usr;
        //}
    }
}
