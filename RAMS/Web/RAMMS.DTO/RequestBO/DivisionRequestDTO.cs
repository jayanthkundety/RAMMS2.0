using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class DivisionRequestDTO
    {
        [MapTo("DivPkRefNo")] public int PkRefNo { get; set; }
        [MapTo("DivCode")] public string Code { get; set; }
        [MapTo("DivName")] public string Name { get; set; }
        [MapTo("DivIsActive")] public bool Isactive { get; set; }
    }
}
