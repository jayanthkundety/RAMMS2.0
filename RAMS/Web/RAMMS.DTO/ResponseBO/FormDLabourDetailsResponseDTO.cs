using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RAMMS.DTO.ResponseBO
{
    public class FormDLabourDetailsResponseDTO
    {
        [MapTo("FdldPkRefNo")]
        public int No { get; set; }

        [MapTo("FdldFdhPkRefNo")]
        public int? FdmdFdhPkRefNo { get; set; }

        [MapTo("FdldSrno")]
        public int? SerialNo { get; set; }

        [MapTo("FdldLabCode")]
        public string LabourCode { get; set; }

        [MapTo("FdldLabDesc")]
        public string LabourDesc { get; set; }

        [MapTo("FdldLabQty")]
        public int? Quantity { get; set; }

        [MapTo("FdldLabUnit")]
        public string Unit { get; set; }

        [MapTo("FdldModBy")]
        public string ModifiedBy { get; set; }

        [MapTo("FdldModDt")]
        public DateTime? ModifiedDate { get; set; }

        [MapTo("FdldCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("FdldCrDt")]
        public DateTime? CreatedDate { get; set; }

        [MapTo("FdldSubmitSts")]
        public bool SubmitStatus { get; set; }

        [MapTo("FdldActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo("FdldCodeDesc")]
        public string CodeDesc { get; set; }

        public FormDHeaderResponseDTO FormldFDHRefNoNavigation { get; set; }

        /// <summary>
        /// /
        /// </summary>


    }
}
