using System;
using System.Collections.Generic;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class FormS2DetailRequestDto
    {
        public FormS2DetailRequestDto()
        {
        }
        [MapTo("FsiidPkRefNo")]
        public int PkRefNo { get; set; }
        [MapTo("FsiidFsiihPkRefNo")]
        public int? HeaderPkRefNo { get; set; }
        [MapTo("FsiidRefId")]
        public string RefId { get; set; }
        [MapTo("FsiidRoadId")]
        public int? RoadId { get; set; }

        [MapTo("FsiidRoadName")]
        public string RoadName { get; set; }

        [MapTo("FsiidRoadPavedLength")]
        public decimal? Pavedlength { get; set; }

        [MapTo("FsiidRoadUnPavedLength")]
        public decimal? UnPavedlength { get; set; }

        [MapTo("FsiidRdLocSeq")]
        public string RdLocSeq { get; set; }
        [MapTo("FsiidCil")]
        public int? Cil { get; set; }
        [MapTo("FsiidPriorityI")]
        public int? PriorityI { get; set; }
        [MapTo("FsiidPriorityIi")]
        public int? PriorityIi { get; set; }
        [MapTo("FsiidPriority")]
        public int? Priority { get; set; }
        [MapTo("FsiidAdp")]
        public int? Adp { get; set; }
        [MapTo("FsiidCrwDaysReq")]
        public int? CrwDaysReq { get; set; }
        [MapTo("FsiidCrwAllwcdQuar")]
        public int? CrwAllwcdQuar { get; set; }
        [MapTo("FsiidTargetPercent")]
        public decimal? TargetPercent { get; set; }
        [MapTo("FsiidRemarks")]
        public string Remarks { get; set; }
        [MapTo("FsiidModBy")]
        public int? ModBy { get; set; }
        [MapTo("FsiidModDt")]
        public DateTime? ModDt { get; set; }
        [MapTo("FsiidCrBy")]
        public int? CrBy { get; set; }
        [MapTo("FsiidCrDt")]
        public DateTime? CrDt { get; set; }
        [MapTo("FsiidSubmitSts")]
        public bool SubmitSts { get; set; }
        [MapTo("FsiidActiveYn")]
        public bool? ActiveYn { get; set; }
        public List<int> WeekDetail { get; set; }

        [MapTo("FsiidWorkQty")]
        public string WorkQty { get; set; }
    }
}
