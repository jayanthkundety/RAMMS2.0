using AutoMapper;
using RAMMS.Common;
using RAMMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.ResponseBO
{
    public class FormFCDTO
    {
        public int PkRefNo { get; set; }
        public string DivCode { get; set; }
        public string Dist { get; set; }
        public string RmuName { get; set; }
        public int? RoadId { get; set; }
        public string RoadCode { get; set; }
        public string RoadName { get; set; }
        public decimal? RoadLength { get; set; }
        public int? YearOfInsp { get; set; }
        public int? UserIdInspBy { get; set; }
        public string UserNameInspBy { get; set; }
        public string UserDesignationInspBy { get; set; }
        public DateTime? DtInspBy { get; set; }
        public string SignpathInspBy { get; set; }
        public string FormRefId { get; set; }
        public int? CrewLeaderId { get; set; }
        public string CrewLeaderName { get; set; }
        public string Remarks { get; set; }
        public int? ModBy { get; set; }
        public DateTime? ModDt { get; set; }
        public int? CrBy { get; set; }
        public DateTime? CrDt { get; set; }
        public bool SubmitSts { get; set; }
        public bool? ActiveYn { get; set; }
        public decimal? FrmCh { get; set; }
        public decimal? ToCh { get; set; }
        public string AssetTypes { get; set; }

        public string Status { get; set; }
        public string AuditLog { get; set; }
        public IList<FormFCDetailsDTO> InsDtl { get; set; }
    }
}
