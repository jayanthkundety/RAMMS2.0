using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormAHeaderResponseDTO
    {
        public FormAHeaderResponseDTO()
        {
            FormADetails = new List<FormADetailResponseDTO>();
        }

        [MapTo("FahPkRefNo")]
        public int No { get; set; }

        [MapTo("FahRefId")]
        public string Id { get; set; }

        [MapTo("FahRoadCode")]
        public string RoadCode { get; set; }

        [MapTo("FahRmu")]
        public string Rmu { get; set; }

        public string RmuName { get; set; }

        [MapTo("FahRoadName")]
        public string RoadName { get; set; }

        [MapTo("FahContNo")]
        public string ContNo { get; set; }

        [MapTo("FahAssetGroupCode")]
        public string AssetGroupCode { get; set; }

        [MapTo("FahMonth")]
        public int? Month { get; set; }

        [MapTo("FahYear")]
        public int? Year { get; set; }

        [MapTo("FahRemarks")]
        public string Remarks { get; set; }

        [MapTo("FahSignPrp")]
        public string SignPrp { get; set; }

        [MapTo("FahUseridPrp")]
        public int? UseridPrp { get; set; }

        [MapTo("FahUsernamePrp")]
        public string UsernamePrp { get; set; }

        [MapTo("FahDesignationPrp")]
        public string DesignationPrp { get; set; }

        [MapTo("FahDtPrp")]
        public DateTime? DtPrp { get; set; }

        [MapTo("FahSignVer")]
        public string SignVer { get; set; }

        [MapTo("FahUseridVer")]
        public int? UseridVer { get; set; }

        [MapTo("FahUsernameVer")]
        public string UsernameVer { get; set; }

        [MapTo("FahDesignationVer")]
        public string DesignationVer { get; set; }

        [MapTo("FahDtVer")]
        public DateTime? VerifiedDt { get; set; }

        [MapTo("FahModBy")]
        public string ModBy { get; set; }

        [MapTo("FahModDt")]
        public DateTime? ModDt { get; set; }

        [MapTo("FahCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("FahCrDt")]
        public DateTime? CreatedDt { get; set; }

        [MapTo("FahSubmitSts")]
        public bool SubmitSts { get; set; }

        [MapTo("FahActiveYn")]
        public bool? ActiveYn { get; set; }

        public string SectionCode { get; set; }

        [MapTo("FahSection")]
        public string section { get; set; }
        public List<FormADetailResponseDTO> FormADetails { get; set; }

        public string MonthYear { get; set; }

        public string Status { get; set; }

        public string ProcessStatus { get; set; }
    }
}
