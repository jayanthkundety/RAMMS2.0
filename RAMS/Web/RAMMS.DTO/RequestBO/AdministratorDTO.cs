using System;
using System.Collections.Generic;
using System.Text;

namespace RAMMS.DTO.RequestBO
{
    public class AdministratorDTO
    {
        public int Id { get; set; }
        public string DivCode { get; set; }
        public string Division { get; set; }
        public string RMUName { get; set; }
        public string RMUCode { get; set; }
        public string SectionName { get; set; }
        public string SectionCode { get; set; }
        public string PageName { get; set; }
        public string AssestGroupName { get; set; }
        public string AssestGroupCode { get; set; }
        public string AssestTypeDesc { get; set; }
        public string AssestTypeCode { get; set; }
        public string AssestTypeContractCode { get; set; }
        public string FormNo { get; set; }
    }
}
