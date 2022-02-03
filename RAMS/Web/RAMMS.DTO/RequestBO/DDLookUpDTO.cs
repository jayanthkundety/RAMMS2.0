using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.RequestBO
{
    public class DDLookUpDTO
    {
        [MapTo("DdlPkRefNo")]
        public int No { get; set; }

        [MapTo("DdlType")]
        public string Type { get; set; }

        [MapTo("DdlTypeCode")]
        public string TypeCode { get; set; }

        [MapTo("DdlTypeDesc")]
        public string TypeDesc { get; set; }

        [MapTo("DdlTypeValue")]
        public string TypeValue { get; set; }

        [MapTo("DdlTypeRemarks")]
        public string TypeRemarks { get; set; }

        [MapTo("DdlModBy")]
        public string ModifiedBy { get; set; }

        [MapTo("DdlModBy")]
        public DateTime? ModifiedDate { get; set; }

        [MapTo("DdlCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("DdlCrDt")]
        public DateTime? CreateDate { get; set; }

        [MapTo("DdlActiveYn")]
        public bool? Active { get; set; }
    }
}
