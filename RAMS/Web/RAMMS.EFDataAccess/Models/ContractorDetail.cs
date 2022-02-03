using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class ContractorDetail
    {
        public int Pk { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegNo { get; set; }
        public string PersonInCharge { get; set; }
        public string Email { get; set; }
        public string ContactNo { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTime? AppointDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Code { get; set; }
        public int? TotalTeam { get; set; }
    }
}
