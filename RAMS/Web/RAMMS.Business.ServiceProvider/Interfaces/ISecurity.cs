
using Microsoft.Extensions.Configuration;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.Business.ServiceProvider.Interfaces
{
    public interface ISecurity
    {
        /// <summary>
        /// Sysstem Login
        /// </summary>
        bool IsLogin { get; }
        /// <summary>
        /// Login Username
        /// </summary>
        string UserName { get; }
        int UserID { get; set; }
        /// <summary>
        /// Login User Email
        /// </summary>
        string Email { get; }
        string Group { get; }
        string Module { get; }
        IList<string> Groups { get; }
        IList<string> Modules { get; }
        /// <summary>
        /// Logoin User Roles
        /// </summary>        
        void WebSignInRegister(RAMMS.Domain.Models.RmUsers user);
        /// <summary>
        /// Device / Mogile Login roles
        /// </summary>
        /// <param name="config"></param>
        /// <param name="user">User object</param>
        /// <returns></returns>
        public string DeviceSignInRegister(IConfiguration config, RAMMS.Domain.Models.RmUsers user);
        void WebSignOut();
        bool HasGroup(string groupName);
        public bool HasAnyGroup(params string[] groupNames);

        bool HasAnyGroup(ICollection<string> groupNames);
        bool HasModule(string modName);
        public bool HasAnyModule(params string[] modNames);
        bool HasAnyModule(ICollection<string> modNames);
        IList<RmGroup> AllGroups();
        IList<RmUvModuleGroupFieldRights> FieldRights();
        IList<RmUvModuleGroupRights> ModuleRights();
        RmUvModuleGroupFieldRights FieldRights(string fieldName);
        /// <summary>
        /// Is field disabled with respective of Group and Module
        /// </summary>
        /// <param name="fieldName">Field Name</param>
        /// <returns>True / False. Default is false, if there is no records in configura table</returns>
        bool IsFieldDisabled(string moduleName, string fieldName);
        /// <summary>
        /// Is field  hidden with respective of Group and Module. Regarding Modulename, you can get it from ModuleNameList
        /// </summary>
        /// <param name="fieldName">Field Name</param>
        /// <returns>True / False. Default is false, if there is no records in configura table</returns>
        bool IsFieldHide(string moduleName, string fieldName);
        /// <summary>
        ///  Is PC View with respective of Group and Module. Regarding Modulename, you can get it from ModuleNameList
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        bool IsPCView(string moduleName);
        /// <summary>
        ///  Is PC Modify with respective of Group and Module. Regarding Modulename, you can get it from ModuleNameList
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        bool IsPCModify(string moduleName);
        /// <summary>
        ///  Is PC Delete with respective of Group and Module. Regarding Modulename, you can get it from ModuleNameList
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        bool IsPCDelete(string moduleName);
        /// <summary>
        /// Is PC having ADD rights
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns></returns>
        bool IsPCAdd(string moduleName);
        bool IsPCView(string moduleName, params string[] groupName);
        bool IsPCModify(string moduleName, params string[] groupName);
        bool IsPCDelete(string moduleName, params string[] groupName);
        bool IsPCAdd(string moduleName, params string[] groupName);
        /// <summary>
        ///  Get PC Operation Rights (View / Modify / Delete) with respective of Group and Module. Regarding Modulename, you can get it from ModuleNameList
        /// </summary>
        /// <param name="moduleName"></param>
        /// <returns>Item1-->IsView, Item2-->IsModify,Item3-->IsDelete</returns>
        public Tuple<bool, bool, bool, bool> PCOperationRights(string moduleName, string[] groupName, bool reqView, bool reqModify, bool reqDelete, bool reqAdd = false);
        bool IsSupervisor { get; }
        bool IsExecutive { get; }
        bool IsHeadMaintenance { get; }
        bool IsJKRSSuperiorOfficer { get; }
        bool IsRegionManager { get; }

    }
}
