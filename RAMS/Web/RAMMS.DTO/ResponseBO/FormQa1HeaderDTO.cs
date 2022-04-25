using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormQa1HeaderDTO
    {
        public int PkRefNo { get; set; }
        public int? ContNo { get; set; }
        public string Rmu { get; set; }
        public string RmuName { get; set; }
        public string SecCode { get; set; }
        public string SecName { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }

        public int? WeekNo { get; set; }
        public string Day { get; set; }
        public int? Year { get; set; }
        public string RefId { get; set; }
        public int? Crew { get; set; }
        public string Crewname { get; set; }
        public string ActCode { get; set; }
        public string ActName { get; set; }
        public DateTime? Dt { get; set; }
        public int? UseridAssgn { get; set; }
        public bool? InitialAssgn { get; set; }
        public string UsernameAssgn { get; set; }
        public DateTime? DtAssgn { get; set; }
        public int? UseridExec { get; set; }
        public bool? InitialExec { get; set; }
        public string UsernameExec { get; set; }
        public DateTime? DtExec { get; set; }
        public int? UseridChked { get; set; }
        public bool? InitialChked { get; set; }
        public string UsernameChked { get; set; }
        public DateTime? DtChked { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool? SignAudit { get; set; }
        public int? UseridAudit { get; set; }
        public string UsernameAudit { get; set; }
        public string DesignationAudit { get; set; }
        public DateTime? DtAudit { get; set; }
        public string OfficeAudit { get; set; }
        public bool? SignWit { get; set; }
        public int? UseridWit { get; set; }
        public string UsernameWit { get; set; }
        public string DesignationWit { get; set; }
        public DateTime? DtWit { get; set; }
        public string OfficeWit { get; set; }
        public string Remarks { get; set; }
        public bool ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }

        public virtual List<FormQa1EqVhDTO> EqVh { get; set; }
        public virtual FormQa1GCDTO   Gc { get; set; }
        public virtual List<FormQa1GenDTO>  Gen { get; set; }
        public virtual FormQa1LabDTO  Lab { get; set; }
        public virtual List<FormQa1MatDTO>  Mat { get; set; }
        public virtual FormQa1SscDTO  Ssc  { get; set; }
        public virtual FormQa1TesDTO  Tes { get; set; }
        public virtual FormQa1WcqDTO  Wcq  { get; set; }
        public virtual FormQa1WeDTO We { get; set; }
    }

    public class FormQa1EqVhDTO
    {
        //Fqa1ev
        public int PkRefNo { get; set; }
        public int? Fqa1hPkRefNo { get; set; }
        public string Type { get; set; }
        public string Desc { get; set; }
        public string PVNo { get; set; }
        public decimal? Capacity { get; set; }
        public int? Unit { get; set; }
        public string Condition { get; set; }
        public string Remark { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }

        public virtual FormQa1HeaderDTO FormQa1Hdr { get; set; }
    }

    public class FormQa1GCDTO
    {
        //Fqa1gc
        public int PkRefNo { get; set; }
        public int? Fqa1hPkRefNo { get; set; }
        public bool? Whs { get; set; }
        public string WhsRemark { get; set; }
        public string WhsReason { get; set; }
        public bool? Wis { get; set; }
        public string WisRemark { get; set; }
        public string WisReason { get; set; }
        public bool? Wius { get; set; }
        public bool? WiusMat { get; set; }
        public string WiusMatRemark { get; set; }
        public string WiusMatReason { get; set; }
        public bool? WiusEqp { get; set; }
        public string WiusEqpReason { get; set; }
        public string WiusEqpRemark { get; set; }
        public bool? WiusWrk { get; set; }
        public string WiusWrkRemark { get; set; }
        public string WiusWrkReason { get; set; }

        public bool ActiveYn { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public virtual FormQa1HeaderDTO FormQa1Hdr { get; set; }
    }
    public class FormQa1GenDTO
    {
        //Fqa1gen
        public int PkRefNo { get; set; }
        public int? Fqa1hPkRefNo { get; set; }
        public string Item { get; set; }
        public string AttTo { get; set; }
        public string AttRemarks { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }

        public virtual FormQa1HeaderDTO FormQa1Hdr { get; set; }
    }

    public class FormQa1LabDTO
    {
        //Fqa1l
        public int PkRefNo { get; set; }
        public int? Fqa1hPkRefNo { get; set; }
        public int? LabCsOnSite { get; set; }
        public int? LabCsOnLeave { get; set; }
        public string LabCsPerfStd { get; set; }
        public string LabCsRemark { get; set; }
        public int? LabOpOnSite { get; set; }
        public int? LabOpOnLeave { get; set; }
        public string LabOpPerfStd { get; set; }
        public string LabOpRemark { get; set; }
        public int? LabDrOnSite { get; set; }
        public int? LabDrOnLeave { get; set; }
        public string LabDrPerfStd { get; set; }
        public string LabDrRemark { get; set; }
        public int? LabWmOnSite { get; set; }
        public int? LabWmOnLeave { get; set; }
        public string LabWmPerFStd { get; set; }
        public string LabWmRemark { get; set; }
        public int? LabOthOnSite { get; set; }
        public int? LabOthOnLeave { get; set; }
        public string LabOthPerfStd { get; set; }
        public string LabOthRemark { get; set; }

        public bool ActiveYn { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }


        public virtual FormQa1HeaderDTO Fqa1hPkRefNoNavigation { get; set; }
    }

    public class FormQa1MatDTO
    {
        //Fqa1m
        public int PkRefNo { get; set; }
        public int? Fqa1hPkRefNo { get; set; }
        public string Type { get; set; }
        public decimal? Qty { get; set; }
        public string Unit { get; set; }
        public string Spec { get; set; }
        public string Remark { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public virtual FormQa1HeaderDTO Fqa1hPkRefNoNavigation { get; set; }
    }

    public class FormQa1SscDTO
    {
        //Fqa1ssc
        public int PkRefNo { get; set; }
        public int? Fqa1hPkRefNo { get; set; }
        public string Sp { get; set; }
        public string SpRemark { get; set; }
        public string Ed { get; set; }
        public string EdRemark { get; set; }
        public string Wpe { get; set; }
        public string WpeRemark { get; set; }
        public string Ims { get; set; }
        public string ImsRemark { get; set; }
        public string Asd { get; set; }
        public string AsdRemark { get; set; }

        public bool ActiveYn { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }

        public virtual FormQa1HeaderDTO Fqa1hPkRefNoNavigation { get; set; }
    }

    public class FormQa1TesDTO
    {
        //Fqa1tes
        public int PkRefNo { get; set; }
        public int? Fqa1hPkRefNo { get; set; }
        public string CtCs { get; set; }
        public int? CtCsA { get; set; }
        public string CtCsRemark { get; set; }
        public string DtCs { get; set; }
        public int? DtCsA { get; set; }
        public string DtCsRemark { get; set; }
        public string MgtCs { get; set; }
        public int? MgtCsA { get; set; }
        public string MgtCsRemark { get; set; }
        public string CbrCs { get; set; }
        public int? CbrCsA { get; set; }
        public string CbrCsRemark { get; set; }
        public string OtCs { get; set; }
        public int? OtCsA { get; set; }
        public string OtCsRemark { get; set; }

        public bool ActiveYn { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }

        public virtual FormQa1HeaderDTO Fqa1hPkRefNoNavigation { get; set; }
    }

    public class FormQa1WcqDTO
    {
        //Fqa1wcq
        public int PkRefNo { get; set; }
        public int? Fqa1hPkRefNo { get; set; }
        public bool? FlFlushType { get; set; }
        public decimal? FlFlush { get; set; }
        public string FlFlushRemark { get; set; }
        public bool? FlThType { get; set; }
        public decimal? FlTh { get; set; }
        public string FlThRemark { get; set; }
        public bool? FlTlType { get; set; }
        public decimal? FlTl { get; set; }
        public string FlTlRemark { get; set; }
        public bool? FlScType { get; set; }
        public string FlScRemark { get; set; }
        public bool? FlUcType { get; set; }
        public string FlUcRemark { get; set; }
        public bool? JnType { get; set; }
        public string JnRemark { get; set; }
        public bool? JiType { get; set; }
        public string JiRemark { get; set; }
        public bool? SrevType { get; set; }
        public string SrevRemark { get; set; }
        public bool? SruevType { get; set; }
        public string SruevRemark { get; set; }
        public bool? SrprType { get; set; }
        public string SrprRemark { get; set; }

        public bool ActiveYn { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }

        public virtual FormQa1HeaderDTO Fqa1hPkRefNoNavigation { get; set; }
    }

    public class FormQa1WeDTO
    {
        //Fqa1w
        public int PkRefNo { get; set; }
        public int? Fqa1hPkRefNo { get; set; }
        public decimal? AcwcThinkness { get; set; }
        public int? AcwcThinknessUnit { get; set; }
        public decimal? AcwcTemperature { get; set; }
        public int? AcwcTemperatureUnit { get; set; }
        public int? AcwcPasses { get; set; }
        public string AcwcRemark { get; set; }
        public decimal? TcDRate { get; set; }
        public int? TcDRateUnit { get; set; }
        public string TcType { get; set; }
        public bool? TcEvenlySpread { get; set; }
        public string TcRemark { get; set; }
        public decimal? AcbcThinkness { get; set; }
        public int? AcbcThinknessUnit { get; set; }
        public decimal? AcbcTemperature { get; set; }
        public int? AcbcTemperatureUnit { get; set; }
        public int? AcbcPasses { get; set; }
        public string AcbcRemark { get; set; }
        public decimal? PcDRate { get; set; }
        public int? PcDRateUnit { get; set; }
        public string PcType { get; set; }
        public bool? PcEvenlySpread { get; set; }
        public string PcRemark { get; set; }
        public decimal? RbThinkness { get; set; }
        public int? RbThinknessUnit { get; set; }
        public int? RbLayers { get; set; }
        public int? RbPasses { get; set; }
        public string RbRemark { get; set; }
        public decimal? SbThinkness { get; set; }
        public int? SbThinknessUnit { get; set; }
        public int? SbLayers { get; set; }
        public int? SbPasses { get; set; }
        public string SbRemark { get; set; }
        public decimal? SgThinkness { get; set; }
        public int? SgThinknessUnit { get; set; }
        public int? SgLayers { get; set; }
        public int? SgPasses { get; set; }
        public string SgRemark { get; set; }
        public decimal? SsdSb { get; set; }
        public int? SsdSbUnit { get; set; }
        public decimal? SsdPp { get; set; }
        public int? SsdPpUnit { get; set; }
        public string SsdRemark { get; set; }

        public decimal? SsdCh { get; set; }
        public decimal? SsdChDeci { get; set; }
        public decimal? SsdRhsL { get; set; }
        public decimal? SsdRhsW { get; set; }
        public decimal? SsdLhsL { get; set; }
        public decimal? SsdLhsW { get; set; }

        public bool ActiveYn { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }

        public virtual FormQa1HeaderDTO Fqa1hPkRefNoNavigation { get; set; }
    }
}
