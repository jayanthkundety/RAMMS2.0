using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RAMMS.Business.ServiceProvider;
using RAMMS.Business.ServiceProvider.Interfaces;
using RAMMS.Repository.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RAMMS.Web.UI.Models
{
    [Audit]
    public class BaseController : Controller
    {
        private IDDLookUpService lookupService;
        private IRoadMasterService rmService;
        private IUserService userService;
        public BaseController()
        {

        }
        public IDDLookUpService LookupService { get { return lookupService ??= HttpContext.RequestServices.GetService(typeof(IDDLookUpService)) as IDDLookUpService; } }
        private IRoadMasterService RMService { get { return rmService ??= HttpContext.RequestServices.GetService(typeof(IRoadMasterService)) as IRoadMasterService; } }

        private IUserService UserService { get { return userService ??= HttpContext.RequestServices.GetService(typeof(IUserService)) as IUserService; } }
        public void LoadLookupService(params string[] types)
        {
            if (types.Contains("RD_Code") || types.Contains("Road_Master"))
            {
                if (types.Contains("Road_Master"))
                    ViewData["Road_Master"] = RMService.GetAllRoadCodeAndName(true).Result;
                else
                    ViewData["RD_Code"] = RMService.GetAllRoadCodeAndName(false).Result;
                types = types.Where(x => x != "RD_Code" && x != "Road_Master").ToArray();
            }
            if (types.Contains("User"))
            {
                ViewData["User"] = UserService.GetUser().Result;
                types = types.Where(x => x != "User").ToArray();
            }
            if (types.Contains(GroupNameList.Supervisor))
            {
                ViewData[GroupNameList.Supervisor] = UserService.GetSupervisor(GroupNameList.Supervisor, GroupNameList.JKRSSuperiorOfficerSO).Result;
                types = types.Where(x => x != GroupNameList.Supervisor).ToArray();
            }
            if (types.Contains(GroupNameList.JKRSSuperiorOfficerSO))
            {
                ViewData[GroupNameList.JKRSSuperiorOfficerSO] = UserService.GetSupervisor(GroupNameList.JKRSSuperiorOfficerSO, null).Result;
                types = types.Where(x => x != GroupNameList.JKRSSuperiorOfficerSO).ToArray();
            }
            if (types.Contains(GroupNameList.OperationsExecutive))
            {
                ViewData[GroupNameList.OperationsExecutive] = UserService.GetSupervisor(GroupNameList.OperationsExecutive, null).Result;
                types = types.Where(x => x != GroupNameList.OperationsExecutive).ToArray();
            }
            if (types.Contains(GroupNameList.OperRegionManager))
            {
                ViewData[GroupNameList.OperRegionManager] = UserService.GetSupervisor(GroupNameList.OperRegionManager, null).Result;
                types = types.Where(x => x != GroupNameList.OperRegionManager).ToArray();
            }
            if (types.Contains(GroupNameList.OpeHeadMaintenance))
            {
                ViewData[GroupNameList.OpeHeadMaintenance] = UserService.GetSupervisor(GroupNameList.OpeHeadMaintenance, null).Result;
                types = types.Where(x => x != GroupNameList.OpeHeadMaintenance).ToArray();
            }
            if (types.Length > 0)
            {
                IList<CSelectListItem> lstItem = LookupService.GetDdLookup(types).Result.ToList();
                foreach (string type in types)
                {
                    if (type.Contains("^"))
                    {
                        //Method to get the DDL_Type_VALUE  in Value(Dropdown) Format(DDLType^Value/DDLType~Code^Value)
                        if (type.Contains("~"))
                        {
                            string[] arrType = type.Split('~');
                            ViewData[type.Replace(" ", "_")] = lstItem.Where(x => x.Key == arrType[0] && x.Code == arrType[1].Split('^')[0]);
                        }
                        else
                        {
                            string[] arrType = type.Split('^');
                            ViewData[type.Replace(" ", "_")] = lstItem.Where(x => x.Key == arrType[0].Trim().ToString());
                        }
                    }
                    else if (type.Contains("~"))
                    {
                        string[] arrType = type.Split('~');
                        ViewData[type.Replace(" ", "_")] = lstItem.Where(x => x.Key == arrType[0] && x.Code == arrType[1]);
                    }
                    else if (type == "Year")
                        ViewData[type.Replace(" ", "_")] = lstItem.Where(x => x.Key == type).OrderBy(x => x.Text);
                    else
                        ViewData[type.Replace(" ", "_")] = lstItem.Where(x => x.Key == type);
                }
            }

        }
        public object JsonOption()
        {
            return Common.Utility.JsonOption;
        }
    }
}
