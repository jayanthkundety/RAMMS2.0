using System;
using System.Collections.Generic;

namespace RAMMS.Domain.Models
{
    public partial class RmIwFormW2
    {
        public RmIwFormW2()
        {
            RmIwFormW2Fecm = new HashSet<RmIwFormW2Fecm>();
            RmIwFormW2Image = new HashSet<RmIwFormW2Image>();
        }

        public int Fw2PkRefNo { get; set; }
        public string Fw2Fw1IwRefNo { get; set; }
        public int? Fw2Fw1PkRefNo { get; set; }
        public string Fw2Fw1ProjectTitle { get; set; }
        public string Fw2JkrRefNo { get; set; }
        public string Fw2SerProvRefNo { get; set; }
        public string Fw2ServProvName { get; set; }
        public DateTime? Fw2DateOfInitation { get; set; }
        public string Fw2RegionText { get; set; }
        public string Fw2RegionName { get; set; }
        public string Fw2DivText { get; set; }
        public string Fw2DivCode { get; set; }
        public string Fw2DivisonName { get; set; }
        public string Fw2RmuText { get; set; }
        public string Fw2RmuCode { get; set; }
        public string Fw2RmuName { get; set; }
        public string Fw2SecCode { get; set; }
        public string Fw2Attn { get; set; }
        public string Fw2Cc { get; set; }
        public string Fw2RoadCode { get; set; }
        public string Fw2RoadName { get; set; }
        public int? Fw2Ch { get; set; }
        public int? Fw2ChDeci { get; set; }
        public DateTime? Fw2DtCommence { get; set; }
        public DateTime? Fw2DtCompl { get; set; }
        public decimal? Fw2IwDuration { get; set; }
        public string Fw2Remarks { get; set; }
        public string Fw2DetailsOfWorks { get; set; }
        public decimal? Fw2EstCostAmt { get; set; }
        public int? Fw2UseridIssu { get; set; }
        public bool? Fw2SignIssu { get; set; }
        public string Fw2UsernameIssu { get; set; }
        public string Fw2DesignationIssu { get; set; }
        public DateTime? Fw2DtIssu { get; set; }
        public string Fw2OfficeIssu { get; set; }
        public bool? Fw2SignReq { get; set; }
        public int? Fw2UseridReq { get; set; }
        public string Fw2UsernameReq { get; set; }
        public string Fw2DesignationReq { get; set; }
        public string Fw2OfficeReq { get; set; }
        public DateTime? Fw2DtReq { get; set; }
        public int? Fw2ModBy { get; set; }
        public DateTime? Fw2ModDt { get; set; }
        public int? Fw2CrBy { get; set; }
        public DateTime? Fw2CrDt { get; set; }
        public bool Fw2SubmitSts { get; set; }
        public bool? Fw2ActiveYn { get; set; }
        public string Fw2Status { get; set; }
        public string Fw2AuditLog { get; set; }

        public virtual RmIwFormW1 Fw2Fw1PkRefNoNavigation { get; set; }
        public virtual ICollection<RmIwFormW2Fecm> RmIwFormW2Fecm { get; set; }
        public virtual ICollection<RmIwFormW2Image> RmIwFormW2Image { get; set; }
    }
}
