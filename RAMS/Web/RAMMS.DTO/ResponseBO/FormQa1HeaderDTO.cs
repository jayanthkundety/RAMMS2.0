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
        public string SecCode { get; set; }
        public string SecName { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public string RefId { get; set; }
        public int? Crew { get; set; }
        public string ActCode { get; set; }
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

        public virtual ICollection<FormQa1GCdDTO> FormQa1Gc { get; set; }
        public virtual ICollection<FormQa1GenDTO> FormQa1Gen { get; set; }
        public virtual ICollection<FormQa1LabDTO> FormQa1Lab { get; set; }
        public virtual ICollection<FormQa1MatDTO> FormQa1Mat { get; set; }
        public virtual ICollection<FormQa1SscDTO> FormQa1Ssc { get; set; }
        public virtual ICollection<FormQa1TesDTO> FormQa1Tes { get; set; }
        public virtual ICollection<FormQa1WcqDTO> FormQa1Wcq { get; set; }
        public virtual ICollection<FormQa1WeDTO> FormQa1We { get; set; }
    }

    public class FormQa1GCdDTO
    {    }
    public class FormQa1GenDTO
    {    }

    public class FormQa1LabDTO
    {    }

    public class FormQa1MatDTO
    {    }

    public class FormQa1SscDTO{}

    public class FormQa1TesDTO { }


    public class FormQa1WcqDTO { }
    public class FormQa1WeDTO { }
}
