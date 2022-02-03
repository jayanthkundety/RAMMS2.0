using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.MobileApps
{
    public class PagingResult<T>
    {
        public int PageNo { get; set; }
        public long TotalRecords { get; set; }
        public int FilteredRecords { get; set; }
        public List<T> PageResult { get; set; }
    }
}
