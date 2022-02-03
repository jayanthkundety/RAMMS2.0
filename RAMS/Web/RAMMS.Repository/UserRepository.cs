using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RAMMS.Domain;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Repository;
using RAMMS.Repository.Interfaces;
using RAMS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAMS.Repository
{
    //public interface IUserRepository : IRepositoryBase<RmUsers>
    //{
    //    //int SaveUser(RmUsers user);
    //    //RmUsers GetUser(RmUsers userDetails);
    //    //Task<IEnumerable<RmUsers>> GetUsers();
    //    //Task<List<RmUsers>> GetUserWithPosition(UserRequestDTO userRequestDTO);
    //    //Task<List<RmUsers>> GetUserByCode(UserRequestDTO userRequestDTO);
    //}
    public class UserRepository : RepositoryBase<RmUsers>, IUserRepository
    {
        public UserRepository(RAMMSContext context) : base(context)
        {
            _context = context;
        }
        public RmUsers GetUser(RmUsers userDetails)
        {
            //var usrInst = new RmUsers();
            //var userInst = _context.Set<RmUsers>().AsNoTracking();
            //.Where(x => x.Username == userDetails.Username && x.Password == userDetails.Password);
            RmUsers user = _context.RmUsers.Where(x => x.UsrUserName == userDetails.UsrUserName).FirstOrDefault();

            //if (userInst != null && userInst.Count() > 0)
            //{
            //usrInst = user;
            //}
            return user;
        }
        public async Task<RmUsers> GetUserByUsrUserId(UserRequestDTO user)
        {
            return await _context.RmUsers.Where(x => x.UsrUserid == user.UsrUserId && x.UsrActiveYn == true).FirstOrDefaultAsync();
        }
        public RmUsers GetUser(string userId)
        {
            return _context.RmUsers.Include(x => x.RmGroupUser).ThenInclude(x => x.RmGroupsUgPk).Where(x => x.UsrUserid == userId).FirstOrDefault();
        }
        public int SaveUser(RmUsers user)
        {
            try
            {
                if (user.UsrPkId > 0)
                {
                    _context.RmUsers.Update(user);
                }
                else
                {
                    _context.RmUsers.Add(user);
                }
                _context.SaveChanges();

                //return Task.FromResult(user.Pk);
                return 0;
            }
            catch (Exception)
            {
                return 500;

            }
        }

        public async Task<IEnumerable<RmUsers>> GetUsers()
        {
            ////List<RmUsers> userList = null;
            ////////await _context.ExecuteStoredProc(StoreProcedures.Get_User_List)
            ////////           .WithSqlParam("@Usr_Active_YN", true)
            ////////           .ExecuteCommandStoredProcAsync((handler) =>
            ////////           {
            ////////               userList = handler.ReadToListAsync<RmUsers>().Result.ToList();
            ////////           });
            ////return userList;

            return await _context.RmUsers.Where(x => x.UsrActiveYn == true).ToListAsync();
        }

        public async Task<List<RmUsers>> GetUserWithPosition(UserRequestDTO userRequestDTO)
        {
            return await _context.RmUsers.Where(x => x.UsrPosition == userRequestDTO.Position).ToListAsync();
        }

        public async Task<RmUsers> GetUserByCode(UserRequestDTO userRequestDTO)
        {
            return await _context.RmUsers.Where(x => x.UsrPkId == userRequestDTO.UserId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<SelectListItem>> getSupervisor()
        {
            var result = await (from hd in _context.RmGroup
                                join h in _context.RmGroupUser on hd.UgPkId equals h.RmGroupsUgPkId
                                join d in _context.RmUsers on h.RmUsersUsrPkId equals d.UsrPkId
                                where hd.UgGroupName == "Operations (Supervisor)"
                                select new SelectListItem
                                {
                                    Text = d.UsrPkId + " - " + d.UsrUserName,
                                    Value = d.UsrPkId.ToString()
                                }).ToListAsync();
            var other = new SelectListItem { Text = "99999999-Others", Value = "99999999" };
            result.Add(other);
            return result;
        }

        public async Task<bool> CheckUserEmailExist(string email)
        {
            return await _context.RmUsers.AnyAsync(x => x.UsrEmail == email && x.UsrActiveYn == true);
        }

        public async Task<long> GetFilteredRecordCount(FilteredPagingDefinition<UserDetailRequestDTO> filterOptions)
        {
            return await (from s in _context.RmUsers
                          where s.UsrActiveYn == true
                           && (
                         s.UsrCompanyName.Contains(filterOptions.Filters.SmartSearch) ||
                         s.UsrContactNo.Contains(filterOptions.Filters.SmartSearch) ||
                         s.UsrDepartment.Contains(filterOptions.Filters.SmartSearch) ||
                         s.UsrEmail.Contains(filterOptions.Filters.SmartSearch) ||
                         (s.UsrIsDisabled ? "Yes" : "No").Contains(filterOptions.Filters.SmartSearch) ||
                         s.UsrPosition.Contains(filterOptions.Filters.SmartSearch) ||
                         s.UsrUserid.Contains(filterOptions.Filters.SmartSearch) ||
                         s.UsrUserName.Contains(filterOptions.Filters.SmartSearch))
                          select s).LongCountAsync();
        }
        public async Task<List<UserDetailRequestDTO>> GetFilteredRecordList(FilteredPagingDefinition<UserDetailRequestDTO> filterOptions)
        {
            var query = (from s in _context.RmUsers
                         where s.UsrActiveYn == true
                         && (
                         s.UsrCompanyName.Contains(filterOptions.Filters.SmartSearch) ||
                         s.UsrContactNo.Contains(filterOptions.Filters.SmartSearch) ||
                         s.UsrDepartment.Contains(filterOptions.Filters.SmartSearch) ||
                         s.UsrEmail.Contains(filterOptions.Filters.SmartSearch) ||
                         (s.UsrIsDisabled ? "Yes" : "No").Contains(filterOptions.Filters.SmartSearch) ||
                         s.UsrPosition.Contains(filterOptions.Filters.SmartSearch) ||
                         s.UsrUserid.Contains(filterOptions.Filters.SmartSearch) ||
                         s.UsrUserName.Contains(filterOptions.Filters.SmartSearch))
                         select s);

            if (filterOptions.sortOrder == SortOrder.Ascending)
            {
                if (filterOptions.ColumnIndex == 0) { query = query.OrderByDescending(s => s.UsrPkId); }
                if (filterOptions.ColumnIndex == 1) { query = query.OrderBy(s => s.UsrUserid); }
                if (filterOptions.ColumnIndex == 2) { query = query.OrderBy(s => s.UsrUserName); }
                if (filterOptions.ColumnIndex == 3) { query = query.OrderBy(s => s.UsrEmail); }
                if (filterOptions.ColumnIndex == 4) { query = query.OrderBy(s => s.UsrCompanyName); }
                if (filterOptions.ColumnIndex == 5) { query = query.OrderBy(s => s.UsrContactNo); }
                if (filterOptions.ColumnIndex == 6) { query = query.OrderBy(s => s.UsrPosition); }
                if (filterOptions.ColumnIndex == 7) { query = query.OrderBy(s => s.UsrDepartment); }
                if (filterOptions.ColumnIndex == 8) { query = query.OrderBy(s => s.UsrEmail); }
                if (filterOptions.ColumnIndex == 9) { query = query.OrderBy(s => s.UsrLoginDate); }
                if (filterOptions.ColumnIndex == 10) { query = query.OrderBy(s => s.UsrIsDisabled); }
                if (filterOptions.ColumnIndex == 11) { query = query.OrderBy(s => s.UsrLockedUntil); }
                if (filterOptions.ColumnIndex == 12) { query = query.OrderBy(s => s.UsrRetryCount); }
                if (filterOptions.ColumnIndex == 13) { query = query.OrderBy(s => s.UsrPasswordExpiry); }

            }
            else if (filterOptions.sortOrder == SortOrder.Descending)
            {
                if (filterOptions.ColumnIndex == 0) { query = query.OrderByDescending(s => s.UsrPkId); }
                if (filterOptions.ColumnIndex == 1) { query = query.OrderByDescending(s => s.UsrUserid); }
                if (filterOptions.ColumnIndex == 2) { query = query.OrderByDescending(s => s.UsrUserName); }
                if (filterOptions.ColumnIndex == 3) { query = query.OrderByDescending(s => s.UsrEmail); }
                if (filterOptions.ColumnIndex == 4) { query = query.OrderByDescending(s => s.UsrCompanyName); }
                if (filterOptions.ColumnIndex == 5) { query = query.OrderByDescending(s => s.UsrContactNo); }
                if (filterOptions.ColumnIndex == 6) { query = query.OrderByDescending(s => s.UsrPosition); }
                if (filterOptions.ColumnIndex == 7) { query = query.OrderByDescending(s => s.UsrDepartment); }
                if (filterOptions.ColumnIndex == 8) { query = query.OrderByDescending(s => s.UsrEmail); }
                if (filterOptions.ColumnIndex == 9) { query = query.OrderByDescending(s => s.UsrLoginDate); }
                if (filterOptions.ColumnIndex == 10) { query = query.OrderByDescending(s => s.UsrIsDisabled); }
                if (filterOptions.ColumnIndex == 11) { query = query.OrderByDescending(s => s.UsrLockedUntil); }
                if (filterOptions.ColumnIndex == 12) { query = query.OrderByDescending(s => s.UsrRetryCount); }
                if (filterOptions.ColumnIndex == 13) { query = query.OrderByDescending(s => s.UsrPasswordExpiry); }
            }
            var lst = await query.Skip(filterOptions.StartPageNo).Take(filterOptions.RecordsPerPage).ToListAsync();
            return lst.Select(s => new UserDetailRequestDTO
            {
                PkId = s.UsrPkId,
                ContrPkId = s.UsrContrPkId,
                Username = s.UsrUserName,
                Password = s.UsrPassword,
                Position = s.UsrPosition,
                Department = s.UsrDepartment,
                Companyname = s.UsrCompanyName,
                Email = s.UsrEmail,
                Contactno = s.UsrContactNo,
                ReportingPkId = s.UsrReportingUsrPkId,
                ModBy = s.UsrModBy,
                ModDt = s.UsrModDt,
                CrBy = s.UsrCrBy,
                CrDt = s.UsrCrDt,
                SubmitSts = s.UsrSubmitSts,
                ActiveYn = s.UsrActiveYn,
                Logindate = s.UsrLoginDate,
                Isdisabled = s.UsrIsDisabled,
                Retrycount = s.UsrRetryCount,
                Lockeduntil = s.UsrLockedUntil,
                Passwordexpiry = s.UsrPasswordExpiry,
                DfltUserrole = s.UsrDfltUserRole,
                UgDfltYn = s.UsrUgDfltYn,
                Sign = s.UsrSign,
                Userid = s.UsrUserid,
                ForceRstPwd = s.UsrForceRstPwd
            }).ToList();
        }

        public List<RmDepartment> GetDepartments()
        {
            return _context.RmDepartment.ToList();
        }

        public List<RmGroup> GetGroups(string department)
        {
            var dpt = _context.RmDepartment.Where(s => s.DeptName == department).FirstOrDefault();
            if (dpt != null)
            {
                return _context.RmGroup.Where(s => s.DepartmentDeptPkId == dpt.DeptPkId).ToList();
            }
            else
            {
                return _context.RmGroup.ToList();
            }
        }

        public List<RmGroupUser> GetGroupUsers(int userid)
        {
            return _context.RmGroupUser.Where(s => s.RmUsersUsrPkId == userid).ToList();
        }

        public List<RmUserGroup> GetUserGroups()
        {
            return _context.RmUserGroup.ToList();
        }

        public async Task<int> ChangeGroup(UserDetailRequestDTO model)
        {
            try
            {
                string grIds = string.Join(',', model.GroupId);
                StringBuilder sb = new StringBuilder();
                sb.Append($"DELETE FROM [RM_Group_User] WHERE RmUsersUsrPkId = {model.PkId} ");
                sb.Append($"Insert into [RM_Group_user](RmUsersUsrPkId,RmGroupsUgPkId) select {model.PkId}, g.UgPkId from [RM_Group] as g where g.UgPkId in({grIds})");
                return await _context.Database.ExecuteSqlCommandAsync(sb.ToString());

            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
