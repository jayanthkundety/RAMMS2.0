using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class EpiNod
    {
        public int Pk { get; set; }
        public int NodPk { get; set; }
        public string MharefNo { get; set; }
        public int EpiMaintenanceChecklistPk { get; set; }
        public int EpiTypeOfDefectPk { get; set; }
        public int DefectRating { get; set; }
        public int Source { get; set; }
        public int Status { get; set; }
        public string ApprovedRejectedBy { get; set; }
        public string MhaassetId { get; set; }
        public string MhaassetRefNo { get; set; }
        public int? EpiMhaassetMainCategoryPk { get; set; }
        public int? EpiMhaassetSubCategoryPk { get; set; }
        public string DescriptionOfDefect { get; set; }
        public int? EpiInitialPhotoPk { get; set; }
        public int? EpiFinalPhotoPk { get; set; }
        public DateTime? InspectionDate { get; set; }
        public string InspectedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ExpectedCompletionDate { get; set; }
        public DateTime? CloseDate { get; set; }
        public DateTime? RectificationDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime ComplaintDate { get; set; }
    }
}
