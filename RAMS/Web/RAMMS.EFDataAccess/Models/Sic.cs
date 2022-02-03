using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class Sic
    {
        public Sic()
        {
            SicFixedL1 = new HashSet<SicFixedL1>();
            SicLandscape = new HashSet<SicLandscape>();
            SicMobile = new HashSet<SicMobile>();
            SicPhoto = new HashSet<SicPhoto>();
            SicVehicle = new HashSet<SicVehicle>();
        }

        public int Pk { get; set; }
        public string Shift { get; set; }
        public string FormType { get; set; }
        public string Status { get; set; }
        public DateTime? UploadedDt { get; set; }
        public string UploadedBy { get; set; }
        public double? EditedAtLat { get; set; }
        public double? EditedAtLng { get; set; }
        public string LastCalculatedBy { get; set; }
        public DateTime? LastCalculatedDt { get; set; }
        public string InspectedByName { get; set; }
        public string InspectedByPosition { get; set; }
        public string InspectedByCompany { get; set; }
        public string InspectedBySignature { get; set; }
        public string ConfirmedByName { get; set; }
        public string ConfirmedByPosition { get; set; }
        public string ConfirmedByCompany { get; set; }
        public string ConfirmedBySignature { get; set; }
        public string VehicleWorking { get; set; }
        public string VehicleRemark { get; set; }
        public DateTime? EditStartDt { get; set; }
        public DateTime? EditEndDt { get; set; }
        public double? ActualCrew { get; set; }

        public virtual McsActivityScorecard PkNavigation { get; set; }
        public virtual SicFertilizer SicFertilizer { get; set; }
        public virtual ICollection<SicFixedL1> SicFixedL1 { get; set; }
        public virtual ICollection<SicLandscape> SicLandscape { get; set; }
        public virtual ICollection<SicMobile> SicMobile { get; set; }
        public virtual ICollection<SicPhoto> SicPhoto { get; set; }
        public virtual ICollection<SicVehicle> SicVehicle { get; set; }
    }
}
