using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RAMMS.DTO.RequestBO
{
    public class FormDEquipRequestDTO
    {
        [MapTo("FdedPkRefNo")]
        public int No { get; set; }

        [MapTo("FdedFdhPkRefNo")]
        public int FormDEDFHeaderNo { get; set; }

        [MapTo("FdedSrno")]
        public int? SerialNo { get; set; }

        [MapTo("FdedEqpCode")]
        public string EquipmentCode { get; set; }

        [MapTo("FdedEqpDesc")]
        public string EquipmentDesc { get; set; }


        [MapTo("FdedEqpQty")]
        public decimal? Quantity { get; set; }

        [MapTo("FdedEqpUnit")]
        public string Unit { get; set; }

        [MapTo("FdedModBy")]
        public string ModifiedBy { get; set; }

        [MapTo("FdedModDt")]
        public DateTime? ModifiedDate { get; set; }

        [MapTo("FdedCrBy")]
        public string CreatedBy { get; set; }

        [MapTo("FdedCrDt")]
        public DateTime? CreatedDate { get; set; }

        [MapTo("FdedSubmitSts")]
        public bool SubmitStatus { get; set; }

        [MapTo("FdedActiveYn")]
        public bool? ActiveYn { get; set; }

        [MapTo ("FdedCodeDesc")]
        public string CodeDesc { get; set; }

        public FormDHeaderRequestDTO FormDedFDHRefNoNavigation { get; set; }

        /// <summary>
        /// /
        /// </summary>


    }
}
