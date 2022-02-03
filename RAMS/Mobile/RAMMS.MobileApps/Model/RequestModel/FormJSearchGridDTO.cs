using System;
using System.Collections.Generic;
using RAMMS.DTO.Wrappers;
using RAMMS.MobileApps.Services;

namespace RAMMS.MobileApps
{

    public class FormJDetailResponse
    {
        public int No { get; set; }
        public int HeaderNo { get; set; }
        public string Dt { get; set; }
        public int Srno { get; set; }
        public string SiteRef { get; set; }
        public object FromCh { get; set; }
        public object FromChDeci { get; set; }
        public object ToCh { get; set; }
        public object ToChDeci { get; set; }
        public string DefCode { get; set; }
        public object Problem { get; set; }
        public object WorkNeed { get; set; }
        public object Priority { get; set; }
        public object ActCode { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Wi { get; set; }
        public int Ws { get; set; }
        public int Wtc { get; set; }
        public int Wc { get; set; }
        public int Rt { get; set; }
        public object Remarks { get; set; }
        public object FormhApp { get; set; }
        public object ModBy { get; set; }
        public object ModDt { get; set; }
        public object CreatedBy { get; set; }
        public object CreatedDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string FadRefNO { get; set; }
        public List<string> SiteRef_multiSelect { get; set; }
        public object CallFrom { get; set; }
        public object HeaderRefNo { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
    }
    public class FormJSearchGridDTO
    {

        public int No { get; set; }
        public string Id { get; set; }
        public string RoadCode { get; set; }
        public string Rmu { get; set; }
        public string RoadName { get; set; }
        public object ContNo { get; set; }
        public string AssetGroupCode { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public object Remarks { get; set; }
        public object SignPrp { get; set; }
        public int? UseridPrp { get; set; }
        public string UsernamePrp { get; set; }
        public string DesignationPrp { get; set; }
        public DateTime? DtPrp { get; set; }
        public object SignVer { get; set; }
        public int? UseridVer { get; set; }
        public string UsernameVer { get; set; }
        public string DesignationVer { get; set; }
        public DateTime? VerifiedDt { get; set; }
        public object SignVet { get; set; }
        public int? UseridVet { get; set; }
        public string UsernameVet { get; set; }
        public string DesignationVet { get; set; }
        public DateTime? VerifiedVDt { get; set; }
        public object ModBy { get; set; }
        public object ModDt { get; set; }
        public object CreatedBy { get; set; }
        public object CreatedDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string section { get; set; }
        public List<FormJDetailResponse> FormADetails { get; set; }
        public object MonthYear { get; set; }
        public object Status { get; set; }
        public string RmuName { get; set; }
        public string SectionCode { get; set; }

    }
    public class FormJDataList
    {
        public int PageNo { get; set; }
        public int TotalRecords { get; set; }
        public int FilteredRecords { get; set; }
        public List<FormJSearchGridDTO> PageResult { get; set; }
    }
    public class FormJLadingGridList: ResponseBase
    {
      
       public FormJDataList data { get; set; }
    }
    public class FormJSearchDTO
    {
        public string Id { get; set; }
        public string Reference_No { get; set; }
        public string RMU { get; set; }
        public int? Month { get; set; }
        public string Road_Code { get; set; }
        public string Asset_Type { get; set; }
        public string Asset_GroupCode { get; set; }
        public string Owner { get; set; }
        public string Verified_By { get; set; }
        public string Section { get; set; }
        public int? ChinageFromKm { get; set; }

        public int? ChinageToKm { get; set; }
        public int? ChinageFromM { get; set; }
        public int? ChinageToM { get; set; }
        public int? Year { get; set; }
        public string SmartInputValue { get; set; }
        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? Page_No { get; set; }
        public int? pageSize { get; set; }
    }
    public class FormJSearchBase
    {
        public int StartPageNo { get; set; }
        public int RecordsPerPage { get; set; }
        public string sortOrder { get; set; }
        public int ColumnIndex { get; set; }
        public FormJSearchDTO Filters { get; set; }
    }
    public class FormJByHeader:ResponseBase
    {
        public FormJSearchDTO data { get; set; }
    }
}
