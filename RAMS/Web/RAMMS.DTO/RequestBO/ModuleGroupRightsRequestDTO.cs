using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class ModuleGroupRightsRequestDTO
    {
        [MapTo("MgrPkId")] public int MgrPkid { get; set; }
        [MapTo("ModPkId")] public int? ModPkid { get; set; }
        [MapTo("UgpkId")] public int? Ugpkid { get; set; }
        [MapTo("DvIsView")] public bool? DvIsview { get; set; }
        [MapTo("DvIsModify")] public bool? DvIsmodify { get; set; }
        [MapTo("DvIsDelete")] public bool? DvIsdelete { get; set; }
        [MapTo("PcIsView")] public bool? PcIsview { get; set; }
        [MapTo("PcIsModify")] public bool? PcIsmodify { get; set; }
        [MapTo("PcIsDelete")] public bool? PcIsdelete { get; set; }
        [MapTo("MgrCreatedBy")] public string MgrCreatedby { get; set; }
        [MapTo("MgrCreatedOn")] public DateTime MgrCreatedon { get; set; }
        [MapTo("MgrModifiedBy")] public string MgrModifiedby { get; set; }
        [MapTo("MgrModifiedOn")] public DateTime MgrModifiedon { get; set; }
        [MapTo("DvIsAdd")] public bool? DvIsadd { get; set; }
        [MapTo("PcIsAdd")] public bool? PcIsadd { get; set; }
        public string Group { get; set; }
        public string Module { get; set; }
    }
}
