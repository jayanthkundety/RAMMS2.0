using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class Contractor
    {
        public Contractor()
        {
            Complaint = new HashSet<Complaint>();
            Emergency = new HashSet<Emergency>();
            RmSchedule = new HashSet<RmSchedule>();
            UserBak2 = new HashSet<UserBak2>();
            Work = new HashSet<Work>();
        }

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

        public virtual ICollection<Complaint> Complaint { get; set; }
        public virtual ICollection<Emergency> Emergency { get; set; }
        public virtual ICollection<RmSchedule> RmSchedule { get; set; }
        public virtual ICollection<UserBak2> UserBak2 { get; set; }
        public virtual ICollection<Work> Work { get; set; }
    }
}
