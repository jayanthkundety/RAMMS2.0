using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class FormS2HeaderRequestDto
    {
        public FormS2HeaderRequestDto()
        {
        }

        [MapTo("FsiihPkRefNo")]
        public int PkRefNo { get; set; }
        [MapTo("FsiihRefId")]
        public string RefId { get; set; }
        [MapTo("FsiihYear")]
        public int? Year { get; set; }
        [MapTo("FsiihQuaterId")]
        public int? QuaterId { get; set; }
        [MapTo("FsiihContNo")]
        public string ContNo { get; set; }
        [MapTo("FsiihRmu")]
        public string Rmu { get; set; }
        [MapTo("FsiihActId")]
        public int? ActId { get; set; }
        [MapTo("FsiihUseridPrioritised")]
        public int? UseridPrioritised { get; set; }
        [MapTo("FsiihDtPrioritised")]
        public DateTime? DtPrioritised { get; set; }
        [MapTo("FsiihUseridSchld")]
        public int? UseridSchld { get; set; }
        [MapTo("FsiihDtSchld")]
        public DateTime? DtSchld { get; set; }
        [MapTo("FsiihUseridSub")]
        public int? UseridSub { get; set; }
        [MapTo("FsiihDtSub")]
        public DateTime? DtSub { get; set; }
        [MapTo("FsiihUseridVet")]
        public int? UseridVet { get; set; }
        [MapTo("FsiihDtVet")]
        public DateTime? DtVet { get; set; }
        [MapTo("FsiihUseridAgrd")]
        public int? UseridAgrd { get; set; }
        [MapTo("FsiihDtAgrd")]
        public DateTime? DtAgrd { get; set; }
        [MapTo("FsiihModBy")]
        public int? ModBy { get; set; }
        [MapTo("FsiihModDt")]
        public DateTime? ModDt { get; set; }
        [MapTo("FsiihCrBy")]
        public int? CrBy { get; set; }
        [MapTo("FsiihCrDt")]
        public DateTime? CrDt { get; set; }
        [MapTo("FsiihSubmitSts")]
        public bool SubmitSts { get; set; }
        [MapTo("FsiihActiveYn")]
        public bool? ActiveYn { get; set; }
        [MapTo("FsiihActCode")]
        public string ActCode { get; set; }
        [MapTo("FsiihActName")]
        public string ActName { get; set; }
        [MapTo("FsiihUserNamePrioritised")]
        public string UserNamePrioritised { get; set; }
        [MapTo("FsiihUserDesignationPrioritised")]
        public string UserDesignationPrioritised { get; set; }
        [MapTo("FsiihUserNameSchId")]
        public string UserNameSchId { get; set; }
        [MapTo("FsiihUserDesignationSchId")]
        public string UserDesignationSchId { get; set; }
        [MapTo("FsiihUserNameSub")]
        public string UserNameSub { get; set; }
        [MapTo("FsiihUserDesignationSub")]
        public string UserDesignationSub { get; set; }
        [MapTo("FsiihUserNameVet")]
        public string UserNameVet { get; set; }
        [MapTo("FsiihUserDesignationVet")]
        public string UserDesignationVet { get; set; }
        [MapTo("FsiihUserNameAgrd")]
        public string UserNameAgrd { get; set; }
        [MapTo("FsiihUserDesignationAgrd")]
        public string UserDesignationAgrd { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }
        public bool IsViewMode { get; set; }
    }
}
