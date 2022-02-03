using System;
namespace RAMMS.DTO.RequestBO
{
    public class S2HeaderSearchRequestDTO
    {
        public string Rmu { get; set; }
        public int? ActivityCode { get; set; }
        public int? Year { get; set; }
        public int? Quarter { get; set; }
        public string SmartInput { get; set; }
    }
}
