using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.ResponseBO;
using RAMMS.DTO.Wrappers;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDTO>> GetUserNameRespectToPosition(UserRequestDTO userRequestDTO);
        Task<UserResponseDTO> GetUserNameByCode(UserRequestDTO userRequestDTO);
        Task<IEnumerable<SelectListItem>> GetUserList();
        IEnumerable<SelectListItem> GetUserSelectList(int? userId = null);
        Task<IEnumerable<CSelectListItem>> GetUser();
        Task<IEnumerable<SelectListItem>> GetSupervisor();
        Task<IEnumerable<CSelectListItem>> GetSupervisor(string groupCode, string gC);
        RmUsers GetUser(RmUserCredential userDetails);
        Task<UserDetailRequestDTO> GetDetailById(int id);
        Task<int> GetPasswordReset(string email, string hostedUrl);
        Task<UserResponseDTO> GetUserByUsrUserId(UserRequestDTO userRequestDTO);
        Task<bool> RemoveDetail(int id);
        Task<int> SaveDetail(UserDetailRequestDTO model);
        long LastDetailInsertedNo();
        Task<PagingResult<UserDetailRequestDTO>> GetDetailList(FilteredPagingDefinition<UserDetailRequestDTO> filteredPagingDefinition);
        Task<int> ChangePassword(UserDetailRequestDTO model);
        List<RmDepartment> GetDepartments();
        List<RmGroup> GetGroups(string department);
        List<RmGroupUser> GetGroupUsers(int userid);
        List<RmUserGroup> GetUserGroups();
        Task<int> ChangeGroup(UserDetailRequestDTO model);
        Task<IEnumerable<CSelectListItem>> GetGroupList(int userId);
        Task<IEnumerable<CSelectListItem>> GetModuleList();
        Task<List<UserModuleRightsDTO>> GetModuleRightsList(int groupId);
        Task<int> SaveModuleRightsList(int groupId, List<UserModuleRightsDTO> rights, string createdBy);
        Task<int> SaveGroup(int GroupId, string GroupName, string GroupCode, string createdBy);
        Task<object> GetUserNotification(int topCount, DateTime? from);
    }
}
