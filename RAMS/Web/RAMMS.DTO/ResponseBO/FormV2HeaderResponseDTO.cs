using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public partial class FormV2HeaderResponseDTO
    {

        public int PkRefNo { get; set; }
        public int? ContNo { get; set; }
        public string Rmu { get; set; }
        public string SecCode { get; set; }
        public string RefId { get; set; }
        public int? Crew { get; set; }
        public string Crewname { get; set; }
        public int? ActCode { get; set; }
        public DateTime? Dt { get; set; }
        public bool? SignSch { get; set; }
        public string UseridSch { get; set; }
        public string UsernameSch { get; set; }
        public string DesignationSch { get; set; }
        public DateTime? DtSch { get; set; }
        public bool? SignAgr { get; set; }
        public string UseridAgr { get; set; }
        public string UsernameAgr { get; set; }
        public string DesignationAgr { get; set; }
        public DateTime? DtAgr { get; set; }
        public bool? SignAck { get; set; }
        public string UseridAck { get; set; }
        public string UsernameAck { get; set; }
        public string DesignationAck { get; set; }
        public DateTime? DtAck { get; set; }
        public string ServiceProvider { get; set; }
        public string Verifier { get; set; }
        public string Facilitator { get; set; }
        public string Remarks { get; set; }
        public string ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public string CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool? SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }

        public virtual List<FormV2EquipDetailsResponseDTO> FormV2Eqp { get; set; }
        public virtual List<FormV2LabourDetailsResponseDTO> FormV2Lab { get; set; }
        public virtual List<FormV2MaterialDetailsResponseDTO> FormV2Mat { get; set; }
    }
}
