using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmIwFormW2
    {
        public RmIwFormW2()
        {
            RmIwFormW2Fcem = new HashSet<RmIwFormW2Fcem>();
            RmIwFormW2Image = new HashSet<RmIwFormW2Image>();
        }

        public int Fw2PkRefNo { get; set; }
        public string Fw2Fw1IwRefNo { get; set; }
        public int? Fw2Fw1RefNo { get; set; }
        public string Fw2JkrRefNo { get; set; }
        public string Fw2SerProviderRefNo { get; set; }
        public string Fw2ServiceProvider { get; set; }
        public DateTime? Fw2DtInitation { get; set; }
        public string Fw2Region { get; set; }
        public string Fw2RegionName { get; set; }
        public string Fw2Division { get; set; }
        public string Fw2DivisonName { get; set; }
        public string Fw2Rmu { get; set; }
        public string Fw2RmuName { get; set; }
        public string Fw2Attn { get; set; }
        public string Fw2Cc { get; set; }
        public string Fw2RoadCode { get; set; }
        public string Fw2RoadName { get; set; }
        public int? Fw2ChKm { get; set; }
        public int? Fw2ChM { get; set; }
        public string Fw2TitleOfInstructWork { get; set; }
        public DateTime? Fw2DtCommence { get; set; }
        public DateTime? Fw2DtCompl { get; set; }
        public double? Fw2IwDuration { get; set; }
        public string Fw2Remarks { get; set; }
        public string Fw2DetailsOfWorks { get; set; }
        public double? Fw2CeilingEstCost { get; set; }
        public int? Fw2UseridIssu { get; set; }
        public bool? Fw2SignIssu { get; set; }
        public string Fw2UsernameIssu { get; set; }
        public DateTime? Fw2DtIssu { get; set; }
        public bool? Fw2SignReq { get; set; }
        public int? Fw2UseridReq { get; set; }
        public string Fw2UsernameReq { get; set; }
        public DateTime? Fw2DtReq { get; set; }
        public int? Fw2ModBy { get; set; }
        public DateTime? Fw2ModDt { get; set; }
        public int? Fw2CrBy { get; set; }
        public DateTime? Fw2CrDt { get; set; }
        public bool? Fw2SubmitSts { get; set; }
        public bool? Fw2ActiveYn { get; set; }
        public string Fw2Status { get; set; }
        public string Fw2AuditLog { get; set; }

        public virtual RmIwFormW1 Fw2Fw1RefNoNavigation { get; set; }
        public virtual ICollection<RmIwFormW2Fcem> RmIwFormW2Fcem { get; set; }
        public virtual ICollection<RmIwFormW2Image> RmIwFormW2Image { get; set; }
    }
}
