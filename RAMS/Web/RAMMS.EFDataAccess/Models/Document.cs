using System;
using System.Collections.Generic;

namespace RAMMS.EFDataAccess.Models
{
    public partial class Document
    {
        public int Pk { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
        public string DocumentNo { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedDate { get; set; }
        public string Status { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string RejectedBy { get; set; }
        public DateTime? RejectedDate { get; set; }
        public string RejectedReason { get; set; }
        public string Remarks { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
