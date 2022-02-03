using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Wrappers
{
    public class PagingDefinition
    {
        public int StartPageNo { get; set; }
        public int RecordsPerPage { get; set; }
        public int ColumnIndex { get; set; }
        public SortOrder sortOrder { get; set; }
    }

    public enum SortOrder
    {
        Ascending =0,
       Descending  =1
    }
}
