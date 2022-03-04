﻿using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
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
        public string Fw2Fw1ProjectTitle { get; set; }
        public string Fw2JkrRefNo { get; set; }
        public string Fw2SerProviderRefNo { get; set; }
        public string Fw2ServiceProvider { get; set; }
        public DateTime? Fw2DateOfInitation { get; set; }
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
        public int? Fw2FrmChKm { get; set; }
        public int? Fw2FrmChM { get; set; }
        public int? Fw2ToChKm { get; set; }
        public int? Fw2ToChM { get; set; }
        public string Fw2TitleOfInstructWork { get; set; }
        public DateTime? Fw2DateOfCommencement { get; set; }
        public DateTime? Fw2DateOfCompletion { get; set; }
        public double? Fw2InstructWorkDuration { get; set; }
        public string Fw2Remarks { get; set; }
        public string Fw2DetailsOfWorks { get; set; }
        public double? Fw2CeilingEstCost { get; set; }
        public int? Fw2IssuedBy { get; set; }
        public bool? Fw2IssuedSign { get; set; }
        public string Fw2IssuedName { get; set; }
        public DateTime? Fw2IssuedDate { get; set; }
        public bool? Fw2RequestedSign { get; set; }
        public int? Fw2RequestedBy { get; set; }
        public string Fw2RequestedName { get; set; }
        public DateTime? Fw2RequestedDate { get; set; }
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
