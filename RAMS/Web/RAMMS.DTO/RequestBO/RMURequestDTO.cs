using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class RMURequestDTO
    {
        [MapTo("RmuPkRefNo")] public int PkRefNo { get; set; }
        [MapTo("DivCode")] public string DivCode { get; set; }
        [MapTo("RmuCode")] public string Code { get; set; }
        [MapTo("RmuName")] public string Name { get; set; }
        [MapTo("RmuIsActive")] public bool IsActive { get; set; }
    }
}
