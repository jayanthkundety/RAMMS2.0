using System;
using System.Collections.Generic;

namespace RAMMS.Domain.EF
{
    public partial class RmRoadMaster
    {
        public RmRoadMaster()
        {
            RmAllassetInventory = new HashSet<RmAllassetInventory>();
            RmFormF2GrInsHdr = new HashSet<RmFormF2GrInsHdr>();
            RmFormF4InsHdr = new HashSet<RmFormF4InsHdr>();
            RmFormF5InsHdr = new HashSet<RmFormF5InsHdr>();
            RmFormFcInsHdr = new HashSet<RmFormFcInsHdr>();
            RmFormFdInsHdr = new HashSet<RmFormFdInsHdr>();
            RmFormFsInsHdr = new HashSet<RmFormFsInsHdr>();
        }

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
        public string RdmRmuName { get; set; }
        public double? RdmRdCdSort { get; set; }

        public virtual ICollection<RmAllassetInventory> RmAllassetInventory { get; set; }
        public virtual ICollection<RmFormF2GrInsHdr> RmFormF2GrInsHdr { get; set; }
        public virtual ICollection<RmFormF4InsHdr> RmFormF4InsHdr { get; set; }
        public virtual ICollection<RmFormF5InsHdr> RmFormF5InsHdr { get; set; }
        public virtual ICollection<RmFormFcInsHdr> RmFormFcInsHdr { get; set; }
        public virtual ICollection<RmFormFdInsHdr> RmFormFdInsHdr { get; set; }
        public virtual ICollection<RmFormFsInsHdr> RmFormFsInsHdr { get; set; }
    }
}
