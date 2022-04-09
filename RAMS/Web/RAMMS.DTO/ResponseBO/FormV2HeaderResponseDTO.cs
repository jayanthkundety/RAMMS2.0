using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public partial class FormV2HeaderResponseDTO
    {

        public int PkRefNo { get; set; }
        public int? ContNo { get; set; }
        public string Rmu { get; set; }
        public string SecCode { get; set; }

        public string SecName { get; set; }

        public string DivCode { get; set; }
        public string DivName { get; set; }

        public string RefId { get; set; }
        public int? Crew { get; set; }
        public string Crewname { get; set; }
        public string ActCode { get; set; }

        public string ActName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? Dt { get; set; }
        public bool? SignSch { get; set; }
        public int? UseridSch { get; set; }
        public string UsernameSch { get; set; }
        public string DesignationSch { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DtSch { get; set; }
        public bool? SignAgr { get; set; }
        public int? UseridAgr { get; set; }
        public string UsernameAgr { get; set; }
        public string DesignationAgr { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DtAgr { get; set; }
        public bool? SignAck { get; set; }
        public int? UseridAck { get; set; }
        public string UsernameAck { get; set; }
        public string DesignationAck { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DtAck { get; set; }
        public string ServiceProvider { get; set; }
        public string Verifier { get; set; }
        public string Facilitator { get; set; }
        public string Remarks { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }

        public virtual List<FormV2EquipDetailsResponseDTO> FormV2Eqp { get; set; }
        public virtual List<FormV2LabourDetailsResponseDTO> FormV2Lab { get; set; }
        public virtual List<FormV2MaterialDetailsResponseDTO> FormV2Mat { get; set; }
    }
}
