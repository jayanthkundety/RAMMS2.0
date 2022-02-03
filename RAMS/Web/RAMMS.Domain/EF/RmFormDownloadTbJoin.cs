using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormDownloadTbJoin
    {
        public int FdtPk { get; set; }
        public string FdtFormType { get; set; }
        public string FdtTbJoins { get; set; }
        public string FdtTblPkFldName { get; set; }
        public string FdtTableTypeHdrDtl { get; set; }
    }
}
