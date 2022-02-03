using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmFormDownloadUseFormABak
    {
        public int FduPkRefNo { get; set; }
        public string FduFormType { get; set; }
        public string FduHeaderName { get; set; }
        public string FduTableFieldName { get; set; }
        public string FduTableName { get; set; }
        public string FduTableTypeHdrDtl { get; set; }
        public string FduExcelCellNo { get; set; }
        public string FduAppendOverwrite { get; set; }
        public string FduSeperator { get; set; }
        public string FduModBy { get; set; }
        public DateTime? FduModDt { get; set; }
        public string FduCrBy { get; set; }
        public DateTime? FduCrDt { get; set; }
        public bool FduSubmitSts { get; set; }
        public bool FduActiveYn { get; set; }
        public int? FduExcelRowNo { get; set; }
        public int? FduExcelColumnNo { get; set; }
        public int? Maxlength { get; set; }
        public bool? Mutilpleline { get; set; }
        public int? Startindex { get; set; }
        public int? Endindex { get; set; }
    }
}
