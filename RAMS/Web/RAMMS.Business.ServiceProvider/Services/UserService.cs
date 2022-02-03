using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Common;
using RAMMS.Domain;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RAMMS.Business.ServiceProvider.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryUnit _repoUnit;
        private readonly IMapper _mapper;
        private readonly ISecurity _security;
        public UserService(IRepositoryUnit repoUnit, IMapper mapper, ISecurity security)
        {
            _repoUnit = repoUnit ?? throw new ArgumentNullException(nameof(repoUnit));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _security = security;
        }
        public async Task<object> GetUserNotification(int topCount, DateTime? from)
        {
            var query = _repoUnit._context.RmUserNotification.Where(x => _security.Groups.Contains(x.RmNotGroup) || ("," + (x.RmNotUserId ?? "") + ",").Contains("," + _security.UserID + ","));
            int iNotifyCount = await query.Where(x => string.IsNullOrEmpty(x.RmNotViewed) || ("," + x.RmNotViewed + ",").Contains("," + _security.UserID + ",") == false).CountAsync();
            var oquery = query.Select(x => new { Dt = x.RmNotOn, Msg = x.RmNotMessage, URL = x.RmNotUrl, IsViewed = ("," + (x.RmNotViewed ?? "") + ",").Contains("," + _security.UserID + ",") }).OrderByDescending(x => x.Dt);
            object obj = null;
            if (from.HasValue) { obj = await oquery.Where(x => x.Dt > from).ToListAsync(); }
            else { obj = await oquery.Take(topCount).ToListAsync(); }

            return new { NotifyCount = iNotifyCount, NotifyList = obj };
        }
        public async Task<List<UserResponseDTO>> GetUserNameRespectToPosition(UserRequestDTO userRequestDTO)
        {
            List<UserResponseDTO> user = new List<UserResponseDTO>();
            try
            {
                var users = await _repoUnit.UserRepository.GetUserWithPosition(userRequestDTO).ConfigureAwait(false);

                foreach (var listuser in users)
                {
                    user.Add(_mapper.Map<UserResponseDTO>(listuser));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }

        public async Task<UserResponseDTO> GetUserNameByCode(UserRequestDTO userRequestDTO)
        {
            UserResponseDTO user;
            try
            {
                var users = await _repoUnit.UserRepository.GetUserByCode(userRequestDTO).ConfigureAwait(false);
                user = _mapper.Map<UserResponseDTO>(users);
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<SelectListItem> GetUserSelectList(int? userId = null)
        {
            /*List<SelectListItem> lstItems = null;
            if (userId.HasValue && userId.Value > 0)
                lstItems = _repoUnit.UserRepository.FindAsync<SelectListItem>(x => x.UsrPkId == userId.Value, x => new SelectListItem() { Value = Convert.ToString(x.UsrPkId), Text = Convert.ToString(x.UsrPkId) + "-" + x.UsrUserName }).Result.ToList();
            else
                lstItems = new List<SelectListItem>();
            lstItems.Add(new SelectListItem() { Value = _security.UserID.ToString(), Text = _security.UserID.ToString() + "-" + _security.UserName });
            return lstItems.Distinct();*/
            return GetUserList().Result;
        }
        public async Task<IEnumerable<SelectListItem>> GetUserList()
        {
            var userList = new List<SelectListItem>();
            try
            {
                var users = await _repoUnit.UserRepository.GetUsers();

                return users.OrderBy(s => s.UsrPkId).Select(s => new SelectListItem
                {
                    Value = s.UsrPkId.ToString(),
                    Text = s.UsrPkId + "-" + s.UsrUserName.ToString()
                }).ToArray();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        public async Task<IEnumerable<CSelectListItem>> GetUser()
        {
            //var result = new List<SelectListItem>();
            try
            {
                return await _repoUnit.UserRepository.FindAsync(null, x => new CSelectListItem() { Text = x.UsrPkId + "-" + x.UsrUserName, Value = x.UsrPkId.ToString(), CValue = x.UsrPkId.ToString(), Item1 = x.UsrUserName, Item2 = x.UsrPosition });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<CSelectListItem>> GetGroupList(int userId)
        {
            //var result = new List<SelectListItem>();
            try
            {
                var _context = _repoUnit.UserRepository._context;
                var result = await (from hd in _context.RmGroup
                                    select new CSelectListItem
                                    {
                                        Text = hd.UgGroupName,
                                        Value = hd.UgPkId.ToString(),
                                        CValue = hd.UgGroupCode
                                    }).OrderBy(x => x.Text).ToListAsync();
                if (userId > 0)
                {
                    var grpuser = await _context.RmGroupUser.Where(x => x.RmUsersUsrPkId == userId).Select(x => x.RmGroupsUgPkId.ToString()).ToListAsync();
                    result.Where(x => grpuser.Contains(x.Value)).ToList().ForEach((val) => { val.Selected = true; });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<int> SaveGroup(int GroupId, string GroupName, string GroupCode, string createdBy)
        {
            var _context = _repoUnit.UserRepository._context;
            RmGroup newRmGroup = null;
            if (GroupId > 0)
            {
                var existObj = _context.RmGroup.Where(x => x.UgPkId == GroupId).FirstOrDefault();
                if (existObj == null) { throw new Exception("Invalid object / Group Name to update."); }
                else
                {
                    if (_context.RmGroup.Where(x => x.UgPkId != GroupId && x.UgGroupCode == GroupCode).Count() > 0)
                    {
                        throw new Exception("Group Code is already exists");
                    }
                    else if (_context.RmGroup.Where(x => x.UgPkId != GroupId && x.UgGroupName == GroupName).Count() > 0)
                    {
                        throw new Exception("Group Name is already exists");
                    }
                    else
                    {
                        existObj.UgGroupCode = GroupCode;
                        existObj.UgGroupName = GroupName;
                        existObj.UgModDt = DateTime.Now;
                        existObj.UgModifiedBy = createdBy;
                    }
                }
            }
            else
            {
                if (_context.RmGroup.Where(x => x.UgGroupCode == GroupCode).Count() > 0)
                {
                    throw new Exception("Group Code is already exists");
                }
                else if (_context.RmGroup.Where(x => x.UgGroupName == GroupName).Count() > 0)
                {
                    throw new Exception("Group Name is already exists");
                }
                else
                {
                    newRmGroup = new RmGroup()
                    {
                        UgGroupCode = GroupCode,
                        UgGroupName = GroupName,
                        UgCrBy = createdBy,
                        UgCrDt = DateTime.Now,
                        UgModDt = DateTime.Now,
                        UgModifiedBy = createdBy
                    };
                    _context.RmGroup.Add(newRmGroup);
                }
            }
            await _context.SaveChangesAsync();
            return GroupId > 0 ? GroupId : newRmGroup.UgPkId;
        }
        public async Task<IEnumerable<CSelectListItem>> GetModuleList()
        {
            //var result = new List<SelectListItem>();
            try
            {
                var _context = _repoUnit.UserRepository._context;
                var result = await (from hd in _context.RmModule
                                    select new CSelectListItem
                                    {
                                        Text = hd.ModDescription,
                                        Value = hd.ModPkId.ToString(),
                                        CValue = hd.ModName
                                    }).OrderBy(x => x.Text).ToListAsync();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<List<UserModuleRightsDTO>> GetModuleRightsList(int groupId)
        {
            try
            {
                var _context = _repoUnit.UserRepository._context;
                return await _context.RmModuleGroupRights.Where(x => x.UgPkId == groupId).Select(x => new UserModuleRightsDTO()
                {
                    DIsAdd = x.DvIsAdd,
                    DIsDelete = x.DvIsDelete,
                    DIsModify = x.DvIsModify,
                    DIsView = x.DvIsView,
                    ModPkId = x.ModPkId,
                    PIsAdd = x.PcIsAdd,
                    PIsDelete = x.PcIsDelete,
                    PIsModify = x.PcIsModify,
                    PIsView = x.PcIsView,
                    PkId = x.MgrPkId,
                    GroupPkId = x.UgPkId
                }).ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveModuleRightsList(int groupId, List<UserModuleRightsDTO> rights, string createdBy)
        {
            try
            {
                var _context = _repoUnit.UserRepository._context;
                var list = await _context.RmModuleGroupRights.Where(x => x.UgPkId == groupId).ToListAsync();
                var modIds = rights.Where(x => x.PkId > 0).Select(x => x.PkId).ToList();
                var delete = list.Where(x => !modIds.Contains(x.MgrPkId)).ToList();
                delete.ForEach((mod) => { _context.RmModuleGroupRights.Remove(mod); });
                rights.ForEach((right) =>
                {
                    var exstobj = list.Where(x => x.ModPkId == right.ModPkId).FirstOrDefault();
                    if (exstobj != null)
                    {
                        exstobj.ModPkId = right.ModPkId;
                        exstobj.UgPkId = right.GroupPkId;
                        exstobj.MgrPkId = right.PkId;
                        exstobj.DvIsAdd = right.DIsAdd;
                        exstobj.DvIsDelete = right.DIsDelete;
                        exstobj.DvIsModify = right.DIsModify;
                        exstobj.DvIsView = right.DIsView;
                        exstobj.MgrModifiedBy = createdBy;
                        exstobj.MgrModifiedOn = DateTime.Now;
                        exstobj.PcIsAdd = right.PIsAdd;
                        exstobj.PcIsDelete = right.PIsDelete;
                        exstobj.PcIsModify = right.PIsModify;
                        exstobj.PcIsView = right.PIsView;
                    }
                    else
                    {
                        _context.RmModuleGroupRights.Add(new RmModuleGroupRights()
                        {
                            ModPkId = right.ModPkId,
                            UgPkId = right.GroupPkId,
                            DvIsAdd = right.DIsAdd,
                            DvIsDelete = right.DIsDelete,
                            DvIsModify = right.DIsModify,
                            DvIsView = right.DIsView,
                            MgrCreatedBy = createdBy,
                            MgrModifiedBy = createdBy,
                            MgrCreatedOn = DateTime.Now,
                            MgrModifiedOn = DateTime.Now,
                            PcIsAdd = right.PIsAdd,
                            PcIsDelete = right.PIsDelete,
                            PcIsModify = right.PIsModify,
                            PcIsView = right.PIsView
                        });
                    }
                });
                MemoryCache.Instance.ModuleRights = null;
                return await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<IEnumerable<CSelectListItem>> GetSupervisor(string groupCode, string gC)
        {
            //var result = new List<SelectListItem>();
            try
            {
                var _context = _repoUnit.UserRepository._context;
                var result = await (from hd in _context.RmGroup
                                    join h in _context.RmGroupUser on hd.UgPkId equals h.RmGroupsUgPkId
                                    join x in _context.RmUsers on h.RmUsersUsrPkId equals x.UsrPkId
                                    where hd.UgGroupCode == groupCode || hd.UgGroupCode == gC
                                    select new CSelectListItem
                                    {
                                        Text = x.UsrPkId + " - " + x.UsrUserName,
                                        Value = x.UsrPkId.ToString(),
                                        CValue = x.UsrPkId.ToString(),
                                        Item1 = x.UsrUserName,
                                        Item2 = x.UsrPosition
                                    }).ToListAsync();
                result.Add(new CSelectListItem { Text = "99999999 - Others", Value = "99999999", CValue = "99999999", Item1 = "others" });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<SelectListItem>> GetSupervisor()
        {
            var supervisor = new List<SelectListItem>();
            try
            {
                return await _repoUnit.UserRepository.getSupervisor();
            }

            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public RmUsers GetUser(Domain.Models.RmUserCredential userDetails)
        {
            RmUsers userEntry = null;
            try
            {
                userEntry = _repoUnit.UserRepository.GetUser(userDetails.UsrUserName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userEntry;
        }
        public async Task<UserResponseDTO> GetUserByUsrUserId(UserRequestDTO userRequestDTO)
        {
            UserResponseDTO user = new UserResponseDTO();
            var result = await _repoUnit.UserRepository.GetUserByUsrUserId(userRequestDTO);
            return user = _mapper.Map<UserResponseDTO>(result);
        }

        #region Forgot Password - Email User Temp Password
        public async Task<int> GetPasswordReset(string email, string hostedUrl)
        {
            int rowsAffected = 0;
            DataSet dsUserPassword = new DataSet();
            DataTable dt = new DataTable();
            bool isUsrEmailExist = await _repoUnit.UserRepository.CheckUserEmailExist(email);

            if (isUsrEmailExist)
            {
                // Generate Randome Password Alpha_Numeric_SpecialChar 
                var random = new Random();
                var rString = Enumerable.Range(0, 8).Select(n => (Char)(random.Next(33, 122)));
                var rPassword = new string(rString.ToArray());

                //URL shared with UsrUserId(Encrypted)
                var user = await _repoUnit.UserRepository.FindAsync(x => x.UsrEmail == email && x.UsrActiveYn == true);
                hostedUrl = hostedUrl + "?id=" + Cryptography.EncryptData(user.UsrUserid);

                Dictionary<string, object> param = new Dictionary<string, object>
                {
                       {"@usr_email", email },
                       {"@url", hostedUrl },
                       {"@Temp_Password",rPassword }
                };
                dsUserPassword = _repoUnit.UserRepository.GetDataSet("proc_reset_pswd", System.Data.CommandType.StoredProcedure, param);
                dt = dsUserPassword.Tables[0];
                string result = dt.Rows[0].Field<string>("Result");
                if (result == "Temporary Password will be sent to your Email address.")
                {
                    if (user != null)
                    {
                        user.UsrPassword = Cryptography.PasswordHashed(rPassword);
                        user.UsrForceRstPwd = true;
                        user.UsrPasswordExpiry = DateTime.UtcNow.AddDays(1);
                        user.UsrIsDisabled = false;
                        user.UsrRetryCount = 0;
                        _repoUnit.UserRepository.Update(user);
                        rowsAffected = await _repoUnit.CommitAsync();
                    }
                }
            }
            return rowsAffected;
        }

        public async Task<PagingResult<UserDetailRequestDTO>> GetDetailList(FilteredPagingDefinition<UserDetailRequestDTO> filterOptions)
        {
            PagingResult<UserDetailRequestDTO> result = new PagingResult<UserDetailRequestDTO>();
            result.PageResult = await _repoUnit.UserRepository.GetFilteredRecordList(filterOptions);
            result.TotalRecords = await _repoUnit.UserRepository.GetFilteredRecordCount(filterOptions);
            return result;
        }

        public long LastDetailInsertedNo()
        {
            var model = _repoUnit.UserRepository.GetAll().OrderByDescending(s => s.UsrPkId).FirstOrDefault(); if (model != null) { return model.UsrPkId; } else { return 0; }
        }
        public async Task<UserDetailRequestDTO> GetDetailById(int id)
        {
            var _context = _repoUnit.UserRepository._context;
            var model = await _context.RmUsers.Include(x => x.RmModuleGroupRights).Where(x => x.UsrPkId == id).FirstOrDefaultAsync();
            //var model = await _repoUnit.UserRepository.FindAsync(s => s.UsrPkId == id); 
            if (model == null) { return null; }
            UserDetailRequestDTO result = _mapper.Map<Domain.Models.RmUsers, UserDetailRequestDTO>(model);
            if (model.RmModuleGroupRights != null && model.RmModuleGroupRights.Count > 0)
            {
                result.ModuleRights = new List<UserModuleRightsDTO>();
                model.RmModuleGroupRights.ToList().ForEach((module) =>
                {
                    result.ModuleRights.Add(new UserModuleRightsDTO()
                    {
                        PkId = module.MgrPkId,
                        ModPkId = module.ModPkId,
                        UsrPkId = module.UsrPkId,
                        DIsAdd = module.DvIsAdd,
                        DIsDelete = module.DvIsDelete,
                        DIsModify = module.DvIsModify,
                        DIsView = module.DvIsView,
                        PIsAdd = module.PcIsAdd,
                        PIsDelete = module.PcIsDelete,
                        PIsModify = module.PcIsModify,
                        PIsView = module.PcIsView
                    });
                });
            }
            result.Password = "@Pass@12@345@";
            return result;
        }
        public async Task<int> SaveDetail(UserDetailRequestDTO model)
        {
            int rowsAffected; try
            {
                var form = _mapper.Map<Domain.Models.RmUsers>(model);
                if (model.GroupId != null && model.GroupId.Length > 0)
                {
                    form.RmGroupUser = new List<RmGroupUser>();
                    model.GroupId.ToList().ForEach((gid) =>
                    {
                        form.RmGroupUser.Add(new RmGroupUser() { RmGroupsUgPkId = gid });
                    });
                }
                if (model.ModuleRights != null && model.ModuleRights.Count > 0)
                {
                    form.RmModuleGroupRights = new List<RmModuleGroupRights>();
                    model.ModuleRights.ForEach((mrights) =>
                    {
                        form.RmModuleGroupRights.Add(new RmModuleGroupRights()
                        {
                            DvIsAdd = mrights.DIsAdd,
                            DvIsDelete = mrights.DIsDelete,
                            DvIsModify = mrights.DIsModify,
                            DvIsView = mrights.DIsView,
                            ModPkId = mrights.ModPkId,
                            PcIsAdd = mrights.PIsAdd,
                            PcIsDelete = mrights.PIsDelete,
                            PcIsModify = mrights.PIsModify,
                            PcIsView = mrights.PIsView,
                            MgrCreatedBy = model.ModBy,
                            MgrModifiedBy = model.ModBy,
                            MgrCreatedOn = model.ModDt,
                            MgrModifiedOn = model.ModDt
                        });
                    });
                }
                if (form.UsrPkId != 0)
                {
                    var _context = _repoUnit.UserRepository._context;
                    var existModel = await _context.RmUsers.Include(x => x.RmGroupUser).Include(x => x.RmModuleGroupRights).Where(x => x.UsrPkId == form.UsrPkId).FirstOrDefaultAsync();
                    if (existModel != null)
                    {
                        existModel.RmModuleGroupRights = form.RmModuleGroupRights;
                        existModel.RmGroupUser = form.RmGroupUser;
                        existModel.UsrCompanyName = form.UsrCompanyName;
                        existModel.UsrContactNo = form.UsrContactNo;
                        existModel.UsrModBy = form.UsrModBy;
                        existModel.UsrModDt = form.UsrModDt;
                        existModel.UsrPasswordExpiry = form.UsrPasswordExpiry;
                        existModel.UsrPosition = form.UsrPosition;
                        existModel.UsrUserid = form.UsrUserid;
                        if (form.UsrPassword != "@Pass@12@345@") { existModel.UsrPassword = Cryptography.PasswordHashed(form.UsrPassword); }
                        existModel.UsrUserName = form.UsrUserName;
                        existModel.UsrDepartment = form.UsrDepartment;
                        existModel.UsrEmail = form.UsrEmail;
                        existModel.UsrReportingUsrPkId = form.UsrReportingUsrPkId;
                        existModel.UsrIsDisabled = form.UsrIsDisabled;
                        existModel.UsrLockedUntil = form.UsrLockedUntil;
                    }
                    //_repoUnit.UserRepository.Update(form);
                }
                else
                {
                    var exist = _repoUnit.UserRepository.Find(s => s.UsrUserid == model.Userid);
                    if (exist == null)
                        _repoUnit.UserRepository.Create(form);
                    else
                    {
                        return -1;
                    }
                }
                await _repoUnit.CommitAsync();
                MemoryCache.Instance.ModuleRights = null;
                return form.UsrPkId;
            }
            catch (Exception ex) { await _repoUnit.RollbackAsync(); throw ex; }
            return rowsAffected;
        }
        public async Task<bool> RemoveDetail(int id)
        {
            var model = _repoUnit.UserRepository.Find(s => s.UsrPkId == id);
            if (model != null)
            {
                model.UsrActiveYn = false;
                return await _repoUnit.CommitAsync() != 0;
            }
            else { return false; }
        }

        public async Task<int> ChangePassword(UserDetailRequestDTO model)
        {
            var usr = _repoUnit.UserRepository.Find(s => s.UsrPkId == model.PkId);
            if (usr != null)
            {
                usr.UsrPassword = Cryptography.PasswordHashed(model.Password);
                return await _repoUnit.CommitAsync();
            }
            else
            {
                return -1;
            }
        }

        public List<RmDepartment> GetDepartments()
        {
            List<RmDepartment> lst = _repoUnit.UserRepository.GetDepartments();
            return lst;
        }

        public List<RmGroup> GetGroups(string department)
        {
            List<RmGroup> lst = _repoUnit.UserRepository.GetGroups(department);
            return lst;
        }

        public List<RmGroupUser> GetGroupUsers(int userid)
        {
            List<RmGroupUser> lst = _repoUnit.UserRepository.GetGroupUsers(userid);
            return lst;
        }

        public List<RmUserGroup> GetUserGroups()
        {
            List<RmUserGroup> lst = _repoUnit.UserRepository.GetUserGroups();
            return lst;
        }

        public async Task<int> ChangeGroup(UserDetailRequestDTO model)
        {
            return await _repoUnit.UserRepository.ChangeGroup(model);
        }



        #endregion
    }
}
