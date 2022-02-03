using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class FormF2HeaderRequestDTO
    {
        [MapTo("FgrihPkRefNo")] public int PkRefNo { get; set; }
        [MapTo("FgrihDivCode")] public string DivCode { get; set; }
        [MapTo("FgrihDist")] public string Dist { get; set; }
        [MapTo("FgrihRoadId")] public int? RoadId { get; set; }
        [MapTo("FgrihRoadCode")] public string RoadCode { get; set; }
        [MapTo("FgrihRoadName")] public string RoadName { get; set; }
        [MapTo("FgrihRoadLength")] public decimal? RoadLength { get; set; }
        [MapTo("FgrihYearOfInsp")] public int? YearOfInsp { get; set; }
        [MapTo("FgrihDtOfInsp")] public DateTime? DtOfInsp { get; set; }
        [MapTo("FgrihUserIdInspBy")] public int? UserIdInspBy { get; set; }
        [MapTo("FgrihUserNameInspBy")] public string UserNameInspBy { get; set; }
        [MapTo("FgrihUserDesignationInspBy")] public string UserDesignationInspBy { get; set; }
        [MapTo("FgrihDtInspBy")] public DateTime? DtInspBy { get; set; }
        [MapTo("FgrihSignpathInspBy")] public string SignpathInspBy { get; set; }
        [MapTo("FgrihFormRefId")] public string FormRefId { get; set; }
        [MapTo("FgrihCrewLeaderId")] public int? CrewLeaderId { get; set; }
        [MapTo("FgrihCrewLeaderName")] public string CrewLeaderName { get; set; }
        [MapTo("FgrihModBy")] public int? ModBy { get; set; }
        [MapTo("FgrihModDt")] public DateTime? ModDt { get; set; }
        [MapTo("FgrihCrBy")] public int? CrBy { get; set; }
        [MapTo("FgrihCrDt")] public DateTime? CrDt { get; set; }
        [MapTo("FgrihSubmitSts")] public bool SubmitSts { get; set; }
        [MapTo("FgrihActiveYn")] public bool ActiveYn { get; set; }
        [MapTo("FgrihStatus")]  public string Status { get; set; }
        [MapTo("FgrihAuditLog")] public string AuditLog { get; set; }
        public bool IsViewMode { get; set; }
        public int? SectionCode { get; set; }
        public string SectionName { get; set; }
        public string RmuName { get; set; }
        public string RmuCode { get; set; }
    }
}