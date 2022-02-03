using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class AuditActionRequestDTO
    {
        [MapTo("AlaPkRefNo")] public long PkRefNo { get; set; }
        [MapTo("AlaRequestip")] public string Requestip { get; set; }
        [MapTo("AlaRequester")] public string Requester { get; set; }
        [MapTo("AlaActionname")] public string Actionname { get; set; }
        [MapTo("AlaCrDt")] public DateTime CrDt { get; set; }
        [MapTo("AlaCrBy")] public int? CrBy { get; set; }
    }
}
