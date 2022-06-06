using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormF3ResponseDTO
    {


        public int PkRefNo { get; set; }
        public int? G1hPkRefNo { get; set; }
        public string PkRefId { get; set; }
        public string DivCode { get; set; }
        public string Dist { get; set; }
        public string RmuCode { get; set; }
        public string RdCode { get; set; }
        public string SecCode { get; set; }
        public string SecName { get; set; }
        public string RdName { get; set; }
        public string CrewSup { get; set; }
        public string CrewName { get; set; }
        public string AssetId { get; set; }
        public int? InspectedYear { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? InspectedDate { get; set; }
        public int? InspectedBy { get; set; }
        public string InspectedName { get; set; }
        public bool? InspectedBySign { get; set; }
        public decimal? RoadLength { get; set; }
        public int? ConditionI { get; set; }
        public int? ConditionIi { get; set; }
        public int? ConditionIii { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool ActiveYn { get; set; }
        public string Status { get; set; }
        public string AuditLog { get; set; }


         

    }
}
