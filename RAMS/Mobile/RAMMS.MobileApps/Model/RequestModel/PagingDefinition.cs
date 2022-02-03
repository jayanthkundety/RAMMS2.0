using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.Wrappers
{
    public class PagingDefinition
    {
        public int StartPageNo { get; set; }
        public int RecordsPerPage { get; set; }
        public string sortOrder { get; set; }
    }
}
