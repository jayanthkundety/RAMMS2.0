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
        public string RefId { get; set; }
        public int? Crew { get; set; }

        public int? WeekNo { get; set; }

        public string Crewname { get; set; }
        public string ActCode { get; set; }

        public string ActName { get; set; }
        public DateTime? Dt { get; set; }
        public int? UseridAssgn { get; set; }
        public string InitialAssgn { get; set; }
        public string UsernameAssgn { get; set; }
        public DateTime? DtAssgn { get; set; }
        public int? UseridExec { get; set; }
        public string InitialExec { get; set; }
        public string UsernameExec { get; set; }
        public DateTime? DtExec { get; set; }
        public int? UseridChked { get; set; }
        public string InitialChked { get; set; }
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
        public DateTime? DateAudit { get; set; }
        public string OfAudit { get; set; }
        public bool? SignWit { get; set; }
        public int? UseridWit { get; set; }
        public string UsernameWit { get; set; }
        public string DesignationWit { get; set; }
        public DateTime? DateWit { get; set; }
        public string OfWit { get; set; }
        public string Remarks { get; set; }
        public bool ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }

        public virtual ICollection<FormQa1EqVh> FormQa1EqVh { get; set; }
        public virtual ICollection<FormQa1GCdDTO> FormQa1Gc { get; set; }
        public virtual ICollection<FormQa1GenDTO> FormQa1Gen { get; set; }
        public virtual ICollection<FormQa1LabDTO> FormQa1Lab { get; set; }
        public virtual ICollection<FormQa1MatDTO> FormQa1Mat { get; set; }
        public virtual ICollection<FormQa1SscDTO> FormQa1Ssc { get; set; }
        public virtual ICollection<FormQa1TesDTO> FormQa1Tes { get; set; }
        public virtual ICollection<FormQa1WcqDTO> FormQa1Wcq { get; set; }
        public virtual ICollection<FormQa1WeDTO> FormQa1We { get; set; }
    }

    public class FormQa1EqVh
    {
        //Fqa1ev
        public int PkRefNo { get; set; }
        public int? Fqa1hPkRefNo { get; set; }
        public string Type { get; set; }
        public string VNo { get; set; }
        public decimal? Capacity { get; set; }
        public int? Unit { get; set; }
        public string Condition { get; set; }
        public string LabourRemark { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }

        public virtual FormQa1HeaderDTO FormQa1Hdr { get; set; }
    }

    public class FormQa1GCdDTO
    {
        //Fqa1gc
        public int PkRefNo { get; set; }
        public int? Fqa1hPkRefNo { get; set; }
        public string QualityRating { get; set; }
        public string QualityRemarks { get; set; }
        public string UsRatingReason { get; set; }
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
        public string Labour { get; set; }
        public int? LabourOnSite { get; set; }
        public int? LabourOnLeave { get; set; }
        public bool? LabourPerformanceStd { get; set; }
        public string LabourRemark { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }

        public virtual FormQa1HeaderDTO FormQa1Hdr { get; set; }
    }

    public class FormQa1MatDTO
    {
        //Fqa1m
        public int PkRefNo { get; set; }
        public int? Fqa1hPkRefNo { get; set; }
        public string Type { get; set; }
        public decimal? Qty { get; set; }
        public int? Unit { get; set; }
        public bool? Spec { get; set; }
        public string Remark { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public virtual FormQa1HeaderDTO FormQa1Hdr { get; set; }
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
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }

        public virtual FormQa1HeaderDTO FormQa1Hdr { get; set; }
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
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }

        public virtual FormQa1HeaderDTO FormQa1Hdr { get; set; }
    }


    public class FormQa1WcqDTO 
    {
        //Fqa1wcq
        public int PkRefNo { get; set; }
        public int? Fqa1hPkRefNo { get; set; }
        public string FlType { get; set; }
        public decimal? Fl { get; set; }
        public int? FlUnit { get; set; }
        public string FlRemark { get; set; }
        public string JType { get; set; }
        public string JRemark { get; set; }
        public string SrType { get; set; }
        public string SrRemark { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }

        public virtual FormQa1HeaderDTO FormQa1Hdr { get; set; }
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
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }

        public virtual FormQa1HeaderDTO FormQa1Hdr { get; set; }
    }
}
