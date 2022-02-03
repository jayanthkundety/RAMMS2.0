using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Domain.Models;
using RAMMS.DTO.RequestBO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RAMMS.Repository.Interfaces
{
    public interface IUserRepository : IRepositoryBase<RmUsers>, IUserDetailRepository
    {
        int SaveUser(RmUsers user);
        RmUsers GetUser(RmUsers userDetails);
        Task<RmUsers> GetUserByUsrUserId(UserRequestDTO user);
        RmUsers GetUser(string userName);
        Task<IEnumerable<RmUsers>> GetUsers();
        Task<List<RmUsers>> GetUserWithPosition(UserRequestDTO userRequestDTO);
        Task<RmUsers> GetUserByCode(UserRequestDTO userRequestDTO);
        Task<IEnumerable<SelectListItem>> getSupervisor();
        Task<bool> CheckUserEmailExist(string email);
        List<RmDepartment> GetDepartments();
    }
}
