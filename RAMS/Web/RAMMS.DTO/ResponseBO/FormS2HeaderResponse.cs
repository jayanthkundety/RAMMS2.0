using System;
namespace RAMMS.DTO.ResponseBO
{
    public class S2HeaderResponse
    {
        public int Id { get; set; }
        public string Rmu { get; set; }
        public string RmuName { get; set; }
        public string ActivityCode { get; set; }
        public int? Year { get; set; }
        public string Quarter { get; set; }
        public string ReferenceNo { get; set; }
        public string PrioritizedBy { get; set; }
        public string ScheduledBy { get; set; }
        public string SubmittedBy { get; set; }
        public string VettedBy { get; set; }
        public string AgreedBy { get; set; }
        public bool SubmitSts { get; set; }
        public string Status { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public string ProcessStatus { get; set; }
    }
}
