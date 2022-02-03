using System;
using System.Collections.Generic;
using RAMMS.MobileApps.Services;

namespace RAMMS.MobileApps
{
  
    public class PageResult
    {
        public int No { get; set; }
        public int HeaderNo { get; set; }
        public object ReferenceId { get; set; }
        public string Dt { get; set; }
        public int Srno { get; set; }
        public string SiteRef { get; set; }
        public int? FromCh { get; set; }
        public int? FromChDeci { get; set; }
        public int? ToCh { get; set; }
        public int? ToChDeci { get; set; }
        public string DefCode { get; set; }
        public string Problem { get; set; }
        public string WorkNeed { get; set; }
        public string Priority { get; set; }
        public object ActCode { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        //public double? Length { get; set; }
        //public double? Width { get; set; }
        //public double? Height { get; set; }
        public int? Wi { get; set; }
        public int? Ws { get; set; }
        public int? Wtc { get; set; }
        public int? Wc { get; set; }
        public int? Rt { get; set; }
        public string Remarks { get; set; }
        public object FormhApp { get; set; }
        public object ModBy { get; set; }
        public object ModDt { get; set; }
        public object CreatedBy { get; set; }
        public object CreatedDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string FadRefNO { get; set; }
        public object SiteRef_multiSelect { get; set; }
        public object CallFrom { get; set; }
        public object HeaderRefNo { get; set; }
    }

    public class FormJGridListResponseData
    {
        public int PageNo { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredRecords { get; set; }
        public List<PageResult> PageResult { get; set; }
    }

    public class FormJGridListResponse:ResponseBase
    {
        public FormJGridListResponseData data { get; set; }
    }


}
