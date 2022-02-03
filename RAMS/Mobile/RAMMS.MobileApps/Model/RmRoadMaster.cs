using System;
using System.Collections.Generic;

namespace RAMMS.MobileApps
{
    public partial class RmRoadMaster
    {
        public int RdmPkRefNo { get; set; }
        public string RdmFeatureId { get; set; }
        public string RdmDivCode { get; set; }
        public string RdmRmuCode { get; set; }
        public string RdmSecName { get; set; }
        public string RdmRdCatgName { get; set; }
        public string RdmRdCatgCode { get; set; }
        public string RdmRdCode { get; set; }
        public string RdmRdName { get; set; }
        public string RdmFrmLoc { get; set; }
        public string RdmToLoc { get; set; }
        public int? RdmFrmCh { get; set; }
        public int? RdmFrmChDeci { get; set; }
        public int? RdmToCh { get; set; }
        public int? RdmToChDeci { get; set; }
        public decimal? RdmLengthPaved { get; set; }
        public decimal? RdmLengthUnpaved { get; set; }
        public string RdmOwner { get; set; }
        public string RdmModBy { get; set; }
        public DateTime? RdmModDt { get; set; }
        public string RdmCrBy { get; set; }
        public DateTime? RdmCrDt { get; set; }
        public bool? RdmActiveYn { get; set; }
        public int? RdmSecCode { get; set; }
    }
}
