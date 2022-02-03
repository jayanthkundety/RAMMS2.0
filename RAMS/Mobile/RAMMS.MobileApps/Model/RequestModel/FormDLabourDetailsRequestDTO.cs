
using System;
using System.Collections.Generic;


namespace RAMMS.DTO.RequestBO
{
    public class FormDLabourDetailsRequestDTO
    {

        public int No { get; set; }


        public int? FdmdFdhPkRefNo { get; set; }


        public int? SerialNo { get; set; }


        public string LabourCode { get; set; }

        public string LabourDesc { get; set; }


        public int? Quantity { get; set; }


        public string Unit { get; set; }


        public string ModifiedBy { get; set; }


        public DateTime? ModifiedDate { get; set; }


        public string CreatedBy { get; set; }


        public DateTime? CreatedDate { get; set; }


        public bool SubmitStatus { get; set; }


        public bool? ActiveYn { get; set; }


        public string CodeDesc { get; set; }

        public FormDHeaderRequestDTO FormldFDHRefNoNavigation { get; set; }

        /// <summary>
        /// /
        /// </summary>


    }
}
