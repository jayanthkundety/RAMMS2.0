using System;
using System.Collections.Generic;


namespace RAMMS.DTO.RequestBO
{
    public class FormDMaterialDetailsRequestDTO
    {
        
        public int No { get; set; }

        public int? FdmdFdhPkRefNo { get; set; }

        public int? SerialNo { get; set; }

        public string MaterialCode { get; set; }

        public string MaterialDesc { get; set; }

        public decimal? Quantity { get; set; }

        public string Unit { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }

        public bool SubmitStatus { get; set; }

        public bool? ActiveYn { get; set; }

        public string CodeDesc { get; set; }

        public FormDHeaderRequestDTO FormDmdFDHRefNoNavigation { get; set; }

        /// <summary>
        /// /
        /// </summary>


    }
}
