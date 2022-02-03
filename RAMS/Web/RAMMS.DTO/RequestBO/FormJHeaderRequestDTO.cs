using AutoMapper.Configuration.Conventions;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.RequestBO
{
    public class FormJHeaderRequestDTO
    {
        public FormJHeaderRequestDTO()
        {
            FormJDetails = new List<FormJDetailsRequestDTO>();
        }

        public string SmartInputValue { get; set; }

        [MapTo("FjhPkRefNo")]
        public int No { get; set; }

        [MapTo("FjhRefId")]
        public string Id { get; set; }

        [MapTo("FjhRoadCode")]
        public string RoadCode { get; set; }

        [MapTo("FjhRmu")]
        public string Rmu { get; set; }

        [MapTo("FjhRoadName")]
        public string RoadName { get; set; }

        [MapTo("FjhContNo")]
        public string ContNo { get; set; }

        [MapTo("FjhAssetGroupCode")]
        public string AssetGroupCode { get; set; }

        [MapTo("FjhMonth")]
        public int? Month { get; set; }


        [MapTo("FjhYear")]
        public int? Year { get; set; }

        [MapTo("FjhRemarks")]
        public string Remarks { get; set; }

        [MapTo("FjhSignPrp")]
        public string SignPrp { get; set; }

        [MapTo("FjhUseridPrp")]
        public int? UseridPrp { get; set; }

        [MapTo("FjhUsernamePrp")]
        public string UsernamePrp { get; set; }

        [MapTo("FjhDesignationPrp")]
        public string DesignationPrp { get; set; }

        [MapTo("FjhDtPrp")]
        public DateTime? DtPrp { get; set; }

        [MapTo("FjhSignVer")]
        public string SignVer { get; set; }

        [MapTo("FjhUseridVer")]
        public int? UseridVer { get; set; }

        [MapTo("FjhUsernameVer")]
        public string UsernameVer { get; set; }

        [MapTo("FjhDesignationVer")]
        public string DesignationVer { get; set; }

        [MapTo("FjhSignVet")]
        public string SignVet{ get; set; }

        [MapTo("FjhDtVer")]
        public DateTime? VerifiedDt { get; set; }
        [MapTo("FjhUsernameVet")]
        public string UsernameVet { get; set; }
        [MapTo("FjhUseridVet")]
        public int? UseridVet { get; set; }
        [MapTo("FjhDesignationVet")]
        public string DesignationVet { get; set; }

        [MapTo("FjhDtVet")]
        public DateTime? AuditedDt { get; set; }

        [MapTo("FjhModBy")]
        public string ModBy { get; set; }

        [MapTo("FjhModDt")]
        public DateTime? ModDt { get; set; }

        [MapTo("FjhCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("FjhCrDt")]
        public DateTime? CreatedDt { get; set; }

        [MapTo("FjhSubmitSts")]
        public bool SubmitSts { get; set; }

        [MapTo("FjhActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo("FjhSection")]
        public string section { get; set; }
        [MapTo("FjhStatus")]
        public string Status { get; set; }
        [MapTo("FjhAuditLog")]
        public string AuditLog { get; set; }
        public List<FormJDetailsRequestDTO> FormJDetails { get; set; }

        public string sortOrder { get; set; }
        public string currentFilter { get; set; }
        public string searchString { get; set; }
        public int? Page_No { get; set; }
        public int? pageSize { get; set; }

    }
}
