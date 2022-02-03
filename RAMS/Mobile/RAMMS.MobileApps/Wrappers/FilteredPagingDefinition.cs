using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.MobileApps
{ 
    public class FilteredPagingDefinition<T> : PagingDefinition
    {
        public T Filters { get; set; }
    }
}
