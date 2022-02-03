using System;
using System.Collections.Generic;
using RAMMS.MobileApps.Services;

namespace RAMMS.MobileApps
{
    public class GetFormJDtlById
    {
        public int No { get; set; }
        public int HeaderNo { get; set; }
        public string Dt { get; set; }
        public int Srno { get; set; }
        public object SiteRef { get; set; }
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
        public object SiteRef_multiSelect { get; set; }
        public object CallFrom { get; set; }
        public object HeaderRefNo { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class FormADetail
    {
        public int No { get; set; }
        public int HeaderNo { get; set; }
        public object ReferenceId { get; set; }
        public string Dt { get; set; }
        public int Srno { get; set; }
        public string SiteRef { get; set; }
        public int FromCh { get; set; }
        public int FromChDeci { get; set; }
        public int ToCh { get; set; }
        public int ToChDeci { get; set; }
        public string DefCode { get; set; }
        public string Problem { get; set; }
        public string WorkNeed { get; set; }
        public string Priority { get; set; }
        public object ActCode { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int Wi { get; set; }
        public int Ws { get; set; }
        public int Wtc { get; set; }
        public int Wc { get; set; }
        public int Rt { get; set; }
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

    public class FormJFindDetailResponseData
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
        public object UseridPrp { get; set; }
        public object UsernamePrp { get; set; }
        public object DesignationPrp { get; set; }
        public object DtPrp { get; set; }
        public object SignVer { get; set; }
        public object UseridVer { get; set; }
        public object UsernameVer { get; set; }
        public object DesignationVer { get; set; }
        public object VerifiedDt { get; set; }
        public object UsernameVet { get; set; }
        public object DesignationVet { get; set; }
        public object VerifiedVDt { get; set; }
        public object ModBy { get; set; }
        public object ModDt { get; set; }
        public object CreatedBy { get; set; }
        public object CreatedDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string section { get; set; }
        public List<FormADetail> FormADetails { get; set; }
        public object MonthYear { get; set; }
        public object Status { get; set; }
        public object RmuName { get; set; }
        public object SectionCode { get; set; }
    }

    public class FormJFindDetailResponse:ResponseBase
    {
        public FormJFindDetailResponseData data { get; set; }
    }
    public class GetFormJDtlByIdRest:ResponseBase
    {
        public GetFormJDtlById data { get; set; }
    }

    public class GetSerialNo:ResponseBase
    {
        public string data { get; set; }
    }

}
