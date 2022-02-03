using System;
using AutoMapper.Configuration.Conventions;

namespace RAMMS.DTO.RequestBO
{
    public class AuditTransactionRequestDTO
    {
        [MapTo("AltPkRefNo")] public long PkRefNo { get; set; }
        [MapTo("AltAlaPkRefNo")] public long AlaPkRefNo { get; set; }
        [MapTo("AltTransactionname")] public string Transactionname { get; set; }
        [MapTo("AltTablename")] public string Tablename { get; set; }
        [MapTo("AltTransactindetails")] public string Transactindetails { get; set; }
    }
}
