using RAMMS.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using RAMMS.MobileApps.Model.Adapter;

namespace RAMMS.MobileApps.Model
{
    public static class Security
    {
        public static string Token { get; set; }
        
        public static UserDetails userDetails { get; set; }         
        
        

        /// <summary>
        /// Contains Group with respective of gorup name
        /// </summary>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static bool HasGroup(string groupName)
        {
            return ("," + userDetails.Group + ",").Contains("," + groupName + ",");
        }
        /// <summary>
        /// Contains group with respective of list of group name
        /// </summary>
        /// <param name="groupNames">List of group name</param>
        /// <returns></returns>
        public static bool HasAnyGroup(params string[] groupNames)
        {
            foreach (var group in groupNames)
            {
                if (HasGroup(group))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Contains Module with respective of Module Name
        /// </summary>
        /// <param name="modName">Module Name</param>
        /// <returns></returns>
        public static bool HasModule(string modName)
        {
            return ("," + userDetails.Module + ",").Contains("," + modName + ",");
        }
        /// <summary>
        /// Contains Module with respective of list of module name
        /// </summary>
        /// <param name="modNames">Collection of Module name</param>
        /// <returns></returns>
        public static bool HasAnyModule(params string[] modNames)
        {
            foreach (var modName in modNames)
            {
                if (HasModule(modName))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Get Field Rights with respective of field name, Group and Module
        /// </summary>
        /// <param name="fieldName">Field Name</param>
        /// <returns>First In First Out / Default</returns>
        public static RmUvModuleGroupFieldRights FieldRights(string fieldName)
        {
            IList<RmUvModuleGroupFieldRights> fields = userDetails.AllFieldRights;
            RmUvModuleGroupFieldRights result = fields.Where(x => x.MgfrFieldName == fieldName && userDetails.Groups.Contains(x.GroupCode) && userDetails.Modules.Contains(x.ModuleName)).FirstOrDefault();
            return result == null ? new RmUvModuleGroupFieldRights() { MgfrIsDisabled = false, MgfrIsHide = false } : result;
        }
        /// <summary>
        /// Is field disabled with respective of Group and Module
        /// </summary>
        /// <param name="fieldName">Field Name</param>
        /// <returns>True / False. Default is false, if there is no records in configura table</returns>
        public static bool IsFieldDisabled(string moduleName, string fieldName)
        {
            IList<RmUvModuleGroupFieldRights> fields = userDetails.AllFieldRights;
            if (userDetails.Groups != null && userDetails.Modules != null)
            {
                return fields.Where(x => x.MgfrFieldName == fieldName && x.MgfrIsDisabled.HasValue && x.MgfrIsDisabled.Value && userDetails.Groups.Contains(x.GroupCode) && x.ModuleName == moduleName && userDetails.Modules.Contains(x.ModuleName)).Count() > 0 ? true : false;
            }
            return false;
        }
        /// <summary>
        /// Is field  hidden with respective of Group and Module
        /// </summary>
        /// <param name="fieldName">Field Name</param>
        /// <returns>True / False. Default is false, if there is no records in configura table</returns>
        public static bool IsFieldHide(string moduleName, string fieldName)
        {
            IList<RmUvModuleGroupFieldRights> fields = userDetails.AllFieldRights;
            if (userDetails.Groups != null && userDetails.Modules != null)
            {
                return fields.Where(x => x.MgfrFieldName == fieldName && x.MgfrIsHide.HasValue && x.MgfrIsHide.Value && userDetails.Groups.Contains(x.GroupCode) && x.ModuleName == moduleName && userDetails.Modules.Contains(x.ModuleName)).Count() > 0 ? true : false;
            }
            return false;
        }
        public static bool IsView(string moduleName)
        {
            return OperationRights(moduleName,null, true, false, false).Item1;
        }
        public static bool IsModify(string moduleName)
        {
            return OperationRights(moduleName,null, false, true, false).Item2;
        }
        public static bool IsDelete(string moduleName)
        {
            return OperationRights(moduleName,null, false, false, true).Item3;
        }
        public static bool IsAdd(string moduleName)
        {
            return OperationRights(moduleName, null, false, false, false,true).Item4;
        }
        public static Tuple<bool, bool, bool, bool> OperationRights(string moduleName, string[] groupName, bool reqView, bool reqModify, bool reqDelete, bool reqAdd = false)
        {
            bool blnView = false, blnDelete = false, blnModify = false, blnAdd = false;
            IList<RmUvModuleGroupRights> mgRights = userDetails.ModuleRights;
            if (userDetails.Modules != null)
            {
                if (HasGroup("admin"))
                {
                    if (reqView)
                        blnView = true;
                    if (reqModify)
                        blnModify = true;
                    if (reqDelete)
                        blnDelete = true;
                    if (reqAdd)
                        blnAdd = true;
                }
                else
                {
                    var rsltFilter = mgRights.Where(x => userDetails.Groups.Contains(x.GroupCode) && userDetails.Modules.Contains(x.ModuleName)).ToList();
                    if (reqView)
                        blnView = rsltFilter.Where(x => (groupName == null || groupName.Contains(x.GroupCode)) && x.ModuleName == moduleName && x.PcIsView.HasValue && x.PcIsView.Value).Count() > 0 ? true : false;
                    if (reqModify)
                        blnModify = rsltFilter.Where(x => (groupName == null || groupName.Contains(x.GroupCode)) && x.ModuleName == moduleName && x.PcIsModify.HasValue && x.PcIsModify.Value).Count() > 0 ? true : false;
                    if (reqDelete)
                        blnDelete = rsltFilter.Where(x => (groupName == null || groupName.Contains(x.GroupCode)) && x.ModuleName == moduleName && x.PcIsDelete.HasValue && x.PcIsDelete.Value).Count() > 0 ? true : false;
                    if (reqAdd)
                        blnAdd = rsltFilter.Where(x => (groupName == null || groupName.Contains(x.GroupCode)) && x.ModuleName == moduleName && x.PcIsAdd.HasValue && x.PcIsAdd.Value).Count() > 0 ? true : false;
                }
            }
            return new Tuple<bool, bool, bool, bool>(blnView, blnModify, blnDelete, blnAdd);
        }
    }
}
