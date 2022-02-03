using RAMMS.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.MobileApps.Model.Adapter
{
    public class UserDetails
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        private string _Group = "";
        public string Group { get { return _Group; } set { _Group = value; Groups = value.Split(','); } }
        private string _Module = "";
        public string Module { get { return _Module; } set { _Module = value; Modules = value.Split(','); } }
        public IList<string> Groups { get; set; }
        public IList<string> Modules { get; set; }
        public IList<RmUvModuleGroupRights> ModuleRights
        {
            get; set;
        }
        /// <summary>
        /// Gett all the groups
        /// </summary>
        /// <returns></returns>
        //public IList<RmGroup> AllGroups
        //{
        //    get;
        //    set;            
        //}

        public IList<RmUvModuleGroupFieldRights> AllFieldRights
        {
            get; set;            
        }
    }
}
