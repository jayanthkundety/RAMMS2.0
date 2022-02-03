using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.Business.ServiceProvider
{
    public class MemoryCache
    {
        public IList<RAMMS.Domain.Models.RmUvModuleGroupFieldRights> FieldRights { get; set; }
        public IList<RAMMS.Domain.Models.RmUvModuleGroupRights> ModuleRights { get; set; }
        public IList<RAMMS.Domain.Models.RmGroup> Groups { get; set; }

        public static MemoryCache _MemoryCache = null;
        public static MemoryCache Instance
        {
            get { return _MemoryCache ??= new MemoryCache(); }
        }

    }
}
