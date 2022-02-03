using System;
using System.Collections.Generic;

namespace RAMMS.DTO.RequestBO
{
    public class FormDEquipDetailsResponseDTO
    {
       
        public int No { get; set; }

       
        public int? FormDEDFHeaderNo { get; set; }

       
        public int? SerialNo { get; set; }

       
        public string EquipmentCode { get; set; }

        public string EquipmentDesc { get; set; }

        
        public decimal? Quantity { get; set; }

        
        public string Unit { get; set; }

        
        public string ModifiedBy { get; set; }

        
        public DateTime? ModifiedDate { get; set; }

        
        public string CreatedBy { get; set; }

        
        public DateTime? CreatedDate { get; set; }

        
        public bool SubmitStatus { get; set; }

        
        public bool? ActiveYn { get; set; }

        
        public string CodeDesc { get; set; }

        public FormDHeaderRequestDTO FormDedFDHRefNoNavigation { get; set; }

        /// <summary>
        /// /
        /// </summary>


    }
}
