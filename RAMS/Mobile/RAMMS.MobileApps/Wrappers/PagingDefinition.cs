using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.MobileApps
{
    public class PagingDefinition
    {
        public int StartPageNo { get; set; }
        public int RecordsPerPage { get; set; }
        public string sortOrder { get; set; }
        public int ColumnIndex { get; set; }
        
    }
}
