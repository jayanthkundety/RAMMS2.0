using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class FormFSHeaderRequestDTO
    {
        public int locchFromKm;

        [MapTo("FshPkRefNo")] public int PkRefNo { get; set; }
        [MapTo("FshDivCode")] public string DivCode { get; set; }
        [MapTo("FshDist")] public string Dist { get; set; }
        [MapTo("FshRmuName")] public string RmuName { get; set; }
        [MapTo("FshRoadId")] public int? RoadId { get; set; }
        [MapTo("FshRoadCode")] public string RoadCode { get; set; }
        [MapTo("FshRoadName")] public string RoadName { get; set; }
        [MapTo("FshRoadLength")] public decimal? RoadLength { get; set; }
        [MapTo("FshYearOfInsp")] public int? YearOfInsp { get; set; }
        [MapTo("FshUserIdInspBy")] public int? UserIdInspBy { get; set; }
        [MapTo("FshUserNameInspBy")] public string UserNameInspBy { get; set; }
        [MapTo("FshUserDesignationInspY")] public string UserDesignationInspY { get; set; }
        [MapTo("FshDtInspBy")] public DateTime? DtInspBy { get; set; }
        [MapTo("FshSignpathInspBy")] public string SignpathInspBy { get; set; }
        [MapTo("FshFormRefId")] public string FormRefId { get; set; }
        [MapTo("FshCrewLeaderId")] public int? CrewLeaderId { get; set; }
        [MapTo("FshCrewLeaderName")] public string CrewLeaderName { get; set; }
        [MapTo("FshUserIdSmzdBy")] public int? UserIdSmzdBy { get; set; }
        [MapTo("FshUserNameSmzdBy")] public string UserNameSmzdBy { get; set; }
        [MapTo("FshUserDesignationSmzdY")] public string UserDesignationSmzdY { get; set; }
        [MapTo("FshDtSmzdBy")] public DateTime? DtSmzdBy { get; set; }
        [MapTo("FshSignpathSmzdBy")] public string SignpathSmzdBy { get; set; }
        [MapTo("FshUserIdChckdBy")] public int? UserIdChckdBy { get; set; }
        [MapTo("FshUserNameChckdBy")] public string UserNameChckdBy { get; set; }
        [MapTo("FshUserDesignationChckdBy")] public string UserDesignationChckdBy { get; set; }
        [MapTo("FshDtChckdBy")] public DateTime? DtChckdBy { get; set; }
        [MapTo("FshSignpathChckdBy")] public string SignpathChckdBy { get; set; }
        [MapTo("FshModBy")] public int? ModBy { get; set; }
        [MapTo("FshModDt")] public DateTime? ModDt { get; set; }
        [MapTo("FshCrBy")] public int? CrBy { get; set; }
        [MapTo("FshCrDt")] public DateTime? CrDt { get; set; }
        [MapTo("FshSubmitSts")] public bool SubmitSts { get; set; }
        [MapTo("FshActiveYn")] public bool ActiveYn { get; set; }
        [MapTo("FshStatus")] public string Status { get; set; }
        [MapTo("FshAuditLog")] public string AuditLog { get; set; }
        public bool IsView { get; set; }
        public string SmartSearch { get; set; }
        public string RmuCode { get; set; }
        public int SecCode { get; set; }
        public int FromYear { get; set; }
        public int ToYear { get; set; }
        public string FormType { get; set; }
        public string locchFromM { get; set; }
        public int locchToKm { get; set; }
        public string locchToM { get; set; }
        public string SecName { get; set; }
    }
}
