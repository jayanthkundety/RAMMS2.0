using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Common;
using RAMMS.Domain.Models;
using RAMMS.DTO.JQueryModel;
using RAMMS.DTO.RequestBO;
using RAMMS.DTO.Wrappers;
using RAMMS.Web.UI.Models;

namespace RAMMS.Web.UI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService userService;
        private readonly ISecurity security;
        private readonly IModuleGroupRightsService modulegrouprights;
        public AccountController(ISecurity sec, IUserService userService, IModuleGroupRightsService modulegrouprights)
        {
            this.userService = userService;
            this.security = sec;
            this.modulegrouprights = modulegrouprights;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Add(int id, bool view)
        {
            UserDetailRequestDTO obj = new UserDetailRequestDTO();
            if (id > 0)
            {
                obj = await userService.GetDetailById(id); obj = obj ?? new UserDetailRequestDTO();
            }
            obj.IsView = view;
            ViewData["GroupList"] = userService.GetGroupList(id).Result;
            ViewData["ModuleList"] = userService.GetModuleList().Result;
            return View("~/Views/Account/_Add.cshtml", obj);
        }

        public async Task<IActionResult> GetDetailById(int id)
        {
            UserDetailRequestDTO obj = new UserDetailRequestDTO();
            if (id > 0)
            {
                obj = await userService.GetDetailById(id); obj = obj ?? new UserDetailRequestDTO();
            }
            return Json(obj);
        }
        public async Task<bool> RemoveDetail(int id) => await userService.RemoveDetail(id);
        public async Task<int> SaveDetail(UserDetailRequestDTO model)
        {
            if (model.PkId == 0)
            {
                model.CrBy = security.UserName;
                model.CrDt = DateTime.UtcNow;
                model.Password = Cryptography.PasswordHashed(model.Password);

            }
            model.ModBy = security.UserName;
            model.ModDt = DateTime.UtcNow;
            model.Logindate = DateTime.UtcNow;
            int i = await userService.SaveDetail(model);
            return i;
        }

        public async Task<int> ChangePassword(UserDetailRequestDTO model)
        {
            int response = await userService.ChangePassword(model);
            return response;

        }

        public async Task<int> ChangeGroup(UserDetailRequestDTO model)
        {
            int response = await userService.ChangeGroup(model);
            return response;

        }

        public IActionResult GetGroupUsers(int userid)
        {
            var lst = userService.GetGroupUsers(userid);
            (int?[] ug, int[] g) result = (lst.Select(s => s.RmGroupsUgPkId).ToArray(), lst.Select(s => s.UsrGpkid).ToArray());
            return Json(new { ug = result.ug, g = result.g });
        }
        public IActionResult GetGroups(string department)
        {
            try
            {
                var lst = userService.GetGroups(department);
                var result = lst.Select(s => new
                {
                    s.UgGroupName,
                    s.UgPkId
                }).ToList();
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        public IActionResult GetUserGroups()
        {
            var lst = userService.GetUserGroups();
            return Json(lst);
        }



        public long LastInsertedDetailNo() => userService.LastDetailInsertedNo();
        public async Task<IActionResult> GetDetailList(DataTableAjaxPostModel<UserDetailRequestDTO> searchData)
        {
            FilteredPagingDefinition<UserDetailRequestDTO> filteredPagingDefinition = new FilteredPagingDefinition<UserDetailRequestDTO>();
            searchData.filterData = searchData.filterData ?? new UserDetailRequestDTO();
            if (Request.Form.ContainsKey("columns[0][search][value]"))
            {
                searchData.filterData.SmartSearch = Request.Form["columns[0][search][value]"].ToString();
            }
            filteredPagingDefinition.Filters = searchData.filterData; if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length;
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await userService.GetDetailList(filteredPagingDefinition);
            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }

        public IActionResult UserGroup()
        {
            return View("~/Views/Account/ModuleGroupRights.cshtml");
        }

        public async Task<IActionResult> GetMGById(int id)
        {
            ModuleGroupRightsRequestDTO obj = new ModuleGroupRightsRequestDTO(); if (id > 0) { obj = await modulegrouprights.GetDetailById(id); obj = obj ?? new ModuleGroupRightsRequestDTO(); }
            return Json(obj);
        }
        public async Task<bool> RemoveMG(int id) => await modulegrouprights.RemoveDetail(id);
        public async Task<int> SaveMG(ModuleGroupRightsRequestDTO model) => await modulegrouprights.SaveDetail(model);
        public long LastInsertedMGNo(int id) => modulegrouprights.LastDetailInsertedNo();
        public async Task<IActionResult> GetMGList(DataTableAjaxPostModel<ModuleGroupRightsRequestDTO> searchData)
        {
            FilteredPagingDefinition<ModuleGroupRightsRequestDTO> filteredPagingDefinition = new FilteredPagingDefinition<ModuleGroupRightsRequestDTO>();
            filteredPagingDefinition.Filters = searchData.filterData;
            if (searchData.order != null)
            {
                filteredPagingDefinition.ColumnIndex = searchData.order[0].column;
                filteredPagingDefinition.sortOrder = searchData.order[0].SortOrder == SortDirection.Asc ? SortOrder.Ascending : SortOrder.Descending;
            }
            filteredPagingDefinition.RecordsPerPage = searchData.length; //Convert.ToInt32(Request.Form["length"]);
            filteredPagingDefinition.StartPageNo = searchData.start; //Convert.ToInt32(Request.Form["start"]); //TODO
            var result = await modulegrouprights.GetDetailList(filteredPagingDefinition);

            return Json(new { draw = searchData.draw, recordsFiltered = result.TotalRecords, recordsTotal = result.TotalRecords, data = result.PageResult });
        }
        public IActionResult GroupRights(int id = 0)
        {
            ViewBag.GroupId = id;
            ViewData["GroupList"] = userService.GetGroupList(0).Result;
            ViewData["ModuleList"] = userService.GetModuleList().Result;
            return View("~/Views/Account/GroupModuleRights.cshtml");
        }
        public async Task<JsonResult> GetModuleRights(int groupId)
        {
            return Json(await userService.GetModuleRightsList(groupId), base.JsonOption());
        }
        public async Task<JsonResult> SaveModuleRights(int GroupId, List<UserModuleRightsDTO> ModuleRights)
        {
            await userService.SaveModuleRightsList(GroupId, ModuleRights, security.UserName);
            return Json(new { Message = "Sucessfully Saved" }, base.JsonOption());
        }
        public async Task<JsonResult> SaveGroup(int GroupId, string GroupName, string GroupCode)
        {
            Result<int> objresult = new Result<int>();
            try
            {
                objresult.Data = await userService.SaveGroup(GroupId, GroupName, GroupCode, security.UserName);
                objresult.IsSuccess = true;
                objresult.Message = "Sucessfully Saved";
            }
            catch (Exception ex)
            {
                objresult.IsSuccess = false;
                objresult.Message = ex.Message;
            }
            return Json(objresult, base.JsonOption());
        }
    }
}
