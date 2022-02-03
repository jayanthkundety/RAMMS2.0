using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmFormGenDtl
    {
        public int FgdPkId { get; set; }
        public string FgdFormName { get; set; }
        public string FgdFileName { get; set; }
        public string FgdFilePath { get; set; }
        public string FgdRemarks { get; set; }
        public string FgdModBy { get; set; }
        public DateTime FgdModDt { get; set; }
        public string FgdCrBy { get; set; }
        public DateTime FgdCrDt { get; set; }
        public bool FgdSubmitSts { get; set; }
    }
}
