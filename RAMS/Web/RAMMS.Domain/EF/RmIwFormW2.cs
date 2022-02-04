using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmIwFormW2
    {
        public int Fw2PkRefNo { get; set; }
        public string Fw1RefNo { get; set; }
        public string Fw2JkrRefNo { get; set; }
        public string Fw2SerProviderRefNo { get; set; }
        public DateTime? Fw2DateOfInitation { get; set; }
        public string Fw2Region { get; set; }
        public string Fw2Division { get; set; }
        public string Fw2Rmu { get; set; }
        public int? Fw2Attn { get; set; }
        public string Fw2ServiceProvider { get; set; }
        public string Fw2Cc { get; set; }
        public string Fw2RoadCode { get; set; }
        public string Fw2RoadName { get; set; }
        public int? Fw2FrmCh { get; set; }
        public int? Fw2ToCh { get; set; }
        public string Fw2TitleOfInstructWork { get; set; }
        public DateTime? Fw2DateOfCommencement { get; set; }
        public DateTime? Fw2DateOfComplition { get; set; }
        public decimal? Fw2InstructWorkDuration { get; set; }
        public string Fw2Remarks { get; set; }
        public string Fw2DetailsOfWorks { get; set; }
        public decimal? Fw2CeilingEstCost { get; set; }
        public bool? Fw2IssuedSignature { get; set; }
        public string Fw2IssuedName { get; set; }
        public DateTime? Fw2IssuedDate { get; set; }
        public bool? Fw2ReceivedSignature { get; set; }
        public string Fw2ReceivedName { get; set; }
        public DateTime? Fw2ReceivedDate { get; set; }
        public int? Fw2ModBy { get; set; }
        public DateTime? Fw2ModDt { get; set; }
        public int? Fw2CrBy { get; set; }
        public DateTime? Fw2CrDt { get; set; }
        public bool? Fw2SubmitSts { get; set; }
        public bool? Fw2ActiveYn { get; set; }
    }
}
