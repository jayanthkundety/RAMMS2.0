using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class InvCulvertContractor
    {
        public int Pk { get; set; }
        public int InvCulvertConstructionPk { get; set; }
        public string ContractNumber { get; set; }
        public string ContractTitle { get; set; }
        public string VendorNumber { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ContactPerson { get; set; }
        public string Department { get; set; }
        public string Country { get; set; }
        public string Designation { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string FaxNumber { get; set; }
        public string CableTelexNumber { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
